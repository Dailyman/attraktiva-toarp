using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit;
using Antlr.Runtime.Misc;
using EventHandlingSystem.Database;
using WebGrease.Css.Extensions;

namespace EventHandlingSystem
{
    public partial class WebPageComponentControl : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            PopulateBullListCommWebpage();
            //CreateDropDownList();
        }

        #region Populate Methods

        public void PopulateBullListCommWebpage()
        {
            bullListCommWebpages.Items.Clear();

            //Hitta webpages för communities och sortera efter titel
            List<webpages> commWebpageList = WebPageDB.GetAllCommunityWebpages().OrderBy(w => w.Title).ToList();

            foreach (ListItem li in from wp in commWebpageList
                where wp.CommunityId != null && wp.CommunityId != 0
                select new ListItem
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

            foreach (associations asso in 
                CommunityDB.GetCommunityById(
                    WebPageDB.GetWebPageById(int.Parse(bullListCommWebpages.Items[e.Index].Value))
                        .CommunityId.GetValueOrDefault()).associations)
            {
                assoIdsInCommunityList.Add(asso.Id);
            }

            foreach (var wp in assoWebpageList.OrderBy(w => w.Title))
            {
                if (assoIdsInCommunityList.Contains(wp.AssociationId.GetValueOrDefault()))
                {
                    bullListAssoWebpages.Items.Add(new ListItem(wp.Title, wp.Id.ToString()));
                }
            }
        }

        //Dropdownlist Controls
        public void PopulateDropDownListControls()
        {
            ddlAddComControls.Items.Clear();

            List<ListItem> controlList = ControlDB.GetAllControls().Select(c=> new ListItem
            {
                Text = c.Name,
                Value = c.Id.ToString()
            }).ToList();

            foreach (ListItem item in controlList.OrderBy(i => i.Text))
            {
                ddlAddComControls.Items.Add(item);
            }
        }


        #endregion


        #region Show Methods

        // För att visa komponenter
        protected void ShowComponentsInWebPage(List<components> componentList)
        {
            lbComponentDetails.Text = componentList.Count != 0 ? "Components" : "No components in webpage";

            RepeaterComponents.DataSource = componentList;
            RepeaterComponents.DataBind();
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

            //Visa information från databas för Community webpage
            webpages currentWp = WebPageDB.GetWebPageById(int.Parse(bullListCommWebpages.Items[e.Index].Value));
            tbWebpageTitle.Text = currentWp.Title ?? "Untitled";
            lbCommAssoName.Text = "<b>Community Name: </b>";
            hlnkCommAssoName.Text = CommunityDB.GetCommunityById(currentWp.CommunityId.GetValueOrDefault()).Name;
            hlnkCommAssoName.NavigateUrl = "/SitePage.aspx?id=" +
                                           (WebPageDB.GetWebPageByCommunityId(currentWp.CommunityId.GetValueOrDefault()) !=
                                            null
                                               ? WebPageDB.GetWebPageByCommunityId(
                                                   currentWp.CommunityId.GetValueOrDefault()).Id.ToString()
                                               : "") + "&type=C";
            tbLayout.Text = currentWp.Layout ?? "No Layout Specified";
            tbStyle.Text = currentWp.Style ?? "No Style Specified";
            hdnfWebpageId.Value = currentWp.Id.ToString();

            // Visa komponenter i en webpage
            ShowComponentsInWebPage(
                ComponentDB.GetComponentsByWebPageId(currentWp.Id)
                    .OrderBy(c => c.OrderingNumber)
                    .ThenBy(c => c.controls.Name)
                    .ToList());

            PopulateDropDownListControls();
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
            bullListAssoWebpages.Items[e.Index].Attributes.CssStyle.Add(HtmlTextWriterStyle.Color, "Cyan");

            //Visa WebPage Details för Association
            MultiViewWebPageDetails.ActiveViewIndex = 0;

            //Visa information från databas för Association webpage
            webpages currentWp = WebPageDB.GetWebPageById(int.Parse(bullListAssoWebpages.Items[e.Index].Value));
            tbWebpageTitle.Text = currentWp.Title ?? "Untitled";
            lbCommAssoName.Text = "<b>Association Name: </b>";
            hlnkCommAssoName.Text = AssociationDB.GetAssociationById(currentWp.AssociationId.GetValueOrDefault()).Name;
            hlnkCommAssoName.NavigateUrl = "/SitePage.aspx?id=" +
                                           (WebPageDB.GetWebPageByAssociationId(
                                               currentWp.AssociationId.GetValueOrDefault()) != null
                                               ? WebPageDB.GetWebPageByAssociationId(
                                                   currentWp.AssociationId.GetValueOrDefault()).Id.ToString()
                                               : "") + "&type=A";
            tbLayout.Text = currentWp.Layout ?? "No Layout Specified";
            tbStyle.Text = currentWp.Style ?? "No Style Specified";
            hdnfWebpageId.Value = currentWp.Id.ToString();

            // Visa komponenter i en webpage
            ShowComponentsInWebPage(
                ComponentDB.GetComponentsByWebPageId(currentWp.Id)
                    .OrderBy(c => c.OrderingNumber)
                    .ThenBy(c => c.controls.Name)
                    .ToList());

            PopulateDropDownListControls();
        }


        //För att uppdatera Webpage
        protected void btnWebpageUpdate_OnClick(object sender, EventArgs e)
        {
            //Använd hiddenfield för att hitta rätt webpage Id
            webpages wpToUpdate = WebPageDB.GetWebPageById(int.Parse(hdnfWebpageId.Value));
            wpToUpdate.Title = tbWebpageTitle.Text;
            wpToUpdate.Layout = tbLayout.Text;
            wpToUpdate.Style = tbStyle.Text;

            //Anropa Update-metoden
            int affectedRows = WebPageDB.UpdateWebPage(wpToUpdate);

            if (affectedRows != 0)
            {
                lbWebPageUpdate.Text = tbWebpageTitle.Text + " has been updated";
                lbWebPageUpdate.Style.Add(HtmlTextWriterStyle.Color, "#217ebb");
            }
        }


        // För att lägga till nya komponenter
        protected void AddControl_OnClick(object sender, EventArgs e)
        {

        }


        #endregion


        #region Repeater

        // För att populera Dropdownlistorna för komponenter
        protected void RepeaterComponents_OnItemCreated(object sender, RepeaterItemEventArgs e)
        {
            // Lägg in databunden lista som redan finns i Repeater
            List<components> compList = (List<components>) RepeaterComponents.DataSource;
            
            DropDownList ddlOrderingNo = e.Item.FindControl("ddlOrderingNO") as DropDownList;
            DropDownList ddlComControl = e.Item.FindControl("ddlComControls") as DropDownList;
            
            List<ListItem> orderNo = new List<ListItem>();
            List<ListItem> controlName = new List<ListItem>();

            if (compList != null)
            {
                // Omvandla till listItem för dropdownlist
                foreach (components comp in compList)
                {
                    orderNo.Add(new ListItem
                    {
                        Text = comp.OrderingNumber.ToString(),
                        Value = comp.Id.ToString()
                    });

                    controlName.Add(new ListItem
                    {
                        Text = comp.controls.Name,
                        Value = comp.Id.ToString()
                    });
                }

                // Lägg in item i dropdownlist
                if (ddlOrderingNo != null && ddlComControl != null)
                {
                    foreach (ListItem item in orderNo)
                    {
                        ddlOrderingNo.Items.Add(item);
                    }

                    foreach (ListItem item in controlName)
                    {
                        ddlComControl.Items.Add(item);
                    }
                }
            }
        }

        #endregion


        
    }
}