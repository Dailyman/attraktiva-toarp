using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;

namespace EventHandlingSystem.Database
{
    public class AssociationDB
    {
        private static readonly ATEntities Context = Database.Context;

        // GET
        private static IEnumerable<associations> GetAllNotDeletedAssociations()
        {
            return Context.associations.Where(a => !a.IsDeleted);
        }

        public static List<associations> GetAllAssociations()
        {
            return GetAllNotDeletedAssociations().ToList();
        }

        public static associations GetAssociationById(int id)
        {
            return GetAllNotDeletedAssociations().SingleOrDefault(a => a.Id.Equals(id));
        }

        public static associations GetAssociationByName(string name)
        {
            return GetAllNotDeletedAssociations().SingleOrDefault(a => a.Name.Equals(name));
        }

        public static List<associations> GetAllAssociationsInCommunity(communities com)
        {
            return GetAllNotDeletedAssociations().Where(a => a.communities.Equals(com)).ToList();
        }

        public static List<associations> GetAllParentAssociations()
        {
            return GetAllNotDeletedAssociations().Where(a => a.ParentAssociationId.Equals(null)).ToList();
        }

        public static List<associations> GetAllParentAssociationsByCommunityId(int id)
        {
            return GetAllNotDeletedAssociations().Where(a => a.ParentAssociationId.Equals(null) && a.Communities_Id.Equals(id)).ToList();
        }

        public static List<categories> GetAllCategoriesForAssociationByAssociation(associations a)
        {
            List<categories> catList = a.categories.ToList(); 
            return catList;
        }
        
        public static List<associations> GetAllSubAssociationsByParentAssociationId(int id)
        {
            return GetAllNotDeletedAssociations().Where(sa => sa.ParentAssociationId.Equals(id)).ToList();
        }


        // UPDATE
        public static int UpdateAssociation(associations assoc)
        {
            associations assoToUpdate = GetAssociationById(assoc.Id);

            assoToUpdate.Name = assoc.Name; 
            assoToUpdate.Communities_Id = assoc.Communities_Id;
            assoToUpdate.ParentAssociationId = assoc.ParentAssociationId;
            assoToUpdate.Description = assoc.Description;
            assoToUpdate.categories = assoc.categories;
            assoToUpdate.users = assoc.users;

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
                Context.Entry(assoToUpdate).Reload();
                return affectedRows = 0;
            }
            
            
            return affectedRows;
        }


        // DELETE
        public static int DeleteAssociationById(int id)
        {
            associations assoToDelete = GetAssociationById(id);

            if (assoToDelete != null)
                assoToDelete.IsDeleted = true;

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


        //ADD
        public static bool AddAssociation(associations asso)
        {
            Context.associations.Add(asso);
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

        //Not: Har inte fått metoden att funka när jag testade den /E
        public static bool AddAssociationWithWebPage(associations asso, webpages wp)
        {
            Context.associations.Add(asso);
            WebPageDB.AddWebPage(wp);
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

        //// Categories In Associations

        //// GET
        //public static IEnumerable<categoriesinassociations> GetAllCIA()
        //{
        //    return Context.categoriesinassociations;
        //}

        //public static List<categoriesinassociations> GetCIAbyAssociationId(int aid)
        //{
        //    return GetAllCIA().Where(cia => cia.Associations_Id.Equals(aid)).ToList();
        //}

        //// ADD
        //public static bool AddCategoriesToAssociation(categoriesinassociations cia)
        //{
        //    Context.categoriesinassociations.Add(cia);
        //    try
        //    {
        //        Context.SaveChanges();
        //    }
        //    catch (DbUpdateException dbEx)
        //    {
        //        return false;
        //    }
        //    return true;
        //}

        //// DELETE
        //public static int RemoveCIAByAssoIdAndCatId(int aId, int cId)
        //{
        //    //Hitta CIA-objektet mha aId, cId
        //    categoriesinassociations ciaToDelete = new categoriesinassociations();

        //    GetCIAbyAssociationId(aId);

        //}

            
        //{
        //    Context.categoriesinassociations.Add(cia);
        //    try
        //    {
        //        Context.SaveChanges();
        //    }
        //    catch (DbUpdateException dbEx)
        //    {
        //        return false;
        //    }
        //    return true;
        //}
    }
}