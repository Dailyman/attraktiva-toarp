using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EventHandlingSystem.Database;


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
                    Text = comm.Name,
                    Value = comm.Id.ToString()
                });
            }

            ListItem emptyItem = new ListItem("", " ");
            emptyItem.Attributes.Add("disabled", "disabled");
            ddl.Items.Add(emptyItem);

            //Sorterar commList i alfabetisk ordning i dropdownlistan för Communities
            foreach (var item in commList.OrderBy(item => item.Text))
            {
                ddl.Items.Add(item);
            }
        }


        //Metoden ska kunna användas av olika dropdownlistor
        public void PopulateAssociationDropDownList(DropDownList ddl)
        {
            ddl.Items.Clear();

            //Lägg in alla föreningar i dropdownlista
            List<ListItem> assoList = AssociationDB.GetAllAssociations().Select(asso => new ListItem
            {
                Text = asso.Name,  
                Value = asso.Id.ToString()
            }).ToList();

            //// ALTERNATIV med foreach
            //foreach (var asso in AssociationDB.GetAllAssociations())
            //{
            //    assoList.Add(new ListItem
            //    {
            //        Text = asso.Name,
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
        

        public void PopulateAssociationListBox()
        {
            ListBoxAsso.Items.Clear();

            List<ListItem> assoList = new List<ListItem>();

            // Lägg in alla föreningar i listboxen
            foreach (
                var asso in
                    AssociationDB.GetAllAssociationsInCommunity(
                        CommunityDB.GetCommunityById(int.Parse(DropDownListCommunity.SelectedItem.Value))))
            {
                assoList.Add(new ListItem
                {
                    Text = asso.Name,
                    Value = asso.Id.ToString()
                });
            }

            foreach (var item in assoList.OrderBy(item => item.Text))
            {
                ListBoxAsso.Items.Add(item);
            }
        }

        public void PopulateAssociationListBox(int aId)
        {
            ListBoxAsso.Items.Clear();

            List<ListItem> assoList = new List<ListItem>();

            // Lägg in alla föreningar i listboxen
            foreach (
                var asso in
                    AssociationDB.GetAllAssociationsInCommunity(
                        CommunityDB.GetCommunityById(int.Parse(DropDownListCommunity.SelectedItem.Value))))
            {
                assoList.Add(new ListItem
                {
                    Text = asso.Name,
                    Value = asso.Id.ToString()
                });
            }

            foreach (var item in assoList.OrderBy(item => item.Text))
            {
                ListBoxAsso.Items.Add(item);
            }

            //Markera vilket item som ska vara vald när listan renderas
            ListBoxAsso.SelectedValue = aId.ToString();
        }

        //Visa associations i en community
        public void PopulateAssociationInCommunityDropDownList(DropDownList ddlPa, DropDownList ddlComm)
        {
            ddlPa.Items.Clear();

            List<ListItem> assoList = new List<ListItem>();

            // Lägg in alla föreningar i ddl
            foreach (var asso in AssociationDB.GetAllAssociationsInCommunity(CommunityDB.GetCommunityById(int.Parse(ddlComm.SelectedItem.Value))))
            {
                assoList.Add(new ListItem
                {
                    Text = asso.Name,
                    Value = asso.Id.ToString()
                });
            }

            ListItem emptyItem = new ListItem("", " ");
            ddlPa.Items.Add(emptyItem); //index 0

            foreach (var item in assoList.OrderBy(item => item.Text))
            {
                ddlPa.Items.Add(item);
            }
        }

        //Visa associations i en community, förvald/markerad association
        public void PopulateAssociationInCommunityDropDownList(int aid, DropDownList ddl)
        {
            ddl.Items.Clear();

            List<ListItem> assoList = new List<ListItem>();

            // Lägg in alla föreningar i listboxen
            foreach (var asso in AssociationDB.GetAllAssociationsInCommunity(CommunityDB.GetCommunityById(int.Parse(DropDownListCommunity.SelectedItem.Value))))
            {
                assoList.Add(new ListItem
                {
                    Text = asso.Name,
                    Value = asso.Id.ToString()
                });
            }

            ListItem emptyItem = new ListItem("", " ");
            ddl.Items.Add(emptyItem); //index 0

            foreach (var item in assoList.OrderBy(item => item.Text))
            {
                ddl.Items.Add(item);
            }

            //Markera vilket item som ska vara vald när listan renderas
            ddl.SelectedValue = aid.ToString();
        }
        


        // Visa ALLA föreningstyper i ddl
        public void PopulateAllAssociationCategoriesDropDownList()
        {
            DropDownListCategories.Items.Clear();

            //Hämta alla kategorierna och lägg i dropdownlistan 
            List<ListItem> acList = new List<ListItem>();

            foreach (var category in  CategoryDB.GetAllCategories())
            {
                acList.Add(new ListItem
                {
                    Text = category.Name,
                    Value = category.Id.ToString()
                });
            }

            ListItem emptyItem = new ListItem("", " ");
            DropDownListCategories.Items.Add(emptyItem); //index 0

            foreach (var item in acList.OrderBy(item => item.Text))
            {
                DropDownListCategories.Items.Add(item);
            }
        }


        public void PopulateSubAssociationsBulletedList()
        {
            BulletedListSubAssociations.Items.Clear();

            List<ListItem> subAssoList = new List<ListItem>();

            //Lägg in alla underföreningar i punktlistan
            foreach (var asso in
                AssociationDB.GetAllSubAssociationsByParentAssociationId(int.Parse(ListBoxAsso.SelectedItem.Value)))
            {
                subAssoList.Add(new ListItem
                {
                    Text = asso.Name,
                    //Value = asso.Id.ToString()
                });
            }

            foreach (var item in subAssoList.OrderBy(item => item.Text))
            {
                BulletedListSubAssociations.Items.Add(item);
            }
        }

        
        public void PopulateCategoriesInAssoListBox()
        {
            ListBoxCatInAsso.Items.Clear();

            //Hämta alla kategorier för den aktuella föreningen 
            List<ListItem> ciaList = new List<ListItem>();

            foreach (var category in
                AssociationDB.GetAllCategoriesForAssociationByAssociation
                (AssociationDB.GetAssociationById(int.Parse(ListBoxAsso.SelectedItem.Value))))
            {
                ciaList.Add(new ListItem
                {
                    Text = category.Name,
                    Value = category.Id.ToString()
                });
            }

            foreach (var item in ciaList.Distinct()
                                       .OrderBy(item => item.Text))
            {
                ListBoxCatInAsso.Items.Add(item);
            }
        }

        #endregion



        #region Show Methods

        public void ShowCommunityDetails()
        {
            // Visa Community Name i textboxen och i rubrik över föreningslista
            TextBoxCommName.Text = DropDownListCommunity.SelectedItem.Text;

            communities comm = CommunityDB.GetCommunityById(int.Parse(DropDownListCommunity.SelectedItem.Value));

            TextBoxCommDescript.Text = comm.Description ?? "This is a very friendly community...";
            TextBoxCommLogoImgUrl.Text = comm.LogoUrl ?? "~/Images/Community.jpg";

            LabelAssoInComm.Text = "Associations in " + DropDownListCommunity.SelectedItem.Text;
            LabelAssoInComm.Style.Add(HtmlTextWriterStyle.Color, "black");

            // Visa Community logga med länk innehållande tooltip
            ImageLogoCommunity.ImageUrl = comm.LogoUrl ?? "~/Images/Community.jpg";
            HyperLinkLogoCommunity.NavigateUrl =
                "/SitePage.aspx?id=" +
                (WebPageDB.GetWebPageByCommunityId(int.Parse(DropDownListCommunity.SelectedValue)) != null
                    ? WebPageDB.GetWebPageByCommunityId(int.Parse(DropDownListCommunity.SelectedValue)).Id.ToString()
                    : "") + "&type=C";
            HyperLinkLogoCommunity.Target = "_blank";
            HyperLinkLogoCommunity.ToolTip = "This link goes to the web page of " +
                                             DropDownListCommunity.SelectedItem.Text + ". (^-^)v";

            // Visa Created och Created By
            LabelCreated.Text = "<b>Created: </b>" +
                                CommunityDB.GetCommunityById(int.Parse(DropDownListCommunity.SelectedValue))
                                    .Created.ToShortDateString();

            LabelCreatedBy.Text = "<b>Created by: </b>" +
                                  CommunityDB.GetCommunityById(int.Parse(DropDownListCommunity.SelectedValue))
                                      .CreatedBy;
        }

        public void ShowCommunityDetails(communities comm)
        {
            // Visa Community Name i textboxen och i rubrik över föreningslista
            TextBoxCommName.Text = comm.Name;
            TextBoxCommDescript.Text = comm.Description ?? "This is a very friendly community...";
            TextBoxCommLogoImgUrl.Text = comm.LogoUrl ?? "~/Images/Community.jpg";
            LabelAssoInComm.Text = "Associations in " + comm.Name;
            LabelAssoInComm.Style.Add(HtmlTextWriterStyle.Color, "black");

            // Visa Community logga med länk innehållande tooltip
            ImageLogoCommunity.ImageUrl = comm.LogoUrl ?? "~/Images/Community.jpg";
            HyperLinkLogoCommunity.NavigateUrl =
                "/SitePage.aspx?id=" +
                (WebPageDB.GetWebPageByCommunityId(comm.Id) != null
                    ? WebPageDB.GetWebPageByCommunityId(comm.Id).Id.ToString()
                    : "") + "&type=C";
            HyperLinkLogoCommunity.Target = "_blank";
            HyperLinkLogoCommunity.ToolTip = "This link goes to the web page of " +
                                             comm.Name + ". (^-^)v";

            // Visa Created och Created By
            LabelCreated.Text = "<b>Created: </b>" +
                                CommunityDB.GetCommunityById(comm.Id)
                                    .Created.ToShortDateString();

            LabelCreatedBy.Text = "<b>Created by: </b>" +
                                  CommunityDB.GetCommunityById(comm.Id)
                                      .CreatedBy;
        }


        public void ShowAssociationDetails(associations a)
        {
            associations asso = a;

            //Visa Association Name i textboxen
            TextBoxAssoName.Text = a.Name;

            //Visa Description i multitextbox
            TextBoxAssoDescript.Text = a.Description ?? "There is no one more friendly than us. ^^";

            //Visa Association logga plus web page link
            ImageLogoAssociation.ImageUrl = a.LogoUrl ?? "~/Images/Association.jpg";
            HyperLinkLogoAssociation.NavigateUrl =
                "/SitePage.aspx?id=" +
                (WebPageDB.GetWebPageByAssociationId(a.Id) != null
                    ? WebPageDB.GetWebPageByAssociationId(a.Id).Id.ToString()
                    : "") + "&type=A";
            
            HyperLinkLogoAssociation.Target = "_blank";
            HyperLinkLogoAssociation.ToolTip = "This link goes to the web page of " + a.Name +
                                               "! o(^O^)o ";

            // Visa Community-dropdownlista
            PopulateCommunityDropDownList(DropDownListCommunityInAsso);
            DropDownListCommunityInAsso.SelectedIndex =
                DropDownListCommunityInAsso.Items.IndexOf(
                    DropDownListCommunityInAsso.Items.FindByValue(asso.Communities_Id.ToString()));

            // Visa ParentAssociation-dropdownlista
            PopulateAssociationInCommunityDropDownList(asso.Id, DropDownListParentAsso);

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

            // Visa alla kategorier i dropdownlista
            PopulateAllAssociationCategoriesDropDownList();
            PopulateCategoriesInAssoListBox();
            DropDownListCategories.SelectedIndex = 0;

            //Visa Logo Url i textbox
            TextBoxAssoLogoImgUrl.Text = a.LogoUrl ?? "~/Images/Association.jpg";

            // Visa Created, Created By och Publishing TermSet
            LabelCreatedAsso.Text = "<b>Created: </b>" +
                                    AssociationDB.GetAssociationById(int.Parse(ListBoxAsso.SelectedValue))
                                        .Created.ToShortDateString();
            LabelCreatedByAsso.Text = "<b>Created by: </b>" +
                                      AssociationDB.GetAssociationById(int.Parse(ListBoxAsso.SelectedValue)).CreatedBy;

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

            bullListMemberList.Items.Clear();
            lnkbtnMembers.Text = "Show members";
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
                PopulateAssociationListBox();
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
            //Lägg in värdet av AssoId i hiddenfield
            hdfAssoId.Value = ListBoxAsso.SelectedItem.Value;

            MultiViewAssoDetails.ActiveViewIndex = 0;
            MultiViewAssoCreate.ActiveViewIndex = -1;
            ShowAssociationDetails(AssociationDB.GetAssociationById(int.Parse(ListBoxAsso.SelectedItem.Value)));
            LabelUpdateAsso.Text = "";
        }
        
        #endregion



        #region Button Click

        protected void ButtonEditAsso_OnClick(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(ListBoxAsso.SelectedValue))
            {
                MultiViewAssoCreate.ActiveViewIndex = -1;
                MultiViewAssoDetails.ActiveViewIndex = 0;
                ShowAssociationDetails(AssociationDB.GetAssociationById(int.Parse(ListBoxAsso.SelectedItem.Value)));
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

                //Uppdatera det nya namnet från textboxen
                communities commToUpdate = CommunityDB.GetCommunityById(commId);
                commToUpdate.Name = TextBoxCommName.Text;

                //Uppdatera det nya namnet i webpage också
                webpages wpToUpdate = WebPageDB.GetWebPageByCommunityId(commId);
                wpToUpdate.Title = TextBoxCommName.Text;

                //Uppdatera description från textboxen
                commToUpdate.Description = TextBoxCommDescript.Text;

                //Uppdatera logo-adressen från textboxen
                commToUpdate.LogoUrl = TextBoxCommLogoImgUrl.Text; 

                int affectedRows = CommunityDB.UpdateCommunity(commToUpdate) + WebPageDB.UpdateWebPage(wpToUpdate);
                
                if (affectedRows != 0)
                {
                    LabelCommSave.Text = TextBoxCommName.Text + " has been updated.";
                    LabelCommSave.Style.Add(HtmlTextWriterStyle.Color, "#217ebb");
                    PopulateCommunityDropDownList(DropDownListCommunity);
                }
                else
                {
                    LabelUpdateAsso.Text = "Error: " + TextBoxAssoName.Text +
                        " could not be updated!";
                    LabelUpdateAsso.Style.Add(HtmlTextWriterStyle.Color, "red");
                }
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
            MultiViewCommCreate.ActiveViewIndex = 0;
        }

        //För att skapa en ny community
        protected void ButtonCreateComm_OnClick(object sender, EventArgs e)
        {
            communities comm = new communities
            {
                Name = TextBoxCommNameCreate.Text,
                LogoUrl = TextBoxCommLogoUrl.Text,
                CreatedBy = HttpContext.Current.User.Identity.Name,
                UpdatedBy = HttpContext.Current.User.Identity.Name
            };

            if (CommunityDB.AddCommunity(comm))
            {
                webpages wp = new webpages
                {
                    Title = TextBoxCommNameCreate.Text,
                    CommunityId = CommunityDB.GetCommunityByName(comm.Name).Id,
                    //Layout och style - fixa dropdownlistor senare!
                    CreatedBy = HttpContext.Current.User.Identity.Name,
                    UpdatedBy = HttpContext.Current.User.Identity.Name
                };

                if (WebPageDB.AddWebPage(wp))
                {
                    MultiViewCommDetails.ActiveViewIndex = 0;
                    MultiViewCommCreate.ActiveViewIndex = -1;
                    ShowCommunityDetails(CommunityDB.GetCommunityByName(comm.Name));
                }
                else
                {
                    LabelCreateComm.Text = "Webpage could not be created. Try again!";
                    LabelCreateComm.Style.Add(HtmlTextWriterStyle.Color, "red");
                }
            }
            else
            {
                LabelCreateComm.Text = "Community could not be created. Try again!";
                LabelCreateComm.Style.Add(HtmlTextWriterStyle.Color, "red");
            }
        }

        //För att gå ur "Create New Community" vyn
        protected void ButtonCreateCommCancel_OnClick(object sender, EventArgs e)
        {
            MultiViewCommCreate.ActiveViewIndex = -1;
        }

        // För att komma till "Create New Association" vy
        protected void ButtonCreateNewAsso_OnClick(object sender, EventArgs e)
        {
            MultiViewAssoDetails.ActiveViewIndex = -1;
            MultiViewAssoCreate.ActiveViewIndex = 0;

            //Hämta aktuell CommunityId
            int commId = int.Parse(DropDownListCommunity.SelectedItem.Value);

            //Populera ParentAssociation dropdownlist
            DropDownListCreateParAsso.Items.Clear();

            List<ListItem> assoList = new List<ListItem>();

            // Lägg in alla föreningar i ddl
            foreach (var asso in AssociationDB.GetAllAssociationsInCommunity(CommunityDB.GetCommunityById(commId)))
            {
                assoList.Add(new ListItem
                {
                    Text = asso.Name,
                    Value = asso.Id.ToString()
                });
            }

            ListItem emptyItem = new ListItem("", " ");
            DropDownListCreateParAsso.Items.Add(emptyItem); //index 0

            foreach (var item in assoList.OrderBy(item => item.Text))
            {
                DropDownListCreateParAsso.Items.Add(item);
            }
        }
        
        //Cancel Create Association View
        protected void ButtonCreateAssoCancel_OnClick(object sender, EventArgs e)
        {
            MultiViewAssoCreate.ActiveViewIndex = -1;
        }
        
        // För att SKAPA en ny förening
        protected void ButtonCreateAsso_OnClick(object sender, EventArgs e)
        {
            associations asso = new associations
            {
                Name = TextBoxCreateAssoName.Text,
                ParentAssociationId = string.IsNullOrWhiteSpace(DropDownListCreateParAsso.SelectedValue) ?
                (int?)null : int.Parse(DropDownListCreateParAsso.SelectedItem.Value),
                LogoUrl = TextBoxAssoImgUrl.Text,
                CreatedBy = HttpContext.Current.User.Identity.Name,
                UpdatedBy = HttpContext.Current.User.Identity.Name,
                Communities_Id = int.Parse(DropDownListCommunity.SelectedItem.Value)
            };

            if (AssociationDB.AddAssociation(asso))
            {
                webpages wp = new webpages
                {
                    Title = TextBoxCreateAssoName.Text,
                    AssociationId = AssociationDB.GetAssociationByName(asso.Name).Id,
                    //Layout och style - fixa dropdownlistor senare!
                    CreatedBy = HttpContext.Current.User.Identity.Name,
                    UpdatedBy = HttpContext.Current.User.Identity.Name
                };

                if (WebPageDB.AddWebPage(wp))
                {
                    MultiViewAssoCreate.ActiveViewIndex = -1;
                    PopulateAssociationListBox();
                    LabelErrorMessage.Text = asso.Name + " has been successfully created! (^o^)/";
                    LabelErrorMessage.Style.Add(HtmlTextWriterStyle.Color, "#217ebb");
                }
                else
                {
                    LabelErrorMessage.Text = "Webpage could not be created. Try again!";
                    LabelErrorMessage.Style.Add(HtmlTextWriterStyle.Color, "red");
                }
            }
            else
            {
                LabelErrorMessage.Text = "Association could not be created. Try again!";
                LabelErrorMessage.Style.Add(HtmlTextWriterStyle.Color, "red");
            }
        }
        
        // För att lägga till kategori(er) i listboxen
        protected void ButtonCatAdd_OnClick(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(DropDownListCategories.SelectedValue))
            {
                if (!ListBoxCatInAsso.Items.Contains(DropDownListCategories.SelectedItem))
                {
                    ListBoxCatInAsso.Items.Add(DropDownListCategories.SelectedItem);
                }
                else
                {
                    LabelUpdateAsso.Text = "You cannot add the same category twice!";
                    LabelUpdateAsso.Style.Add(HtmlTextWriterStyle.Color, "red");
                }
            }
            else
            {
                LabelUpdateAsso.Text = "You cannot add an empty category! Try again. ";
                LabelUpdateAsso.Style.Add(HtmlTextWriterStyle.Color, "red");
            }
        }


        // För att ta bort kategori(er) i listboxen
        protected void ButtonCatRemove_OnClick(object sender, EventArgs e)
        {
            List<ListItem> itemsToRemove = ListBoxCatInAsso.GetSelectedIndices().Select(index => (ListBoxCatInAsso.Items[index])).ToList();

            foreach (ListItem itemToRemove in itemsToRemove)
            {
                ListBoxCatInAsso.Items.Remove(itemToRemove);
            }
        }
        
        //För att visa alla members i en bull-lista
        protected void lnkbtnMembers_OnClick(object sender, EventArgs e)
        {
            lnkbtnMembers.Text = "Members in " +
                                      AssociationDB.GetAssociationById(int.Parse(ListBoxAsso.SelectedItem.Value)).Name;
            bullListMemberList.Items.Clear();

            List<ListItem> memberList = MemberDB.GetAllMembersByAssociationId
                (AssociationDB.GetAssociationById(int.Parse(ListBoxAsso.SelectedItem.Value)))
                .OrderBy(m => m.SurName)
                .ThenBy(m => m.FirstName)
                .Select(member => new ListItem
            {
                Text = member.FirstName + " " + member.SurName, 
                Value = member.Id.ToString()
            }).ToList();

            foreach (var item in memberList)
            {
                bullListMemberList.Items.Add(item);
            }
        }

        //För att hantera en member - gå till current member view
        protected void bullListMemberList_OnClick(object sender, BulletedListEventArgs e)
        {
            //Visa view när "Show Member"-länken klickas
            MultiViewManageMembers.ActiveViewIndex = 0;
            btnMemberDelete.Visible = true;

            //Labeltext och member info
            lbMemberUpdate.Text = string.Empty;
            lbMembersTitle.Text = "Current Member Information, in " +
                                  AssociationDB.GetAssociationById(int.Parse(ListBoxAsso.SelectedItem.Value)).Name;

            members member = MemberDB.GetMemberById(int.Parse(bullListMemberList.Items[e.Index].Value)); //e.Index på valt item i listan
                
            tbMemberFName.Text = member.FirstName;
            tbMemberSName.Text = member.SurName;
            tbMemberEmail.Text = member.Email;
            tbMemberPhone.Text = member.Phone;
            hdfMemberId.Value = member.Id.ToString(); //spara id i en hiddenfield
        }

        //För att spara ändringar som gjorts för en member 
        //ELLER 
        //för att SKAPA en ny member
        protected void btnMembersSaveChanges_OnClick(object sender, EventArgs e)
        {
            if (hdfMemberId.Value != "-1") //om hiddenfield inte är -1
            {
                //Hitta befintlig member och spara ändringar
                members memberToUpdate = MemberDB.GetMemberById(int.Parse(hdfMemberId.Value));

                memberToUpdate.FirstName = tbMemberFName.Text;
                memberToUpdate.SurName = tbMemberSName.Text;
                memberToUpdate.Email = tbMemberEmail.Text;
                memberToUpdate.Phone = tbMemberPhone.Text;

                //Anropa Update-metoden
                affectedRows = MemberDB.UpdateMember(memberToUpdate);

                if (affectedRows != 0)
                {
                    lbMemberUpdate.Text = "Information about " + tbMemberFName.Text + " " + tbMemberSName.Text +
                                          " has been updated! o(^O^)o";
                    lbMemberUpdate.Style.Add(HtmlTextWriterStyle.Color, "#217ebb");
                }
                else
                {
                    lbMemberUpdate.Text += "Error: Changes might not have been made in " + tbMemberFName.Text +
                                           "... Make sure to set the update information.";
                    lbMemberUpdate.Style.Add(HtmlTextWriterStyle.Color, "red");
                }
            }
            else
            {
                members newMember = new members
                {
                    FirstName = tbMemberFName.Text,
                    SurName = tbMemberSName.Text,
                    Email = tbMemberEmail.Text,
                    Phone = tbMemberPhone.Text,
                    Associations_Id = int.Parse(ListBoxAsso.SelectedItem.Value)
                };

                if (MemberDB.Addmember(newMember))
                {
                    lbMemberUpdate.Text = newMember.FirstName + " " + newMember.SurName + " has been successfully created! (^o^)/";
                    lbMemberUpdate.Style.Add(HtmlTextWriterStyle.Color, "#217ebb");
                }
                else
                {
                    lbMemberUpdate.Text = "Member could not be added. Try again!";
                    lbMemberUpdate.Style.Add(HtmlTextWriterStyle.Color, "red");
                }
            }
        }

        //För att komma till vyn där man skapar en ny member
        protected void lnkbtnAddNewMember_OnClick(object sender, EventArgs e)
        {
            MultiViewManageMembers.ActiveViewIndex = 0;
            hdfMemberId.Value = "-1";
            btnMemberDelete.Visible = false;

            //Rensa textboxarna och label
            tbMemberFName.Text = string.Empty;
            tbMemberSName.Text = string.Empty;
            tbMemberEmail.Text = string.Empty;
            tbMemberPhone.Text = string.Empty;
            lbMemberUpdate.Text = string.Empty;
        }

        protected void btnMemberDelete_OnClick(object sender, EventArgs e)
        {
            //Ta bort den aktuella membern - använda värdet i hiddenfield
            int affectedRows = MemberDB.DeleteMemberById(int.Parse(hdfMemberId.Value));

            if (affectedRows != 0)
            {
                lbMemberUpdate.Text = tbMemberFName.Text + " " + tbMemberSName.Text + " was successfully deleted. T_T ";
                lbMemberUpdate.Style.Add(HtmlTextWriterStyle.Color, "#217ebb");
            }
            else
            {
                lbMemberUpdate.Text = tbMemberFName.Text + " " + tbMemberSName.Text + "could not be deleted. Please try again.";
                lbMemberUpdate.Style.Add(HtmlTextWriterStyle.Color, "red");
            }
        }

        private int affectedRows;

        // För att spara ändringar i Association details - UPDATE-knappen
        protected void ButtonUpdateAsso_OnClick(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(ListBoxAsso.SelectedValue))
            {
                //Hitta Association-Id i listboxen - value
                int assoId = int.Parse(ListBoxAsso.SelectedItem.Value);
                
                // UPPDATERA det nya namnet från textboxen
                associations assoToUpdate = AssociationDB.GetAssociationById(assoId);
                assoToUpdate.Name = TextBoxAssoName.Text;

                //Uppdatera det nya namnet i webpage också
                webpages wpToUpdate = WebPageDB.GetWebPageByAssociationId(int.Parse(hdfAssoId.Value));
                if (wpToUpdate != null)
                {
                    wpToUpdate.Title = TextBoxAssoName.Text;
                    //affectedRows += WebPageDB.UpdateWebPage(wpToUpdate);
                }

                PopulateAssociationListBox(assoId);
                
                //UPPDATERA Description från textboxen
                assoToUpdate.Description = TextBoxAssoDescript.Text;

                // UPPDATERA community i vilken föreningen finns
                assoToUpdate.Communities_Id = int.Parse(DropDownListCommunityInAsso.SelectedItem.Value);

                
                // UPPDATERA ParentAssociation - omflyttningar i strukturen
                associations newParentAsso = new associations();
                
                if (assoToUpdate.ParentAssociationId == null) //assoToUpdate är en parentAsso, flyttar neråt eller under en annan asso
                {
                    if (!string.IsNullOrWhiteSpace(DropDownListParentAsso.SelectedItem.Value))
                    {
                        //Den nya ParentAsso blir den valda i ddl-listan
                        newParentAsso.Id = int.Parse(DropDownListParentAsso.SelectedItem.Value);

                        if (assoToUpdate.Id != newParentAsso.Id) //Får inte välja sig själv
                        {
                            //Om assoToUpdate flyttar till en annan ParentAsso behåller den sina barn
                            if (newParentAsso.ParentAssociationId == null)
                            {
                                //Får ny PAId                                
                                assoToUpdate.ParentAssociationId = newParentAsso.Id;
                            }
                            else //om den flyttar under sig själv eller till en childAsso
                            {
                                //Hitta alla barnen för att släppa dem. De blir alla parentAssos och får PA = null
                                foreach (var subAsso in AssociationDB.GetAllSubAssociationsByParentAssociationId(assoToUpdate.Id))
                                {
                                    subAsso.ParentAssociationId = null;
                                    affectedRows += AssociationDB.UpdateAssociation(subAsso);
                                }
                            }
                        }
                        else
                        {
                            LabelUpdateAsso.Text = TextBoxAssoName.Text +
                                " cannot be Parent Association to itself. Please try again. \n";
                            LabelUpdateAsso.Style.Add(HtmlTextWriterStyle.Color, "red");
                        }
                    }
                    else
                    {
                        //Om man väljer blankt i ddl blir ParentAssociation null
                        assoToUpdate.ParentAssociationId = null;
                    }
                }
                else //assoToUpdate är en childAsso
                {
                    int? oldPAId = assoToUpdate.ParentAssociationId;
                    if (!string.IsNullOrWhiteSpace(DropDownListParentAsso.SelectedItem.Value))
                    {
                        //assoToUpdate får ny PAId, får inte välja sig själv
                        if (assoToUpdate.Id != int.Parse(DropDownListParentAsso.SelectedItem.Value))
                        {
                            assoToUpdate.ParentAssociationId = newParentAsso.Id;
                        }
                        else
                        {
                            LabelUpdateAsso.Text = TextBoxAssoName.Text +
                                                   " cannot be Parent Association to itself. Please try again. \r\n";
                            LabelUpdateAsso.Style.Add(HtmlTextWriterStyle.Color, "red");
                        }
                    }
                    else
                    {
                        //Om man väljer blankt i ddl blir ParentAssociation null
                        assoToUpdate.ParentAssociationId = null;
                    }

                    if (assoToUpdate.ParentAssociationId > oldPAId) //assoToUpdate flyttar neråt
                    {
                        //Hitta barnen och ge dem assoToUpdates gamla PAId
                        foreach (var subAsso in AssociationDB.GetAllSubAssociationsByParentAssociationId(assoToUpdate.Id))
                        {
                            subAsso.ParentAssociationId = oldPAId;
                            affectedRows += AssociationDB.UpdateAssociation(subAsso);
                        }
                    }
                }
                

                // UPPDATERA föreningskategori, lägg till de valda kategorierna från listboxen

                List<categories> catToAddList = new List<categories>();
                List<categories> catToRemoveList = new List<categories>();

                foreach (ListItem addedCategory in ListBoxCatInAsso.Items)
                {
                    categories category = CategoryDB.GetCategoryById(int.Parse(addedCategory.Value));

                    if (assoToUpdate.categories.Count != 0)
                    {
                        catToAddList.AddRange(from item in assoToUpdate.categories where item.Id != category.Id select category);
                    }
                    else
                    {
                        catToAddList.Add(category);
                    }
                }

                foreach (categories addItem in catToAddList)
                {
                    assoToUpdate.categories.Add(addItem);
                }

                // Föreningskategori - REMOVE
                foreach (var removedItem in assoToUpdate.categories)
                {
                    bool exist = false;

                    foreach (ListItem item in ListBoxCatInAsso.Items)
                    {
                        if (item.Value == removedItem.Id.ToString())
                        {
                            exist = true;
                            break;
                        }
                        exist = false;
                    }

                    if (!exist)
                    {
                        catToRemoveList.Add(removedItem);
                    }
                }

                foreach (categories removeItem in catToRemoveList)
                {
                    assoToUpdate.categories.Remove(removeItem);
                }
                
                //UPPDATERA Logo Url
                assoToUpdate.LogoUrl = TextBoxAssoLogoImgUrl.Text;
              
                //Anropa Update-metoden
                affectedRows = AssociationDB.UpdateAssociation(assoToUpdate);
                affectedRows += WebPageDB.UpdateWebPage(wpToUpdate);
                PopulateAssociationListBox();


                if (affectedRows != 0)
                {
                    LabelUpdateAsso.Text = TextBoxAssoName.Text + " has been updated!";
                    LabelUpdateAsso.Style.Add(HtmlTextWriterStyle.Color, "#217ebb");
                }
                else
                {
                    LabelUpdateAsso.Text += "Error: Changes might not have been made in " + TextBoxAssoName.Text +
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

        private int _assoToDeleteId;

        // För att ta bort en association
        protected void ButtonDeleteAsso_OnClick(object sender, EventArgs e)
        {
            _assoToDeleteId = int.Parse(ListBoxAsso.SelectedItem.Value);

            //Om föreningen inte har några underföreningar
            if (AssociationDB.GetAllSubAssociationsByParentAssociationId(_assoToDeleteId).Count == 0)
            {
                //Ta även bort webpage för föreningen
                webpages webpageToDelete = WebPageDB.GetWebPageByAssociationId(_assoToDeleteId);

                //Anropa Delete-metoderna
                if (webpageToDelete != null)
                {
                    int affectedRows =
                        AssociationDB.DeleteAssociationById(_assoToDeleteId) + WebPageDB.DeleteWebPageByAssoId(_assoToDeleteId);

                    PopulateAssociationListBox();

                    if (affectedRows != 0)
                    {
                        LabelAssoInComm.Text = TextBoxAssoName.Text + " was successfully deleted. ";
                        LabelAssoInComm.Style.Add(HtmlTextWriterStyle.Color, "#217ebb");
                        MultiViewAssoDetails.ActiveViewIndex = -1;
                    }
                    else
                    {
                        LabelUpdateAsso.Text = TextBoxAssoName.Text + "could not be deleted. Please try again.";
                        LabelUpdateAsso.Style.Add(HtmlTextWriterStyle.Color, "red");
                    }
                }
            }
            //Visa Delete-view och lista på underföreningar som kommer att tas bort.
            else
            {
                MultiViewAssoDelete.ActiveViewIndex = 0;

                BulletedListSubAssoToDelete.Items.Clear();

                FindSubAssociationsAndAddtoDeleteList(AssociationDB.GetAssociationById(_assoToDeleteId));
            }
        }
        

        //Metod för att hitta alla under- och underföreningar
        protected void FindSubAssociationsAndAddtoDeleteList(associations asso)
        {
            List<ListItem> subAssoDeleteList = new List<ListItem>();

            //Lägg in alla underföreningar som ska tas bort i en itemlista
            foreach (var subasso in AssociationDB.GetAllSubAssociationsByParentAssociationId(asso.Id))
            {
                subAssoDeleteList.Add(new ListItem
                {
                    Text = subasso.Name,
                    Value = subasso.Id.ToString()
                });
            }

            //Lägg underföreningarna i ordning i punktlistan
            foreach (ListItem item in subAssoDeleteList.OrderBy(item => item.Text))
            {
                BulletedListSubAssoToDelete.Items.Add((item));

                //Hitta alla underföreningar och lägg till i listan - rekursiv
                FindSubAssociationsAndAddtoDeleteList(AssociationDB.GetAssociationById(int.Parse(item.Value)));
            }
        }
        
        protected void ButtonDeleteAsso2_OnClick(object sender, EventArgs e)
        {
            //Ta bort den valda föreningen i listboxen
            _assoToDeleteId = int.Parse(ListBoxAsso.SelectedItem.Value);

            int affectedRows = AssociationDB.DeleteAssociationById(_assoToDeleteId) + WebPageDB.DeleteWebPageByAssoId(_assoToDeleteId);
            
            // Ta bort underföreningarna
            foreach (ListItem itemToDelete in BulletedListSubAssoToDelete.Items)
            {
                affectedRows =+ 
                AssociationDB.DeleteAssociationById(int.Parse(itemToDelete.Value)) + WebPageDB.DeleteWebPageByAssoId(int.Parse(itemToDelete.Value));
            }

            if (affectedRows != 0)
            {
                LabelUpdateAsso.Text = TextBoxAssoName.Text + " (and all sub associations) was successfully deleted. ";
                LabelUpdateAsso.Style.Add(HtmlTextWriterStyle.Color, "#217ebb");
            }
            else
            {
                LabelUpdateAsso.Text = TextBoxAssoName.Text + "could not be deleted. Please try again.";
                LabelUpdateAsso.Style.Add(HtmlTextWriterStyle.Color, "red");
            }

            PopulateAssociationListBox();
            MultiViewAssoDelete.ActiveViewIndex = -1;
        }

        protected void ButtonDeleteAssoCancel_OnClick(object sender, EventArgs e)
        {
            MultiViewAssoDelete.ActiveViewIndex = -1;
        }


        #endregion

        
    }
}