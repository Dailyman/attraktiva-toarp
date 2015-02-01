using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
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


        //ADD
        public static bool AddCategory(categories c)
        {
            Context.categories.Add(c);
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
        public static int UpdateCategory(categories category)
        {
            categories categoryToUpdate = GetCategoryById(category.Id);

            categoryToUpdate.Name = category.Name;
            categoryToUpdate.associations = category.associations;
            categoryToUpdate.subcategories = category.subcategories;

            return Context.SaveChanges();
        }

        // DELETE
        public static int DeleteCategoryById(int id)
        {
            categories categoryToDelete = GetCategoryById(id);

            if (categoryToDelete != null)
                categoryToDelete.IsDeleted = true;

            int affectedRows = Context.SaveChanges();
            return affectedRows;
        }

        // DELETE
        public static int DeleteCategoryAndSubCategoriesByCategoryId(int id)
        {
            categories categoryToDelete = GetCategoryById(id);

            foreach (var subCategory in categoryToDelete.subcategories)
            {
                SubCategoryDB.DeleteSubCategoryById(subCategory.Id);
            }

            if (categoryToDelete != null)
                categoryToDelete.IsDeleted = true;

            return Context.SaveChanges();
        }
    }
}