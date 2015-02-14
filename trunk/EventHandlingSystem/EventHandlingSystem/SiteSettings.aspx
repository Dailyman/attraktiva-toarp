﻿<%@ Page Title="Site settings" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SiteSettings.aspx.cs" Inherits="EventHandlingSystem.SiteSettings" %>

<%@ Register TagPrefix="aspCategory" TagName="CategoryHandler" Src="CategoryHandlingControl.ascx" %>
<%@ Register TagPrefix="aspCommAsso" TagName="CommAssoHandler" Src="CommunityAssociationControl.ascx" %>
<%@ Register TagPrefix="aspUser" TagName="UserHandler" Src="UserHandlingControl.ascx" %>
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
        <h1>User Manager</h1>
    </div>
    <aspUser:UserHandler ID="UserHandler" runat="server" />

</asp:Content>