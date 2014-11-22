using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EventHandlingSystem.Database;
using Microsoft.Ajax.Utilities;

namespace EventHandlingSystem
{
    public partial class TaxonomyControl : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
            LabelDisplay.Text = "";
            if (!IsPostBack)
            {
                //AddNodesToTreeView(TreeViewTaxonomy, 1);
                //AddNodesToTreeView(TreeViewTaxonomy, 2);
                //AddNodesToTreeView(TreeViewTaxonomy, 3);
            }
        }

        private void AddNodesToTreeView(TreeView treeView, int taxId)
        {
            //Hämtar taxonomin
            Taxonomy tax = TaxonomyDB.GetTaxonomyById(taxId);

            if (tax != null)
            {
                TreeNode taxNode = new TreeNode
                {
                    Text = tax.Name,
                    Value = tax.Id.ToString(),
                    SelectAction = TreeNodeSelectAction.Expand,
                    ImageUrl = "~/Images/Aux_folder_16x16.gif"
                };
                //Lägger till HuvudNoden (ex. Publiceringstaxonomi).
                treeView.Nodes.Add(taxNode);

                //Hämtar all TermSets som ligger på den översta nivån i taxonomin
                List<TermSet> parentTermSets = TermSetDB.GetAllParentTermSetsByTaxonomy(tax).OrderBy(ts => ts.Name).ToList();
                
                //Lägger till alla ParentNodes (ex. Äspered).
                foreach (var parentTermSet in parentTermSets)
                {
                    TreeNode node = new TreeNode
                    {
                        Text = parentTermSet.Name,
                        Value = parentTermSet.Id.ToString(),
                        ShowCheckBox = true,
                        SelectAction = TreeNodeSelectAction.Expand,
                        ImageUrl = "~/Images/Aux_folder_16x16.gif"
                    };

                    FindTermNodesAndAddToTermSetNode(parentTermSet, node);
                    taxNode.ChildNodes.Add(node);

                    //För att hitta alla ChildNodes till den aktuella ParentNoden.
                    FindChildNodesAndAddToParentNode(parentTermSet, node);
                    
                }
            }
        }

        private void FindChildNodesAndAddToParentNode(TermSet termSet, TreeNode parentNode)
        {
            //Lägger till alla ChildrenNodes (ex. Vikingen IF).
            foreach (var ts in TermSetDB.GetChildTermSetsByParentTermSetId(termSet.Id).OrderBy(ts => ts.Name).ToList())
            {
                TreeNode childNode = new TreeNode
                {
                    Text = ts.Name,
                    Value = ts.Id.ToString(),
                    ShowCheckBox = true,
                    SelectAction = TreeNodeSelectAction.Expand,
                    ImageUrl = "~/Images/Aux_folder_16x16.gif"
                };


                FindTermNodesAndAddToTermSetNode(ts, childNode);
                parentNode.ChildNodes.Add(childNode);
                

                //För att hitta alla ChildNodes till den aktuella ParentNoden. 
                //Redundant anropning av metoden görs för att bygga upp hela "grenen".
                FindChildNodesAndAddToParentNode(ts, childNode);
            }
            
            
        }

        public void FindTermNodesAndAddToTermSetNode(TermSet tSet, TreeNode tNode)
        {
            foreach (var term in TermDB.GetAllTermsByTermSet(tSet))
            {
                TreeNode termNode = new TreeNode
                {
                    Text = term.Name,
                    Value = term.Id.ToString(),
                    ShowCheckBox = true,
                    SelectAction = TreeNodeSelectAction.Expand,
                    ImageUrl = "~/Images/Tag-16_cyan.png"
                };

                tNode.ChildNodes.Add(termNode);
            }
        }


        protected void TreeViewTaxonomy_OnTreeNodeCheckChanged(object sender, TreeNodeEventArgs e)
        {
            //foreach (TreeNode parentNode in TreeViewTaxonomy.Nodes)
            //{
            //    parentNode.Checked = !parentNode.Checked;
            //    CheckAllNodesNodesRecursive(parentNode);
            //}
        }


        public static List<TreeNode> CheckedTreeNodes;

        protected void BtnEdit_OnClick(object sender, EventArgs e)
        {
            CheckedTreeNodes = new List<TreeNode>();
            foreach (TreeNode parentNode in TreeViewTaxonomy.Nodes)
            {
                if (parentNode.Checked)
                {
                    CheckedTreeNodes.Add(parentNode);
                }
                FindCheckedNodesFromAllNodesNodesRecursive(parentNode);
            }
            LabelDisplay.Text = CheckedTreeNodes.Count == 1
                ? CheckedTreeNodes[0].Text
                : "Please select one and only one!";

            //Båda if-alternativen kan inte finnas samtidigt. Den tar den som står först.
            //HUR hittar man terms och termsets från TreeViewTaxonomy (som är en control i aspx)?

            //if (CheckedTreeNodes.Count == 1)
            //{
            //    LabelDisplay.Text = CheckedTreeNodes[0].Text;
            //}
            //else if (CheckedTreeNodes.Count == 0)
            //{
            //    LabelDisplay.Text = "Please select a termset or a term!";
            //}
            //else
            //{
            //    LabelDisplay.Text = "Please check one checkbox ONLY!";
            //}
        }

        public void FindCheckedNodesFromAllNodesNodesRecursive(TreeNode parentNode)
        {
            foreach (TreeNode subNode in parentNode.ChildNodes)
            {
                if (subNode.Checked)
                {
                    CheckedTreeNodes.Add(subNode);
                }
                FindCheckedNodesFromAllNodesNodesRecursive(subNode);
            }
        }

        protected void BtnDelete_OnClick(object sender, EventArgs e)
        {
            foreach (TreeNode node in TreeViewTaxonomy.CheckedNodes)
            {
                LabelDisplay.Text += node.Value + ", ";
            }

            if(!String.IsNullOrEmpty(LabelDisplay.Text))
            {
                LabelDisplay.Text += "has been deleted, or have they...?";
            }
            else
            {
                LabelDisplay.Text += "None has been deleted";
            }
            

        }


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

        public void UncheckAllNodesNodesRecursive(TreeNode parentNode)
        {
            foreach (TreeNode subNode in parentNode.ChildNodes)
            {
                if (subNode.Checked)
                {
                    subNode.Checked = false;
                }
                UncheckAllNodesNodesRecursive(subNode);
            }
        }
    }
}