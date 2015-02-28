<%@ Page Title="Event details" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EventDetails.aspx.cs" Inherits="EventHandlingSystem.EventDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <style type="text/css">
        .content-table
        {
            width: 100%;
            height: auto;
        }

            .content-table td
            {
                padding: 0;
            }

            .content-table tr
            {
            }

        .img-link a
        {
            display: block;
        }

        .img-link img
        {
            padding: 5px;
        }

            .img-link img:hover
            {
                background-color: gainsboro;
                -moz-border-radius: 10px;
                -webkit-border-radius: 10px;
                -ms-border-radius: 10px;
                border-radius: 10px;
                position: relative;
                top: -2px;
            }

        .image-box
        {
            float: right;
            max-height: 350px;
            max-width: 350px;
            width: auto;
            height: auto;
        }
    </style>
    <br />
    <asp:DropDownList ID="DropDownListEvents" runat="server"></asp:DropDownList><asp:Button ID="BtnSearch" runat="server" Text="Load event" OnClick="BtnSearch_OnClick" />

    <div id="Main" runat="server"></div>
    <br />
    <table class="content-table">
        <tr>
            <td>
                <asp:HyperLink ID="LinkCopy" ImageUrl="http://static.iconsplace.com/icons/preview/black/copy-32.png" runat="server" CssClass="img-link"></asp:HyperLink>
                <asp:HyperLink ID="LinkUpdate" ImageUrl="http://static.iconsplace.com/icons/preview/black/edit-32.png" runat="server" CssClass="img-link"></asp:HyperLink>
            </td>
        </tr>
        <tr>
            <td>
                <h1>
                    <asp:Literal ID="EventTitle" runat="server" Text="No title"></asp:Literal>
                </h1>
            </td>
            <td></td>
            <td rowspan="50">
                <asp:Image ID="EventImage" runat="server" AlternateText="No image" CssClass="image-box" />
            </td>
        </tr>
        <tr>
            <td>
                <h6>Start</h6>
                <asp:Label ID="EventStartDate" runat="server"></asp:Label>
                <br />
                <br />
            </td>
            <td>
                <h6>End</h6>
                <asp:Label ID="EventEndDate" runat="server"></asp:Label>
                <br />
                <br />
            </td>
        </tr>
        <tr>
            <td>
                <h6>Description</h6>
            </td>
            <td>
                <asp:Label ID="EventDescription" runat="server" Text="No description"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <h6>Summary</h6>
            </td>
            <td>
                <asp:Label ID="EventSummary" runat="server" Text="No summary"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <h6>Other</h6>
            </td>
            <td>
                <asp:Label ID="EventOther" runat="server" Text="No other information"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <h6>Location</h6>
            </td>
            <td>
                <asp:Label ID="EventLocation" runat="server" Text="No location"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <h6>Link</h6>
            </td>
            <td>
                <asp:HyperLink ID="EventLink" runat="server" Target="_blank"></asp:HyperLink>
            </td>
        </tr>
        <tr>
            <td>
                <h6>Day event</h6>
            </td>
            <td>
                <asp:CheckBox ID="DayEvent" Enabled="False" runat="server" />
            </td>
        </tr>
        <tr>
            <td>
                <h6>Target group</h6>
            </td>
            <td>
                <asp:Label ID="EventTargetGroup" runat="server" Text="No targetgroup"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <h6>Approximate attendees</h6>
            </td>
            <td>
                <asp:Label ID="EventApproxAttend" runat="server" Text="No approximate attendees"></asp:Label>
            </td>
        </tr>
    </table>
    <br />
    <br />
    <span>Created: </span>
    <asp:Label ID="EventCreated" runat="server"></asp:Label>
    <br />
    <span>Created by: </span>
    <asp:Label ID="EventCreatedBy" runat="server"></asp:Label>
    <br />
    <br />
    <span>Latest update: </span>
    <asp:Label ID="EventLatestUpdate" runat="server"></asp:Label>
    <br />
    <span>Updated by: </span>
    <asp:Label ID="EventUpdatedBy" runat="server"></asp:Label>

</asp:Content>
