<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Event.aspx.cs" Inherits="EventHandlingSystem.Event1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Create event</h1>
    <br />
    <h6>* = Required field.</h6>
    <br />
    <h6>Title*</h6>
    <asp:TextBox ID="TxtBoxTitle" runat="server"></asp:TextBox>
    <br />
    <h6>Description</h6>
    <asp:TextBox ID="TxtBoxDescription" runat="server" TextMode="MultiLine"></asp:TextBox>
    <br />
    <h6>Summary</h6>
    <asp:TextBox ID="TxtBoxSummary" runat="server" TextMode="MultiLine"></asp:TextBox>
    <br />
    <h6>Other</h6>
    <asp:TextBox ID="TxtBoxOther" runat="server" TextMode="MultiLine"></asp:TextBox>
    <br />
    <h6>Location</h6>
    <asp:TextBox ID="TxtBoxLocation" runat="server"></asp:TextBox>
    <br />
    <h6>Image(Url)</h6>
    <asp:TextBox ID="TxtBoxImageUrl" runat="server"></asp:TextBox>
    <br />
    <h6>A whole day event?</h6>
    <asp:CheckBox ID="ChkBoxDayEvent" runat="server" />
    <br />
    <h6>Start date*</h6>
    <asp:TextBox ID="TxtBoxStartDate" runat="server" OnTextChanged="TxtBoxStartDate_OnTextChanged" Width="205px" AutoPostBack="True"></asp:TextBox>
    <asp:TextBox ID="TxtBoxStartTime" runat="server" Width="45px"></asp:TextBox>
    <asp:ImageButton ID="ImageButtonStartDate" runat="server" ImageUrl="Images/calendar-22x21.png" Height="22px" Width="21px" OnClick="ImageButtonStartDate_OnClick" />
    <asp:RegularExpressionValidator ID="RegExpValStartTime" runat="server" ControlToValidate="TxtBoxStartTime" ErrorMessage="Use the right format! (e.g. 15:30)" ValidationGroup="ValGroupCreateEvent"></asp:RegularExpressionValidator>
    <br />
    <asp:Calendar ID="CalendarStartDate" runat="server" OnSelectionChanged="CalendarStartDate_OnSelectionChanged"></asp:Calendar>
    <br />
    <h6>End date*</h6>
    <asp:TextBox ID="TxtBoxEndDate" runat="server" OnTextChanged="TxtBoxEndDate_OnTextChanged" Width="205px" AutoPostBack="True"></asp:TextBox>
    <asp:TextBox ID="TxtBoxEndTime" runat="server" Width="45px"></asp:TextBox>
    <asp:ImageButton ID="ImageButtonEndDate" runat="server" ImageUrl="Images/calendar-22x21.png" Height="22px" Width="21px" OnClick="ImageButtonEndDate_OnClick" />
    <asp:RegularExpressionValidator ID="RegExpValEndTime" runat="server" ControlToValidate="TxtBoxEndTime" ErrorMessage="Use the right format! (e.g. 15:30)" ValidationGroup="ValGroupCreateEvent"></asp:RegularExpressionValidator>
    <br />
    <asp:Calendar ID="CalendarEndDate" runat="server" OnSelectionChanged="CalendarEndDate_OnSelectionChanged"></asp:Calendar>
    <br />
    <h6>Target group</h6>
    <asp:TextBox ID="TxtBoxTargetGroup" runat="server"></asp:TextBox>
    <br />
    <h6>Approximate attendees*</h6>
    <asp:TextBox ID="TxtBoxApproximateAttendees" runat="server"></asp:TextBox>
    <asp:RequiredFieldValidator ID="ReqFieldValiApproxAttend" runat="server" ControlToValidate="TxtBoxApproximateAttendees" ErrorMessage="* Required Field" SetFocusOnError="True" ValidationGroup="ValGroupCreateEvent"></asp:RequiredFieldValidator>
    <asp:CompareValidator ID="CompValiApproxAttend" runat="server" ControlToValidate="TxtBoxApproximateAttendees" Type="Integer" Operator="DataTypeCheck" ErrorMessage="Value must be an integer!" SetFocusOnError="True" ValidationGroup="ValGroupCreateEvent" />
    <br />
    <asp:Button ID="BtnCreateEvent" runat="server" Text="Create event" OnClick="BtnCreateEvent_OnClick" ValidationGroup="ValGroupCreateEvent" />
    <br />
    <asp:Label ID="LabelMessage" runat="server" Text=""></asp:Label>


</asp:Content>
