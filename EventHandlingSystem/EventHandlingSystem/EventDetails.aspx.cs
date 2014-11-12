using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using DotNetOpenAuth.OpenId.Extensions.AttributeExchange;
using EventHandlingSystem.Database;

namespace EventHandlingSystem
{
    public partial class EventDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string id = Request.QueryString["Id"];

            Event @event = EventDB.GetEventById(int.Parse("1"));

            var title = new HtmlGenericControl("h2") {InnerHtml = @event.Title};
            var imageUrl = new HtmlImage() { Src = @event.ImageUrl };
            imageUrl.Style.Add(HtmlTextWriterStyle.MarginTop, "25px");
            var description = new HtmlGenericControl("p") {InnerHtml = @event.Description};
            var summary = new HtmlGenericControl("p") {InnerHtml = "<b>Summary:</b> " + @event.Summary};
            var other = new HtmlGenericControl("p") { InnerHtml = "<b>Other:</b> " + @event.Other };
            var location = new HtmlGenericControl("p") { InnerHtml = "<b>Location:</b> " + @event.Location };
            var dayEvent = new HtmlGenericControl("p") { InnerHtml = "<b>Whole day event:</b> " + (@event.DayEvent ? "Yes" : "No") };
            var startDate = new HtmlGenericControl("p")
            {
                InnerHtml = "<b>Start date:</b> " + 
                    (@event.DayEvent
                        ? @event.StartDate.ToString("yyyy-MM-dd")
                        : @event.StartDate.ToString("yyyy-MM-dd HH:mm"))
            };
            var endDate = new HtmlGenericControl("p")
            {
                InnerHtml = "<b>End date:</b> " + 
                    (@event.DayEvent
                        ? @event.EndDate.ToString("yyyy-MM-dd")
                        : @event.EndDate.ToString("yyyy-MM-dd HH:mm"))
            };
            var targetGroup = new HtmlGenericControl("p") { InnerHtml = "<b>Target group:</b> " + @event.TargetGroup };
            var approximateAttendees = new HtmlGenericControl("p") { InnerHtml = "<b>Approximate attendees:</b> " + @event.ApproximateAttendees.ToString() };
            var association = new HtmlGenericControl("p") { InnerHtml = "<b>Association:</b> " + @event.AssociationId.ToString() };
            var created = new HtmlGenericControl("p") { InnerHtml = "<b>Created:</b> " + @event.Created.ToString("yyyy-MM-dd HH:mm") };
            var createdBy = new HtmlGenericControl("p") { InnerHtml = "<b>Created by:</b> " + @event.CreatedBy };


            Main.Controls.Add(title);
            Main.Controls.Add(imageUrl);
            Main.Controls.Add(description);
            Main.Controls.Add(summary);
            Main.Controls.Add(other);
            Main.Controls.Add(location);
            Main.Controls.Add(dayEvent);
            Main.Controls.Add(startDate);
            Main.Controls.Add(endDate);
            Main.Controls.Add(targetGroup);
            Main.Controls.Add(approximateAttendees);
            Main.Controls.Add(association);
            Main.Controls.Add(created);
            Main.Controls.Add(createdBy);

        }
    }
}