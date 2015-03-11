using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using EventHandlingSystem.Database;

namespace EventHandlingSystem
{
    public partial class RolesPermissionsControl : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                foreach (var role in Roles.GetAllRoles())
                {
                    DropDownListRoles.Items.Add(new ListItem(role));
                }
            }
        }

        protected void DropDownListRoles_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            BulletedListUsersInRoles.Items.Clear();

            var usersInRole = Roles.GetUsersInRole(DropDownListRoles.SelectedValue);
            foreach (var user in usersInRole)
            {
                if (UserDB.GetUsersByUsername(user) != null)
                {
                    BulletedListUsersInRoles.Items.Add(new ListItem(user, UserDB.GetUsersByUsername(user).Id.ToString()));
                }
            }
        }
    }
}