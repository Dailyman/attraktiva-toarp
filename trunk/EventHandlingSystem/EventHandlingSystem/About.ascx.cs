using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EventHandlingSystem.Database;

namespace EventHandlingSystem
{
    public partial class About : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Hämtar EventId från URL.
            var stId = Request.QueryString["Id"];

            var stType = Request.QueryString["Type"];

            //Om Id värdet som tas från URLn är i giltigt format hämtas WebPage objektet och visas på sidan.
            int id;
            if (!string.IsNullOrWhiteSpace(stId) && int.TryParse(stId, out id) && !string.IsNullOrWhiteSpace(stType))
            {
                webpages webPage = WebPageDB.GetWebPageById(id);
                if (webPage != null)
                {
                    if (String.Equals(stType, "c", StringComparison.OrdinalIgnoreCase))
                    {

                        LiteralAbout.Text = "This is a Community with no description";
                        if (webPage.CommunityId != null)
                            ImageLogo.ImageUrl = CommunityDB.GetCommunityById((int)webPage.CommunityId).LogoUrl;
                    }
                    else if (String.Equals(stType, "a", StringComparison.OrdinalIgnoreCase))
                    {
                        LiteralAbout.Text = "This is an Association with no description";
                        if (webPage.AssociationId != null)
                            ImageLogo.ImageUrl = AssociationDB.GetAssociationById((int)webPage.AssociationId).LogoUrl;
                    }
                    else
                    {
                        //Sätter rätt pagetitel på sidan
                        LiteralAbout.Text = "Unknown type";
                    }
                }
            }
        }
    }
}