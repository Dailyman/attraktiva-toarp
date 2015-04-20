<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PageManager.ascx.cs" Inherits="EventHandlingSystem.PageSettingsControlls.PageManager" %>
<style type="text/css">
    .bigger-link
    {
        font-family: Symbola;
        text-decoration: none;
        font-size: 50px;
    }
.edit-link {
    color: #000 !important;
}
.edit-link:hover {
    color: cornflowerblue !important;
}
    .delete-mode-link
    {
        color: #000 !important;
    }

        .delete-mode-link:hover
        {
            color: #FF0000 !important;
        }

    .edit-mode-link
    {
        color: #FFF !important;
    }

        .edit-mode-link:hover
        {
            color: #000 !important;
        }

        .header-cell-style {
            text-align: center !important;
        }

    .tb-small
    {
        font-size: 16px;
        width: 50px;
    }

    
</style>

<asp:HiddenField ID="HiddenFieldWebPageId" runat="server" />

<p align="center">
    <asp:Label ID="ActionStatus" runat="server" Text=""></asp:Label>
</p>

<asp:Panel ID="PanelContent" runat="server">
    <asp:MultiView ID="MultiViewWepPageManager" runat="server">
        <Views>

            <asp:View ID="DisplayView" runat="server">
                <h2 align="center">Page details</h2>
                <br />
                <h5>Title</h5>
                <asp:Label ID="LabelWepPageTitle" runat="server"></asp:Label>
                <h5>Layout</h5>
                <asp:Label ID="LabelWepPageLayout" runat="server"></asp:Label>
                <h5>Style</h5>
                <asp:Label ID="LabelWepPageStyle" runat="server"></asp:Label>
                <h5>Latest update</h5>
                <asp:Label ID="LabelWebPageLatestUpdate" runat="server"></asp:Label>
                <h5>Updated by</h5>
                <asp:Label ID="LabelWebPageUpdatedBy" runat="server"></asp:Label>
                <br />
                <br />
                <asp:LinkButton ID="LinkBtnEditWepPage" runat="server" OnClick="LinkBtnEditWepPage_OnClick" CssClass="bigger-link" ToolTip="Edit this webpage">✏</asp:LinkButton>
            </asp:View>


            <asp:View ID="EditView" runat="server">
                <h2 align="center">Edit page details</h2>
                <p>
                    <b>Title</b>
                    <br />
                    <asp:TextBox ID="TxtBoxWepPageTitle" runat="server"></asp:TextBox>
                </p>
                <p>
                    <b>Layout</b>
                    <br />
                    <asp:TextBox ID="TxtBoxWepPageLayout" runat="server"></asp:TextBox>
                </p>
                <p>
                    <b>Style</b>
                    <br />
                    <asp:TextBox ID="TxtBoxWepPageStyle" runat="server"></asp:TextBox>
                </p>
                <asp:Button ID="BtnSaveChanges" runat="server" Text="Save" OnClick="BtnSaveChanges_OnClick" />
                <asp:Button ID="BtnCancelChanges" runat="server" Text="Cancel" OnClick="BtnCancelChanges_OnClick" />
            </asp:View>

        </Views>
    </asp:MultiView>

    <h2 align="center">Components on webpage</h2>

    <p align="center">
        <asp:Label ID="ActionStatusComponentsList" runat="server"></asp:Label>
    </p>
    <asp:GridView ID="GridViewComponentList" runat="server"
        AutoGenerateColumns="False"
        OnRowCancelingEdit="GridViewComponentList_OnRowCancelingEdit"
        OnRowEditing="GridViewComponentList_OnRowEditing"
        OnRowUpdating="GridViewComponentList_OnRowUpdating"
        OnRowUpdated="GridViewComponentList_OnRowUpdated"
        OnRowDeleting="GridViewComponentList_OnRowDeleting"
        CellPadding="4"
        ForeColor="#333333"
        GridLines="None"
        CssClass="user-list-table"
        EmptyDataText="No Componets">
        <AlternatingRowStyle BackColor="White" />
        <EditRowStyle BackColor="#2461BF" />
        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" CssClass="header-cell-style" />
        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
        <RowStyle BackColor="#EFF3FB" />
        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
        <SortedAscendingCellStyle BackColor="#F5F7FB" />
        <SortedAscendingHeaderStyle BackColor="#6D95E1" />
        <SortedDescendingCellStyle BackColor="#E9EBEF" />
        <SortedDescendingHeaderStyle BackColor="#4870BE" />
        <Columns>
            <%--<asp:BoundField DataField="Item1" HeaderText="Ordering Number" />--%>
            <asp:BoundField DataField="Item1" HeaderText="Id" ReadOnly="True" />
            <asp:TemplateField HeaderText="Ordering Number">
                <ItemTemplate>
                    <%# Eval("Item2")  %>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="TextBoxOrderingNO" runat="server"
                        Text='<%#Eval("Item2") %>'
                        TextMode="Number"></asp:TextBox>
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Control Name">
                <ItemTemplate>
                    <%# Eval("Item3")  %>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:DropDownList ID="DropDownListControlName" runat="server"
                        DataSourceID="DataSource"
                        DataValueField="Value"
                        DataTextField="Text"
                        Text='<%#Eval("Item4") %>'>
                    </asp:DropDownList>
                    <asp:ObjectDataSource runat="server"
                        ID="DataSource"
                        SelectMethod="GetAllControlsListItems"
                        DataObjectTypeName="System.Web.UI.WebControls.ListItem"
                        TypeName="EventHandlingSystem.PageSettingsControlls.PageManager"></asp:ObjectDataSource>
                </EditItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField>
                <ItemTemplate>
                    <asp:LinkButton ID="LinkButtonEditEvent" ClientIDMode="Static" runat="server" CausesValidation="False" CommandName="Edit" Text="Edit" CssClass="edit-link"></asp:LinkButton>
                    <asp:LinkButton ID="LinkButtonUpdateEvent" ClientIDMode="Static" runat="server" CausesValidation="False" CommandName="Update" Text="Update" Visible="False" CssClass="edit-mode-link" />
                    <asp:LinkButton ID="LinkButtonCancelEdit" ClientIDMode="Static" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel" Visible="False" CssClass="edit-mode-link" />
                    <asp:LinkButton ID="LinkButtonDeleteEvent" ClientIDMode="Static" runat="server" CausesValidation="False"
                        CommandName="Delete" Text="Remove"
                        OnClientClick="return confirm('Are you sure you want to remove this component?');"
                        CssClass="delete-mode-link"></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>

            <%--<asp:CommandField ShowEditButton="True" ShowDeleteButton="True"/>--%>

            <%--<asp:BoundField DataField="PasswordQuestion" HeaderText="PasswordQuestion" ReadOnly="True"/>
        <asp:CheckBoxField DataField="IsLockedOut" HeaderText="IsLockedOut" ReadOnly="true" />
        <asp:BoundField DataField="LastLockoutDate" HeaderText="LastLockoutDate" ReadOnly="true" />
        <asp:BoundField DataField="CreationDate" HeaderText="CreationDate" ReadOnly="true" />
        <asp:BoundField DataField="LastLoginDate" HeaderText="LastLoginDate" ReadOnly="true" />
        <asp:BoundField DataField="LastActivityDate" HeaderText="LastActivityDate" ReadOnly="true" />
        <asp:BoundField DataField="LastPasswordChangedDate" HeaderText="LastPasswordChangedDate" ReadOnly="true" />--%>
        </Columns>
    </asp:GridView>


    <asp:TextBox ID="tbAddOrderingNumber" runat="server"
        TextMode="Number"
        CssClass="tb-small"
        Text="1" min="1"></asp:TextBox>
    <asp:RegularExpressionValidator ID="RegularExpressionValidator1"
        runat="server"
        ErrorMessage="Use Numbers Only!!! (^O^)v"
        ControlToValidate="tbAddOrderingNumber"
        ValidationExpression="^[0-9]+$"
        Display="Dynamic"
        SetFocusOnError="True">
    </asp:RegularExpressionValidator>
    <asp:DropDownList ID="ddlAddComControls" runat="server"></asp:DropDownList>
    <asp:Button ID="AddControl" runat="server"
        Text="Add Component"
        CssClass="btn-small"
        OnClick="AddControl_OnClick" />
</asp:Panel>
