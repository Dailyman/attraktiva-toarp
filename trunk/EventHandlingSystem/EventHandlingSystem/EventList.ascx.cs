using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EventHandlingSystem.Database;

namespace EventHandlingSystem
{
    public partial class EventList : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PopulateDropDownAsso();

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
                        //comm = CommunityDB.GetCommunityById(webPage.CommunityId);
                        //if (comm != null)
                        //{
                        //    DropDownListComm.SelectedIndex =
                        //        DropDownListComm.Items.IndexOf(
                        //            DropDownListComm.Items.FindByValue(
                        //                comm.Id.ToString()));
                        //}
                        //else
                        //{
                        //    DropDownListComm.SelectedIndex = DropDownListComm.Items.IndexOf(
                        //        DropDownListComm.Items.FindByValue(""));
                        //}
                    }
                }
            }
            return comm;
        }

        public associations GetAssociationFromQueryStringAndSelectInDropDown()
        {
            associations asso = new associations();
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
                        asso = AssociationDB.GetAssociationById((int) webPage.AssociationId);
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

        protected void Page_PreRender(object sender, EventArgs e)
        {
            RepeaterEvents.Visible = RepeaterEvents.Items.Count != 0;
            LabelNoData.Visible = RepeaterEvents.Items.Count == 0;
        }

        
        public List<events> RenderEventList()
        {
            List<events> eventList = EventDB.GetAllEvents();

            RepeaterEvents.DataSource = eventList.OrderBy(e => e.StartDate);
            RepeaterEvents.DataBind();
            return eventList;

        }

        public List<events> RenderEventList(DateTime sDate)
        {
            List<events> eventList = EventDB.GetEventsFromSpecifiedStartDate(sDate.Date);

            RepeaterEvents.DataSource = eventList.OrderBy(e => e.StartDate);
            RepeaterEvents.DataBind();
            return eventList;

        }

        public List<events> RenderEventList(DateTime sDate, DateTime eDate)
        {
            List<events> eventList = EventDB.GetEventsByRangeDate(sDate, eDate);

            RepeaterEvents.DataSource = eventList.OrderBy(e => e.StartDate);
            RepeaterEvents.DataBind();

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

                RepeaterEvents.DataSource = eventList.OrderBy(e => e.StartDate);
                RepeaterEvents.DataBind();
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


                RepeaterEvents.DataSource =
                    eventList.Where(e => e.StartDate > sDate && e.EndDate < eDate).OrderBy(e => e.StartDate);
                RepeaterEvents.DataBind();

                return eventList;
            }
            else
            {
                return RenderEventList();
            }
        }

        public List<events> RenderEventList(associations asso)
        {
            List<events> eventList = new List<events>();
            if (asso != null)
            {
                eventList = EventDB.GetEventsByAssociation(asso);

                RepeaterEvents.DataSource = eventList.OrderBy(e => e.StartDate);
                RepeaterEvents.DataBind();

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

            RepeaterEvents.DataSource = eventList.OrderBy(e => e.StartDate);
            RepeaterEvents.DataBind();

            return eventList;

        }

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

                RepeaterEvents.DataSource = eventList.OrderBy(e => e.StartDate);
                RepeaterEvents.DataBind();

                return eventList;
            }
            else
            {
                return RenderEventList();
            }
        }



        protected void BtnFilter_OnClick(object sender, EventArgs e)
        {
            int aId;
            DateTime sDate;
            DateTime eDate;
            if (!String.IsNullOrWhiteSpace(TxtSearch.Text))
            {

                if (!String.IsNullOrWhiteSpace(DropDownListAsso.SelectedValue) &&
                    int.TryParse(DropDownListAsso.SelectedValue, out aId) && !String.IsNullOrWhiteSpace(TxtStart.Text) &&
                    !String.IsNullOrWhiteSpace(TxtEnd.Text) && DateTime.TryParse(TxtStart.Text, out sDate) &&
                    DateTime.TryParse(TxtEnd.Text, out eDate))
                {
                    RenderEventList(sDate, eDate,
                        AssociationDB.GetAssociationById(aId), TxtSearch.Text);
                }
                else if (!String.IsNullOrWhiteSpace(DropDownListAsso.SelectedValue) &&
                         int.TryParse(DropDownListAsso.SelectedValue, out aId))
                {
                    RenderEventList(AssociationDB.GetAssociationById(aId), TxtSearch.Text);
                }
                else
                {
                    RenderEventList(TxtSearch.Text);
                    return;
                }
                return;
            }




            if (!String.IsNullOrWhiteSpace(DropDownListAsso.SelectedValue) &&
                int.TryParse(DropDownListAsso.SelectedValue, out aId))
            {
                if (!String.IsNullOrWhiteSpace(TxtStart.Text) && !String.IsNullOrWhiteSpace(TxtEnd.Text) &&
                    DateTime.TryParse(TxtStart.Text, out sDate) && DateTime.TryParse(TxtEnd.Text, out eDate))
                {
                    RenderEventList(sDate, eDate,
                        AssociationDB.GetAssociationById(aId));
                }
                else
                {
                    RenderEventList(AssociationDB.GetAssociationById(aId));
                }
            }
            else if (!String.IsNullOrWhiteSpace(TxtStart.Text) && !String.IsNullOrWhiteSpace(TxtEnd.Text) &&
                     DateTime.TryParse(TxtStart.Text, out sDate) && DateTime.TryParse(TxtEnd.Text, out eDate))
            {
                RenderEventList(sDate, eDate);
            }
            else if (!String.IsNullOrWhiteSpace(TxtStart.Text) && DateTime.TryParse(TxtStart.Text, out sDate))
            {
                RenderEventList(Convert.ToDateTime(TxtStart.Text));
            }
            else
            {
                RenderEventList();
            }
        }

        public void PopulateDropDownAsso()
        {
            //Skapar och lägger till alla associations i dropdownboxen.
            List<ListItem> listItems = new List<ListItem>();
            foreach (var association in AssociationDB.GetAllAssociations())
            {
                listItems.Add(new ListItem
                {
                    Text = association.Name,
                    Value = association.Id.ToString()
                });
            }
            DropDownListAsso.Items.Add(new ListItem("All", ""));
            //Sorterar ListItems i alfabetisk ordning i DropDownListan för Association
            foreach (var item in listItems.OrderBy(item => item.Text))
            {
                DropDownListAsso.Items.Add(item);
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
    }
}