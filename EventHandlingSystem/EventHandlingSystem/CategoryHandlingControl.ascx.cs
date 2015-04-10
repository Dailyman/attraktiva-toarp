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


        private void AddCategoryNodesToTreeView(TreeView treeView)
        {
            treeView.Nodes.Clear();
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
                if (CheckedTreeNodes.Count == 1)
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
                        foreach (var category in CategoryDB.GetAllCategories().OrderBy(c => c.Name))
                        {
                            DropDownListCategoryForSubCategory.Items.Add(new ListItem
                            {
                                Text = category.Name,
                                Value = category.Id.ToString()
                            });
                        }

                        DropDownListCategoryForSubCategory.SelectedIndex =
                            DropDownListCategoryForSubCategory.Items.IndexOf(
                                DropDownListCategoryForSubCategory.Items.FindByValue(sC.categories_Id.ToString()));
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

            DropDownListCreateParentCategory.Items.Clear();
            DropDownListCreateParentCategory.Items.Add(new ListItem());
            foreach (categories category in CategoryDB.GetAllCategories().OrderBy(c => c.Name))
            {
                DropDownListCreateParentCategory.Items.Add(new ListItem(category.Name, category.Id.ToString()));
            }
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

            //Ger Displaytexten en röd färg (Display texten inneåller endast varningar i denna metoden).
            LabelDisplay.Style.Add(HtmlTextWriterStyle.Color, "red");


            if (TreeViewCategories.CheckedNodes.Count > 0)
            {
                //Lägger till alla CheckedTreeNodes i listan.
                foreach (TreeNode node in TreeViewCategories.CheckedNodes)
                {
                    if (node.Checked) CheckedTreeNodes.Add(node);
                }





                bool validObject = false;

                //Här sparas resultatet från "TryParse" av strId.
                int id;
                foreach (var node in CheckedTreeNodes)
                {
                    string nodeValue = node.Value;

                    //Bryter upp värdet i två delar (Type(category eller subcategory) och Id)
                    string type = nodeValue.Substring(0, nodeValue.IndexOf('_'));
                    string strId = nodeValue.Substring(nodeValue.IndexOf('_') + 1);

                    //Kontrollerar att användaren inte försöka ta bort objekt med inkorrekta värden. 
                    if ((type == "category" && int.TryParse(strId, out id)) || (type == "subcategory" && int.TryParse(strId, out id)))
                    {
                        validObject = true;
                    }
                    else
                    {
                        validObject = false;
                        break;
                    }
                }
                if (validObject)
                {
                    ConfirmDeletion(CheckedTreeNodes);
                }
                else
                {
                    LabelDisplay.Text = "Something went wrong when loading what type of object to edit";
                }
            }
            else
            {
                LabelDisplay.Text = "Please select categories to delete.!";
            }

        }

        #endregion


        #region BtnConfirmDeletion_OnClick mm.

        private void ConfirmDeletion(List<TreeNode> nodes)
        {
            //Visar Delete(Edit)View
            MultiViewEdit.ActiveViewIndex = 2;

            //Kommer innehålla nodvärde.
            string nodeValue;

            //Kommer innehålla Id för category/subcategory.
            string strId;

            //Kommer innehålla Type(ex. category eller subcategory).
            string type;

            CheckBoxListItemsToDelete.Items.Clear();
            foreach (var treeNode in nodes)
            {
                //Lägger in nodens värde.
                nodeValue = treeNode.Value;

                //Delar upp nodens värde till Id delen.
                strId = nodeValue.Substring(nodeValue.IndexOf('_') + 1);

                //Delar upp nodens värde till type delen.
                type = nodeValue.Substring(0, nodeValue.IndexOf('_'));

                //Ger texten en röd färg (LabelWarning inneåller endast varningar i denna metoden).
                LabelWarning.Style.Add(HtmlTextWriterStyle.Color, "red");

                //Här tas olika typer av objekt bort på olika sätt.
                int id;
                if (type == "category" && int.TryParse(strId, out id))
                {
                    CheckBoxListItemsToDelete.Items.Add(new ListItem("(Category*) " + treeNode.Text, treeNode.Value));
                    LabelWarning.Text = "* Deleting an category will delete all its subcategories!!";
                }
                else if (type == "subcategory" && int.TryParse(strId, out id))
                {
                    CheckBoxListItemsToDelete.Items.Add(new ListItem("(SubCategory) " + treeNode.Text, treeNode.Value));
                    //LabelWarning.Text = "* Deleting an parent item(category) will delete all child items!!";
                }
                else
                {
                    LabelDisplay.Text = "Something went wrong when loading what type of object to edit";
                }
            }
        }

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
                if (type == "category" && int.TryParse(strId, out id))
                {
                    categories categoryToDelete = CategoryDB.GetCategoryById(id);
                    if (categoryToDelete != null)
                    {
                        if (CategoryDB.DeleteCategoryById(id) != 0) LabelDisplay.Text += "(Category) " + categoryToDelete.Name + ",<br>";
                    }
                    DeleteAllSubCategoriesForCategory(categoryToDelete);
                }
                else if (type == "subcategory" && int.TryParse(strId, out id))
                {
                    subcategories subCategoryToDelete = SubCategoryDB.GetSubCategoryById(id);
                    if (subCategoryToDelete != null)
                    {
                        if (SubCategoryDB.DeleteSubCategoryById(id) != 0)
                            LabelDisplay.Text += "(SubCategory) " + subCategoryToDelete.Name + ",<br>";
                    }
                }
                else
                {
                    LabelDisplay.Text = "Something went wrong when loading what type of object to edit";
                }
            }

            //Refresh treeview.
            AddCategoryNodesToTreeView(TreeViewCategories);
        }

        //Redundant metod för att hitta alla ChildTerm och TermSets för att sedan ta bort dem.
        private void DeleteAllSubCategoriesForCategory(categories category)
        {
            foreach (var subCat in category.subcategories)
            {
                if (SubCategoryDB.DeleteSubCategoryById(subCat.Id) != 0)
                    LabelDisplay.Text += "(SubCategory) " + subCat.Name + ",<br>";
            }
        }

        #endregion


        #region MultiViewEdit


        //Här skickas ändringar av Category objektet i "EditView" till DBn och skriver ut om editeringen lyckades.

        #region BtnUpdateCategory_OnClick

        protected void BtnUpdateCategory_OnClick(object sender, EventArgs e)
        {
            categories updateCategory = CategoryDB.GetCategoryById(int.Parse(LabelIdC.Text));
            updateCategory.Name = TxtBoxNameC.Text;

            if (CategoryDB.UpdateCategory(updateCategory) != 0)
            {
                LabelMessageC.Style.Add(HtmlTextWriterStyle.Color, "green");
                LabelMessageC.Text = "Category was updated";

                //Refresh taxonomytreeview.
                AddCategoryNodesToTreeView(TreeViewCategories);
            }
            else
            {
                LabelMessageC.Style.Add(HtmlTextWriterStyle.Color, "red");
                LabelMessageC.Text = "Category couldn't be updated";
            }
        }

        #endregion

        //Här skickas ändringar av SubCategory objektet i "EditView" till DBn och skriver ut om editeringen lyckades.

        #region BtnUpdateSC_OnClick

        protected void BtnUpdateSC_OnClick(object sender, EventArgs e)
        {
            subcategories updateSubCategory = SubCategoryDB.GetSubCategoryById(int.Parse(LabelIdSC.Text));
            updateSubCategory.Name = TxtBoxNameSC.Text;
            updateSubCategory.categories_Id = int.Parse(DropDownListCategoryForSubCategory.SelectedValue);

            if (SubCategoryDB.UpdateSubCategory(updateSubCategory) != 0)
            {
                LabelMessageSC.Style.Add(HtmlTextWriterStyle.Color, "green");
                LabelMessageSC.Text = "Term was updated";

                //Refresh taxonomytreeview.
                AddCategoryNodesToTreeView(TreeViewCategories);
            }
            else
            {
                LabelMessageSC.Style.Add(HtmlTextWriterStyle.Color, "red");
                LabelMessageSC.Text = "Term couldn't be updated";
            }
        }

        #endregion

        #endregion



        #region BtnCreateCategory/SubCategory

        protected void BtnCreateCategory_OnClick(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(DropDownListCreateParentCategory.SelectedValue))
            {
                categories newCategory = new categories()
                {
                    Name = TxtBoxNameCreateCategoryName.Text
                };
                if (CategoryDB.AddCategory(newCategory))
                {
                    LabelMessageCreateCategory.Style.Add(HtmlTextWriterStyle.Color, "green");
                    LabelMessageCreateCategory.Text = newCategory.Name + " was created!";
                }
                else
                {
                    LabelMessageCreateCategory.Style.Add(HtmlTextWriterStyle.Color, "red");
                    LabelMessageCreateCategory.Text = "Category could not be created!";
                }

            }
            else
            {
                subcategories newSubCategory = new subcategories()
                {
                    Name = TxtBoxNameCreateCategoryName.Text,
                    categories_Id =
                        (CategoryDB.GetCategoryById(int.Parse(DropDownListCreateParentCategory.SelectedValue)) != null
                            ? int.Parse(DropDownListCreateParentCategory.SelectedValue)
                            : 0)
                };

                if (SubCategoryDB.AddSubCategory(newSubCategory))
                {

                    LabelMessageCreateCategory.Style.Add(HtmlTextWriterStyle.Color, "green");
                    LabelMessageCreateCategory.Text = newSubCategory.Name + " was created under " +
                                                      CategoryDB.GetCategoryById(
                                                          int.Parse(DropDownListCreateParentCategory.SelectedValue))
                                                          .Name + "!";
                }
                else
                {
                    LabelMessageCreateCategory.Style.Add(HtmlTextWriterStyle.Color, "red");
                    LabelMessageCreateCategory.Text = "SubCategory could not be created!";
                }

            }

            //Refresh treeview.
            AddCategoryNodesToTreeView(TreeViewCategories);
        }

        #endregion



        #region ImageButtonCloseMultiviews

        protected void ImageButtonCloseMultiViewCreate_OnClick(object sender, ImageClickEventArgs e)
        {
            MultiViewCreate.ActiveViewIndex = -1;
        }

        protected void ImageButtonCloseMultiViewEdit_OnClick(object sender, ImageClickEventArgs e)
        {
            MultiViewEdit.ActiveViewIndex = -1;
        }

        #endregion

        protected void BtnSelectAll_OnClick(object sender, EventArgs e)
        {
            bool allSelected = false;
            foreach (ListItem item in CheckBoxListItemsToDelete.Items)
            {
                if (item.Selected)
                {
                    allSelected = true;
                }
                else
                {
                    allSelected = false;
                    break;
                }
            }

            if (!allSelected)
            {
                foreach (ListItem item in CheckBoxListItemsToDelete.Items)
                {
                    item.Selected = true;
                }
            }
            else
            {
                foreach (ListItem item in CheckBoxListItemsToDelete.Items)
                {
                    item.Selected = false;
                }
            }
        }
    }
}