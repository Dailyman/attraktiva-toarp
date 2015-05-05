<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PageSettings.aspx.cs" Inherits="EventHandlingSystem.Admin.PageSettings" %>

<%@ Register tagPrefix="aspPageManager" tagName="PageManager" src="~/PageSettingsControls/PageManager.ascx" %>
<%@ Register tagPrefix="aspRolePermManager" tagName="RolePermManager" src="~/PageSettingsControls/RolesPermissionsManager.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <%--<asp:Label ID="LabelUserName" runat="server" Text=""></asp:Label>--%>
    
    <aspPageManager:PageManager ID="PageManager" runat="server"/>
    <br/>
    <aspRolePermManager:RolePermManager ID="RolePermManager" runat="server"/>
</asp:Content>
