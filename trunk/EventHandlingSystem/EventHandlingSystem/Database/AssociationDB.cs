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
    }
}