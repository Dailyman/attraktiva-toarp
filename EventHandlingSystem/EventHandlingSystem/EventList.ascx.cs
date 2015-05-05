using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using EventHandlingSystem.Database;

namespace EventHandlingSystem
{
    public partial class EventList : System.Web.UI.UserControl
    {
        private int _id;
        private string _title;
        private string _location;
        private DateTime _sDate;
        private DateTime _eDate;

        public string AssociationId { get; set; }
        public string CommunityId { get; set; }

        public EventList()
        {
            this.AssociationId = "";
            this.CommunityId = "";
        }

        public EventList(string aId, string cId)
        {
            this.AssociationId = aId;
            this.CommunityId = cId;
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            ActionStatus.Text = string.Empty;

            if (!IsPostBack)
            {
                PopulateDropDownComm();
                PopulateDropDownAsso();
                PopulateDropDownCat();
                PopulateDropDownSubCat();

                if (Request.QueryString["Type"] != null)
                {
                    if (Request.QueryString["Type"].Equals("c", StringComparison.OrdinalIgnoreCase))
                    {
                        GetCommunityFromQueryStringsAndSelectInDropDown();
                    }
                    else if (Request.QueryString["Type"].Equals("a", StringComparison.OrdinalIgnoreCase))
                    {
                        RenderEventList(GetAssociationFromQueryStringAndSelectInDropDown());
                    }
                }
                else
                {
                    RenderEventList();
                }
            }

            FilterEventAndRender();
        }

        public communities GetCommunityFromQueryStringsAndSelectInDropDown()
        {
            communities comm = new communities();
            //Hämtar WebPageId från URL.
            var stId = Request.QueryString["Id"];
            int id;
            //Om Id värdet som tas från URLn är i giltigt format hämtas WebPage objektet och visas på sidan.
            if (!string.IsNullOrWhiteSpace(stId) && int.TryParse(stId, out id))
            {
                webpages webPage = WebPageDB.GetWebPageById(id);
                if (webPage != null)
                {
                    if (webPage.CommunityId != null)
                    {
                        comm = CommunityDB.GetCommunityById(webPage.CommunityId.GetValueOrDefault());
                        if (comm != null)
                        {
                            DropDownListComm.SelectedIndex =
                                DropDownListComm.Items.IndexOf(
                                    DropDownListComm.Items.FindByValue(
                                        comm.Id.ToString()));
                        }
                        else
                        {
                            DropDownListComm.SelectedIndex = DropDownListComm.Items.IndexOf(
                                DropDownListComm.Items.FindByValue(""));
                        }
                    }
                }
            }
            return comm;
        }

        public associations GetAssociationFromQueryStringAndSelectInDropDown()
        {
            var asso = new associations();
            //Hämtar WebPageId från URL.
            var stId = Request.QueryString["Id"];
            int id;
            //Om Id värdet som tas från URLn är i giltigt format hämtas WebPage objektet och visas på sidan.
            if (!string.IsNullOrWhiteSpace(stId) && int.TryParse(stId, out id))
            {
                webpages webPage = WebPageDB.GetWebPageById(id);
                if (webPage != null)
                {
                    if (webPage.AssociationId != null)
                    {
                        asso = AssociationDB.GetAssociationById(webPage.AssociationId.GetValueOrDefault());
                        if (asso != null)
                        {
                            DropDownListAsso.SelectedIndex =
                                DropDownListAsso.Items.IndexOf(
                                    DropDownListAsso.Items.FindByValue(
                                        asso.Id.ToString()));
                        }
                        else
                        {
                            DropDownListAsso.SelectedIndex = DropDownListAsso.Items.IndexOf(
                                DropDownListAsso.Items.FindByValue(""));
                        }
                    }
                }
            }
            return asso;
        }

