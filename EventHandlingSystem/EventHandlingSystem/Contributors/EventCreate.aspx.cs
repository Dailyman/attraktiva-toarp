﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Migrations;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using EventHandlingSystem.Database;

namespace EventHandlingSystem
{
    public partial class EventCreate : Page
    {
        #region Page_Load


        protected void Page_Load(object sender, EventArgs e)
        {
            LabelMessage.Text = "";
            //Lägger kalender ikonen i våg med DateTextBoxarna.
            ImageButtonStartDate.Style.Add("vertical-align", "top");
            ImageButtonEndDate.Style.Add("vertical-align", "top");

            if (!IsPostBack)
            {
                //Skapar och lägger till alla associations i dropdownboxen.
                List<ListItem> listItems = new List<ListItem>();
                foreach (var association in AssociationDB.GetAllAssociations())
                {
                    listItems.Add(new ListItem
                    {
                        Text = association.Name,
                        Value = association.Id.ToString()
                    });
                }

                //Sorterar ListItems i alfabetisk ordning i DropDownListan för Association
                foreach (var item in listItems.OrderBy(item => item.Text))
                {
                    DropDownAssociation.Items.Add(item);
                }
                //Slut 'lägger till objekt i associationdropdownlist'. 


                //Lägger in Associations i alfabetisk ordning i ListBox
                //ListBoxAssociations.Items.AddRange(listItems.OrderBy(item => item.Text).ToArray());

                
                string dateStr = Request.QueryString["d"];
                DateTime date;
                if (!string.IsNullOrWhiteSpace(dateStr) && DateTime.TryParse(dateStr, out date))
                {
                    TxtBoxStartDate.Text = date.ToString("yyyy-MM-dd");
                    TxtBoxStartTime.Text = date.ToString("HH:mm");
                    CalendarStartDate.SelectedDate = date.Date;
                    TxtBoxEndDate.Text = date.AddHours(1).ToString("yyyy-MM-dd");
                    TxtBoxEndTime.Text = date.AddHours(1).ToString("HH:mm");
                    CalendarEndDate.SelectedDate = date.AddHours(1).Date;
                }
                else
                {
                    //Sätter in dagens datum och tid i textboxarna.
                    TxtBoxStartDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
                    TxtBoxStartTime.Text = "00:00";
                    CalendarStartDate.SelectedDate = DateTime.Now.Date;
                    TxtBoxEndDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
                    TxtBoxEndTime.Text = "00:00";
                    CalendarEndDate.SelectedDate = DateTime.Now.Date;
                }
                //Gömmer kalendrarna från början. 
                CalendarEndDate.Visible = false;
                CalendarStartDate.Visible = false;

                int eventId;
                if (!String.IsNullOrWhiteSpace(Request.QueryString["copy"]) &&
                    !String.IsNullOrWhiteSpace(Request.QueryString["id"]) &&
                    int.TryParse(Request.QueryString["id"], out eventId))
                {
                    if (String.Equals(Request.QueryString["copy"], "true", StringComparison.OrdinalIgnoreCase))
                    {
                        //Fyller formuläret med evenemangets nuvarande information.
                        events @event = EventDB.GetEventById(eventId);

                        TxtBoxTitle.Text = @event.Title;
                        TxtBoxDescription.Text = @event.Description;
                        TxtBoxSummary.Text = @event.Summary;
                        TxtBoxOther.Text = @event.Other;
                        TxtBoxLocation.Text = @event.Location;
                        TxtBoxImageUrl.Text = @event.ImageUrl;
                        TxtBoxEventUrl.Text = @event.EventUrl;
                        ChkBoxDayEvent.Checked = @event.DayEvent;
                        TxtBoxStartDate.Text = @event.StartDate.ToString("yyyy-MM-dd");
                        TxtBoxStartTime.Text = @event.StartDate.ToString("HH:mm");
                        TxtBoxEndDate.Text = @event.EndDate.ToString("yyyy-MM-dd");
                        TxtBoxEndTime.Text = @event.EndDate.ToString("HH:mm");
                        TxtBoxTargetGroup.Text = @event.TargetGroup;
                        CalendarStartDate.SelectedDate = @event.StartDate;
                        CalendarEndDate.SelectedDate = @event.EndDate;
                        TxtBoxApproximateAttendees.Text = @event.ApproximateAttendees.ToString();

                        //Lägg till vvv HÄR vvv kod för att kunna skapa events med fler associations kopplade till sig....
                        foreach (var asso in @event.associations.OrderBy(a => a.Name))
                        {
                            ListBoxAssociations.Items.Add(new ListItem(asso.Name, asso.Id.ToString()));
                        }

                        //var firstAsso = @event.associations.FirstOrDefault();
                        //if (firstAsso != null)
                        //{
                        //    DropDownAssociation.SelectedIndex =
                        //        DropDownAssociation.Items.IndexOf(
                        //            DropDownAssociation.Items.FindByValue(
                        //                firstAsso.Id.ToString()));
                        //}
                        //else
                        //{
                        //    DropDownAssociation.SelectedIndex = DropDownAssociation.Items.IndexOf(
                        //        DropDownAssociation.Items.FindByValue(""));
                        //}
                    }
                }
            }
        }

