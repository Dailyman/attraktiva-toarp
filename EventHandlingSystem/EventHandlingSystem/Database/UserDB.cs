using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Providers.Entities;

namespace EventHandlingSystem.Database
{
    public class UserDB
    {
        private static readonly ATEntities Context = Database.Context;

        // GET
        private static IEnumerable<users> GetAllNotDeletedUsers()
        {
            return Context.users.Where(u => !u.IsDeleted);
        }

        public static List<users> GetAllUsersByAssociation(associations a)
        {
            var usersInAssociation = new List<users>();
            foreach (
                var user in
                    AssociationPermissionsDB.GetAllAssociationPermissionsByAssociation(a)
                        .Select(aP => aP.users)
                        .Where(user => !usersInAssociation.Contains(user)))
            {
                usersInAssociation.Add(user);
            }
            return usersInAssociation;
        }

        public static List<users> GetAllUsersByCommunity(communities c)
        {
            var usersInCommunity = new List<users>();
            foreach (
                var user in
                    CommunityPermissionsDB.GetAllCommunityPermissionsByCommunity(c)
                        .Select(aP => aP.users)
                        .Where(user => !usersInCommunity.Contains(user)))
            {
                usersInCommunity.Add(user);
            }
            return usersInCommunity;
        }

        public static users GetUserById(int id)
        {
            return GetAllNotDeletedUsers().SingleOrDefault(u => u.Id.Equals(id));
        }

        public static users GetUserByUsername(string username)
        {
            return GetAllNotDeletedUsers().SingleOrDefault(u => u.Username.Equals(username));
        }

        // UPDATE
        public static int UpdateUser(users user)
        {
            users userToUpdate = GetUserById(user.Id);

            userToUpdate.Username = user.Username;
            

            int affectedRows;

            try
            {
                affectedRows = Context.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                foreach (DbEntityValidationResult item in ex.EntityValidationErrors)
                {
                    // Get entry

                    DbEntityEntry entry = item.Entry;
                    string entityTypeName = entry.Entity.GetType().Name;

                    // Display or log error messages

                    foreach (DbValidationError subItem in item.ValidationErrors)
                    {
                        string message = string.Format("Error '{0}' occurred in {1} at {2}",
                                 subItem.ErrorMessage, entityTypeName, subItem.PropertyName);
                        Console.WriteLine(message);
                    }
                    // Rollback changes

                    switch (entry.State)
                    {
                        case EntityState.Added:
                            entry.State = EntityState.Detached;
                            break;
                        case EntityState.Modified:
                            entry.CurrentValues.SetValues(entry.OriginalValues);
                            entry.State = EntityState.Unchanged;
                            break;
                        case EntityState.Deleted:
                            entry.State = EntityState.Unchanged;
                            break;
                    }
                }
                return affectedRows = 0;
            }
            Context.Entry(userToUpdate).Reload();
            return affectedRows;
        }

        //ADD
        public static bool AddUser(users user)
        {
            Context.users.Add(user);
            try
            {
                Context.SaveChanges();
            }
            catch (DbUpdateException dbEx)
            {
                return false;
            }
            catch (DbEntityValidationException ex)
            {
                foreach (DbEntityValidationResult item in ex.EntityValidationErrors)
                {
                    // Get entry
                    DbEntityEntry entry = item.Entry;
                    string entityTypeName = entry.Entity.GetType().Name;

                    // Display or log error messages
                    foreach (DbValidationError subItem in item.ValidationErrors)
                    {
                        string message = string.Format("Error '{0}' occurred in {1} at {2}",
                                 subItem.ErrorMessage, entityTypeName, subItem.PropertyName);
                        Console.WriteLine(message);
                    }

                    // Rollback changes
                    switch (entry.State)
                    {
                        case EntityState.Added:
                            entry.State = EntityState.Detached;
                            break;
                        case EntityState.Modified:
                            entry.CurrentValues.SetValues(entry.OriginalValues);
                            entry.State = EntityState.Unchanged;
                            break;
                        case EntityState.Deleted:
                            entry.State = EntityState.Unchanged;
                            break;
                    }
                }
                return false;
            }
            Context.Entry(user).Reload();
            return true;
        }

        // DELETE
        public static int DeleteUserById(int id)
        {
            users userToDelete = GetUserById(id);

            if (userToDelete != null)
                userToDelete.IsDeleted = true;

            int affectedRows;

            try
            {
                affectedRows = Context.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                foreach (DbEntityValidationResult item in ex.EntityValidationErrors)
                {
                    // Get entry

                    DbEntityEntry entry = item.Entry;
                    string entityTypeName = entry.Entity.GetType().Name;

                    // Display or log error messages

                    foreach (DbValidationError subItem in item.ValidationErrors)
                    {
                        string message = string.Format("Error '{0}' occurred in {1} at {2}",
                                 subItem.ErrorMessage, entityTypeName, subItem.PropertyName);
                        Console.WriteLine(message);
                    }
                    // Rollback changes

                    switch (entry.State)
                    {
                        case EntityState.Added:
                            entry.State = EntityState.Detached;
                            break;
                        case EntityState.Modified:
                            entry.CurrentValues.SetValues(entry.OriginalValues);
                            entry.State = EntityState.Unchanged;
                            break;
                        case EntityState.Deleted:
                            entry.State = EntityState.Unchanged;
                            break;
                    }
                }
                return affectedRows = 0;
            }
            return affectedRows;
        }
    }
}