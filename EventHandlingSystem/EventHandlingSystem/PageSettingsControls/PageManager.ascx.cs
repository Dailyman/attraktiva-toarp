using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EventHandlingSystem.Database;

namespace EventHandlingSystem.PageSettingsControls
{
    public partial class PageManager : System.Web.UI.UserControl
    {
        private int _wpId;

        protected void Page_Load(object sender, EventArgs e)
        {
            // Reset ActionLabels
            ActionStatus.Text = "";
            ActionStatus.ForeColor = Color.Black;
            ActionStatusComponentsList.Text = "";
            ActionStatusComponentsList.ForeColor = Color.Black;
            ActionStatusFilterDataList.Text = "";
            ActionStatusFilterDataList.ForeColor = Color.Black;

            if (!IsPostBack)
            { 
                // Gets the ID from the QueryString
                var stId = Request.QueryString["Id"];

                // This is not needed right now
                //var stType = Request.QueryString["Type"];

                // If the ID from the QueryString is in a valid format its stored
                if (!string.IsNullOrWhiteSpace(stId) && int.TryParse(stId, out _wpId))
                {
                    HiddenFieldWebPageId.Value = _wpId.ToString();
                    webpages webPage = WebPageDB.GetWebPageById(_wpId);

                    Page.Title = webPage != null ? webPage.Title : "Unknown page";
                }

                if (WebPageDB.GetWebPageById(_wpId) != null)
                {
                    PopulateDropDownListControls();
                    DisplayCurrentWebPageProperties();
                    DisplayComponentsForWebPage();
                }
                else
                {
                    ActionStatus.Text = string.Format("No Webpage with that Id found");
                    PanelContent.Visible = false;
                }
            }
            

            if (!int.TryParse(HiddenFieldWebPageId.Value, out _wpId))
            {

                ActionStatus.Text = string.Format("The webpage Id could not be loaded");
                PanelContent.Visible = false;
            }
            else
            {
                PopulateDropDownListControls();
            }

        }

        private void ActivateDisplayView()
        {
            MultiViewWepPageManager.SetActiveView(DisplayView);
            
        }

        private void ActivateEditView()
        {
            MultiViewWepPageManager.SetActiveView(EditView);
        }

        private void DisplayCurrentWebPageProperties()
        {
            ActivateDisplayView();
            webpages currentWebPage = WebPageDB.GetWebPageById(_wpId);
            LabelWepPageTitle.Text = String.IsNullOrEmpty(currentWebPage.Title) ? "-" : string.Format("{0}",currentWebPage.Title);
            LabelWepPageLayout.Text = String.IsNullOrEmpty(currentWebPage.Layout) ? "-" : string.Format("{0}", currentWebPage.Layout);
            LabelWepPageStyle.Text = String.IsNullOrEmpty(currentWebPage.Style) ? "-" : string.Format("{0}", currentWebPage.Style);
            LabelWebPageLatestUpdate.Text = string.Format("{0}", currentWebPage.LatestUpdate);
            LabelWebPageUpdatedBy.Text = string.Format("{0}", currentWebPage.UpdatedBy);


        }

        private void DisplayEditCurrentWebPageProperties()
        {
            ActivateEditView();

            if (WebPageDB.GetWebPageById(_wpId) != null)
            {
                webpages currentWebPage = WebPageDB.GetWebPageById(_wpId);
                TxtBoxWepPageTitle.Text = currentWebPage.Title;
                TxtBoxWepPageLayout.Text = currentWebPage.Layout;
                TxtBoxWepPageStyle.Text = currentWebPage.Style;
            }

        }

        protected void LinkBtnEditWepPage_OnClick(object sender, EventArgs e)
        {
                DisplayEditCurrentWebPageProperties();
        }

