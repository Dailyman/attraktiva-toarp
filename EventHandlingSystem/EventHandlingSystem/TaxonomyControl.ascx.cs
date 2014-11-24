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
        protected void Page_Load(object sender, EventArgs e)
        {
            //Återställ texten.
            LabelDisplay.Text = "";
            if (!IsPostBack)
            {
                
            }
        }

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
                    SelectAction = TreeNodeSelectAction.Expand,
                    ImageUrl = "~/Images/folder_16x16.png"
                };
                //Lägger till HuvudNoden (ex. Publiceringstaxonomi).
                treeView.Nodes.Add(taxNode);

                //Hämtar all TermSets som ligger på den översta nivån i taxonomin.
                List<TermSet> parentTermSets = TermSetDB.GetAllParentTermSetsByTaxonomy(tax).OrderBy(ts => ts.Name).ToList();
                
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


        protected void TreeViewTaxonomy_OnTreeNodeCheckChanged(object sender, TreeNodeEventArgs e)
        {
            //Behövs inte då den inte kan köras om man inte gör en manuell postback.
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
            //LabelDisplay.Text = CheckedTreeNodes.Count != 1
            //    ? CheckedTreeNodes[0].Text
            //    : "Please select one and only one!";

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
                    TxtBoxIdTax.Text = tax.Id.ToString();
                    TxtBoxNameTax.Text = tax.Name;
                    LabelCreatedTax.Text = "<b>Created:</b> " + tax.Created.ToString("yyyy-MM-dd HH:mm");
                }
                else if (type == "termset" && int.TryParse(strId, out id))
                {
                    MultiViewEdit.ActiveViewIndex = 1;
                    TermSet tS = TermSetDB.GetTermSetById(id);
                    TxtBoxIdTS.Text = tS.Id.ToString();
                    TxtBoxNameTS.Text = tS.Name;
                    LabelCreatedTS.Text = "<b>Created:</b> " + tS.Created.ToString("yyyy-MM-dd HH:mm");
                }
                else if (type == "term" && int.TryParse(strId, out id))
                {
                    MultiViewEdit.ActiveViewIndex = 2;
                    Term t = TermDB.GetTermById(id);
                    TxtBoxIdT.Text = t.Id.ToString();
                    TxtBoxNameT.Text = t.Name;
                    LabelCreatedT.Text = "<b>Created:</b> " + t.Created.ToString("yyyy-MM-dd HH:mm");
                }
                else
                {
                    MultiViewEdit.ActiveViewIndex = -1;
                    LabelDisplay.Text = "Something went wrong when loading what type of object to edit";
                }

            }
            else
            {
                LabelDisplay.Text = "Please select one and ONLY one!";
            }

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

        //Här skickas ändringar av Taxonomi objektet i "EditView".
        protected void BtnUpdateTax_OnClick(object sender, EventArgs e)
        {
            Taxonomy originalTax = TaxonomyDB.GetTaxonomyById(int.Parse(TxtBoxIdTax.Text));
            Taxonomy tax = new Taxonomy
            {
                Id = originalTax.Id,
                Name = TxtBoxNameTax.Text,
                Created = originalTax.Created
            };
            
            LabelMessageTax.Style.Add(HtmlTextWriterStyle.FontSize, "25px");
            LabelMessageTax.Text = TaxonomyDB.UpdateTaxonomy(tax) != 0 ? "Taxonomy was updated" : "Taxonomy couldn't be updated";
        }

        //Här skickas ändringar av TermSet objektet i "EditView".
        protected void BtnUpdateTS_OnClick(object sender, EventArgs e)
        {
            TermSet originalTermSet = TermSetDB.GetTermSetById(int.Parse(TxtBoxIdTS.Text));
            TermSet tS = new TermSet
            {
                Id = originalTermSet.Id,
                Name = TxtBoxNameTS.Text,
                Created = originalTermSet.Created,
                ParentTermSetId = originalTermSet.ParentTermSetId,
                TaxonomyId = originalTermSet.TaxonomyId
            };

            LabelMessageTS.Style.Add(HtmlTextWriterStyle.FontSize, "25px");
            LabelMessageTS.Text = TermSetDB.UpdateTermSet(tS) != 0 ? "Termset was updated" : "Termset couldn't be updated";
        }

        //Här skickas ändringar av Term objektet i "EditView".
        protected void BtnUpdateT_OnClick(object sender, EventArgs e)
        {
            Term originalTerm = TermDB.GetTermById(int.Parse(TxtBoxIdT.Text));
            Term term = new Term
            {
                Id = originalTerm.Id,
                Name = TxtBoxNameT.Text,
                Created = originalTerm.Created
                
            };
            LabelMessageT.Style.Add(HtmlTextWriterStyle.FontSize, "25px");
            LabelMessageT.Text = TermDB.UpdateTerm(term) != 0 ? "Term was updated" : "Term couldn't be updated";
        }
    }
}