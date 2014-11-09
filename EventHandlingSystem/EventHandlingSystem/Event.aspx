<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Event.aspx.cs" Inherits="EventHandlingSystem.Event1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
    <h1>FeaturedContent</h1>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <h1>MainContent</h1>
    <asp:TextBox ID="TxtBoxTitle" runat="server"></asp:TextBox>
    <asp:TextBox ID="TxtBoxDescription" runat="server"></asp:TextBox>
    <asp:TextBox ID="TxtBoxSummary" runat="server"></asp:TextBox>
    <asp:CheckBox ID="ChkBoxDayEvent" runat="server" />
    <asp:TextBox ID="TxtBoxStartDate" runat="server"></asp:TextBox><asp:ImageButton ID="ImageButtonStartDate" runat="server" />
    <asp:calendar ID="CalendarStartDate" runat="server"></asp:calendar>
    <asp:TextBox ID="TxtBoxEndDate" runat="server"></asp:TextBox><asp:ImageButton ID="ImageButtonEndDate" runat="server" />
    <asp:Calendar ID="CalendarEndDate" runat="server"></asp:Calendar>


</asp:Content>
