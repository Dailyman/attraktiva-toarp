<%@ Page Title="Unknown page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SitePage.aspx.cs" Inherits="EventHandlingSystem.SitePage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <br />
    <div class="titlebox">
        <h1>
            <asp:Label ID="LabelTitle" CssClass="ribbon-title-big" runat="server" Text="Unknown community or association"></asp:Label></h1>
    </div>
    <br />
    <asp:Label ID="LabelWelcome" runat="server" Text=""></asp:Label>
</asp:Content>
