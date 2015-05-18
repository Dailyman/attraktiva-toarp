<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RolesPermissionsManager.ascx.cs" Inherits="EventHandlingSystem.PageSettingsControls.RolesPermissionsManager" %>
<%@ Import Namespace="EventHandlingSystem" %>
<%@ Import Namespace="EventHandlingSystem.Database" %>

<asp:HiddenField ID="HiddenFieldWebPageId" runat="server" />
<asp:HiddenField ID="HiddenFieldWebPageType" runat="server" />
<asp:HiddenField ID="HiddenFieldAssociationId" runat="server" />
<asp:HiddenField ID="HiddenFieldCommunityId" runat="server" />

<h3>Manage Contributors</h3>
<p align="center">
    <asp:Label ID="ActionStatus" runat="server" Text=""></asp:Label>
</p>
                        <p>
                            <b>User:</b>
                            <asp:DropDownList ID="UserList" runat="server"></asp:DropDownList>
                        </p>
<p>
                            <b>Role:</b>
                            <asp:DropDownList ID="RoleList" runat="server"></asp:DropDownList>
                        </p>
<p>
    <asp:Button ID="BtnAddUser" runat="server" Text="Add" OnClick="BtnAddUser_OnClick" />
</p>

<asp:GridView ID="PermissionUserList" runat="server" AutoGenerateColumns="False" EmptyDataText="No Permissions." OnRowDeleting="PermissionUserList_OnRowDeleting">
                                <Columns>
                                    <asp:TemplateField HeaderText="Permission Id">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="IdLabel"
                                                Text='<%# Eval("Id")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Users">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="UserNameLabel"
                                                Text='<%# UserDB.GetUserById((int)Eval("users_Id")).Username%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Role">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="RoleLabel"
                                                Text='<%# Eval("Role") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:CommandField DeleteText="Remove" ShowDeleteButton="True" />
                                </Columns>
                            </asp:GridView>