        protected void BtnSaveChanges_OnClick(object sender, EventArgs e)
        {
            if (WebPageDB.GetWebPageById(_wpId) == null)
            {
                ActionStatus.Text = string.Format("The Webpage was not found! The webpage might have been deleted.");
                ActionStatus.ForeColor = Color.Red;
                return;
            }

            webpages currentWebPage = WebPageDB.GetWebPageById(_wpId);
            currentWebPage.Title = TxtBoxWepPageTitle.Text;
            currentWebPage.Layout = TxtBoxWepPageLayout.Text;
            currentWebPage.Style = TxtBoxWepPageStyle.Text;
            currentWebPage.UpdatedBy = HttpContext.Current.User.Identity.Name;

            if (WebPageDB.UpdateWebPage(currentWebPage) > 0)
            {
                ActionStatus.Text = string.Format("The Webpage was updated successfully.");
                ActionStatus.ForeColor = Color.CornflowerBlue;
            }
            else
            {
                ActionStatus.Text = string.Format("The Webpage was not updated!");
                ActionStatus.ForeColor = Color.Red;
            }
            DisplayCurrentWebPageProperties();
        }

        protected void BtnCancelChanges_OnClick(object sender, EventArgs e)
        {
            ActionStatus.Text = string.Format("Changes was cancelled.");
            ActionStatus.ForeColor = Color.Black;
            DisplayCurrentWebPageProperties();
        }






        #region ComponentList methods

        // Add Controls in the Dropdownlist
        public void PopulateDropDownListControls()
        {
            ddlAddComControls.Items.Clear();

            var controlList =
                ControlDB.GetAllControlsNotInWebpage(WebPageDB.GetWebPageById(int.Parse(HiddenFieldWebPageId.Value)))
                    .Select(c => new ListItem
                    {
                        Text = c.Name,
                        Value = c.Id.ToString()
                    }).ToList();

            foreach (ListItem item in controlList.OrderBy(i => i.Text))
            {
                ddlAddComControls.Items.Add(item);
            }

            AddControl.Enabled = ddlAddComControls.Items.Count != 0;
        }

        public List<ListItem> GetAllControlsListItems()
        {
            return ControlDB.GetAllControls().Select(c => new ListItem
            {
                Text = c.Name,
                Value = c.Id.ToString()
            }).OrderBy(i => i.Text).ToList();
        }


        
        protected void DisplayComponentsForWebPage()
        {
            List<Tuple<int, string, string, int>> componentDataList = new List<Tuple<int, string, string, int>>();
            int webPageId;

            if (int.TryParse(HiddenFieldWebPageId.Value, out webPageId))
            {
                List<components> componentList = ComponentDB.GetComponentsByWebPageId(webPageId).
                 OrderBy(c => c.OrderingNumber)
                     .ThenBy(c => c.controls.Name)
                     .ToList();

                //if (componentList.Count == 0)
                //{
                //    ActionStatusComponentsList.Text = "No components in webpage";
                //}
                //else
                //{
                    //ActionStatusComponentsList.Text = "Components";

                    foreach (var comp in componentList)
                    {
                        var tupdata = new Tuple<int, string, string, int>
                            (comp.Id, comp.OrderingNumber.ToString(), comp.controls.Name, comp.controls_Id);
                        componentDataList.Add(tupdata);
                    }
                //}
            }

            GridViewComponentList.DataSource = componentDataList;
            GridViewComponentList.DataBind();

            DisplayFilterDataForComponent();
        }



        protected void GridViewComponentList_OnRowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridViewComponentList.EditIndex = -1;
            DisplayComponentsForWebPage();
        }

