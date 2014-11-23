<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EventDetails.aspx.cs" Inherits="EventHandlingSystem.EventDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <br />
    <%--<asp:TextBox ID="TxtBoxSearch" runat="server" Enabled="False"></asp:TextBox>--%>
    <asp:DropDownList ID="DropDownListEvents" runat="server" Enabled="False" Visible="False"></asp:DropDownList>
    <%--<asp:CompareValidator ID="CompValiSearch" runat="server" ControlToValidate="TxtBoxSearch" Type="Integer" Operator="DataTypeCheck" ErrorMessage="Value must be an integer!" ValidationGroup="ValGroupSearchEvent" Display="Dynamic" SetFocusOnError="True" Enabled="False" Visible="False"/>--%>
    <asp:Button ID="BtnSearch" runat="server" Text="Load event" OnClick="BtnSearch_OnClick" Enabled="False" Visible="False" />
    <div class="content-box">
        <div id="Main" runat="server"></div>
    </div>
</asp:Content>
