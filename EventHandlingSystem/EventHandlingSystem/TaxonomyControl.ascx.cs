using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using EventHandlingSystem.Database;
using Microsoft.Ajax.Utilities;

namespace EventHandlingSystem
{
    public partial class TaxonomyControl : System.Web.UI.UserControl
    {
        #region Page_Load

        protected void Page_Load(object sender, EventArgs e)
        {
            //Återställer Displaytexten.
            LabelDisplay.Text = "";

            if (!IsPostBack)
            {

            }
        }

        #endregion

        protected void Page_PreRender(object sender, EventArgs e)
        {
            LabelTabContentDisplay.Visible = TreeViewTaxonomy.Nodes.Count == 0;

            //Visar CreateBox/EditBox(Div-taggar) på sidan om en View är aktiv i respektive MultiViewControl.
            CreateBox.Visible = MultiViewCreate.ActiveViewIndex != -1;
            EditBox.Visible = MultiViewEdit.ActiveViewIndex != -1;
        }


        #region AddNodesToTreeView

        private void AddNodesToTreeView(TreeView treeView, int taxId)
        {
            //////Hämtar taxonomin.
            ////Taxonomy tax = TaxonomyDB.GetTaxonomyById(taxId);

            //////Om Taxonomin hittas i DBn skapas en HuvudNode med taxonomins namn.
            ////if (tax != null)
            ////{
            ////    //Här skapas HuvudNoden till TreeViewn.
            ////    TreeNode taxNode = new TreeNode
            ////    {
            ////        Text = tax.Name,
            ////        Value = "taxonomy_" + tax.Id,
            ////        ShowCheckBox = true,
            ////        Expanded = true,
            ////        SelectAction = TreeNodeSelectAction.Expand,
            ////        ImageUrl = "~/Images/folder_25x25.png"
            ////    };

            ////    //Lägger till HuvudNoden (ex. Publiceringstaxonomi).
            ////    treeView.Nodes.Add(taxNode);

            ////    //Hämtar alla TermSets som ligger på den översta nivån i taxonomin och sorterar dem efter namn.
            ////    List<TermSet> parentTermSets =
            ////        TermSetDB.GetAllParentTermSetsByTaxonomy(tax).OrderBy(ts => ts.Name).ToList();

            ////    //Lägger till alla ParentNodes (ex. Äspered).
            ////    foreach (var parentTermSet in parentTermSets)
            ////    {
            ////        TreeNode node = new TreeNode
            ////        {
            ////            Text = parentTermSet.Name,
            ////            Value = "termset_" + parentTermSet.Id,
            ////            ShowCheckBox = true,
            ////            SelectAction = TreeNodeSelectAction.Expand,
            ////            ImageUrl = "~/Images/folder_25x25.png"
            ////        };

            ////        //Kallar på en redundant metod för att hitta alla undernoder(Terms & TermSets) till det aktuella TermSet'et.
            ////        FindTermNodesAndAddToTermSetNode(parentTermSet, node);
            ////        taxNode.ChildNodes.Add(node);

            ////        //För att hitta alla ChildNodes till den aktuella ParentNoden.
            ////        FindChildNodesAndAddToParentNode(parentTermSet, node);
            ////    }
            ////}
        }

        #endregion


        #region FindChildNodesAndAddToParentNode

        ////private void FindChildNodesAndAddToParentNode(TermSet termSet, TreeNode parentNode)
        ////{
        ////    //Lägger till alla ChildrenNodes (ex. Vikingen IF(TermSet)).
        ////    foreach (var ts in TermSetDB.GetChildTermSetsByParentTermSetId(termSet.Id).OrderBy(ts => ts.Name).ToList())
        ////    {
        ////        TreeNode childNode = new TreeNode
        ////        {
        ////            Text = ts.Name,
        ////            Value = "termset_" + ts.Id,
        ////            ShowCheckBox = true,
        ////            SelectAction = TreeNodeSelectAction.Expand,
        ////            ImageUrl = "~/Images/folder_25x25.png"
        ////        };