        // Old rendering methods.
        public List<events> RenderEventList()
        {
            List<events> eventList = EventDB.GetAllEvents();

            GridViewEventList.DataSource = eventList.OrderBy(e => e.StartDate);
            GridViewEventList.DataBind();
            return eventList;
        }
        public List<events> RenderEventList(DateTime sDate)
        {
            List<events> eventList = EventDB.GetEventsFromSpecifiedStartDate(sDate.Date);

            GridViewEventList.DataSource = eventList.OrderBy(e => e.StartDate);
            GridViewEventList.DataBind();
            return eventList;

        }
        public List<events> RenderEventList(DateTime sDate, DateTime eDate)
        {
            List<events> eventList = EventDB.GetEventsByRangeDate(sDate, eDate);

            GridViewEventList.DataSource = eventList.OrderBy(e => e.StartDate);
            GridViewEventList.DataBind();

            return eventList;

        }
        public List<events> RenderEventList(DateTime sDate, DateTime eDate, associations asso)
        {
            List<events> eventList = new List<events>();
            if (asso != null)
            {
                foreach (events e in EventDB.GetEventsByRangeDate(sDate, eDate))
                {
                    foreach (associations a in e.associations)
                    {
                        if (a.Id == asso.Id)
                        {
                            eventList.Add(e);
                            break;
                        }
                    }
                }

                GridViewEventList.DataSource = eventList.OrderBy(e => e.StartDate);
                GridViewEventList.DataBind();
                return eventList;
            }
            else
            {
                return RenderEventList();
            }


        }
        public List<events> RenderEventList(DateTime sDate, DateTime eDate, associations asso, string searchStr)
        {
            List<events> eventList = new List<events>();

            if (asso != null)
            {
                foreach (events e in EventDB.GetEventsBySearchWord(searchStr))
                {
                    foreach (associations a in e.associations)
                    {
                        if (a.Id == asso.Id)
                        {
                            eventList.Add(e);
                            break;
                        }
                    }
                }


                GridViewEventList.DataSource =
                    eventList.Where(e => e.StartDate > sDate && e.EndDate < eDate).OrderBy(e => e.StartDate);
                GridViewEventList.DataBind();

                return eventList;
            }
            else
            {
                return RenderEventList();
            }
        }
        public List<events> RenderEventList(string searchStr)
        {
            List<events> eventList = EventDB.GetEventsBySearchWord(searchStr);

            GridViewEventList.DataSource = eventList.OrderBy(e => e.StartDate);
            GridViewEventList.DataBind();

            return eventList;

        }
        // Still in use vvv (optimize/merge)
        public List<events> RenderEventList(communities comm)
        {
            List<events> eventList = new List<events>();
            if (comm != null)
            {
                eventList = EventDB.GetEventsByCommunity(comm).Where(e => !e.IsDeleted).ToList();

                GridViewEventList.DataSource = eventList.OrderBy(e => e.StartDate);
                GridViewEventList.DataBind();

                return eventList;
            }
                return RenderEventList();

        }
        public List<events> RenderEventList(associations asso)
        {
            List<events> eventList = new List<events>();
            if (asso != null)
            {
                eventList = EventDB.GetEventsByAssociation(asso).Where(e => !e.IsDeleted).ToList();

                GridViewEventList.DataSource = eventList.OrderBy(e => e.StartDate);
                GridViewEventList.DataBind();

                return eventList;
            }
            return RenderEventList();
        }
        // Still in use ^^^
       

        public List<events> RenderEventList(associations asso, string searchStr)
        {
            List<events> eventList = new List<events>();

            if (asso != null)
            {
                foreach (events e in EventDB.GetEventsBySearchWord(searchStr))
                {
                    foreach (associations a in e.associations)
                    {
                        if (a.Id == asso.Id)
                        {
                            eventList.Add(e);
                            break;
                        }
                    }
                }

                GridViewEventList.DataSource = eventList.OrderBy(e => e.StartDate);
                GridViewEventList.DataBind();

                return eventList;
            }
            else
            {
                return RenderEventList();
            }
        }


