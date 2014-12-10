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

        public static List<TermSet> GetTermSetsByTaxonomy(Taxonomy tax)
        {
            return GetAllNotDeletedTermSets().Where(ts => ts.TaxonomyId.Equals(tax.Id)).ToList();
        }

        public static List<TermSet> GetAllParentTermSetsByTaxonomy(Taxonomy tax)
        {
            //Om en termset inte har ett ParentTermSetId är det själv en parent
            return GetTermSetsByTaxonomy(tax).Where(ts => ts.ParentTermSetId.Equals(null)).ToList();
        }

        public static List<TermSet> GetChildTermSetsByParentTermSetId(int id)
        {
            return GetAllNotDeletedTermSets().Where(ts => ts.ParentTermSetId.Equals(id)).ToList();
        }

        public static int CreateTermSet(TermSet termSet)
        {
            TermSet termSetToCreate = new TermSet
            {
                Name = !string.IsNullOrWhiteSpace(termSet.Name) ? termSet.Name : "Untitled",
                ParentTermSetId = termSet.ParentTermSetId,
                TaxonomyId = termSet.TaxonomyId,
                Created = DateTime.Now
            };

            Context.TermSets.Add(termSetToCreate);

            int affectedRows = Context.SaveChanges();
            return affectedRows;
        }

        public static int UpdateTermSet(TermSet termSet)
        {
            TermSet termSetToUpdate = GetTermSetById(termSet.Id);
            termSetToUpdate.Name = termSet.Name;
            termSetToUpdate.ParentTermSetId = termSet.ParentTermSetId;
            termSetToUpdate.TaxonomyId = termSet.TaxonomyId;
            
            int affectedRows = Context.SaveChanges();
            return affectedRows;
        }

        public static int DeleteTermSetById(int id)
        {
            TermSet termSetToDelete = GetTermSetById(id);

            if (termSetToDelete != null)
                termSetToDelete.IsDeleted = true;

            int affectedRows = Context.SaveChanges();
            return affectedRows;
        }

        public static string GetTermSetNameByTermSetId(int id)
        {
            TermSet ts = GetTermSetById(id);
            return ts.Name;
        }
    }
}