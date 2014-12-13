using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EventHandlingSystem.Database;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.FriendlyUrls;

namespace EventHandlingSystem
{
    public partial class CommunityAssociationControl : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PopulateCommunityDropDownList(DropDownListCommunity);

                //Visa Association Edit-knapp
                if (!string.IsNullOrWhiteSpace(ListBoxAsso.SelectedValue))
                {
                    ButtonEditAsso.Visible = true;
                }
            }
        }
        

        #region Populate Methods

       //Metoden ska kunna användas av olika dropdownlistor
        public void PopulateCommunityDropDownList(DropDownList ddl)
        {
            ddl.Items.Clear();

            List<ListItem> commList = new List<ListItem>();

            //Lägg in alla communities i dropdownlist
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
            ddl.Items.Add(emptyItem);

            //Sorterar commList i alfabetisk ordning i dropdownlistan för Communities
            foreach (var item in commList.OrderBy(item => item.Text))
            {
                ddl.Items.Add(item);
            }
        }
        


        public void PopulateAssocationListBox()
        {
            ListBoxAsso.Items.Clear();

            List<ListItem> assoList = new List<ListItem>();

            // Lägg in alla föreningar i listboxen
            foreach (var asso in AssociationDB.GetAllAssociationsInCommunity(CommunityDB.GetCommunityById(int.Parse(DropDownListCommunity.SelectedItem.Value))))
            {
                assoList.Add(new ListItem
                {
                    Text = AssociationDB.GetAssocationNameByPublishingTermSetId(asso.PublishingTermSetId),
                    Value = asso.Id.ToString()
                });
            }

            foreach (var item in assoList.OrderBy(item => item.Text))
            {
                ListBoxAsso.Items.Add(item);
            }
        }



        //Bortkommenterad kod
        #region PopulateAssociationDropDownList
        //public void PopulateAssocationDropDownList()
        //{
        //    List<ListItem> assoList = new List<ListItem>();

        //    //Lägg in alla föreningar i dropdownlista
        //    if (!string.IsNullOrWhiteSpace(DropDownListCommunity.SelectedValue))
        //    {
        //        assoList.AddRange(AssociationDB.GetAllAssociationsInCommunity(CommunityDB.GetCommunityById(int.Parse(DropDownListCommunity.SelectedItem.Value))).Select(asso => new ListItem
        //        {
        //            Text = AssociationDB.GetAssocationNameByPublishingTermSetId(asso.PublishingTermSetId), Value = asso.Id.ToString()
        //        }));

        //        //Sorterar assoList i alfabetisk ordning i dropdownlistan för Associations
        //        foreach (var item in assoList.OrderBy(item => item.Text))
        //        {
        //           // DropDownListAssociation.Items.Add(item);
        //        }
        //    }
        //}
        #endregion
        #endregion

        
        #region Show Methods

        public void ShowCommunityDetails()
        {
            //Visa Community Name i textboxen och i rubrik över föreningslista
            TextBoxCommName.Text = DropDownListCommunity.SelectedItem.Text;
            LabelAssoInComm.Text = "Associations in " + DropDownListCommunity.SelectedItem.Text;

            //Visa Community Link i textboxen
            HyperLinkCommLink.NavigateUrl =
                "/SitePage.aspx?id=" +
                CommunityDB.GetCommunityById(int.Parse(DropDownListCommunity.SelectedValue)).WebPage.Id + "&type=C";
            HyperLinkCommLink.Target = "_blank";
            HyperLinkCommLink.ToolTip = "This link goes to the web page of the community! (^-^)v";
            LabelCommLink.Text = DropDownListCommunity.SelectedItem.Text + "'s web page";

            // Visa Created och Created By
            LabelCreated.Text = "Created: " +
                                CommunityDB.GetCommunityById(int.Parse(DropDownListCommunity.SelectedValue))
                                    .Created.ToShortDateString();

            LabelCreatedBy.Text = "Created by: " +
                                  CommunityDB.GetCommunityById(int.Parse(DropDownListCommunity.SelectedValue))
                                      .CreatedBy;
        }



        public void ShowAssociationDetails()
        {
            //Visa Association Name i textboxen
            TextBoxAssoName.Text = ListBoxAsso.SelectedItem.Text;

            //Visa Community Link i textboxen
            HyperLinkAssoLink.NavigateUrl =
                "/SitePage.aspx?id=" +
                AssociationDB.GetAssociationById(int.Parse(ListBoxAsso.SelectedValue)).WebPage.Id + "&type=C";
            HyperLinkAssoLink.Target = "_blank";
            HyperLinkAssoLink.ToolTip = "This link goes to the web page of the association! o(^O^)o ";
            LabelAssoLink.Text = ListBoxAsso.SelectedItem.Text + "'s web page";

            // Visa Created och Created By
            LabelCreated.Text = "Created: " +
                                AssociationDB.GetAssociationById(int.Parse(ListBoxAsso.SelectedValue))
                                    .Created.ToShortDateString();
            LabelCreatedBy.Text = "Created by: " +
                                  AssociationDB.GetAssociationById(int.Parse(ListBoxAsso.SelectedValue)).CreatedBy;
        }

        #endregion
        

        #region DropDownLists OnSelectedIndexChanged

        protected void DropDownListCommunity_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(DropDownListCommunity.SelectedValue))
            {
                //Visa community-dropdownlist
                MultiViewSelectComm.ActiveViewIndex = 0;

                //Visa community info
                MultiViewCommDetails.ActiveViewIndex = 0;
                ShowCommunityDetails();

                //Visa lista med föreningar
                PopulateAssocationListBox();
                MultiViewAssoDetails.ActiveViewIndex = -1;
            }
            else
            {
                //Visa endast community-dropdownlist-view --> GÖM alla andra
                MultiViewSelectComm.ActiveViewIndex = 0;
                MultiViewCommDetails.ActiveViewIndex = -1;
                MultiViewAssoDetails.ActiveViewIndex = -1;
                MultiViewAssoCreate.ActiveViewIndex = -1;
            }

            //Rensa Labeltexter
            LabelErrorMessage.Text = string.Empty;
            LabelCommSave.Text = string.Empty;
        }


        //Dropdownlista för Associationslistboxen för en viss Community
        protected void ListBoxAsso_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            MultiViewSelectComm.ActiveViewIndex = 0;
        }


        //Dropdownlista för Communities i Create Association View
        protected void DropDownListCommCreateAsso_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(DropDownListCommunityCreateAsso.SelectedValue))
            {

            }
        }


        //Dropdownlista för Parent Associations 
        protected void DropDownListCreateParAsso_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        
        //Dropdownlista för Association Types
        protected void DropDownListCreateAssoType_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        #endregion


        #region Button Click

        protected void ButtonEditAsso_OnClick(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(ListBoxAsso.SelectedValue))
            {
                MultiViewAssoCreate.ActiveViewIndex = -1;
                MultiViewAssoDetails.ActiveViewIndex = 0;
                ShowAssociationDetails();
                LabelErrorMessage.Text = "";
            }
            else
            {
                LabelErrorMessage.Text = "You have to select an association!";
                LabelErrorMessage.Style.Add(HtmlTextWriterStyle.Color, "red");
            }
        }

        

        protected void ButtonCommSave_OnClick(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(DropDownListCommunity.SelectedValue))
            {
                //Hitta community Id i dropdownlista - value
                int commId = int.Parse(DropDownListCommunity.SelectedItem.Value);

                //Hitta publiseringsTS-id via community
                int pubId = CommunityDB.GetPublishingTermSetIdByCommunityId(commId);

                //Uppdatera det nya namnet från textboxen
                TermSetDB.UpdateTermSetName(TermSetDB.GetTermSetById(pubId), TextBoxCommName.Text);

                LabelCommSave.Text = TextBoxCommName.Text + " has been updated.";
                LabelCommSave.Style.Add(HtmlTextWriterStyle.Color, "black");
                PopulateCommunityDropDownList(DropDownListCommunity);
            }
            else
            {
                LabelCommSave.Text = "Select a community before trying to save changes again.";
                LabelCommSave.Style.Add(HtmlTextWriterStyle.Color, "red");
            }
        }



        protected void ButtonCreateNewAsso_OnClick(object sender, EventArgs e)
        {
            MultiViewAssoDetails.ActiveViewIndex = -1;
            MultiViewAssoCreate.ActiveViewIndex = 0;
            PopulateCommunityDropDownList(DropDownListCommunityCreateAsso);
        }


        protected void ButtonCreateAssoCancel_OnClick(object sender, EventArgs e)
        {
            
        }


        protected void ButtonCreateAsso_OnClick(object sender, EventArgs e)
        {
            
        }

        #endregion
    }
}