using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;

namespace EventHandlingSystem.Database
{
    public class CommunityDB
    {
        private static readonly ATEntities Context = Database.Context;

        // GET
        private static IEnumerable<communities> GetAllNotDeletedCommunities()
        {
            return Context.communities.Where(c => !c.IsDeleted);
        }

        public static communities GetCommunityById(int id)
        {
            return GetAllNotDeletedCommunities().SingleOrDefault(c => c.Id.Equals(id));
        }

        public static communities GetCommunityByName(string name)
        {
            return GetAllNotDeletedCommunities().SingleOrDefault(c => c.Name.Equals(name));
        }

        public static List<communities> GetAllCommunities()
        {
            return GetAllNotDeletedCommunities().ToList();
        }

        // UPDATE
        public static int UpdateCommunity(communities comm)
        {
            communities commToUpdate = GetCommunityById(comm.Id);

            commToUpdate.Name = comm.Name;
            int affectedRows = Context.SaveChanges();
            return affectedRows;
        }

        //ADD
        public static bool AddCommunity(communities comm)
        {
            Context.communities.Add(comm);
            try
            {
                Context.SaveChanges();
            }
            catch (DbUpdateException dbEx)
            {
                return false;
            }
            return true;
        }
    }
}