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
using Microsoft.Ajax.Utilities;

namespace EventHandlingSystem
{
    public partial class Calendar : System.Web.UI.UserControl
    {
        public string CommunityId { get; set; }
        public string AssociationId { get; set; }
        public string DisplayDate { get; set; }

        public bool OpenEventInSameWindow { get; set; }

        public Calendar()
        {
            this.CommunityId = "";
            this.AssociationId = "";
            this.DisplayDate = "";
        }

        public Calendar(string comm,  string asso, string showDate)
        {
            this.CommunityId = comm;
            this.AssociationId = asso;
            this.DisplayDate = showDate;
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DateTime monthToDisplay;
                if (DateTime.TryParse(DisplayDate, out monthToDisplay))
                {
                    hdnDate.Value = monthToDisplay.ToString();
                    RenderCalendar(monthToDisplay);
                }
                else
                {
                    hdnDate.Value = DateTime.Now.ToString();
                    RenderCalendar(DateTime.Now);
                }
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
        
        // Filtrerar events om Kalendern finns på en Comm eller Asso sida.
        public string BuildEventInCalendarCell(DateTime date)
        {
            string divs = "";

            int id;
            if (!String.IsNullOrEmpty(CommunityId) && int.TryParse(CommunityId, out id))
            {
                communities c = CommunityDB.GetCommunityById(id);
                var commEvents = new List<events>();
                if (c != null)
                    {
                        foreach (var eventInMonth in EventDB.GetAllEventsInMonth(date))
                        {
                            foreach (communities community in eventInMonth.communities)
                            {
                                if (community.Id == c.Id && !commEvents.Contains(eventInMonth))
                                {
                                    commEvents.Add(eventInMonth);
                                }
                            }
                            // Add if the event is set to be displayed in the community of the association
                            if (eventInMonth.DisplayInCommunity)
                            {
                                foreach (var assoInComm in c.associations)
                                {
                                    // Add the event if any association connected to the event is also an association in the community
                                    if (eventInMonth.associations.Contains(assoInComm) && !commEvents.Contains(eventInMonth))
                                    {
                                        commEvents.Add(eventInMonth);
                                    }
                                }
                            }

                        }
                        divs = GetHtmlForEventCellsByEventList(commEvents, date);
                    }
            }
            else if (!String.IsNullOrEmpty(AssociationId) && int.TryParse(AssociationId, out id))
            {
                associations a = AssociationDB.GetAssociationById(id);
                var assoEvents = new List<events>();
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
            else
            {
                divs = GetHtmlForEventCellsByEventList(EventDB.GetAllEventsInMonth(date), date);
            }

            //var stType = Request.QueryString["Type"];

            //if (!string.IsNullOrWhiteSpace(stType))
            //{
            //    if (String.Equals(stType, "c", StringComparison.OrdinalIgnoreCase))
            //    {
            //        communities c = CommunityDB.GetCommunityByName(Page.Title);
            //        var commEvents = new List<events>();
            //        if (c != null)
            //        {
            //            foreach (var eventInMonth in EventDB.GetAllEventsInMonth(date))
            //            {
            //                foreach (communities community in eventInMonth.communities)
            //                {
            //                    if (community.Id == c.Id)
            //                    {
            //                        commEvents.Add(eventInMonth);
            //                    }
            //                }
            //            }
            //            divs = GetHtmlForEventCellsByEventList(commEvents, date);
            //        }
            //    }
            //    else if (String.Equals(stType, "a", StringComparison.OrdinalIgnoreCase))
            //    {
            //        associations a = AssociationDB.GetAssociationByName(Page.Title);
            //        List<events> assoEvents = new List<events>();
            //        if (a != null)
            //        {
            //            foreach (var eventInMonth in EventDB.GetAllEventsInMonth(date))
            //            {
            //                foreach (associations association in eventInMonth.associations)
            //                {
            //                    if (association.Id == a.Id)
            //                    {
            //                        assoEvents.Add(eventInMonth);
            //                    }
            //                }
            //            }
            //            divs = GetHtmlForEventCellsByEventList(assoEvents, date);
            //        }
            //    }
            //}
            //else
            //{
            //    divs = GetHtmlForEventCellsByEventList(EventDB.GetAllEventsInMonth(date), date);
            //}
            return divs;
        }

        // Metod som bygger upp html-kod, kollar först om datumet har några events
        private string GetHtmlForEventCellsByEventList(IEnumerable<events> eList, DateTime date)
        {
            string htmlEventCells = "";

            foreach (var ev in eList.OrderBy(e => e.DayEvent).ThenBy(e => e.StartDate))
            {
                if (date.Date >= ev.StartDate.Date && date.Date <= ev.EndDate.Date)
                {

                    //htmlEventCells += "<a target=\"_blank\" href=\"/EventDetails?id=" + ev.Id + "\">" +
                    //                    "<div id=\"" + ev.Id + "\" class=\"event-in-cell\" >" + ev.Title + "</div>" +
                    //                  "</a>" +
                    //                  "<div id=\"" + ev.Id +"\" class=\"event-pop-up\">" +
                    //                    "<div class=\"arrow-up\"></div>" +
                    //                    "<div class=\"pop-up-text\">" +
                    //                    "<b>Title</b><br/> " + ev.Title + 
                    //                    "<br/><b>Date</b><br/> " +
                    //                    ev.StartDate.ToString("dddd, MMM d, HH:mm") +
                    //                    (ev.StartDate.Date == ev.EndDate.Date
                    //                        ? "-" + ev.EndDate.ToString("HH:mm")
                    //                        : "-<br/>" + ev.EndDate.ToString("dddd, MMM d, HH:mm")) +
                    //                    "<br/><b>Location</b><br/> " + ev.Location + 
                    //                    "<br/><b>Summary</b><br/> " + ev.Summary + 
                    //                    "<br/><b>Association" + (ev.associations.Count > 1 ? "s" : "") + "</b><br/> " + WriteAllAssociations(ev.associations) + 
                    //                    "</div>" +
                    //                  "</div>";

                    htmlEventCells += "<a target=\"" + (OpenEventInSameWindow ? "_self" : "_blank") +
                                      "\" href=\"/EventDetails?id=" + ev.Id + "\">" +
                                      "<div id=\"" + ev.Id + "\" class=\"event-in-cell " +
                                      (!ev.DayEvent ? "one-day" : "multiple-days") + "\" >" + ev.Title + "</div>" +
                                      "</a>" +
                                      "<div id=\"" + ev.Id + "\" class=\"event-pop-up\">" +
                                      "<div class=\"arrow-up\"></div>" +
                                      "<div class=\"pop-up-text\">" +
                                      "<p><b>" +
                                      ev.Title + "</b></p><br>" +
                                      "<p><b>Date</b><br>" +
                                      ev.StartDate.ToString("dddd, MMM d, HH:mm") +
                                      (ev.StartDate.Date == ev.EndDate.Date
                                          ? "-" + ev.EndDate.ToString("HH:mm")
                                          : "-<br/>" + ev.EndDate.ToString("dddd, MMM d, HH:mm")) +
                                      "</p><p><b>Location</b><br>" + ev.Location +
                                      "</p><p><b>Summary</b><br>" + ev.Summary +
                                      "</p><p><b>Association" + (ev.associations.Count > 1 ? "s" : "") + "</b><br>" +
                                      WriteAllAssociations(ev.associations) +
                                      "</p></div>" +
                                      "</div>";
                }
            }
            return htmlEventCells;
        }

        public string WriteAllAssociations(ICollection<associations> list)
        {
            string result = "";
            foreach (var association in list.OrderBy(a => a.Name))
            {
                result += association.Name + ", ";
            }
            if (String.IsNullOrEmpty(result))
            {
                return result;
            }
            else
            {
                return result.TrimEnd(',', ' ');
            }
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
    





