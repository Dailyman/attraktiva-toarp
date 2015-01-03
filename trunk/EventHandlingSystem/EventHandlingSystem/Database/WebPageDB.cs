using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventHandlingSystem.Database
{
    //public class WebPageDB
    //{
    //    private static readonly EventHandlingDataModelContainer Context = Database.Context;

    //    private static IEnumerable<WebPage> GetAllNotDeletedWebPages()
    //    {
    //        return Context.WebPages.Where(wP => !wP.IsDeleted);
    //    }

    //    //Ta bort denna? -Kan väl spara den, vem vet om vi behöver använda den i framtiden? =P
    //    public static List<WebPage> GetAllWebPages()
    //    {
    //        return GetAllNotDeletedWebPages().ToList();
    //    }

    //    public static WebPage GetWebPageById(int id)
    //    {
    //        return GetAllNotDeletedWebPages().SingleOrDefault(wP => wP.Id.Equals(id));
    //    }
    //}
}