using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Providers.Entities;
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
                if (SetWebPageIdAndType())
                {
                    BindUsersToUserList();
                    BindRolesToRoleList();
                    BindPermissionsToPermissionUserList();
                }
                else
                {
                    ActionStatus.Text = "The Id or Type could not be set. Try refreshing the page.";
                }

            }
        }

        private bool SetWebPageIdAndType()
        {
            bool IsIdAndTypeSet = false;
            // Gets the ID from the QueryString
            var stId = Request.QueryString["Id"];
            int wPId;
            // If the ID from the QueryString is in a valid format its stored
            if (!string.IsNullOrWhiteSpace(stId) && int.TryParse(stId, out wPId))
            {
                HiddenFieldWebPageId.Value = wPId.ToString();
                IsIdAndTypeSet = true;
                webpages webPage = WebPageDB.GetWebPageById(wPId);
                if (webPage != null)
                {
                    if (webPage.AssociationId != null)
                    {
                        HiddenFieldWebPageType.Value = "A";
                        HiddenFieldAssociationId.Value = webPage.AssociationId.ToString();
                        IsIdAndTypeSet = true;
                    }
                    else if (webPage.CommunityId != null)
                    {
                        HiddenFieldWebPageType.Value = "C";
                        HiddenFieldCommunityId.Value = webPage.CommunityId.ToString();
                        IsIdAndTypeSet = true;
                    }
                }
            }
            return IsIdAndTypeSet;
        }

        private void BindUsersToUserList()
        {
            // Get all of the user accounts 
            MembershipUserCollection users = Membership.GetAllUsers();
            UserList.DataSource = users;
            UserList.DataBind();
        }

        private void BindRolesToRoleList()
        {
            RoleList.DataSource = new object[] {"Contributors", "Administrators", "Members"};
            RoleList.DataBind();
        }

        // This method is not used!!!
        private void BindUsersToPermissionUserList()
        {
            var usersWithPermissionForAssociation = new List<string>();
            int currentAssoId;

            // Get the currentAssociation
            if (int.TryParse(HiddenFieldAssociationId.Value, out currentAssoId))
            {
                var usersWithPermissionsToCurrentAssociation = UserDB.GetAllUsersByAssociation(AssociationDB.GetAssociationById(currentAssoId));

                foreach (var user in usersWithPermissionsToCurrentAssociation)
                {
                    if (user.association_permissions.Any(p => !p.IsDeleted && p.associations_Id == currentAssoId))
                    {
                        usersWithPermissionForAssociation.Add(user.Username);
                    }
                }
            }

            // Get the list of usernames that belong to the role 
            //string[] usersWithPermissionForAssociation = AssociationPermissionsDB.GetAllAssociationPermissionsByAssociation();

            // Bind the list of users to the GridView 
            PermissionUserList.DataSource = usersWithPermissionForAssociation;
            PermissionUserList.DataBind();
        }

        private void BindPermissionsToPermissionUserList()
        {
            //// Clear items in gridview before refreshing the items
            //PermissionUserList.DataSource = null;
            //PermissionUserList.DataBind();

            var permissionsForAssociation = new List<association_permissions>();
            int currentAssoId;

            // Get the currentAssociationId
            if (int.TryParse(HiddenFieldAssociationId.Value, out currentAssoId))
            {
                var usersWithPermissionsToCurrentAssociation = UserDB.GetAllUsersByAssociation(AssociationDB.GetAssociationById(currentAssoId));

                foreach (var user in usersWithPermissionsToCurrentAssociation)
                {
                    foreach (
                        var associationPermission in
                            user.association_permissions.Where(p => !p.IsDeleted && p.associations_Id == currentAssoId))
                    {
                        permissionsForAssociation.Add(associationPermission);
                    }

                }

                // Bind the list of Permissions to the GridView 
                PermissionUserList.DataSource = permissionsForAssociation;
                PermissionUserList.DataBind();
            }


            var permissionsForCommunity = new List<community_permissions>();
            int currentCommId;

            // Get the currentCommunityId
            if (int.TryParse(HiddenFieldCommunityId.Value, out currentCommId))
            {
                var usersWithPermissionsToCurrentCommunity = UserDB.GetAllUsersByCommunityId(CommunityDB.GetCommunityById(currentCommId));

                foreach (var user in usersWithPermissionsToCurrentCommunity)
                {
                    foreach (
                        var communityPermission in
                            user.community_permissions.Where(p => !p.IsDeleted && p.communities_Id == currentCommId))
                    {
                        permissionsForCommunity.Add(communityPermission);
                    }

                }

                // Bind the list of Permissions to the GridView 
                PermissionUserList.DataSource = permissionsForCommunity;
                PermissionUserList.DataBind();
            }
        }

        protected void BtnAddUser_OnClick(object sender, EventArgs e)
        {
            int assoId;
            int commId;
            int.TryParse(HiddenFieldAssociationId.Value, out assoId);
            int.TryParse(HiddenFieldCommunityId.Value, out commId);
            var currentAssociation = AssociationDB.GetAssociationById(assoId);
            var currentCommunity = CommunityDB.GetCommunityById(commId);
            var selectedUser = UserDB.GetUserByUsername(UserList.SelectedValue);
            var selectedRole = RoleList.SelectedValue;

            if (selectedUser == null)
            {
                ActionStatus.Text = "Selected user does not exist!";
                return;
            }
            if (Membership.GetUser(selectedUser.Username) == null)
            {
                ActionStatus.Text = "Selected user does not exist in the membership database!";
                return;
            }
            if (String.IsNullOrWhiteSpace(selectedRole))
            {
                ActionStatus.Text = "Selected role value is empty!";
                return;
            }
            if(!Roles.RoleExists(selectedRole))
            {
                ActionStatus.Text = "Selected role does not exist!";
                return;
            }

            if (currentAssociation != null && HiddenFieldWebPageType.Value == "A")
            {
                var newAssoPermission = new association_permissions
                {
                    associations = currentAssociation,
                    associations_Id = currentAssociation.Id,
                    users = selectedUser,
                    users_Id = selectedUser.Id,
                    Role = selectedRole
                };

                if (!Roles.IsUserInRole(selectedUser.Username, selectedRole))
                {
                    Roles.AddUserToRole(selectedUser.Username, selectedRole);
                }

                if (
                    !AssociationPermissionsDB.HasUserPermissionForAssociationWithRole(selectedUser,
                        currentAssociation,
                        selectedRole))
                {
                    if (AssociationPermissionsDB.AddAssociationPermissions(newAssoPermission))
                    {

                        ActionStatus.Text = "New permission for " + currentAssociation.Name +
                                            " was successfully added!";
                    }
                    else
                    {
                        ActionStatus.Text = "New permission for " + currentAssociation.Name + " could not be added!";
                    }
                }
                BindPermissionsToPermissionUserList();
                return;
            }

            if (currentCommunity != null && HiddenFieldWebPageType.Value == "C")
            {
                var newCommPermission = new community_permissions
                {
                    communities = currentCommunity,
                    communities_Id = currentCommunity.Id,
                    users = selectedUser,
                    users_Id = selectedUser.Id,
                    Role = selectedRole

                };

                if (!Roles.IsUserInRole(selectedUser.Username, selectedRole))
                {
                    Roles.AddUserToRole(selectedUser.Username, selectedRole);
                }

                if (
                    !CommunityPermissionsDB.HasUserPermissionForCommunityWithRole(selectedUser,
                        currentCommunity,
                        selectedRole))
                {
                    if (CommunityPermissionsDB.AddCommunityPermissions(newCommPermission))
                    {
                        ActionStatus.Text = "New permission for " + currentCommunity.Name +
                                            " was successfully added!";
                    }
                    else
                    {
                        ActionStatus.Text = "New permission for " + currentCommunity.Name + " could not be added!";
                    }
                }
                BindPermissionsToPermissionUserList();
                return;
            }







        }

        protected void PermissionUserList_OnRowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            // Reference the UserNameLabel 
            Label IdLabel = PermissionUserList.Rows[e.RowIndex].FindControl("IdLabel") as Label;
            Label UserNameLabel = PermissionUserList.Rows[e.RowIndex].FindControl("UserNameLabel") as Label;
            Label RoleLabel = PermissionUserList.Rows[e.RowIndex].FindControl("RoleLabel") as Label;

            if (IdLabel == null)
            {
                ActionStatus.Text = "Permission was not removed! Error: LabelId is null";
                return;
            }
            if (UserNameLabel == null)
            {
                ActionStatus.Text = "Permission was not removed! Error: UserNameLabel is null";
                return;
            }
            if (RoleLabel == null)
            {
                ActionStatus.Text = "Permission was not removed! Error: RoleLabel is null";
                return;
            }
            
            int permissionId;
            if (!int.TryParse(IdLabel.Text, out permissionId))
            {
                ActionStatus.Text = "Permission was not removed! Error: PermissionId could not be translated into a number.";
                    return;
            }
            
            users user;
            if (UserDB.GetUserByUsername(UserNameLabel.Text) == null)
            {
                ActionStatus.Text = "Permission was not removed! Error: User by that username could not be found.";
                    return;
            }
            string role;
            if (string.IsNullOrWhiteSpace(RoleLabel.Text))
            {
                ActionStatus.Text = "Permission was not removed! Error: Role is null or empty.";
                return;
            }

            user = UserDB.GetUserByUsername(UserNameLabel.Text);
            role = RoleLabel.Text;

            if (!string.IsNullOrWhiteSpace(HiddenFieldAssociationId.Value))
            {
                if (AssociationPermissionsDB.GetAssociationPermissionsById(permissionId) == null)
                {
                    ActionStatus.Text = "The permission was not found!";
                    return;
                }

                if (AssociationPermissionsDB.DeleteAssociationPermissionsById(permissionId) > 0)
                {
                    ActionStatus.Text = string.Format("{0}'s permission for {1} was successfully removed!",
                        user.Username, AssociationDB.GetAssociationById(int.Parse(HiddenFieldAssociationId.Value)).Name);

                    if (!AssociationPermissionsDB.HasUserPermissionWithRole(user, role))
                    {
                        Roles.RemoveUserFromRole(user.Username, role);
                        ActionStatus.Text += string.Format("<br/>The Role {0} was completely removed from {1}.", role,
                            user.Username);
                    }
                }
                BindPermissionsToPermissionUserList();
                return;
            }

            if (!string.IsNullOrWhiteSpace(HiddenFieldCommunityId.Value))
            {
                if (CommunityPermissionsDB.GetCommunityPermissionsById(permissionId) == null)
                {
                    ActionStatus.Text = "The permission was not found!";
                    return;
                }

                if (CommunityPermissionsDB.DeleteCommunityPermissionsById(permissionId) > 0)
                {
                    ActionStatus.Text = string.Format("{0}'s permission for {1} was successfully removed!", user.Username, AssociationDB.GetAssociationById(int.Parse(HiddenFieldAssociationId.Value)));

                    if (!CommunityPermissionsDB.HasUserPermissionWithRole(user, role))
                    {
                        Roles.RemoveUserFromRole(user.Username, role);
                        ActionStatus.Text += string.Format("<br/>The Role {0} was completely removed from {1}.", role, user.Username);
                    }
                }
                BindPermissionsToPermissionUserList();
                return;
            }

            ActionStatus.Text = "Permission was not removed! Refresh page and try again.";
        }
    }
}