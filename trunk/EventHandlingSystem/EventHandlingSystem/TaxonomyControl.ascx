<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TaxonomyControl.ascx.cs" Inherits="EventHandlingSystem.TaxonomyControl" %>
<h1>Create your terms here ^^v</h1>
<br />
<asp:Label ID="LabelDisplay" runat="server" Text=""></asp:Label>
<br />
<br />
<asp:Button ID="BtnPublishTax" runat="server" Text="Publishing taxonomy" OnClick="BtnPublishTax_OnClick"/>
<asp:Button ID="BtnCategoryTax" runat="server" Text="Category taxonomy" OnClick="BtnCategoryTax_OnClick"/>
<asp:Button ID="BtnCustomCategoryTax" runat="server" Text="Custom category taxonomy" OnClick="BtnCustomCategoryTax_OnClick"/>
<br />
<div class="content-box">
    <asp:TreeView ID="TreeViewTaxonomy" runat="server" OnTreeNodeCheckChanged="TreeViewTaxonomy_OnTreeNodeCheckChanged" ShowLines="True"></asp:TreeView>
    <div class="btn-align-right">
        <asp:Button ID="BtnClearSelected" runat="server" Text="Uncheck all"  OnClick="BtnClearSelected_OnClick"/>
        <asp:Button ID="BtnEdit" runat="server" Text="Edit" OnClick="BtnEdit_OnClick" />
        <asp:Button ID="BtnDelete" runat="server" Text="Delete" OnClick="BtnDelete_OnClick" />
    </div>
</div>
<br />


