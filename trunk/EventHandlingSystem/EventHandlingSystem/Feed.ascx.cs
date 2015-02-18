using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using EventHandlingSystem.Database;

namespace EventHandlingSystem
{
    public partial class Feed : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            RenderFeeds();
        }

        //DateTime eventdate = Convert.ToDateTime("03/15/2015");

        public void RenderFeeds()
        {
            List<events> eventList = EventDB.GetAllEventsInMonth(DateTime.Now);
            
            //foreach (var ev in eventList)
            //{
            //        //Lägg in...
            //        // datum
                
            //        lnkbtnEventDate.Text = ev.StartDate.ToShortDateString();

            //        // titel och url
            //        hlnkEventTitle.Text = ev.Title;
            //        hlnkEventTitle.NavigateUrl = "EventDetails?id=" + ev.Id;

            //        // summary
            //        lbEventSummary.Text = ev.Summary;

            //        // bildurl
            //        imgEventImage.ImageUrl = ev.ImageUrl;
            //}
            //Controls.Add(panelFeed);

            RepeaterFeed.DataSource = eventList;
            RepeaterFeed.DataBind();

            foreach (var ev in eventList)
            {
                Controls.Add(new HtmlGenericControl("div"));
                Controls.Add(new HtmlGenericControl("br"));
                Label eventDate = new Label();
                eventDate.CssClass = "feedbox-eventdate";
                eventDate.Text = ev.StartDate.ToString("dd MMM");
                Controls.Add(new HtmlGenericControl("br"));

                HyperLink title = new HyperLink();
                title.Text = ev.Title;

                Controls.Add(eventDate);
                Controls.Add(title);
                Controls.Add(new HtmlGenericControl("div"));

            }
            
        }
    }
}