using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EventHandlingSystem.Database;

namespace EventHandlingSystem
{
    public partial class CategoryHandlingControl : System.Web.UI.UserControl
    {
        #region Page_Load

        protected void Page_Load(object sender, EventArgs e)
        {
            //Återställer Displaytexten.
            LabelDisplay.Text = "";

            if (!IsPostBack)
            {
                AddCategoryNodesToTreeView(TreeViewCategories);
            }
        }

        #endregion

        protected void Page_PreRender(object sender, EventArgs e)
        {
            //Visar CreateBox/EditBox(Div-taggar) på sidan om en View är aktiv i respektive MultiViewControl.
            CreateBox.Visible = MultiViewCreate.ActiveViewIndex != -1;
            EditBox.Visible = MultiViewEdit.ActiveViewIndex != -1;
        }


        #region AddCategoryNodesToTreeView

        private void AddCategoryNodesToTreeView(TreeView treeView)
        {
            //Om Categories hittas i DBn skapas en HuvudNode med Categories.
            if (CategoryDB.GetAllCategories().Count != 0)
            {
                //Här skapas HuvudNoden till TreeViewn.
                TreeNode titleNode = new TreeNode
                {
                    Text = "Categories",
                    Value = "Categories",
                    ShowCheckBox = false,
                    Expanded = true,
                    SelectAction = TreeNodeSelectAction.Expand,
                    ImageUrl = "~/Images/folder_25x25.png"
                };

                //Lägger till HuvudNoden.
                treeView.Nodes.Add(titleNode);


                foreach (var c in CategoryDB.GetAllCategories().OrderBy(c => c.Name).ToList())
                {
                    TreeNode categoryNode = new TreeNode
                    {
                        Text = c.Name,
                        Value = "category_" + c.Id,
                        ShowCheckBox = true,
                        Expanded = false,
                        SelectAction = TreeNodeSelectAction.Expand,
                        ImageUrl = "~/Images/folder_25x25.png"
                    };

                    titleNode.ChildNodes.Add(categoryNode);

                    //Kallar på en redundant metod för att hitta alla SubCategories till den aktuella Category.
                    FindSubCategoriesAndAddToParentCategoryNode(c, categoryNode);
                }
            }
        }

        #endregion


        #region FindSubCategoriesAndAddToParentCategoryNode

        private void FindSubCategoriesAndAddToParentCategoryNode(categories category, TreeNode categoryNode)
        {
            foreach (var subC in SubCategoryDB.GetAllSubCategoryByCategory(category).OrderBy(subC => subC.Name).ToList()
                )
            {
                TreeNode subCategoryNode = new TreeNode
                {
                    Text = subC.Name,
                    Value = "subcategory_" + subC.Id,
                    ShowCheckBox = true,
                    SelectAction = TreeNodeSelectAction.Expand,
                    ImageUrl = "~/Images/folder_25x25.png"
                };
                //Lägger det aktuella TermSet'et.
                categoryNode.ChildNodes.Add(subCategoryNode);
            }
        }

        #endregion



        #region BtnClearSelected_OnClick

        protected void BtnClearSelected_OnClick(object sender, EventArgs e)
        {
            //Gömmer "Edit view" och "Create view".
            MultiViewEdit.ActiveViewIndex = -1;
            MultiViewCreate.ActiveViewIndex = -1;

            //Uncheckar alla checkade noder i TreeView.
            List<TreeNode> nodes = TreeViewCategories.CheckedNodes.Cast<TreeNode>().ToList();
            foreach (TreeNode node in nodes)
            {
                node.Checked = false;
            }
        }

        #endregion

        //Lista som används för att spara CheckedTreeNodes under körning mellan olika metoder. (En global variabel)
        public static List<TreeNode> CheckedTreeNodes;

        #region BtnEdit_OnClick