        // This is the best method for filtering and rendering.
        public List<events> FilterEventAndRender()
        {

            DateTime sDate;
            DateTime eDate;
            int cId;
            int aId;
            int catId;
            int subCatId;

            DateTime? sT = DateTime.TryParse(TxtStart.Text, out sDate) ? sDate : (DateTime?)null;
            DateTime? eT = DateTime.TryParse(TxtEnd.Text, out eDate) ? eDate : (DateTime?)null;
            communities c = int.TryParse(DropDownListComm.SelectedValue, out cId) ? CommunityDB.GetCommunityById(cId) : null;
            associations a = int.TryParse(DropDownListAsso.SelectedValue, out aId) ? AssociationDB.GetAssociationById(aId) : null;
            categories cat = int.TryParse(DropDownListCat.SelectedValue, out catId) ? CategoryDB.GetCategoryById(catId) : null;
            subcategories subCat = int.TryParse(DropDownListSubCat.SelectedValue, out subCatId) ? SubCategoryDB.GetSubCategoryById(subCatId) : null;
            string searchWord = !String.IsNullOrWhiteSpace(TxtSearch.Text) ? TxtSearch.Text : null;

            List<events> eventList = EventDB.FilterEvents(sT, eT, c, a, cat, subCat, searchWord);

            GridViewEventList.DataSource = eventList.OrderBy(e => e.StartDate);
            GridViewEventList.DataBind();

            return eventList;
        }

        protected void BtnFilter_OnClick(object sender, EventArgs e)
        {
            FilterEventAndRender();
        }



        public void PopulateDropDownComm(List<communities> cList = null)
        {
            List<communities> communitiesToAddInDropDown = cList ?? CommunityDB.GetAllCommunities();

            //Lägger till alla communities i dropdownboxen.
            DropDownListComm.Items.Clear();
            DropDownListComm.Items.Add((communitiesToAddInDropDown.Count == 0 ? new ListItem("", "") : new ListItem("All", "")));
            foreach (var community in communitiesToAddInDropDown.OrderBy(c => c.Name))
            {
                 DropDownListComm.Items.Add(new ListItem
                {
                    Text = community.Name,
                    Value = community.Id.ToString()
                });
            }
        }

        public void PopulateDropDownAsso(List<associations> aList = null)
        {
            List<associations> associationsToAddInDropDown = aList ?? AssociationDB.GetAllAssociations();

            //Lägger till alla communities i dropdownboxen.
            DropDownListAsso.Items.Clear();
            DropDownListAsso.Items.Add((associationsToAddInDropDown.Count == 0 ? new ListItem("", "") : new ListItem("All", "")));
            foreach (var association in associationsToAddInDropDown.OrderBy(a => a.Name))
            {
                DropDownListAsso.Items.Add(new ListItem
                {
                    Text = association.Name,
                    Value = association.Id.ToString()
                });
            }
        }

        public void PopulateDropDownCat(List<categories> catList = null)
        {
            List<categories> categoriesToAddInDropDown = catList ?? CategoryDB.GetAllCategories();

            //Lägger till alla categories i dropdownboxen.
            DropDownListCat.Items.Clear();
            DropDownListCat.Items.Add((categoriesToAddInDropDown.Count == 0 ? new ListItem("", "") : new ListItem("All", "")));
            foreach (var category in categoriesToAddInDropDown.OrderBy(cat => cat.Name))
            {
                DropDownListCat.Items.Add(new ListItem
                {
                    Text = category.Name,
                    Value = category.Id.ToString()
                });
            }
        }

        public void PopulateDropDownSubCat(List<subcategories> subCatList = null )
        {
            List<subcategories> subCategoriesToAddInDropDown = subCatList ?? SubCategoryDB.GetAllSubCategories();

            //Lägger till alla subcategories i dropdownboxen.
            DropDownListSubCat.Items.Clear();
            DropDownListSubCat.Items.Add((subCategoriesToAddInDropDown.Count == 0 ? new ListItem("", "") : new ListItem("All", "")));
            foreach (var subCategory in subCategoriesToAddInDropDown.OrderBy(subC => subC.Name))
            {
                DropDownListSubCat.Items.Add(new ListItem
                {
                    Text = subCategory.Name,
                    Value = subCategory.Id.ToString()
                });
            }
        }



