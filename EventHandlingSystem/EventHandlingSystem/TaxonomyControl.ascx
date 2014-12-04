<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TaxonomyControl.ascx.cs" Inherits="EventHandlingSystem.TaxonomyControl" %>
    <br />
<div class="inner-content-box">
    <h3>Choose a taxonomy</h3>
    <br />
    <asp:Button ID="BtnPublishTax" runat="server" Text="Publishing taxonomy" OnClick="BtnPublishTax_OnClick" />
    <asp:Button ID="BtnCategoryTax" runat="server" Text="Category taxonomy" OnClick="BtnCategoryTax_OnClick" />
    <asp:Button ID="BtnCustomCategoryTax" runat="server" Text="Custom category taxonomy" OnClick="BtnCustomCategoryTax_OnClick" />
    <asp:TreeView ID="TreeViewTaxonomy" runat="server" OnTreeNodeCheckChanged="TreeViewTaxonomy_OnTreeNodeCheckChanged" ShowLines="True"></asp:TreeView>
    <br />
    <br />
    <div class="btn-align-right">
        <asp:Label ID="LabelDisplay" runat="server" Text=""></asp:Label>
        <br />
        <asp:Button ID="BtnClearSelected" runat="server" Text="Uncheck all" OnClick="BtnClearSelected_OnClick" />
        <asp:Button ID="BtnEdit" runat="server" Text="Edit" OnClick="BtnEdit_OnClick" />
        <asp:Button ID="BtnCreate" runat="server" Text="Create" OnClick="BtnCreate_OnClick" />
        <asp:Button ID="BtnDelete" runat="server" Text="Delete" OnClick="BtnDelete_OnClick" />
    </div>
</div>
<br />
    <div id="CreateBox" class="inner-content-box" runat="server" Visible="False">
        <asp:MultiView ID="MultiViewCreate" runat="server" ActiveViewIndex="-1">
            <asp:View ID="ViewSelectCreate" runat="server">
                <h2>Choose what to create</h2>
                <br />
                <asp:Button ID="BtnCreateTerm" runat="server" Text="Term" OnClick="BtnCreateTerm_OnClick" /><span style="width: 20px; height: 20px; display: inline-block;"></span><asp:Button ID="BtnCreateTermSet" runat="server" Text="TermSet" OnClick="BtnCreateTermSet_OnClick" />
                <br />
                <br />
            </asp:View>
            <asp:View ID="ViewTermCreate" runat="server">
                <h2>
                    <asp:Label ID="LabelCreateTerm" runat="server" Text="Label"></asp:Label></h2>
                <br />
                <br />
                <span><b>Name: </b></span>
                <asp:TextBox ID="TxtBoxNameCreateT" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidatorCreateTerm" runat="server" ErrorMessage="Required field!" ControlToValidate="TxtBoxNameCreateT" ValidationGroup="CreateTerm"></asp:RequiredFieldValidator>
                <br />
                <br />
                <span><b>Termset: </b></span>
                <asp:DropDownList ID="DropDownListTInTS" runat="server"></asp:DropDownList>
                <br />
                <br />
                <asp:ImageButton ID="ImageButtonBack1" runat="server" ImageUrl="Images/back-32x32.png" Height="32px" Width="32px" OnClick="ImageButtonBack_OnClick"/>
                <asp:Button ID="BtnCreateT" runat="server" Text="Create" OnClick="BtnCreateT_OnClick" ValidationGroup="CreateTerm" />
                <span style="width: 20px; height: 20px; display: inline-block;"></span>
                <asp:Label ID="LabelMessageCreateT" runat="server" Text=""></asp:Label>
                <br />
                <br />
            </asp:View>
            <asp:View ID="ViewTermSetCreate" runat="server">
                <h2>
                    <asp:Label ID="LabelCreateTermSet" runat="server" Text="Label"></asp:Label></h2>
                <br />
                <br />
                <span><b>Name: </b></span>
                <asp:TextBox ID="TxtBoxNameCreateTS" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidatorCreateTermSet" runat="server" ErrorMessage="Required field!" ControlToValidate="TxtBoxNameCreateTS" ValidationGroup="CreateTermSet"></asp:RequiredFieldValidator>
                <br />
                <br />
                <span><b>Parent termset: </b></span>
                <asp:DropDownList ID="DropDownListCreateParentTS" runat="server"></asp:DropDownList>
                <br />
                <br />
                <asp:ImageButton ID="ImageButtonBack2" runat="server" ImageUrl="Images/back-32x32.png" Height="32px" Width="32px" OnClick="ImageButtonBack_OnClick" />
                <asp:Button ID="BtnCreateTS" runat="server" Text="Create" OnClick="BtnCreateTS_OnClick" ValidationGroup="CreateTermSet" />
                <span style="width: 20px; height: 20px; display: inline-block;"></span>
                <asp:Label ID="LabelMessageCreateTS" runat="server" Text=""></asp:Label>
                <br />
                <br />
            </asp:View>
            <asp:View ID="ViewDelete" runat="server">
                <h2>Do you want to delete these?</h2>
                <br />
                <br />                
                <asp:CheckBoxList ID="CheckBoxListItemsToDelete" runat="server"></asp:CheckBoxList>
                <br />
                <br />
                <asp:Label ID="LabelWarning" runat="server" Text=""></asp:Label>
                <br />
                <asp:Button ID="BtnConfirmDeletion" runat="server" Text="Confirm deletion" OnClick="BtnConfirmDeletion_OnClick" />
            </asp:View>
        </asp:MultiView>
    </div>

