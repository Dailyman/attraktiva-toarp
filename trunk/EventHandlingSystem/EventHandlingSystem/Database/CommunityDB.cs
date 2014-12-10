using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventHandlingSystem.Database
{
    public class CommunityDB
    {
        private static readonly EventHandlingDataModelContainer Context = Database.Context;

        private static IEnumerable<Community> GetAllNotDeletedCommunities()
        {
            return Context.Communities.Where(c => !c.IsDeleted);
        }

        public static Community GetCommunityById(int id)
        {
            return GetAllNotDeletedCommunities().SingleOrDefault(c => c.Id.Equals(id));
        }

        public static List<Community> GetAllCommunities()
        {
            return GetAllNotDeletedCommunities().ToList();
        }

        public static Community GetCommunityByPublishingTermSetId(int id)
        {
            return GetAllNotDeletedCommunities().SingleOrDefault(c => c.PublishingTermSetId.Equals(id));
        }

        public static string GetCommunityNameByPublishingTermSetId(int id)
        {
            return TermSetDB.GetTermSetNameByTermSetId(id);
        }
    }
}