using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EventHandlingSystem.Database;

namespace EventHandlingSystem
{
    public partial class EventList : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            RenderEventList(new DateTime(2000,01,01), new DateTime(2050,01,01));
        }

        //DateTime eventdate = Convert.ToDateTime("03/15/2015");

        public void RenderEventList(DateTime sDate, DateTime eDate)
        {
            List<events> eventList = EventDB.GetEventsByRangeDate(sDate, eDate);
            
            RepeaterEvents.DataSource = eventList;
            RepeaterEvents.DataBind();

        }

        protected void BtnFilter_OnClick(object sender, EventArgs e)
        {
            RenderEventList(Convert.ToDateTime(TxtStart.Text), Convert.ToDateTime(TxtEnd.Text));
        }
    }
}