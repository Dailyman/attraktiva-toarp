using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EventHandlingSystem.Database;

namespace EventHandlingSystem
{
    public partial class TaxonomyControl : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LabelDisplay.Text = "";
            if (!IsPostBack)
            {
                AddNodesToTreeView(TreeViewTaxonomy, 1);
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
                    SelectAction = TreeNodeSelectAction.Expand
                };
                //Lägger till HuvudNoden (ex. Publiceringstaxonomi).
                treeView.Nodes.Add(taxNode);

                //Hämtar all TermSets som ligger på den översta nivån i taxonomin
                List<TermSet> parentTermSets = TermSetDB.GetAllParentTermSetsByTaxonomy(tax);
                
                //Lägger till alla ParentNodes (ex. Äspered).
                foreach (var parentTermSet in parentTermSets)
                {
                    TreeNode node = new TreeNode
                    {
                        Text = parentTermSet.Name,
                        Value = parentTermSet.Id.ToString(),
                        ShowCheckBox = true,
                        SelectAction = TreeNodeSelectAction.Expand
                    };
                    taxNode.ChildNodes.Add(node);

                    //För att hitta alla ChildNodes till den aktuella ParentNoden.
                    FindChildAndAddNodesToParentNode(parentTermSet, node);
                    
                }
            }
        }

        private void FindChildAndAddNodesToParentNode(TermSet termSet, TreeNode parentNode)
        {
            //Lägger till alla ChildrenNodes (ex. Vikingen IF).
            foreach (var ts in TermSetDB.GetChildTermSetsByParentTermSetId(termSet.Id))
            {
                TreeNode childNode = new TreeNode
                {
                    Text = ts.Name,
                    Value = ts.Id.ToString(),
                    ShowCheckBox = true,
                    SelectAction = TreeNodeSelectAction.Expand
                };
                
                parentNode.ChildNodes.Add(childNode);

                //För att hitta alla ChildNodes till den aktuella ParentNoden. 
                //Redundant anropning av metoden görs för att bygga upp hela "grenen".
                FindChildAndAddNodesToParentNode(ts, childNode);
            }
            
            
        }

        protected void TreeViewTaxonomy_OnSelectedNodeChanged(object sender, EventArgs e)
        {
            
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
    }
}