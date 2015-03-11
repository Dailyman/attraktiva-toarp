using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using EventHandlingSystem.Database;
using Microsoft.AspNet.Membership.OpenAuth;

namespace EventHandlingSystem.Account
{
    public partial class Register : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
            RegisterUser.ContinueDestinationPageUrl = Request.QueryString["ReturnUrl"];
        }

        protected void RegisterUser_CreatedUser(object sender, EventArgs e)
        {
            FormsAuthentication.SetAuthCookie(RegisterUser.UserName, createPersistentCookie: false);

            //Add user to the Members role
            Roles.AddUserToRole(RegisterUser.UserName, "Members");
            //Copy the user to the second users table
            if (!String.IsNullOrWhiteSpace(RegisterUser.UserName))
            {
                UserDB.AddUser(new users() { Username = RegisterUser.UserName });
            }

            string continueUrl = RegisterUser.ContinueDestinationPageUrl;
            if (!OpenAuth.IsLocalUrl(continueUrl))
            {
                continueUrl = "~/";
            }
            Response.Redirect(continueUrl);


           
        }
    }
}