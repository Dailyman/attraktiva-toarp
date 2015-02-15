using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Data;

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

            table.Columns.Add("Mon");
            table.Columns.Add("Tue");
            table.Columns.Add("Wed");
            table.Columns.Add("Thu");
            table.Columns.Add("Fri");
            table.Columns.Add("Sat");
            table.Columns.Add("Sun");

            DataRow row = table.NewRow();

            for (int i = 0; i < DateTime.DaysInMonth(dateTime.Year, dateTime.Month); i += 1)
            {
                if (Convert.ToDateTime(dateTime.AddDays(i)).ToString("dddd") == "måndag")
                {
                    row["Mon"] = "<div class=\"table-event\">" + (i + 1) + "</div>";
                }
                if (dateTime.AddDays(i).ToString("dddd") == "tisdag")
                {
                    row["Tue"] = "<div class=\"table-event\">" + (i + 1) + "</div>";
                }
                if (dateTime.AddDays(i).ToString("dddd") == "onsdag")
                {
                    row["Wed"] = "<div class=\"table-event\">" + (i + 1) + "</div>";
                }
                if (dateTime.AddDays(i).ToString("dddd") == "torsdag")
                {
                    row["Thu"] = "<div class=\"table-event\">" + (i + 1) + "</div>";
                }
                if (dateTime.AddDays(i).ToString("dddd") == "fredag")
                {
                    row["Fri"] = "<div class=\"table-event\">" + (i + 1) + "</div>";
                }
                if (dateTime.AddDays(i).ToString("dddd") == "lördag")
                {
                    row["Sat"] = "<div class=\"table-event\">" + (i + 1) + "</div>";
                }
                if (dateTime.AddDays(i).ToString("dddd") == "söndag")
                {
                    row["Sun"] = "<div class=\"table-event\">" + (i + 1) + "</div>";

                    table.Rows.Add(row);
                    row = table.NewRow();
                    continue;
                }

                if (i != DateTime.DaysInMonth(dateTime.Year, dateTime.Month) - 1) continue;
                table.Rows.Add(row);
                row = table.NewRow();
            }

            GridView1.DataSource = table;
            GridView1.DataBind();
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
    }
}
    





