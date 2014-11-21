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
            foreach (var wP in WebPageDB.GetAllWebPages())
            {
                DropDownListWebPages.Items.Add(new ListItem(wP.Id.ToString(), wP.Id.ToString()));
            }

            
            var stId = Request.QueryString["Id"];

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
                    }
                    else if (webPage.Association != null)
                    {
                        LabelTitle.Text =
                            TermSetDB.GetTermSetById(webPage.Association.PublishingTermSetId).Name;
                    }
                    else
                    {
                        LabelTitle.Text = "Startpage?";
                    }
                }
            }
        }

        protected void BtnLoadPage_OnClick(object sender, EventArgs e)
        {
            Response.Redirect(Request.Url.AbsolutePath + "?id=" + DropDownListWebPages.SelectedValue, true);
        }
    }
}