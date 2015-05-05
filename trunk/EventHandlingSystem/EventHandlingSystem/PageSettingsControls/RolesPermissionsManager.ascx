<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RolesPermissionsManager.ascx.cs" Inherits="EventHandlingSystem.PageSettingsControls.RolesPermissionsManager" %>

<asp:HiddenField ID="HiddenFieldWebPageId" runat="server" />
<asp:HiddenField ID="HiddenFieldWebPageType" runat="server" />

<h3>Manage Contributors</h3>
                        <p>
                            <b>Users:</b>
                            <asp:DropDownList ID="UserList" runat="server"></asp:DropDownList>
                        </p>
<p>
    <asp:Button ID="BtnAddUser" runat="server" Text="Add" OnClick="BtnAddUser_OnClick" />
</p>

<asp:GridView ID="PermissionUserList" runat="server" AutoGenerateColumns="False" EmptyDataText="No Contributors for this Association." OnRowDeleting="PermissionUserList_OnRowDeleting">
                                <Columns>
                                    <asp:TemplateField HeaderText="Users">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="UserNameLabelInAssociation"
                                                Text='<%# Container.DataItem %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:CommandField DeleteText="Remove" ShowDeleteButton="True" />
                                </Columns>
                            </asp:GridView>