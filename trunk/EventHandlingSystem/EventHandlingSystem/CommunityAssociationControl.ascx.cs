using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.Services.Description;
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
            }
        }
        

        #region Populate Methods

       //Metoden ska kunna användas av olika dropdownlistor
        public void PopulateCommunityDropDownList(DropDownList ddl)
        {
            ddl.Items.Clear();

            List<ListItem> commList = new List<ListItem>();

            //Lägg in alla communities i dropdownlista
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


        //Metoden ska kunna användas av olika dropdownlistor
        public void PopulateAssocationDropDownList(DropDownList ddl)
        {
            ddl.Items.Clear();

            //Lägg in alla föreningar i dropdownlista
            List<ListItem> assoList = AssociationDB.GetAllAssociations().Select(asso => new ListItem
            {
                Text = AssociationDB.GetAssocationNameByPublishingTermSetId(asso.PublishingTermSetId), Value = asso.Id.ToString()
            }).ToList();

            // ALTERNATIV med foreach
            //foreach (var asso in AssociationDB.GetAllAssociations())
            //{
            //    assoList.Add(new ListItem
            //    {
            //        Text = AssociationDB.GetAssocationNameByPublishingTermSetId(asso.PublishingTermSetId),
            //        Value = asso.Id.ToString()
            //    });
            //}

            ListItem emptyItem = new ListItem("", " ");
            ddl.Items.Add(emptyItem); //index 0

            //Sorterar assoList i alfabetisk ordning i dropdownlistan för Associations
            foreach (var item in assoList.OrderBy(item => item.Text))
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

        public void PopulateAssocationListBox(int aid)
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

            //Markera vilket item som ska vara vald när listan renderas
            ListBoxAsso.SelectedValue = aid.ToString();
        }
        

        public void PopulateAssociationTypesDropDownList()
        {
            DropDownListAssoType.Items.Clear();

            List<ListItem> atList = new List<ListItem>();

            foreach (var a in AssociationDB.GetAllAssociationsWithAssociationType())
            {
                int? at = a.AssociationType;
                //omvandla nullable int till int
                int assoTypeId = at.GetValueOrDefault(); //at ?? default(int); 

                atList.Add(new ListItem
                {
                    Text = TermDB.GetTermById(assoTypeId).Name,
                    Value = assoTypeId.ToString()
                });
            }

            ListItem emptyItem = new ListItem("", " ");
            DropDownListAssoType.Items.Add(emptyItem); //index 0

            //Tar bort dubbletter och sorterar i bokstavsordning
            foreach (var item in atList.Distinct().OrderBy(item => item.Text))
            {
                DropDownListAssoType.Items.Add(item);
            }
        }


        // Visa ALLA föreningstyper i ddl
        public void PopulateAllAssociationTypesDropDownList()
        {
            DropDownListAssoType.Items.Clear();

            //Hämta alla föreningstyperna och lägg i dropdownlistan (föreningstyp Id = 2)
            List<ListItem> atList = new List<ListItem>();

            foreach (var at in TermDB.GetAllTermsByTermSetId(2))
            {
                atList.Add(new ListItem
                {
                    Text = at.Name,
                    Value = at.Id.ToString()
                });
            }

            ListItem emptyItem = new ListItem("", " ");
            DropDownListAssoType.Items.Add(emptyItem); //index 0

            //Tar bort dubbletter och sorterar i bokstavsordning
            foreach (var item in atList.OrderBy(item => item.Text))
            {
                DropDownListAssoType.Items.Add(item);
            }
        }


        public void PopulateSubAssociationsBulletedList()
        {
            BulletedListSubAssociations.Items.Clear();

            List<ListItem> subAssoList = new List<ListItem>();

            // Lägg in alla underföreningar i punktlistan
            foreach (var asso in
                    AssociationDB.GetAllSubAssociationsByParentAssociationId(int.Parse(ListBoxAsso.SelectedItem.Value)))
            {
                subAssoList.Add(new ListItem
                {
                    Text = AssociationDB.GetAssocationNameByPublishingTermSetId(asso.PublishingTermSetId),
                    //Value = asso.Id.ToString()
                });
            }

            foreach (var item in subAssoList.OrderBy(item => item.Text))
            {
                BulletedListSubAssociations.Items.Add(item);
            }
        }
        
        #endregion

        

        #region Show Methods

        public void ShowCommunityDetails()
        {
            // Visa Community Name i textboxen och i rubrik över föreningslista
            TextBoxCommName.Text = DropDownListCommunity.SelectedItem.Text;
            LabelAssoInComm.Text = "Associations in " + DropDownListCommunity.SelectedItem.Text;
            LabelAssoInComm.Style.Add(HtmlTextWriterStyle.Color, "black");

            // Visa Community logga med länk innehållande tooltip
            HyperLinkLogoCommunity.NavigateUrl =
                "/SitePage.aspx?id=" +
                CommunityDB.GetCommunityById(int.Parse(DropDownListCommunity.SelectedValue)).WebPage.Id + "&type=C";
            HyperLinkLogoCommunity.Target = "_blank";
            HyperLinkLogoCommunity.ToolTip = "This link goes to the web page of " + DropDownListCommunity.SelectedItem.Text + ". (^-^)v";
            
            // Visa Created och Created By
            LabelCreated.Text = "<b>Created: </b>" +
                                CommunityDB.GetCommunityById(int.Parse(DropDownListCommunity.SelectedValue))
                                    .Created.ToShortDateString();

            LabelCreatedBy.Text = "<b>Created by: </b>" +
                                  CommunityDB.GetCommunityById(int.Parse(DropDownListCommunity.SelectedValue))
                                      .CreatedBy;
        }



        public void ShowAssociationDetails()
        {
            Association asso = AssociationDB.GetAssociationById(int.Parse(ListBoxAsso.SelectedItem.Value));

            //Visa Association Name i textboxen
            TextBoxAssoName.Text = ListBoxAsso.SelectedItem.Text;

            //Visa Association logga plus web page link
            HyperLinkLogoAssociation.NavigateUrl =
                "/SitePage.aspx?id=" +
                AssociationDB.GetAssociationById(int.Parse(ListBoxAsso.SelectedValue)).WebPage.Id + "&type=C";
            HyperLinkLogoAssociation.Target = "_blank";
            HyperLinkLogoAssociation.ToolTip = "This link goes to the web page of " + ListBoxAsso.SelectedItem.Text + "! o(^O^)o ";

            // Visa Community-dropdownlista
            PopulateCommunityDropDownList(DropDownListCommunityInAsso);
            DropDownListCommunityInAsso.SelectedIndex =
                        DropDownListCommunityInAsso.Items.IndexOf(
                            DropDownListCommunityInAsso.Items.FindByValue(asso.CommunityId.ToString()));

            // Visa ParentAssociation-dropdownlista
            PopulateAssocationDropDownList(DropDownListParentAsso);

            if (asso.ParentAssociationId == null)
            {
                //Om föreningen inte har en förälder ska dropdownlistan visa blankt
                DropDownListParentAsso.SelectedIndex = 0;
            }
            else
            {
                DropDownListParentAsso.SelectedIndex =
                DropDownListParentAsso.Items.IndexOf(
                    DropDownListParentAsso.Items.FindByValue(asso.ParentAssociationId.ToString()));
            }
            
            // Visa alla föreningstyper i dropdownlista
            PopulateAllAssociationTypesDropDownList();

            if (asso.AssociationType == null)
            {
                //Om föreningen inte har en föreningstyp ska dropdownlistan visa blankt
                DropDownListAssoType.SelectedIndex = 0;
            }
            else
            {
                DropDownListAssoType.SelectedIndex =
                DropDownListAssoType.Items.IndexOf(
                    DropDownListAssoType.Items.FindByValue(asso.AssociationType.ToString()));
            }

            // Visa Created, Created By och Publishing TermSet
            LabelCreatedAsso.Text = "<b>Created: </b>" +
                                AssociationDB.GetAssociationById(int.Parse(ListBoxAsso.SelectedValue))
                                    .Created.ToShortDateString();
            LabelCreatedByAsso.Text = "<b>Created by: </b>" +
                                  AssociationDB.GetAssociationById(int.Parse(ListBoxAsso.SelectedValue)).CreatedBy;

            LabelPTSAsso.Text = "<b>Publishing TermSet: </b>" +
                                TermSetDB.GetTermSetNameByTermSetId(
                                    TermSetDB.GetTermSetById(asso.PublishingTermSetId).Id);

            // Visa underföreningar för en förening i en lista
            if (AssociationDB.GetAllSubAssociationsByParentAssociationId(asso.Id).Count != 0)
            {
                PopulateSubAssociationsBulletedList();
            }
            else
            {
                BulletedListSubAssociations.Items.Clear();
                ListItem emptyItem = new ListItem(" --- ", " ");
                BulletedListSubAssociations.Items.Add(emptyItem); //index 0
            }            
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


        //Associationslistbox för en viss Community - när ett item klickas visas föreningsdetaljerna
        protected void ListBoxAsso_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            MultiViewAssoDetails.ActiveViewIndex = 0;
            MultiViewAssoCreate.ActiveViewIndex = -1;
            ShowAssociationDetails();
            LabelUpdateAsso.Text = "";
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

        //protected void ButtonEditAsso_OnClick(object sender, EventArgs e)
        //{
        //    if (!string.IsNullOrWhiteSpace(ListBoxAsso.SelectedValue))
        //    {
        //        MultiViewAssoCreate.ActiveViewIndex = -1;
        //        MultiViewAssoDetails.ActiveViewIndex = 0;
        //        ShowAssociationDetails();
        //        LabelErrorMessage.Text = "";
        //    }
        //    else
        //    {
        //        LabelErrorMessage.Text = "You have to select an association!";
        //        LabelErrorMessage.Style.Add(HtmlTextWriterStyle.Color, "red");
        //    }
        //}

        

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
                LabelCommSave.Style.Add(HtmlTextWriterStyle.Color, "#217ebb");
                PopulateCommunityDropDownList(DropDownListCommunity);
            }
            else
            {
                LabelCommSave.Text = "Select a community before trying to save changes again.";
                LabelCommSave.Style.Add(HtmlTextWriterStyle.Color, "red");
            }
        }


        // För att komma till "Create New Community" vy
        protected void ButtonCreateNewComm_OnClick(object sender, EventArgs e)
        {
            
        }


        // För att komma till "Create New Association" vy
        protected void ButtonCreateNewAsso_OnClick(object sender, EventArgs e)
        {
            MultiViewAssoDetails.ActiveViewIndex = -1;
            MultiViewAssoCreate.ActiveViewIndex = 0;
            PopulateCommunityDropDownList(DropDownListCommunityCreateAsso);
        }
        

        protected void ButtonCreateAssoCancel_OnClick(object sender, EventArgs e)
        {
            
        }


        // För att SKAPA en ny förening
        protected void ButtonCreateAsso_OnClick(object sender, EventArgs e)
        {
            
        }



        // Spara ändringar i Association details - UPDATE-knappen
        protected void ButtonUpdateAsso_OnClick(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(ListBoxAsso.SelectedValue))
            {
                //Hitta Association-Id i listboxen - value
                int assoId = int.Parse(ListBoxAsso.SelectedItem.Value);

                //Hitta publiseringsTS-id via community
                int pubId = AssociationDB.GetPublishingTermSetIdByAssociationId(assoId);

                //Uppdatera det nya namnet från textboxen
                TermSet tsToUpdate = new TermSet
                {
                    Id = pubId,
                    Name = TextBoxAssoName.Text,
                    ParentTermSetId = CommunityDB.GetPublishingTermSetIdByCommunityId(
                        int.Parse(DropDownListCommunityInAsso.SelectedItem.Value))
                };

                Term termToUpdate = TermDB.GetAllTermsByTermSetId(pubId).FirstOrDefault();
                termToUpdate.Name = TextBoxAssoName.Text;
            
                int affectedRows = TermSetDB.UpdateTermSet(tsToUpdate);
                affectedRows += TermDB.UpdateTerm(termToUpdate);

                PopulateAssocationListBox(assoId);
               
                
                //Uppdatera community i vilken föreningen finns
                Association assoToUpdate = new Association();
                assoToUpdate.Id = assoId;
                assoToUpdate.CommunityId = int.Parse(DropDownListCommunityInAsso.SelectedItem.Value);
                //Hitta alla barnen
                
                foreach (var termSet in TermSetDB.GetChildTermSetsByParentTermSetId(
                    AssociationDB.GetAssociationById(assoToUpdate.Id).PublishingTermSetId))
                {
                    Association asso = AssociationDB.GetAssociationByPublishingTermSetId(termSet.Id);
                    asso.CommunityId = assoToUpdate.CommunityId;
                    affectedRows += AssociationDB.UpdateAssociation(asso);

                   affectedRows += ChangeCommunityIdForChildAssocations(termSet, asso);
                }

                //Uppdatera föreningstyp
                if (!string.IsNullOrWhiteSpace(DropDownListAssoType.SelectedValue))
                {
                    assoToUpdate.AssociationType = int.Parse(DropDownListAssoType.SelectedItem.Value);
                }
                else
                {
                    //Om man väljer blankt i ddl blir föreningstypen null
                    assoToUpdate.AssociationType = null;
                }

                //ShowAssociationDetails();

                //Anropa Update-metoden
                affectedRows += AssociationDB.UpdateAssociation(assoToUpdate);
                PopulateAssocationListBox();
                

                if (affectedRows != 0)
                {
                    LabelUpdateAsso.Text = TextBoxAssoName.Text + " has been updated!";
                    LabelUpdateAsso.Style.Add(HtmlTextWriterStyle.Color, "#217ebb");
                }
                else
                {
                    LabelUpdateAsso.Text = "Error: Changes might not have been made in " + TextBoxAssoName.Text + 
                        "... Make sure to set the update information.";
                    LabelUpdateAsso.Style.Add(HtmlTextWriterStyle.Color, "red");
                }
            }
            else
            {
                LabelUpdateAsso.Text = "Select an association in the listbox before trying to save changes again.";
                LabelUpdateAsso.Style.Add(HtmlTextWriterStyle.Color, "red");
                LabelAssoInComm.Text = "Select An Association in this Listbox";
                LabelAssoInComm.Style.Add(HtmlTextWriterStyle.Color, "red");
            }
        }

        private int affectedRows;

        //Rekursiv metod för att hitta alla led nedåt i publishing termset för associations.
        private int ChangeCommunityIdForChildAssocations(TermSet termSet, Association assoToUpdate)
        {
            foreach (var tSet in TermSetDB.GetChildTermSetsByParentTermSetId(termSet.Id))
            {
                Association asso = AssociationDB.GetAssociationByPublishingTermSetId(tSet.Id);
                asso.CommunityId = assoToUpdate.CommunityId;
                affectedRows += AssociationDB.UpdateAssociation(asso);

                ChangeCommunityIdForChildAssocations(tSet, assoToUpdate);
            }
            return affectedRows;
        }

        #endregion
    }
}