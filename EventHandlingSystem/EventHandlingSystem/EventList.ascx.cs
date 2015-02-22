﻿using System;
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
                RenderEventList();
            }
        }

        //DateTime eventdate = Convert.ToDateTime("03/15/2015");
        public void RenderEventList()
        {
            List<events> eventList = EventDB.GetAllEvents();

            RepeaterEvents.DataSource = eventList;
            RepeaterEvents.DataBind();

        }
        public void RenderEventList(DateTime sTime)
        {
            List<events> eventList = EventDB.GetEventsFromSpecifiedStartDate(sTime.Date);

            RepeaterEvents.DataSource = eventList;
            RepeaterEvents.DataBind();

        }
        public void RenderEventList(DateTime sDate, DateTime eDate)
        {
            List<events> eventList = EventDB.GetEventsByRangeDate(sDate, eDate);
            
            RepeaterEvents.DataSource = eventList;
            RepeaterEvents.DataBind();

        }
        public void RenderEventList(DateTime sDate, DateTime eDate, associations asso)
        {
            if (asso != null)
            {
            List<events> eventList = new List<events>();

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

            RepeaterEvents.DataSource = eventList;
            RepeaterEvents.DataBind();
            }
            else
            {
                RenderEventList();
            }

        }
        public void RenderEventList(associations asso)
        {
            if (asso != null)
            {
                List<events> eventList = new List<events>();

                foreach (events e in EventDB.GetAllEvents())
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

                RepeaterEvents.DataSource = eventList;
                RepeaterEvents.DataBind();
            }
            else
            {
                RenderEventList();
            }

        }

        public void RenderEventList(string searchStr)
        {
            List<events> eventList = EventDB.GetEventsBySearchWord(searchStr);

            RepeaterEvents.DataSource = eventList;
            RepeaterEvents.DataBind();

        }
        

        protected void BtnFilter_OnClick(object sender, EventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(TxtSearch.Text))
            {
                RenderEventList(TxtSearch.Text);
                return;
            }

            int aId;
            if (!String.IsNullOrWhiteSpace(DropDownListAsso.SelectedValue) &&
                int.TryParse(DropDownListAsso.SelectedValue, out aId))
            {
                if (!String.IsNullOrWhiteSpace(TxtStart.Text) && !String.IsNullOrWhiteSpace(TxtEnd.Text))
                {
                    RenderEventList(Convert.ToDateTime(TxtStart.Text), Convert.ToDateTime(TxtEnd.Text),
                    AssociationDB.GetAssociationById(aId));
                }
                else
                {
                    RenderEventList(AssociationDB.GetAssociationById(aId));
                }
            }
            else if (!String.IsNullOrWhiteSpace(TxtStart.Text) && !String.IsNullOrWhiteSpace(TxtEnd.Text))
            {
                RenderEventList(Convert.ToDateTime(TxtStart.Text), Convert.ToDateTime(TxtEnd.Text));
            }
            else if (!String.IsNullOrWhiteSpace(TxtStart.Text))
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
    }
}