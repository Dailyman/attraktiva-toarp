using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EventHandlingSystem.Database;

namespace EventHandlingSystem
{
    public partial class EventUpdate : System.Web.UI.Page
    {
        #region Page_Load

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (TryStoringEventIdFromQueryString())
                {
                    if (CheckUsersPermissionForEvent())
                    {
                        AddEventInControls();
                    }
                    else
                    {
                        // If the User does not have permission to edit the event show error message and hide controls
                        ErrorMessage.Text =
                            "You have no permission to edit this event! <br/> You must have contributor status and permission for any of the associations connected to this event to be able to make any changes. <br/>";
                        PanelMain.Visible = false;
                    }
                }
                else
                {
                    // If the EventId could not be stored, show error message and hide controls
                    ErrorMessage.Text =
                        "The event Id was not found! <br/>";
                    PanelMain.Visible = false;
                }
            }
        }

        #endregion

        #region Check if User have permission to edit the Event

        private bool CheckUsersPermissionForEvent()
        {
            events eventToEdit = GetEventToUpdate();

            var currentUser = UserDB.GetUserByUsername(HttpContext.Current.User.Identity.Name);
            if (currentUser != null && eventToEdit != null)
            {
                foreach (var permission in currentUser.association_permissions.Where(aP => !aP.IsDeleted))
                {
                    foreach (var e in permission.associations.events)
                    {
                        if (e.Id == eventToEdit.Id)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        #endregion

        #region Add Event details in controls for editing

        private void AddEventInControls()
        {
            var currentUser = UserDB.GetUserByUsername(HttpContext.Current.User.Identity.Name);
            if (currentUser != null)
            {
                foreach (var permission in currentUser.association_permissions)
                {
                    DropDownAssociation.Items.Add(new ListItem
                    {
                        Text = permission.associations.Name,
                        Value = permission.associations.Id.ToString()
                    });
                }
                foreach (var permission in currentUser.association_permissions)
                {
                    foreach (
                    var subCat in
                        SubCategoryDB.GetAllSubCategoriesByAssociations(new[] { permission.associations })
                            .OrderBy(s => s.Name))
                    {
                        DropDownSubCategories.Items.Add(new ListItem(subCat.Name, subCat.Id.ToString()));
                    }
                }
                
            }

            //Gömmer kalendrarna från början.
            CalendarEndDate.Visible = false;
            CalendarStartDate.Visible = false;

            //Fyller formuläret med evenemangets nuvarande information.
            if (GetEventToUpdate() != null)
            {
                events @event = GetEventToUpdate();

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

                ToggleCheckBoxesIfWholeDayEvent();

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
            else
            {
                //Om man inte har ett evenemang att hämta information från 
                ErrorMessage.Text = "The event was not found!";

                //BtnUpdateEvent.Enabled = false;
                //BtnUpdateEvent.Visible = false;
                //BtnDeleteEvent.Enabled = false;
                //BtnDeleteEvent.Visible = false;
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


        #region Set/Get Event(Id) To Update

        private bool TryStoringEventIdFromQueryString()
        {
            int id;
            if (!string.IsNullOrWhiteSpace(Request.QueryString["Id"]) && int.TryParse(Request.QueryString["Id"], out id))
            {
                if (EventDB.GetEventById(id) != null)
                {
                    // Store the eventID so that only the event that was loaded with the page is the same event that is beeing updated
                    CurrentEventId.Value = id.ToString();
                    return true;
                }

                // Event with that Id was not found and will not be stored
                return false;
            }

            // The EventId from querystring was not in the right format or was missing
            return false;
        }
       
        public events GetEventToUpdate()
        {
            int id;
            if (!String.IsNullOrEmpty(CurrentEventId.Value ) && int.TryParse(CurrentEventId.Value, out id))
            {
                return EventDB.GetEventById(id);
            }
            return null;
        }

        #endregion


        #region ChkBoxDayEvent_OnCheckedChanged

        private void ToggleCheckBoxesIfWholeDayEvent()
        {
            //Gömmer tidsTextboxarna om man checkar heldags checkboxen.
            TxtBoxStartTime.Enabled = !ChkBoxDayEvent.Checked;
            TxtBoxStartTime.Visible = !ChkBoxDayEvent.Checked;
            TxtBoxEndTime.Enabled = !ChkBoxDayEvent.Checked;
            TxtBoxEndTime.Visible = !ChkBoxDayEvent.Checked;
        }

        protected void ChkBoxDayEvent_OnCheckedChanged(object sender, EventArgs e)
        {
            ToggleCheckBoxesIfWholeDayEvent();
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
        
        #region Button UpdateEvent
        protected void BtnUpdateEvent_OnClick(object sender, EventArgs e)
        {
            if (CheckUsersPermissionForEvent())
            {
                //Gör om texterna i textboxarna Start- och EndDate till typen DateTime, som används vid skapandet av evenemanget.
                var start = Convert.ToDateTime(TxtBoxStartDate.Text)
                    .Add(TimeSpan.FromHours(Convert.ToDateTime(TxtBoxStartTime.Text).Hour))
                    .Add(TimeSpan.FromMinutes(Convert.ToDateTime(TxtBoxStartTime.Text).Minute));
                var end = Convert.ToDateTime(TxtBoxEndDate.Text)
                    .Add(TimeSpan.FromHours(Convert.ToDateTime(TxtBoxEndTime.Text).Hour))
                    .Add(TimeSpan.FromMinutes(Convert.ToDateTime(TxtBoxEndTime.Text).Minute));

                // Hämtar valda Associations i ListBox och lägger dem i en List<associations>.
                var associationsList = new List<associations>();
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

                // Hämtar valda SubCategories i ListBox och lägger dem i en List<subcategories>.
                var subCategoriesList = new List<subcategories>();
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


                //Hämtar evenemanget som ska uppdateras.
                events ev = GetEventToUpdate();

                if (ev != null)
                {
                    int assoId;

                    //Nytt Event Objekt skapas och alla värdena från formuläret läggs in i objektet.
                    var evToUpdate = new events
                    {
                        Id = ev.Id,
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
                            (ChkBoxDayEvent.Checked)
                                ? Convert.ToDateTime(TxtBoxEndDate.Text).Add(new TimeSpan(23, 59, 59))
                                : end,
                        TargetGroup = TxtBoxTargetGroup.Text,
                        ApproximateAttendees =
                            !string.IsNullOrEmpty(TxtBoxApproximateAttendees.Text)
                                ? int.Parse(TxtBoxApproximateAttendees.Text)
                                : 0,
                                DisplayInCommunity = ChkBoxDisplayInCommunity.Checked,
                        associations = associationsList,
                        subcategories = subCategoriesList,
                        CreatedBy = HttpContext.Current.User.Identity.Name,
                        UpdatedBy = HttpContext.Current.User.Identity.Name
                    };


                    //Ger LabelMessage en större font-storlek.
                    LabelMessage.Style.Add(HtmlTextWriterStyle.FontSize, "25px");
                    if (EventDB.UpdateEvent(evToUpdate) != 0)
                    {
                        //Response.Redirect(
                        //    HttpContext.Current.Request.Url.AbsoluteUri.Replace(
                        //        HttpContext.Current.Request.Url.PathAndQuery, "/") + "EventDetails.aspx?Id=" + ev.Id,
                        //    false);
                        //Server.Transfer(Request.Url.AbsolutePath);
                        LabelMessage.ForeColor = Color.CornflowerBlue;
                        LabelMessage.Text = "Event was updated";
                    }
                    else
                    {
                        LabelMessage.ForeColor = Color.Red;
                        LabelMessage.Text = "Event couldn't be updated";
                    }
                }
            }
            else
            {
                LabelMessage.ForeColor = Color.Red;
                LabelMessage.Text = "You have no permission to edit this event!";
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
        
        
        #region Button DeleteEvent
        protected void BtnDeleteEvent_OnClick(object sender, EventArgs e)
        {
            if (CheckUsersPermissionForEvent())
            {
                //Hämtar evenemanget som ska uppdateras.
                events ev = GetEventToUpdate();

                if (ev != null)
                {
                    //Ger LabelMessage en större font-storlek som visar om eventet kunde uppdateras eller ej.
                    //Onödig kod ta bort och ersätt med css class
                    LabelMessage.Style.Add(HtmlTextWriterStyle.FontSize, "25px");

                    if (EventDB.DeleteEvent(ev))
                    {
                        LabelMessage.Text = "Event was deleted";
                        LabelMessage.ForeColor = Color.Green;
                        Response.Redirect(
                            HttpContext.Current.Request.Url.AbsoluteUri.Replace(
                                HttpContext.Current.Request.Url.PathAndQuery, "/") + "EventDetails.aspx?Id=" + ev.Id,
                            false);
                    }
                    else
                    {
                        LabelMessage.Text = "Event couldn't be updated";
                        LabelMessage.ForeColor = Color.Red;
                    }
                }
            }
            else
            {
                LabelMessage.ForeColor = Color.Red;
                LabelMessage.Text = "You have no permission to delete this event!";
            }
        }
        #endregion

        protected void ChkBoxDisplayInCommunity_OnCheckedChanged(object sender, EventArgs e)
        {

        }

    }
}