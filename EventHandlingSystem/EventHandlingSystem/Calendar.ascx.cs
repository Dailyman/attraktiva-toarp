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
            
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string month = txtMonth.Text; // should be in the format of Jan, Feb, Mar, Apr, etc...
            int yearofMonth = Convert.ToInt32(txtYear.Text);
            DateTime dateTime = Convert.ToDateTime("01-" + month + "-" + yearofMonth);

            DataTable table = new DataTable();
            DataRow row;

            table.Columns.Add("Mon");
            table.Columns.Add("Tue");
            table.Columns.Add("Wed");
            table.Columns.Add("Thu");
            table.Columns.Add("Fri");
            table.Columns.Add("Sat");
            table.Columns.Add("Sun");
            row = table.NewRow();

            //row[0] = 1 + " test";

            //table.Rows.Add(row);

            for (int i = 0; i < DateTime.DaysInMonth(dateTime.Year, dateTime.Month); i += 1)
            {
                //txtMonth.Text = Convert.ToDateTime(dateTime.AddDays(0)).ToString("dddd");
                txtMonth.Text = dateTime.ToString("dddd");


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

                if (i == DateTime.DaysInMonth(dateTime.Year, dateTime.Month) - 1)
                {
                    table.Rows.Add(row);
                    row = table.NewRow();
                }
            }

            GridView1.DataSource = table;
            GridView1.DataBind();
        }
        
    }
}





