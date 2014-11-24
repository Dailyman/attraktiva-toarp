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

        public static Association GetAssociationById(int id)
        {
            return GetAllNotDeletedAssociations().SingleOrDefault(a => a.Id.Equals(id));
        }


    }
}