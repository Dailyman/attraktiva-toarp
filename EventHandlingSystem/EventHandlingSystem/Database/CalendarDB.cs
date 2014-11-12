using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventHandlingSystem.Database
{
    public class CalendarDB
    {
        private static readonly EventHandlingDataModelContainer Context = Database.Context;

        private static IEnumerable<Calendar> GetAllNotDeletedCalendars()
        {
            return Context.Calendars.Where(c => !c.IsDeleted);
        }

        public static Calendar GetCalendarById(int id)
        {
            return GetAllNotDeletedCalendars().SingleOrDefault(c => c.Id.Equals(id));
        }
            

    }
}