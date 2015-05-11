<%@ Page Title="Create event" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EventCreate.aspx.cs" Inherits="EventHandlingSystem.EventCreate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <style type="text/css">
        .listbox
        {
            min-width: 100px;
        }

        .box-inline
        {
            display: inline;
            margin: 0 10px 0 0;
        }

            .box-inline select
            {
                margin: 0 5px 0 0;
                vertical-align: top;
            }

            .box-inline input
            {
                margin: 0 5px 0 0;
                vertical-align: top;
            }
    </style>


    <div class="titlebox">
        <h1>Create event</h1>
    </div>

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
    <h6>Event(Url) eg. Facebook eventpage</h6>
    <asp:TextBox ID="TxtBoxEventUrl" runat="server"></asp:TextBox>
    <br />
    <h6>Whole day event</h6>
    <asp:CheckBox ID="ChkBoxDayEvent" runat="server" OnCheckedChanged="ChkBoxDayEvent_OnCheckedChanged" AutoPostBack="True" />
    <br />
    <h6>Start date*</h6>
    <asp:TextBox ID="TxtBoxStartDate" runat="server" OnTextChanged="TxtBoxStartDate_OnTextChanged" Width="205px" AutoPostBack="True"></asp:TextBox>
    <asp:TextBox ID="TxtBoxStartTime" runat="server" Width="50px" MaxLength="5"></asp:TextBox>
    <asp:ImageButton ID="ImageButtonStartDate" runat="server" ImageUrl="~/Images/calendar-22x21.png" Height="22px" Width="21px" OnClick="ImageButtonStartDate_OnClick" />
    <asp:RequiredFieldValidator ID="ReqFieldValiStartDate" runat="server" ControlToValidate="TxtBoxStartDate" ErrorMessage="* Required Field" ValidationGroup="ValGroupCreateEvent" Display="Dynamic" SetFocusOnError="True"></asp:RequiredFieldValidator>
    <asp:RequiredFieldValidator ID="ReqFieldValiStartTime" runat="server" ControlToValidate="TxtBoxStartTime" ErrorMessage="* Required Field" ValidationGroup="ValGroupCreateEvent" Display="Dynamic" SetFocusOnError="True"></asp:RequiredFieldValidator>
    <asp:RegularExpressionValidator ID="RegExpValStartTime" runat="server" ControlToValidate="TxtBoxStartTime" ErrorMessage="Use the right format! (e.g. 15:30)" ValidationGroup="ValGroupCreateEvent" Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([01]?[0-9]|2[0-3]):[0-5][0-9]$"></asp:RegularExpressionValidator>
    <asp:CustomValidator ID="CustomValiStartDate" runat="server" ControlToValidate="TxtBoxStartDate" ErrorMessage="Use the right format! (e.g. 2005-06-21)" OnServerValidate="CustomValiStartDate_OnServerValidate" ValidationGroup="ValGroupCreateEvent" Display="Dynamic" SetFocusOnError="True" ValidateEmptyText="True"></asp:CustomValidator>
    <br />
    <asp:Calendar ID="CalendarStartDate" runat="server" OnSelectionChanged="CalendarStartDate_OnSelectionChanged"></asp:Calendar>
    <br />
    <h6>End date*</h6>
    <asp:TextBox ID="TxtBoxEndDate" runat="server" OnTextChanged="TxtBoxEndDate_OnTextChanged" Width="205px" AutoPostBack="True"></asp:TextBox>
    <asp:TextBox ID="TxtBoxEndTime" runat="server" Width="50px" MaxLength="5"></asp:TextBox>
    <asp:ImageButton ID="ImageButtonEndDate" runat="server" ImageUrl="~/Images/calendar-22x21.png" Height="22px" Width="21px" OnClick="ImageButtonEndDate_OnClick" />
    <asp:RequiredFieldValidator ID="ReqFieldValiEndDate" runat="server" ControlToValidate="TxtBoxEndDate" ErrorMessage="* Required Field" ValidationGroup="ValGroupCreateEvent" Display="Dynamic" SetFocusOnError="True"></asp:RequiredFieldValidator>
    <asp:RequiredFieldValidator ID="ReqFieldValiEndTime" runat="server" ControlToValidate="TxtBoxEndTime" ErrorMessage="* Required Field" ValidationGroup="ValGroupCreateEvent" Display="Dynamic" SetFocusOnError="True"></asp:RequiredFieldValidator>
    <asp:RegularExpressionValidator ID="RegExpValEndTime" runat="server" ControlToValidate="TxtBoxEndTime" ErrorMessage="Use the right format! (e.g. 15:30)" ValidationGroup="ValGroupCreateEvent" Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([01]?[0-9]|2[0-3]):[0-5][0-9]$"></asp:RegularExpressionValidator>
    <asp:CustomValidator ID="CustomValiEndDate" runat="server" ControlToValidate="TxtBoxEndDate" ErrorMessage="Use the right format! (e.g. 2005-06-21)" OnServerValidate="CustomValiEndDate_OnServerValidate" ValidationGroup="ValGroupCreateEvent" Display="Dynamic" SetFocusOnError="True" ValidateEmptyText="True"></asp:CustomValidator>
    <br />
    <asp:Calendar ID="CalendarEndDate" runat="server" OnSelectionChanged="CalendarEndDate_OnSelectionChanged"></asp:Calendar>
    <br />
    <h6>Target group</h6>
    <asp:TextBox ID="TxtBoxTargetGroup" runat="server"></asp:TextBox>
    <br />
    <h6>Approximate attendees</h6>
    <asp:TextBox ID="TxtBoxApproximateAttendees" runat="server"></asp:TextBox>
    <asp:CompareValidator ID="CompValiApproxAttend" runat="server" ControlToValidate="TxtBoxApproximateAttendees" Type="Integer" Operator="DataTypeCheck" ErrorMessage="Value is not in the right format! Use positive numbers." ValidationGroup="ValGroupCreateEvent" Display="Dynamic" SetFocusOnError="True" />
     <br />
    <h6>Do you want to show this event in Communities for the seleted Associations</h6>
    <asp:CheckBox ID="ChkBoxDisplayInCommunity" runat="server" OnCheckedChanged="ChkBoxDisplayInCommunity_OnCheckedChanged" />
    <br />
    <br />
    <h6>Associations</h6>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
        <ContentTemplate>
            <div class="box-inline">
                <asp:DropDownList ID="DropDownAssociation" runat="server" CssClass="listbox" AutoPostBack="True"></asp:DropDownList>
                <asp:Button ID="ButtonAddAssociation" runat="server" Text="Add Association" CssClass="btn-small" OnClick="ButtonAddAssociation_OnClick" />

            </div>
            <div class="box-inline">
                <asp:ListBox ID="ListBoxAssociations" AutoPostBack="True" CssClass="listbox" runat="server" SelectionMode="Multiple"></asp:ListBox>
                <asp:Button ID="ButtonRemoveAssociation" runat="server" Text="Remove association" CssClass="btn-small" OnClick="ButtonRemoveAssociation_OnClick" />
            </div>
            <br/>
            <asp:Label ID="LabelErrorAsso" runat="server" Text=""></asp:Label>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="DropDownAssociation" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="ButtonAddAssociation" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="ButtonRemoveAssociation" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="ListBoxAssociations" EventName="SelectedIndexChanged" />
        </Triggers>
    </asp:UpdatePanel>
    <br />
    <h6>Categories</h6>
    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Always">
        <ContentTemplate>
            <div class="box-inline">
                <asp:DropDownList ID="DropDownSubCategories" runat="server" CssClass="listbox" AutoPostBack="True"></asp:DropDownList>
                <asp:Button ID="ButtonAddSubCat" runat="server" Text="Add Category" CssClass="btn-small" OnClick="ButtonAddSubCat_OnClick" />
            </div>
            <div class="box-inline">
                <asp:ListBox ID="ListBoxSubCategories" AutoPostBack="True" CssClass="listbox" runat="server" SelectionMode="Multiple"></asp:ListBox>
                <asp:Button ID="ButtonRemoveSubCat" runat="server" Text="Remove Category" CssClass="btn-small" OnClick="ButtonRemoveSubCat_OnClick" />
            </div>
            <br/>
            <asp:Label ID="LabelErrorSubCat" runat="server" Text=""></asp:Label>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="DropDownSubCategories" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="ButtonAddSubCat" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="ButtonRemoveSubCat" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="ListBoxSubCategories" EventName="SelectedIndexChanged" />
        </Triggers>
    </asp:UpdatePanel>
    <br />
    <br />
    <asp:Label ID="LabelMessage" runat="server" Text=""></asp:Label>
    <div class="btn-align-right">
        <asp:Button ID="BtnCreateEvent" CssClass="btn-blue" runat="server" Text="Create event" OnClick="BtnCreateEvent_OnClick" ValidationGroup="ValGroupCreateEvent" />
    </div>
    <br />




