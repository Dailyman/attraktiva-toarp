using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;

namespace EventHandlingSystem.Database
{
    public class CommunityPermissionsDB
    {
        private static readonly ATEntities Context = Database.Context;

        // GET
        private static IEnumerable<community_permissions> GetAllNotDeletedCommunityPermissions()
        {
            return Context.community_permissions.Where(cP => !cP.IsDeleted);
        }

        public static List<community_permissions> GetAllCommunityPermissionsByCommunity(communities comm)
        {
            return GetAllNotDeletedCommunityPermissions().Where(p => p.communities_Id.Equals(comm.Id)).ToList();
        }

        public static List<community_permissions> GetAllCommunityPermissionsByUser(users user)
        {
            return GetAllNotDeletedCommunityPermissions().Where(p => p.users_Id.Equals(user.Id)).ToList();
        }

        public static community_permissions GetCommunityPermissionsById(int id)
        {
            return GetAllNotDeletedCommunityPermissions().SingleOrDefault(p => p.Id.Equals(id));
        }

        public static bool HasUserPermissionForCommunity(users u, communities c)
        {
            return GetAllCommunityPermissionsByCommunity(c).Any(cP => cP.users.Id == u.Id);
        }

        public static bool HasUserPermissionForCommunityWithRole(users u, communities c, string r)
        {
            return GetAllCommunityPermissionsByCommunity(c).Any(cP => cP.users.Id == u.Id && cP.Role.Equals(r));
        }

        public static bool HasUserPermissionWithRole(users u, string r)
        {
            return GetAllCommunityPermissionsByUser(u).Any(cP => cP.Role.Equals(r));
        }

        // UPDATE
        public static int UpdateCommunityPermissions(community_permissions cP)
        {
            community_permissions cPToUpdate = GetCommunityPermissionsById(cP.Id);

            cPToUpdate.users = cP.users;
            cPToUpdate.communities = cP.communities;
            cPToUpdate.Role = cP.Role;

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
            Context.Entry(cPToUpdate).Reload();
            return affectedRows;
        }

        //ADD
        public static bool AddCommunityPermissions(community_permissions cP)
        {
            Context.community_permissions.Add(cP);
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
            Context.Entry(cP).Reload();
            return true;
        }

        // DELETE
        public static int DeleteCommunityPermissionsById(int id)
        {
            community_permissions cPToDelete = GetCommunityPermissionsById(id);

            if (cPToDelete != null)
                cPToDelete.IsDeleted = true;

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