        ////        //Hittar alla terms i ett termset och lägger till dem i TreeView.
        ////        FindTermNodesAndAddToTermSetNode(ts, childNode);

        ////        //Lägger det aktuella TermSet'et.
        ////        parentNode.ChildNodes.Add(childNode);

        ////        //För att hitta alla ChildNodes till den aktuella ParentNoden. 
        ////        //Redundant anropning av metoden görs för att bygga upp hela "grenen".
        ////        FindChildNodesAndAddToParentNode(ts, childNode);
        ////    }
        ////}

        #endregion


        #region FindTermNodesAndAddToTermSetNode

        ////public void FindTermNodesAndAddToTermSetNode(TermSet tSet, TreeNode tNode)
        ////{
        ////    //Hittar alla terms i ett termset och lägger till dem i TreeView.
        ////    foreach (var term in TermDB.GetAllTermsByTermSet(tSet).OrderBy(t => t.Name).ToList())
        ////    {
        ////        TreeNode termNode = new TreeNode
        ////        {
        ////            Text = term.Name,
        ////            Value = "term_" + term.Id,
        ////            ShowCheckBox = true,
        ////            SelectAction = TreeNodeSelectAction.Expand,
        ////            ImageUrl = "~/Images/tag_25x25.png"

        ////        };

        ////        tNode.ChildNodes.Add(termNode);
        ////    }
        ////}

        #endregion


        //Bortkommenterad kod

        #region TreeViewTaxonomy_OnTreeNodeCheckChanged 

        protected void TreeViewTaxonomy_OnTreeNodeCheckChanged(object sender, TreeNodeEventArgs e)
        {
            //Behövs inte då den inte kan köras om man inte gör en manuell postback.
        }


        //Ersattes då TreeView.CheckedNodes fungerar.

        //public void FindCheckedNodesFromAllNodesNodesRecursive(TreeNode parentNode)
        //{
        //    foreach (TreeNode subNode in parentNode.ChildNodes)
        //    {
        //        if (subNode.Checked)
        //        {
        //            CheckedTreeNodes.Add(subNode);
        //        }
        //        FindCheckedNodesFromAllNodesNodesRecursive(subNode);
        //    }
        //}

        #endregion


        #region BtnClearSelected_OnClick