        #endregion


        #region ChkBoxDayEvent_OnCheckedChanged

        protected void ChkBoxDayEvent_OnCheckedChanged(object sender, EventArgs e)
        {
            //Gömmer tidsTexboxarna om man checkar heldags checkboxen.
            TxtBoxStartTime.Enabled = !ChkBoxDayEvent.Checked;
            TxtBoxStartTime.Visible = !ChkBoxDayEvent.Checked;
            TxtBoxEndTime.Enabled = !ChkBoxDayEvent.Checked;
            TxtBoxEndTime.Visible = !ChkBoxDayEvent.Checked;
        }

        #endregion


        #region ImageButtonStartDate_OnClick : ImageButtonEndDate_OnClick

        protected void ImageButtonStartDate_OnClick(object sender, ImageClickEventArgs e)
        {
            //Gömmer/Visar StartDatekalendern när användaren klickar på kalenderikonen(knappen).
            CalendarStartDate.Visible = CalendarStartDate.Visible == false;
        }

        protected void ImageButtonEndDate_OnClick(object sender, ImageClickEventArgs e)
        {
            //Gömmer/Visar EndDatekalendern när användaren klickar på kalenderikonen(knappen).
            CalendarEndDate.Visible = CalendarEndDate.Visible == false;
        }

        #endregion


        #region TxtBoxStartDate_OnTextChanged

        protected void TxtBoxStartDate_OnTextChanged(object sender, EventArgs e)
        {
            //Validerar StartDateTextBoxen. Om datumet i textboxen är giltig väljs den in i kalendern.
            CustomValiStartDate.Validate();
            if (CustomValiStartDate.IsValid)
            {
                CalendarStartDate.SelectedDate = Convert.ToDateTime(TxtBoxStartDate.Text);
            }
        }

        #endregion


        #region CustomValiStartDate_OnServerValidate

        protected void CustomValiStartDate_OnServerValidate(object source, ServerValidateEventArgs args)
        {
            //Validerar om texten i StartDateTextBoxen är ett giltig datum. 
            DateTime result;
            args.IsValid = !string.IsNullOrWhiteSpace(TxtBoxStartDate.Text) &&
                           DateTime.TryParse(TxtBoxStartDate.Text, out result);
        }

        #endregion


        #region TxtBoxEndDate_OnTextChanged

        protected void TxtBoxEndDate_OnTextChanged(object sender, EventArgs e)
        {
            //Validerar EndDateTextBoxen. Om datumet i textboxen är giltig väljs den in i kalendern.
            CustomValiEndDate.Validate();

            if (CustomValiEndDate.IsValid)
            {
                CalendarEndDate.SelectedDate = Convert.ToDateTime(TxtBoxEndDate.Text);
            }
        }

        #endregion


        #region CustomValiEndDate_OnServerValidate

        protected void CustomValiEndDate_OnServerValidate(object source, ServerValidateEventArgs args)
        {
            //Validerar om texten i EndDateTextBoxen är ett giltig datum. 
            DateTime result;
            args.IsValid = !string.IsNullOrWhiteSpace(TxtBoxEndDate.Text) &&
                           DateTime.TryParse(TxtBoxEndDate.Text, out result);
        }

        #endregion


        #region CalendarStartDate_OnSelectionChanged : CalendarEndDate_OnSelectionChanged

        protected void CalendarStartDate_OnSelectionChanged(object sender, EventArgs e)
        {
            //Lägger in det valda kalenderdatumet som text i StartDateTextBoxen.
            TxtBoxStartDate.Text = CalendarStartDate.SelectedDate.ToString("yyyy-MM-dd");
        }

        protected void CalendarEndDate_OnSelectionChanged(object sender, EventArgs e)
        {
            //Lägger in det valda kalenderdatumet som text i EndDateTextBoxen.
            TxtBoxEndDate.Text = CalendarEndDate.SelectedDate.ToString("yyyy-MM-dd");
        }

