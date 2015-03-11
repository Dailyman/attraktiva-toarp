<%@ Page Title="Site settings" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SiteSettings.aspx.cs" Inherits="EventHandlingSystem.SiteSettings" %>

<%@ Register TagPrefix="aspCategory" TagName="CategoryHandler" Src="CategoryHandlingControl.ascx" %>
<%@ Register TagPrefix="aspCommAsso" TagName="CommAssoHandler" Src="CommunityAssociationControl.ascx" %>
<%@ Register TagPrefix="aspUser" TagName="UserHandler" Src="UserHandlingControl.ascx" %>
<%@ Register tagPrefix="aspWPComponent" tagName="WPComponentHandler" src="WebPageComponentControl.ascx" %>
<%@ Register tagPrefix="aspRolPer" tagName="RolPerHandler" src="RolesPermissionsControl.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">

    <div class="titlebox">
        <h1>Category Manager</h1>
    </div>
    <aspCategory:CategoryHandler runat="server" />

    <br />
    <br />

    <div class="titlebox">
        <h1>Community/Association Manager</h1>
    </div>
    <aspCommAsso:CommAssoHandler runat="server" />
    
    <br />
    <br />

    <div class="titlebox">
        <h1>Webpage/Component Manager</h1>
        </div>
    <aspWPComponent:WPComponentHandler runat="server"/>
    
    <br />
    <br />
    
    <div class="titlebox">
        <h1>Roles and Permissions Manager</h1>
        </div>
    <aspRolPer:RolPerHandler ID="RolPerHandler1" runat="server"/>
    
    <br />
    <br />

    <div class="titlebox">
        <h1>User Manager</h1>
    </div>
    <aspUser:UserHandler ID="UserHandler" runat="server" />

</asp:Content>
