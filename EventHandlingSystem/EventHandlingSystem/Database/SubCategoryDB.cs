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
            return Context.subcategories.Where(sc => !sc.IsDeleted);
        }

        public static subcategories GetSubCategoryById(int id)
        {
            return GetAllNotDeletedSubCategories().SingleOrDefault(sc => sc.Id.Equals(id));
        }

        public static List<subcategories> GetAllSubCategoryByCategory(categories cat)
        {
            return Context.subcategories.Where(sc => sc.categories.Equals(cat)).ToList();
        }
    }
}