using System;
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
        private static readonly ATEntities Context = Database.Context;

        // GET
        private static IEnumerable<events> GetAllNotDeletedEvents()
        {
            return Context.events.Where(e => !e.IsDeleted);
        }

        public static List<events> GetEventsBySpecifiedNumberOfPreviousMonthsFromToday(int nrOfMonths = 3)
        {
            return GetAllNotDeletedEvents().Where(e => e.StartDate > (DateTime.Now.AddMonths(-(nrOfMonths)))).ToList();
        }

        public static List<events> GetEventsFromSpecifiedStartDate(DateTime startDate)
        {
            return GetAllNotDeletedEvents().Where(e => e.StartDate > startDate).ToList();
        }

        public static events GetEventById(int id)
        {
            return GetAllNotDeletedEvents().SingleOrDefault(e => e.Id.Equals(id));
        }


        // ADD
        public static bool AddEvent(events @event)
        {
            Context.events.Add(@event);
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

        // CREATE
        //    public static int CreateTerm(Term term)
        //    {
        //        Term termToCreate = new Term
        //        {
        //            Name = !string.IsNullOrWhiteSpace(term.Name) ? term.Name : "Untitled",
        //            TermSet = term.TermSet,
        //            Created = DateTime.Now
        //        };

        //        Context.Terms.Add(termToCreate);

        //        int affectedRows = Context.SaveChanges();
        //        return affectedRows;
        //    }


        // UPDATE
        public static int UpdateEvent(events @event)
        {
            events eventToUpdate = GetEventById(@event.Id);

            eventToUpdate.Title = @event.Title;
            eventToUpdate.Description = @event.Description;
            eventToUpdate.Summary = @event.Summary;
            eventToUpdate.Other = @event.Other;
            eventToUpdate.Location = @event.Location;
            eventToUpdate.ImageUrl = @event.ImageUrl;
            eventToUpdate.DayEvent = @event.DayEvent;
            eventToUpdate.StartDate = @event.StartDate;
            eventToUpdate.EndDate = @event.EndDate;
            eventToUpdate.TargetGroup = @event.TargetGroup;
            eventToUpdate.ApproximateAttendees = @event.ApproximateAttendees;
            
            int affectedRows = Context.SaveChanges();
            return affectedRows;
        }
    }
}