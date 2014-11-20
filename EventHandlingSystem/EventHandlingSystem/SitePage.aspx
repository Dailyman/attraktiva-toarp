<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SitePage.aspx.cs" Inherits="EventHandlingSystem.SitePage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <asp:DropDownList ID="DropDownListWePages" runat="server"></asp:DropDownList>
    <asp:Button ID="BtnLoadPage" runat="server" Text="Load page" OnClick="BtnLoadPage_OnClick" />
    <br />
    <br />
    <asp:Label ID="LabelTitle" runat="server" Text="Unknown community or association"></asp:Label>
</asp:Content>