        protected void BtnEdit_OnClick(object sender, EventArgs e)
        {
            //Gömmer "Create view".
            MultiViewCreate.ActiveViewIndex = -1;

            //Skapas en ny instans av den globala listvariabeln.
            CheckedTreeNodes = new List<TreeNode>();

            //Lägger till alla CheckedTreeNodes i listan.
            foreach (TreeNode node in TreeViewCategories.CheckedNodes)
            {
                if (node.Checked)
                    CheckedTreeNodes.Add(node);
            }

            //Ger Displaytexten en röd färg (Displaytexten inneåller endast varningar i denna metoden).
            LabelDisplay.Style.Add(HtmlTextWriterStyle.Color, "red");




            if (TreeViewCategories.Nodes.Count != 0)
            {
                //Om listan innehåller ett objekt kommer dess information läggas in i Edit formuläret.
                if (CheckedTreeNodes.Count != 0)
                {
                    string nodeValue = CheckedTreeNodes[0].Value;

                    string strId = nodeValue.Substring(nodeValue.IndexOf('_') + 1);
                    string type = nodeValue.Substring(0, nodeValue.IndexOf('_'));

                    //Här sparas resultatet från "TryParse" av strId.
                    int id;

                    //Kollar vilken typ av node som ska editeras.
                    //Olika Views visas i MultiViewControl beroende på vilken typ som ska editeras.
                    if (type == "category" && int.TryParse(strId, out id))
                    {
                        //Ändra till Category View med alla dess Controls.
                        MultiViewEdit.ActiveViewIndex = 0;

                        categories cate = CategoryDB.GetCategoryById(id);
                        LabelIdC.Text = cate.Id.ToString();
                        TxtBoxNameC.Text = cate.Name;
                    }
                    else if (type == "subcategory" && int.TryParse(strId, out id))
                    {
                        //Ändra till SubCategory View med alla dess Controls.
                        MultiViewEdit.ActiveViewIndex = 1;

                        subcategories sC = SubCategoryDB.GetSubCategoryById(id);
                        LabelIdSC.Text = sC.Id.ToString();
                        TxtBoxNameSC.Text = sC.Name;

                        DropDownListCategoryForSubCategory.Items.Clear();
                        //DropDownListCategoryForSubCategory.Items.Add(new ListItem(){Text = "", Value = ""});
                        foreach (var category in CategoryDB.GetAllCategories())
                        {
                            DropDownListCategoryForSubCategory.Items.Add(new ListItem
                            {
                                Text = category.Name,
                                Value = category.Id.ToString()
                            });
                        }

                        DropDownListCategoryForSubCategory.SelectedIndex =
                            DropDownListCategoryForSubCategory.Items.IndexOf(
                                DropDownListCategoryForSubCategory.Items.FindByValue(sC.Categories_Id.ToString()));
                    }
                    else
                    {
                        //Gömmer "Edit view".
                        MultiViewEdit.ActiveViewIndex = -1;

                        LabelDisplay.Text = "Something went wrong when loading what type of object to edit";
                    }
                }
                else if (CheckedTreeNodes.Count == 0)
                {
                    //Gömmer "Edit view".
                    MultiViewEdit.ActiveViewIndex = -1;

                    LabelDisplay.Text = "Please select a category or a subcategory!";
                }
                else
                {
                    //Gömmer "Edit view".
                    MultiViewEdit.ActiveViewIndex = -1;

                    LabelDisplay.Text = "Please check one checkbox ONLY!";
                }
            }
            else
            {
                //Användare måste ha laddat in en taxonomi i TreeViewn.
                LabelDisplay.Text = "Select a taxonomy to edit in!";
            }
        }

        #endregion


        #region BtnCreate_OnClick

        protected void BtnCreate_OnClick(object sender, EventArgs e)
        {
            //Gömmer "Edit view".
            MultiViewEdit.ActiveViewIndex = -1;

            MultiViewCreate.ActiveViewIndex = 0;
        }

        #endregion


        #region BtnDelete_OnClick

