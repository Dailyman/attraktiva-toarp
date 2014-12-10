using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EventHandlingSystem.Database;

namespace EventHandlingSystem
{
    public partial class CommunityAssociationControl : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PopulateCommunityDropDownList();
                PopulateAssocationDropDownList();
            }
        }

        List<ListItem> commList = new List<ListItem>();
        List<ListItem> assoList = new List<ListItem>();

        #region Populera Community dropdownlist

        //Lägg in alla communities i dropdownlist
        public void PopulateCommunityDropDownList()
        {
            DropDownListCommunity.Items.Clear();

            foreach (var comm in CommunityDB.GetAllCommunities())
            {
                commList.Add(new ListItem
                {
                    Text = CommunityDB.GetCommunityNameByPublishingTermSetId(comm.PublishingTermSetId),
                    Value = comm.Id.ToString()
                });
            }

            //Sorterar commList i alfabetisk ordning i dropdownlistan för Communities
            foreach (var item in commList.OrderBy(item => item.Text))
            {
                DropDownListCommunity.Items.Add(item);
            }
        }
        #endregion



        public void PopulateAssocationDropDownList()
        {
            DropDownListAssociation.Items.Clear();

            foreach (var asso in AssociationDB.GetAllAssociationsInCommunity(CommunityDB.GetCommunityById(int.Parse(DropDownListCommunity.SelectedItem.Value))))
            {
                assoList.Add(new ListItem
                {
                    Text = AssociationDB.GetAssocationNameByPublishingTermSetId(asso.PublishingTermSetId),
                    Value = asso.Id.ToString()
                });
            }

            //Sorterar assoList i alfabetisk ordning i dropdownlistan för Associations
            foreach (var item in assoList.OrderBy(item => item.Text))
            {
                DropDownListAssociation.Items.Add(item);
            }
        }

        protected void DropDownListCommunity_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            PopulateAssocationDropDownList();
        }
    }
}