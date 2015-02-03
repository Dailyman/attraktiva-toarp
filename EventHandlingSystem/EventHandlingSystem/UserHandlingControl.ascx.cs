using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EventHandlingSystem
{
    public partial class UserHandlingControl : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            
            BuildGridView();

        }
        

        private void BuildGridView()
        {
            GridViewUsers.DataSource = CreateDataTable();
            GridViewUsers.DataBind();
        }

        private DataTable CreateDataTable()
        {
            DataTable table = new DataTable();
            table.Columns.Add("IsOnline", typeof (bool));
            table.Columns.Add("Username", typeof (string));
            table.Columns.Add("Email", typeof (string));
            table.Columns.Add("LastLoginDate", typeof (DateTime));
            table.Columns.Add("IsLockedOut", typeof (bool));
            //table.Columns.Add("IsLockedOut", typeof(Button));
            //table.Columns.Add("Drug", typeof(int));
            //table.Columns.Add("Patient", typeof(string));
            //table.Columns.Add("Date", typeof(DateTime));
            
            foreach (MembershipUser user in Membership.GetAllUsers())
            {
                table.Rows.Add(user.IsOnline, user.UserName, user.Email, user.LastLoginDate, user.IsLockedOut);
            }

            return table;
        }

        public void Update()
        {
            //Membership.UpdateUser();
                //GridViewUsers.UpdateRow(GridViewUsers.EditIndex, true);
        }

        protected void BtnEdit_OnClick(object sender, EventArgs e)
        {
            GridViewUsers.SetEditRow(GridViewUsers.SelectedIndex);
            //LabelTest.Text = GridViewUsers.SelectedRow.DataItem.ToString();

        }
       
        protected void GridViewUsers_OnRowEditing(object sender, GridViewEditEventArgs e)
        {
            
        }

        protected void GridViewUsers_OnRowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            
        }
    }
}