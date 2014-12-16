<%@ Page Title="Create event" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EventCreate.aspx.cs" Inherits="EventHandlingSystem.EventCreate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">

    <div class="titlebox">
        <h1>Create event</h1>
    </div>
    <br />
    <asp:Label ID="LabelMessage" runat="server" Text=""></asp:Label>
    <h6>* = Required field.</h6>
    <br />
    <h6>Title*</h6>
    <asp:TextBox ID="TxtBoxTitle" runat="server"></asp:TextBox>
    <asp:RequiredFieldValidator ID="ReqFieldValiTitle" runat="server" ControlToValidate="TxtBoxTitle" ErrorMessage="* Required Field" ValidationGroup="ValGroupCreateEvent" Display="Dynamic" SetFocusOnError="True"></asp:RequiredFieldValidator>
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
    <h6>Whole day event</h6><asp:CheckBox ID="ChkBoxDayEvent" runat="server" OnCheckedChanged="ChkBoxDayEvent_OnCheckedChanged" AutoPostBack="True" />
    <br />
    <h6>Start date*</h6>
    <asp:TextBox ID="TxtBoxStartDate" runat="server" OnTextChanged="TxtBoxStartDate_OnTextChanged" Width="205px" AutoPostBack="True"></asp:TextBox>
    <asp:TextBox ID="TxtBoxStartTime" runat="server" Width="50px"></asp:TextBox>
    <asp:ImageButton ID="ImageButtonStartDate" runat="server" ImageUrl="Images/calendar-22x21.png" Height="22px" Width="21px" OnClick="ImageButtonStartDate_OnClick" />
    <asp:RequiredFieldValidator ID="ReqFieldValiStartDate" runat="server" ControlToValidate="TxtBoxStartDate" ErrorMessage="* Required Field" ValidationGroup="ValGroupCreateEvent" Display="Dynamic" SetFocusOnError="True"></asp:RequiredFieldValidator>
    <asp:RequiredFieldValidator ID="ReqFieldValiStartTime" runat="server" ControlToValidate="TxtBoxStartTime" ErrorMessage="* Required Field" ValidationGroup="ValGroupCreateEvent" Display="Dynamic" SetFocusOnError="True"></asp:RequiredFieldValidator>
    <asp:RegularExpressionValidator ID="RegExpValStartTime" runat="server" ControlToValidate="TxtBoxStartTime" ErrorMessage="Use the right format! (e.g. 15:30)" ValidationGroup="ValGroupCreateEvent" Display="Dynamic" SetFocusOnError="True"></asp:RegularExpressionValidator>
    <asp:CustomValidator ID="CustomValiStartDate" runat="server" ControlToValidate="TxtBoxStartDate" ErrorMessage="Use the right format! (e.g. 2005-06-21)" OnServerValidate="CustomValiStartDate_OnServerValidate" ValidationGroup="ValGroupCreateEvent" Display="Dynamic" SetFocusOnError="True" ValidateEmptyText="True"></asp:CustomValidator>
    <br />
    <asp:Calendar ID="CalendarStartDate" runat="server" OnSelectionChanged="CalendarStartDate_OnSelectionChanged"></asp:Calendar>
    <br />
    <h6>End date*</h6>
    <asp:TextBox ID="TxtBoxEndDate" runat="server" OnTextChanged="TxtBoxEndDate_OnTextChanged" Width="205px" AutoPostBack="True"></asp:TextBox>
    <asp:TextBox ID="TxtBoxEndTime" runat="server" Width="50px"></asp:TextBox>
    <asp:ImageButton ID="ImageButtonEndDate" runat="server" ImageUrl="Images/calendar-22x21.png" Height="22px" Width="21px" OnClick="ImageButtonEndDate_OnClick" />
    <asp:RequiredFieldValidator ID="ReqFieldValiEndDate" runat="server" ControlToValidate="TxtBoxEndDate" ErrorMessage="* Required Field" ValidationGroup="ValGroupCreateEvent" Display="Dynamic" SetFocusOnError="True"></asp:RequiredFieldValidator>
    <asp:RequiredFieldValidator ID="ReqFieldValiEndTime" runat="server" ControlToValidate="TxtBoxEndTime" ErrorMessage="* Required Field" ValidationGroup="ValGroupCreateEvent" Display="Dynamic" SetFocusOnError="True"></asp:RequiredFieldValidator>
    <asp:RegularExpressionValidator ID="RegExpValEndTime" runat="server" ControlToValidate="TxtBoxEndTime" ErrorMessage="Use the right format! (e.g. 15:30)" ValidationGroup="ValGroupCreateEvent" Display="Dynamic" SetFocusOnError="True"></asp:RegularExpressionValidator>
    <asp:CustomValidator ID="CustomValiEndDate" runat="server" ControlToValidate="TxtBoxEndDate" ErrorMessage="Use the right format! (e.g. 2005-06-21)" OnServerValidate="CustomValiEndDate_OnServerValidate" ValidationGroup="ValGroupCreateEvent" Display="Dynamic" SetFocusOnError="True" ValidateEmptyText="True"></asp:CustomValidator>
    <br />
    <asp:Calendar ID="CalendarEndDate" runat="server" OnSelectionChanged="CalendarEndDate_OnSelectionChanged"></asp:Calendar>
    <br />
    <h6>Target group</h6>
    <asp:TextBox ID="TxtBoxTargetGroup" runat="server"></asp:TextBox>
    <br />
    <h6>Approximate attendees*</h6>
    <asp:TextBox ID="TxtBoxApproximateAttendees" runat="server"></asp:TextBox>
    <asp:RequiredFieldValidator ID="ReqFieldValiApproxAttend" runat="server" ControlToValidate="TxtBoxApproximateAttendees" ErrorMessage="* Required Field" ValidationGroup="ValGroupCreateEvent" Display="Dynamic" SetFocusOnError="True"></asp:RequiredFieldValidator>
    <asp:CompareValidator ID="CompValiApproxAttend" runat="server" ControlToValidate="TxtBoxApproximateAttendees" Type="Integer" Operator="DataTypeCheck" ErrorMessage="Value must be an integer!" ValidationGroup="ValGroupCreateEvent" Display="Dynamic" SetFocusOnError="True" />
    <br />
    <h6>Association</h6>
    <asp:DropDownList ID="DropDownAssociation" runat="server"></asp:DropDownList>
    <br />
    <h6>Link</h6>
    <asp:TextBox ID="TxtBoxLink" runat="server"></asp:TextBox>
    <br />
    <br />
    <div class="btn-align-right">
        <asp:Button ID="BtnCreateEvent" CssClass="btn-blue" runat="server" Text="Create event" OnClick="BtnCreateEvent_OnClick" ValidationGroup="ValGroupCreateEvent" />
    </div>
    <br />




</asp:Content>