</asp:Content>

<%--<asp:Content ID="Content4" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Stylesheets" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="FeaturedContent" runat="server">
    <p>
       FeaturedContent
   </p>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="TitleContent" runat="server">
    <p>
       TitleContent
   </p>
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="LeftColumnContent" runat="server">
   <p>
       LeftContent
   </p>
</asp:Content>
<asp:Content ID="Content8" ContentPlaceHolderID="RightColumnContent" runat="server">
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
    <h6>Approximate attendees</h6>
    <asp:TextBox ID="TxtBoxApproximateAttendees" runat="server"></asp:TextBox>
    <asp:CompareValidator ID="CompValiApproxAttend" runat="server" ControlToValidate="TxtBoxApproximateAttendees" Type="Integer" Operator="DataTypeCheck" ErrorMessage="Value must be an integer!" ValidationGroup="ValGroupCreateEvent" Display="Dynamic" SetFocusOnError="True" />
    <br />
    <h6>Association</h6>
    <asp:DropDownList ID="DropDownAssociation" runat="server"></asp:DropDownList>
    <br />
    <div class="btn-align-right">
        <asp:Button ID="BtnCreateEvent" CssClass="btn-blue" runat="server" Text="Create event" OnClick="BtnCreateEvent_OnClick" ValidationGroup="ValGroupCreateEvent" />
    </div>
    <br />
</asp:Content>--%>
