using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventHandlingSystem.Database
{
    public class AssociationDB
    {
        private static readonly EventHandlingDataModelContainer Context = Database.Context;

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
            return GetAllAssociations().Where(a => a.Community.Equals(com)).ToList();
        }

        public static string GetAssocationNameByPublishingTermSetId(int id)
        {
            return TermSetDB.GetTermSetNameByTermSetId(id);
        }
    }
}