        protected void BtnDelete_OnClick(object sender, EventArgs e)
        {
            //Gömmer "Create view".
            MultiViewCreate.ActiveViewIndex = -1;

            //Gömmer "Edit view".
            MultiViewEdit.ActiveViewIndex = -1;

            //Skapas en ny instans av den globala listvariabeln.
            CheckedTreeNodes = new List<TreeNode>();

            //Lägger till alla CheckedTreeNodes i listan.
            foreach (TreeNode node in TreeViewCategories.CheckedNodes)
            {
                if (node.Checked) CheckedTreeNodes.Add(node);
            }

            //Ger Displaytexten en röd färg (Display texten inneåller endast varningar i denna metoden).
            LabelDisplay.Style.Add(HtmlTextWriterStyle.Color, "red");


            if (TreeViewCategories.Nodes.Count != 0)
            {
                //Om listan med CheckedNodes innehåller minst ett objekt kommer alla objekt som inte ligger i PubliceringsTaxonomin att tas bort (IsDeleted).
                if (CheckedTreeNodes.Count > 0)
                {
                    //Tar ut värdet från det första objektet i listan.
                    string nodeValue = CheckedTreeNodes[0].Value;

                    //Bryter upp värdet i två delar (Type(taxonomy, termset eller term) och Id)
                    string type = nodeValue.Substring(0, nodeValue.IndexOf('_'));
                    string strId = nodeValue.Substring(nodeValue.IndexOf('_') + 1);

                    //Här sparas resultatet från "TryParse" av strId.
                    int id;

                    //Kontrollerar att användaren inte försöka ta bort objekt från Publiseringstaxonomin. 
                    if (type == "taxonomy" && int.TryParse(strId, out id))
                    {
                        if (id == 1)
                        {
                            LabelDisplay.Text =
                                "You can't delete items from the \"PublishingTaxonomy\".<br /> Deleting communities/associations will remove its publishingTerm/TermSet from the taxonomy.";
                        }
                        else
                        {
                            LabelDisplay.Text = "You can't delete taxonomies!";
                            //ConfirmDeletion(CheckedTreeNodes);
                        }
                    }
                    else if (type == "termset" && int.TryParse(strId, out id))
                    {
                        ////if (TermSetDB.GetTermSetById(id).TaxonomyId == 1)
                        ////{
                        ////    LabelDisplay.Text =
                        ////        "You can't delete items from the \"PublishingTaxonomy\".<br /> Deleting communities/associations will remove its publishingTerm/TermSet from the taxonomy.";
                        ////}
                        ////else
                        ////{
                        ////    ConfirmDeletion(CheckedTreeNodes);
                        ////}
                    }
                    else if (type == "term" && int.TryParse(strId, out id))
                    {
                        ////if (TermDB.GetTermById(id).TermSet.ToList()[0].TaxonomyId == 1)
                        ////{
                        ////    LabelDisplay.Text =
                        ////        "You can't delete items from the \"PublishingTaxonomy\".<br /> Deleting communities/associations will remove its publishingTerm/TermSet from the taxonomy.";
                        ////}
                        ////else
                        ////{
                        ////    ////ConfirmDeletion(CheckedTreeNodes);
                        ////}
                    }
                    else
                    {
                        LabelDisplay.Text = "Something went wrong when loading what type of object to edit";
                    }
                }
                else
                {
                    LabelDisplay.Text = "Please select a termset or a term!";
                }
            }
            else
            {
                //Användare måste ha laddat in en taxonomi i TreeViewn.
                LabelDisplay.Text = "Select a taxonomy to delete from!";
            }
        }

        #endregion


        #region BtnConfirmDeletion_OnClick mm.

        ////private void ConfirmDeletion(List<TreeNode> nodes)
        ////{
        ////    //Visar Delete(Edit)View
        ////    MultiViewEdit.ActiveViewIndex = 3;

        ////    //Kommer innehålla nodvärde.
        ////    string nodeValue;

        ////    //Kommer innehålla Id för objekt från taxonomin.
        ////    string strId;

        ////    //Kommer innehålla Type(ex. term, termset eller taxonomi) för objekt från taxonomin.
        ////    string type;

        ////    CheckBoxListItemsToDelete.Items.Clear();
        ////    foreach (var treeNode in nodes)
        ////    {
        ////        //Lägger in nodens värde.
        ////        nodeValue = treeNode.Value;

        ////        //Delar upp nodens värde till Id delen.
        ////        strId = nodeValue.Substring(nodeValue.IndexOf('_') + 1);

        ////        //Delar upp nodens värde till type delen.
        ////        type = nodeValue.Substring(0, nodeValue.IndexOf('_'));

