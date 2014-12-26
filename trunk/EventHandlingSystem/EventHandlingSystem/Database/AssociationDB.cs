using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventHandlingSystem.Database
{
    public class AssociationDB
    {
        private static readonly EventHandlingDataModelContainer Context = Database.Context;


        // GET
        private static IEnumerable<Association> GetAllNotDeletedAssociations()
        {
            return Context.Associations.Where(a => !a.IsDeleted);
        }

        public static List<Association> GetAllAssociations()
        {
            return GetAllNotDeletedAssociations().ToList();
        } 

        public static Association GetAssociationById(int id)
        {
            return GetAllNotDeletedAssociations().SingleOrDefault(a => a.Id.Equals(id));
        }

        public static Association GetAssociationByPublishingTermSetId(int id)
        {
            return GetAllNotDeletedAssociations().SingleOrDefault(a => a.PublishingTermSetId.Equals(id));
        }

        public static List<Association> GetAllAssociationsInCommunity(Community com)
        {
            return GetAllNotDeletedAssociations().Where(a => a.Community.Equals(com)).ToList();
        }

        public static string GetAssocationNameByPublishingTermSetId(int id)
        {
            return TermSetDB.GetTermSetNameByTermSetId(id);
        }

        public static List<Association> GetAllParentAssociations()
        {
            return GetAllNotDeletedAssociations().Where(a => a.ParentAssociationId.Equals(null)).ToList();
        }

        public static List<Association> GetAllAssociationsWithAssociationType()
        {
           return GetAllNotDeletedAssociations().Where(a => !a.AssociationType.Equals(null)).ToList();
        }

        public static List<Association> GetAllSubAssociationsByParentAssociationId(int id)
        {
            return GetAllNotDeletedAssociations().Where(sa => sa.ParentAssociationId.Equals(id)).ToList();
        }

        public static int GetPublishingTermSetIdByAssociationId(int id)
        {
            Association asso = GetAssociationById(id);
            return asso.PublishingTermSetId;
        }


        // UPDATE
        public static int UpdateAssociation(Association assoc)
        {
            Association assoToUpdate = GetAssociationById(assoc.Id);

            assoToUpdate.CommunityId = assoc.CommunityId;
            assoToUpdate.ParentAssociationId = assoc.ParentAssociationId;
            assoToUpdate.AssociationType = assoc.AssociationType;

            int affectedRows = Context.SaveChanges();
            return affectedRows;
        }


        // DELETE
        public static int DeleteAssociationById(int id)
        {
            Association assoToDelete = GetAssociationById(id);

            if (assoToDelete != null)
                assoToDelete.IsDeleted = true;

            int affectedRows = Context.SaveChanges();
            return affectedRows;
        }
    }
}