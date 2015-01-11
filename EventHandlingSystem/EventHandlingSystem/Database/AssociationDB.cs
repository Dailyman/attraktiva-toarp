using System;
using System.Collections.Generic;
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
            List<categories> cat = new List<categories>();
            foreach (var categoryInAssociation in a.categoriesinassociations)
            {
               cat.Add(CategoryDB.GetCategoryById(categoryInAssociation.Categories_Id));
            }
            
            return cat;
        }

        //public static List<associations> GetAllAssociationsWithAssociationType()
        //{
        //    return GetAllNotDeletedAssociations().Where(a => !a.AssociationType.Equals(null)).ToList();
        //}

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
            assoToUpdate.categoriesinassociations = assoc.categoriesinassociations;

            int affectedRows = Context.SaveChanges();
            return affectedRows;
        }


        // DELETE
        public static int DeleteAssociationById(int id)
        {
            associations assoToDelete = GetAssociationById(id);

            if (assoToDelete != null)
                assoToDelete.IsDeleted = true;

            int affectedRows = Context.SaveChanges();
            return affectedRows;
        }
    }
}