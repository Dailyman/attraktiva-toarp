<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CommunityAssociationControl.ascx.cs" Inherits="EventHandlingSystem.CommunityAssociationControl" %>
<div class="inner-content-box">
    <h3>Select a community in the list</h3>
    <br />
    <asp:DropDownList ID="DropDownListCommunity" runat="server" OnSelectedIndexChanged="DropDownListCommunity_OnSelectedIndexChanged" EnableViewState="True" AutoPostBack="True">
        
    </asp:DropDownList>
    <br />
    <br />
    <asp:MultiView ID="MultiViewCommunity" runat="server" ActiveViewIndex="-1">
        <asp:View ID="ViewCommInfo" runat="server">
            <span>Name: <asp:TextBox ID="TextBoxCommName" Text="" runat="server"></asp:TextBox></span>
            <br />
            <span>Link: <asp:HyperLink ID="HyperLinkCommLink" runat="server"><asp:Label ID="LabelCommLink" runat="server" ></asp:Label></asp:HyperLink></span>            
            <br />
            <br />
            <asp:Label ID="LabelCreated" runat="server" Text=""></asp:Label>
            <br />
            <asp:Label ID="LabelCreatedBy" runat="server" Text=""></asp:Label>
            <br />
            <h6>Select an associations in the list</h6>
            <br />
            <asp:DropDownList ID="DropDownListAssociation" runat="server"></asp:DropDownList>
        </asp:View>
    </asp:MultiView>
    <br />
    
</div>
