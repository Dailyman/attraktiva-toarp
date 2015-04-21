using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EventHandlingSystem.Database;
using MySql.Data.MySqlClient;

namespace EventHandlingSystem
{
    public partial class NavigationBar : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
               PopulateMenu(MenuBar);

               if (MenuBar != null)
               {
                   if(!SelectMenuItem(MenuBar.Items))
                   {
                       SelectMenuItemWithOutUrlQuery(MenuBar.Items);
                   }
               }
            }
        }

        private void PopulateMenu(Menu menu)
        {
            //// Clear the menu so no duplicates is created
            //menu.Items.Clear();

            Menu m = menu;
            MenuItem communityListItem = null;
            foreach (MenuItem item in m.Items)
            {
                if (item.Value.Equals("CommunityFolder"))
                {
                    communityListItem = item;
                    break;
                }
            }

            if (communityListItem == null)
            {
                communityListItem = new MenuItem()
                {
                    NavigateUrl = "",
                    Selectable = false,
                    Text = "Communities",
                    Value = "CommunityList",
                    ToolTip = "List of Communities and Associations"
                };
                m.Items.Add(communityListItem);
            }
            

            foreach (var community in CommunityDB.GetAllCommunities().OrderBy(c => c.Name))
            {
                string webPageId = WebPageDB.GetWebPageByCommunityId(community.Id) != null ? WebPageDB.GetWebPageByCommunityId(community.Id).Id.ToString() : community.Name;
                MenuItem communityItem = new MenuItem(community.Name, community.Id.ToString(), null, "/SitePage.aspx?Id="+webPageId+"&Type=C" );
                communityListItem.ChildItems.Add(communityItem);
                //m.Items.Add(communityItem);

                foreach (var association in AssociationDB.GetAllParentAssociationsByCommunityId(community.Id).OrderBy(a => a.Name))
                {
                    webPageId = WebPageDB.GetWebPageByAssociationId(association.Id) != null ? WebPageDB.GetWebPageByAssociationId(association.Id).Id.ToString() : association.Name;
                    MenuItem associationItem = new MenuItem(association.Name, association.Id.ToString(), null, "/SitePage.aspx?Id=" + webPageId + "&Type=A");
                    communityItem.ChildItems.Add(associationItem);
                    AddChildAssociations(associationItem, association.Id);
                }
            }

        }

        private void AddChildAssociations(MenuItem parentItem, int parentAssoId)
        {
            foreach (var a in AssociationDB.GetAllSubAssociationsByParentAssociationId(parentAssoId).OrderBy(a => a.Name))
            {
                string webPageId = WebPageDB.GetWebPageByAssociationId(a.Id) != null ? WebPageDB.GetWebPageByAssociationId(a.Id).Id.ToString() : a.Name;
                MenuItem childAssociation = new MenuItem(a.Name, a.Id.ToString(), null,
                    "/SitePage.aspx?Id=" + webPageId + "&Type=A");
                parentItem.ChildItems.Add(childAssociation);
                AddChildAssociations(childAssociation, a.Id);
            }
        }

        // FIX THIS SO BOTH TOP MENUITEM AND SELECTED CHILDITEM IS SELECTED!
        private bool SelectMenuItem(MenuItemCollection menuItems)
        {
            foreach (MenuItem item in menuItems)
            {
                string url = ResolveUrl(item.NavigateUrl);
                string pageUrl = Request.Path + ".aspx" + Server.UrlDecode(Request.Url.Query);
                if (pageUrl.Equals(url))
                {
                    item.Selected = true;
                    SelectTopParentMenuItem(item);
                    return true;
                }

                if (SelectMenuItem(item.ChildItems))
                    return true;
            }

            return false;
        }

        // FIX THIS SO BOTH TOP MENUITEM AND SELECTED CHILDITEM IS SELECTED!
        private bool SelectMenuItemWithOutUrlQuery(MenuItemCollection menuItems)
        {
            foreach (MenuItem item in menuItems)
            {
                string url = ResolveUrl(item.NavigateUrl);
                string pageUrl = Request.Path + ".aspx";
                if (pageUrl.Equals(url))
                {
                    item.Selected = true;
                    SelectTopParentMenuItem(item);
                    return true;
                }

                if (SelectMenuItemWithOutUrlQuery(item.ChildItems))
                    return true;
            }

            return false;
        }


        // FIX THIS SO BOTH TOP MENUITEM AND SELECTED CHILDITEM IS SELECTED!
        private bool SelectTopParentMenuItem(MenuItem menuItem)
        {
            if (menuItem.Parent == null)
            {
                menuItem.Selected = true;
                //menuItem.Text = "<div class='selected'>"+menuItem.Text+"</div>";
                return true;
            }
            
                if (SelectTopParentMenuItem(menuItem.Parent))
                {
                   return true; 
                }
            
            return false;
        }
    }
}