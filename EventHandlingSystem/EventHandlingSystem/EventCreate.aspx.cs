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
    public partial class EventCreate : Page
    {
        #region Page_Load
        protected void Page_Load(object sender, EventArgs e)
        {
            RegExpValStartTime.ValidationExpression = @"^([01]?[0-9]|2[0-3]):[0-5][0-9]$";
            RegExpValEndTime.ValidationExpression = @"^([01]?[0-9]|2[0-3]):[0-5][0-9]$";

            ImageButtonStartDate.Style.Add("vertical-align", "top");
            ImageButtonEndDate.Style.Add("vertical-align", "top");
            
            if (!IsPostBack)
            {
                TxtBoxStartDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
                TxtBoxStartTime.Text = "00:00";
                CalendarStartDate.SelectedDate = DateTime.Now.Date;

                TxtBoxEndDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
                TxtBoxEndTime.Text = "00:00";
                CalendarEndDate.SelectedDate = DateTime.Now.Date;

                CalendarEndDate.Visible = false;
                CalendarStartDate.Visible = false;

                //Skapar och lägger till alla associations i dropdownboxen.
                List<ListItem> listItems = new List<ListItem>();
                foreach (var association in AssociationDB.GetAllAssociations())
                {
                    Term associationTerm =
                        TermDB.GetAllTermsByTermSet(TermSetDB.GetTermSetById(association.PublishingTermSetId))
                            .SingleOrDefault();
                    if (associationTerm != null)
                    {
                        listItems.Add(new ListItem
                        {
                            Text = TermSetDB.GetTermSetById(association.PublishingTermSetId).Name,
                            Value = associationTerm.Id.ToString()
                        });
                    }
                }
                foreach (var item in listItems.OrderBy(item => item.Text))
                {
                    DropDownAssociation.Items.Add(item);
                }
            }
        }
        #endregion


        #region ChkBoxDayEvent_OnCheckedChanged
        protected void ChkBoxDayEvent_OnCheckedChanged(object sender, EventArgs e)
        {

            TxtBoxStartTime.Enabled = !ChkBoxDayEvent.Checked;
            TxtBoxStartTime.Visible = !ChkBoxDayEvent.Checked;

            TxtBoxEndTime.Enabled = !ChkBoxDayEvent.Checked;
            TxtBoxEndTime.Visible = !ChkBoxDayEvent.Checked;
        }
        #endregion


        #region ImageButtonStartDate_OnClick : ImageButtonEndDate_OnClick
        protected void ImageButtonStartDate_OnClick(object sender, ImageClickEventArgs e)
        {
            CalendarStartDate.Visible = CalendarStartDate.Visible == false;
        }


        protected void ImageButtonEndDate_OnClick(object sender, ImageClickEventArgs e)
        {
            CalendarEndDate.Visible = CalendarEndDate.Visible == false;
        }
        #endregion


        #region TxtBoxStartDate_OnTextChanged
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
        #endregion


        #region CustomValiStartDate_OnServerValidate
        protected void CustomValiStartDate_OnServerValidate(object source, ServerValidateEventArgs args)
        {
            DateTime result;
            args.IsValid = !string.IsNullOrWhiteSpace(TxtBoxStartDate.Text) && DateTime.TryParse(TxtBoxStartDate.Text, out result);
        }
        #endregion


        #region TxtBoxEndDate_OnTextChanged
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
        #endregion


        #region CustomValiEndDate_OnServerValidate
        protected void CustomValiEndDate_OnServerValidate(object source, ServerValidateEventArgs args)
        {
            DateTime result;
            args.IsValid = !string.IsNullOrWhiteSpace(TxtBoxEndDate.Text) && DateTime.TryParse(TxtBoxEndDate.Text, out result);
        }
        #endregion


        #region CalendarStartDate_OnSelectionChanged : CalendarEndDate_OnSelectionChanged
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
        #endregion


        #region BtnCreateEvent_OnClick
        protected void BtnCreateEvent_OnClick(object sender, EventArgs e)
        {
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
                Created = DateTime.Now,
                CreatedBy = HttpContext.Current.User.Identity.Name
                //IsDeleted = false
            };
            
            LabelMessage.Style.Add(HtmlTextWriterStyle.FontSize, "25px");
            if (EventDB.AddEvent(@event))
            {
                Response.Redirect(HttpContext.Current.Request.Url.AbsoluteUri.Replace(HttpContext.Current.Request.Url.PathAndQuery, "/") + "EventDetails.aspx?Id=" + @event.Id, false);
                //LabelMessage.Text = "Event was created";
            }
            else
            {
                LabelMessage.Text = "Event couldn't be created";
            }
        }
        #endregion
    }
}