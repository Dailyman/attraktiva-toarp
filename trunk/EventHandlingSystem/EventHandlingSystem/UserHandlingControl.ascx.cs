using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EventHandlingSystem
{
    public partial class UserHandlingControl : System.Web.UI.UserControl
    {
        private bool _isonline;
        private string _username;
        private string _email;
        private string _comment;
        private bool _isapproved;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BuildGridView();
            }



        }


        private void BuildGridView()
        {
            //GridViewUsers.DataSource = CreateDataTable();
            GridViewUsers.DataSource = Membership.GetAllUsers();
            GridViewUsers.DataBind();
        }

        private DataTable CreateDataTable()
        {
            DataTable table = new DataTable();
            //table.Columns.Add("IsOnline", typeof (bool));
            table.Columns.Add("Username", typeof (string));
            table.Columns.Add("Email", typeof (string));
            //table.Columns.Add("LastLoginDate", typeof (DateTime));
            //table.Columns.Add("IsLockedOut", typeof (bool));
            //table.Columns.Add("IsLockedOut", typeof(Button));
            //table.Columns.Add("Drug", typeof(int));
            //table.Columns.Add("Patient", typeof(string));
            //table.Columns.Add("Date", typeof(DateTime));

            foreach (MembershipUser user in Membership.GetAllUsers())
            {
                table.Rows.Add(user.UserName, user.Email);
                //table.Rows.Add(user.IsOnline, user.UserName, user.Email, user.LastLoginDate, user.IsLockedOut);
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

        protected void GridViewUsers_OnRowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridViewUsers.EditIndex = -1;
            BuildGridView();
        }

        protected void GridViewUsers_OnRowEditing(object sender, GridViewEditEventArgs e)
        {
            GridViewUsers.EditIndex = e.NewEditIndex;
            BuildGridView();
        }

        protected void GridViewUsers_OnRowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            _username = GridViewUsers.Rows[e.RowIndex].Cells[1].Text;
            Membership.DeleteUser(_username);
            if (Membership.GetUser(_username) == null)
            {
                LabelDisplay.Text = string.Format("{0} was deleted Successfully", _username);
                LabelDisplay.ForeColor = Color.Green;
            }
            else
            {
                LabelDisplay.Text = string.Format("{0} was could not be deleted", _username);
                LabelDisplay.ForeColor = Color.Red;
            }
            BuildGridView();
        }

        protected void GridViewUsers_OnRowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int index = GridViewUsers.EditIndex;
            GridViewRow gvrow = GridViewUsers.Rows[index];
            _isonline = ((CheckBox)gvrow.Cells[0].Controls[0]).Checked;
            _username = GridViewUsers.Rows[e.RowIndex].Cells[1].Text;
            _email = ((TextBox) gvrow.Cells[2].Controls[0]).Text.Trim();
            _comment = ((TextBox) gvrow.Cells[3].Controls[0]).Text;
            _isapproved = ((CheckBox) gvrow.Cells[4].Controls[0]).Checked;

            MembershipUser user = Membership.GetUser(_username);

            if (user != null)
            {
                user.Email = _email;
                user.Comment = _comment;
                user.IsApproved = _isapproved;
                Membership.UpdateUser(user);
                LabelDisplay.Text = string.Format("{0} Details updated Successfully", _username);
                LabelDisplay.ForeColor = Color.Green;
            }
            GridViewUsers.EditIndex = -1;
            BuildGridView();
        }


        protected void GridViewUsers_OnRowUpdated(object sender, GridViewUpdatedEventArgs e)
        {
            foreach (MembershipUser user in Membership.GetAllUsers())
            {
                Membership.UpdateUser(user);
            }
            BuildGridView();
        }



        protected void btnCreateUser_OnClick(object sender, EventArgs e)
        {
            string username = txtUserName.Text.Trim();
            string email = txtEmail.Text.Trim();
            string password = txtPassword.Text;
            string confirmPassword = txtConfirmPassword.Text;

            if (password == confirmPassword)
            {
                if (Membership.GetUser(username) == null)
                {
                    if (password.Length >= Membership.MinRequiredPasswordLength)
                    {
                        Membership.CreateUser(username, password, email);
                        LabelDisplay.Text = string.Format("{0} was created Successfully", username);
                        LabelDisplay.ForeColor = Color.Green;
                    }
                    else
                    {
                        LabelDisplay.Text = string.Format("The password is not valid");
                        LabelDisplay.ForeColor = Color.Red;
                    }
                }
                else
                {
                    LabelDisplay.Text = string.Format("{0} is not valid or already exist", username);
                    LabelDisplay.ForeColor = Color.Red;
                }
            }
            else
            {
                LabelDisplay.Text = string.Format("The password is not the same");
                LabelDisplay.ForeColor = Color.Red;
            }

            BuildGridView();
        }
    }
}