        #endregion


        #region BtnCreateEvent_OnClick

        protected void BtnCreateEvent_OnClick(object sender, EventArgs e)
        {
            //Gör om texterna i textboxarna Start- och EndDate till typen DateTime, som används vid skapandet av evenemanget.
            var start = Convert.ToDateTime(TxtBoxStartDate.Text)
                .Add(TimeSpan.FromHours(Convert.ToDateTime(TxtBoxStartTime.Text).Hour))
                .Add(TimeSpan.FromMinutes(Convert.ToDateTime(TxtBoxStartTime.Text).Minute));
            var end = Convert.ToDateTime(TxtBoxEndDate.Text)
                .Add(TimeSpan.FromHours(Convert.ToDateTime(TxtBoxEndTime.Text).Hour))
                .Add(TimeSpan.FromMinutes(Convert.ToDateTime(TxtBoxEndTime.Text).Minute));

            List<associations> associationsList = new List<associations>();
            foreach (ListItem item in ListBoxAssociations.Items)
            {
                int aId;
                if (!String.IsNullOrWhiteSpace(item.Value) && int.TryParse(item.Value, out aId))
                {
                    if (AssociationDB.GetAssociationById(aId) != null)
                    {
                        associationsList.Add(AssociationDB.GetAssociationById(aId));
                    }
                }
            }

            //Nytt Event Objekt skapas och alla värdena från formuläret läggs in i objektet
            var ev = new events
            {
                Title = TxtBoxTitle.Text,
                Description = TxtBoxDescription.Text,
                Summary = TxtBoxSummary.Text,
                Other = TxtBoxOther.Text,
                Location = TxtBoxLocation.Text,
                ImageUrl = TxtBoxImageUrl.Text,
                EventUrl = TxtBoxEventUrl.Text,
                DayEvent = ChkBoxDayEvent.Checked,
                StartDate = (ChkBoxDayEvent.Checked) ? Convert.ToDateTime(TxtBoxStartDate.Text) : start,
                EndDate =
                    (ChkBoxDayEvent.Checked) ? Convert.ToDateTime(TxtBoxEndDate.Text).Add(new TimeSpan(23, 59, 0)) : end,
                TargetGroup = TxtBoxTargetGroup.Text,
                ApproximateAttendees =
                    !string.IsNullOrEmpty(TxtBoxApproximateAttendees.Text)
                        ? int.Parse(TxtBoxApproximateAttendees.Text)
                        : 0,
                associations = associationsList,
                CreatedBy = HttpContext.Current.User.Identity.Name,
                UpdatedBy = HttpContext.Current.User.Identity.Name

            };

            //Ger LabelMessage en större font-storlek som visar om eventet kunde skapas eller ej (!!om evenemanget kunde skapas skickas användaren just nu till denna visningssida!!). 
            LabelMessage.Style.Add(HtmlTextWriterStyle.FontSize, "25px");
            if (EventDB.AddEvent(ev))
            {
                Response.Redirect(
                        HttpContext.Current.Request.Url.AbsoluteUri.Replace(
                            HttpContext.Current.Request.Url.PathAndQuery, "/") + "EventDetails.aspx?Id=" + ev.Id, false);
               
                //LabelMessage.Text = "Event was created";
            }
            else
            {
                LabelMessage.ForeColor = Color.Red;
                LabelMessage.Text = "Event couldn't be created";
            }

        }

        #endregion

        protected void ListBoxAssociations_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        protected void ButtonRemoveAssociation_OnClick(object sender, EventArgs e)
        {
            List<ListItem> itemsToRemove = ListBoxAssociations.GetSelectedIndices().Select(index => (ListBoxAssociations.Items[index])).ToList();

            foreach (ListItem itemToRemove in itemsToRemove)
            {
                ListBoxAssociations.Items.Remove(itemToRemove);
            }
        }

        protected void ButtonAddAssociation_OnClick(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(DropDownAssociation.SelectedValue))
            {
                if (!ListBoxAssociations.Items.Contains(DropDownAssociation.SelectedItem))
                {
                    ListBoxAssociations.Items.Add(DropDownAssociation.SelectedItem);
                }
                else
                {
                    LabelMessage.ForeColor = Color.Red;
                    LabelMessage.Text = "You cannot add the same association twice!";
                }
            }
            else
            {
                LabelMessage.ForeColor = Color.Red;
                LabelMessage.Text = "You cannot add an empty association! Try again.";
            }
        }
    }
}