        protected void GridViewComponentList_OnRowEditing(object sender, GridViewEditEventArgs e)
        {
            GridViewComponentList.EditIndex = e.NewEditIndex;
            GridViewComponentList.SelectedIndex = -1; 
            DisplayComponentsForWebPage();

            int index = GridViewComponentList.EditIndex;
            GridViewRow gvrow = GridViewComponentList.Rows[index];

            LinkButton selectComponentBtn = (LinkButton)gvrow.FindControl("LinkButtonSelectComponent");
            LinkButton editComponentBtn = (LinkButton)gvrow.FindControl("LinkButtonEditComponent");
            LinkButton cancelEditBtn = (LinkButton)gvrow.FindControl("LinkButtonCancelEdit");
            LinkButton updateComponentBtn = (LinkButton)gvrow.FindControl("LinkButtonUpdateComponent");
            LinkButton deleteComponentBtn = (LinkButton)gvrow.FindControl("LinkButtonDeleteComponent");

            selectComponentBtn.Visible = !selectComponentBtn.Visible;
            editComponentBtn.Visible = !editComponentBtn.Visible;
            cancelEditBtn.Visible = !cancelEditBtn.Visible;
            updateComponentBtn.Visible = !updateComponentBtn.Visible;
            deleteComponentBtn.Visible = !deleteComponentBtn.Visible;
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
            if (ControlDB.GetControlsById(controlId) != null)
            {
                //foreach (var com in WebPageDB.GetWebPageById(int.Parse(HiddenFieldWebPageId.Value)).components)
                //{
                //    if (com.controls.Id == controlId)
                //    {
                        
                //    }
                //}
                 compToUpdate.controls_Id = controlId;
            }
            else
            {
                ActionStatusComponentsList.Text = "No Control By that Id was found";
                ActionStatusComponentsList.ForeColor = Color.Red;
                DisplayComponentsForWebPage();
                return;
            }

            int affectedRows = ComponentDB.UpdateComponent(compToUpdate);

            foreach (var fd in compToUpdate.filterdata.Where(fd => !fd.IsDeleted))
            {
                if (FilterDataDB.DeleteFilterData(fd))
                {
                    ActionStatus.Text = " " + fd.Type + "=deleted";
                }
            }

            controls c = ControlDB.GetControlsById(compToUpdate.controls_Id);
            var filterTypeNameList = new List<string>();
            if (c != null)
            {
                //EventHandlingSystem.Components.
                Type cls = Type.GetType("EventHandlingSystem.Components." + c.Name);
                if (cls != null)
                {
                    foreach (var prop in cls.GetProperties())
                    {
                        filterTypeNameList.Add(prop.Name);
                    }
                }
            }
            ActionStatusComponentsList.Text += "<br/>FilterData added:";
            foreach (var type in filterTypeNameList)
            {

                filterdata newFilterData = new filterdata { Type = type, Components_Id = compToUpdate.Id, Data = "" };
                if (FilterDataDB.AddFilterData(newFilterData))
                {
                    ActionStatusComponentsList.Text += " " + newFilterData.Type + "=\"" + newFilterData.Data + "\"";
                }
            }
                

            

            if (affectedRows > 0)
            {
                ActionStatusComponentsList.Text =
                    string.Format("The {0} component was updated SUCCESSFULLY!!!! v~(^3^)V",
                        compToUpdate.controls.Name);
                ActionStatusComponentsList.ForeColor = Color.CornflowerBlue;
            }
            else
            {
                ActionStatusComponentsList.Text = string.Format("The {0} component was not updated. q(>3<)p",
                    compToUpdate.controls.Name);
                ActionStatusComponentsList.ForeColor = Color.Red;
            }

            GridViewComponentList.EditIndex = -1;
            DisplayComponentsForWebPage();
            PopulateDropDownListControls();
        }

        protected void GridViewComponentList_OnRowUpdated(object sender, GridViewUpdatedEventArgs e)
        {
            DisplayComponentsForWebPage();
        }

        protected void GridViewComponentList_OnRowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int id = int.Parse(GridViewComponentList.Rows[e.RowIndex].Cells[0].Text);

