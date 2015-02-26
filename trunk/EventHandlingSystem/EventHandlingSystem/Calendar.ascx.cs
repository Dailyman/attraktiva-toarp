using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Data;
using EventHandlingSystem.Database;

namespace EventHandlingSystem
{
    public partial class Calendar : System.Web.UI.UserControl
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                hdnDate.Value = DateTime.Now.ToString(); //("yyyy-MM");
                RenderCalendar(DateTime.Now);
            }
        }
        
        public void RenderCalendar(DateTime date)
        {
            DateTime dateTime = Convert.ToDateTime("01-" + date.ToString("MMMM") + "-" + date.Year);
            
            string dateText = dateTime.ToString("MMMM yyyy");
            lblCurrentDate.Text = char.ToUpper(dateText[0]) + dateText.Substring(1);

            DataTable table = new DataTable();

            table.Columns.Add("Week");
            table.Columns.Add("Mon");
            table.Columns.Add("Tue");
            table.Columns.Add("Wed");
            table.Columns.Add("Thu");
            table.Columns.Add("Fri");
            table.Columns.Add("Sat");
            table.Columns.Add("Sun");

            DataRow row = table.NewRow();


            var culture = new CultureInfo("en-US");
            
            for (int i = 0; i < DateTime.DaysInMonth(dateTime.Year, dateTime.Month); i += 1)
            {
                row["week"] = "<div class=\"table-small-cell\"><div class=\"cell-week\">" + GetIso8601WeekOfYear(dateTime.AddDays(i)) + "</div></div>";
                
                #region Den gamla TabelCell-läggaren
                //if (Convert.ToDateTime(dateTime.AddDays(i)).ToString("dddd") == "måndag")
                //{
                //    //row["Mon"] =
                //    //    "<div class=\"table-cell\" onclick=\"location.href='EventCreate.aspx?d=" + dateTime.AddDays(i).AddHours(DateTime.Now.Hour).ToString("yyyy-MM-dd HH:mm") + "';\" style=\"cursor: pointer;\">"
                //    //    + "<div class=\"cell-date " + SetCssClassIfToday(dateTime.AddDays(i)) + "\">" + (i + 1) + "</div>" + BuildEventInCalendarCell(dateTime.AddDays(i)) +
                //    //    " </div>"; 

                //    //(string.IsNullOrEmpty(BuildEventInCalendarCell(dateTime.AddDays(i))) ? "<br/>&nbsp;" : BuildEventInCalendarCell(dateTime.AddDays(i)))
                    
                //    row["Mon"] = CreateHtmlForDateCell(dateTime.AddDays(i));
                    
                    
                //}
                //if (dateTime.AddDays(i).ToString("dddd") == "tisdag")
                //{
                //    row["Tue"] = CreateHtmlForDateCell(dateTime.AddDays(i));
                //}
                //if (dateTime.AddDays(i).ToString("dddd") == "onsdag")
                //{
                //    row["Wed"] = CreateHtmlForDateCell(dateTime.AddDays(i)); 
                //}
                //if (dateTime.AddDays(i).ToString("dddd") == "torsdag")
                //{
                //    row["Thu"] = CreateHtmlForDateCell(dateTime.AddDays(i)); 
                //}
                //if (dateTime.AddDays(i).ToString("dddd") == "fredag")
                //{
                //    row["Fri"] = CreateHtmlForDateCell(dateTime.AddDays(i));
                //}
                //if (dateTime.AddDays(i).ToString("dddd") == "lördag")
                //{
                //    row["Sat"] = CreateHtmlForDateCell(dateTime.AddDays(i));
                //}
                //if (dateTime.AddDays(i).ToString("dddd") == "söndag")
                //{
                //    row["Sun"] = CreateHtmlForDateCell(dateTime.AddDays(i));

                //    table.Rows.Add(row);
                //    row = table.NewRow();
                //    continue;
                //}
                #endregion

                row[GetColumnNameByDate(dateTime.AddDays(i), culture)] = CreateHtmlForDateCell(dateTime.AddDays(i));

                if (dateTime.AddDays(i).ToString("dddd") == "söndag")
                {
                    table.Rows.Add(row);
                    row = table.NewRow();
                    continue;
                }

                if (i != DateTime.DaysInMonth(dateTime.Year, dateTime.Month) - 1)
                {
                    continue;
                }
                table.Rows.Add(row);
                row = table.NewRow();
            }

            GridViewCalendar.DataSource = table;
            GridViewCalendar.DataBind();
        }



        public string GetColumnNameByDate(DateTime date, CultureInfo culture )
        {
            return  culture.DateTimeFormat.GetDayName(date.DayOfWeek).Substring(0, 3);
        }

        public string CreateHtmlForDateCell(DateTime date)
        {
            return "<div class=\"table-cell\" data-date=\"" + date.AddHours(DateTime.Now.Hour).ToString("yyyy-MM-dd HH:mm") + "\" style=\"cursor: pointer;\">"
                        + "<div class=\"cell-date " + SetCssClassIfToday(date) + "\">" + (date.Day) + "</div>" + BuildEventInCalendarCell(date) +
                        " </div>";
        }

        public string SetCssClassIfToday(DateTime date)
        {
            if (date.Date == DateTime.Now.Date)
            {
                return "cell-today";
            }
            return "";
        }
        
        //Metod som bygger upp en div, kollar först datumet har några events
        public string BuildEventInCalendarCell(DateTime date)
        {
            string divs = ""; 
            var stType = Request.QueryString["Type"];

            if (!string.IsNullOrWhiteSpace(stType))
            {
                if (String.Equals(stType, "c", StringComparison.OrdinalIgnoreCase))
                {
                    communities c = CommunityDB.GetCommunityByName(Page.Title);
                    List<events> commEvents = new List<events>();
                    if (c != null)
                    {
                        foreach (var eventInMonth in EventDB.GetAllEventsInMonth(date))
                        {
                            foreach (communities community in eventInMonth.communities)
                            {
                                if (community.Id == c.Id)
                                {
                                    commEvents.Add(eventInMonth);
                                }
                            }
                        }
                        divs = GetHtmlForEventCellsByEventList(commEvents, date);
                    }
                }
                else if (String.Equals(stType, "a", StringComparison.OrdinalIgnoreCase))
                {
                    associations a = AssociationDB.GetAssociationByName(Page.Title);
                    List<events> assoEvents = new List<events>();
                    if (a != null)
                    {
                        foreach (var eventInMonth in EventDB.GetAllEventsInMonth(date))
                        {
                            foreach (associations association in eventInMonth.associations)
                            {
                                if (association.Id == a.Id)
                                {
                                    assoEvents.Add(eventInMonth);
                                }
                            }
                        }
                        divs = GetHtmlForEventCellsByEventList(assoEvents, date);
                    }
                }
            }
            else
            {
                divs = GetHtmlForEventCellsByEventList(EventDB.GetAllEventsInMonth(date), date);
            }
            return divs;
        }

        private string GetHtmlForEventCellsByEventList(IEnumerable<events> eList, DateTime date)
        {
            string htmlEventCells = "";
            foreach (var ev in eList)
            {
                if (date.Date >= ev.StartDate.Date && date.Date <= ev.EndDate.Date)
                {
                    htmlEventCells += "<a target=\"_blank\" href=\"/EventDetails?id=" + ev.Id + "\"><div class=\"event-in-cell\" >" + ev.Title + "</div></a>";
                }
            }
            return htmlEventCells;
        }


        protected void btnBackArrow_OnClick(object sender, EventArgs e)
        {
            hdnDate.Value = Convert.ToDateTime(hdnDate.Value).AddMonths(-1).ToString();
            RenderCalendar(Convert.ToDateTime(hdnDate.Value));
        }

        protected void btnForwardArrow_OnClick(object sender, EventArgs e)
        {
            hdnDate.Value = Convert.ToDateTime(hdnDate.Value).AddMonths(1).ToString();
            RenderCalendar(Convert.ToDateTime(hdnDate.Value));    
        }

        // This presumes that weeks start with Monday.
        // Week 1 is the 1st week of the year with a Thursday in it.
        public static int GetIso8601WeekOfYear(DateTime time)
        {
            // Seriously cheat.  If its Monday, Tuesday or Wednesday, then it'll 
            // be the same week# as whatever Thursday, Friday or Saturday are,
            // and we always get those right
            DayOfWeek day = CultureInfo.InvariantCulture.Calendar.GetDayOfWeek(time);
            if (day >= DayOfWeek.Monday && day <= DayOfWeek.Wednesday)
            {
                time = time.AddDays(3);
            }

            // Return the week of our adjusted day
            return CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(time, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
        }
    }
}
    





