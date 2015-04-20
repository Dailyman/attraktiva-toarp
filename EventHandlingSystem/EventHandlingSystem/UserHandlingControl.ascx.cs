using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Providers.Entities;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using EventHandlingSystem.Database;

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
            

            //int index = GridViewUsers.EditIndex;
            //GridViewRow gvrow = GridViewUsers.Rows[index];

            //LinkButton cancelEditBtn = (LinkButton)gvrow.FindControl("LinkButtonCancelEdit");

            //cancelEditBtn.Visible = !cancelEditBtn.Visible;

            GridViewUsers.EditIndex = -1;
            BuildGridView();

            
        }

        protected void GridViewUsers_OnRowEditing(object sender, GridViewEditEventArgs e)
        {
            GridViewUsers.EditIndex = e.NewEditIndex;
            BuildGridView();

            int index = GridViewUsers.EditIndex;
            GridViewRow gvrow = GridViewUsers.Rows[index];

            LinkButton editEventBtn = (LinkButton)gvrow.FindControl("LinkButtonEditEvent");
            LinkButton cancelEditBtn = (LinkButton)gvrow.FindControl("LinkButtonCancelEdit");
            LinkButton updateEventBtn = (LinkButton)gvrow.FindControl("LinkButtonUpdateEvent");
            LinkButton deleteEventBtn = (LinkButton)gvrow.FindControl("LinkButtonDeleteEvent");
            

            editEventBtn.Visible = !editEventBtn.Visible;
            cancelEditBtn.Visible = !cancelEditBtn.Visible;
            updateEventBtn.Visible = !updateEventBtn.Visible;
            deleteEventBtn.Visible = !deleteEventBtn.Visible;

        }

        protected void GridViewUsers_OnRowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            _username = GridViewUsers.Rows[e.RowIndex].Cells[1].Text;
            Membership.DeleteUser(_username);
            if (Membership.GetUser(_username) == null)
            {
                LabelDisplay.Text = string.Format("{0} was deleted Successfully", _username);
                LabelDisplay.ForeColor = Color.CornflowerBlue;
                var userToDelete = UserDB.GetUsersByUsername(_username);
                if (userToDelete != null)
                {
                    if (UserDB.DeleteUserById(userToDelete.Id) > 0)
                    {
                        LabelDisplay.Text =
                            string.Format("{0} and all permissions associated to the user was deleted Successfully",
                                _username);
                    }
                }
            }
            else
            {
                LabelDisplay.Text = string.Format("{0} could not be deleted", _username);
                LabelDisplay.ForeColor = Color.Red;
            }
            GridViewUsers.EditIndex = -1;
            BuildGridView();
        }

        protected void GridViewUsers_OnRowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int index = GridViewUsers.EditIndex;
            GridViewRow gvrow = GridViewUsers.Rows[index];
            _isonline = ((CheckBox) gvrow.Cells[0].Controls[0]).Checked;
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
                LabelDisplay.ForeColor = Color.CornflowerBlue;
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
                        Roles.AddUserToRole(username, "Members");
                        // Copy the user to the second users table
                        if (!String.IsNullOrWhiteSpace(username))
                        {
                            UserDB.AddUser(new users() {Username = username});
                        }

                        LabelDisplay.Text = string.Format("{0} was created Successfully", username);
                        LabelDisplay.ForeColor = Color.CornflowerBlue;

                        // Clear the TextBoxes
                        txtUserName.Text = string.Empty;
                        txtEmail.Text = string.Empty;
                        // Password fields will probably empty them self, but just in case they dont
                        txtPassword.Text = string.Empty;
                        txtConfirmPassword.Text = string.Empty;
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
                LabelDisplay.Text = string.Format("The passwords are not the same");
                LabelDisplay.ForeColor = Color.Red;
            }

            BuildGridView();
        }


        private void ChangePassword()
        {
            string username = TxtUserNameChangePassword.Text.Trim();
            string password = TxtNewPassword.Text;
            string confirmPassword = TxtNewPasswordConfirm.Text;

            if (string.IsNullOrWhiteSpace(username))
            {
                LabelDisplay.Text = string.Format("No username was specified.");
                LabelDisplay.ForeColor = Color.Red;
                return;
            }

             MembershipUser user = Membership.GetUser(username);

            if (user == null)
            {
                LabelDisplay.Text = string.Format("The user {0} does not exist in the system.", username);
                LabelDisplay.ForeColor = Color.Red;
                return;
            }

            if (password != confirmPassword)
            {
                LabelDisplay.Text = string.Format("The passwords are not the same.");
                LabelDisplay.ForeColor = Color.Red;
                return;
            }
           
            if (password.Length < Membership.MinRequiredPasswordLength)
            {
                LabelDisplay.Text = string.Format("The password is to short or not vaild.");
                LabelDisplay.ForeColor = Color.Red;
                return;
            }

            if (user.ChangePassword(user.ResetPassword(), password)) 
            {
                LabelDisplay.Text = string.Format("The password for user {0} has been changed!", username);
                //LabelDisplay.Text = string.Format("The password for user {0} has been changed to <b>{1}</b>", username,
                //password);
                LabelDisplay.ForeColor = Color.CornflowerBlue;

                // Clear the TextBoxes
                TxtUserNameChangePassword.Text = string.Empty;
                // Password fields will probably empty them self, but just in case they dont
                TxtNewPassword.Text = string.Empty;
                TxtNewPasswordConfirm.Text = string.Empty;
            }
            else
            {
                LabelDisplay.Text =
                    string.Format(
                        "The password for user {0} was not changed! Weird... (o.O;)<br/>Try removing and recreate the user.",
                        username);
                LabelDisplay.ForeColor = Color.Red;
            }
            
        }


        protected void BtnChangePassword_OnClick(object sender, EventArgs e)
        {
            ChangePassword();
        }


        private void ResetPassword(string email)
        {
            var usersByEmail = Membership.FindUsersByEmail(email.Trim());

            if (usersByEmail.Count == 0)
            {
                LabelDisplay.Text = string.Format("There is no user with that email.");
                LabelDisplay.ForeColor = Color.Red;
                return;
            }

            if (usersByEmail.Count > 1)
            {
                LabelDisplay.Text =
                    string.Format("There is multiple users with that email. <br/> Change password by username instead.");
                LabelDisplay.ForeColor = Color.Red;
                return;
            }

            string userName = Membership.GetUserNameByEmail(email.Trim());

            if (string.IsNullOrWhiteSpace(userName))
            {
                LabelDisplay.Text = string.Format("There is no user with that email.");
                LabelDisplay.ForeColor = Color.Red;
                return;
            }

            MembershipUser user = Membership.GetUser(userName);

            if (user == null)
            {
                LabelDisplay.Text = string.Format("The user did not exist.");
                LabelDisplay.ForeColor = Color.Red;
                return;
            }

            string newstr = user.ResetPassword();

            if (string.IsNullOrWhiteSpace(newstr))
            {
                LabelDisplay.Text =
                    string.Format(
                        "NO new password for user {0} was created! Weird... (o.O;)<br/>Try removing and recreate the user.",
                        userName);
                return;
            }
            LabelDisplay.Text = string.Format("The password for user {0} has been changed to <b>{1}</b>", userName,
                newstr);
            LabelDisplay.ForeColor = Color.CornflowerBlue;

            // Clear the textbox
            TxtEmailReset.Text = string.Empty;
        }

        protected void BtnReset_OnClick(object sender, EventArgs e)
        {
            ResetPassword(TxtEmailReset.Text);
        }
    }
}