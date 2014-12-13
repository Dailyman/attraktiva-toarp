<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CommunityAssociationControl.ascx.cs" Inherits="EventHandlingSystem.CommunityAssociationControl" %>
<div class="inner-content-box">
    
    <asp:MultiView ID="MultiViewSelectComm" runat="server" ActiveViewIndex="0">
        <asp:View ID="ViewSelectComm" runat="server">
            <div class="view-select-community">
                <h3>Select a community</h3>
                <br />
                <asp:DropDownList ID="DropDownListCommunity"
                    runat="server"
                    OnSelectedIndexChanged="DropDownListCommunity_OnSelectedIndexChanged"
                    EnableViewState="True"
                    AutoPostBack="True">
                </asp:DropDownList><br />
            </div>
        </asp:View>
    </asp:MultiView>
   <br />

    <asp:MultiView ID="MultiViewCommDetails" runat="server" ActiveViewIndex="-1">
        <asp:View ID="ViewCommDetails" runat="server">
            <div class="view-community-details">
                <span><b>Name: </b>
                    <asp:TextBox ID="TextBoxCommName" Text="" runat="server"></asp:TextBox></span><br />
                <span><b>Link: </b>
                    <asp:HyperLink ID="HyperLinkCommLink" runat="server">
                        <asp:Label ID="LabelCommLink" runat="server"></asp:Label></asp:HyperLink></span>
                <br />
                <br />
                <asp:Label ID="LabelCreated" runat="server" Text=""></asp:Label><br />
                <asp:Label ID="LabelCreatedBy" runat="server" Text=""></asp:Label><br />
                <br />
                <asp:Button ID="ButtonCommSave" runat="server" Text="Save Changes" OnClick="ButtonCommSave_OnClick"/>
            </div><br />

            <div class="view-community-details-association">
                <b><asp:Label ID="LabelAssoInComm" runat="server" Text="Label"></asp:Label></b>
                <br /><br />
                <asp:ListBox ID="ListBoxAsso" OnSelectedIndexChanged="ListBoxAsso_OnSelectedIndexChanged" Width="200px" runat="server"></asp:ListBox>
                <br />
                <br />
                <asp:Button ID="ButtonEditAsso" runat="server" Text="Show Association Details" OnClick="ButtonEditAsso_OnClick" />
                <asp:Label ID="LabelErrorMessage" runat="server" Text=""></asp:Label><br />
            </div>
        </asp:View>
    </asp:MultiView>
    
    <asp:MultiView ID="MultiViewAssoDetails" runat="server" ActiveViewIndex="-1">
        <asp:View ID="ViewAssoDetails" runat="server">
            <br />
            <div class="view-association-details">
                <span><b>Name: </b>
                    <asp:TextBox ID="TextBoxAssoName" Text="" runat="server"></asp:TextBox></span><br />
                <span><b>Community: </b>
                    <asp:DropDownList ID="DropDownListCommListInAsso" runat="server"></asp:DropDownList></span><br />
                <span><b>Parent Association: </b>
                    <asp:DropDownList ID="DropDownListParentAsso" runat="server"></asp:DropDownList></span><br />
                <span><b>Association Type: </b>
                    <asp:DropDownList ID="DropDownListAssoType" runat="server"></asp:DropDownList></span><br />
                <asp:Label ID="LabelCreatedAsso" runat="server" Text=""></asp:Label><br />
                <asp:Label ID="LabelCreatedByAsso" runat="server" Text=""></asp:Label><br />
                <span>Link:
                    <asp:HyperLink ID="HyperLinkAssoLink" runat="server">
                        <asp:Label ID="LabelAssoLink" runat="server"></asp:Label></asp:HyperLink></span><br />
                <asp:Label ID="LabelPTSAsso" runat="server" Text=""></asp:Label><br />
                <span><b>Logo Url: </b>
                    <asp:TextBox ID="LogoUrl" Text="" runat="server"></asp:TextBox></span><br />
                <br />
                <h3>Sub Associations</h3>
                <asp:BulletedList ID="BulletedListSubAssociations" runat="server"></asp:BulletedList>
                <br />
                <asp:Button ID="ButtonUpdateAsso" runat="server" Text="Update" />
                <asp:Button ID="ButtonDeleteAsso" runat="server" Text="Delete this association" />
            </div>
        </asp:View>
    </asp:MultiView>
    <br />
    
</div>
