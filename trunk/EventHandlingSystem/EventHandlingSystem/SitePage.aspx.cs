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

            var stType = Request.QueryString["Type"];

            //Om Id värdet som tas från URLn är i giltigt format hämtas WebPage objektet och visas på sidan.
            int id;
            if (!string.IsNullOrWhiteSpace(stId) && int.TryParse(stId, out id) && !string.IsNullOrWhiteSpace(stType))
            {
                WebPage webPage = WebPageDB.GetWebPageById(id);
                if (webPage != null)
                {
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
            else
            {
                LabelTitle.CssClass = "ribbon-title-small";
            }

        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            //Hämtar EventId från URL.
            var stId = Request.QueryString["Id"];

            var stType = Request.QueryString["Type"];

            //Om Id värdet som tas från URLn är i giltigt format hämtas WebPage objektet och visas på sidan.
            int id;
            if (!string.IsNullOrWhiteSpace(stId) && int.TryParse(stId, out id) && !string.IsNullOrWhiteSpace(stType))
            {
                WebPage webPage = WebPageDB.GetWebPageById(id);
                if (webPage != null)
                {
                    if (webPage.Community != null)
                    {
                        //Sätter rätt titel på sidan
                        Page.Title =
                            TermSetDB.GetTermSetById(webPage.Community.PublishingTermSetId).Name;
                    }
                    else if (webPage.Association != null)
                    {
                        //Sätter rätt titel på sidan
                        Page.Title =
                            TermSetDB.GetTermSetById(webPage.Association.PublishingTermSetId).Name;
                    }
                    else
                    {
                        //Sätter rätt titel på sidan
                        Page.Title = "Unknown";
                    }
                }
            }
        }
    }
}