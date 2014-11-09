using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventHandlingSystem.Database
{
    public class EventDB
    {
        private  static  readonly  EventHandlingDataModelContainer  Context = new EventHandlingDataModelContainer();

        private static IEnumerable<Event> GetAllNotDeletedEvents()
        {
            return Context.Events.Where(e => !e.IsDeleted);
        }

        public static List<Event> GetEventsBySpecifiedNumberOfPreviousMonthsFromToday(int nrOfMonths = 3)
        {
            DateTime defaulTimeSpan = DateTime.Now.AddMonths(-(nrOfMonths));
            return GetAllNotDeletedEvents().Where(e => (e.StartDate > defaulTimeSpan)).ToList();
        }

        public static bool AddEvent(Event @event)
        {
            Context.Events.Add(@event);
            if (Context.SaveChanges() != 0)
            {
                return true;
            }
            return false;
        }
        
        
    }
}