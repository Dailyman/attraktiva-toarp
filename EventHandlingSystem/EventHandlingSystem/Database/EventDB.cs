using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Migrations;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;

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

        public static List<events> GetAllEventsInMonth(DateTime date)
        {
            return
                GetAllNotDeletedEvents()
                    .Where(e => e.StartDate.Month.Equals(date.Month) || e.EndDate.Month.Equals(date.Month))
                    .ToList();
        }

        // ADD
        public static bool AddEvent(events ev)
        {
            Context.events.Add(ev);
            try
            {
                Context.SaveChanges();
            }
            //catch (DbEntityValidationException ex)
            //{
            //    using (var sw = new StreamWriter(File.Open(@"C:\Users\Robin\Desktop\myfile2.txt", FileMode.CreateNew), Encoding.GetEncoding("iso-8859-1")))
            //    {
            //        foreach (var str in ex.EntityValidationErrors)
            //        {
            //            foreach (var valErr in str.ValidationErrors)
            //            {
            //                sw.WriteLine(str.Entry + "###" + valErr.PropertyName + "###" + valErr.ErrorMessage);
            //            }
                        
            //        }
            //    }
            //}
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
        public static int UpdateEvent(events ev)
        {
            events eventToUpdate = GetEventById(ev.Id);

            eventToUpdate.Title = ev.Title;
            eventToUpdate.Description = ev.Description;
            eventToUpdate.Summary = ev.Summary;
            eventToUpdate.Other = ev.Other;
            eventToUpdate.Location = ev.Location;
            eventToUpdate.ImageUrl = ev.ImageUrl;
            eventToUpdate.DayEvent = ev.DayEvent;
            eventToUpdate.StartDate = ev.StartDate;
            eventToUpdate.EndDate = ev.EndDate;
            eventToUpdate.TargetGroup = ev.TargetGroup;
            eventToUpdate.ApproximateAttendees = ev.ApproximateAttendees;
            eventToUpdate.subcategories = ev.subcategories;
            //if (eventToUpdate.associationsinevents.Count() != 0)
            //{
            //    for (int i = 0; i < eventToUpdate.associationsinevents.Count(); i++)
            //    {
            //        eventToUpdate.associationsinevents.Remove(eventToUpdate.associationsinevents.ElementAt(i));
            //        Database.Context.associationsinevents.AddOrUpdate(eventToUpdate.associationsinevents.ElementAt(i));
            //    }
            //}
            //foreach (var associationsinevents in ev.associations)
            //{
            //    eventToUpdate.associations.Add(associationsinevents);
            //}
            eventToUpdate.associations = ev.associations;
            eventToUpdate.communities = ev.communities;
            
            int affectedRows = Context.SaveChanges();
            return affectedRows;
        }
    }
}