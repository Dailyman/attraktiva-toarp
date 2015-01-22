using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventHandlingSystem.Database
{
    public class SubCategoryDB
    {
        private static readonly ATEntities Context = new ATEntities();

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
            return GetAllSubCategories().Where(sc => sc.Categories_Id == (cat.Id)).ToList();
        }

        public static int UpdateSubCategory(subcategories subCategory)
        {
            subcategories subCategoryToUpdate = GetSubCategoryById(subCategory.Id);

            subCategoryToUpdate.Name = subCategory.Name;
            subCategoryToUpdate.Categories_Id = subCategory.Categories_Id;
            subCategoryToUpdate.categories = subCategory.categories;
            subCategoryToUpdate.events = subCategory.events;

            return Context.SaveChanges();
        }
    }
}