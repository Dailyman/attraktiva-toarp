<%@ Page Title="Event details" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EventDetails.aspx.cs" Inherits="EventHandlingSystem.EventDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <style type="text/css">
        .image-box {
            float: right;
            max-height: 250px;
max-width: 250px;
width: 100%;
height: 100%;
        }
    </style>
    <br />
    <asp:DropDownList ID="DropDownListEvents" runat="server"></asp:DropDownList><asp:Button ID="BtnSearch" runat="server" Text="Load event" OnClick="BtnSearch_OnClick" />
    
        <div id="Main" runat="server"></div>
     <br />
     <asp:HyperLink ID="LinkUpdate" ImageUrl="http://static.iconsplace.com/icons/preview/black/edit-32.png"  runat="server"></asp:HyperLink>
    <asp:HyperLink ID="LinkCopy" ImageUrl="http://static.iconsplace.com/icons/preview/black/copy-32.png" runat="server"></asp:HyperLink>
     <br />
    
    <h5>Title</h5>
    <asp:Label ID="EventTitle" runat="server" Text="No title" CssClass="title"></asp:Label>
    
    <asp:Image ID="EventImage" runat="server" AlternateText="No image" CssClass="image-box" />
    
    <h4>Description</h4>
    <asp:Label ID="EventDescription" runat="server" Text="No description"></asp:Label>
    <br/>
    <asp:Label ID="EventSummary" runat="server" Text="No summary"></asp:Label>
    <br/>
    <asp:Label ID="EventOther" runat="server" Text="No other information"></asp:Label>
    <br/>
    <asp:Label ID="EventLocation" runat="server" Text="No location"></asp:Label>
    <br/>
    <asp:HyperLink ID="EventLink" runat="server" Target="_blank">External eventpage</asp:HyperLink>
    <br/>
    <asp:CheckBox ID="DayEvent" Enabled="False" runat="server" />
    <br/>
    <asp:Label ID="EventStartDate" runat="server"></asp:Label>
    <br/>
    <asp:Label ID="EventEndDate" runat="server"></asp:Label>
    <br/>
    <asp:Label ID="EventTargetGroup" runat="server" Text="No targetgroup"></asp:Label>
    <br/>
    <asp:Label ID="EventApproxAttend" runat="server" Text="No approximate attendees"></asp:Label>
    <br/>
    <br/>
    <asp:Label ID="EventCreated" runat="server"></asp:Label>
    <br/>
    <asp:Label ID="EventCreatedBy" runat="server"></asp:Label>
    <br/>
    <asp:Label ID="EventLatestUpdate" runat="server"></asp:Label>
    <br/>
    <asp:Label ID="EventUpdatedBy" runat="server"></asp:Label>
   
</asp:Content>
