using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventHandlingSystem.Database
{
    //public class TaxonomyDB
    //{
    //    private static readonly EventHandlingDataModelContainer Context = Database.Context;

    //    private static IEnumerable<Taxonomy> GetAllNotDeletedTaxonomies()
    //    {
    //        return Context.Taxonomies.Where(t => !t.IsDeleted);
    //    }

    //    public static Taxonomy GetTaxonomyById(int id)
    //    {
    //        return GetAllNotDeletedTaxonomies().SingleOrDefault(t => t.Id.Equals(id));
    //    }

    //    public static Taxonomy GetPublishingTaxonomy()
    //    {
    //        //Publishing Taxonomy är först i Taxonomi-tabellen och har id=1.
    //        return GetTaxonomyById(1);
    //    }
    //    public static Taxonomy GetCategoryTaxonomy()
    //    {
    //        return GetTaxonomyById(2);
    //    }
    //    public static Taxonomy GetCustomizedTaxonomy()
    //    {
    //        return GetTaxonomyById(3);
    //    }

    //    public static int UpdateTaxonomy(Taxonomy tax)
    //    {
    //        Taxonomy taxonomyToUpdate = GetTaxonomyById(tax.Id);

    //        taxonomyToUpdate.Name = tax.Name;

    //        int affectedRows = Context.SaveChanges();
    //        return affectedRows;
    //    }

    //    public static int DeleteTaxonomyById(int id)
    //    {
    //        Taxonomy taxonomyToDelete = GetTaxonomyById(id);

    //        if (taxonomyToDelete != null)
    //        {
    //            taxonomyToDelete.IsDeleted = true;
    //        }
    //        int affectedRows = Context.SaveChanges();
    //        return affectedRows;
    //    }
    //}
}