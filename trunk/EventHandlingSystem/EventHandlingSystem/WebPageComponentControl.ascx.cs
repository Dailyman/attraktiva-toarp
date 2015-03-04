using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit;
using EventHandlingSystem.Database;

namespace EventHandlingSystem
{
    public partial class WebPageComponentControl : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            PopulateBullListCommWebpage();
            PopulateBullListAssoWebpage();
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

        public void PopulateBullListAssoWebpage()
        {
            bullListAssoWebpages.Items.Clear();

            //Hitta webpages för communities och sortera efter titel
            List<webpages> assoWebpageList = WebPageDB.GetAllAssociationWebpages().OrderBy(w => w.Title).ToList();

            foreach (var wp in assoWebpageList)
            {
                //Är endast en community webpage om CommId inte är null eller noll
                if (wp.AssociationId != null && wp.AssociationId != 0)
                {
                    //För att lägga in i bulleted list
                    ListItem li = new ListItem
                    {
                        Text = wp.Title,
                        Value = wp.Id.ToString()
                    };
                    bullListAssoWebpages.Items.Add(li);
                }
            }
        }

        #endregion

        protected void bullListCommWebpages_OnClick(object sender, BulletedListEventArgs e)
        {
            //bullListCommWebpages.SelectedItem.Attributes.CssStyle.Add(HtmlTextWriterStyle.FontWeight, "bold");
        }

        protected void bullListAssoWebpages_OnClick(object sender, BulletedListEventArgs e)
        {
           
        }
    }
}