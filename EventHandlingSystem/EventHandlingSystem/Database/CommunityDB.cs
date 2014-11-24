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
    }
}