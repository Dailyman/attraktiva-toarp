using System;
using System.Collections.Generic;
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
            //RegEx för att kontrollera att rätt tidsformat används i TimeTextboxarna.
            RegExpValStartTime.ValidationExpression = @"^([01]?[0-9]|2[0-3]):[0-5][0-9]$";
            RegExpValEndTime.ValidationExpression = @"^([01]?[0-9]|2[0-3]):[0-5][0-9]$";

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

                //Gömmer kalendrarna från början.
                CalendarEndDate.Visible = false;
                CalendarStartDate.Visible = false;

                //Fyller formuläret med evenemangets nuvarande information.
                if (!String.IsNullOrWhiteSpace(Request.QueryString["Id"]))
                {
                    events @event = GetEventToUpdate();

                    TxtBoxTitle.Text = @event.Title;
                    TxtBoxDescription.Text = @event.Description;
                    TxtBoxSummary.Text = @event.Summary;
                    TxtBoxOther.Text = @event.Other;
                    TxtBoxLocation.Text = @event.Location;
                    TxtBoxImageUrl.Text = @event.ImageUrl;
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
                    var firstOrDefault   = @event.associationsinevents.FirstOrDefault();
                    if (firstOrDefault != null)
                    {
                        DropDownAssociation.SelectedIndex =
                            DropDownAssociation.Items.IndexOf(
                                DropDownAssociation.Items.FindByValue(
                                    firstOrDefault.associations.Id.ToString()));
                    }
                }
                else
                {
                    //Om man inte har ett evenemang att hämta information från 
                    BtnUpdateEvent.Enabled = false;
                    BtnUpdateEvent.Visible = false;
                }
            }
        }

        #endregion


        #region GetEventToUpdate

        //Hämtar värdet från Id i URL och hämtar sedan evenemanget med samma Id.
        public events GetEventToUpdate()
        {
            int id;
            if (!string.IsNullOrWhiteSpace(Request.QueryString["Id"]) && int.TryParse(Request.QueryString["Id"], out id))
            {
                //Om denna returneras...
                return EventDB.GetEventById(id);
            }

            //...kommer denna ej returneras
            return null;
        }

        #endregion


        #region ChkBoxDayEvent_OnCheckedChanged

        protected void ChkBoxDayEvent_OnCheckedChanged(object sender, EventArgs e)
        {
            //Gömmer tidsTextboxarna om man checkar heldagscheckboxen.
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


        #region BtnUpdateEvent_OnClick

        protected void BtnUpdateEvent_OnClick(object sender, EventArgs e)
        {
            //Gör om texterna i textboxarna Start- och EndDate till typen DateTime, som används vid skapandet av evenemanget.
            var start = Convert.ToDateTime(TxtBoxStartDate.Text)
                .Add(TimeSpan.FromHours(Convert.ToDateTime(TxtBoxStartTime.Text).Hour))
                .Add(TimeSpan.FromMinutes(Convert.ToDateTime(TxtBoxStartTime.Text).Minute));
            var end = Convert.ToDateTime(TxtBoxEndDate.Text)
                .Add(TimeSpan.FromHours(Convert.ToDateTime(TxtBoxEndTime.Text).Hour))
                .Add(TimeSpan.FromMinutes(Convert.ToDateTime(TxtBoxEndTime.Text).Minute));

            //Hämtar evenemanget som ska uppdateras.
            events ev = GetEventToUpdate();

            if (ev != null)
            {
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
                    DayEvent = ChkBoxDayEvent.Checked,
                    StartDate = (ChkBoxDayEvent.Checked) ? Convert.ToDateTime(TxtBoxStartDate.Text) : start,
                    EndDate =
                        (ChkBoxDayEvent.Checked)
                            ? Convert.ToDateTime(TxtBoxEndDate.Text).Add(new TimeSpan(23, 59, 0))
                            : end,
                    TargetGroup = TxtBoxTargetGroup.Text,
                    ApproximateAttendees =
                        !string.IsNullOrEmpty(TxtBoxApproximateAttendees.Text)
                            ? int.Parse(TxtBoxApproximateAttendees.Text)
                            : 0,
                    CreatedBy = HttpContext.Current.User.Identity.Name,
                    UpdatedBy = HttpContext.Current.User.Identity.Name,
                    associationsinevents = new associationsinevents[]
                    {
                        //Lägg till vvv HÄR vvv kod för att kunna skapa events med fler associations kopplade till sig....
                        new associationsinevents()
                        {
                            associations =
                                AssociationDB.GetAssociationById(int.Parse(DropDownAssociation.SelectedItem.Value)),
                            events = ev
                        }
                    }

                };

                //Ger LabelMessage en större font-storlek som visar om eventet kunde uppdateras eller ej.
                LabelMessage.Style.Add(HtmlTextWriterStyle.FontSize, "25px");
                if (EventDB.UpdateEvent(evToUpdate) != 0)
                {
                    //Response.Redirect(
                    //    HttpContext.Current.Request.Url.AbsoluteUri.Replace(
                    //        HttpContext.Current.Request.Url.PathAndQuery, "/") + "EventDetails.aspx?Id=" + ev.Id,
                    //    false);
                    //Server.Transfer(Request.Url.AbsolutePath);
                    LabelMessage.Text = "Event was updated";
                }
                else
                {
                    LabelMessage.Text = "Event couldn't be updated";
                }
            }
        }

        #endregion
    }
}