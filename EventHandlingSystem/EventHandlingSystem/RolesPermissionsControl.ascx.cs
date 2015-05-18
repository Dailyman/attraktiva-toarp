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
                BindUsersToUserList2();
                BindAssociationsToListBox();
                BindAssociationsToList();

                // Add the selected user's associations in the SelectedAssociationsListBox
                SelectAssociationsForSelectedUser();

                // Display those users belonging to the currently selected association 
                DisplayUsersBelongingToAssociation();
            }
        }

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
                ActionStatusPermissions.Text = string.Format("The user {0} does not exist in the system.", selectedUserName);

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




        private void BindUsersToUserList2()
        {
            // Get all of the user accounts 
            var users = Membership.GetAllUsers().Cast<MembershipUser>().OrderBy(user => user.UserName).ToList();
            UserList2.DataSource = users;
            UserList2.DataBind();
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


        private void SelectAssociationsForSelectedUser()
        {
            // Reset the ListBoxes
            BindAssociationsToListBox();

            // Get the selected user
            string selectedUserName = UserList2.SelectedValue;

            if (UserDB.GetUserByUsername(selectedUserName) == null)
            {
                ActionStatusPermissions.Text = "User does not exist. Try refreshing the page.";
            }
            else
            {
                var selectedUsersAssociationsList = new List<associations>();
                foreach (var asso in  (from permission in UserDB.GetUserByUsername(selectedUserName).association_permissions
                        where !permission.associations.IsDeleted && !permission.IsDeleted
                        select permission.associations).Where(a => !selectedUsersAssociationsList.Contains(a)))
                {
                    selectedUsersAssociationsList.Add(asso);
                }

                associations[] selectedUsersAssociations = selectedUsersAssociationsList.ToArray();


                var selectedUsersAssociationsNamesList = new List<string>();
                foreach (var assoName in (from association in selectedUsersAssociations where !association.IsDeleted select association.Name).Where(name => !selectedUsersAssociationsNamesList.Contains(name)))
                {
                    selectedUsersAssociationsNamesList.Add(assoName);
                }
                   
                string[] selectedUsersAssociationsNames =selectedUsersAssociationsNamesList.ToArray();

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
        }

        protected void UserList2_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            SelectAssociationsForSelectedUser();
        }

        private void UpdateUsersAssociationsPermissions(IEnumerable<ListItem> items, bool isAdding)
        {
            // Get the selected user
            string selectedUserName = UserList2.SelectedValue;

            users user = UserDB.GetUserByUsername(selectedUserName);

            // Make sure the user exists
            if (user == null)
            {
                ActionStatusPermissions.Text = string.Format("The user {0} does not exist in the system.", selectedUserName);

                // Refresh the UserList because it was probably not updated
                BindUsersToUserList2();
                return;
            }


            // Get Associations by items in SelectedAssociationsListbox
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
                
                    foreach (var association in associationsByListItems)
                    {
                        if (!AssociationPermissionsDB.HasUserPermissionForAssociation(user, association))
                        {
                            permissionsRowsChanged +=
                                (AssociationPermissionsDB.AddAssociationPermissions(new association_permissions()
                                {
                                    users_Id = user.Id,
                                    associations_Id = association.Id,
                                    users = user,
                                    associations = association,
                                    Role = "Contributors"
                                }))
                                    ? 1
                                    : 0;
                        }
                    }
                
            }
            else
            {
                foreach (var association in associationsByListItems)
                {
                    if (AssociationPermissionsDB.HasUserPermissionForAssociation(user, association))
                    {

                        //int test = (from permission in user.association_permissions
                        //            where !permission.IsDeleted && permission.associations_Id == association.Id
                        //            select permission.Id).SingleOrDefault();

                        association_permissions aP =
                            user.association_permissions.SingleOrDefault(
                                p => !p.IsDeleted && p.associations == association && p.Role.Equals("Contributors"));
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


                //foreach (var item in associationsByListItems)
                //{
                //    // Loop through users association permissions 
                //    foreach (var uPermission in user.association_permissions)
                //    {
                //        if (associationsByListItems.Select(a => a.Id).All(id => id != uPermission.associations_Id))
                //        {
                //            isPermissionsChanged = AssociationPermissionsDB.DeleteAssociationPermissionsById(uPermission.Id) > 0;
                //        }
                //    }
                //}

            }



            

            //// Get association permissions by associations in the list of SelcetedAssociations In Listbox
            //var aPermission = new List<association_permissions>();
            //foreach (var association in associationsInAssociationListbox)
            //{
            //    aPermission.AddRange(association.association_permissions);
            //}

            

            

            if (permissionsRowsChanged > 0)
            {
                ActionStatusPermissions.Text = string.Format("User {0}'s Permissions was Updated.", selectedUserName);
            }
            else
            {
                ActionStatusPermissions.Text = string.Format("User {0}'s Permissions Could Not be Updated!",
                    selectedUserName);
            }

            // Refresh the GridView 
            DisplayUsersBelongingToAssociation();
        }

        protected void AddAssociation_OnClick(object sender, EventArgs e)
        {
            int[] selectedIndices = AssociationsListBox.GetSelectedIndices();

            ListItem[] selectedItems = selectedIndices.Select(index => AssociationsListBox.Items[index]).ToArray();
            
            UpdateUsersAssociationsPermissions(selectedItems, true);

            SelectedAssociationsListBox.Items.AddRange(selectedItems);

            foreach (var item in selectedItems)
            {
                AssociationsListBox.Items.Remove(item);
            }

            
        }

        protected void RemoveAssociation_OnClick(object sender, EventArgs e)
        {
            int[] selectedIndices = SelectedAssociationsListBox.GetSelectedIndices();

            ListItem[] selectedItems =
                selectedIndices.Select(index => SelectedAssociationsListBox.Items[index]).ToArray();

            UpdateUsersAssociationsPermissions(selectedItems, false);

            AssociationsListBox.Items.AddRange(selectedItems);

            foreach (var item in selectedItems)
            {
                SelectedAssociationsListBox.Items.Remove(item);
            }

            
        }


        private void DisplayUsersBelongingToAssociation()
        {
            // Get the selected association 
            associations selectedAssociation = AssociationDB.GetAssociationById(int.Parse(AssociationList.SelectedValue));

            // Get the list of usernames that belong to the association 
            List<string> userNames = new List<string>();
            foreach (var permission in selectedAssociation.association_permissions)
            {
                if (!permission.users.IsDeleted)
                {
                    userNames.Add(permission.users.Username);
                }  
            }
            string[] usersPermissionToAssociation = userNames.ToArray();

            // Bind the list of users to the GridView 
            AssociationUserList.DataSource = usersPermissionToAssociation;
            AssociationUserList.DataBind();
        }

        protected void AssociationList_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            DisplayUsersBelongingToAssociation();
        }

        protected void AssociationUserList_OnRowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            // Get the selected association 
            associations selectedAssociation = AssociationDB.GetAssociationById(int.Parse(AssociationList.SelectedValue));

            // Reference the UserNameLabel 
            Label UserNameLabel = AssociationUserList.Rows[e.RowIndex].FindControl("UserNameLabelInAssociation") as Label;

            users userToRemove = UserDB.GetUserByUsername(UserNameLabel.Text);

            // Make sure the association exists
            if (selectedAssociation == null)
            {
                ActionStatusPermissions.Text = string.Format("The user {0} does not exist in the system.",
                   UserNameLabel.Text);

                // Refresh the AssociationList because it was probably not updated
                BindAssociationsToList();
                // Refresh the GridView because it was probably not updated, and bacause the AssociationList is being updated
                DisplayUsersBelongingToAssociation();
                return;
            }

            // Make sure the user exists
            if (userToRemove == null )
            {
                ActionStatusPermissions.Text = string.Format("The user {0} does not exist in the system.",
                    UserNameLabel.Text);
                
                // Refresh the GridView because it was probably not updated
            DisplayUsersBelongingToAssociation();
                return;
            }

            // Remove the user from the selected associations permission 
            List<int> permissionIdToDelete = new List<int>();
            foreach (var permission in AssociationPermissionsDB.GetAllAssociationPermissionsByAssociation(selectedAssociation))
            {
                if (permission.users == userToRemove)
                {
                    permissionIdToDelete.Add(permission.Id);
                }   
            }

            int affectedRows = 0;
            foreach (var id in permissionIdToDelete)
            {
                affectedRows = AssociationPermissionsDB.DeleteAssociationPermissionsById(id);
            }


            // Update and display a status message 
            if (affectedRows > 0)
            {
                
            ActionStatusPermissions.Text = string.Format("User {0} was removed from association {1}.",
                UserNameLabel.Text,
                selectedAssociation.Name);
            }
            else
            {
                ActionStatusPermissions.Text = string.Format("User {0} could not be removed from association {1}.",
                UserNameLabel.Text,
                selectedAssociation.Name);
            }
            
            // Refresh the GridView 
            DisplayUsersBelongingToAssociation();

            // Refresh the "by user" interface 
            SelectAssociationsForSelectedUser();
        }

        protected void AddUserToAssociation_OnClick(object sender, EventArgs e)
        {
            associations selectedAssociation = AssociationDB.GetAssociationById(int.Parse(AssociationList.SelectedValue));
            string userNameToAdd = UserNameToAddToAssociation.Text;

            // Make sure that a value was entered 
            if (userNameToAdd.Trim().Length == 0)
            {
                ActionStatusPermissions.Text = "You must enter a username in the textbox.";
                return;
            }

            users userToAdd = UserDB.GetUserByUsername(UserNameToAddToAssociation.Text);

            // Make sure the user exists
            if (userToAdd == null)
            {
                ActionStatusPermissions.Text = string.Format("The user {0} does not exist in the system.", userNameToAdd);
                return;
            }

            // Make sure that the user doesn't already has permission to this association 
            foreach (var associationPermission in userToAdd.association_permissions)
            {
                if (associationPermission.associations.Id == selectedAssociation.Id)
                {
                    ActionStatusPermissions.Text = string.Format("The user {0} already has permissions to association {1}.", userNameToAdd, selectedAssociation.Name);
                    return;
                }
            }
           

            // Add the permission to the user and make sure it was successfully added
            if (AssociationPermissionsDB.AddAssociationPermissions(new association_permissions() { users_Id = userToAdd.Id, associations_Id = selectedAssociation.Id,users = userToAdd, associations = selectedAssociation, Role = "Contributors"}))
            {
                ActionStatusPermissions.Text = string.Format("User {0} was added to role {1}.", userNameToAdd, selectedAssociation.Name);
            }

            // Clear out the TextBox 
            UserNameToAddToAssociation.Text = string.Empty;

            // Refresh the GridView
            DisplayUsersBelongingToAssociation();

            // Refresh the "by user" interface
            SelectAssociationsForSelectedUser();
        }
    }
}