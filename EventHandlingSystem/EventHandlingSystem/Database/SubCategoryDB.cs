using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;

namespace EventHandlingSystem.Database
{
    public class SubCategoryDB
    {
        private static readonly ATEntities Context = Database.Context;

        // GET
        private static IEnumerable<subcategories> GetAllNotDeletedSubCategories()
        {
            
                return Context.subcategories.Where(sc => !sc.IsDeleted).ToList();
            
        }
        public static List<subcategories> GetAllSubCategories()
        {
            return GetAllNotDeletedSubCategories().ToList();
        }

        public static subcategories GetSubCategoryById(int id)
        {
            
            return GetAllNotDeletedSubCategories().SingleOrDefault(sc => sc.Id.Equals(id));
            
        }

        public static List<subcategories> GetAllSubCategoryByCategory(categories cat)
        {
            return GetAllSubCategories().Where(sc => sc.categories_Id == (cat.Id)).ToList();
        }

        public static List<subcategories> GetAllSubCategoriesByAssociations(associations[] asso)
        {
            List<subcategories> subCateList = new List<subcategories>();
            foreach (associations a in asso)
            {
                foreach (var c in a.categories)
                {
                    subCateList.AddRange(GetAllSubCategoryByCategory(c));
                }
            }
            return subCateList;
        }

        //ADD
        public static bool AddSubCategory(subcategories sC)
        {
            Context.subcategories.Add(sC);
            try
            {
                Context.SaveChanges();
            }
            catch (DbUpdateException dbEx)
            {
                return false;
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }


        //UPDATE
        public static int UpdateSubCategory(subcategories subCategory)
        {
            subcategories subCategoryToUpdate = GetSubCategoryById(subCategory.Id);

            subCategoryToUpdate.Name = subCategory.Name;
            subCategoryToUpdate.categories_Id = subCategory.categories_Id;
            subCategoryToUpdate.categories = subCategory.categories;
            subCategoryToUpdate.events = subCategory.events;
            

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
            Context.Entry(subCategoryToUpdate).Reload();
            return affectedRows;
        }

        // DELETE
        public static int DeleteSubCategoryById(int id)
        {
            subcategories subCategoryToDelete = GetSubCategoryById(id);

            if (subCategoryToDelete != null)
                subCategoryToDelete.IsDeleted = true;

            int affectedRows = Context.SaveChanges();
            return affectedRows;
        }
    }
}