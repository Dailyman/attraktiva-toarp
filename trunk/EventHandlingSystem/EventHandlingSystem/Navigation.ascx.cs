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
                        Session["NavCommunityId"] =
                            WebPageDB.GetWebPageById(int.Parse(Request.QueryString["Id"])).CommunityId.ToString();
                    }
                }
                else if (Request.QueryString["Type"] == "A")
                {
                    if (!string.IsNullOrEmpty(Request.QueryString["Id"]))
                    {
                        Session["NavCommunityId"] =
                            AssociationDB.GetAssociationById((int)WebPageDB.GetWebPageById(int.Parse(Request.QueryString["Id"])).AssociationId).Communities_Id;
                    }
                }
                else if (Request.Path == "/default.aspx")
                {
                    Session["NavCommunityId"] = null;
                }


                //Bygger LeftSideNavigation
                if (Session["NavCommunityId"] != null)
                {
                    AddSpecificNodesToTreeView(TreeViewNavigation, int.Parse(Session["NavCommunityId"].ToString()));
                }
                else
                {
                    AddAllNodesToTreeView(TreeViewNavigation);
                }

                // Apply the recorded expanded/collapsed state to the TreeView.
                List<string> list = (List<string>)Session["TreeViewState"];
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
                List<string> list = (List<string>)Session["TreeViewState"];
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
                if (node.ChildNodes.Count != 0 && node.ChildNodes.Count != 0)
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
                    if (node.ChildNodes.Count != 0 && node.ChildNodes.Count != 0 && node.Expanded.HasValue &&
                        node.Expanded == false) node.Expand();
                }
                else if (node.ChildNodes.Count != 0 && node.ChildNodes.Count != 0 && node.Expanded.HasValue &&
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

        private void AddAllNodesToTreeView(TreeView treeView)
        {
            TreeViewNavigation.Nodes.Clear();

            TreeNode startNode = new TreeNode
            {
                Text = "Communities/Associations",
                Value = "0",
                Expanded = true,
                NavigateUrl = "/SitePage.aspx",
                SelectAction = TreeNodeSelectAction.Select
            };
            //Lägger till HuvudNoden (ex. Publiceringstaxonomi).
            treeView.Nodes.Add(startNode);

            //Hämtar all TermSets som ligger på den översta nivån i taxonomin
            List<communities> communities =
                CommunityDB.GetAllCommunities().OrderBy(com => com.Name).ToList();

            //Lägger till alla ParentNodes (ex. Äspered/Orter).
            foreach (var com in communities)
            {
                TreeNode node = new TreeNode
                {
                    Text = com.Name,
                    Value = "C-" + com.Id,
                    Expanded = false,
                    NavigateUrl =
                        "/SitePage.aspx?id=" + (WebPageDB.GetWebPageById(com.Id) != null ?WebPageDB.GetWebPageById(com.Id).Id.ToString() : "") + "&type=C",
                    SelectAction = TreeNodeSelectAction.Select
                };
                startNode.ChildNodes.Add(node);

                //Hämtar all huvudAssociations in en community.
                List<associations> parentAssociations =
                    AssociationDB.GetAllParentAssociationsByCommunityId(com.Id).OrderBy(asso => asso.Name).ToList();

                //Lägger till alla HuvudFöreningar (ex. Rödhaken IF).
                foreach (var parentAssociation in parentAssociations)
                {
                    //För att hitta alla ChildNodes till den aktuella ParentNoden.
                    AddParentAssociationNodesToCommunityNode(parentAssociation, node);
                }
            }

            foreach (KeyValuePair<TreeNode, TreeNode> item in categoryNodesToAdd.OrderBy(i => i.Key.Text))
            {
                item.Value.ChildNodes.Add(item.Key);
            }

        }



        private void AddSpecificNodesToTreeView(TreeView treeView, int comId)
        {
            TreeViewNavigation.Nodes.Clear();
            //Hämtar Community
            communities com = CommunityDB.GetCommunityById(comId);

            if (com != null)
            {
                TreeNode startNode = new TreeNode
                {
                    Text = com.Name,
                    Value = com.Id.ToString(),
                    Expanded = true,
                    NavigateUrl = "/SitePage.aspx?id=" + (WebPageDB.GetWebPageByCommunityId(comId) != null ? WebPageDB.GetWebPageByCommunityId(comId).Id.ToString() : "") + "&type=C",
                    SelectAction = TreeNodeSelectAction.Select
                };

                //Lägger till HuvudNoden (ex. Dalsjöfors).
                treeView.Nodes.Add(startNode);

                //Hämtar all huvudAssociations in en community.
                List<associations> parentAssociations =
                    AssociationDB.GetAllParentAssociationsByCommunityId(comId).OrderBy(asso => asso.Name).ToList();

                //Lägger till alla HuvudFöreningar (ex. Rödhaken IF).
                foreach (var parentAssociation in parentAssociations)
                {
                    AddParentAssociationNodesToCommunityNode(parentAssociation, startNode);
                }

                foreach (KeyValuePair<TreeNode, TreeNode> item in categoryNodesToAdd.OrderBy(i => i.Key.Text))
                {
                    item.Value.ChildNodes.Add(item.Key);
                }
            }
        }



        private Dictionary<TreeNode, TreeNode> categoryNodesToAdd = new Dictionary<TreeNode, TreeNode>();
        private List<TreeNode> associationTypesNodes = new List<TreeNode>();

        private void AddParentAssociationNodesToCommunityNode(associations parentAsso, TreeNode communityNode)
        {
            //Lägger till alla ParentAssociationNodes (ex. Vikingen IF/Föreningar).


            TreeNode parentAssociationNode = new TreeNode
            {
                Text = parentAsso.Name,
                Value = "A-" + parentAsso.Id,
                Expanded = false,
                NavigateUrl = "/SitePage.aspx?id=" + (WebPageDB.GetWebPageByAssociationId(parentAsso.Id) != null ? WebPageDB.GetWebPageByAssociationId(parentAsso.Id).Id.ToString() : "") + "&type=A",
                SelectAction = TreeNodeSelectAction.Select
            };

            if (AssociationDB.GetAllCategoriesForAssociationByAssociation(parentAsso).Count == 0)
            {
                TreeNode uncategorized = new TreeNode()
                {
                    Text = "Övrigt",
                    Value = "Ovrigt-" + communityNode.Value,
                    Expanded = false,
                    SelectAction = TreeNodeSelectAction.Expand
                };

                if (!associationTypesNodes.Exists(
                    categoryNode => categoryNode.Value.Equals("Ovrigt-" + communityNode.Value)))
                {
                    //parentNode.ChildNodes.Add(uncategorized);
                    categoryNodesToAdd.Add(uncategorized, communityNode);
                    associationTypesNodes.Add(uncategorized);
                }

                associationTypesNodes.Find(t => t.Value.Equals("Ovrigt-" + communityNode.Value))
                    .ChildNodes.Add(parentAssociationNode);
            }
            else
            {
                string typeName = AssociationDB.GetAllCategoriesForAssociationByAssociation(parentAsso)[0].Name;

                TreeNode category = new TreeNode()
                {
                    Text = typeName,
                    Value = typeName + "-" + communityNode.Value,
                    Expanded = false,
                    SelectAction = TreeNodeSelectAction.Expand
                };

                if (
                    !associationTypesNodes.Exists(
                        categoryNode => categoryNode.Value.Equals(typeName + "-" + communityNode.Value)))
                {
                    //parentNode.ChildNodes.Add(category);
                    categoryNodesToAdd.Add(category, communityNode);
                    associationTypesNodes.Add(category);
                }
                associationTypesNodes.Find(t => t.Value.Equals(typeName + "-" + communityNode.Value))
                    .ChildNodes.Add(parentAssociationNode);
            }

            FindSubAssociationsAndAddToParentNode(parentAsso, parentAssociationNode);
        }

        private void FindSubAssociationsAndAddToParentNode(associations asso, TreeNode parentNode)
        {
            foreach (
                    var a in
                        AssociationDB.GetAllSubAssociationsByParentAssociationId(asso.Id).OrderBy(a => a.Name).ToList()
                    )
            {
                TreeNode childAssociationNode = new TreeNode
                {
                    Text = a.Name,
                    Value = "A-" + a.Id,
                    Expanded = false,
                    NavigateUrl = "/SitePage.aspx?id=" + (WebPageDB.GetWebPageByAssociationId(a.Id) != null ? WebPageDB.GetWebPageByAssociationId(a.Id).Id.ToString() : "") + "&type=A",
                    SelectAction = TreeNodeSelectAction.Select
                };

                if (a.ParentAssociationId == null)
                {
                    if (AssociationDB.GetAllCategoriesForAssociationByAssociation(a).Count == 0)
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
                            .ChildNodes.Add(childAssociationNode);
                    }
                    else
                    {
                        string typeName = AssociationDB.GetAllCategoriesForAssociationByAssociation(a)[0].Name;

                        TreeNode category = new TreeNode
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
                            .ChildNodes.Add(childAssociationNode);
                    }
                }
                else
                {
                    parentNode.ChildNodes.Add(childAssociationNode);
                }

                //För att hitta alla ChildAssociationNodes till den aktuella AssociationNoden.
                //Rekursiv anropning av metoden görs för att bygga upp hela "grenen".
                FindSubAssociationsAndAddToParentNode(a, childAssociationNode);
            }
        }

        #endregion


    }
}