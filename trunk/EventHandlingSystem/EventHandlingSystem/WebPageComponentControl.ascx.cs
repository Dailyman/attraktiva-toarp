using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit;
using EventHandlingSystem.Database;
using WebGrease.Css.Extensions;

namespace EventHandlingSystem
{
    public partial class WebPageComponentControl : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            PopulateBullListCommWebpage();
        }

        #region Populate Methods

        public void PopulateBullListCommWebpage()
        {
            bullListCommWebpages.Items.Clear();

            //Hitta webpages för communities och sortera efter titel
            List<webpages> commWebpageList = WebPageDB.GetAllCommunityWebpages().OrderBy(w => w.Title).ToList();

            foreach (ListItem li in from wp in commWebpageList where wp.CommunityId != null && wp.CommunityId != 0 select new ListItem
            {
                Text = wp.Title,
                Value = wp.Id.ToString()
            })
            {
                bullListCommWebpages.Items.Add(li);
            }
        }

        public void PopulateBullListAssoWebpage(BulletedListEventArgs e)
        {
            bullListAssoWebpages.Items.Clear();

            //Hitta webpages för ALLA associations och lägg i en lista
            List<webpages> assoWebpageList = WebPageDB.GetAllAssociationWebpages();

            //Gör en lista för assoId som tillhör samma community
            List<int> assoIdsInCommunityList = new List<int>();

            ////Id för vald Community Webpage
            //int selectedCommWebpageId = int.Parse(bullListCommWebpages.Items[e.Index].Value);
            
            ////Det riktiga id för den valda community
            //int selectedCommWebpageCommId = WebPageDB.GetWebPageById(selectedCommWebpageId).CommunityId.GetValueOrDefault();

            //communities comm = CommunityDB.GetCommunityById(selectedCommWebpageCommId);
            //List<associations> assoList = AssociationDB.GetAllAssociationsInCommunity(comm);

            //foreach (var a in assoList)
            //{
            //    assoIdsInCommunityList.Add(a.Id);
            //}

            foreach (associations asso in AssociationDB.GetAllAssociationsInCommunity(
                        CommunityDB.GetCommunityById(
                            WebPageDB.GetWebPageById(int.Parse(bullListCommWebpages.Items[e.Index].Value))
                                .CommunityId.GetValueOrDefault())))
            {
                assoIdsInCommunityList.Add(asso.Id);
            }

            foreach (var wp in assoWebpageList.OrderBy(w => w.Title))
            {
                if (assoIdsInCommunityList.Contains(wp.AssociationId.GetValueOrDefault()))
                {
                    bullListAssoWebpages.Items.Add(new ListItem(wp.Title,wp.Id.ToString()));
                }
            }
        }

        #endregion


        #region Click Methods

        //Klick på Comm Webpage listitem
        protected void bullListCommWebpages_OnClick(object sender, BulletedListEventArgs e)
        {
            lbWebPageUpdate.Text = string.Empty;

            //Markera valt item
            bullListCommWebpages.Items[e.Index].Attributes.CssStyle.Add(HtmlTextWriterStyle.FontWeight, "Bold");
            bullListCommWebpages.Items[e.Index].Attributes.CssStyle.Add(HtmlTextWriterStyle.Color, "Cyan");

            //Visa Association Webpage-lista
            lbAssoWebPage.Visible = true;
            lbAssoWebPage.Text = "Association Webpages";
            bullListAssoWebpages.Visible = true;
            PopulateBullListAssoWebpage(e);

            //Visa WebPage Details för Association
            MultiViewWebPageDetails.ActiveViewIndex = 0;
            lbWebpageCommId.Visible = true;
            lbWebpageAssoId.Visible = false;

            //Visa information från databas för Community webpage
            webpages currentWp = WebPageDB.GetWebPageById(int.Parse(bullListCommWebpages.Items[e.Index].Value));
            lbWebPageTitle.Text = currentWp.Title ?? "Untitled";
            lbWebpageCommId.Text = "Community Id: " + currentWp.CommunityId;
            tbLayout.Text = currentWp.Layout ?? "No Layout Specified";
            tbStyle.Text = currentWp.Style ?? "No Style Specified";
            hdnfWebpageId.Value = currentWp.Id.ToString();
        }


        //Klick på Asso Webpage listitem
        protected void bullListAssoWebpages_OnClick(object sender, BulletedListEventArgs e)
        {
            lbWebPageUpdate.Text = string.Empty;

            //Visa Association Webpage-lista
            lbAssoWebPage.Visible = true;
            lbAssoWebPage.Text = "Association Webpages";
            bullListAssoWebpages.Visible = true;

            //Markera valt item
            bullListAssoWebpages.Items[e.Index].Attributes.CssStyle.Add(HtmlTextWriterStyle.FontWeight, "Bold");
            bullListAssoWebpages.Items[e.Index].Attributes.CssStyle.Add(HtmlTextWriterStyle.BackgroundColor, "Cyan");
            
            //Visa WebPage Details för Association
            MultiViewWebPageDetails.ActiveViewIndex = 0;
            lbWebpageAssoId.Visible = true;
            lbWebpageCommId.Visible = false;

            //Visa information från databas för Association webpage
            webpages currentWp = WebPageDB.GetWebPageById(int.Parse(bullListAssoWebpages.Items[e.Index].Value));
            lbWebPageTitle.Text = currentWp.Title ?? "Untitled";
            lbWebpageAssoId.Text = "Association Id: " + currentWp.AssociationId;
            tbLayout.Text = currentWp.Layout ?? "No Layout Specified";
            tbStyle.Text = currentWp.Style ?? "No Style Specified";
            hdnfWebpageId.Value = currentWp.Id.ToString();
        }


        //För att uppdatera Webpage
        protected void btnWebpageUpdate_OnClick(object sender, EventArgs e)
        {
            //Använd hiddenfield för att hitta rätt webpage Id
            webpages wpToUpdate = WebPageDB.GetWebPageById(int.Parse(hdnfWebpageId.Value));
            wpToUpdate.Layout = tbLayout.Text;
            wpToUpdate.Style = tbStyle.Text;

            //Anropa Update-metoden
            int affectedRows = WebPageDB.UpdateWebPage(wpToUpdate);

            if (affectedRows != 0)
            {
                lbWebPageUpdate.Text = lbWebPageTitle.Text + " has been updated";
                lbWebPageUpdate.Style.Add(HtmlTextWriterStyle.Color, "#217ebb");
            }
        }
        
        #endregion
    }
}