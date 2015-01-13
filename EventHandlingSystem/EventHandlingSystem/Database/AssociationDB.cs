using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
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
            List<categories> catList = a.categories.ToList(); 
            return catList;
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
            //assoToUpdate.communities = assoc.communities;
            assoToUpdate.ParentAssociationId = assoc.ParentAssociationId;
            assoToUpdate.categories = assoc.categories;

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