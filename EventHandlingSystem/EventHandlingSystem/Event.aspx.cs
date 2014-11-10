using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EventHandlingSystem.Database;

namespace EventHandlingSystem
{
    public partial class Event1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CalendarEndDate.Visible = false;
                CalendarStartDate.Visible = false;
            }
        }


        protected void ImageButtonStartDate_OnClick(object sender, ImageClickEventArgs e)
        {
            CalendarStartDate.Visible = CalendarStartDate.Visible == false ? true : false;
        }

        protected void ImageButtonEndDate_OnClick(object sender, ImageClickEventArgs e)
        {
            CalendarEndDate.Visible = CalendarEndDate.Visible == false ? true : false;
        }

        protected void TxtBoxStartDate_OnTextChanged(object sender, EventArgs e)
        {
            DateTime startDate = Convert.ToDateTime(TxtBoxStartDate.Text);
            CalendarStartDate.SelectedDate = startDate;
        }

        protected void TxtBoxEndDate_OnTextChanged(object sender, EventArgs e)
        {

        }

        protected void CalendarStartDate_OnSelectionChanged(object sender, EventArgs e)
        {
            TxtBoxStartDate.Text = CalendarStartDate.SelectedDate.ToString("yyyy-MM-dd");
            TxtBoxStartTime.Text = CalendarStartDate.SelectedDate.ToString("HH:mm");
        }

        protected void CalendarEndDate_OnSelectionChanged(object sender, EventArgs e)
        {
            TxtBoxEndDate.Text = CalendarEndDate.SelectedDate.ToString("yyyy-MM-dd");
            TxtBoxEndTime.Text = CalendarEndDate.SelectedDate.ToString("HH:mm");
        }


        protected void BtnCreateEvent_OnClick(object sender, EventArgs e)
        {
            ReqFieldValiApproxAttend.Enabled = true;
            var start = Convert.ToDateTime(TxtBoxStartDate.Text)
                .Add(TimeSpan.FromHours(Convert.ToDateTime(TxtBoxStartTime.Text).Hour))
                .Add(TimeSpan.FromMinutes(Convert.ToDateTime(TxtBoxStartTime.Text).Minute));

            var end = Convert.ToDateTime(TxtBoxEndDate.Text)
                .Add(TimeSpan.FromHours(Convert.ToDateTime(TxtBoxEndTime.Text).Hour))
                .Add(TimeSpan.FromMinutes(Convert.ToDateTime(TxtBoxEndTime.Text).Minute));

            var @event = new Event
            {
                Title = TxtBoxTitle.Text,
                Description = TxtBoxDescription.Text,
                Summary = TxtBoxSummary.Text,
                Other = "",
                Location = "",
                ImageUrl = "",
                DayEvent = ChkBoxDayEvent.Checked,
                StartDate = start,
                EndDate = end,
                TargetGroup = "",
                ApproximateAttendees = long.Parse(TxtBoxApproximateAttendees.Text),
                AssociationId = 1,
                Created = DateTime.Now,
                CreatedBy = "System",
                //IsDeleted = false
            };


            LabelMessage.Text = (EventDB.AddEvent(@event)) ? "Event was created" : "Event couldn't be created";

            //LabelMessage.Text = EventDB.AddEvent(@event);

        }


        
    }
}