        ////        //Ger texten en röd färg (LabelWarning inneåller endast varningar i denna metoden).
        ////        LabelWarning.Style.Add(HtmlTextWriterStyle.Color, "red");

        ////        //Här tas olika typer av objekt bort på olika sätt.
        ////        int id;
        ////        if (type == "taxonomy" && int.TryParse(strId, out id))
        ////        {
        ////            CheckBoxListItemsToDelete.Items.Add(new ListItem("(Taxonomi*) " + treeNode.Text, treeNode.Value));
        ////            LabelWarning.Text = "* Deleting an parent item(taxonomy or termset) will delete all child items!!";
        ////        }
        ////        else if (type == "termset" && int.TryParse(strId, out id))
        ////        {
        ////            CheckBoxListItemsToDelete.Items.Add(new ListItem("(Termset*) " + treeNode.Text, treeNode.Value));
        ////            LabelWarning.Text = "* Deleting an parent item(taxonomy or termset) will delete all child items!!";
        ////        }
        ////        else if (type == "term" && int.TryParse(strId, out id))
        ////        {
        ////            CheckBoxListItemsToDelete.Items.Add(new ListItem("(Term) " + treeNode.Text, treeNode.Value));
        ////        }
        ////        else
        ////        {
        ////            LabelDisplay.Text = "Something went wrong when loading what type of object to edit";
        ////        }
        ////    }
        ////}

        protected void BtnConfirmDeletion_OnClick(object sender, EventArgs e)
        {
            List<ListItem> items = new List<ListItem>();
            foreach (ListItem item in CheckBoxListItemsToDelete.Items)
            {
                if (item.Selected)
                {
                    items.Add(item);
                }
            }

            //Ta bort checkade objekt i ConfirmDeletionCheckBoxList.
            DeleteAllItemsInList(items);

            //Gömmer Delete(Create)View
            MultiViewEdit.ActiveViewIndex = -1;
        }

        private void DeleteAllItemsInList(List<ListItem> itemsToDelete)
        {
            int taxId = 0;
            LabelDisplay.Text = "Deleted: ";

            //För varje ListItem i listan 'items' kontrolleras vilken typ objekt är som ska tas bort.
            foreach (ListItem itemToDelete in itemsToDelete)
            {
                //Lägger in objektets värde.
                string itemValue = itemToDelete.Value;

                //Delar upp objektets värde till Id delen.
                string strId = itemValue.Substring(itemValue.IndexOf('_') + 1);

                //Delar upp objektets värde till type delen.
                string type = itemValue.Substring(0, itemValue.IndexOf('_'));

                //Ger Displaytexten en röd färg (Displaytexten inneåller endast varningar i denna metoden).
                LabelDisplay.Style.Add(HtmlTextWriterStyle.Color, "red");

                //Här tas olika typer av objekt bort på olika sätt.
                int id;
                if (type == "taxonomy" && int.TryParse(strId, out id))
                {
                    ////Taxonomy taxonomyToDelete = TaxonomyDB.GetTaxonomyById(id);
                    ////foreach (TermSet termSet in TermSetDB.GetTermSetsByTaxonomy(taxonomyToDelete))
                    ////{
                    ////    DeleteAllChildTermsAndTermSetsForTermSet(termSet);
                    ////}
                    ////if (taxonomyToDelete != null)
                    ////{
                    ////    if (TaxonomyDB.DeleteTaxonomyById(id) != 0) LabelDisplay.Text += "(Taxonomy) "+taxonomyToDelete.Name + ",<br>";
                    ////    taxId = taxonomyToDelete.Id;
                    ////}
                }
                else if (type == "termset" && int.TryParse(strId, out id))
                {
                    ////TermSet termSetToDelete = TermSetDB.GetTermSetById(id);
                    ////if (termSetToDelete != null)
                    ////{
                    ////    DeleteAllChildTermsAndTermSetsForTermSet(termSetToDelete);
                    ////    if (TermSetDB.DeleteTermSetById(id) != 0) LabelDisplay.Text += "(Termset) " + termSetToDelete.Name + ",<br>";
                    ////    taxId = termSetToDelete.TaxonomyId;
                    ////}
                }
                else if (type == "term" && int.TryParse(strId, out id))
                {
                    ////Term termToDelete = TermDB.GetTermById(id);
                    ////if (termToDelete != null)
                    ////{
                    ////    if (TermDB.DeleteTermById(id) != 0) LabelDisplay.Text += "(Term) " + termToDelete.Name + ",<br>";
                    ////    taxId = termToDelete.TermSet.FirstOrDefault().TaxonomyId;
                    ////}
                }
                else
                {
                    LabelDisplay.Text = "Something went wrong when loading what type of object to edit";
                }
            }
        }

