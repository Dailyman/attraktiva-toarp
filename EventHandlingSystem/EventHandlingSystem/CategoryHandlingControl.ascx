<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CategoryHandlingControl.ascx.cs" Inherits="EventHandlingSystem.CategoryHandlingControl" %>

<div class="inner-content-box">
    <asp:TreeView ID="TreeViewCategories" runat="server" ShowLines="False" SkipLinkText=""></asp:TreeView>
    <asp:Label ID="LabelDisplay" runat="server" Text=""></asp:Label>
    <div class="float-right">
        <asp:Button ID="BtnClearSelected" CssClass="btn-blue" runat="server" Text="Uncheck all" OnClick="BtnClearSelected_OnClick" />
        <asp:Button ID="BtnEdit" CssClass="btn-blue" runat="server" Text="Edit" OnClick="BtnEdit_OnClick" />
        <asp:Button ID="BtnCreate" CssClass="btn-blue" runat="server" Text="Create" OnClick="BtnCreate_OnClick" />
        <asp:Button ID="BtnDelete" CssClass="btn-blue" runat="server" Text="Delete" OnClick="BtnDelete_OnClick" />
    </div>

    <br />
    <div id="CreateBox" class="inner-content-box" runat="server" visible="False">
        <asp:MultiView ID="MultiViewCreate" runat="server" ActiveViewIndex="-1">
            <asp:View ID="ViewTermSetCreate" runat="server">
                <asp:ImageButton ID="ImageButtonClose2" runat="server" ImageUrl="/Images/close-round-32x32.png" Height="20px" Width="20px" OnClick="ImageButtonCloseMultiViewCreate_OnClick" />
                <h2>
                    <asp:Label ID="LabelCreateCategory" runat="server" Text="Create a category"></asp:Label></h2>
                <br />
                <br />
                <span><b>Name: </b></span>
                <asp:TextBox ID="TxtBoxNameCreateCategoryName" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidatorCreateTermSet" runat="server" ErrorMessage="Required field!" ControlToValidate="TxtBoxNameCreateCategoryName" ValidationGroup="CreateCategory"></asp:RequiredFieldValidator>
                <br />
                <br />
                <span><b>Parent Category: </b></span>
                <asp:DropDownList ID="DropDownListCreateParentCategory" runat="server"></asp:DropDownList>
                <br />
                <br />
                <asp:Button ID="BtnCreateCategory" runat="server" Text="Create" OnClick="BtnCreateCategory_OnClick" ValidationGroup="CreateCategory" />
                <span style="width: 20px; height: 20px; display: inline-block;"></span>
                <asp:Label ID="LabelMessageCreateCategory" runat="server" Text=""></asp:Label>
                <br />
                <br />
            </asp:View>
        </asp:MultiView>
    </div>

    <div id="EditBox" class="inner-content-box" runat="server" visible="False">
        <asp:MultiView ID="MultiViewEdit" runat="server" ActiveViewIndex="-1">
            <asp:View ID="ViewCategoryEdit" runat="server">
                <asp:ImageButton ID="ImageButtonClose3" runat="server" ImageUrl="/Images/close-round-32x32.png" Height="20px" Width="20px" OnClick="ImageButtonCloseMultiViewEdit_OnClick" />
                <h2>Edit Category item</h2>
                <br />
                <br />
                <span><b>Id: </b></span>
                <asp:Label ID="LabelIdC" runat="server" Text=""></asp:Label>
                <br />
                <br />
                <span><b>Name: </b></span>
                <asp:TextBox ID="TxtBoxNameC" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidatorEditC" runat="server" ErrorMessage="Required field!" ControlToValidate="TxtBoxNameC" ValidationGroup="EditCategory"></asp:RequiredFieldValidator>
                <br />
                <br />
                <asp:Button ID="BtnUpdateCategory" runat="server" Text="Update" OnClick="BtnUpdateCategory_OnClick" ValidationGroup="EditCategory" />
                <span style="width: 20px; height: 20px; display: inline-block;"></span>
                <asp:Label ID="LabelMessageC" runat="server" Text=""></asp:Label>
                <br />
                <br />
            </asp:View>
            <asp:View ID="ViewSubCategoryEdit" runat="server">
                <asp:ImageButton ID="ImageButtonClose4" runat="server" ImageUrl="/Images/close-round-32x32.png" Height="20px" Width="20px" OnClick="ImageButtonCloseMultiViewEdit_OnClick" />
                <h2>Edit SubCategory item</h2>
                <br />
                <br />
                <span><b>Id: </b></span>
                <asp:Label ID="LabelIdSC" runat="server" Text="Label"></asp:Label>
                <br />
                <br />
                <span><b>Name: </b></span>
                <asp:TextBox ID="TxtBoxNameSC" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidatorEditSC" runat="server" ErrorMessage="Required field!" ControlToValidate="TxtBoxNameSC" ValidationGroup="EditSC"></asp:RequiredFieldValidator>
                <br />
                <br />
                <span><b>Category: </b></span>
                <asp:DropDownList ID="DropDownListCategoryForSubCategory" runat="server"></asp:DropDownList>
                <br />
                <br />
                <asp:Button ID="BtnUpdateSC" runat="server" Text="Update" OnClick="BtnUpdateSC_OnClick" ValidationGroup="EditSC" />
                <span style="width: 20px; height: 20px; display: inline-block;"></span>
                <asp:Label ID="LabelMessageSC" runat="server" Text=""></asp:Label>
                <br />
                <br />
            </asp:View>
            <asp:View ID="ViewDelete" runat="server">
                <asp:ImageButton ID="ImageButtonClose5" runat="server" ImageUrl="/Images/close-round-32x32.png" Height="20px" Width="20px" OnClick="ImageButtonCloseMultiViewEdit_OnClick" />
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
</div>
