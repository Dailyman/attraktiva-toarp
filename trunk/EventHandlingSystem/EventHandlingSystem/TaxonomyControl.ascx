<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TaxonomyControl.ascx.cs" Inherits="EventHandlingSystem.TaxonomyControl" %>


<br />
<div class="content-box">
    <div class="titlebox">
        <h1>Create your terms here ^^v</h1>
    </div>
    <br />

    <asp:MultiView ID="MultiViewEdit" runat="server" ActiveViewIndex="-1">
        <asp:View ID="ViewTaxonomyEdit" runat="server">
            <h2>Edit taxonomy item</h2>
            <h6>Id</h6>
            <asp:TextBox ID="TxtBoxIdTax" runat="server" Enabled="False"></asp:TextBox>
            <br />
            <h6>Name</h6>
            <asp:TextBox ID="TxtBoxNameTax" runat="server"></asp:TextBox>
            <br />
            <asp:Label ID="LabelCreatedTax" runat="server" Text=""></asp:Label>
            <br />
            <br />
            <asp:Button ID="BtnUpdateTax" runat="server" Text="Update" OnClick="BtnUpdateTax_OnClick" />
            <asp:Label ID="LabelMessageTax" runat="server" Text=""></asp:Label>
            <br />
        </asp:View>
        <asp:View ID="ViewTermSetEdit" runat="server">
            <h2>Edit Termset item</h2>
            <h6>Id</h6>
            <asp:TextBox ID="TxtBoxIdTS" runat="server" Enabled="False"></asp:TextBox>
            <br />
            <h6>Name</h6>
            <asp:TextBox ID="TxtBoxNameTS" runat="server"></asp:TextBox>
            <br />
            <asp:Label ID="LabelCreatedTS" runat="server" Text=""></asp:Label>
            <br />
            <br />
            <asp:Button ID="BtnUpdateTS" runat="server" Text="Update" OnClick="BtnUpdateTS_OnClick" />
            <asp:Label ID="LabelMessageTS" runat="server" Text=""></asp:Label>
            <br />
        </asp:View>
        <asp:View ID="ViewTermEdit" runat="server">
            <h2>Edit Term item</h2>
            <h6>Id</h6>
            <asp:TextBox ID="TxtBoxIdT" runat="server" Enabled="False"></asp:TextBox>
            <br />
            <h6>Name</h6>
            <asp:TextBox ID="TxtBoxNameT" runat="server"></asp:TextBox>
            <br />
            <asp:Label ID="LabelCreatedT" runat="server" Text=""></asp:Label>
            <br />
            <br />
            <asp:Button ID="BtnUpdateT" runat="server" Text="Update" OnClick="BtnUpdateT_OnClick" />
            <asp:Label ID="LabelMessageT" runat="server" Text=""></asp:Label>
            <br />
        </asp:View>
    </asp:MultiView>

    <br />
    <h3>Choose a taxonomy</h3>
    <asp:Label ID="LabelDisplay" runat="server" Text=""></asp:Label>
    <br />
    <asp:Button ID="BtnPublishTax" runat="server" Text="Publishing taxonomy" OnClick="BtnPublishTax_OnClick" />
    <asp:Button ID="BtnCategoryTax" runat="server" Text="Category taxonomy" OnClick="BtnCategoryTax_OnClick" />
    <asp:Button ID="BtnCustomCategoryTax" runat="server" Text="Custom category taxonomy" OnClick="BtnCustomCategoryTax_OnClick" />
    <asp:TreeView ID="TreeViewTaxonomy" runat="server" OnTreeNodeCheckChanged="TreeViewTaxonomy_OnTreeNodeCheckChanged" ShowLines="True"></asp:TreeView>
    <div class="btn-align-right">
        <asp:Button ID="BtnClearSelected" runat="server" Text="Uncheck all" OnClick="BtnClearSelected_OnClick" />
        <asp:Button ID="BtnEdit" runat="server" Text="Edit" OnClick="BtnEdit_OnClick" />
        <asp:Button ID="BtnDelete" runat="server" Text="Delete" OnClick="BtnDelete_OnClick" />
    </div>
</div>
<br />


