using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EventHandlingSystem.Database;
using Microsoft.Ajax.Utilities;

namespace EventHandlingSystem
{
    public partial class CommunityAssociationControl : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PopulateCommunityDropDownList();
                PopulateAssocationDropDownList();
            }
        }


        #region PopulateCommunityDropDownList

        //Lägg in alla communities i dropdownlist
        public void PopulateCommunityDropDownList()
        {
            DropDownListCommunity.Items.Clear();

            List<ListItem> commList = new List<ListItem>();
            foreach (var comm in CommunityDB.GetAllCommunities())
            {
                commList.Add(new ListItem
                {
                    Text = CommunityDB.GetCommunityNameByPublishingTermSetId(comm.PublishingTermSetId),
                    Value = comm.Id.ToString()
                });
            }

            ListItem emptyItem = new ListItem("", " ");
            //emptyItem.Attributes.Add("disabled", "disabled");
            DropDownListCommunity.Items.Add(emptyItem);

            //Sorterar commList i alfabetisk ordning i dropdownlistan för Communities
            foreach (var item in commList.OrderBy(item => item.Text))
            {
                DropDownListCommunity.Items.Add(item);
            }
        }
        #endregion



        #region PopulateAssociationDropDownList

        //Lägg in alla föreningar i dropdownlista
        public void PopulateAssocationDropDownList()
        {
            DropDownListAssociation.Items.Clear();

            List<ListItem> assoList = new List<ListItem>();

            if (!string.IsNullOrWhiteSpace(DropDownListCommunity.SelectedValue))
            {
                assoList.AddRange(AssociationDB.GetAllAssociationsInCommunity(CommunityDB.GetCommunityById(int.Parse(DropDownListCommunity.SelectedItem.Value))).Select(asso => new ListItem
                {
                    Text = AssociationDB.GetAssocationNameByPublishingTermSetId(asso.PublishingTermSetId), Value = asso.Id.ToString()
                }));

                //Sorterar assoList i alfabetisk ordning i dropdownlistan för Associations
                foreach (var item in assoList.OrderBy(item => item.Text))
                {
                    DropDownListAssociation.Items.Add(item);
                }
            }
        }

        #endregion




        public void ShowCommunityDetails()
        {
            //Visa Community Name i textboxen
            TextBoxCommName.Text = DropDownListCommunity.SelectedItem.Text;

            //Visa Community Link i textboxen
            HyperLinkCommLink.NavigateUrl =
                "/SitePage.aspx?id=" +
                CommunityDB.GetCommunityById(int.Parse(DropDownListCommunity.SelectedValue)).WebPage.Id + "&type=C";
            HyperLinkCommLink.Target = "_blank";
            HyperLinkCommLink.ToolTip = "This link goes to the web page of the community! (^-^)v";
            LabelCommLink.Text = DropDownListCommunity.SelectedItem.Text + "'s web page";

            //Visa Created och Created By
            //int commId = int.Parse(DropDownListCommunity.SelectedValue);
            //string createdDate = CommunityDB.GetCommunityById(int.Parse(DropDownListCommunity.SelectedValue)).Created.ToShortDateString();
            LabelCreated.Text = "Created: " +
                                CommunityDB.GetCommunityById(int.Parse(DropDownListCommunity.SelectedValue))
                                    .Created.ToShortDateString();

            LabelCreatedBy.Text = "Created by: " +
                                  CommunityDB.GetCommunityById(int.Parse(DropDownListCommunity.SelectedValue))
                                      .CreatedBy.ToString();
        }


        protected void DropDownListCommunity_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(DropDownListCommunity.SelectedValue))
            {
                MultiViewCommunity.ActiveViewIndex = 0;
                PopulateAssocationDropDownList();
                ShowCommunityDetails();
            }
            else
            {
                MultiViewCommunity.ActiveViewIndex = -1;
            }
        }
    }
}