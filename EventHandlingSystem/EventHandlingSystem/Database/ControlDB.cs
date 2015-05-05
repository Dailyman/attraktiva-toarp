using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;

namespace EventHandlingSystem.Database
{
    public class ControlDB
    {
        private static readonly ATEntities Context = Database.Context;

        // GET
        private static IEnumerable<controls> GetAllNotDeletedControls()
        {
            return Context.controls.Where(c => !c.IsDeleted);
        }

        public static List<controls>GetAllControls()
        {
            return GetAllNotDeletedControls().ToList();
        }

        public static List<controls> GetAllControlsNotInWebpage(webpages wP)
        {
            var controlsNotInWebPage = GetAllControls();
            if (wP != null)
            {
               foreach (var c in wP.components.Where(c => !c.IsDeleted))
            {
                if (GetAllControls().Contains(c.controls))
                {
                    controlsNotInWebPage.Remove(c.controls);
                }
            } 
            }
            

            return controlsNotInWebPage;
        }


        public static controls GetControlsById(int id)
        {
            return GetAllNotDeletedControls().SingleOrDefault(c => c.Id.Equals(id));
        }
        

        // ADD
        public static bool AddControl(controls c)
        {

            Context.controls.Add(c);
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
            Context.Entry(c).Reload();
            return true;
        }


        // UPDATE
        public static int UpdateControl(controls c)
        {
            controls controlsToUpdate = GetControlsById(c.Id);

            controlsToUpdate.Name = c.Name;
            controlsToUpdate.FilePath = c.FilePath;
            controlsToUpdate.components = c.components;

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
            Context.Entry(controlsToUpdate).Reload();
            return affectedRows;
        }



        // DELETE
        public static bool DeleteControl(controls c)
        {
            controls controlToDelete = GetControlsById(c.Id);

            controlToDelete.IsDeleted = true;

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
                return false;
            }
            return affectedRows > 0;
        }
    }
}