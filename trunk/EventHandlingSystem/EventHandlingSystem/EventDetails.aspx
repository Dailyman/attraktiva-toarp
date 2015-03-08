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
                padding: 5px;
                vertical-align: top;
                max-width: 500px;
            }


            .content-table tr
            {
            }

            .content-table td span
            {
                display: inline;
            }

        .bold-text
        {
            font-weight: bold;
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
            /*float: right;*/
            /*text-align: center;*/
            /*min-height: 150px;*/
            /*min-width: 150px;*/
            width: auto;
            height: auto;
            margin: 10px;
        }

            .image-box img
            {
                width: 100%;
                height: 100%;
            }

        .small-text-14
        {
            font-size: 14px;
        }

            .small-text-14 td
            {
                padding: 5px;
            }

        .view-img {
            display: inline-block;
            position: fixed;
            top: 0;
            bottom: 0;
            left: 0;
            right: 0;
            width: auto !important;
            height: auto !important;
            margin: auto;
            padding: 10px;
            background-color: #969696;
            background-color: rgba(150, 150, 150, 0.7);
        }
    </style>

    <script type="text/javascript">
        $(document).ready(function () {
            $('#EventImage').click(function () {
                $(this).toggleClass("view-img");
            });
        });
    </script>

    <br />
    <asp:DropDownList ID="DropDownListEvents" runat="server"></asp:DropDownList><asp:Button ID="BtnSearch" runat="server" Text="Load event" OnClick="BtnSearch_OnClick" />
    <br />
    
    <div id="Main" runat="server"></div>
    <asp:Panel ID="PanelMain" runat="server">
    <br />

    <table class="content-table">
        <tr>
            <td colspan="2">
                <asp:HyperLink ID="LinkCopy" ImageUrl="http://static.iconsplace.com/icons/preview/black/copy-32.png" runat="server" CssClass="img-link"></asp:HyperLink>
                <asp:HyperLink ID="LinkUpdate" ImageUrl="http://static.iconsplace.com/icons/preview/black/edit-32.png" runat="server" CssClass="img-link"></asp:HyperLink>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <h1>
                    <asp:Literal ID="EventTitle" runat="server" Text="No title"></asp:Literal>
                </h1>
            </td>
        </tr>
        <tr>
            <td></td>
            <td></td>
            <td rowspan="50">
                <div class="image-box">
                    <asp:Image ID="EventImage" ClientIDMode="Static" runat="server" AlternateText="No image" ToolTip="Click to enlarge/scale down" />
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <span class="bold-text">Start</span>
            </td>
            <td>
                <asp:Label ID="EventStartDate" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <span class="bold-text">End</span>
            </td>
            <td>
                <asp:Label ID="EventEndDate" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td></td>
            <td></td>
        </tr>
        <tr>
            <td>
                <span class="bold-text">Description</span>
            </td>
            <td>
                <asp:Label ID="EventDescription" runat="server" Text="No description"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <span class="bold-text">Summary</span>
            </td>
            <td>
                <asp:Label ID="EventSummary" runat="server" Text="No summary"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <span class="bold-text">Other</span>
            </td>
            <td>
                <asp:Label ID="EventOther" runat="server" Text="No other information"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <span class="bold-text">Location</span>
            </td>
            <td>
                <asp:Label ID="EventLocation" runat="server" Text="No location"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <span class="bold-text">Link</span>
            </td>
            <td>
                <asp:HyperLink ID="EventLink" runat="server" Target="_blank"></asp:HyperLink>
            </td>
        </tr>
        <tr>
            <td>
                <span class="bold-text">Day event</span>
            </td>
            <td>
                <asp:CheckBox ID="DayEvent" Enabled="False" runat="server" />
            </td>
        </tr>
        <tr>
            <td>
                <span class="bold-text">Target group</span>
            </td>
            <td>
                <asp:Label ID="EventTargetGroup" runat="server" Text="No targetgroup"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <span class="bold-text">Approximate attendees</span>
            </td>
            <td>
                <asp:Label ID="EventApproxAttend" runat="server" Text="No approximate attendees"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <span class="bold-text">Published in Associations</span>
            </td>
            <td>
                <asp:ListBox ID="ListBoxAssociations" runat="server" OnSelectedIndexChanged="ListBoxAssociations_OnSelectedIndexChanged" AutoPostBack="True"></asp:ListBox>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
                    <ContentTemplate>
                        <asp:Panel ID="PanelAsso" runat="server">
                            <asp:Label ID="AssoName" runat="server" CssClass="bold-text"></asp:Label>
                            <asp:HyperLink ID="AssoLink" runat="server" Target="_blank"></asp:HyperLink>
                        </asp:Panel>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="ListBoxAssociations" EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td></td>
            <td></td>
        </tr>
    </table>

    <br />
    <table class="small-text-14">
        <tr>
            <td><span class="bold-text">Created: </span></td>
            <td>
                <asp:Label ID="EventCreated" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td><span class="bold-text">Created by: </span></td>
            <td>
                <asp:Label ID="EventCreatedBy" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td><span class="bold-text">Latest update: </span></td>
            <td>
                <asp:Label ID="EventLatestUpdate" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td><span class="bold-text">Updated by: </span></td>
            <td>
                <asp:Label ID="EventUpdatedBy" runat="server"></asp:Label>
            </td>
        </tr>
    </table>
        </asp:Panel>

</asp:Content>
