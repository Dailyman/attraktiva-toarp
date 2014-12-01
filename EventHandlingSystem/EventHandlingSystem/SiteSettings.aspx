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
        <aspTaxonomy:TaxonomyHandler runat="server" />
    </div>
</asp:Content>
