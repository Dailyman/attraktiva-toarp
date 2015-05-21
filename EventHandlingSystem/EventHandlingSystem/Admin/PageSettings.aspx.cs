using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using EventHandlingSystem.Database;

namespace EventHandlingSystem.Admin
{
    public partial class PageSettings : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bool ispermitted = IsCurrentUserPermitedToPage(HttpContext.Current.User.Identity.Name);
                PanelContent.Enabled = ispermitted;
                PanelContent.Visible = ispermitted;
            }
            
        }

        private int GetCurrentWebPageIdFromQueryString()
        {
            // Gets the ID from the QueryString
                var stId = Request.QueryString["Id"];
            
                // If the ID from the QueryString is in a valid format its stored
                if (string.IsNullOrWhiteSpace(stId))
                {
                    DetailErrorLabel.Text = "Id was empty or null!";
                    return -1;
                }

            int wPId;
            if(!int.TryParse(stId, out wPId))
                {
                    DetailErrorLabel.Text = "Id is not an number!";
                    return -1;
                }

                if (WebPageDB.GetWebPageById(wPId) == null)
                {
                    DetailErrorLabel.Text = "No page by that id was found!";
                    return -1;
                }

            return wPId;
        }

        private string GetCurrentWebPageTypeFromQueryString()
        {
            // Variabel to return
            string pageType = "";

            // Gets the Type from the QueryString
            var stType = Request.QueryString["Type"];

                
                if (string.IsNullOrWhiteSpace(stType))
                {
                    DetailErrorLabel.Text = "The pagetype was empty or null!";
                    return pageType;
                }
                
                if (!stType.Equals("c", StringComparison.OrdinalIgnoreCase) && !stType.Equals("a", StringComparison.OrdinalIgnoreCase))
                {
                    DetailErrorLabel.Text = "The pagetype is not a correct value!";
                    return pageType;
                }
                // Overwrite the variabel to return, with the approved value
                    pageType = stType;

                return pageType;
        }


        private bool IsCurrentUserPermitedToPage(string username)
        {
            int wPId = GetCurrentWebPageIdFromQueryString();
            string wPType = GetCurrentWebPageTypeFromQueryString();

            if (wPId == -1 && String.IsNullOrWhiteSpace(wPType))
            {
                ErrorLabel.Text = "Page could not be loaded!";
                return false;
            }
            var user = UserDB.GetUserByUsername(username);
            var memberUser = Membership.GetUser(username);
            if (user == null || memberUser == null)
            {
                ErrorLabel.Text = "Page could not be loaded! User does not exist.";
                return false;
            }

            webpages wP = WebPageDB.GetWebPageById(wPId);
            if (wP == null)
            {
                ErrorLabel.Text = "Page could not be found!.";
                return false;
            }

            if (wPType.Equals("c", StringComparison.OrdinalIgnoreCase) && WebPageDB.IsWebPageForCommunity(wP))
            {
                var community = CommunityDB.GetCommunityById((int)wP.CommunityId);
                if (community != null &&
                    CommunityPermissionsDB.HasUserPermissionForCommunityWithRole(user, community, "Administrators"))
                    return true;
                ErrorLabel.Text = "Page could not be loaded! User does not have permission.";
                return false;
            }
            
            if (wPType.Equals("a", StringComparison.OrdinalIgnoreCase) && WebPageDB.IsWebPageForAssociation(wP) )
            {
                var association = AssociationDB.GetAssociationById((int)wP.AssociationId);
                if (association != null &&
                    AssociationPermissionsDB.HasUserPermissionForAssociationWithRole(user, association, "Administrators"))
                    return true;
                ErrorLabel.Text = "Page could not be loaded! User does not have permission.";
                return false;
            }
            return false;
        }
    }
}