            if (ComponentDB.GetComponentById(id) != null)
            {
                if (ComponentDB.DeleteComponent(ComponentDB.GetComponentById(id)))
                {
                    ActionStatusComponentsList.Text = string.Format("Congrats! The component was deleted successfully.");
                    ActionStatusComponentsList.ForeColor = Color.CornflowerBlue;
                }
                else
                {
                    ActionStatusComponentsList.Text = string.Format("Component could not be deleted. Please try again.");
                    ActionStatusComponentsList.ForeColor = Color.Red;
                }
            }
            GridViewComponentList.EditIndex = -1;
            DisplayComponentsForWebPage();
            PopulateDropDownListControls();
        }

        protected void GridViewComponentList_OnSelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            GridViewComponentList.EditIndex = -1;
            DisplayComponentsForWebPage();
            DisplayFilterDataForComponent();

            if (GridViewComponentList.SelectedRow != null)
            {
                if (GridViewComponentList.SelectedIndex == e.NewSelectedIndex)
                {
                    e.Cancel = true;
                    GridViewComponentList.SelectedIndex = -1;
                    DisplayFilterDataForComponent();
                }
            }

        }

        protected void GridViewComponentList_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            //DisplayComponentsForWebPage();
            DisplayFilterDataForComponent();

            //GridViewComponentList.SelectedIndex = -1;

        }

        protected void AddControl_OnClick(object sender, EventArgs e)
        {
            components newComponent = new components();

            int oNo;
            newComponent.webpages_Id = int.Parse(HiddenFieldWebPageId.Value);
            newComponent.OrderingNumber = int.TryParse(tbAddOrderingNumber.Text, out oNo) ? oNo : 1;
            newComponent.controls_Id = int.Parse(ddlAddComControls.SelectedValue);

            if (ComponentDB.AddComponent(newComponent))
            {
                ActionStatusComponentsList.Text = "New component has successfully been added!";
                ActionStatusComponentsList.ForeColor = Color.CornflowerBlue;


                controls c = ControlDB.GetControlsById(newComponent.controls_Id);
                var filterTypeNameList = new List<string>();
                if (c != null)
                {
                    //EventHandlingSystem.Components.
                    Type cls = Type.GetType("EventHandlingSystem.Components."+c.Name);
                    if (cls != null)
                    {
                        foreach (var prop in cls.GetProperties())
                        {
                            filterTypeNameList.Add(prop.Name);
                        }
                    }
                }
                ActionStatusComponentsList.Text += "<br/>FilterData added:";
                foreach (var type in filterTypeNameList)
                {
                    
                    filterdata newFilterData = new filterdata {Type = type, Components_Id = newComponent.Id, Data = ""};
                    if (FilterDataDB.AddFilterData(newFilterData))
                    {
                        ActionStatusComponentsList.Text += " " + newFilterData.Type + "=\""+ newFilterData.Data+"\"";
                    }
                }
            }
            else
            {
                ActionStatusComponentsList.Text = "Sorry! New component was not added.";
                ActionStatusComponentsList.ForeColor = Color.Red;
            }
            PopulateDropDownListControls();
            DisplayComponentsForWebPage();
        } 
        
        #endregion








        #region GridViewFilterData

        private GridViewRow GetCurrentRowOfComponentSelectedOrEditing()
        {
            GridViewRow gvRow;

            int editIndex = GridViewComponentList.EditIndex;
            int selectIndex = GridViewComponentList.SelectedIndex;

            if (editIndex > -1)
            {
                gvRow = GridViewComponentList.Rows[editIndex];
            }
            else if (selectIndex > -1)
            {
                gvRow = GridViewComponentList.Rows[selectIndex];
            }
            else
            {
                gvRow = null;
            }
            return gvRow;
        }

        protected void DisplayFilterDataForComponent()
        {
            GridViewRow gvRow = GetCurrentRowOfComponentSelectedOrEditing();

            if (gvRow != null)
            {
                int id = int.TryParse(gvRow.Cells[0].Text, out id) ? id : -1;
                //int controlId = int.Parse(((DropDownList)gvRow.Cells[2].Controls[1]).SelectedValue);

                components currentComp = ComponentDB.GetComponentById(id);

                if (currentComp != null)
                {
                    
                        GridViewFilterData.DataSource = currentComp.filterdata.Where(fd => !fd.IsDeleted);
                        GridViewFilterData.DataBind();
                        ActionStatusFilterDataList.Text = (currentComp.filterdata.Any(fd => !fd.IsDeleted) ? "FilterData for " + currentComp.controls.Name : "No FilterData");
                   }
            }
            else
            {
                GridViewFilterData.DataSource = null;
                GridViewFilterData.DataBind();
                ActionStatusFilterDataList.Text = "No Component is selected";
            }
        }

        protected void GridViewFilterData_OnRowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridViewFilterData.EditIndex = -1;
            DisplayFilterDataForComponent();
        }

        protected void GridViewFilterData_OnRowEditing(object sender, GridViewEditEventArgs e)
        {
            GridViewFilterData.EditIndex = e.NewEditIndex;
            DisplayFilterDataForComponent();

            GridViewRow gvRow = GridViewFilterData.Rows[e.NewEditIndex];

            if (gvRow != null)
            {
                LinkButton editFilterDataBtn = (LinkButton) gvRow.FindControl("LinkButtonEditFilterData");
                LinkButton updateFilterDataBtn = (LinkButton) gvRow.FindControl("LinkButtonUpdateFilterData");
                LinkButton cancelEditBtn = (LinkButton) gvRow.FindControl("LinkButtonCancelEdit");
                //LinkButton deleteEventBtn = (LinkButton)gvRow.FindControl("LinkButtonDeleteFilterData");

                editFilterDataBtn.Visible = !editFilterDataBtn.Visible;
                updateFilterDataBtn.Visible = !updateFilterDataBtn.Visible;
                cancelEditBtn.Visible = !cancelEditBtn.Visible;
            }
        }

        protected void GridViewFilterData_OnRowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int index = GridViewFilterData.EditIndex;

            GridViewRow gvrow = GridViewFilterData.Rows[index];

            int id = int.Parse(GridViewFilterData.Rows[e.RowIndex].Cells[0].Text);
            string data = ((TextBox)gvrow.Cells[2].Controls[1]).Text;

            filterdata fdToUpdate = FilterDataDB.GetFilterDataById(id);

            if (fdToUpdate != null)
            {
                fdToUpdate.Data = data;
            }
            else
            {
                ActionStatusFilterDataList.Text = "No FilterData By that Id was found";
                ActionStatusFilterDataList.ForeColor = Color.Red;
                DisplayFilterDataForComponent();
                return;
            }

            int affectedRows = FilterDataDB.UpdateFilterData(fdToUpdate);

            if (affectedRows > 0)
            {
                ActionStatusFilterDataList.Text =
                    string.Format("FilterData with by type {0} was updated successfully.",
                        fdToUpdate.Type);
                ActionStatusFilterDataList.ForeColor = Color.CornflowerBlue;
            }
            else
            {
                ActionStatusFilterDataList.Text = string.Format("FilterData with by type {0} was not updated.",
                    fdToUpdate.Type);
                ActionStatusFilterDataList.ForeColor = Color.Red;
            }
            GridViewFilterData.EditIndex = -1;
            DisplayFilterDataForComponent();
        }

        protected void GridViewFilterData_OnRowUpdated(object sender, GridViewUpdatedEventArgs e)
        {
            DisplayFilterDataForComponent();
        }

        protected void GridViewFilterData_OnRowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            // YOU SHOULD NOT BE ABLE TO DELETE FILTERDATA! >:O Just change it <3
        }
        
        protected void GridViewFilterData_OnSelectedIndexChanged(object sender, EventArgs e)
        {
                
        }

        #endregion

        
    }
}