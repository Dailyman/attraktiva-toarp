using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
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

            return Context.SaveChanges();
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