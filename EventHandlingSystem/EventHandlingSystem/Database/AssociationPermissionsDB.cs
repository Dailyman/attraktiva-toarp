using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;

namespace EventHandlingSystem.Database
{
    public class AssociationPermissionsDB
    {
        private static readonly ATEntities Context = Database.Context;

        // GET
        private static IEnumerable<association_permissions> GetAllNotDeletedAssociationPermissions()
        {
            return Context.association_permissions.Where(e => !e.IsDeleted);
        }

        // This might not be working correctly!
        // This might not be working correctly!
        // This might not be working correctly!
        public static List<association_permissions> GetAllAssociationPermissionsByAssociation(associations asso)
        {
            return GetAllNotDeletedAssociationPermissions().Where(p => p.associations_Id.Equals(asso.Id)).ToList();
        }

        public static association_permissions GetAssociationPermissionsById(int id)
        {
            return GetAllNotDeletedAssociationPermissions().SingleOrDefault(p => p.Id.Equals(id));
        }

        public static bool HasUserPermissionForAssociation(users u, associations a)
        {
            return GetAllAssociationPermissionsByAssociation(a).Any(associationPermission => associationPermission.users.Id == u.Id);
        }

        // UPDATE
        public static int UpdateAssociationPermissions(association_permissions aP)
        {
            association_permissions aPToUpdate = GetAssociationPermissionsById(aP.Id);

            aPToUpdate.users = aP.users;
            aPToUpdate.associations = aP.associations;
            aPToUpdate.Role = aP.Role;
            

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
            Context.Entry(aPToUpdate).Reload();
            return affectedRows;
        }

        //ADD
        public static bool AddAssociationPermissions(association_permissions aP)
        {
            Context.association_permissions.Add(aP);
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
            Context.Entry(aP).Reload();
            return true;
        }

        // DELETE
        public static int DeleteAssociationPermissionsById(int id)
        {
            association_permissions aPToDelete = GetAssociationPermissionsById(id);

            if (aPToDelete != null)
                aPToDelete.IsDeleted = true;

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