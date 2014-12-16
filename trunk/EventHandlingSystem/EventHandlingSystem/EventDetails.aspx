<%@ Page Title="Event details" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EventDetails.aspx.cs" Inherits="EventHandlingSystem.EventDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <br />
    <asp:DropDownList ID="DropDownListEvents" runat="server"></asp:DropDownList><asp:Button ID="BtnSearch" runat="server" Text="Load event" OnClick="BtnSearch_OnClick" />
    
        <div id="Main" runat="server"></div>
    
</asp:Content>
