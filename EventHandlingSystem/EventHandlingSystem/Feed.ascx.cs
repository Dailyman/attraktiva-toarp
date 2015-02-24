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
            List<events> eventList = EventDB.GetEventsBySpecifiedNumberOfMonthsFromToday()
                .OrderBy(item => item.StartDate)
                .Take(2) //visar antalet angivet (om items är färre än antalet visar det antalet items som finns)
                .ToList();

            RepeaterFeed.DataSource = eventList;
            RepeaterFeed.DataBind();

            #region

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

            //foreach (var ev in eventList)
            //{
            //    Controls.Add(new HtmlGenericControl("div"));
            //    Controls.Add(new LiteralControl("<br />"));
            //    Label eventDate = new Label();
            //    eventDate.CssClass = "feedbox-eventdate";
            //    eventDate.Text = ev.StartDate.ToString("dd MMM");
            //    Controls.Add(new LiteralControl("<br />"));

            //    HyperLink title = new HyperLink();
            //    title.Text = ev.Title;

            //    Controls.Add(eventDate);
            //    Controls.Add(title);
            //    Controls.Add(new HtmlGenericControl("div"));

            //}

            #endregion
        }

        protected void btnShowMore_OnClick(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(hdfFeedLimit.Value))
            {
                hdfFeedLimit.Value = "2";
            }
                int numberOfShownEvents = int.Parse(hdfFeedLimit.Value); //

                List<events> eventList = EventDB.GetEventsBySpecifiedNumberOfMonthsFromToday()
                    .OrderBy(item => item.StartDate)
                    .Take(numberOfShownEvents + 2)
                    .ToList();

            hdfFeedLimit.Value = (numberOfShownEvents + 2).ToString();

                RepeaterFeed.DataSource = eventList;
            RepeaterFeed.DataBind();



        }
    }
}