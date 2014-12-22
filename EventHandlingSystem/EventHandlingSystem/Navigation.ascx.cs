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

        #region Page_Load()
        protected void Page_Load(object sender, EventArgs e)
        {
            //Om Treeview har noder = Spara TreeViewState till Session.
            if (TreeViewNavigation.Nodes.Count != 0)
            {
                List<string> list = new List<string>();
                SaveTreeViewState(TreeViewNavigation.Nodes, list);
                Session["TreeViewState"] = list;
            }

            // Disable ExpandDepth if the TreeView’s expanded/collapsed 
            // state is stored in session. 
            if (Session["TreeViewState"] != null) TreeViewNavigation.ExpandDepth = -1;

           

            if (!IsPostBack)
            {
                TreeNode selectedNode = null;

                //Session["NavVisible"] = SiteNavMenuList.Style["display"] != "none";

                //LabelDisplay.Text += Request.Path;
                
                    if (Request.QueryString["Type"] == "C")
                    {
                        if (!string.IsNullOrEmpty(Request.QueryString["Id"]))
                        {
                            Session["NavCommunityPubId"] =
                                WebPageDB.GetWebPageById(int.Parse(Request.QueryString["Id"]))
                                    .Community.PublishingTermSetId;
                        }
                    }
                    else if (Request.QueryString["Type"] == "A")
                    {
                        if (!string.IsNullOrEmpty(Request.QueryString["Id"]))
                        {
                            Session["NavCommunityPubId"] =
                                WebPageDB.GetWebPageById(int.Parse(Request.QueryString["Id"]))
                                    .Association.Community.PublishingTermSetId;
                        }
                    }
                    else if(Request.Path == "/default.aspx" )
                    {
                        Session["NavCommunityPubId"] = null;
                    }
                

                //Bygger LeftSideNavigation
                if (Session["NavCommunityPubId"] != null)
                {
                    AddSpecificNodesToTreeView(TreeViewNavigation, int.Parse(Session["NavCommunityPubId"].ToString()));
                }
                else
                {
                    AddAllNodesToTreeView(TreeViewNavigation, 1);
                }

                // Apply the recorded expanded/collapsed state to the TreeView. 
                List<string> list = (List<string>) Session["TreeViewState"];
                if (list != null)
                {
                    RestoreTreeViewState(TreeViewNavigation.Nodes, list);
                }
                
                //Hittar och markerar den aktiva sidan/länken/noden i navigeringen.
                foreach (TreeNode node in TreeViewNavigation.Nodes)
                {
                    selectedNode = (node.NavigateUrl == Request.Url.PathAndQuery) ? node : GetSelectedTreeNodeFromChildNodes(node);
                }

                if (selectedNode != null)
                {
                    ExpandParentNodesForTreeNode(selectedNode);
                }
            }

            if (IsPostBack)
            {
                //Hämtar Display värdet för navigeringen.
                string passedArgument = Request.Params.Get("__EVENTARGUMENT");
                
                if (passedArgument != null)
                {
                    //Kontrollerar att passedArguments värde är relevant.
                    if (passedArgument == "block" || passedArgument == "none")
                    {
                        //Gömmer/visar navigeringen
                        Session["NavVisible"] = passedArgument == "block" ? "true" : "false";
                    }
                }
            }

            //vvv Denna koden måste köras sist i Page_Load()
            //Hämtar navigationens synlighetsstatus från Session.
            bool visible;
            if (Session["NavVisible"] != null && bool.TryParse(Session["NavVisible"].ToString(), out visible))
            {
                //Visar/gömmer navigationen
                SiteNavMenuList.Style["display"] = visible ? "block" : "none";
            }
            else
            {
                //Sparar navigationens synlighetsstatus i Session.
                Session["NavVisible"] = SiteNavMenuList.Style["display"] != "none" ? "true" : "false";
            }
        }