        protected void BtnClearSelected_OnClick(object sender, EventArgs e)
        {
            //Gömmer "Edit view" och "Create view".
            MultiViewEdit.ActiveViewIndex = -1;
            MultiViewCreate.ActiveViewIndex = -1;

            //Uncheckar alla checkade noder i TreeView.
            List<TreeNode> nodes = TreeViewTaxonomy.CheckedNodes.Cast<TreeNode>().ToList();
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
            foreach (TreeNode node in TreeViewTaxonomy.CheckedNodes)
            {
                if (node.Checked)
                    CheckedTreeNodes.Add(node);
            }

            //Ger Displaytexten en röd färg (Displaytexten inneåller endast varningar i denna metoden).
            LabelDisplay.Style.Add(HtmlTextWriterStyle.Color, "red");




            if (TreeViewTaxonomy.Nodes.Count != 0)
            {
                //Hämtar huvudnodens(Taxonomin) value från TreeView.
                string TaxnodeValue = TreeViewTaxonomy.Nodes[0].Value;

                //Delar upp nodeValue till Id delen.
                string TaxstrId = TaxnodeValue.Substring(TaxnodeValue.IndexOf('_') + 1);

                //Delar upp nodeValue till type delen.
                //string type = nodeValue.Substring(0, nodeValue.IndexOf('_'));

                //Om Id inte är samma som PubliceringsTaxonomin visas View där man kan editera.
                if (TaxstrId != "1")
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
                        if (type == "taxonomy" && int.TryParse(strId, out id))
                        {
                            //Ändra till Taxonomy View med alla dess Controls.
                            MultiViewEdit.ActiveViewIndex = 0;

                        ////    Taxonomy tax = TaxonomyDB.GetTaxonomyById(id);
                        ////    LabelIdTax.Text = tax.Id.ToString();
                        ////    TxtBoxNameTax.Text = tax.Name;
                        ////    LabelCreatedTax.Text = "<b>Created:</b> " + tax.Created.ToString("yyyy-MM-dd HH:mm");
                        }
                        else if (type == "termset" && int.TryParse(strId, out id))
                        {
                            //Ändra till TermSet View med alla dess Controls.
                            MultiViewEdit.ActiveViewIndex = 1;

                            ////TermSet tS = TermSetDB.GetTermSetById(id);
                            ////LabelTaxNameTSView.Text = tS.Taxonomy.Name;
                            ////LabelIdTS.Text = tS.Id.ToString();
                            ////TxtBoxNameTS.Text = tS.Name;
                            ////LabelCreatedTS.Text = "<b>Created:</b> " + tS.Created.ToString("yyyy-MM-dd HH:mm");

                            ////DropDownListEditParentTS.Items.Clear();
                            ////DropDownListEditParentTS.Items.Add(new ListItem());
                            ////foreach (var termSet in TermSetDB.GetTermSetsByTaxonomy(tS.Taxonomy))
                            ////{
                            ////    DropDownListEditParentTS.Items.Add(new ListItem
                            ////    {
                            ////        Text = termSet.Name,
                            ////        Value = termSet.Id.ToString()
                            ////    });
                            ////}
                            ////DropDownListEditParentTS.Items.Remove(
                            ////    DropDownListEditParentTS.Items.FindByValue(tS.Id.ToString()));

                            ////DropDownListEditParentTS.SelectedIndex =
                            ////    DropDownListEditParentTS.Items.IndexOf(
                            ////        DropDownListEditParentTS.Items.FindByValue(tS.ParentTermSetId.ToString()));
                        }
                        else if (type == "term" && int.TryParse(strId, out id))
                        {
                            //Ändra till Term View med alla dess Controls.
                            MultiViewEdit.ActiveViewIndex = 2;

                            ////Term t = TermDB.GetTermById(id);
                            ////LabelTaxNameTView.Text = t.TermSet.FirstOrDefault().Taxonomy.Name;
                            ////LabelIdT.Text = t.Id.ToString();
                            ////TxtBoxNameT.Text = t.Name;
                            ////LabelCreatedT.Text = "<b>Created:</b> " + t.Created.ToString("yyyy-MM-dd HH:mm");

                            ////string taxId =
                            ////    TreeViewTaxonomy.Nodes[0].Value.Substring(TreeViewTaxonomy.Nodes[0].Value.IndexOf('_') +
                            ////                                              1);

                            ////DropDownListTermSetForTerm.Items.Clear();
                            ////foreach (
                            ////    var termSet in
                            ////        TermSetDB.GetTermSetsByTaxonomy(TaxonomyDB.GetTaxonomyById(int.Parse(taxId))))
                            ////{
                            ////    DropDownListTermSetForTerm.Items.Add(new ListItem
                            ////    {
                            ////        Text = termSet.Name,
                            ////        Value = termSet.Id.ToString()
                            ////    });
                            ////}

                            ////var firstOrDefault = t.TermSet.FirstOrDefault();
                            ////if (firstOrDefault != null)
                            ////    DropDownListTermSetForTerm.SelectedIndex = DropDownListTermSetForTerm.Items.IndexOf(
                            ////        DropDownListTermSetForTerm.Items.FindByValue(firstOrDefault.Id.ToString()));
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

                        LabelDisplay.Text = "Please select a termset or a term!";
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
                    MultiViewEdit.ActiveViewIndex = -1;

                    LabelDisplay.Text =
                        "You can't edit items in the \"PublishingTaxonomy\".<br /> Creating and editing communities/associations will create/change an publishingTerm/TermSet in the taxonomy.";
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

            //Ger Displaytexten en röd färg (Displaytexten innehåller endast varningar i denna metoden).
            LabelDisplay.Style.Add(HtmlTextWriterStyle.Color, "red");

            if (TreeViewTaxonomy.Nodes.Count != 0)
            {
                //Hämtar huvudnodens(Taxonomin) value från TreeView.
                string nodeValue = TreeViewTaxonomy.Nodes[0].Value;

                //Delar upp nodeValue till Id delen.
                string strId = nodeValue.Substring(nodeValue.IndexOf('_') + 1);

                //Delar upp nodeValue till type delen.
                //string type = nodeValue.Substring(0, nodeValue.IndexOf('_'));

                //Om Id inte är samma som PubliceringsTaxonomin visas View där man kan välja vad man ska skapa.
                if (strId != "1")
                {
                    MultiViewCreate.ActiveViewIndex = 0;
                }
                else
                {
                    MultiViewCreate.ActiveViewIndex = -1;

                    LabelDisplay.Text =
                        "You can't create new termsets/terms in the \"PublishingTaxonomy\".<br /> Creating communities/associations will create an publishingTerm/TermSet in the taxonomy.";
                }

            }
            else
            {
                //Användare måste ha laddat in en taxonomi i TreeViewn.
                LabelDisplay.Text = "Select a taxonomy to create in!";
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

            //Lägger till alla CheckedTreeNodes i listan.
            foreach (TreeNode node in TreeViewTaxonomy.CheckedNodes)
            {
                if (node.Checked) CheckedTreeNodes.Add(node);
            }

            //Ger Displaytexten en röd färg (Display texten inneåller endast varningar i denna metoden).
            LabelDisplay.Style.Add(HtmlTextWriterStyle.Color, "red");


            if (TreeViewTaxonomy.Nodes.Count != 0)
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

            ////Rensar TreeViewn så användaren kan återuppbygga den med de kvarstående objekten.
            //TreeViewTaxonomy.Nodes.Clear();

            if (taxId == 2)
            {
                BtnCategoryTax_OnClick(null, EventArgs.Empty);
            }
            else
            {
                BtnCustomCategoryTax_OnClick(null, EventArgs.Empty);
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


        #region BtnPublishTax_OnClick : BtnCategoryTax_OnClick : BtnCustomCategoryTax_OnClick

        protected void BtnPublishTax_OnClick(object sender, EventArgs e)
        {
            //Rensar den nuvarande TreeViewn och bygger upp PubliceringsTaxonomin.
            TreeViewTaxonomy.Nodes.Clear();
            AddNodesToTreeView(TreeViewTaxonomy, 1);
            BtnPublishTax.CssClass = "tab-btn";
            BtnCategoryTax.CssClass = "tab-btn";
            BtnCustomCategoryTax.CssClass = "tab-btn";
            BtnPublishTax.CssClass += " active";
        }

        protected void BtnCategoryTax_OnClick(object sender, EventArgs e)
        {
            //Rensar den nuvarande TreeViewn och bygger upp KategoriseringsTaxonomin.
            TreeViewTaxonomy.Nodes.Clear();
            AddNodesToTreeView(TreeViewTaxonomy, 2);
            BtnPublishTax.CssClass = "tab-btn";
            BtnCategoryTax.CssClass = "tab-btn";
            BtnCustomCategoryTax.CssClass = "tab-btn";
            BtnCategoryTax.CssClass += " active";
        }

        protected void BtnCustomCategoryTax_OnClick(object sender, EventArgs e)
        {
            //Rensar den nuvarande TreeViewn och bygger upp den AnpassadeKategoriserngsTaxonomin.
            TreeViewTaxonomy.Nodes.Clear();
            AddNodesToTreeView(TreeViewTaxonomy, 3);
            BtnPublishTax.CssClass = "tab-btn";
            BtnCategoryTax.CssClass = "tab-btn";
            BtnCustomCategoryTax.CssClass = "tab-btn";
            BtnCustomCategoryTax.CssClass += " active";
        }

        #endregion




        #region MultiViewEdit

        //Här skickas ändringar av Taxonomi objektet i "EditView" till DBn och skriver ut om editeringen lyckades.

        #region BtnUpdateTax_OnClick

        protected void BtnUpdateTax_OnClick(object sender, EventArgs e)
        {
            ////Taxonomy originalTax = TaxonomyDB.GetTaxonomyById(int.Parse(LabelIdTax.Text));
            ////Taxonomy tax = new Taxonomy
            ////{
            ////    Id = originalTax.Id,
            ////    Name = TxtBoxNameTax.Text,
            ////    Created = originalTax.Created
            ////};

            ////if (TaxonomyDB.UpdateTaxonomy(tax) != 0)
            ////{
            ////    LabelMessageCreateT.Style.Add(HtmlTextWriterStyle.Color, "green");
            ////    LabelMessageTax.Text = "Taxonomy was updated";
            ////}
            ////else
            ////{
            ////    LabelMessageCreateT.Style.Add(HtmlTextWriterStyle.Color, "red");
            ////    LabelMessageTax.Text = "Taxonomy couldn't be updated";
            ////}

            //////Refresh taxonomytreeview.
            ////if (tax.Id == 1)
            ////{
            ////    BtnPublishTax_OnClick(null, EventArgs.Empty);
            ////}
            ////else if (tax.Id == 2)
            ////{
            ////    BtnCategoryTax_OnClick(null, EventArgs.Empty);
            ////}
            ////else
            ////{
            ////    BtnCustomCategoryTax_OnClick(null, EventArgs.Empty);
            ////}
        }

        #endregion


        //Här skickas ändringar av TermSet objektet i "EditView" till DBn och skriver ut om editeringen lyckades.

        #region BtnUpdateTS_OnClick

        protected void BtnUpdateTS_OnClick(object sender, EventArgs e)
        {
            ////TermSet originalTermSet = TermSetDB.GetTermSetById(int.Parse(LabelIdTS.Text));
            ////TermSet tS = new TermSet
            ////{
            ////    Id = originalTermSet.Id,
            ////    Name = TxtBoxNameTS.Text,
            ////    Created = originalTermSet.Created,
            ////    ParentTermSetId =
            ////        DropDownListEditParentTS.SelectedValue == ""
            ////            ? new int?[1] {null}[0]
            ////            : int.Parse(DropDownListEditParentTS.SelectedValue),
            ////    TaxonomyId = originalTermSet.TaxonomyId
            ////};

            ////if (TermSetDB.UpdateTermSet(tS) != 0)
            ////{
            ////    LabelMessageCreateT.Style.Add(HtmlTextWriterStyle.Color, "green");
            ////    LabelMessageTS.Text = "Termset was updated";
            ////}
            ////else
            ////{
            ////    LabelMessageCreateT.Style.Add(HtmlTextWriterStyle.Color, "red");
            ////    LabelMessageTS.Text = "Termset couldn't be updated";
            ////}

            //////Refresh taxonomytreeview.
            ////if (tS.TaxonomyId == 1)
            ////{
            ////    BtnPublishTax_OnClick(null, EventArgs.Empty);
            ////}
            ////else if (tS.TaxonomyId == 2)
            ////{
            ////    BtnCategoryTax_OnClick(null, EventArgs.Empty);
            ////}
            ////else
            ////{
            ////    BtnCustomCategoryTax_OnClick(null, EventArgs.Empty);
            ////}
        }

        #endregion


        //Här skickas ändringar av Term objektet i "EditView" till DBn och skriver ut om editeringen lyckades.

        #region BtnUpdateT_OnClick

        protected void BtnUpdateT_OnClick(object sender, EventArgs e)
        {
            ////Term originalTerm = TermDB.GetTermById(int.Parse(LabelIdT.Text));
            ////Term term = new Term
            ////{
            ////    Id = originalTerm.Id,
            ////    Name = TxtBoxNameT.Text,
            ////    Created = originalTerm.Created,
            ////    TermSet =
            ////        new Collection<TermSet>
            ////        {
            ////            TermSetDB.GetTermSetById(int.Parse(DropDownListTermSetForTerm.SelectedValue))
            ////        }

            ////};

            ////if (TermDB.UpdateTerm(term) != 0)
            ////{
            ////    LabelMessageCreateT.Style.Add(HtmlTextWriterStyle.Color, "green");
            ////    LabelMessageT.Text = "Term was updated";
            ////}
            ////else
            ////{
            ////    LabelMessageCreateT.Style.Add(HtmlTextWriterStyle.Color, "red");
            ////    LabelMessageT.Text = "Term couldn't be updated";
            ////}

            //////Refresh taxonomytreeview.
            ////if (term.TermSet.FirstOrDefault() != null)
            ////{
            ////    if (term.TermSet.FirstOrDefault().TaxonomyId == 1)
            ////    {
            ////        BtnPublishTax_OnClick(null, EventArgs.Empty);
            ////    }
            ////    else if (term.TermSet.FirstOrDefault().TaxonomyId == 2)
            ////    {
            ////        BtnCategoryTax_OnClick(null, EventArgs.Empty);
            ////    }
            ////    else
            ////    {
            ////        BtnCustomCategoryTax_OnClick(null, EventArgs.Empty);
            ////    }
            ////}
        }

        #endregion

        #endregion


        #region MultiViewCreate

        #region Choose

        protected void BtnCreateTerm_OnClick(object sender, EventArgs e)
        {
            LabelMessageCreateT.Text = "";
            TxtBoxNameCreateT.Text = "";

            //Ändra till TaxonomyView med alla dess Controls.
            MultiViewCreate.ActiveViewIndex = 1;

            string nodeValue = TreeViewTaxonomy.Nodes[0].Value;
            string strId = nodeValue.Substring(nodeValue.IndexOf('_') + 1);

            //////h1-tag som får anpassad text beroende på vilken taxonomi man valt att skapa objekt i.
            ////LabelCreateTerm.Text = "";
            ////LabelCreateTerm.Text = "Create new term in " + TaxonomyDB.GetTaxonomyById(int.Parse(strId)).Name;

            //////Tar bort alla ListItems som kanske redan finns i DropDownListan (Annars kan den dubbel populeras).
            ////DropDownListTInTS.Items.Clear();

            //////Lägger till alla TermSet som finns i taxonomin, som ListItem i DropDownListan.
            ////foreach (var termSet in TermSetDB.GetTermSetsByTaxonomy(TaxonomyDB.GetTaxonomyById(int.Parse(strId))))
            ////{
            ////    DropDownListTInTS.Items.Add(new ListItem
            ////    {
            ////        Text = termSet.Name,
            ////        Value = termSet.Id.ToString()
            ////    });
            ////}
        }

        protected void BtnCreateTermSet_OnClick(object sender, EventArgs e)
        {
            LabelMessageCreateTS.Text = "";
            TxtBoxNameCreateTS.Text = "";

            //Ändra till TermSetView med alla dess Controls.
            MultiViewCreate.ActiveViewIndex = 2;

            string nodeValue = TreeViewTaxonomy.Nodes[0].Value;
            string strId = nodeValue.Substring(nodeValue.IndexOf('_') + 1);

            //////h1-tag som får anpassad text beroende på vilken taxonomi man valt att skapa objekt i.
            ////LabelCreateTermSet.Text = "";
            ////LabelCreateTermSet.Text = "Create new termset in " + TaxonomyDB.GetTaxonomyById(int.Parse(strId)).Name;

            //////Tar bort alla ListItems som kanske redan finns i DropDownListan (Annars kan den dubbel populeras).
            ////DropDownListCreateParentTS.Items.Clear();

            //////Lägger till alla TermSet som finns i taxonomin, som ListItem i DropDownListan.
            ////DropDownListCreateParentTS.Items.Add(new ListItem("", "tax_" + strId));
            ////foreach (var termSet in TermSetDB.GetTermSetsByTaxonomy(TaxonomyDB.GetTaxonomyById(int.Parse(strId))))
            ////{
            ////    DropDownListCreateParentTS.Items.Add(new ListItem
            ////    {
            ////        Text = termSet.Name,
            ////        Value = termSet.Id.ToString()
            ////    });
            ////}
        }

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

        #endregion

        protected void ImageButtonBack_OnClick(object sender, ImageClickEventArgs e)
        {
            MultiViewCreate.ActiveViewIndex = 0;
        }

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