        //Redundant metod för att hitta alla ChildTerm och TermSets för att sedan ta bort dem.
        ////private void DeleteAllChildTermsAndTermSetsForTermSet(TermSet termSet)
        ////{
        ////    foreach (var t in termSet.Term)
        ////    {
        ////        Term term = TermDB.GetTermById(t.Id);
        ////        if (term != null)
        ////        {
        ////            //Tar bort Terms och visar att de gick att ta bort.
        ////            if (TermDB.DeleteTermById(term.Id) != 0) LabelDisplay.Text += "(Term) " + term.Name + ",<br>";
        ////        }
        ////    }

        ////    foreach (var tS in TermSetDB.GetChildTermSetsByParentTermSetId(termSet.Id))
        ////    {
        ////        DeleteAllChildTermsAndTermSetsForTermSet(tS);

        ////        TermSet termS = TermSetDB.GetTermSetById(tS.Id);
        ////        if (termS != null)
        ////        {
        ////            //Tar bort TermSets och visar att de gick att ta bort.
        ////            if (TermSetDB.DeleteTermSetById(termS.Id) != 0)
        ////                LabelDisplay.Text += "(Term) "+ termS.Name + ",<br>";
        ////        }
        ////    }
        ////}

        #endregion


        #region MultiViewEdit


        //Här skickas ändringar av TermSet objektet i "EditView" till DBn och skriver ut om editeringen lyckades.

        #region BtnUpdateCategory_OnClick

        protected void BtnUpdateCategory_OnClick(object sender, EventArgs e)
        {
            categories updateCategory = CategoryDB.GetCategoryById(int.Parse(LabelIdC.Text));
            updateCategory.Name = TxtBoxNameC.Text;

            if (CategoryDB.UpdateCategory(updateCategory) != 0)
            {
                LabelMessageC.Style.Add(HtmlTextWriterStyle.Color, "green");
                LabelMessageC.Text = "Category was updated";
            }
            else
            {
                LabelMessageC.Style.Add(HtmlTextWriterStyle.Color, "red");
                LabelMessageC.Text = "Category couldn't be updated";
            }
        }

        #endregion


        //Här skickas ändringar av Term objektet i "EditView" till DBn och skriver ut om editeringen lyckades.

        #region BtnUpdateSC_OnClick

        protected void BtnUpdateSC_OnClick(object sender, EventArgs e)
        {
            subcategories updateSubCategory = SubCategoryDB.GetSubCategoryById(int.Parse(LabelIdSC.Text));
            updateSubCategory.Name = TxtBoxNameSC.Text;
            updateSubCategory.Categories_Id = int.Parse(DropDownListCategoryForSubCategory.SelectedValue);

            if (SubCategoryDB.UpdateSubCategory(updateSubCategory) != 0)
            {
                LabelMessageSC.Style.Add(HtmlTextWriterStyle.Color, "green");
                LabelMessageSC.Text = "Term was updated";
            }
            else
            {
                LabelMessageSC.Style.Add(HtmlTextWriterStyle.Color, "red");
                LabelMessageSC.Text = "Term couldn't be updated";
            }
        }

        #endregion

        #endregion


        #region MultiViewCreate

      

        #endregion

        #region Create

