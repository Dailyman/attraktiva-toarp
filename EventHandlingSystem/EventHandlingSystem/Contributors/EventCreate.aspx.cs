using System;
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
using DotNetOpenAuth.Messaging;
using EventHandlingSystem.Database;

namespace EventHandlingSystem
{
    public partial class EventCreate : Page
    {
        #region Page_Load
        protected void Page_Load(object sender, EventArgs e)
        {
            LabelErrorAsso.Text = "";
            LabelErrorSubCat.Text = "";
            LabelMessage.Text = "";
            //Lägger kalender ikonen i våg med DateTextBoxarna.
            ImageButtonStartDate.Style.Add("vertical-align", "top");
            ImageButtonEndDate.Style.Add("vertical-align", "top");

            if (!IsPostBack)
            {
                var currentUser = UserDB.GetUsersByUsername(HttpContext.Current.User.Identity.Name);
                if (currentUser != null)
                {
                    foreach (var association in currentUser.associations.OrderBy(a => a.Name))
                    {
                        DropDownAssociation.Items.Add(new ListItem
                        {
                            Text = association.Name,
                            Value = association.Id.ToString()
                        });
                    }
                    AddNoneToListBoxAssoIfListBoxIsEmpty();
                    
                    foreach (
                        var subCat in
                            SubCategoryDB.GetAllSubCategoriesByAssociations(currentUser.associations.ToArray()).OrderBy(s => s.Name))
                    {
                        DropDownSubCategories.Items.Add(new ListItem(subCat.Name, subCat.Id.ToString()));
                    }
                    AddNoneToListBoxSubCateIfListBoxIsEmpty();
                }


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

                        
                        foreach (var asso in @event.associations.OrderBy(a => a.Name))
                        {
                            ListBoxAssociations.Items.Add(new ListItem(asso.Name, asso.Id.ToString()));
                        }
                        AddNoneToListBoxAssoIfListBoxIsEmpty();

                        foreach (var subC in @event.subcategories.OrderBy(sC => sC.Name))
                        {
                            ListBoxSubCategories.Items.Add(new ListItem(subC.Name, subC.Id.ToString()));
                        }
                        AddNoneToListBoxSubCateIfListBoxIsEmpty();
                    }
                }
            }
        }

        #endregion

        private void AddNoneToListBoxAssoIfListBoxIsEmpty()
        {
            // Lägger in ett "None" ListItem om inga associations är valda.
            // Tar bort det igen om man valt associations
            if (ListBoxAssociations.Items.Count == 0)
            {
                ListBoxAssociations.Items.Add(new ListItem("None", ""));
            }
            else
            {
                if (ListBoxAssociations.Items.FindByValue(String.Empty) != null && ListBoxAssociations.Items.Count >= 2)
                {
                    ListBoxAssociations.Items.RemoveAt(ListBoxAssociations.Items.IndexOf(
                    ListBoxAssociations.Items.FindByValue(String.Empty)));
                }
            }
        }
        private void AddNoneToListBoxSubCateIfListBoxIsEmpty()
        {
            // Lägger in ett "None" ListItem om inga subcategories är valda.
            // Tar bort det igen om man valt subcategories
            if (ListBoxSubCategories.Items.Count == 0)
            {
                ListBoxSubCategories.Items.Add(new ListItem("None", ""));
            }
            else
            {
                if (ListBoxSubCategories.Items.FindByValue(String.Empty) != null && ListBoxSubCategories.Items.Count >= 2)
                {
                    ListBoxSubCategories.Items.RemoveAt(ListBoxSubCategories.Items.IndexOf(
                    ListBoxSubCategories.Items.FindByValue(String.Empty)));
                }
            }
        }

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


            List<subcategories> subCategoriesList = new List<subcategories>();
            foreach (ListItem item in ListBoxSubCategories.Items)
            {
                int subCatId;
                if (!String.IsNullOrWhiteSpace(item.Value) && int.TryParse(item.Value, out subCatId))
                {
                    if (SubCategoryDB.GetSubCategoryById(subCatId) != null)
                    {
                        subCategoriesList.Add(SubCategoryDB.GetSubCategoryById(subCatId));
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
                subcategories = subCategoriesList,
                //subcategories = (from ListItem item in ListBoxSubCategories.Items select SubCategoryDB.GetSubCategoryById(int.Parse(item.Value))).ToList(),
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


        #region Buttons Add/Remove Association
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
                    LabelErrorAsso.ForeColor = Color.Red;
                    LabelErrorAsso.Text = "You cannot add the same association twice!";
                }
            }
            else
            {
                LabelErrorAsso.ForeColor = Color.Red;
                LabelErrorAsso.Text = "You cannot add an empty association! Try again.";
            }
            AddNoneToListBoxAssoIfListBoxIsEmpty();
        }
        protected void ButtonRemoveAssociation_OnClick(object sender, EventArgs e)
        {
            List<ListItem> itemsToRemove = ListBoxAssociations.GetSelectedIndices().Select(index => (ListBoxAssociations.Items[index])).ToList();

            foreach (ListItem itemToRemove in itemsToRemove)
            {
                ListBoxAssociations.Items.Remove(itemToRemove);
            }
            AddNoneToListBoxAssoIfListBoxIsEmpty();
        }
        #endregion


        #region Buttons Add/Remove SubCategory
        protected void ButtonAddSubCat_OnClick(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(DropDownSubCategories.SelectedValue))
            {
                if (!ListBoxSubCategories.Items.Contains(DropDownSubCategories.SelectedItem))
                {
                    ListBoxSubCategories.Items.Add(DropDownSubCategories.SelectedItem);
                }
                else
                {
                    LabelErrorSubCat.ForeColor = Color.Red;
                    LabelErrorSubCat.Text = "You cannot add the same category twice!";
                }
            }
            else
            {
                LabelErrorSubCat.ForeColor = Color.Red;
                LabelErrorSubCat.Text = "You cannot add an empty category! Try again.";
            }
            AddNoneToListBoxSubCateIfListBoxIsEmpty();
        }
        protected void ButtonRemoveSubCat_OnClick(object sender, EventArgs e)
        {
            List<ListItem> itemsToRemove = ListBoxSubCategories.GetSelectedIndices().Select(index => (ListBoxSubCategories.Items[index])).ToList();

            foreach (ListItem itemToRemove in itemsToRemove)
            {
                ListBoxSubCategories.Items.Remove(itemToRemove);
            }
            AddNoneToListBoxSubCateIfListBoxIsEmpty();
        }
        #endregion
        
    }
}