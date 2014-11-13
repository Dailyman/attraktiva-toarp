﻿using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Web;

namespace EventHandlingSystem.Database
{
    public class EventDB
    {
        private static readonly EventHandlingDataModelContainer Context = Database.Context;

        
        private static IEnumerable<Event> GetAllNotDeletedEvents()
        {
            return Context.Events.Where(e => !e.IsDeleted);
        }

        public static List<Event> GetEventsBySpecifiedNumberOfPreviousMonthsFromToday(int nrOfMonths = 3)
        {
            return GetAllNotDeletedEvents().Where(e => e.StartDate > (DateTime.Now.AddMonths(-(nrOfMonths)))).ToList();
        }

        public static List<Event> GetEventsFromSpecifiedStartDate(DateTime startDate)
        {
            return GetAllNotDeletedEvents().Where(e => e.StartDate > startDate).ToList();
        } 

        public static Event GetEventById(int id)
        {
            return GetAllNotDeletedEvents().SingleOrDefault(e => e.Id.Equals(id));
        }

        public static bool AddEvent(Event @event)
        {
            Context.Events.Add(@event);
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

        public static int UpdateEvent(Event @event)
        {
            Event eventToUpdate = GetEventById(@event.Id);

            eventToUpdate.Title = @event.Title;
            eventToUpdate.StartDate = @event.StartDate;
            eventToUpdate.EndDate = @event.EndDate;
            eventToUpdate.ApproximateAttendees = @event.ApproximateAttendees;
            eventToUpdate.Created = @event.Created;
            eventToUpdate.CreatedBy = @event.CreatedBy;

            int affectedRows = Context.SaveChanges();
            return affectedRows;
        }
        
        
    }
}