using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventHandlingSystem.Database
{
    public class TermSetDB
    {
        private static readonly EventHandlingDataModelContainer Context = Database.Context;

        private static IEnumerable<TermSet> GetAllNotDeletedTermSets()
        {
            return Context.TermSets.Where(ts => !ts.IsDeleted);
        }

        public static TermSet GetTermSetById(int id)
        {
            return GetAllNotDeletedTermSets().SingleOrDefault(ts => ts.Id.Equals(id));
        }

    }
}