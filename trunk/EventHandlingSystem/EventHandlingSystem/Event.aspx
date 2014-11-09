<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Event.aspx.cs" Inherits="EventHandlingSystem.Event1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
    <h1>FeaturedContent</h1>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <h1>MainContent</h1>
    <br/>
    <h6>Title*</h6>
    <asp:TextBox ID="TxtBoxTitle" runat="server"></asp:TextBox>
    <br/>
    <h6>Description</h6>
    <asp:TextBox ID="TxtBoxDescription" runat="server"></asp:TextBox>
    <br/>
    <h6>Summary</h6>
    <asp:TextBox ID="TxtBoxSummary" runat="server"></asp:TextBox>
    <br/>
    <h6>A whole day event?</h6>
    <asp:CheckBox ID="ChkBoxDayEvent" runat="server" />
    <br/>
    <h6>Start date*</h6>
    <asp:TextBox ID="TxtBoxStartDate" runat="server"></asp:TextBox><asp:ImageButton ID="ImageButtonStartDate" runat="server" ImageUrl="Images/calendar-22x21.png" Height="22px" Width="21px" OnClick="ImageButtonStartDate_OnClick" />
    <br/>
    <asp:calendar ID="CalendarStartDate" runat="server" OnSelectionChanged="CalendarStartDate_OnSelectionChanged"></asp:calendar>
    <br/>
    <h6>End date*</h6>
    <asp:TextBox ID="TxtBoxEndDate" runat="server"></asp:TextBox><asp:ImageButton ID="ImageButtonEndDate" runat="server" ImageUrl="Images/calendar-22x21.png" Height="22px" Width="21px" OnClick="ImageButtonEndDate_OnClick" />
    <br/>
    <asp:Calendar ID="CalendarEndDate" runat="server" OnSelectionChanged="CalendarEndDate_OnSelectionChanged"></asp:Calendar>
    <br/>
    <h6>Approximate attendees*</h6>
    <asp:TextBox ID="TxtBoxApproximateAttendees" runat="server"></asp:TextBox>
    <br/>
    <asp:Button ID="BtnCreateEvent" runat="server" Text="Create event" OnClick="BtnCreateEvent_OnClick" />
    <br/>
    <asp:Label ID="LabelMessage" runat="server" Text=""></asp:Label>


</asp:Content>
