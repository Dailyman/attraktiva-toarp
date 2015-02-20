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
        protected void Page_PreInit(object sender, EventArgs e)
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
                    if (webPage.CommunityId != null)
                    {
                        //Sätter rätt pagetitel på sidan
                        Page.Title =
                            CommunityDB.GetCommunityById((int)webPage.CommunityId).Name;
                    }
                    else if (webPage.AssociationId != null)
                    {
                        //Sätter rätt pagetitel på sidan
                        Page.Title = AssociationDB.GetAssociationById((int)webPage.AssociationId).Name;
                    }
                    else
                    {
                        //Sätter rätt pagetitel på sidan
                        Page.Title = "Unknown";
                    }
                }
            }
           
        }



        protected void Page_Init(object sender, EventArgs e)
        {

            //MyControl is the Custom User Control with a code behind file
            About myControl = (About)Page.LoadControl("~/About.ascx");

            //ControlHolder is a place holder on the aspx page where I want to load the
            //user control to.
            ControlHolder.Controls.Add(myControl);

        }



        protected void Page_Load(object sender, EventArgs e)
        {
            ////Hämtar EventId från URL.
            //var stId = Request.QueryString["Id"];

            //var stType = Request.QueryString["Type"];

            ////Om Id värdet som tas från URLn är i giltigt format hämtas WebPage objektet och visas på sidan.
            //int id;
            //if (!string.IsNullOrWhiteSpace(stId) && int.TryParse(stId, out id) && !string.IsNullOrWhiteSpace(stType))
            //{
            //    webpages webPage = WebPageDB.GetWebPageById(id);
            //    if (webPage != null)
            //    {
            //        if (webPage.CommunityId != null)
            //        {
            //            LabelTitle.Text = CommunityDB.GetCommunityById((int)webPage.CommunityId).Name;
            //            LabelWelcome.Text = "Welcome to this community!";
            //        }
            //        else if (webPage.AssociationId != null)
            //        {
            //            LabelTitle.Text = AssociationDB.GetAssociationById((int)webPage.AssociationId).Name;
            //            LabelWelcome.Text = "Welcome to this association!";
            //        }
            //        else
            //        {
            //            LabelTitle.Text = "Unknown";
            //            LabelWelcome.Text = "Empty page?";
            //        }
            //    }
            //}
            //else
            //{
            //    LabelTitle.CssClass = "ribbon-title-small";
            //}



        }
    }
}