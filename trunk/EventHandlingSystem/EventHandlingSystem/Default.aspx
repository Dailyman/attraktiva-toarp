<%@ Page Title="Home" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="EventHandlingSystem.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">

    <div class="titlebox">
        <h1>Home</h1>
    </div>
    <%--<br/>
    <asp:TextBox ID="TxtBox" runat="server" type="datetime-local"></asp:TextBox>
    <br/>--%>
    <div class="warning-pattern">
    </div>
    <div class="temp-background">
        
        <h1>¡Site under construction!</h1>
        <img src="http://i.imgur.com/kgcVCEG.gif" style="position: relative; top: 100px; right: -300px; height: 180px"/>
        <br/>
        <img src="http://i.imgur.com/HB44I.jpg" style="position: relative; top: 200px; left: -50px; height: 200px;"/>
        <img src="http://media.giphy.com/media/1wDy0NewJUS4w/giphy.gif" style="position: relative; top: 200px; left: 0px; height: 200px;"/>
        <img src="http://media.giphy.com/media/92YG8KKSjYhMc/giphy.gif" style="position: relative; top: 200px; left: 50px; height: 200px;"/>
    </div>
    <div class="warning-pattern"></div>
    <asp:Menu ID="MenuHome" runat="server"></asp:Menu>
    <asp:Label ID="TestLable" runat="server" Text=""></asp:Label>

</asp:Content>
