using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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

        public List<ListItem> GetAllControlsListItems()
        {
            return ControlDB.GetAllControls().Select(c => new ListItem
            {
                Text = c.Name,
                Value = c.Id.ToString()
            }).OrderBy(i => i.Text).ToList();
        }

        #endregion


        #region Show Methods

        // För att visa komponenter
        protected void ShowComponentsInWebPage()
        {
            List<Tuple<int,string, string, int>> componentDataList = new List<Tuple<int,string, string,int>>();
            int webPageId;

            if (int.TryParse(hdnfWebpageId.Value, out webPageId))
            {
               List<components> componentList = ComponentDB.GetComponentsByWebPageId(webPageId).
                OrderBy(c => c.OrderingNumber)
                    .ThenBy(c => c.controls.Name)
                    .ToList();

                if (componentList == null || componentList.Count == 0)
                {
                    lbComponentDetails.Text = "No components in webpage";
                }
                else
                {
                    lbComponentDetails.Text = "Components";

                    foreach (var comp in componentList)
                    {
                        var tupdata = new Tuple<int,string, string, int>
                            (comp.Id,comp.OrderingNumber.ToString(), comp.controls.Name, comp.controls_Id);
                        componentDataList.Add(tupdata);
                    }
                }
            }

            GridViewComponentList.DataSource = componentDataList;
            GridViewComponentList.DataBind();

            //RepeaterComponents.DataSource = componentList;
            //RepeaterComponents.DataBind();
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
            ShowComponentsInWebPage();

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
            ShowComponentsInWebPage();

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
            components newComponent = new components();
            
            int oNo;
            newComponent.webpages_Id = int.Parse(hdnfWebpageId.Value);
            newComponent.OrderingNumber = int.TryParse(tbAddOrderingNumber.Text, out oNo) ? oNo : 1;
            newComponent.controls_Id = int.Parse(ddlAddComControls.SelectedValue);

            if (ComponentDB.AddComponent(newComponent))
            {
                LabelActionStatus.Text = "New component has successfully been added!";
                LabelActionStatus.ForeColor = Color.CornflowerBlue;
            }
            else
            {
                LabelActionStatus.Text = "Sorry! New component was not added.";
                LabelActionStatus.ForeColor = Color.Red;
            }
            ShowComponentsInWebPage();
        }

        #endregion


        #region Repeater

        // För att populera Dropdownlistorna för komponenter
        //protected void RepeaterComponents_OnItemCreated(object sender, RepeaterItemEventArgs e)
        //{
        //    // Lägg in databunden lista som redan finns i Repeater
        //    List<components> compList = (List<components>) RepeaterComponents.DataSource;
            
        //    DropDownList ddlOrderingNo = e.Item.FindControl("ddlOrderingNO") as DropDownList;
        //    DropDownList ddlComControl = e.Item.FindControl("ddlComControls") as DropDownList;
            
        //    List<ListItem> orderNo = new List<ListItem>();
        //    List<ListItem> controlName = new List<ListItem>();

        //    if (compList != null)
        //    {
        //        // Omvandla till listItem för dropdownlist
        //        foreach (components comp in compList)
        //        {
        //            orderNo.Add(new ListItem
        //            {
        //                Text = comp.OrderingNumber.ToString(),
        //                Value = comp.Id.ToString()
        //            });

        //            controlName.Add(new ListItem
        //            {
        //                Text = comp.controls.Name,
        //                Value = comp.Id.ToString()
        //            });
        //        }

        //        // Lägg in item i dropdownlist
        //        if (ddlOrderingNo != null && ddlComControl != null)
        //        {
        //            foreach (ListItem item in orderNo)
        //            {
        //                ddlOrderingNo.Items.Add(item);
        //            }

        //            foreach (ListItem item in controlName)
        //            {
        //                ddlComControl.Items.Add(item);
        //            }
        //        }
        //    }
        //}

        #endregion

        protected void GridViewComponentList_OnRowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridViewComponentList.EditIndex = -1;
            ShowComponentsInWebPage();
        }

        protected void GridViewComponentList_OnRowEditing(object sender, GridViewEditEventArgs e)
        {
            GridViewComponentList.EditIndex = e.NewEditIndex;
            ShowComponentsInWebPage();

            int index = GridViewComponentList.EditIndex;
            GridViewRow gvrow = GridViewComponentList.Rows[index];

            LinkButton editEventBtn = (LinkButton)gvrow.FindControl("LinkButtonEditEvent");
            LinkButton cancelEditBtn = (LinkButton)gvrow.FindControl("LinkButtonCancelEdit");
            LinkButton updateEventBtn = (LinkButton)gvrow.FindControl("LinkButtonUpdateEvent");

            editEventBtn.Visible = !editEventBtn.Visible;
            cancelEditBtn.Visible = !cancelEditBtn.Visible;
            updateEventBtn.Visible = !updateEventBtn.Visible;
        }

        protected void GridViewComponentList_OnRowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int index = GridViewComponentList.EditIndex;

            GridViewRow gvrow = GridViewComponentList.Rows[index];

            int id = int.Parse(GridViewComponentList.Rows[e.RowIndex].Cells[0].Text);
            int oNo = int.Parse(((TextBox)gvrow.Cells[1].Controls[1]).Text);
            int controlId = int.Parse(((DropDownList)gvrow.Cells[2].Controls[1]).SelectedValue);

            components compToUpdate = ComponentDB.GetComponentById(id);
            compToUpdate.OrderingNumber = oNo;
            compToUpdate.controls_Id = controlId;

            if (ComponentDB.UpdateComponent(compToUpdate) > 0)
            {
                LabelActionStatus.Text = string.Format("The {0} component was updated SUCCESSFULLY!!!! v~(^3^)V",
                    compToUpdate.controls.Name);
                LabelActionStatus.ForeColor = Color.CornflowerBlue;
                LabelActionStatus.BackColor = ColorTranslator.FromHtml("#217ebb");
            }
            else
            {
                LabelActionStatus.Text = string.Format("The {0} component was not updated. q(>3<)p",
                    compToUpdate.controls.Name);
                LabelActionStatus.ForeColor = Color.Red;
            }

            GridViewComponentList.EditIndex = -1;
            ShowComponentsInWebPage();
        }

        protected void GridViewComponentList_OnRowUpdated(object sender, GridViewUpdatedEventArgs e)
        {
            
        }

        protected void GridViewComponentList_OnRowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int id = int.Parse(GridViewComponentList.Rows[e.RowIndex].Cells[0].Text);

            if (ComponentDB.GetComponentById(id) != null )
            {
                if (ComponentDB.DeleteComponent(ComponentDB.GetComponentById(id)))
                {
                    LabelActionStatus.Text = string.Format("Congrats! The component was deleted successfully.");
                    LabelActionStatus.ForeColor = Color.CornflowerBlue;
                }
                else
                {
                    LabelActionStatus.Text = string.Format("Component could not be deleted. Please try again.");
                    LabelActionStatus.ForeColor = Color.Red;
                }
            }
            GridViewComponentList.EditIndex = -1;
            ShowComponentsInWebPage();

        }
    }
}