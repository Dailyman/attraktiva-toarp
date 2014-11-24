using System;
using System.Collections.Generic;
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
            //Återställ texten.
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
                    ImageUrl = "~/Images/folder_16x16.png"
                };
                //Lägger till HuvudNoden (ex. Publiceringstaxonomi).
                treeView.Nodes.Add(taxNode);

                //Hämtar all TermSets som ligger på den översta nivån i taxonomin.
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
                        ImageUrl = "~/Images/folder_16x16.png"
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
                    ImageUrl = "~/Images/folder_16x16.png"
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
            foreach (var term in TermDB.GetAllTermsByTermSet(tSet))
            {
                TreeNode termNode = new TreeNode
                {
                    Text = term.Name,
                    Value = "term_" + term.Id,
                    ShowCheckBox = true,
                    SelectAction = TreeNodeSelectAction.Expand,
                    ImageUrl = "~/Images/tag-16x16.png"
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


        public static List<TreeNode> CheckedTreeNodes;


        #region BtnEdit_OnClick
        protected void BtnEdit_OnClick(object sender, EventArgs e)
        {
            CheckedTreeNodes = new List<TreeNode>();
            foreach (TreeNode node in TreeViewTaxonomy.CheckedNodes)
            {
                if (node.Checked) CheckedTreeNodes.Add(node);
            }

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
                }
                else if (type == "term" && int.TryParse(strId, out id))
                {
                    MultiViewEdit.ActiveViewIndex = 2;
                    Term t = TermDB.GetTermById(id);
                    LabelIdT.Text = t.Id.ToString();
                    TxtBoxNameT.Text = t.Name;
                    LabelCreatedT.Text = "<b>Created:</b> " + t.Created.ToString("yyyy-MM-dd HH:mm");
                }
                else
                {
                    MultiViewEdit.ActiveViewIndex = -1;
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
        private void DeleteAllCheckedItemsInTreeView(List<TreeNode> nodes )
        {
            //foreach (TreeNode checkedNode in TreeViewTaxonomy.CheckedNodes)
            //{
            //    LabelDisplay.Text += checkedNode.Value + " ";
            //}

            foreach (TreeNode treeNode in nodes)
            {
                string nodeValue = treeNode.Value;

                string strId = nodeValue.Substring(nodeValue.IndexOf('_') + 1);
                string type = nodeValue.Substring(0, nodeValue.IndexOf('_'));

                LabelDisplay.Style.Add(HtmlTextWriterStyle.Color, "red");
                
                int id;
                LabelDisplay.Text += "Deleted: ";
                if (type == "taxonomy" && int.TryParse(strId, out id))
                {
                    //TaxonomyDB.DeleteTaxonomyById(id);
                    //LabelDisplay.Text += id + ", ";
                }
                else if (type == "termset" && int.TryParse(strId, out id))
                {
                    //TermSetDB.DeleteTermSetById(id);
                    //LabelDisplay.Text += id + ", ";
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


        #region BtnDelete_OnClick
        protected void BtnDelete_OnClick(object sender, EventArgs e)
        {
            CheckedTreeNodes = new List<TreeNode>();
            foreach (TreeNode node in TreeViewTaxonomy.CheckedNodes)
            {
                if (node.Checked) CheckedTreeNodes.Add(node);
            }

            if (CheckedTreeNodes.Count != 0)
            {
                string nodeValue = CheckedTreeNodes[0].Value;

                string strId = nodeValue.Substring(nodeValue.IndexOf('_') + 1);
                string type = nodeValue.Substring(0, nodeValue.IndexOf('_'));

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


        #region BtnPublishTax_OnClick : BtnCategoryTax_OnClick : BtnCustomCategoryTax_OnClick
        protected void BtnPublishTax_OnClick(object sender, EventArgs e)
        {
            TreeViewTaxonomy.Nodes.Clear();
            AddNodesToTreeView(TreeViewTaxonomy, 1);
        }

        protected void BtnCategoryTax_OnClick(object sender, EventArgs e)
        {
            TreeViewTaxonomy.Nodes.Clear();
            AddNodesToTreeView(TreeViewTaxonomy, 2);
        }

        protected void BtnCustomCategoryTax_OnClick(object sender, EventArgs e)
        {
            TreeViewTaxonomy.Nodes.Clear();
            AddNodesToTreeView(TreeViewTaxonomy, 3);
        }
        #endregion


        #region BtnClearSelected_OnClick
        protected void BtnClearSelected_OnClick(object sender, EventArgs e)
        {
            foreach (TreeNode parentNode in TreeViewTaxonomy.Nodes)
            {
                if (parentNode.Checked)
                {
                    parentNode.Checked = false;
                }
                UncheckAllNodesNodesRecursive(parentNode);
            }
        }
        #endregion


        #region UncheckAllNodesNodesRecursive
        public void UncheckAllNodesNodesRecursive(TreeNode parentNode)
        {
            //Gömmer "Edit view".
            MultiViewEdit.ActiveViewIndex = -1;

            foreach (TreeNode subNode in parentNode.ChildNodes)
            {
                if (subNode.Checked)
                {
                    subNode.Checked = false;
                }
                UncheckAllNodesNodesRecursive(subNode);
            }
        }
        #endregion


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
                ParentTermSetId = originalTermSet.ParentTermSetId,
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
                Created = originalTerm.Created

            };
            LabelMessageT.Style.Add(HtmlTextWriterStyle.FontSize, "25px");
            LabelMessageT.Text = TermDB.UpdateTerm(term) != 0 ? "Term was updated" : "Term couldn't be updated";
        }
        #endregion
    }
}