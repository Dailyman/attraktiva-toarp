<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TaxonomyControl.ascx.cs" Inherits="EventHandlingSystem.TaxonomyControl" %>


<br />
<div class="content-box">
    <div class="titlebox">
        <h1>Create your terms here ^^v</h1>
    </div>
    <br />
    <asp:MultiView ID="MultiViewCreate" runat="server" ActiveViewIndex="-1">
        <asp:View ID="ViewSelectCreate" runat="server">
            <h2>Choose what to create</h2>
            <br />
            <asp:Button ID="BtnCreateTerm" runat="server" Text="Term" OnClick="BtnCreateTerm_OnClick"/><asp:Button ID="BtnCreateTermSet" runat="server" Text="TermSet" OnClick="BtnCreateTermSet_OnClick" />
            <br />
        </asp:View>
        <asp:View ID="ViewTermCreate" runat="server">
            <h2 ID="H2CreateTerm" runat="server"></h2>
            <br /><br />
            <span><b>Name: </b></span><asp:TextBox ID="TxtBoxNameCreateT" runat="server"></asp:TextBox>
            <br /><br />
            <span><b>Termset: </b></span><asp:DropDownList ID="DropDownListTInTS" runat="server"></asp:DropDownList>
            <br /><br />
            <asp:Button ID="BtnCreateT" runat="server" Text="Create" OnClick="BtnCreateT_OnClick" />
            <asp:Label ID="LabelMessageCreateT" runat="server" Text=""></asp:Label>
            <br />
        </asp:View>
        <asp:View ID="ViewTermSetCreate" runat="server">
            <h2 ID="H2CreateTermSet" runat="server"></h2>
            <br /><br />
            <span><b>Name: </b></span><asp:TextBox ID="TxtBoxNameCreateTS" runat="server"></asp:TextBox>
            <br /><br />
            <span><b>Parent termset: </b></span><asp:DropDownList ID="DropDownListCreateParentTS" runat="server"></asp:DropDownList>
            <br /><br />
            <asp:Button ID="BtnCreateTS" runat="server" Text="Create" OnClick="BtnCreateTS_OnClick" />
            <asp:Label ID="LabelMessageCreateTS" runat="server" Text=""></asp:Label>
            <br />
        </asp:View>
    </asp:MultiView>
    

    <asp:MultiView ID="MultiViewEdit" runat="server" ActiveViewIndex="-1">
        <asp:View ID="ViewTaxonomyEdit" runat="server">
            <h2>Edit taxonomy item</h2>
            <br /><br />
            <asp:Label ID="LabelIdTxtTax" runat="server" Text="Id: " CssClass="bold-txt"></asp:Label><asp:Label ID="LabelIdTax" runat="server" Text=""></asp:Label>
            <br /><br />
            <span><b>Name: </b></span><asp:TextBox ID="TxtBoxNameTax" runat="server"></asp:TextBox>
            <br /><br />
            <asp:Label ID="LabelCreatedTax" runat="server" Text=""></asp:Label>
            <br /><br />
            <asp:Button ID="BtnUpdateTax" runat="server" Text="Update" OnClick="BtnUpdateTax_OnClick" />
            <asp:Label ID="LabelMessageTax" runat="server" Text=""></asp:Label>
            <br />
        </asp:View>
        <asp:View ID="ViewTermSetEdit" runat="server">
            <h2>Edit Termset item</h2>
            <br /><br />
            <asp:Label ID="LabelIdTxtTS" runat="server" Text="Id: " CssClass="bold-txt"></asp:Label><asp:Label ID="LabelIdTS" runat="server" Text=""></asp:Label>
            <br /><br />
            <span><b>Name: </b></span><asp:TextBox ID="TxtBoxNameTS" runat="server"></asp:TextBox>
            <br /><br />
            <span><b>Parent termset: </b></span><asp:DropDownList ID="DropDownListEditParentTS" runat="server"></asp:DropDownList>
            <br /><br />
            <asp:Label ID="LabelCreatedTS" runat="server" Text=""></asp:Label>
            <br /><br />
            <asp:Button ID="BtnUpdateTS" runat="server" Text="Update" OnClick="BtnUpdateTS_OnClick" />
            <asp:Label ID="LabelMessageTS" runat="server" Text=""></asp:Label>
            <br />
        </asp:View>
        <asp:View ID="ViewTermEdit" runat="server">
            <h2>Edit Term item</h2>
            <br /><br />
            <asp:Label ID="LabelIdTxtT" runat="server" Text="Id: " CssClass="bold-txt"></asp:Label><asp:Label ID="LabelIdT" runat="server" Text="Label"></asp:Label> 
            <br /><br />      
            <span><b>Name: </b></span><asp:TextBox ID="TxtBoxNameT" runat="server"></asp:TextBox>
            <br /><br />
            <span><b>Termset: </b></span><asp:DropDownList ID="DropDownListTermSetForTerm" runat="server"></asp:DropDownList>
            <br /><br />
            <asp:Label ID="LabelCreatedT" runat="server" Text=""></asp:Label>
            <br /><br />
            <asp:Button ID="BtnUpdateT" runat="server" Text="Update" OnClick="BtnUpdateT_OnClick" />
            <asp:Label ID="LabelMessageT" runat="server" Text=""></asp:Label>
            <br />
        </asp:View>
    </asp:MultiView>

    <br />
    <h3>Choose a taxonomy</h3>
    <br />
    <asp:Button ID="BtnPublishTax" runat="server" Text="Publishing taxonomy" OnClick="BtnPublishTax_OnClick" />
    <asp:Button ID="BtnCategoryTax" runat="server" Text="Category taxonomy" OnClick="BtnCategoryTax_OnClick" />
    <asp:Button ID="BtnCustomCategoryTax" runat="server" Text="Custom category taxonomy" OnClick="BtnCustomCategoryTax_OnClick" />
    <asp:TreeView ID="TreeViewTaxonomy" runat="server" OnTreeNodeCheckChanged="TreeViewTaxonomy_OnTreeNodeCheckChanged" ShowLines="True"></asp:TreeView>
    <br /><br />
    <asp:Label ID="LabelDisplay" runat="server" Text=""></asp:Label>
    <div class="btn-align-right">
        
        <asp:Button ID="BtnClearSelected" runat="server" Text="Uncheck all" OnClick="BtnClearSelected_OnClick" />
        <asp:Button ID="BtnEdit" runat="server" Text="Edit" OnClick="BtnEdit_OnClick" />
        <asp:Button ID="BtnCreate" runat="server" Text="Create" OnClick="BtnCreate_OnClick"/>
        <asp:Button ID="BtnDelete" runat="server" Text="Delete" OnClick="BtnDelete_OnClick" />
    </div>
</div>
<br />


