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
        #region Page_Load
        protected void Page_Load(object sender, EventArgs e)
        {
            //Hämtar EventId från URL.
            string stId = Request.QueryString["Id"];

            //Lägger till alla evenemang som ListItems, Titel(Text) och Id(Value), i DropDownListan.
            foreach (var ev in EventDB.GetEventsBySpecifiedNumberOfPreviousMonthsFromToday())
            {
                DropDownListEvents.Items.Add(new ListItem(ev.Title, ev.Id.ToString()));
            }

            //Om Id värdet som tas från URLn är i giltigt format kommer evenemangets information att laddas och skrivas på sidan.
            int id;
            if (!string.IsNullOrWhiteSpace(stId) && int.TryParse(stId, out id ))
            {
                //Hämtar evenemanget som skall visas.
                var @event = EventDB.GetEventById(id);

                //Om evenemanget fanns i DBn läggs all dess information in på sidan.
                if (@event != null)
                {
                    //Skapar alla olika controls med informationen.
                    var title = new HtmlGenericControl("h2") { InnerHtml = @event.Title };
                    var imageUrl = new HtmlImage() { Src = @event.ImageUrl };
                    imageUrl.Style.Add(HtmlTextWriterStyle.MarginTop, "25px");
                    imageUrl.Style.Add("max-width", "100%");
                    var description = new HtmlGenericControl("p") { InnerHtml = @event.Description };
                    var summary = new HtmlGenericControl("p") { InnerHtml = "<b>Summary:</b> " + @event.Summary };
                    var other = new HtmlGenericControl("p") { InnerHtml = "<b>Other:</b> " + @event.Other };
                    var location = new HtmlGenericControl("p") { InnerHtml = "<b>Location:</b> " + @event.Location };
                    var dayEvent = new HtmlGenericControl("p")
                    {
                        InnerHtml = "<b>Whole day event:</b> " + (@event.DayEvent ? "Yes" : "No")
                    };
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
                    var targetGroup = new HtmlGenericControl("p")
                    {
                        InnerHtml = "<b>Target group:</b> " + @event.TargetGroup
                    };
                    var approximateAttendees = new HtmlGenericControl("p")
                    {
                        InnerHtml = "<b>Approximate attendees:</b> " + @event.ApproximateAttendees.ToString()
                    };
                    //Lägg till vvv HÄR vvv kod för att kunna visa events med fler associations kopplade till sig....
                    var association = new HtmlGenericControl("p")
                    {
                        InnerHtml = "<b>Association:</b> 'None'"
                    };
                    //Lägg till vvv HÄR vvv kod för att kunna skapa events med fler associations kopplade till sig....
                    var firstOrDefault = @event.associations.FirstOrDefault();
                    if (firstOrDefault != null)
                    {
                        association = new HtmlGenericControl("p")
                        {
                            InnerHtml = "<b>Association:</b> " + firstOrDefault.Name
                        };
                    }
                    var created = new HtmlGenericControl("p")
                        {
                            InnerHtml = "<b>Created:</b> " + @event.Created.ToString("yyyy-MM-dd HH:mm")
                        };
                        var createdBy = new HtmlGenericControl("p") { InnerHtml = "<b>Created by:</b> " + @event.CreatedBy };
                        var latestUpdate = new HtmlGenericControl("p") { InnerHtml = "<b>Latest update:</b> " + @event.LatestUpdate.ToString("yyyy-MM-dd HH:mm") };
                        var updatedBy = new HtmlGenericControl("p") { InnerHtml = "<b>Updated by:</b> " + @event.UpdatedBy };
                        var updateLink = new HtmlAnchor() { HRef = "~/EventUpdate?Id=" + @event.Id, InnerText = "Update the event here!" };


                        //Lägger in alla skapade controls i Main(en Div-tag på sidan).
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
                        //Main.Controls.Add(linkText);
                        //Main.Controls.Add(link);
                        Main.Controls.Add(new LiteralControl("<br />"));
                        Main.Controls.Add(created);
                        Main.Controls.Add(createdBy);
                        Main.Controls.Add(latestUpdate);
                        Main.Controls.Add(updatedBy);
                        Main.Controls.Add(new LiteralControl("<br />"));
                        Main.Controls.Add(updateLink);
                    
                }
                else
                {
                    //Om evenemanget ej kunde hittas i DBn skrivs ett felmeddelande ut på sidan.
                    var error = new HtmlGenericControl("h4") { InnerHtml = "The event does not exist! It might have been deleted." };
                    Main.Controls.Add(error);
                }

            }
            else
            {
                //Om Id-värdet som tas från URLn INTE är i giltigt format skrivs ett felmeddelande ut på sidan.
                var error = new HtmlGenericControl("h4") { InnerHtml = "Use a correct event ID to show the event!"};
                Main.Controls.Add(error);
            }
        }
        #endregion


        #region BtnSearch_OnClick
        protected void BtnSearch_OnClick(object sender, EventArgs e)
        {
            //Skickar användaren till EventDetails.aspx med det EventId som man valt i DropDownListan.
            Response.Redirect(Request.Url.AbsolutePath + "?id=" + DropDownListEvents.SelectedValue, true);
        }
        #endregion

       }
}