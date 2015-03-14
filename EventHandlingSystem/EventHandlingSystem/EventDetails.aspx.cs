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

            if (!IsPostBack)
            {
                //Lägger till alla evenemang som ListItems, Titel(Text) och Id(Value), i DropDownListan.
                foreach (var ev in EventDB.GetEventsBySpecifiedNumberOfPreviousMonthsFromToday())
                {
                    DropDownListEvents.Items.Add(new ListItem(ev.Title, ev.Id.ToString()));
                }
            }

            //Om Id värdet som tas från URLn är i giltigt format kommer evenemangets information att laddas och skrivas på sidan.
            int id;
            if (!string.IsNullOrWhiteSpace(stId) && int.TryParse(stId, out id))
            {
                //Hämtar evenemanget som skall visas.
                var ev = EventDB.GetEventById(id);

                //Om evenemanget fanns i DBn läggs all dess information in på sidan.
                if (ev != null)
                {
                    //Skapar alla olika controls med informationen.
                    //var title = new HtmlGenericControl("h2") { InnerHtml = ev.Title };
                    //var imageUrl = new HtmlImage() { Src = ev.ImageUrl };
                    //imageUrl.Style.Add(HtmlTextWriterStyle.MarginTop, "25px");
                    //imageUrl.Style.Add("max-width", "100%");
                    //var description = new HtmlGenericControl("p") { InnerHtml = ev.Description };
                    //var summary = new HtmlGenericControl("p") { InnerHtml = "<b>Summary:</b> " + ev.Summary };
                    //var other = new HtmlGenericControl("p") { InnerHtml = "<b>Other:</b> " + ev.Other };
                    //var location = new HtmlGenericControl("p") { InnerHtml = "<b>Location:</b> " + ev.Location };
                    //var eventUrl = new HtmlGenericControl("p")
                    //{
                    //    InnerHtml = "<b>Event URL:</b> <a href=" + ev.EventUrl + ">" + ev.EventUrl + "</a>"
                    //};
                    //var dayEvent = new HtmlGenericControl("p")
                    //{
                    //    InnerHtml = "<b>Whole day event:</b> " + (ev.DayEvent ? "Yes" : "No")
                    //};
                    //var startDate = new HtmlGenericControl("p")
                    //{
                    //    InnerHtml = "<b>Start date:</b> " +
                    //                (ev.DayEvent
                    //                    ? ev.StartDate.ToString("yyyy-MM-dd")
                    //                    : ev.StartDate.ToString("yyyy-MM-dd HH:mm"))
                    //};
                    //var endDate = new HtmlGenericControl("p")
                    //{
                    //    InnerHtml = "<b>End date:</b> " +
                    //                (ev.DayEvent
                    //                    ? ev.EndDate.ToString("yyyy-MM-dd")
                    //                    : ev.EndDate.ToString("yyyy-MM-dd HH:mm"))
                    //};
                    //var targetGroup = new HtmlGenericControl("p")
                    //{
                    //    InnerHtml = "<b>Target group:</b> " + ev.TargetGroup
                    //};
                    //var approximateAttendees = new HtmlGenericControl("p")
                    //{
                    //    InnerHtml = "<b>Approximate attendees:</b> " + ev.ApproximateAttendees.ToString()
                    //};
                    ////Lägg till vvv HÄR vvv kod för att kunna visa events med fler associations kopplade till sig....
                    //var association = new HtmlGenericControl("p")
                    //{
                    //    InnerHtml = "<b>Association:</b> 'None'"
                    //};
                    ////Lägg till vvv HÄR vvv kod för att kunna skapa events med fler associations kopplade till sig....
                    //var firstOrDefault = ev.associations.FirstOrDefault();
                    //if (firstOrDefault != null)
                    //{
                    //    association = new HtmlGenericControl("p")
                    //    {
                    //        InnerHtml = "<b>Association:</b> <a href=\"sitepage?id="+WebPageDB.GetWebPageByAssociationId(firstOrDefault.Id).Id+"&type=a\">"+firstOrDefault.Name+"</a>"
                    //    };
                    //}
                    //var created = new HtmlGenericControl("p")
                    //    {
                    //        InnerHtml = "<b>Created:</b> " + ev.Created.ToString("yyyy-MM-dd HH:mm")
                    //    };
                    //    var createdBy = new HtmlGenericControl("p") { InnerHtml = "<b>Created by:</b> " + ev.CreatedBy };
                    //    var latestUpdate = new HtmlGenericControl("p") { InnerHtml = "<b>Latest update:</b> " + ev.LatestUpdate.ToString("yyyy-MM-dd HH:mm") };
                    //    var updatedBy = new HtmlGenericControl("p") { InnerHtml = "<b>Updated by:</b> " + ev.UpdatedBy };
                    //    var updateLink = new HtmlAnchor() { HRef = "~/EventUpdate?Id=" + ev.Id, InnerText = "Update the event here!" };
                    //    var copyLink = new HtmlAnchor() { HRef = "~/EventCreate?Copy=true&Id=" + ev.Id, InnerText = "Copy the event here!" };


                    //    //Lägger in alla skapade controls i Main(en Div-tag på sidan).
                    //    Main.Controls.Add(title);
                    //    Main.Controls.Add(imageUrl);
                    //    Main.Controls.Add(description);
                    //    Main.Controls.Add(summary);
                    //    Main.Controls.Add(other);
                    //    Main.Controls.Add(location);
                    //    Main.Controls.Add(eventUrl);
                    //    Main.Controls.Add(dayEvent);
                    //    Main.Controls.Add(startDate);
                    //    Main.Controls.Add(endDate);
                    //    Main.Controls.Add(targetGroup);
                    //    Main.Controls.Add(approximateAttendees);
                    //    Main.Controls.Add(association);
                    //    //Main.Controls.Add(linkText);
                    //    //Main.Controls.Add(link);
                    //    Main.Controls.Add(new LiteralControl("<br />"));
                    //    Main.Controls.Add(created);
                    //    Main.Controls.Add(createdBy);
                    //    Main.Controls.Add(latestUpdate);
                    //    Main.Controls.Add(updatedBy);
                    //    Main.Controls.Add(new LiteralControl("<br />"));
                    //    Main.Controls.Add(updateLink);
                    //    Main.Controls.Add(new LiteralControl("<br />"));
                    //    Main.Controls.Add(copyLink);
                    if (!IsPostBack)
                    {
                        LinkUpdate.NavigateUrl = "~/Contributors/EventUpdate?Id=" + ev.Id;
                        LinkUpdate.ToolTip = "Update the event here!";
                        LinkCopy.NavigateUrl = "~/Contributors/EventCreate?Copy=true&Id=" + ev.Id;
                        LinkCopy.ToolTip = "Copy the event here!";

                        EventTitle.Text = ev.Title;
                        EventImage.ImageUrl = ev.ImageUrl;
                        EventDescription.Text = ev.Description;
                        EventSummary.Text = ev.Summary;
                        EventOther.Text = ev.Other;
                        EventLocation.Text = ev.Location;
                        EventLink.NavigateUrl = ev.EventUrl;
                        DayEvent.Checked = ev.DayEvent;
                        EventStartDate.Text = ev.DayEvent
                            ? ev.StartDate.ToString("d MMMM yyyy")
                            : ev.StartDate.ToString("yyyy MMMM d ") + "<img src=\"http://icons.iconarchive.com/icons/glyphish/glyphish/16/11-clock-icon.png\" style=\"vertical-align: middle; margin: 0 3px 0 5px;\">" + ev.StartDate.ToString("HH:mm");
                        EventEndDate.Text = ev.DayEvent
                            ? ev.EndDate.ToString("d MMMM yyyy")
                            : ev.EndDate.ToString("yyyy MMMM d ") + "<img src=\"http://icons.iconarchive.com/icons/glyphish/glyphish/16/11-clock-icon.png\" style=\"vertical-align: middle; margin: 0 3px 0 5px;\">" + ev.EndDate.ToString("HH:mm");
                        EventTargetGroup.Text = ev.TargetGroup;
                        EventApproxAttend.Text = ev.ApproximateAttendees.ToString();


                        EventCreated.Text = ev.Created.ToString("yyyy-MM-dd HH:mm:ss");
                        EventCreatedBy.Text = ev.CreatedBy;
                        EventLatestUpdate.Text = ev.LatestUpdate.ToString("yyyy-MM-dd HH:mm:ss");
                        EventUpdatedBy.Text = ev.UpdatedBy;

                        ListBoxAssociations.Items.Clear();
                        foreach (var a in ev.associations)
                        {
                            if (WebPageDB.GetWebPageByAssociationId(a.Id) != null)
                            {
                            ListBoxAssociations.Items.Add(new ListItem(a.Name,
                                "/sitepage?id=" + WebPageDB.GetWebPageByAssociationId(a.Id).Id + "&type=a"));
                            }
                        }
                        PanelMain.Visible = true;
                    }
                }
                else
                {
                    //Om evenemanget ej kunde hittas i DBn skrivs ett felmeddelande ut på sidan.
                    var error = new HtmlGenericControl("h4")
                    {
                        InnerHtml = "The event does not exist! It might have been deleted."
                    };
                    Main.Controls.Add(error);
                    PanelMain.Visible = false;
                }

            }
            else
            {
                //Om Id-värdet som tas från URLn INTE är i giltigt format skrivs ett felmeddelande ut på sidan.
                var error = new HtmlGenericControl("h4") {InnerHtml = "Use a correct event ID to show the event!"};
                Main.Controls.Add(error);
                PanelMain.Visible = false;
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

        protected void ListBoxAssociations_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (ListBoxAssociations.SelectedIndex >= 0)
            {
                PanelAsso.Visible = true;

                if (ListBoxAssociations.SelectedItem != null)
                {
                    AssoName.Text = ListBoxAssociations.SelectedItem.Text;
                    if (!String.IsNullOrWhiteSpace(ListBoxAssociations.SelectedValue))
                    {
                        AssoLink.NavigateUrl = ListBoxAssociations.SelectedValue;
                        AssoLink.ImageUrl = "http://www.ric.edu/images/icons/icon_new-tab.png";
                    }
                }
                else
                {
                    PanelAsso.Visible = false;
                }
            }
            else
            {
                PanelAsso.Visible = false;
            }

        }
    }
}