        public string WriteAllAssociations(ICollection<associations> list)
        {
            string result = "";
            foreach (var association in list)
            {
                result += association.Name + ", ";
            }
            if (String.IsNullOrEmpty(result))
            {
                return result;
            }
            else
            {
                return result.TrimEnd(',',' ');
            }
        }



        protected void CustomValiStartDate_OnServerValidate(object source, ServerValidateEventArgs args)
        {
            //Validerar om texten i StartDateTextBoxen är ett giltig datum. 
            DateTime result;
            args.IsValid = !string.IsNullOrWhiteSpace(TxtStart.Text) &&
                           DateTime.TryParse(TxtStart.Text, out result);
        }

        protected void CustomValiEndDate_OnServerValidate(object source, ServerValidateEventArgs args)
        {
            //Validerar om texten i EndDateTextBoxen är ett giltig datum. 
            DateTime result;
            args.IsValid = !string.IsNullOrWhiteSpace(TxtEnd.Text) &&
                           DateTime.TryParse(TxtEnd.Text, out result);
        }


        //private List<communities> FilterDropDownCommItems(associations a)
        //{

        //}
        private List<associations> FilterDropDownAssoItems(communities c)
        {
            PopulateDropDownAsso(AssociationDB.GetAllAssociationsInCommunity(c));
            return AssociationDB.GetAllAssociationsInCommunity(c).ToList();
        }
        private List<categories> FilterDropDownCatItems(associations a)
        {
            PopulateDropDownCat(a.categories.ToList());
            return a.categories.ToList();
        }
        private List<subcategories> FilterDropDownSubCatItems(categories cat)
        {
            PopulateDropDownSubCat(SubCategoryDB.GetAllSubCategoryByCategory(cat));
            return SubCategoryDB.GetAllSubCategoryByCategory(cat).ToList();
        }


        protected void DropDownListComm_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            // Kanske vi inte ska filtrera?

            int cId;
            if (int.TryParse(DropDownListComm.SelectedValue, out cId) && CommunityDB.GetCommunityById(cId) != null)
            {
                FilterDropDownAssoItems(CommunityDB.GetCommunityById(cId));
            }
            else
            {
                PopulateDropDownAsso();
            }
        }

        protected void DropDownListAsso_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            // Kanske vi inte ska filtrera?

