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
            //Återställer texten.
            LabelDisplay.Text = "";

            if (!IsPostBack)
            {
            }
        }
        #endregion


        #region AddNodesToTreeView
        private void AddNodesToTreeView(TreeView treeView, int taxId)
        {
            //Hämtar taxonomin.
            Taxonomy tax = TaxonomyDB.GetTaxonomyById(taxId);

            if (tax != null)
            {
                TreeNode taxNode = new TreeNode
                {
                    Text = tax.Name,
                    Value = "taxonomy_" + tax.Id,
                    ShowCheckBox = true,
                    Expanded = true,
                    SelectAction = TreeNodeSelectAction.Expand,
                    ImageUrl = "~/Images/folder_25x25.png"
                };
                //Lägger till HuvudNoden (ex. Publiceringstaxonomi).
                treeView.Nodes.Add(taxNode);

                //Hämtar alla TermSets som ligger på den översta nivån i taxonomin och sorterar dem efter namn.
                List<TermSet> parentTermSets =
                    TermSetDB.GetAllParentTermSetsByTaxonomy(tax).OrderBy(ts => ts.Name).ToList();

                //Lägger till alla ParentNodes (ex. Äspered).
                foreach (var parentTermSet in parentTermSets)
                {
                    TreeNode node = new TreeNode
                    {
                        Text = parentTermSet.Name,
                        Value = "termset_" + parentTermSet.Id,
                        ShowCheckBox = true,
                        SelectAction = TreeNodeSelectAction.Expand,
                        ImageUrl = "~/Images/folder_25x25.png"
                    };

                    FindTermNodesAndAddToTermSetNode(parentTermSet, node);
                    taxNode.ChildNodes.Add(node);

                    //För att hitta alla ChildNodes till den aktuella ParentNoden.
                    FindChildNodesAndAddToParentNode(parentTermSet, node);
                }
            }
        }
        #endregion


        #region FindChildNodesAndAddToParentNode
        private void FindChildNodesAndAddToParentNode(TermSet termSet, TreeNode parentNode)
        {
            //Lägger till alla ChildrenNodes (ex. Vikingen IF).
            foreach (var ts in TermSetDB.GetChildTermSetsByParentTermSetId(termSet.Id).OrderBy(ts => ts.Name).ToList())
            {
                TreeNode childNode = new TreeNode
                {
                    Text = ts.Name,
                    Value = "termset_" + ts.Id,
                    ShowCheckBox = true,
                    SelectAction = TreeNodeSelectAction.Expand,
                    ImageUrl = "~/Images/folder_25x25.png"
                };

                FindTermNodesAndAddToTermSetNode(ts, childNode);
                parentNode.ChildNodes.Add(childNode);
                
                //För att hitta alla ChildNodes till den aktuella ParentNoden. 
                //Redundant anropning av metoden görs för att bygga upp hela "grenen".
                FindChildNodesAndAddToParentNode(ts, childNode);
            }
        }
        #endregion


        #region FindTermNodesAndAddToTermSetNode
        public void FindTermNodesAndAddToTermSetNode(TermSet tSet, TreeNode tNode)
        {
            //Hittar alla terms i ett termset och lägger till dem i TreeView.
            foreach (var term in TermDB.GetAllTermsByTermSet(tSet).OrderBy(t => t.Name).ToList())
            {
                TreeNode termNode = new TreeNode
                {
                    Text = term.Name,
                    Value = "term_" + term.Id,
                    ShowCheckBox = true,
                    SelectAction = TreeNodeSelectAction.Expand,
                    ImageUrl = "~/Images/tag_25x25.png"
                    
                };

                tNode.ChildNodes.Add(termNode);
            }
        }
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
                if (node.Checked) CheckedTreeNodes.Add(node);
            }

            //Om listan innehåller ett objekt kommer dess information läggas in i Edit formuläret.
            if (CheckedTreeNodes.Count == 1)
            {
                string nodeValue = CheckedTreeNodes[0].Value;

                string strId = nodeValue.Substring(nodeValue.IndexOf('_') + 1);
                string type = nodeValue.Substring(0, nodeValue.IndexOf('_'));
                
                int id;
                if (type == "taxonomy" && int.TryParse(strId, out id))
                {
                    MultiViewEdit.ActiveViewIndex = 0;
                    Taxonomy tax = TaxonomyDB.GetTaxonomyById(id);
                    LabelIdTax.Text = tax.Id.ToString();
                    TxtBoxNameTax.Text = tax.Name;
                    LabelCreatedTax.Text = "<b>Created:</b> " + tax.Created.ToString("yyyy-MM-dd HH:mm");
                }
                else if (type == "termset" && int.TryParse(strId, out id))
                {
                    MultiViewEdit.ActiveViewIndex = 1;
                    TermSet tS = TermSetDB.GetTermSetById(id);
                    LabelIdTS.Text = tS.Id.ToString();
                    TxtBoxNameTS.Text = tS.Name;
                    LabelCreatedTS.Text = "<b>Created:</b> " + tS.Created.ToString("yyyy-MM-dd HH:mm");

                    DropDownListEditParentTS.Items.Clear();
                    DropDownListEditParentTS.Items.Add(new ListItem());
                    foreach (var termSet in TermSetDB.GetTermSetsByTaxonomy(tS.Taxonomy))
                    {
                        DropDownListEditParentTS.Items.Add(new ListItem
                        {
                            Text = termSet.Name,
                            Value = termSet.Id.ToString()
                        });
                    }
                    DropDownListEditParentTS.Items.Remove(DropDownListEditParentTS.Items.FindByValue(tS.Id.ToString()));

                    DropDownListEditParentTS.SelectedIndex =
                        DropDownListEditParentTS.Items.IndexOf(
                            DropDownListEditParentTS.Items.FindByValue(tS.ParentTermSetId.ToString()));
                }
                else if (type == "term" && int.TryParse(strId, out id))
                {
                    MultiViewEdit.ActiveViewIndex = 2;
                    Term t = TermDB.GetTermById(id);
                    LabelIdT.Text = t.Id.ToString();
                    TxtBoxNameT.Text = t.Name;
                    LabelCreatedT.Text = "<b>Created:</b> " + t.Created.ToString("yyyy-MM-dd HH:mm");

                    string taxId =
                        TreeViewTaxonomy.Nodes[0].Value.Substring(TreeViewTaxonomy.Nodes[0].Value.IndexOf('_') + 1);
                    
                    DropDownListTermSetForTerm.Items.Clear();
                    foreach (
                        var termSet in TermSetDB.GetTermSetsByTaxonomy(TaxonomyDB.GetTaxonomyById(int.Parse(taxId))))
                    {
                        DropDownListTermSetForTerm.Items.Add(new ListItem
                        {
                            Text = termSet.Name,
                            Value = termSet.Id.ToString()
                        });
                    }

                    var firstOrDefault = t.TermSet.FirstOrDefault();
                    if (firstOrDefault != null)
                        DropDownListTermSetForTerm.SelectedIndex = DropDownListTermSetForTerm.Items.IndexOf(
                            DropDownListTermSetForTerm.Items.FindByValue(firstOrDefault.Id.ToString()));
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
        #endregion


        #region BtnCreate_OnClick
        protected void BtnCreate_OnClick(object sender, EventArgs e)
        {
            if (TreeViewTaxonomy.Nodes.Count != 0)
            {
                string nodeValue = TreeViewTaxonomy.Nodes[0].Value;

                string strId = nodeValue.Substring(nodeValue.IndexOf('_') + 1);
                //string type = nodeValue.Substring(0, nodeValue.IndexOf('_'));

                if (strId != "1")
                {
                    MultiViewCreate.ActiveViewIndex = 0;
                }
                else
                {
                    MultiViewCreate.ActiveViewIndex = -1;

                    LabelDisplay.Style.Add(HtmlTextWriterStyle.Color, "red");
                    LabelDisplay.Text =
                        "You can't create new termsets/terms in the publishing taxonomy. Creating communities/associations will create an publishingTerm/TermSet in the taxonomy.";
                }

            }
            else
            {
                LabelDisplay.Style.Add(HtmlTextWriterStyle.Color, "red");
                LabelDisplay.Text = "Select a taxonomy to create in";
            }

        }
        #endregion


        #region BtnDelete_OnClick
        protected void BtnDelete_OnClick(object sender, EventArgs e)
        {
            //Skapas en ny instans av den globala listvariabeln.
            CheckedTreeNodes = new List<TreeNode>();

            //Lägger till alla CheckedTreeNodes i listan.
            foreach (TreeNode node in TreeViewTaxonomy.CheckedNodes)
            {
                if (node.Checked) CheckedTreeNodes.Add(node);
            }

            //Om listan med CheckedNodes innehåller minst ett objekt kommer alla objekt som inte ligger i PubliceringsTaxonomin att tas bort (IsDeleted).
            if (CheckedTreeNodes.Count != 0)
            {
                //Tar ut värdet från det första objektet i listan.
                string nodeValue = CheckedTreeNodes[0].Value;

                //Bryter upp värdet i två delar (Type(taxonomy, termset eller term) och Id)
                string type = nodeValue.Substring(0, nodeValue.IndexOf('_'));
                string strId = nodeValue.Substring(nodeValue.IndexOf('_') + 1);

                LabelDisplay.Style.Add(HtmlTextWriterStyle.Color, "red");
                int id;
                if (type == "taxonomy" && int.TryParse(strId, out id))
                {
                    if (id == 1)
                    {
                        LabelDisplay.Text = "You can't delete items from the \"PublishingTaxonomy\". Deleting communities/associations will remove its publishingTerm/TermSet from the taxonomy.";
                    }
                    else
                    {
                        DeleteAllCheckedItemsInTreeView(CheckedTreeNodes);
                    }
                }
                else if (type == "termset" && int.TryParse(strId, out id))
                {
                    if (TermSetDB.GetTermSetById(id).TaxonomyId == 1)
                    {
                        LabelDisplay.Text = "You can't delete items from the \"PublishingTaxonomy\". Deleting communities/associations will remove its publishingTerm/TermSet from the taxonomy.";
                    }
                    else
                    {
                        DeleteAllCheckedItemsInTreeView(CheckedTreeNodes);
                    }
                }
                else if (type == "term" && int.TryParse(strId, out id))
                {
                    if (TermDB.GetTermById(id).TermSet.ToList()[0].TaxonomyId == 1)
                    {
                        LabelDisplay.Text = "You can't delete items from the \"PublishingTaxonomy\". Deleting communities/associations will remove its publishingTerm/TermSet from the taxonomy.";
                    }
                    else
                    {
                        DeleteAllCheckedItemsInTreeView(CheckedTreeNodes);
                    }
                }
                else
                {
                    LabelDisplay.Text = "Something went wrong when loading what type of object to edit";
                }
            }
            else if (CheckedTreeNodes.Count == 0)
            {
                LabelDisplay.Text = "Please select a termset or a term!";
            }
            else
            {
                LabelDisplay.Text = "Please check one checkbox ONLY!";
            }
        }
        #endregion


        #region DeleteAllCheckedItemsInTreeView
        private void DeleteAllCheckedItemsInTreeView(List<TreeNode> nodes)
        {
            //foreach (TreeNode checkedNode in TreeViewTaxonomy.CheckedNodes)
            //{
            //    LabelDisplay.Text += checkedNode.Value + " ";
            //}

            //För varje TreeNode i listan 'nodes' kontrolleras vilken typ objekt är som ska tas bort.
            foreach (TreeNode treeNode in nodes)
            {
                string nodeValue = treeNode.Value;

                string strId = nodeValue.Substring(nodeValue.IndexOf('_') + 1);
                string type = nodeValue.Substring(0, nodeValue.IndexOf('_'));

                LabelDisplay.Style.Add(HtmlTextWriterStyle.Color, "red");

                //Här tas olika typer av objekt bort på olika sätt.
                int id;
                LabelDisplay.Text += "Deleted: ";
                if (type == "taxonomy" && int.TryParse(strId, out id))
                {
                    //TaxonomyDB.DeleteTaxonomyById(id);
                    //LabelDisplay.Text += id + ", ";
                    LabelDisplay.Text = "A taxonomi can't be deleted!";
                }
                else if (type == "termset" && int.TryParse(strId, out id))
                {
                    //TermSetDB.DeleteTermSetById(id);
                    LabelDisplay.Text += id + ", ";
                }
                else if (type == "term" && int.TryParse(strId, out id))
                {
                    TermDB.DeleteTermById(id);
                    LabelDisplay.Text += id + ", ";
                }
                else
                {
                    LabelDisplay.Text = "Something went wrong when loading what type of object to edit";
                }
            }
            TreeViewTaxonomy.Nodes.Clear();
        }
        #endregion


        #region BtnPublishTax_OnClick : BtnCategoryTax_OnClick : BtnCustomCategoryTax_OnClick
        protected void BtnPublishTax_OnClick(object sender, EventArgs e)
        {
            //Rensar den nuvarande TreeViewn och bygger upp PubliceringsTaxonomin.
            TreeViewTaxonomy.Nodes.Clear();
            AddNodesToTreeView(TreeViewTaxonomy, 1);
        }

        protected void BtnCategoryTax_OnClick(object sender, EventArgs e)
        {
            //Rensar den nuvarande TreeViewn och bygger upp KategoriseringsTaxonomin.
            TreeViewTaxonomy.Nodes.Clear();
            AddNodesToTreeView(TreeViewTaxonomy, 2);
        }

        protected void BtnCustomCategoryTax_OnClick(object sender, EventArgs e)
        {
            //Rensar den nuvarande TreeViewn och bygger upp den AnpassadeKategoriserngsTaxonomin.
            TreeViewTaxonomy.Nodes.Clear();
            AddNodesToTreeView(TreeViewTaxonomy, 3);
        }
        #endregion




        #region MultiViewEdit
        //Här skickas ändringar av Taxonomi objektet i "EditView".
        #region BtnUpdateTax_OnClick
        protected void BtnUpdateTax_OnClick(object sender, EventArgs e)
        {
            Taxonomy originalTax = TaxonomyDB.GetTaxonomyById(int.Parse(LabelIdTax.Text));
            Taxonomy tax = new Taxonomy
            {
                Id = originalTax.Id,
                Name = TxtBoxNameTax.Text,
                Created = originalTax.Created
            };

            LabelMessageTax.Style.Add(HtmlTextWriterStyle.FontSize, "25px");
            LabelMessageTax.Text = TaxonomyDB.UpdateTaxonomy(tax) != 0
                ? "Taxonomy was updated"
                : "Taxonomy couldn't be updated";
        }
        #endregion


        //Här skickas ändringar av TermSet objektet i "EditView".
        #region BtnUpdateTS_OnClick
        protected void BtnUpdateTS_OnClick(object sender, EventArgs e)
        {
            TermSet originalTermSet = TermSetDB.GetTermSetById(int.Parse(LabelIdTS.Text));
            TermSet tS = new TermSet
            {
                Id = originalTermSet.Id,
                Name = TxtBoxNameTS.Text,
                Created = originalTermSet.Created,
                ParentTermSetId = DropDownListEditParentTS.SelectedValue == "" ? new int?[1] { null }[0] : int.Parse(DropDownListEditParentTS.SelectedValue),
                TaxonomyId = originalTermSet.TaxonomyId
            };

            LabelMessageTS.Style.Add(HtmlTextWriterStyle.FontSize, "25px");
            LabelMessageTS.Text = TermSetDB.UpdateTermSet(tS) != 0
                ? "Termset was updated"
                : "Termset couldn't be updated";
        }
        #endregion


        //Här skickas ändringar av Term objektet i "EditView".
        #region BtnUpdateT_OnClick
        protected void BtnUpdateT_OnClick(object sender, EventArgs e)
        {
            Term originalTerm = TermDB.GetTermById(int.Parse(LabelIdT.Text));
            Term term = new Term
            {
                Id = originalTerm.Id,
                Name = TxtBoxNameT.Text,
                Created = originalTerm.Created,
                TermSet = new Collection<TermSet>(){TermSetDB.GetTermSetById(int.Parse(DropDownListTermSetForTerm.SelectedValue))}

            };
            LabelMessageT.Style.Add(HtmlTextWriterStyle.FontSize, "25px");
            LabelMessageT.Text = TermDB.UpdateTerm(term) != 0 ? "Term was updated" : "Term couldn't be updated";
        }
        #endregion
        #endregion


        #region MultiViewCreate
        protected void BtnCreateTerm_OnClick(object sender, EventArgs e)
        {
            MultiViewCreate.ActiveViewIndex = 1;
            string nodeValue = TreeViewTaxonomy.Nodes[0].Value;
            string strId = nodeValue.Substring(nodeValue.IndexOf('_') + 1);

            H2CreateTerm.InnerText = "";
            H2CreateTerm.InnerText = "Create new term in " + TaxonomyDB.GetTaxonomyById(int.Parse(strId)).Name;

            DropDownListTInTS.Items.Clear();
            foreach (var termSet in TermSetDB.GetTermSetsByTaxonomy(TaxonomyDB.GetTaxonomyById(int.Parse(strId))))
            {
                DropDownListTInTS.Items.Add(new ListItem
                {
                    Text = termSet.Name,
                    Value = termSet.Id.ToString()
                });
            }
        }

        protected void BtnCreateTermSet_OnClick(object sender, EventArgs e)
        {
            MultiViewCreate.ActiveViewIndex = 2;

            string nodeValue = TreeViewTaxonomy.Nodes[0].Value;
            string strId = nodeValue.Substring(nodeValue.IndexOf('_') + 1);

            H2CreateTermSet.InnerText = "";
            H2CreateTermSet.InnerText = "Create new termset in " + TaxonomyDB.GetTaxonomyById(int.Parse(strId)).Name;

            DropDownListCreateParentTS.Items.Clear();
            DropDownListCreateParentTS.Items.Add(new ListItem());
            foreach (var termSet in TermSetDB.GetTermSetsByTaxonomy(TaxonomyDB.GetTaxonomyById(int.Parse(strId))))
            {
                DropDownListCreateParentTS.Items.Add(new ListItem
                {
                    Text = termSet.Name,
                    Value = termSet.Id.ToString()
                });
            }
        }

        protected void BtnCreateT_OnClick(object sender, EventArgs e)
        {

            //!!Framtids kod (ಠ ‿  ಠ)!!!
            //ICollection<TermSet> iSets = new Collection<TermSet>() { TermSetDB.GetTermSetById(int.Parse(DropDownListTInTS.SelectedValue)) };
            
            //iSets.Add(TermSetDB.GetTermSetById(int.Parse(DropDownListTInTS.SelectedValue)));

            Term term = new Term
            {
                Name = TxtBoxNameCreateT.Text,
                TermSet = new Collection<TermSet>() { TermSetDB.GetTermSetById(int.Parse(DropDownListTInTS.SelectedValue)) }
                
            };

            if (TermDB.CreateTerm(term) != 0)
            {
                LabelMessageCreateT.Style.Add(HtmlTextWriterStyle.Color, "green");
                LabelMessageCreateT.Text = "Term was created!";
            }
            else
            {
                LabelMessageCreateT.Style.Add(HtmlTextWriterStyle.Color, "red");
                LabelMessageCreateT.Text = "Term was not created!";
            }


            //LabelMessageCreateT.Text = TxtBoxNameCreateT.Text;
        }

        protected void BtnCreateTS_OnClick(object sender, EventArgs e)
        {
            TermSet termSet = new TermSet
            {
                Name = TxtBoxNameCreateTS.Text,
                ParentTermSetId = DropDownListCreateParentTS.SelectedValue == "" ? new int?[1] { null }[0] : int.Parse(DropDownListCreateParentTS.SelectedValue),
                TaxonomyId = TermSetDB.GetTermSetById(int.Parse(DropDownListCreateParentTS.Items[DropDownListCreateParentTS.Items.Count - 1].Value)).TaxonomyId
            };

            if (TermSetDB.CreateTermSet(termSet) != 0)
            {
                LabelMessageCreateTS.Style.Add(HtmlTextWriterStyle.Color, "green");
                LabelMessageCreateTS.Text = "Termset was created!";
            }
            else
            {
                LabelMessageCreateTS.Style.Add(HtmlTextWriterStyle.Color, "red");
                LabelMessageCreateTS.Text = "Termset was not created!";
            }
            
            //LabelMessageCreateTS.Text = TxtBoxNameCreateTS.Text;
        }
        #endregion

    }
}