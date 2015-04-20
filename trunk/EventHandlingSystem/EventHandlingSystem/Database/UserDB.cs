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

        //public static List<users> GetAllUsersByAssociationId(int id)
        //{
        //    List<users> usersInAsso = new List<users>();
        //    foreach (var notDeletedUser in GetAllNotDeletedUsers())
        //    {
        //        usersInAsso.AddRange(from association in notDeletedUser.associations where association.Id == id select notDeletedUser);
        //    }
        //    return usersInAsso;
        //}

        public static users GetUsersById(int id)
        {
            return GetAllNotDeletedUsers().SingleOrDefault(u => u.Id.Equals(id));
        }

        public static users GetUsersByUsername(string username)
        {
            return GetAllNotDeletedUsers().SingleOrDefault(u => u.Username.Equals(username));
        }

        // UPDATE
        public static int UpdateUser(users user)
        {
            users userToUpdate = GetUsersById(user.Id);

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
            return true;
        }

        // DELETE
        public static int DeleteUserById(int id)
        {
            users userToDelete = GetUsersById(id);

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