using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EventHandlingSystem
{
    public partial class PageSettings : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LabelUserName.Text = "Username: " + HttpContext.Current.User.Identity.Name;
        }
    }
}