﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SiteSettings.aspx.cs" Inherits="EventHandlingSystem.SiteSettings" %>

<%@ Register TagPrefix="aspTaxonomy" TagName="TaxonomyHandler" Src="TaxonomyControl.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <br />
    <br />
    <div class="content-box">
        <div class="titlebox">
        <h1>Taxonomy Manager</h1>
    </div>
        <aspTaxonomy:TaxonomyHandler runat="server" />
    </div>
    <br />
    <br />
    <div class="content-box">
        <div class="titlebox">
        <h1>Community/Association Manager</h1>
    </div>
        
    </div>
</asp:Content>