            //int aId;
            //if (int.TryParse(DropDownListAsso.SelectedValue, out aId) && AssociationDB.GetAssociationById(aId) != null)
            //{
            //    FilterDropDownCatItems(AssociationDB.GetAssociationById(aId));
            //}
            //else
            //{
            //    PopulateDropDownCat();
            //}
        }

        protected void DropDownListCat_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            // Kanske vi inte ska filtrera?

            int catId;
            if (int.TryParse(DropDownListCat.SelectedValue, out catId) && CategoryDB.GetCategoryById(catId) != null)
            {
                FilterDropDownSubCatItems(CategoryDB.GetCategoryById(catId));
            }
            else
            {
                PopulateDropDownSubCat();
            }
        }


        protected void GridViewEventList_OnRowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridViewEventList.EditIndex = -1;
            FilterEventAndRender();
        }

        protected void GridViewEventList_OnRowEditing(object sender, GridViewEditEventArgs e)
        {
            GridViewEventList.EditIndex = e.NewEditIndex;
            FilterEventAndRender();
        }

        protected void GridViewEventList_OnRowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            // BUG WHEN UPDATING PAGE (F5) AFTER DELETING SOMETHING, WILL DELETE THE NEXT EVENT IN THE LIST WITHOUT WARNING!!!
            int.TryParse(GridViewEventList.Rows[e.RowIndex].Cells[0].Text, out _id);
            
            events eventToDelete = EventDB.GetEventById(_id);
            if (eventToDelete != null)
            {
                ActionStatus.Text = EventDB.DeleteEvent(eventToDelete)
                    ? "Event was deleted successfully"
                    : "Event could not be deleted";
            }
            else
            {
                ActionStatus.Text = "The selected event could not be found";
            }
            FilterEventAndRender();

        }

        protected void GridViewEventList_OnRowUpdating(object sender, GridViewUpdateEventArgs e)
        {


            int index = GridViewEventList.EditIndex;
            GridViewRow gvrow = GridViewEventList.Rows[index];
            // _id = vvv
            int.TryParse(GridViewEventList.Rows[e.RowIndex].Cells[0].Text, out _id);
            //StartDate
            string sd = ((TextBox) gvrow.Cells[1].Controls[1]).Text;
            _sDate = DateTime.Parse(sd,
                System.Globalization.CultureInfo.CurrentCulture,
                System.Globalization.DateTimeStyles.None);
            //EndDate
            string ed = ((TextBox) gvrow.Cells[2].Controls[1]).Text;
            _eDate = DateTime.Parse(ed,
                System.Globalization.CultureInfo.CurrentCulture,
                System.Globalization.DateTimeStyles.None);
            _title = ((TextBox) gvrow.Cells[3].Controls[0]).Text;
            _location = ((TextBox) gvrow.Cells[4].Controls[0]).Text;

            //_email = ((TextBox)gvrow.Cells[2].Controls[0]).Text.Trim();
            //_comment = ((TextBox)gvrow.Cells[3].Controls[0]).Text;
            //_isapproved = ((CheckBox)gvrow.Cells[4].Controls[0]).Checked;

            events eventToUpdate = EventDB.GetEventById(_id);

            if (eventToUpdate != null)
            {
                eventToUpdate.Title = _title;
                eventToUpdate.Location = _location;
                eventToUpdate.StartDate = _sDate;
                eventToUpdate.EndDate = ed.Length > 10 ? _eDate : _eDate.Add(new TimeSpan(23, 59, 59));
                if (EventDB.UpdateEvent(eventToUpdate) > 0)
                {
                    ActionStatus.Text = "Event was updated Successfully";
                    ActionStatus.ForeColor = Color.CornflowerBlue;
                }
                else
                {
                    ActionStatus.Text = "Event could not be updated Successfully";
                    ActionStatus.ForeColor = Color.Red;
                }
            }
            else
            {
                ActionStatus.Text = "Event could not be updated because it does not exist";
                ActionStatus.ForeColor = Color.Red;
            }
            GridViewEventList.EditIndex = -1;
            FilterEventAndRender();
        }

        protected void GridViewEventList_OnRowUpdated(object sender, GridViewUpdatedEventArgs e)
        {
           foreach (events ev in EventDB.GetAllEvents())
            {
                EventDB.UpdateEvent(ev);
            }
            FilterEventAndRender();
        }

        protected void GridViewEventList_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (GridViewEventList.EditIndex == e.Row.RowIndex)
                {
                    TextBox tStart = (TextBox)e.Row.FindControl("TextBoxStartDate");
                    TextBox tEnd = (TextBox)e.Row.FindControl("TextBoxEndDate");
                    DateTime dt1, dt2;
                    if (DateTime.TryParse(tStart.Text, out dt1))
                    {
                        tStart.Text = dt1.ToString(tStart.Text.Length > 10 ? "yyyy-MM-dd'T'HH:mm:ss" : "yyyy-MM-dd");
                    }
                    if (DateTime.TryParse(tEnd.Text, out dt2))
                    {
                        tEnd.Text = dt2.ToString(tEnd.Text.Length > 10 ? "yyyy-MM-dd'T'HH:mm:ss" : "yyyy-MM-dd");

                        //tEnd.Text = dt2.ToString("yyyy-MM-dd'T'HH:mm:ss");
                    }
                }
            }
        }
    }
}