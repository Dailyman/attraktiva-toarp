using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Migrations;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using Microsoft.Ajax.Utilities;

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

        public static List<events> GetAllEvents()
        {
            return GetAllNotDeletedEvents().ToList();
        }

        public static List<events> GetEventsBySpecifiedNumberOfPreviousMonthsFromToday(int nrOfMonths = 3)
        {
            return GetAllNotDeletedEvents().Where(e => e.StartDate > (DateTime.Now.AddMonths(-(nrOfMonths)))).ToList();
        }

        public static List<events> GetEventsBySpecifiedNumberOfMonthsFromToday(int nrOfMonths = 3)
        {
            return GetAllNotDeletedEvents().Where(e => 
                e.StartDate <= (DateTime.Now.AddMonths((nrOfMonths))) 
                && e.StartDate >= DateTime.Now 
                && e.EndDate <= (DateTime.Now.AddMonths((nrOfMonths))) 
                && e.EndDate >= DateTime.Now).ToList();
        }

        public static List<events> GetEventsFromSpecifiedStartDate(DateTime startDate)
        {
            return GetAllNotDeletedEvents().Where(e => e.StartDate >= startDate).ToList();
        }

        public static List<events> GetEventsByRangeDate(DateTime startDate, DateTime endDate)
        {
            return GetAllNotDeletedEvents().Where(e => e.StartDate >= startDate && e.EndDate <= endDate).ToList();
        }

        public static List<events> GetEventsBySearchWord(string searchStr)
        {
            return
                GetAllNotDeletedEvents()
                    .Where(
                        e =>
                            (
                                (!String.IsNullOrEmpty(e.Title)
                                    ? e.Title.IndexOf(searchStr, StringComparison.OrdinalIgnoreCase) < 0
                                        ? 0
                                        : e.Title.IndexOf(searchStr, StringComparison.OrdinalIgnoreCase) >= 0 ? 1 : 0
                                    : 0) +
                                (!String.IsNullOrEmpty(e.Description)
                                    ? e.Description.IndexOf(searchStr, StringComparison.OrdinalIgnoreCase) < 0
                                        ? 0
                                        : e.Description.IndexOf(searchStr, StringComparison.OrdinalIgnoreCase) >= 0 ? 1 : 0
                                    : 0) +
                                (!String.IsNullOrEmpty(e.Summary)
                                    ? e.Summary.IndexOf(searchStr, StringComparison.OrdinalIgnoreCase) < 0
                                        ? 0
                                        : e.Summary.IndexOf(searchStr, StringComparison.OrdinalIgnoreCase) >= 0 ? 1 : 0
                                    : 0) +
                                (!String.IsNullOrEmpty(e.Other)
                                    ? e.Other.IndexOf(searchStr, StringComparison.OrdinalIgnoreCase) < 0
                                        ? 0
                                        : e.Other.IndexOf(searchStr, StringComparison.OrdinalIgnoreCase) >= 0 ? 1 : 0
                                    : 0) +
                                (!String.IsNullOrEmpty(e.TargetGroup)
                                    ? e.TargetGroup.IndexOf(searchStr, StringComparison.OrdinalIgnoreCase) < 0
                                        ? 0
                                        : e.TargetGroup.IndexOf(searchStr, StringComparison.OrdinalIgnoreCase) >= 0 ? 1 : 0
                                    : 0) +
                                (!String.IsNullOrEmpty(e.Location)
                                    ? e.Location.IndexOf(searchStr, StringComparison.OrdinalIgnoreCase) < 0
                                        ? 0
                                        : e.Location.IndexOf(searchStr, StringComparison.OrdinalIgnoreCase) >= 0 ? 1 : 0
                                    : 0)
                                ) > 0)
                    .ToList();
        }

        public static List<events> GetEventsFromListBySearchWord(List<events> evList, string searchStr)
        {
            return
                evList.Where(
                    e =>
                        (
                            (!String.IsNullOrEmpty(e.Title)
                                ? e.Title.IndexOf(searchStr, StringComparison.OrdinalIgnoreCase) < 0
                                    ? 0
                                    : e.Title.IndexOf(searchStr, StringComparison.OrdinalIgnoreCase) >= 0 ? 1 : 0
                                : 0) +
                            (!String.IsNullOrEmpty(e.Description)
                                ? e.Description.IndexOf(searchStr, StringComparison.OrdinalIgnoreCase) < 0
                                    ? 0
                                    : e.Description.IndexOf(searchStr, StringComparison.OrdinalIgnoreCase) >= 0 ? 1 : 0
                                : 0) +
                            (!String.IsNullOrEmpty(e.Summary)
                                ? e.Summary.IndexOf(searchStr, StringComparison.OrdinalIgnoreCase) < 0
                                    ? 0
                                    : e.Summary.IndexOf(searchStr, StringComparison.OrdinalIgnoreCase) >= 0 ? 1 : 0
                                : 0) +
                            (!String.IsNullOrEmpty(e.Other)
                                ? e.Other.IndexOf(searchStr, StringComparison.OrdinalIgnoreCase) < 0
                                    ? 0
                                    : e.Other.IndexOf(searchStr, StringComparison.OrdinalIgnoreCase) >= 0 ? 1 : 0
                                : 0) +
                            (!String.IsNullOrEmpty(e.TargetGroup)
                                ? e.TargetGroup.IndexOf(searchStr, StringComparison.OrdinalIgnoreCase) < 0
                                    ? 0
                                    : e.TargetGroup.IndexOf(searchStr, StringComparison.OrdinalIgnoreCase) >= 0 ? 1 : 0
                                : 0) +
                            (!String.IsNullOrEmpty(e.Location)
                                ? e.Location.IndexOf(searchStr, StringComparison.OrdinalIgnoreCase) < 0
                                    ? 0
                                    : e.Location.IndexOf(searchStr, StringComparison.OrdinalIgnoreCase) >= 0 ? 1 : 0
                                : 0)
                            ) > 0)
                    .ToList();
        }
        
        public static List<events> GetEventsByCommunity(communities comm)
        {
            return comm.events.ToList();
        }

        public static List<events> GetEventsByAssociation(associations asso)
        {
            return asso.events.ToList();
        }

        public static List<events> FilterEvents(DateTime? sT, DateTime? eT, communities c, associations a, categories cat, subcategories subCat, string word)
        {
            List<events> resultList = GetAllEvents();

            if (sT != null)
            {
                resultList = resultList.Where(e => e.StartDate >= sT).ToList();
            }

            if (eT != null)
            {
                resultList = resultList.Where(e => e.EndDate <= eT).ToList();
            }

            if (c != null)
            {

                var preResultList = new List<events>(); 

                preResultList.AddRange(resultList.Where(e => e.communities.Contains(c)).ToList());

                List<events> list = resultList;
                foreach (var ev in c.associations.SelectMany(asso => list.Where(e => e.associations.Contains(asso)).Where(ev => !preResultList.Contains(ev))))
                {
                    preResultList.Add(ev);
                }

                resultList = preResultList;
            }

            if (a != null)
            {
                resultList = resultList.Where(e => e.associations.Contains(a)).ToList();
            }

            if (cat != null)
            {
                List<events> eventsInAsso = new List<events>();
                foreach (var asso in cat.associations)
                {
                        eventsInAsso.AddRange(resultList.Where(e => e.associations.Contains(asso)));
                }

                resultList = eventsInAsso;

            }

            if (subCat != null)
            {
                resultList = resultList.Where(e => e.subcategories.Contains(subCat)).ToList();
            }

            if (!String.IsNullOrWhiteSpace(word))
            {
                resultList = GetEventsFromListBySearchWord(resultList, word);
            }

            return resultList;
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
            catch (DbEntityValidationException ex)
            {
                foreach (DbEntityValidationResult item in ex.EntityValidationErrors)
                {
                    // Get entry

                    DbEntityEntry entry = item.Entry;
                    string entityTypeName = entry.Entity.GetType().Name;

                    // Display or log error messages

                    foreach (DbValidationError subItem in item.ValidationErrors)
                    {
                        string message = string.Format("Error '{0}' occurred in {1} at {2}",
                                 subItem.ErrorMessage, entityTypeName, subItem.PropertyName);
                        Console.WriteLine(message);
                    }
                    // Rollback changes

                    switch (entry.State)
                    {
                        case EntityState.Added:
                            entry.State = EntityState.Detached;
                            break;
                        case EntityState.Modified:
                            entry.CurrentValues.SetValues(entry.OriginalValues);
                            entry.State = EntityState.Unchanged;
                            break;
                        case EntityState.Deleted:
                            entry.State = EntityState.Unchanged;
                            break;
                    }
                }
                return false;
            }
            Context.Entry(ev).Reload();
            return true;
        }


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
            eventToUpdate.EventUrl = ev.EventUrl;
            eventToUpdate.DayEvent = ev.DayEvent;
            eventToUpdate.StartDate = ev.StartDate;
            eventToUpdate.EndDate = ev.EndDate;
            eventToUpdate.TargetGroup = ev.TargetGroup;
            eventToUpdate.ApproximateAttendees = ev.ApproximateAttendees;
            eventToUpdate.DisplayInCommunity = ev.DisplayInCommunity;
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

            int affectedRows;

            try
            {
                affectedRows = Context.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                foreach (DbEntityValidationResult item in ex.EntityValidationErrors)
                {
                    // Get entry

                    DbEntityEntry entry = item.Entry;
                    string entityTypeName = entry.Entity.GetType().Name;

                    // Display or log error messages

                    foreach (DbValidationError subItem in item.ValidationErrors)
                    {
                        string message = string.Format("Error '{0}' occurred in {1} at {2}",
                                 subItem.ErrorMessage, entityTypeName, subItem.PropertyName);
                        Console.WriteLine(message);
                    }
                    // Rollback changes

                    switch (entry.State)
                    {
                        case EntityState.Added:
                            entry.State = EntityState.Detached;
                            break;
                        case EntityState.Modified:
                            entry.CurrentValues.SetValues(entry.OriginalValues);
                            entry.State = EntityState.Unchanged;
                            break;
                        case EntityState.Deleted:
                            entry.State = EntityState.Unchanged;
                            break;
                    }
                }

                return affectedRows = 0;
            }
            Context.Entry(eventToUpdate).Reload();
            return affectedRows;
        }



        // DELETE
        public static bool DeleteEvent(events ev)
        {
            events eventToDelete = GetEventById(ev.Id);

            eventToDelete.IsDeleted = true;
            
            int affectedRows;
            try
            {
                affectedRows = Context.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                foreach (DbEntityValidationResult item in ex.EntityValidationErrors)
                {
                    // Get entry

                    DbEntityEntry entry = item.Entry;
                    string entityTypeName = entry.Entity.GetType().Name;

                    // Display or log error messages

                    foreach (DbValidationError subItem in item.ValidationErrors)
                    {
                        string message = string.Format("Error '{0}' occurred in {1} at {2}",
                                 subItem.ErrorMessage, entityTypeName, subItem.PropertyName);
                        Console.WriteLine(message);
                    }
                    // Rollback changes

                    switch (entry.State)
                    {
                        case EntityState.Added:
                            entry.State = EntityState.Detached;
                            break;
                        case EntityState.Modified:
                            entry.CurrentValues.SetValues(entry.OriginalValues);
                            entry.State = EntityState.Unchanged;
                            break;
                        case EntityState.Deleted:
                            entry.State = EntityState.Unchanged;
                            break;
                    }
                }

                return false;
            }

            return affectedRows > 0;
        }

    }
}