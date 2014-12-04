using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EventHandlingSystem.Database;

namespace EventHandlingSystem
{
    public partial class SitePage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Hämtar EventId från URL.
            var stId = Request.QueryString["Id"];

            //Lägger till alla evenemang Titel och Id i DropDownListan.
            foreach (var wP in WebPageDB.GetAllWebPages())
            {
                DropDownListWebPages.Items.Add(new ListItem(wP.Id.ToString(), wP.Id.ToString()));
            }

            //Om Id värdet som tas från URLn är i giltigt format hämtas WebPage objektet och visas på sidan.
            int id;
            if (!string.IsNullOrWhiteSpace(stId) && int.TryParse(stId, out id))
            {
                WebPage webPage = WebPageDB.GetWebPageById(id);
                if (webPage != null)
                {
                    LabelTitle.Style.Add(HtmlTextWriterStyle.FontSize, "35px");
                    if (webPage.Community != null)
                    {
                        LabelTitle.Text =
                            TermSetDB.GetTermSetById(webPage.Community.PublishingTermSetId).Name;
                        LabelWelcome.Text = "Welcome to this community!";
                    }
                    else if (webPage.Association != null)
                    {
                        LabelTitle.Text =
                            TermSetDB.GetTermSetById(webPage.Association.PublishingTermSetId).Name;
                        LabelWelcome.Text = "Welcome to this association!";
                    }
                    else
                    {
                        LabelTitle.Text = "Unknown";
                        LabelWelcome.Text = "Empty page?";
                    }
                }
            }
        }

        protected void BtnLoadPage_OnClick(object sender, EventArgs e)
        {
            //Skickar användaren till SitePage.aspx med det WebPageId som man valt i DropDownListan.
            Response.Redirect(Request.Url.AbsolutePath + "?id=" + DropDownListWebPages.SelectedValue, true);
        }
    }
}