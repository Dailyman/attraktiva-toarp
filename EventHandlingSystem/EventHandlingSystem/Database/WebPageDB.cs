using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;

namespace EventHandlingSystem.Database
{
    public class WebPageDB
    {
        private static readonly ATEntities Context = Database.Context;

        private static IEnumerable<webpages> GetAllNotDeletedWebPages()
        {
            return Context.webpages.Where(wP => !wP.IsDeleted);
        }

        public static webpages GetWebPageById(int id)
        {
            return GetAllNotDeletedWebPages().SingleOrDefault(wP => wP.Id.Equals(id));
        }

        public static webpages GetWebPageByCommunityId(int id)
        {
            return GetAllNotDeletedWebPages().SingleOrDefault(wP => wP.CommunityId.Equals(id));
        }

        public static webpages GetWebPageByAssociationId(int id)
        {
            return GetAllNotDeletedWebPages().SingleOrDefault(wP => wP.AssociationId.Equals(id));
        }

        // DELETE
        public static int DeleteWebPageByAssoId(int id)
        {
            webpages webpageToDelete = GetWebPageByAssociationId(id);

            if (webpageToDelete != null)
                webpageToDelete.IsDeleted = true;

            int affectedRows = Context.SaveChanges();
            return affectedRows;
        }

        //ADD
        public static bool AddWebPage(webpages wp)
        {
            Context.webpages.Add(wp);
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