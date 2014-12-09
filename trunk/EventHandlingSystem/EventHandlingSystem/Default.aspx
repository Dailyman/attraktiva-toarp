<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="EventHandlingSystem.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    
        <div class="titlebox">
            <h1>Home</h1>
        </div>
        <br />
        <br />
        <div class="warning-pattern">
            <br />
            <h1>Site under construction</h1>
            <br />
        </div>
        <div class="warning-pattern">
        </div>
        <br />
        <asp:Menu ID="MenuHome" runat="server"></asp:Menu>
        <asp:Label ID="TestLable" runat="server" Text=""></asp:Label>
    
</asp:Content>
