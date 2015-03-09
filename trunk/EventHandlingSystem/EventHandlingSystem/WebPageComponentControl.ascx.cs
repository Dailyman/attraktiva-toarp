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

            //foreach (var asso in CommunityDB.GetCommunityById(
            //    WebPageDB.GetWebPageById(int.Parse(bullListCommWebpages.Items[e.Index].Value)).CommunityId.GetValueOrDefault())
            //    .associations)
            //{
            //    assoIdsInCommunityList.Add(asso.Id);
            //}

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
        protected void bullListCommWebpages_OnClick(object sender, BulletedListEventArgs e)
        {
            lbAssoWebPage.Visible = true;
            lbAssoWebPage.Text = "Association Webpages";
            bullListAssoWebpages.Visible = true;
            PopulateBullListAssoWebpage(e);
        }

        protected void bullListAssoWebpages_OnClick(object sender, BulletedListEventArgs e)
        {

        }

        #endregion
    }
}