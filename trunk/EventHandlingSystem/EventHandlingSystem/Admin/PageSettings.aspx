<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PageSettings.aspx.cs" Inherits="EventHandlingSystem.Admin.PageSettings" %>

<%@ Register tagPrefix="aspPageManager" tagName="PageManager" src="~/PageSettingsControls/PageManager.ascx" %>
<%@ Register tagPrefix="aspRolePermManager" tagName="RolePermManager" src="~/PageSettingsControls/RolesPermissionsManager.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="DetailErrorLabel" runat="server" Text=""></asp:Label>
    <asp:Label ID="ErrorLabel" runat="server" Text=""></asp:Label>
    <asp:Panel ID="PanelContent" runat="server" Enabled="False" Visible="False">
    <aspPageManager:PageManager ID="PageManager" runat="server"/>
    <br/>
    <aspRolePermManager:RolePermManager ID="RolePermManager" runat="server"/>
    </asp:Panel>
</asp:Content>
