using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventHandlingSystem.Database
{
    public class TermDB
    {
        private static readonly EventHandlingDataModelContainer Context = Database.Context;

        // GET
        private static IEnumerable<Term> GetAllNotDeletedTerms()
        {
            return Context.Terms.Where(term => !term.IsDeleted);
        }

        public static Term GetTermById(int id)
        {
            return GetAllNotDeletedTerms().SingleOrDefault(term => term.Id.Equals(id));
        }

        public static List<Term> GetAllTermsByTermSet(TermSet termSet)
        {
            return TermSetDB.GetTermSetById(termSet.Id).Term.Where(t => !t.IsDeleted).ToList();
        }

        public static List<Term> GetAllTermsByTermSetId(int tsId)
        {
            return TermSetDB.GetTermSetById(tsId).Term.Where(t => !t.IsDeleted).ToList();
        }

        
        // CREATE
        public static int CreateTerm(Term term)
        {
            Term termToCreate = new Term
            {
                Name = !string.IsNullOrWhiteSpace(term.Name) ? term.Name : "Untitled",
                TermSet = term.TermSet,
                Created = DateTime.Now
            };

            Context.Terms.Add(termToCreate);

            int affectedRows = Context.SaveChanges();
            return affectedRows;
        }


        // UPDATE
        public static int UpdateTerm(Term term)
        {
            Term termToUpdate = GetTermById(term.Id);
            termToUpdate.Name = term.Name;
            termToUpdate.TermSet = term.TermSet;

            int affectedRows = Context.SaveChanges();
            return affectedRows;
        }


        // DELETE
        public static int DeleteTermById(int id)
        {
            Term termToDelete = GetTermById(id);

            if (termToDelete != null)
                termToDelete.IsDeleted = true;

            int affectedRows = Context.SaveChanges();
            return affectedRows;
        }
    }
}