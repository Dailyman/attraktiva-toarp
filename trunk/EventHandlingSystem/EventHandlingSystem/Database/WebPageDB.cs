using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;


namespace EventHandlingSystem.Database
{
    public class WebPageDB
    {
        private static readonly ATEntities Context = Database.Context;

        private static IEnumerable<webpages> GetAllNotDeletedWebPages()
        {
            return Context.webpages.Where(wP => !wP.IsDeleted);
        }

        public static List<webpages> GetAllCommunityWebpages()
        {
            return GetAllNotDeletedWebPages().Where(w => w.AssociationId.Equals(null)).ToList();
        }

        public static List<webpages> GetAllAssociationWebpages()
        {
            return GetAllNotDeletedWebPages().Where(w => w.CommunityId.Equals(null)).ToList();
        }

        public static List<webpages> GetAllAssociationWebpagesByCommunityId(int comId)
        {
            return GetAllNotDeletedWebPages().Where(w => w.CommunityId.Equals(comId)).ToList();
        }

        public static webpages GetWebPageById(int id)
        {
            return GetAllNotDeletedWebPages().SingleOrDefault(wP => wP.Id.Equals(id));
        }

        public static webpages GetWebPageByCommunityId(int id)
        {
            return GetAllNotDeletedWebPages().SingleOrDefault(wP => wP.CommunityId.Equals(id));
        }
        
        public static webpages GetWebPageByAssociationId(int id)
        {
            return GetAllNotDeletedWebPages().SingleOrDefault(wP => wP.AssociationId.Equals(id));
        }

        // DELETE
        public static int DeleteWebPageById(int id)
        {
            webpages webpageToDelete = GetWebPageById(id);

            if (webpageToDelete != null)
                webpageToDelete.IsDeleted = true;

            int affectedRows = Context.SaveChanges();
            return affectedRows;
        }

        public static int DeleteWebPageByAssoId(int id) 
        { 
            webpages webpageToDelete = GetWebPageByAssociationId(id); 

            if (webpageToDelete != null) 
                webpageToDelete.IsDeleted = true; 
            
            int affectedRows = Context.SaveChanges(); return affectedRows; 
        }

        public static int DeleteWebPageByCommId(int id)
        {
            webpages webpageToDelete = GetWebPageByCommunityId(id);

            if (webpageToDelete != null)
                webpageToDelete.IsDeleted = true;

            int affectedRows = Context.SaveChanges(); return affectedRows;
        }


        //ADD
        public static bool AddWebPage(webpages wp)
        {
            Context.webpages.Add(wp);
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

        // UPDATE
        public static int UpdateWebPage(webpages wp)
        {
            webpages wpToUpdate = GetWebPageById(wp.Id);

            wpToUpdate.Title = wp.Title;
            wpToUpdate.CommunityId = wp.CommunityId;
            wpToUpdate.AssociationId = wp.AssociationId;
            wpToUpdate.Layout = wp.Layout;
            wpToUpdate.Style = wp.Style;
            wpToUpdate.components = wp.components;
            wpToUpdate.UpdatedBy = wp.UpdatedBy;

            //Context.Entry(wpToUpdate).State = EntityState.Modified;
            
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
            
            Context.Entry(wpToUpdate).Reload();
            return affectedRows;
        }
    }
}