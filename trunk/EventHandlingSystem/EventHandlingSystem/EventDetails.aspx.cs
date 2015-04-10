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

            //if (!IsPostBack)
            //{
            //    //Lägger till alla evenemang som ListItems, Titel(Text) och Id(Value), i DropDownListan.
            //    foreach (var ev in EventDB.GetEventsBySpecifiedNumberOfPreviousMonthsFromToday())
            //    {
            //        DropDownListEvents.Items.Add(new ListItem(ev.Title, ev.Id.ToString()));
            //    }
            //}

            //Om Id värdet som tas från URLn är i giltigt format kommer evenemangets information att laddas och skrivas på sidan.
            int id;
            if (!string.IsNullOrWhiteSpace(stId) && int.TryParse(stId, out id))
            {
                //Hämtar evenemanget som skall visas.
                var ev = EventDB.GetEventById(id);

                //Om evenemanget fanns i DBn läggs all dess information in på sidan.
                if (ev != null)
                {
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
                            else
                            {
                                ListBoxAssociations.Items.Add(new ListItem(a.Name, ""));
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
                var error = new HtmlGenericControl("h4") { InnerHtml = "The event ID was not correct!" };
                Main.Controls.Add(error);
                PanelMain.Visible = false;
            }
        }

        #endregion


        #region BtnSearch_OnClick

        //protected void BtnSearch_OnClick(object sender, EventArgs e)
        //{
        //    //Skickar användaren till EventDetails.aspx med det EventId som man valt i DropDownListan.
        //    Response.Redirect(Request.Url.AbsolutePath + "?id=" + DropDownListEvents.SelectedValue, true);


        //}

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
                        AssoLink.Enabled = true;
                    }
                    else
                    {
                        AssoLink.Enabled = false;
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