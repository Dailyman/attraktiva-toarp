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


                if (!ispermitted)
                {
                    PanelPreContent.Visible = true;

                    foreach (var listItem in GetCommuityListItems())
                    {
                        CommPageSettingsList.Items.Add(listItem);
                    }
                    foreach (var listItem in GetAssociationListItems())
                    {
                        AssoPageSettingsList.Items.Add(listItem);
                    }
                }
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

            if (!stType.Equals("c", StringComparison.OrdinalIgnoreCase) &&
                !stType.Equals("a", StringComparison.OrdinalIgnoreCase))
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


        private IEnumerable<ListItem> GetAssociationListItems()
        {
            return
                GetCurrentUsersAssociations()
                    .Select(
                        asso =>
                            new ListItem(asso.Name,
                                "/Admin/PageSettings?id=" + (WebPageDB.GetWebPageByAssociationId(asso.Id) == null
                                    ? ""
                                    : WebPageDB.GetWebPageByAssociationId(asso.Id).Id.ToString()) +
                                "&type=a"));
        }

        private IEnumerable<ListItem> GetCommuityListItems()
        {
            return
                 GetCurrentUsersCommunities()
                     .Select(
                         comm =>
                             new ListItem(comm.Name,
                                 "/Admin/PageSettings?id=" + (WebPageDB.GetWebPageByCommunityId(comm.Id) == null
                                     ? ""
                                     : WebPageDB.GetWebPageByCommunityId(comm.Id).Id.ToString()) +
                                 "&type=c"));
        }

        private IEnumerable<associations> GetCurrentUsersAssociations()
        {
            string username = HttpContext.Current.User.Identity.Name;

            if (string.IsNullOrWhiteSpace(username))
            {
                DetailErrorLabel.Text += "Current username was empty.";
                return null;
            }
            if (Membership.GetUser(username) == null)
            {
                DetailErrorLabel.Text += "Current user was not found in the mebership database.";
                return null;
            }
            if (UserDB.GetUserByUsername(username) == null)
            {
                DetailErrorLabel.Text += "Current user was not found.";
                return null;
            }

            users currentUser = UserDB.GetUserByUsername(username);

            List<associations> associationsForUser = AssociationPermissionsDB.GetAllAssociationPermissionsByUserAndRole(currentUser, "Administrators").Select(p => p.associations).ToList();
            return associationsForUser;
        }

        private IEnumerable<communities> GetCurrentUsersCommunities()
        {
            string username = HttpContext.Current.User.Identity.Name;

            if (string.IsNullOrWhiteSpace(username))
            {
                DetailErrorLabel.Text += "Current username was empty.";
                return null;
            }
            if (Membership.GetUser(username) == null)
            {
                DetailErrorLabel.Text += "Current user was not found in the mebership database.";
                return null;
            }
            if (UserDB.GetUserByUsername(username) == null)
            {
                DetailErrorLabel.Text += "Current user was not found.";
                return null;
            }

            users currentUser = UserDB.GetUserByUsername(username);

            List<communities> communitiesForUser = CommunityPermissionsDB.GetAllCommunityPermissionsByUserAndRole(currentUser, "Administrators").Select(p => p.communities).ToList();
            return communitiesForUser;
        }  
    }
}