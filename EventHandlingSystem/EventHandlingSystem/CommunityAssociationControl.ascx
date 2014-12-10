<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CommunityAssociationControl.ascx.cs" Inherits="EventHandlingSystem.CommunityAssociationControl" %>
<div class="inner-content-box">
    <h3>Choose a Community</h3>
    <br />
    <asp:DropDownList ID="DropDownListCommunity" runat="server" OnSelectedIndexChanged="DropDownListCommunity_OnSelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
    <br />
    <br />
    <asp:DropDownList ID="DropDownListAssociation" runat="server"></asp:DropDownList>
</div>
