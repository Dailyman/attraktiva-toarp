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
                        if (webPage.CommunityId != null)
                        {
                            LiteralDescription.Text =
                                CommunityDB.GetCommunityById(webPage.CommunityId.GetValueOrDefault()).Description ??
                                "This is a Community with no description.";
                            ImageLogo.ImageUrl = CommunityDB.GetCommunityById(webPage.CommunityId.GetValueOrDefault()).LogoUrl;
                        }
                    }
                    else if (String.Equals(stType, "a", StringComparison.OrdinalIgnoreCase))
                    {
                        if (webPage.AssociationId != null)
                        {
                            LiteralDescription.Text =
                                AssociationDB.GetAssociationById(webPage.AssociationId.GetValueOrDefault()).Description ??
                                "This is an Association with no description.";
                            ImageLogo.ImageUrl = AssociationDB.GetAssociationById(webPage.AssociationId.GetValueOrDefault()).LogoUrl;

                            //Lägg till kontakter - lista
                            List<members> contactList =
                                MemberDB.GetAllContactsInAssociationByAssoId(webPage.AssociationId.GetValueOrDefault())
                                    .OrderBy(i => i.SurName)
                                    .ToList();

                            if (contactList.Count != 0)
                            {
                                RepeaterContacts.DataSource = contactList;
                                RepeaterContacts.DataBind();
                            }
                            else
                            {
                                lbContactMessage.Text = "There are no members in this Association, hence no contacts.";
                            }
                        }
                    }
                    else
                    {
                        //Sätter rätt pagetitel på sidan
                        LiteralDescription.Text = "Unknown type";
                    }
                }
            }
        }
    }
}