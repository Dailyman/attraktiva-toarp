using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventHandlingSystem.Database
{
    public class CommunityDB
    {
        private static readonly ATEntities Context = Database.Context;

    //    // GET
        private static IEnumerable<communities> GetAllNotDeletedCommunities()
        {
            return Context.communities.Where(c => !c.IsDeleted);
        }

        public static communities GetCommunityById(int id)
        {
            return GetAllNotDeletedCommunities().SingleOrDefault(c => c.Id.Equals(id));
        }

        public static List<communities> GetAllCommunities()
        {
            return GetAllNotDeletedCommunities().ToList();
        }
    }
}