<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="EventHandlingSystem.Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="TestLable" runat="server" Text=""></asp:Label>
    <asp:TreeView ID="TreeViewTerms" runat="server"></asp:TreeView>
</asp:Content>
