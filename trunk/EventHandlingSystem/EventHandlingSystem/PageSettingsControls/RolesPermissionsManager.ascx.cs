using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using EventHandlingSystem.Database;

namespace EventHandlingSystem.PageSettingsControls
{
    public partial class RolesPermissionsManager : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindUsersToUserList();
                BindUsersWithPermissionToAssosciation();
            }
        }

        private void SetWebPageIdAndType()
        {
            // Gets the ID from the QueryString
            var stId = Request.QueryString["Id"];
            int wPId;
            // If the ID from the QueryString is in a valid format its stored
            if (!string.IsNullOrWhiteSpace(stId) && int.TryParse(stId, out wPId))
            {
                HiddenFieldWebPageId.Value = wPId.ToString();

                webpages webPage = WebPageDB.GetWebPageById(wPId);
                if (webPage != null)
                {
                    if (webPage.AssociationId != null)
                    {
                        HiddenFieldWebPageType.Value = "A";
                    }
                    else if (webPage.CommunityId != null)
                    {
                        HiddenFieldWebPageType.Value = "C";
                    }
                }
            }
        }

        private void BindUsersToUserList()
        {
            // Get all of the user accounts 
            MembershipUserCollection users = Membership.GetAllUsers();
            UserList.DataSource = users;
            UserList.DataBind();
        }

        private void BindUsersWithPermissionToAssosciation()
        {
        //    // Get the currentAssociation
        //    string a = RoleList.SelectedValue;
        //    if(UserDB.GetAllUsersByAssociationId())
        //    // Get the list of usernames that belong to the role 
        //    string[] usersBelongingToRole = Roles.GetUsersInRole(selectedRoleName);

        //    // Bind the list of users to the GridView 
        //    RolesUserList.DataSource = usersBelongingToRole;
        //    RolesUserList.DataBind();
        }
       

        protected void BtnAddUser_OnClick(object sender, EventArgs e)
        {
                
        }

        protected void PermissionUserList_OnRowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            //// Get the selected role 
            //string selectedRoleName = RoleList.SelectedValue;

            //// Reference the UserNameLabel 
            //Label UserNameLabel = RolesUserList.Rows[e.RowIndex].FindControl("UserNameLabelInRole") as Label;

            //// Remove the user from the role 
            //Roles.RemoveUserFromRole(UserNameLabel.Text, selectedRoleName);

            //// Refresh the GridView 
            //DisplayUsersBelongingToRole();

            //// Display a status message 
            //ActionStatus.Text = string.Format("User {0} was removed from role {1}.", UserNameLabel.Text,
            //    selectedRoleName);

            //// Refresh the "by user" interface 
            //CheckRolesForSelectedUser();
        }
    }
}