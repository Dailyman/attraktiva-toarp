using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventHandlingSystem.Database
{
    public class TermDB
    {
        private static readonly EventHandlingDataModelContainer Context = Database.Context;

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
            return TermSetDB.GetTermSetById(termSet.Id).Term.ToList();
        }

        public static int UpdateTerm(Term term)
        {
            Term termToUpdate = GetTermById(term.Id);
            termToUpdate.Name = term.Name;
          
            int affectedRows = Context.SaveChanges();
            return affectedRows;
        }
    }
}