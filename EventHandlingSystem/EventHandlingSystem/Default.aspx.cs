using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using  EventHandlingSystem.Database;

namespace EventHandlingSystem
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //foreach (var @event in EventDB.GetEventsBySpecifiedNumberOfPreviousMonthsFromToday())
            //{
            //    TestLable.Text += @event.Title + "<br />";
            //}
            
        }
    }
}