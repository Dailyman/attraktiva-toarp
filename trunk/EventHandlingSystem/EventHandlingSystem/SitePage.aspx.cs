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

        private int _wId;

        private string _type;

        protected void Page_PreInit(object sender, EventArgs e)
        {
            //Hämtar WebPageId från URL.
            var stId = Request.QueryString["Id"];

            var stType = Request.QueryString["Type"];

            //Om Id värdet som tas från URLn är i giltigt format hämtas WebPage objektet och visas på sidan.
            if (!string.IsNullOrWhiteSpace(stId) && int.TryParse(stId, out _wId) && !string.IsNullOrWhiteSpace(stType))
            {
                webpages webPage = WebPageDB.GetWebPageById(_wId);
                //if (webPage != null)
                //{
                //    if (webPage.CommunityId != null)
                //    {
                //        //Sätter rätt pagetitel på sidan
                //        Page.Title =
                //            CommunityDB.GetCommunityById((int)webPage.CommunityId).Name;
                //    }
                //    else if (webPage.AssociationId != null)
                //    {
                //        //Sätter rätt pagetitel på sidan
                //        Page.Title = AssociationDB.GetAssociationById((int)webPage.AssociationId).Name;
                //    }
                //    else
                //    {
                //        //Sätter rätt pagetitel på sidan
                //        Page.Title = "Unknown";
                //    }
                //}

                Page.Title = webPage != null ? webPage.Title : "Unknown page";
            }
           
        }



        protected void Page_Init(object sender, EventArgs e)
        {

            ////MyControl is the Custom User Control with a code behind file
            //About myControl = (About)Page.LoadControl("~/About.ascx");

            //Calendar calendarControl = (Calendar) Page.LoadControl("~/Calendar.ascx");

            //var eventListControl = Page.LoadControl("~/EventList.ascx");

            ////ControlHolder is a place holder on the aspx page where I want to load the
            ////user control to.
            //ControlHolder.Controls.Add(myControl);
            //ControlHolder.Controls.Add(calendarControl);
            //ControlHolder.Controls.Add(eventListControl);

            if (_wId > 0)
            {
                if (WebPageDB.GetWebPageById(_wId) != null)
                {
                    foreach (var component in ComponentDB.GetComponentsByWebPageId(_wId).OrderBy(c => c.Row))
                    {
                        ControlHolder.Controls.Add(Page.LoadControl("~/" + component.FileName));
                    }
                    if (ComponentDB.GetComponentsByWebPageId(_wId).Count == 0)
                    {
                        ControlHolder.Controls.Add(Page.LoadControl("~/About.ascx"));
                    }
                }
            }
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