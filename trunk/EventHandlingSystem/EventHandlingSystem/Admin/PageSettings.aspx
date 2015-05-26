<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PageSettings.aspx.cs" Inherits="EventHandlingSystem.Admin.PageSettings" %>

<%@ Register TagPrefix="aspPageManager" TagName="PageManager" Src="~/PageSettingsControls/PageManager.ascx" %>
<%@ Register TagPrefix="aspRolePermManager" TagName="RolePermManager" Src="~/PageSettingsControls/RolesPermissionsManager.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Panel ID="PanelPreContent" runat="server" Visible="False">
        <h5>Communities</h5>
        <asp:BulletedList ID="CommPageSettingsList" runat="server" DisplayMode="HyperLink"></asp:BulletedList>
        <h5>Association</h5>
        <asp:BulletedList ID="AssoPageSettingsList" runat="server" DisplayMode="HyperLink"></asp:BulletedList>
    </asp:Panel>

    <asp:Label ID="DetailErrorLabel" runat="server" Text=""></asp:Label>
    <asp:Label ID="ErrorLabel" runat="server" Text=""></asp:Label>
    <asp:Panel ID="PanelContent" runat="server" Enabled="False" Visible="False">
        <aspPageManager:PageManager ID="PageManager" runat="server" />
        <br />
        <aspRolePermManager:RolePermManager ID="RolePermManager" runat="server" />
    </asp:Panel>
</asp:Content>