<div id="EditBox" class="inner-content-box" runat="server" Visible="False">
    <asp:MultiView ID="MultiViewEdit" runat="server" ActiveViewIndex="-1">
        <asp:View ID="ViewTaxonomyEdit" runat="server">
            <h2>Edit taxonomy item</h2>
            <br />
            <br />
            <span><b>Id: </b></span><asp:Label ID="LabelIdTax" runat="server" Text=""></asp:Label>
            <br />
            <br />
            <span><b>Name: </b></span>
            <asp:TextBox ID="TxtBoxNameTax" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidatorEditTax" runat="server" ErrorMessage="Required field!" ControlToValidate="TxtBoxNameTax" ValidationGroup="EditTax"></asp:RequiredFieldValidator>
            <br />
            <br />
            <asp:Label ID="LabelCreatedTax" runat="server" Text=""></asp:Label>
            <br />
            <br />
            <asp:Button ID="BtnUpdateTax" runat="server" Text="Update" OnClick="BtnUpdateTax_OnClick" ValidationGroup="EditTax" />
            <span style="width: 20px; height: 20px; display: inline-block;"></span>
            <asp:Label ID="LabelMessageTax" runat="server" Text=""></asp:Label>
            <br />
            <br />
        </asp:View>
        <asp:View ID="ViewTermSetEdit" runat="server">
            <h2>Edit Termset item</h2>
            <br />
            <br />
            <span><b>Taxonomy name: </b></span><asp:Label ID="LabelTaxNameTSView" runat="server" Text=""></asp:Label>
            <br />
            <br />
            <span><b>Id: </b></span><asp:Label ID="LabelIdTS" runat="server" Text=""></asp:Label>
            <br />
            <br />
            <span><b>Name: </b></span>
            <asp:TextBox ID="TxtBoxNameTS" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidatorEditTS" runat="server" ErrorMessage="Required field!" ControlToValidate="TxtBoxNameTS" ValidationGroup="EditTS"></asp:RequiredFieldValidator>
            <br />
            <br />
            <span><b>Parent termset: </b></span>
            <asp:DropDownList ID="DropDownListEditParentTS" runat="server"></asp:DropDownList>
            <br />
            <br />
            <asp:Label ID="LabelCreatedTS" runat="server" Text=""></asp:Label>
            <br />
            <br />
            <asp:Button ID="BtnUpdateTS" runat="server" Text="Update" OnClick="BtnUpdateTS_OnClick" ValidationGroup="EditTS" />
            <span style="width: 20px; height: 20px; display: inline-block;"></span>
            <asp:Label ID="LabelMessageTS" runat="server" Text=""></asp:Label>
            <br />
            <br />
        </asp:View>
        <asp:View ID="ViewTermEdit" runat="server">
            <h2>Edit Term item</h2>
            <br />
            <br />
            <span><b>Taxonomy name: </b></span><asp:Label ID="LabelTaxNameTView" runat="server" Text=""></asp:Label>
            <br />
            <br />
            <span><b>Id: </b></span><asp:Label ID="LabelIdT" runat="server" Text="Label"></asp:Label>
            <br />
            <br />
            <span><b>Name: </b></span>
            <asp:TextBox ID="TxtBoxNameT" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidatorEditT" runat="server" ErrorMessage="Required field!" ControlToValidate="TxtBoxNameT" ValidationGroup="EditT"></asp:RequiredFieldValidator>
            <br />
            <br />
            <span><b>Termset: </b></span>
            <asp:DropDownList ID="DropDownListTermSetForTerm" runat="server"></asp:DropDownList>
            <br />
            <br />
            <asp:Label ID="LabelCreatedT" runat="server" Text=""></asp:Label>
            <br />
            <br />
            <asp:Button ID="BtnUpdateT" runat="server" Text="Update" OnClick="BtnUpdateT_OnClick" ValidationGroup="EditT" />
            <span style="width: 20px; height: 20px; display: inline-block;"></span>
            <asp:Label ID="LabelMessageT" runat="server" Text=""></asp:Label>
            <br />
            <br />
        </asp:View>
    </asp:MultiView>
</div>

<br />


