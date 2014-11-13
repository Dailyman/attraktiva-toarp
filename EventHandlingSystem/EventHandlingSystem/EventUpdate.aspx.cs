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
        

        protected void Page_Load(object sender, EventArgs e)
        {
            

            //if (Request.UrlReferrer != null)
            //{
            //    int index1 = Request.UrlReferrer.ToString().LastIndexOf("/");
            //    string url1 = Request.UrlReferrer.ToString().Substring(index1);

            //    int index2 = Request.ServerVariables["SCRIPT_NAME"].ToString().LastIndexOf("/");
            //    string url2 = Request.ServerVariables["SCRIPT_NAME"].ToString().Substring(index2);


            //    LabelMessage.Style.Add(HtmlTextWriterStyle.FontSize, "25px");
            //    LabelMessage.Text = (url1 == url2 ? "The event was Updated" : "Event couldn't be updated");
            //}



            RegExpValStartTime.ValidationExpression = @"^([01]?[0-9]|2[0-3]):[0-5][0-9]$";
            RegExpValEndTime.ValidationExpression = @"^([01]?[0-9]|2[0-3]):[0-5][0-9]$";

            ImageButtonStartDate.Style.Add("vertical-align", "top");
            ImageButtonEndDate.Style.Add("vertical-align", "top");

            
            if (!IsPostBack)
            {
                if (!String.IsNullOrWhiteSpace(Request.QueryString["Id"]))
                {
                    Event @event = GetEventToUpdate();

                    TxtBoxTitle.Text = @event.Title;
                    TxtBoxStartDate.Text = @event.StartDate.ToString("yyyy-MM-dd");
                    TxtBoxStartTime.Text = @event.StartDate.ToString("HH:mm");
                    TxtBoxEndDate.Text = @event.EndDate.ToString("yyyy-MM-dd");
                    TxtBoxEndTime.Text = @event.EndDate.ToString("HH:mm");
                    CalendarStartDate.SelectedDate = @event.StartDate;
                    CalendarEndDate.SelectedDate = @event.EndDate;
                    TxtBoxApproximateAttendees.Text = @event.ApproximateAttendees.ToString();

                    CalendarEndDate.Visible = false;
                    CalendarStartDate.Visible = false;

                }

                Term term = TermDB.GetTermById(1);

                DropDownAssociation.Items.Add(new ListItem
                {
                    Text = term.Name,
                    Value = "1"
                });

            }
        }




        public Event GetEventToUpdate()
        {
            int id;

            if (!string.IsNullOrWhiteSpace(Request.QueryString["Id"]) && int.TryParse(Request.QueryString["Id"], out id))
            {
                return EventDB.GetEventById(id);
            }
            else
            {
                return null;
            }
        }




        protected void ChkBoxDayEvent_OnCheckedChanged(object sender, EventArgs e)
        {

            TxtBoxStartTime.Enabled = !ChkBoxDayEvent.Checked;
            TxtBoxStartTime.Visible = !ChkBoxDayEvent.Checked;

            TxtBoxEndTime.Enabled = !ChkBoxDayEvent.Checked;
            TxtBoxEndTime.Visible = !ChkBoxDayEvent.Checked;
        }




        protected void ImageButtonStartDate_OnClick(object sender, ImageClickEventArgs e)
        {
            CalendarStartDate.Visible = CalendarStartDate.Visible == false;
        }

        protected void ImageButtonEndDate_OnClick(object sender, ImageClickEventArgs e)
        {
            CalendarEndDate.Visible = CalendarEndDate.Visible == false;
        }





        protected void TxtBoxStartDate_OnTextChanged(object sender, EventArgs e)
        {
            CustomValiStartDate.Validate();

            if (CustomValiStartDate.IsValid)
            {
                CalendarStartDate.SelectedDate = Convert.ToDateTime(TxtBoxStartDate.Text);
            }
            else
            {
                //TxtBoxStartDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
                //CalendarStartDate.SelectedDate = DateTime.Now.Date;
            }

        }

        protected void CustomValiStartDate_OnServerValidate(object source, ServerValidateEventArgs args)
        {
            DateTime result;
            args.IsValid = !string.IsNullOrWhiteSpace(TxtBoxStartDate.Text) && DateTime.TryParse(TxtBoxStartDate.Text, out result);
        }

        protected void TxtBoxEndDate_OnTextChanged(object sender, EventArgs e)
        {
            CustomValiEndDate.Validate();

            if (CustomValiEndDate.IsValid)
            {
                CalendarEndDate.SelectedDate = Convert.ToDateTime(TxtBoxEndDate.Text);
            }
            else
            {
                //TxtBoxEndDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
                //CalendarEndDate.SelectedDate = DateTime.Now.Date;
            }

        }

        protected void CustomValiEndDate_OnServerValidate(object source, ServerValidateEventArgs args)
        {
            DateTime result;
            args.IsValid = !string.IsNullOrWhiteSpace(TxtBoxEndDate.Text) && DateTime.TryParse(TxtBoxEndDate.Text, out result);
        }




        protected void CalendarStartDate_OnSelectionChanged(object sender, EventArgs e)
        {
            TxtBoxStartDate.Text = CalendarStartDate.SelectedDate.ToString("yyyy-MM-dd");
            //TxtBoxStartTime.Text = CalendarStartDate.SelectedDate.ToString("HH:mm");
        }

        protected void CalendarEndDate_OnSelectionChanged(object sender, EventArgs e)
        {
            TxtBoxEndDate.Text = CalendarEndDate.SelectedDate.ToString("yyyy-MM-dd");
            //TxtBoxEndTime.Text = CalendarEndDate.SelectedDate.ToString("HH:mm");
        }





        protected void BtnCreateEvent_OnClick(object sender, EventArgs e)
        {
            var start = Convert.ToDateTime(TxtBoxStartDate.Text)
                .Add(TimeSpan.FromHours(Convert.ToDateTime(TxtBoxStartTime.Text).Hour))
                .Add(TimeSpan.FromMinutes(Convert.ToDateTime(TxtBoxStartTime.Text).Minute));

            var end = Convert.ToDateTime(TxtBoxEndDate.Text)
                .Add(TimeSpan.FromHours(Convert.ToDateTime(TxtBoxEndTime.Text).Hour))
                .Add(TimeSpan.FromMinutes(Convert.ToDateTime(TxtBoxEndTime.Text).Minute));


            Event @event = GetEventToUpdate();

            if (@event != null)
            {
                Event updatedEvent = new Event
                {
                    Id = @event.Id,
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
                    ApproximateAttendees = long.Parse(TxtBoxApproximateAttendees.Text),
                    AssociationId = 1,
                    Created = @event.Created,
                    CreatedBy = "System",
                    LatestUpdate = DateTime.Now,
                    UpdatedBy = "System"
                    //IsDeleted = false
                };

                LabelMessage.Style.Add(HtmlTextWriterStyle.FontSize, "25px");
                if (EventDB.UpdateEvent(updatedEvent) != 0)
                {
                    //Server.Transfer(Request.Url.AbsolutePath);
                    LabelMessage.Text = "Event was updated";
                }
                else
                {
                    LabelMessage.Text = "Event couldn't be updated";
                }
            }

        }
    }
}