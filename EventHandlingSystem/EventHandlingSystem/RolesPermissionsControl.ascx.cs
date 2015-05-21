using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using EventHandlingSystem.Database;
using WebGrease.Css.Extensions;

namespace EventHandlingSystem
{
    public partial class RolesPermissionsControl : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ActionStatus.Text = "";
            ActionStatusPermissions1.Text = "";
            ActionStatusPermissions2.Text = "";

            if (!IsPostBack)
            {
                // Bind the users and roles 
                BindUsersToUserList1();
                BindRolesToList();

                // Check the selected user's roles 
                CheckRolesForSelectedUser();

                // Display those users belonging to the currently selected role 
                DisplayUsersBelongingToRole();



                // Bind users and associations
                // Bindings for Manage Users By Association
                BindAssociationsToList();
                BindUsersToUserToAddList1();
                BindRolesToRoleToAddList1();

                // Bindings for Manage Association Permissions By User
                BindAssociationsToListBox();
                BindUsersToUserList2();
                BindRolesToRoleList2();


                // Display those users belonging to the currently selected association 
                BindPermissionsToAssociationUserList();

                // Add the selected user's associations in the SelectedAssociationsListBox
                SelectAssociationsForSelectedUserAndRole();


                // Bind users and communities
                // Bindings for Manage Users By Community
                // Bind communities to dropdown
                BindCommunitiesToCommunityList();
                BindUsersToUserToAddList2();
                BindRolesToRoleToAddList2();

                // Bindings for Manage Community Permissions By User
                // Bind users to dropdown for community permission manangement
                BindUsersToUserList3();
                // Bind roles to dropdown for community permission manangement
                BindRolesToRoleList3();
                // Bind all communities to the listbox
                BindCommuntiesToListBox();

                // Display those users permissions to the currently selected community 
                BindPermissionsToCommunityUserList();

                // Add the selected user's communities in the SelectedCommunitiesListBox
                SelectCommunitiesForSelectedUserAndRole();
                
            }
        }

        #region RoleManager

        private void BindUsersToUserList1()
        {
            // Get all of the user accounts 
            var users = Membership.GetAllUsers().Cast<MembershipUser>().OrderBy(user => user.UserName).ToList();
            UserList1.DataSource = users;
            UserList1.DataBind();
        }

        private void BindRolesToList()
        {
            // Get all of the roles 

            string[] roles = Roles.GetAllRoles();
            UsersRoleList.DataSource = roles;
            UsersRoleList.DataBind();

            RoleList.DataSource = roles;
            RoleList.DataBind();
        }

        private void CheckRolesForSelectedUser()
        {
            // Get the selected user
            string selectedUserName = UserList1.SelectedValue;
            
            // Make sure the user exists
            if (Membership.GetUser(selectedUserName) == null)
            {
                ActionStatusPermissions1.Text = string.Format("The user {0} does not exist in the system.", selectedUserName);

                // Refresh the UserList because it was probably not updated
                BindUsersToUserList1();
                return;
            }

            // Determine what roles the selected user belongs to 
            string[] selectedUsersRoles = Roles.GetRolesForUser(selectedUserName);

            // Loop through the Repeater's Items and check or uncheck the checkbox as needed
            foreach (RepeaterItem ri in UsersRoleList.Items)
            {
                // Programmatically reference the CheckBox 
                CheckBox RoleCheckBox = ri.FindControl("RoleCheckBox") as CheckBox;
                // See if RoleCheckBox.Text is in selectedUsersRoles 
                if (selectedUsersRoles.Contains<string>(RoleCheckBox.Text))
                    RoleCheckBox.Checked = true;
                else
                    RoleCheckBox.Checked = false;
            }
        }

        protected void UserList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            CheckRolesForSelectedUser();
        }

        protected void RoleCheckBox_CheckChanged(object sender, EventArgs e)
        {
            // Reference the CheckBox that raised this event 
            CheckBox RoleCheckBox = sender as CheckBox;

            // Get the currently selected user and role 
            string selectedUserName = UserList1.SelectedValue;

            string roleName = RoleCheckBox.Text;

            // Determine if we need to add or remove the user from this role 
            if (RoleCheckBox.Checked)
            {
                // Add the user to the role 
                Roles.AddUserToRole(selectedUserName, roleName);
                // Display a status message 
                ActionStatus.Text = string.Format("User {0} was added to role {1}.", selectedUserName, roleName);
            }
            else
            {
                // Remove the user from the role 
                Roles.RemoveUserFromRole(selectedUserName, roleName);
                // Display a status message 
                ActionStatus.Text = string.Format("User {0} was removed from role {1}.", selectedUserName, roleName);

            }

            // Refresh the "by role" interface 
            DisplayUsersBelongingToRole();
        }

        private void DisplayUsersBelongingToRole()
        {
            // Get the selected role 
            string selectedRoleName = RoleList.SelectedValue;

            // Get the list of usernames that belong to the role 
            string[] usersBelongingToRole = Roles.GetUsersInRole(selectedRoleName);

            // Bind the list of users to the GridView 
            RolesUserList.DataSource = usersBelongingToRole;
            RolesUserList.DataBind();
        }

        protected void RoleList_SelectedIndexChanged(object sender, EventArgs e)
        {
            DisplayUsersBelongingToRole();
        }

        protected void RolesUserList_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            // Get the selected role 
            string selectedRoleName = RoleList.SelectedValue;

            // Reference the UserNameLabel 
            Label UserNameLabel = RolesUserList.Rows[e.RowIndex].FindControl("UserNameLabelInRole") as Label;

            // Remove the user from the role 
            Roles.RemoveUserFromRole(UserNameLabel.Text, selectedRoleName);

            // Refresh the GridView 
            DisplayUsersBelongingToRole();

            // Display a status message 
            ActionStatus.Text = string.Format("User {0} was removed from role {1}.", UserNameLabel.Text,
                selectedRoleName);

            // Refresh the "by user" interface 
            CheckRolesForSelectedUser();
        }

        protected void AddUserToRoleButton_Click(object sender, EventArgs e)
        {
            // Get the selected role and username 

            string selectedRoleName = RoleList.SelectedValue;
            string userNameToAddToRole = UserNameToAddToRole.Text;

            // Make sure that a value was entered 
            if (userNameToAddToRole.Trim().Length == 0)
            {
                ActionStatus.Text = "You must enter a username in the textbox.";
                return;
            }

            // Make sure that the user exists in the system 
            MembershipUser userInfo = Membership.GetUser(userNameToAddToRole);
            if (userInfo == null)
            {
                ActionStatus.Text = string.Format("The user {0} does not exist in the system.", userNameToAddToRole);
                return;
            }

            // Make sure that the user doesn't already belong to this role 
            if (Roles.IsUserInRole(userNameToAddToRole, selectedRoleName))
            {
                ActionStatus.Text = string.Format("User {0} already is a member of role {1}.", userNameToAddToRole,
                    selectedRoleName);
                return;
            }

            // If we reach here, we need to add the user to the role 
            Roles.AddUserToRole(userNameToAddToRole, selectedRoleName);

            // Clear out the TextBox 
            UserNameToAddToRole.Text = string.Empty;

            // Refresh the GridView 
            DisplayUsersBelongingToRole();

            // Display a status message 

            ActionStatus.Text = string.Format("User {0} was added to role {1}.", userNameToAddToRole, selectedRoleName);

            // Refresh the "by user" interface 
            CheckRolesForSelectedUser();
        }

        #endregion 


        #region AssociationPermissionManager

        #region BindData with IsNotPostBack
        private void BindUsersToUserList2()
        {
            // Get all of the user accounts 
            var users = Membership.GetAllUsers().Cast<MembershipUser>().OrderBy(user => user.UserName).ToList();
            UserList2.DataSource = users;
            UserList2.DataBind();
        }

        private void BindRolesToRoleList2()
        {
            RoleList2.DataSource = new object[] { "Contributors", "Administrators" };
            RoleList2.DataBind();
        }

        private void BindAssociationsToListBox()
        {
            AssociationsListBox.DataSource = AssociationDB.GetAllAssociations().OrderBy(a => a.Name);
            AssociationsListBox.DataTextField = "Name";
            AssociationsListBox.DataValueField = "Id";
            AssociationsListBox.DataBind();
        }

        private void BindAssociationsToList()
        {
            AssociationList.DataSource = AssociationDB.GetAllAssociations().OrderBy(a => a.Name);
            AssociationList.DataTextField = "Name";
            AssociationList.DataValueField = "Id";
            AssociationList.DataBind();
        }

        private void BindUsersToUserToAddList1()
        {
            // Get all of the user accounts 
            var users = Membership.GetAllUsers().Cast<MembershipUser>().OrderBy(user => user.UserName).ToList();
            UserToAddList1.DataSource = users;
            UserToAddList1.DataBind();
        }

        private void BindRolesToRoleToAddList1()
        {
            RoleToAddList1.DataSource = new object[] { "Contributors", "Administrators" };
            RoleToAddList1.DataBind();
        }

        #endregion


        private void BindPermissionsToAssociationUserList()
        {
            var permissionsForAssociation = new List<association_permissions>();
            int currentAssoId;

            // Get the currentAssociationId
            if (int.TryParse(AssociationList.SelectedValue, out currentAssoId))
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
                AssociationUserList.DataSource = permissionsForAssociation.OrderBy(p => p.Role).ThenBy(p => p.users.Username);
                AssociationUserList.DataBind();
            }
        }

        protected void AssociationList_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            BindPermissionsToAssociationUserList();
        }

        protected void AssociationUserList_OnRowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            // Reference the UserNameLabel 
            Label IdLabel = AssociationUserList.Rows[e.RowIndex].FindControl("IdLabel") as Label;
            Label UserNameLabel = AssociationUserList.Rows[e.RowIndex].FindControl("UserNameLabel") as Label;
            Label RoleLabel = AssociationUserList.Rows[e.RowIndex].FindControl("RoleLabel") as Label;

            if (IdLabel == null)
            {
                ActionStatusPermissions1.Text = "Permission was not removed! Error: LabelId is null";
                return;
            }
            if (UserNameLabel == null)
            {
                ActionStatusPermissions1.Text = "Permission was not removed! Error: UserNameLabel is null";
                return;
            }
            if (RoleLabel == null)
            {
                ActionStatusPermissions1.Text = "Permission was not removed! Error: RoleLabel is null";
                return;
            }

            int permissionId;
            if (!int.TryParse(IdLabel.Text, out permissionId))
            {
                ActionStatusPermissions1.Text = "Permission was not removed! Error: PermissionId could not be translated into a number.";
                return;
            }
            if (UserDB.GetUserByUsername(UserNameLabel.Text) == null)
            {
                ActionStatusPermissions1.Text = "Permission was not removed! Error: User by that username could not be found.";
                return;
            }
            if (string.IsNullOrWhiteSpace(RoleLabel.Text))
            {
                ActionStatusPermissions1.Text = "Permission was not removed! Error: Role is null or empty.";
                return;
            }

            users user = UserDB.GetUserByUsername(UserNameLabel.Text);
            string role = RoleLabel.Text;


            int assoId;
            if (!string.IsNullOrWhiteSpace(AssociationList.SelectedValue) && int.TryParse(AssociationList.SelectedValue, out assoId))
            {
                if (AssociationPermissionsDB.GetAssociationPermissionsById(permissionId) == null)
                {
                    ActionStatusPermissions1.Text = "The permission was not found!";
                    return;
                }

                if (AssociationPermissionsDB.DeleteAssociationPermissionsById(permissionId) > 0)
                {
                    ActionStatusPermissions1.Text = string.Format("{0}'s permission for {1} was successfully removed!", user.Username, AssociationDB.GetAssociationById(assoId).Name);

                    if (!CommunityPermissionsDB.HasUserPermissionWithRole(user, role) && !AssociationPermissionsDB.HasUserPermissionWithRole(user, role))
                    {
                        Roles.RemoveUserFromRole(user.Username, role);
                        ActionStatusPermissions1.Text += string.Format("<br/>The Role {0} was completely removed from {1}.", role, user.Username);
                    }
                }

                // Refresh the GridView 
                BindPermissionsToAssociationUserList();

                // Refresh the "by user" interface 
                SelectAssociationsForSelectedUserAndRole();
                return;
            }

            ActionStatusPermissions1.Text = "Permission was not removed! Refresh page and try again.";
        }

        protected void AddUserToAssociation_OnClick(object sender, EventArgs e)
        {
            int assoId;
            int.TryParse(AssociationList.SelectedValue, out assoId);
            var currentAssociation = AssociationDB.GetAssociationById(assoId);
            var selectedUser = UserDB.GetUserByUsername(UserToAddList1.SelectedValue);
            var selectedRole = RoleToAddList1.SelectedValue;

            if (selectedUser == null)
            {
                ActionStatusPermissions1.Text = "Selected user does not exist!";
                return;
            }
            if (Membership.GetUser(selectedUser.Username) == null)
            {
                ActionStatusPermissions1.Text = "Selected user does not exist in the membership database!";
                return;
            }
            if (String.IsNullOrWhiteSpace(selectedRole))
            {
                ActionStatusPermissions1.Text = "Selected role value is empty!";
                return;
            }
            if (!Roles.RoleExists(selectedRole))
            {
                ActionStatusPermissions1.Text = "Selected role does not exist!";
                return;
            }

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
                    ActionStatusPermissions1.Text = "New permission for " + currentAssociation.Name +
                                        " was successfully added!";
                }
                else
                {
                    ActionStatusPermissions1.Text = "New permission for " + currentAssociation.Name + " could not be added!";
                }
            }

            // Refresh the GridView
            BindPermissionsToAssociationUserList();

            // Refresh the "by user" interface
            SelectAssociationsForSelectedUserAndRole();
        }

        private void SelectAssociationsForSelectedUserAndRole()
        {
            // Reset the ListBoxes
            BindAssociationsToListBox();

            // Get the selected user
            string selectedUserName = UserList2.SelectedValue;

            string selectedRole = RoleList2.SelectedValue;

            if (UserDB.GetUserByUsername(selectedUserName) == null)
            {
                ActionStatusPermissions1.Text = "Selected user does not exist! Try refreshing the page.";
                return;
            }

            if (String.IsNullOrWhiteSpace(selectedRole))
            {
                ActionStatusPermissions1.Text = "No role was selected! Try refreshing the page.";
                return;
            }

            if (!Roles.RoleExists(selectedRole))
            {
                ActionStatusPermissions1.Text = "Selected role does not exist!";
                return;
            }

            var selectedUsersAssociationsList = new List<associations>();
            foreach (var asso in (from permission in UserDB.GetUserByUsername(selectedUserName).association_permissions
                                  where
                                      !permission.associations.IsDeleted && !permission.IsDeleted && permission.Role.Equals(selectedRole)
                                  select permission.associations).Where(a => !selectedUsersAssociationsList.Contains(a)))
            {
                selectedUsersAssociationsList.Add(asso);
            }

            associations[] selectedUsersAssociations = selectedUsersAssociationsList.ToArray();


            var selectedUsersAssociationsNamesList = new List<string>();
            foreach (
                var AssoName in
                    (from association in selectedUsersAssociations where !association.IsDeleted select association.Name)
                        .Where(name => !selectedUsersAssociationsNamesList.Contains(name)))
            {
                selectedUsersAssociationsNamesList.Add(AssoName);
            }

            string[] selectedUsersAssociationsNames = selectedUsersAssociationsNamesList.ToArray();

            SelectedAssociationsListBox.DataSource = selectedUsersAssociations.OrderBy(a => a.Name);
            SelectedAssociationsListBox.DataTextField = "Name";
            SelectedAssociationsListBox.DataValueField = "Id";
            SelectedAssociationsListBox.DataBind();

            var itemsToRemove = new List<ListItem>();

            // Loop through the ListBox's Items and Find items to Remove/Add 
            foreach (ListItem li in AssociationsListBox.Items)
            {
                if (selectedUsersAssociationsNames.Any(assoName => li.Text.Equals(assoName)))
                {
                    itemsToRemove.Add(li);
                    //SelectedAssociationsListBox.Items.Add(li);
                }
            }

            foreach (var listItem in itemsToRemove)
            {
                AssociationsListBox.Items.Remove(listItem);
            }
        }


        protected void UserList2_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            SelectAssociationsForSelectedUserAndRole();
        }

        protected void RoleList2_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            SelectAssociationsForSelectedUserAndRole();
        }

        private bool UpdateUsersAssociationsPermissions(IEnumerable<ListItem> items, bool isAdding)
        {
            // Get the selected user
            string selectedUserName = UserList2.SelectedValue;

            // Get the selected role
            string selectedRole = RoleList2.SelectedValue;

            users user = UserDB.GetUserByUsername(selectedUserName);

            // Make sure the user exists
            if (user == null)
            {
                ActionStatusPermissions1.Text = string.Format("The user {0} does not exist in the system.", selectedUserName);

                // Refresh the UserList because it was probably not updated
                BindUsersToUserList2();
                return false;
            }
            if (!Roles.RoleExists(selectedRole))
            {
                ActionStatusPermissions1.Text = "Selected role does not exist!";
                return false;
            }


            // Get Communities by items in SelectedCommunitiesListbox
            var associationsByListItems = new List<associations>();
            foreach (ListItem item in items)
            {
                int id;
                if (int.TryParse(item.Value, out id))
                {
                    associationsByListItems.Add(AssociationDB.GetAssociationById(id));
                }
            }


            int permissionsRowsChanged = 0;

            if (isAdding)
            {

                foreach (var asso in associationsByListItems)
                {
                    if (!AssociationPermissionsDB.HasUserPermissionForAssociationWithRole(user, asso, selectedRole))
                    {
                        permissionsRowsChanged +=
                            (AssociationPermissionsDB.AddAssociationPermissions(new association_permissions
                            {
                                users_Id = user.Id,
                                associations_Id = asso.Id,
                                users = user,
                                associations = asso,
                                Role = selectedRole
                            }))
                                ? 1
                                : 0;
                    }
                }

            }
            else
            {
                foreach (var asso in associationsByListItems)
                {
                    if (AssociationPermissionsDB.HasUserPermissionForAssociationWithRole(user, asso, selectedRole))
                    {
                        association_permissions aP =
                            user.association_permissions.SingleOrDefault(
                                p => !p.IsDeleted && p.associations == asso && p.Role.Equals(selectedRole));
                        int permId = -1;
                        if (aP != null)
                        {
                            permId = aP.Id;
                        }

                        if (permId != -1)
                        {
                            permissionsRowsChanged += (AssociationPermissionsDB.DeleteAssociationPermissionsById(permId) > 0)
                            ? 1
                            : 0;
                        }

                    }
                }
            }

            if (permissionsRowsChanged > 0)
            {
                // Refresh the GridView 
                BindPermissionsToAssociationUserList();

                ActionStatusPermissions1.Text = string.Format("User {0}'s Permissions was Updated.", selectedUserName);
                return true;
            }
            else
            {
                ActionStatusPermissions1.Text = string.Format("User {0}'s Permissions Could Not be Updated!",
                    selectedUserName);
                return false;
            }

           
        }

        protected void AddAssociation_OnClick(object sender, EventArgs e)
        {
            int[] selectedIndices = AssociationsListBox.GetSelectedIndices();

            ListItem[] selectedItems = selectedIndices.Select(index => AssociationsListBox.Items[index]).ToArray();

            if (UpdateUsersAssociationsPermissions(selectedItems, true))
            {
                SelectedAssociationsListBox.Items.AddRange(selectedItems);

                foreach (var item in selectedItems)
                {
                    AssociationsListBox.Items.Remove(item);
                } 
            }
        }

        protected void RemoveAssociation_OnClick(object sender, EventArgs e)
        {
            int[] selectedIndices = SelectedAssociationsListBox.GetSelectedIndices();

            ListItem[] selectedItems =
                selectedIndices.Select(index => SelectedAssociationsListBox.Items[index]).ToArray();

            if (UpdateUsersAssociationsPermissions(selectedItems, false))
            {
                AssociationsListBox.Items.AddRange(selectedItems);

                foreach (var item in selectedItems)
                {
                    SelectedAssociationsListBox.Items.Remove(item);
                } 
            }
        }

        #endregion


        #region CommunityPermissionsManger

        private void BindCommunitiesToCommunityList()
        {
            CommunityList.DataSource = CommunityDB.GetAllCommunities().OrderBy(c => c.Name);
            CommunityList.DataTextField = "Name";
            CommunityList.DataValueField = "Id";
            CommunityList.DataBind();
            
        }

        private void BindPermissionsToCommunityUserList()
        {
           var permissionsForCommunity = new List<community_permissions>();
            int currentCommId;

            // Get the currentCommunityId
            if (int.TryParse(CommunityList.SelectedValue, out currentCommId))
            {
                var usersWithPermissionsToCurrentCommunity = UserDB.GetAllUsersByCommunity(CommunityDB.GetCommunityById(currentCommId));

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
                CommunityUserList.DataSource = permissionsForCommunity.OrderBy(p => p.Role).ThenBy(p => p.users.Username);
                CommunityUserList.DataBind();
            }
        }

        private void BindUsersToUserToAddList2()
        {
            // Get all of the user accounts 
            var users = Membership.GetAllUsers().Cast<MembershipUser>().OrderBy(user => user.UserName).ToList();
            UserToAddList2.DataSource = users;
            UserToAddList2.DataBind();
        }
        
        private void BindRolesToRoleToAddList2()
        {
            RoleToAddList2.DataSource = new object[] { "Contributors", "Administrators" };
            RoleToAddList2.DataBind();
        }

        private void BindCommuntiesToListBox()
        {
            CommunitiesListBox.DataSource = CommunityDB.GetAllCommunities().OrderBy(a => a.Name);
            CommunitiesListBox.DataTextField = "Name";
            CommunitiesListBox.DataValueField = "Id";
            CommunitiesListBox.DataBind();
        }


        protected void CommunityList_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            BindPermissionsToCommunityUserList();
            SelectCommunitiesForSelectedUserAndRole();
        }

        protected void CommunityUserList_OnRowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            // Reference the UserNameLabel 
            Label IdLabel = CommunityUserList.Rows[e.RowIndex].FindControl("IdLabel") as Label;
            Label UserNameLabel = CommunityUserList.Rows[e.RowIndex].FindControl("UserNameLabel") as Label;
            Label RoleLabel = CommunityUserList.Rows[e.RowIndex].FindControl("RoleLabel") as Label;

            if (IdLabel == null)
            {
                ActionStatusPermissions2.Text = "Permission was not removed! Error: LabelId is null";
                return;
            }
            if (UserNameLabel == null)
            {
                ActionStatusPermissions2.Text = "Permission was not removed! Error: UserNameLabel is null";
                return;
            }
            if (RoleLabel == null)
            {
                ActionStatusPermissions2.Text = "Permission was not removed! Error: RoleLabel is null";
                return;
            }

            int permissionId;
            if (!int.TryParse(IdLabel.Text, out permissionId))
            {
                ActionStatusPermissions2.Text = "Permission was not removed! Error: PermissionId could not be translated into a number.";
                return;
            }
            if (UserDB.GetUserByUsername(UserNameLabel.Text) == null)
            {
                ActionStatusPermissions2.Text = "Permission was not removed! Error: User by that username could not be found.";
                return;
            }
            if (string.IsNullOrWhiteSpace(RoleLabel.Text))
            {
                ActionStatusPermissions2.Text = "Permission was not removed! Error: Role is null or empty.";
                return;
            }

            users user = UserDB.GetUserByUsername(UserNameLabel.Text);
            string role = RoleLabel.Text;


            int commId;
            if (!string.IsNullOrWhiteSpace(CommunityList.SelectedValue) && int.TryParse(CommunityList.SelectedValue, out commId))
            {
                if (CommunityPermissionsDB.GetCommunityPermissionsById(permissionId) == null)
                {
                    ActionStatusPermissions2.Text = "The permission was not found!";
                    return;
                }

                if (CommunityPermissionsDB.DeleteCommunityPermissionsById(permissionId) > 0)
                {
                    ActionStatusPermissions2.Text = string.Format("{0}'s permission for {1} was successfully removed!", user.Username, CommunityDB.GetCommunityById(commId).Name);

                    if (!CommunityPermissionsDB.HasUserPermissionWithRole(user, role) && !AssociationPermissionsDB.HasUserPermissionWithRole(user,role))
                    {
                        Roles.RemoveUserFromRole(user.Username, role);
                        ActionStatusPermissions2.Text += string.Format("<br/>The Role {0} was completely removed from {1}.", role, user.Username);
                    }
                }
                
                // Refresh the gridview
                BindPermissionsToCommunityUserList();

                // Refresh the listboxes for managing Community Permissions By User
                SelectCommunitiesForSelectedUserAndRole();
                return;
            }

            ActionStatusPermissions2.Text = "Permission was not removed! Refresh page and try again.";
        }

        protected void AddUserToCommunity_OnClick(object sender, EventArgs e)
        {
            int commId;
            int.TryParse(CommunityList.SelectedValue, out commId);
            var currentCommunity = CommunityDB.GetCommunityById(commId);
            var selectedUser = UserDB.GetUserByUsername(UserToAddList2.SelectedValue);
            var selectedRole = RoleToAddList2.SelectedValue;

            if (selectedUser == null)
            {
                ActionStatusPermissions2.Text = "Selected user does not exist!";
                return;
            }
            if (Membership.GetUser(selectedUser.Username) == null)
            {
                ActionStatusPermissions2.Text = "Selected user does not exist in the membership database!";
                return;
            }
            if (String.IsNullOrWhiteSpace(selectedRole))
            {
                ActionStatusPermissions2.Text = "Selected role value is empty!";
                return;
            }
            if (!Roles.RoleExists(selectedRole))
            {
                ActionStatusPermissions2.Text = "Selected role does not exist!";
                return;
            }

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
                    ActionStatusPermissions2.Text = "New permission for " + currentCommunity.Name +
                                        " was successfully added!";
                }
                else
                {
                    ActionStatusPermissions2.Text = "New permission for " + currentCommunity.Name + " could not be added!";
                }
            }
            BindPermissionsToCommunityUserList();
            SelectCommunitiesForSelectedUserAndRole();
        }

        

        private void BindUsersToUserList3()
        {
            // Get all of the user accounts 
            var users = Membership.GetAllUsers().Cast<MembershipUser>().OrderBy(user => user.UserName).ToList();
            UserList3.DataSource = users;
            UserList3.DataBind();
        }

        private void BindRolesToRoleList3()
        {
            RoleList3.DataSource = new object[] { "Contributors", "Administrators" };
            RoleList3.DataBind();
        }


        private void SelectCommunitiesForSelectedUserAndRole()
        {
            // Reset the ListBoxes
            BindCommuntiesToListBox();

            // Get the selected user
            string selectedUserName = UserList3.SelectedValue;

            string selectedRole = RoleList3.SelectedValue;

            if (UserDB.GetUserByUsername(selectedUserName) == null)
            {
                ActionStatusPermissions2.Text = "Selected user does not exist! Try refreshing the page.";
                return;
            }

            if (String.IsNullOrWhiteSpace(selectedRole))
            {
                ActionStatusPermissions2.Text = "No role was selected! Try refreshing the page.";
                return;
            }

            if (!Roles.RoleExists(selectedRole))
            {
                ActionStatusPermissions2.Text = "Selected role does not exist!";
                return;
            }

            var selectedUsersCommunitiesList = new List<communities>();
            foreach (var comm in (from permission in UserDB.GetUserByUsername(selectedUserName).community_permissions
                where
                    !permission.communities.IsDeleted && !permission.IsDeleted && permission.Role.Equals(selectedRole)
                select permission.communities).Where(a => !selectedUsersCommunitiesList.Contains(a)))
            {
                selectedUsersCommunitiesList.Add(comm);
            }

            communities[] selectedUsersCommunities = selectedUsersCommunitiesList.ToArray();


            var selectedUsersCommunitiesNamesList = new List<string>();
            foreach (
                var commName in
                    (from community in selectedUsersCommunities where !community.IsDeleted select community.Name)
                        .Where(name => !selectedUsersCommunitiesNamesList.Contains(name)))
            {
                selectedUsersCommunitiesNamesList.Add(commName);
            }

            string[] selectedUsersAssociationsNames = selectedUsersCommunitiesNamesList.ToArray();

            SelectedCommunitiesListBox.DataSource = selectedUsersCommunities.OrderBy(a => a.Name);
            SelectedCommunitiesListBox.DataTextField = "Name";
            SelectedCommunitiesListBox.DataValueField = "Id";
            SelectedCommunitiesListBox.DataBind();

            var itemsToRemove = new List<ListItem>();

            // Loop through the ListBox's Items and Find items to Remove/Add 
            foreach (ListItem li in CommunitiesListBox.Items)
            {
                if (selectedUsersAssociationsNames.Any(assoName => li.Text.Equals(assoName)))
                {
                    itemsToRemove.Add(li);
                    //SelectedAssociationsListBox.Items.Add(li);
                }
            }

            foreach (var listItem in itemsToRemove)
            {
                CommunitiesListBox.Items.Remove(listItem);
            }

        }

        protected void UserList3_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            SelectCommunitiesForSelectedUserAndRole();
        }

        protected void RoleList3_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            SelectCommunitiesForSelectedUserAndRole();
        }


        private bool UpdateUsersCommunitiesPermissions(IEnumerable<ListItem> items, bool isAdding)
        {
            // Get the selected user
            string selectedUserName = UserList2.SelectedValue;

            // Get the selected role
            string selectedRole = RoleList3.SelectedValue;

            users user = UserDB.GetUserByUsername(selectedUserName);

            // Make sure the user exists
            if (user == null)
            {
                ActionStatusPermissions2.Text = string.Format("The user {0} does not exist in the system.", selectedUserName);

                // Refresh the UserList because it was probably not updated
                BindUsersToUserList3();
                return false;
            }
            if (!Roles.RoleExists(selectedRole))
            {
                ActionStatusPermissions2.Text = "Selected role does not exist!";
                return false;
            }


            // Get Communities by items in SelectedCommunitiesListbox
            var communitiesByListItems = new List<communities>();
            foreach (ListItem item in items)
            {
                int id;
                if (int.TryParse(item.Value, out id))
                {
                    communitiesByListItems.Add(CommunityDB.GetCommunityById(id));
                }
            }


            int permissionsRowsChanged = 0;

            if (isAdding)
            {

                foreach (var community in communitiesByListItems)
                {
                    if (!CommunityPermissionsDB.HasUserPermissionForCommunityWithRole(user, community,selectedRole))
                    {
                        permissionsRowsChanged +=
                            (CommunityPermissionsDB.AddCommunityPermissions(new community_permissions()
                            {
                                users_Id = user.Id,
                                communities_Id = community.Id,
                                users = user,
                                communities = community,
                                Role = selectedRole
                            }))
                                ? 1
                                : 0;
                    }
                }

            }
            else
            {
                foreach (var community in communitiesByListItems)
                {
                    if (CommunityPermissionsDB.HasUserPermissionForCommunityWithRole(user, community, selectedRole))
                    {
                        community_permissions cP =
                            user.community_permissions.SingleOrDefault(
                                p => !p.IsDeleted && p.communities == community && p.Role.Equals(selectedRole));
                        int permId = -1;
                        if (cP != null)
                        {
                            permId = cP.Id;
                        }

                        if (permId != -1)
                        {
                            permissionsRowsChanged += (CommunityPermissionsDB.DeleteCommunityPermissionsById(permId) > 0)
                            ? 1
                            : 0;
                        }

                    }
                }
            }

            

            if (permissionsRowsChanged > 0)
            {
                // Refresh the GridView 
                BindPermissionsToCommunityUserList();

                ActionStatusPermissions2.Text = string.Format("User {0}'s Permissions was Updated.", selectedUserName);
                return true;
            }
            else
            {
                ActionStatusPermissions2.Text = string.Format("User {0}'s Permissions Could Not be Updated!",
                    selectedUserName);
                return false;
            }

            
        }

        protected void AddCommunity_OnClick(object sender, EventArgs e)
        {
            int[] selectedIndices = CommunitiesListBox.GetSelectedIndices();

            ListItem[] selectedItems = selectedIndices.Select(index => CommunitiesListBox.Items[index]).ToArray();

            if (UpdateUsersCommunitiesPermissions(selectedItems, true))
            {
                SelectedCommunitiesListBox.Items.AddRange(selectedItems);

                foreach (var item in selectedItems)
                {
                    CommunitiesListBox.Items.Remove(item);
                }
            }
        }

        protected void RemoveCommunity_OnClick(object sender, EventArgs e)
        {
            int[] selectedIndices = SelectedCommunitiesListBox.GetSelectedIndices();

            ListItem[] selectedItems =
                selectedIndices.Select(index => SelectedCommunitiesListBox.Items[index]).ToArray();

            if (UpdateUsersCommunitiesPermissions(selectedItems, false))
            {
                CommunitiesListBox.Items.AddRange(selectedItems);

                foreach (var item in selectedItems)
                {
                    SelectedCommunitiesListBox.Items.Remove(item);
                }
            }
        }

        #endregion
       
    }
}