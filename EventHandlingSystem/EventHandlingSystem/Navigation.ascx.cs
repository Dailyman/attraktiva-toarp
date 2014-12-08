using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EventHandlingSystem.Database;
using Microsoft.Ajax.Utilities;

namespace EventHandlingSystem
{
    public partial class Navigation1 : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
               AddNodesToTreeView(TreeViewNavigation, 1); 
            }

            //Hittar och markerar den aktiva sidan/länken/noden i navigeringen.
            foreach (TreeNode node in TreeViewNavigation.Nodes)
            {
                if(node.NavigateUrl == Request.Url.PathAndQuery)
                {
                    TreeViewNavigation.FindNode(node.ValuePath).Select();
                }
                SelectTreeNodeByNavUrl(node);
            }
        }

        //Går igenom childnodes för att hitta och markera den aktiva sidan/länken/noden i navigeringen.
        private void SelectTreeNodeByNavUrl(TreeNode node)
        {
            foreach (TreeNode n in node.ChildNodes)
            {
                if (n.NavigateUrl == Request.Url.PathAndQuery)
                {
                    TreeViewNavigation.FindNode(n.ValuePath).Select(); 
                    //LabelDisplay.Text += "<br>" + n.ValuePath + ","; // <- Remove
                }
                SelectTreeNodeByNavUrl(n);
            }
        }

       

        private void AddNodesToTreeView(TreeView treeView, int taxId)
        {
            TreeViewNavigation.Nodes.Clear();
            //Hämtar taxonomin
            Taxonomy tax = TaxonomyDB.GetTaxonomyById(taxId);

            if (tax != null)
            {
                TreeNode startNode = new TreeNode
                {
                    Text = "Communities/Associations",
                    Value = tax.Id.ToString(),
                    Expanded = true,
                    NavigateUrl = "/SitePage.aspx",
                    SelectAction = TreeNodeSelectAction.Select
                };
                //Lägger till HuvudNoden (ex. Publiceringstaxonomi).
                treeView.Nodes.Add(startNode);

                //Hämtar all TermSets som ligger på den översta nivån i taxonomin
                List<TermSet> parentTermSets = TermSetDB.GetAllParentTermSetsByTaxonomy(tax).OrderBy(ts => ts.Name).ToList();

                //Lägger till alla ParentNodes (ex. Äspered/Orter).
                foreach (var parentTermSet in parentTermSets)
                {
                    TreeNode node = new TreeNode
                    {
                        Text = parentTermSet.Name,
                        Value = parentTermSet.Id.ToString(),
                        Expanded = false,
                        NavigateUrl = "/SitePage.aspx?id="+ CommunityDB.GetCommunityByPublishingTermSetId(parentTermSet.Id).WebPage.Id+"&type=C",
                        SelectAction = TreeNodeSelectAction.Select
                    };
                    startNode.ChildNodes.Add(node);

                    //För att hitta alla ChildNodes till den aktuella ParentNoden.
                    FindChildNodesAndAddToParentNode(parentTermSet, node);

                }
                
                foreach (KeyValuePair<TreeNode, TreeNode> item in categoryNodesToAdd.OrderBy(i => i.Key.Text))
                {
                    item.Value.ChildNodes.Add(item.Key);
                }
            }
        }

        private Dictionary<TreeNode, TreeNode> categoryNodesToAdd = new Dictionary<TreeNode, TreeNode>();
        private List<TreeNode> associationTypesNodes = new List<TreeNode>();

        private void FindChildNodesAndAddToParentNode(TermSet termSet, TreeNode parentNode)
        {
            //Lägger till alla ChildrenNodes (ex. Vikingen IF/Föreningar).
            foreach (var ts in TermSetDB.GetChildTermSetsByParentTermSetId(termSet.Id).OrderBy(ts => ts.Name).ToList())
            {
                Association a = AssociationDB.GetAssociationByPublishingTermSetId(ts.Id);

                TreeNode childNode = new TreeNode
                {
                    Text = ts.Name,
                    Value = ts.Id.ToString(),
                    Expanded = false,
                    NavigateUrl = "/SitePage.aspx?id=" + a.WebPage.Id + "&type=A",
                    SelectAction = TreeNodeSelectAction.Select
                };

                if (a.ParentAssociationId == null)
                {
                    if (a.AssociationType == null)
                    {
                        TreeNode uncategorized = new TreeNode()
                        {
                            Text = "Övrigt",
                            Value = "Övrigt-" + parentNode.Value,
                            Expanded = false,
                            SelectAction = TreeNodeSelectAction.Expand
                        };
                        if (!associationTypesNodes.Exists(
                                categoryNode => categoryNode.Value.Equals("Övrigt-" + parentNode.Value)))
                        {
                            //parentNode.ChildNodes.Add(uncategorized);
                            categoryNodesToAdd.Add(uncategorized, parentNode);
                            associationTypesNodes.Add(uncategorized);
                        }

                        associationTypesNodes.Find(t => t.Value.Equals("Övrigt-" + parentNode.Value))
                            .ChildNodes.Add(childNode);
                    }
                    else
                    {
                        string typeName = TermDB.GetTermById((int) a.AssociationType).Name;

                        TreeNode category = new TreeNode()
                        {
                            Text = typeName,
                            Value = typeName + "-" + parentNode.Value,
                            Expanded = false,
                            SelectAction = TreeNodeSelectAction.Expand
                        };

                        if (
                            !associationTypesNodes.Exists(
                                categoryNode => categoryNode.Value.Equals(typeName + "-" + parentNode.Value)))
                        {
                            //parentNode.ChildNodes.Add(category);
                            categoryNodesToAdd.Add(category, parentNode);
                            associationTypesNodes.Add(category);
                        }
                        associationTypesNodes.Find(t => t.Value.Equals(typeName + "-" + parentNode.Value))
                            .ChildNodes.Add(childNode);
                    }
                }
                else
                {
                    parentNode.ChildNodes.Add(childNode);
                }

                //För att hitta alla ChildNodes till den aktuella ParentNoden. 
                //Rekursiv anropning av metoden görs för att bygga upp hela "grenen".
                FindChildNodesAndAddToParentNode(ts, childNode);
            }
        }

        protected void TreeViewNavigation_OnSelectedNodeChanged(object sender, EventArgs e)
        {

        }

        
    }
}