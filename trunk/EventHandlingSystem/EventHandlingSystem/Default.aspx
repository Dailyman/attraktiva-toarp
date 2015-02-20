<%@ Page Title="Home" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="EventHandlingSystem.Default" %>
<%--<%@ Page Title="Home" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="EventHandlingSystem.Default" %>--%>

<%--<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=4.5.7.1213, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>--%>
<%@ Register TagPrefix="aspCal" TagName="CalendarTable" Src="Calendar.ascx" %>
<%@ Register TagPrefix="aspFeed" TagName="FeedBox" Src="Feed.ascx" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <div class="titlebox">
        <h1>Home</h1>
    </div>
    <br/>
    <%--<asp:TextBox ID="TxtBox" runat="server" type="datetime-local"></asp:TextBox>--%>
    <br/>
    <div class="warning-pattern">
    </div>
    <div class="temp-background">
        <br/>
        <h1>¡Site under construction!</h1>
        <br/>
    </div>
    <div class="warning-pattern"></div>
    <asp:Menu ID="MenuHome" runat="server"></asp:Menu>
    <asp:Label ID="TestLable" runat="server" Text=""></asp:Label>
    
    <aspCal:CalendarTable runat="server"/>
    
      <br/>  <br/>
    <aspFeed:FeedBox runat="server" />

    <!-- Added 12/01 2015 for the ajax control toolkit to work -->
    <%--<asp:ToolkitScriptManager ID="TSM" runat="Server" />
    <asp:TextBox
        ID="txtComments"
        TextMode="MultiLine"
        Columns="60"
        Rows="8"
        runat="server" />

    <asp:HtmlEditorExtender
        ID="HtmlEditorExtender1"
        TargetControlID="txtComments"
        runat="server" />
    <asp:Button ID="BtnShow" runat="server" Text="Show text" />
    <asp:Label ID="Label1" runat="server" Text=""></asp:Label>--%>
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
   
    
</asp:Content>--%>