        protected void BtnCreateT_OnClick(object sender, EventArgs e)
        {
            //Framtidskod (ಠ ‿  ಠ)!!!
            //ICollection<TermSet> iSets = new Collection<TermSet>() { TermSetDB.GetTermSetById(int.Parse(DropDownListTInTS.SelectedValue)) };

            //iSets.Add(TermSetDB.GetTermSetById(int.Parse(DropDownListTInTS.SelectedValue)));
            if (!string.IsNullOrWhiteSpace(DropDownListTInTS.SelectedValue))
            {

                ////Term term = new Term
                ////{
                ////    Name = TxtBoxNameCreateT.Text,
                ////    TermSet =
                ////        new Collection<TermSet>() {TermSetDB.GetTermSetById(int.Parse(DropDownListTInTS.SelectedValue))}
                ////};

                ////if (TermDB.CreateTerm(term) != 0)
                ////{
                ////    LabelMessageCreateT.Style.Add(HtmlTextWriterStyle.Color, "green");
                ////    LabelMessageCreateT.Text = term.Name + " was created in " + term.TermSet.FirstOrDefault().Name + "!";
                ////}
                ////else
                ////{
                ////    LabelMessageCreateT.Style.Add(HtmlTextWriterStyle.Color, "red");
                ////    LabelMessageCreateT.Text = "Term was not created!";
                ////}

                //////Refresh taxonomytreeview.
                ////if (term.TermSet.FirstOrDefault() != null)
                ////{
                ////    if (term.TermSet.FirstOrDefault().TaxonomyId == 2)
                ////    {
                ////        BtnCategoryTax_OnClick(null, EventArgs.Empty);
                ////    }
                ////    else
                ////    {
                ////        BtnCustomCategoryTax_OnClick(null, EventArgs.Empty);
                ////    }
                ////}

            }
            else
            {
                LabelMessageCreateT.Style.Add(HtmlTextWriterStyle.Color, "red");
                LabelMessageCreateT.Text = "Term was not created! Terms must be placed in a termset.";
            }


        }

        protected void BtnCreateTS_OnClick(object sender, EventArgs e)
        {
            ////TermSet termSet = new TermSet
            ////{
            ////    Name = TxtBoxNameCreateTS.Text,
            ////    ParentTermSetId =
            ////        DropDownListCreateParentTS.SelectedValue.Contains("tax")
            ////            ? new int?[1] {null}[0]
            ////            : int.Parse(DropDownListCreateParentTS.SelectedValue),
            ////    TaxonomyId = DropDownListCreateParentTS.SelectedValue.Contains("tax")
            ////        ? int.Parse(
            ////            DropDownListCreateParentTS.SelectedValue.Substring(
            ////                DropDownListCreateParentTS.SelectedValue.IndexOf('_') + 1))
            ////        : TermSetDB.GetTermSetById(
            ////            int.Parse(DropDownListCreateParentTS.Items[DropDownListCreateParentTS.Items.Count - 1].Value))
            ////            .TaxonomyId
            ////};

            ////if (TermSetDB.CreateTermSet(termSet) != 0)
            ////{
            ////    string parentItem = termSet.ParentTermSetId == null
            ////        ? TaxonomyDB.GetTaxonomyById(termSet.TaxonomyId).Name
            ////        : TermSetDB.GetTermSetById((int) termSet.ParentTermSetId).Name;
            ////    LabelMessageCreateTS.Style.Add(HtmlTextWriterStyle.Color, "green");
            ////    LabelMessageCreateTS.Text = termSet.Name + " was created in " + parentItem + "!";
            ////}
            ////else
            ////{
            ////    LabelMessageCreateTS.Style.Add(HtmlTextWriterStyle.Color, "red");
            ////    LabelMessageCreateTS.Text = "Termset was not created!";
            ////}

            //////Refresh taxonomytreeview.
            ////if (termSet.TaxonomyId == 2)
            ////{
            ////    BtnCategoryTax_OnClick(null, EventArgs.Empty);
            ////}
            ////else
            ////{
            ////    BtnCustomCategoryTax_OnClick(null, EventArgs.Empty);
            ////}
        }

        #endregion




        protected void ImageButtonCloseMultiViewCreate_OnClick(object sender, ImageClickEventArgs e)
        {
            MultiViewCreate.ActiveViewIndex = -1;
        }

        protected void ImageButtonCloseMultiViewEdit_OnClick(object sender, ImageClickEventArgs e)
        {
            MultiViewEdit.ActiveViewIndex = -1;
        }
    }
}