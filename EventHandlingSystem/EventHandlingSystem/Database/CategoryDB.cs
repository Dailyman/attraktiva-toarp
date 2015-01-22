using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventHandlingSystem.Database
{
    public class CategoryDB
    {
        private static readonly ATEntities Context = Database.Context;
       
        //GET
        private static IEnumerable<categories> GetAllNotDeletedCategories()
        {
            return Context.categories.Where(c => !c.IsDeleted);
        }

        public static List<categories> GetAllCategories()
        {
            return GetAllNotDeletedCategories().ToList();
        }

        public static categories GetCategoryById(int id)
        {
            return GetAllNotDeletedCategories().SingleOrDefault(c => c.Id.Equals(id));
        }

        public static categories GetCategoryByName(string name)
        {
            return GetAllNotDeletedCategories().SingleOrDefault(c => c.Name.Equals(name));
        }

        public static int UpdateCategory(categories category)
        {
            categories categoryToUpdate = GetCategoryById(category.Id);

            categoryToUpdate.Name = category.Name;
            categoryToUpdate.associations = category.associations;
            categoryToUpdate.subcategories = category.subcategories;

            return Context.SaveChanges();
        }
    }
}