#endregion
        

        #region Testing code (Remove)

        //protected void Page_PreInit(object sender, EventArgs e)
        //{
        //    LabelDisplay.Text += "<br>" + "PreInit"; // <- Remove
        //}

        //protected void Page_PreRender(object sender, EventArgs e)
        //{
        //    LabelDisplay.Text += "<br>" + "PreRender"; // <- Remove
        //}

        //protected virtual void Page_PreRenderComplete(object sender, EventArgs e)
        //{
        //    LabelDisplay.Text += "<br>" + "PreRenderComplete"; // <- Remove
        //}

        //protected void Page_Unload(object sender, EventArgs e)
        //{
        //    LabelDisplay.Text += "<br>" + "Unload"; // <- Remove
        //}

        #endregion


        #region Selecting and Expanding ParentNodes

        //Node som används och skickas tillbaka i GetSelectedTreeNodeFromChildNodes() för att bli startpunkt för expandering av dess ParentNodes.
        private TreeNode sNode = null;

        //Går igenom childnodes för att hitta den aktiva sidan/länken/noden i navigeringen.
        private TreeNode GetSelectedTreeNodeFromChildNodes(TreeNode node)
        {
            if (sNode == null)
            {
                foreach (TreeNode n in node.ChildNodes)
                {
                    if (n.NavigateUrl == Request.Url.PathAndQuery)
                    {
                        sNode = n;
                        break;
                    }
                    GetSelectedTreeNodeFromChildNodes(n);
                }
                return sNode;
            }
            return sNode;
        }

        //Expanerar alla parentnodes i navigeringen för vald node.
        private void ExpandParentNodesForTreeNode(TreeNode node)
        {
            if (node.Parent != null)
            {
                node.Parent.Expand();
                ExpandParentNodesForTreeNode(node.Parent);
            }
        }

        #endregion


        #region TreeViewNavigation_

        //Denna metod används inte.
        protected void TreeViewNavigation_DataBound(object sender, EventArgs e)
        {
            if (Session["TreeViewState"] == null)
            {
                // Record the TreeView’s current expanded/collapsed state. 
                List<string> list = new List<string>();
                SaveTreeViewState(TreeViewNavigation.Nodes, list);
                Session["TreeViewState"] = list;
            }
            else
            {
                // Apply the recorded expanded/collapsed state to 
                // the TreeView. 
                List<string> list = (List<string>) Session["TreeViewState"];
                RestoreTreeViewState(TreeViewNavigation.Nodes, list);
            }
        }

        protected void TreeViewNavigation_TreeNodeCollapsed(object sender, TreeNodeEventArgs e)
        {
            if (IsPostBack)
            {
                List<string> list = new List<string>();
                SaveTreeViewState(TreeViewNavigation.Nodes, list);
                Session["TreeViewState"] = list;
            }
        }

        protected void TreeViewNavigation_TreeNodeExpanded(object sender, TreeNodeEventArgs e)
        {
            if (IsPostBack)
            {
                List<string> list = new List<string>();
                SaveTreeViewState(TreeViewNavigation.Nodes, list);
                Session["TreeViewState"] = list;
            }
        }

        protected void TreeViewNavigation_OnSelectedNodeChanged(object sender, EventArgs e)
        {

        }

        #endregion
        

        #region Save/restoreTreeViewState

        private void SaveTreeViewState(TreeNodeCollection nodes, List<string> list)
        {
            // Recursively record all expanded nodes in the List. 
            foreach (TreeNode node in nodes)
            {
                if (node.ChildNodes != null && node.ChildNodes.Count != 0)
                {
                    if (node.Expanded.HasValue && node.Expanded == true && !String.IsNullOrEmpty(node.Value))
                        list.Add(node.Value);
                    SaveTreeViewState(node.ChildNodes, list);
                }
                else
                {
                    if (node.Expanded.HasValue && node.Expanded == true && !String.IsNullOrEmpty(node.Value))
                        list.Add(node.Value);
                }
            }
        }

        private void RestoreTreeViewState(TreeNodeCollection nodes, List<string> list)
        {
            foreach (TreeNode node in nodes)
            {
                // Restore the state of one node. 
                if (list.Contains(node.Value))
                {
                    if (node.ChildNodes != null && node.ChildNodes.Count != 0 && node.Expanded.HasValue &&
                        node.Expanded == false) node.Expand();
                }
                else if (node.ChildNodes != null && node.ChildNodes.Count != 0 && node.Expanded.HasValue &&
                         node.Expanded == true)
                {
                    node.Collapse();
                }

                // If the node has child nodes, restore their states, too. 
                if (node.ChildNodes != null && node.ChildNodes.Count != 0)
                {
                    RestoreTreeViewState(node.ChildNodes, list);
                }
            }
        }

        #endregion

        
        #region Create&AddNodesToTreeView

        private void AddAllNodesToTreeView(TreeView treeView, int taxId)
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
                List<TermSet> parentTermSets =
                    TermSetDB.GetAllParentTermSetsByTaxonomy(tax).OrderBy(ts => ts.Name).ToList();

                //Lägger till alla ParentNodes (ex. Äspered/Orter).
                foreach (var parentTermSet in parentTermSets)
                {
                    TreeNode node = new TreeNode
                    {
                        Text = parentTermSet.Name,
                        Value = "C-" + parentTermSet.Id.ToString(),
                        Expanded = false,
                        NavigateUrl =
                            "/SitePage.aspx?id=" +
                            CommunityDB.GetCommunityByPublishingTermSetId(parentTermSet.Id).WebPage.Id + "&type=C",
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
        private void AddSpecificNodesToTreeView(TreeView treeView, int comTermSetId)
        {
            TreeViewNavigation.Nodes.Clear();
            //Hämtar TermSet
            TermSet tS = TermSetDB.GetTermSetById(comTermSetId);

            if (tS != null)
            {
                TreeNode startNode = new TreeNode
                {
                    Text = tS.Name,
                    Value = tS.Id.ToString(),
                    Expanded = true,
                    NavigateUrl = "/SitePage.aspx?id=" +
                            CommunityDB.GetCommunityByPublishingTermSetId(tS.Id).WebPage.Id + "&type=C",
                    SelectAction = TreeNodeSelectAction.Select
                };

                //Lägger till HuvudNoden (ex. Publiceringstaxonomi).
                treeView.Nodes.Add(startNode);

                //Hämtar all TermSets som ligger på den översta nivån i taxonomin
                List<TermSet> parentTermSets =
                    TermSetDB.GetChildTermSetsByParentTermSetId(tS.Id).OrderBy(ts => ts.Name).ToList();

                //Lägger till alla HuvudFöreningar (ex. Rödhaken IF).

                FindChildNodesAndAddToParentNode(tS, startNode);

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
                    Value = "A-" + ts.Id.ToString(),
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
                            Value = "Ovrigt-" + parentNode.Value,
                            Expanded = false,
                            SelectAction = TreeNodeSelectAction.Expand
                        };
                        if (!associationTypesNodes.Exists(
                            categoryNode => categoryNode.Value.Equals("Ovrigt-" + parentNode.Value)))
                        {
                            //parentNode.ChildNodes.Add(uncategorized);
                            categoryNodesToAdd.Add(uncategorized, parentNode);
                            associationTypesNodes.Add(uncategorized);
                        }

                        associationTypesNodes.Find(t => t.Value.Equals("Ovrigt-" + parentNode.Value))
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

        #endregion


    }
}