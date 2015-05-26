<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RolesPermissionsControl.ascx.cs" Inherits="EventHandlingSystem.RolesPermissionsControl" %>
<%@ Import Namespace="EventHandlingSystem.Database" %>

<style type="text/css">
    .content-block
    {
        /*vertical-align: top;*/
        /*display: block;*/
    }
    /*.content-block select {
            margin: 0;
display: inline;
vertical-align: middle;
        }
        .content-block input {
            vertical-align: middle;
        }*/
    .bullet-list-users
    {
        list-style-type: none;
        padding: 2px;
    }

    .Important
    {
        font-size: large;
        color: Red;
        -ms-word-break: break-all;
        -moz-word-break: break-all;
        -o-word-break: break-all;
        word-break: break-all;
    }

    .content-table td
    {
        vertical-align: top;
    }

    .bigger-listbox
    {
        min-height: 120px;
    }

    .toogle-tr
    {
        background-color: gainsboro;
        -ms-border-radius: 5px;
        -moz-border-radius: 5px;
        -webkit-border-radius: 5px;
        border-radius: 5px;
        padding: 5px;
        text-decoration: none;
        font-size: 25px;
        font-weight: bold;
        color: black;
        cursor: pointer;
    }
</style>

<%--<script type="text/javascript">
    $(function () {
        if (!isPostback) {
            $('tr#Manage-role').hide();
            $('tr#Manage-asso-perm').hide();
            $('tr#Manage-comm-perm').hide();
        }

        //if (isPostback) {
        //    $('tr#Manage-role').hide();
        //    $('tr#Manage-asso-perm').hide();
        //    $('tr#Manage-comm-perm').hide();
        //}

    });

    $(function () {

        var trRole = $('tr#Manage-role');
        var trAssoPerm = $('tr#Manage-asso-perm');
        var trCommPerm = $('tr#Manage-comm-perm');

        //    trRole.toggle();
        //trAssoPerm.toggle();
        //trCommPerm.toggle();

        $('a#Manage-role-btn').click(function () {
            trRole.toggle("fast");

        });

        $('a#Manage-asso-btn').click(function () {
            trAssoPerm.toggle("fast");
        });

        $('a#Manage-comm-btn').click(function () {
            trCommPerm.toggle("fast");
        });
    });
</script>--%>

<br />

<div class="inner-content-box">

    <p>
        Select what you want to manage:
    <asp:DropDownList ID="SelectManagement" runat="server" AutoPostBack="True" OnSelectedIndexChanged="SelectManagement_OnSelectedIndexChanged">
        <Items>
            <asp:ListItem Text="Roles" Value="0"></asp:ListItem>
            <asp:ListItem Text="Association permissions" Value="1"></asp:ListItem>
            <asp:ListItem Text="Community permissions" Value="2"></asp:ListItem>
        </Items>
    </asp:DropDownList>
    </p>


    <asp:MultiView ID="MultiViewManagement" runat="server">

        <asp:View runat="server">
            <%--<a id="Manage-role-btn" class="toogle-tr">Manage roles</a>--%>
            <table class="content-table" id="Manage-role">
                <tbody>
                    <tr>
                        <td colspan="3">
                            <hr />
                            <br />
                            <p align="center">
                                <asp:Label ID="ActionStatus" runat="server" CssClass="Important"></asp:Label>
                            </p>
                        </td>
                    </tr>

                    <tr>
                        <td>
                            <div class="content-block">
                                <h3>Manage Users By Role</h3>
                                <p>
                                    <b>Select a Role:</b>

                                    <asp:DropDownList ID="RoleList" runat="server" AutoPostBack="true" OnSelectedIndexChanged="RoleList_SelectedIndexChanged"></asp:DropDownList>
                                </p>
                                <p>
                                    <asp:GridView ID="RolesUserList" runat="server" AutoGenerateColumns="False" EmptyDataText="No users belong to this role." OnRowDeleting="RolesUserList_RowDeleting">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Users">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="UserNameLabelInRole"
                                                        Text='<%# Container.DataItem %>'></asp:Label>

                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:CommandField DeleteText="Remove" ShowDeleteButton="True" />
                                        </Columns>
                                    </asp:GridView>
                                </p>
                                <p>
                                    <b>UserName:</b>
                                    <asp:TextBox ID="UserNameToAddToRole" runat="server"></asp:TextBox>
                                    <br />
                                    <asp:Button ID="AddUserToRoleButton" runat="server" Text="Add User to Role" OnClick="AddUserToRoleButton_Click" />

                                </p>

                            </div>
                        </td>
                        <td>
                            <div class="content-block">
                                <h3>Manage Roles By User</h3>

                                <p>
                                    <b>Select a User:</b>
                                    <asp:DropDownList ID="UserList1" runat="server" AutoPostBack="True"
                                        DataTextField="UserName" DataValueField="UserName" OnSelectedIndexChanged="UserList1_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </p>
                                <asp:Repeater ID="UsersRoleList" runat="server">
                                    <ItemTemplate>
                                        <asp:CheckBox runat="server" ID="RoleCheckBox"
                                            AutoPostBack="true"
                                            Text='<%# Container.DataItem %>'
                                            OnCheckedChanged="RoleCheckBox_CheckChanged" />
                                        <br />
                                    </ItemTemplate>
                                </asp:Repeater>
                            </div>

                        </td>
                    </tr>
                </tbody>
            </table>


        </asp:View>
        <asp:View runat="server">


            <%-- <a id="Manage-asso-btn" class="toogle-tr">Manage association permissions</a>--%>
            <table class="content-table" id="Manage-asso-perm">
                <tbody>
                    <tr>
                        <td colspan="3">
                            <hr />
                            <br />
                            <p align="center">
                                <asp:Label ID="ActionStatusPermissions1" runat="server" CssClass="Important"></asp:Label>
                            </p>
                        </td>
                    </tr>

                    <tr>
                        <td>
                            <div class="content-block">
                                <h3>Manage Users By Association</h3>
                                <p>
                                    <b>Select an Association:</b>

                                    <asp:DropDownList ID="AssociationList" runat="server" AutoPostBack="true" OnSelectedIndexChanged="AssociationList_OnSelectedIndexChanged"></asp:DropDownList>
                                </p>
                                <p>
                                    <asp:GridView ID="AssociationUserList" runat="server" AutoGenerateColumns="False" EmptyDataText="No users has permission to this association." OnRowDeleting="AssociationUserList_OnRowDeleting">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Perm.Id">
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
                                </p>
                                <br />
                                <p>
                                    <b>Select a User:</b>
                                    <asp:DropDownList ID="UserToAddList1" runat="server" DataTextField="UserName" DataValueField="UserName"></asp:DropDownList>
                                    <br />
                                    <b>and a Role:</b>
                                    <asp:DropDownList ID="RoleToAddList1" runat="server"></asp:DropDownList>
                                    <br />
                                    <asp:Button ID="AddUserToAssociation" runat="server" Text="Add user to association" OnClick="AddUserToAssociation_OnClick" />
                                </p>
                            </div>

                        </td>
                        <td>
                            <div class="content-block">
                                <h3>Manage Association Permissions By User</h3>

                                <p>
                                    <b>Select a User:</b>
                                    <asp:DropDownList ID="UserList2" runat="server" AutoPostBack="True"
                                        DataTextField="UserName" DataValueField="UserName" OnSelectedIndexChanged="UserList2_OnSelectedIndexChanged">
                                    </asp:DropDownList>
                                    <b>and a Role:</b>
                                    <asp:DropDownList ID="RoleList2" runat="server" AutoPostBack="True" OnSelectedIndexChanged="RoleList2_OnSelectedIndexChanged">
                                    </asp:DropDownList>
                                </p>
                                <p>
                                    <table>
                                        <thead>
                                            <tr>
                                                <th>Associations</th>
                                                <th></th>
                                                <th>Associations user has permission to</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <tr>
                                                <td>
                                                    <asp:ListBox ID="AssociationsListBox" runat="server" SelectionMode="Multiple" CssClass="bigger-listbox"></asp:ListBox>
                                                </td>
                                                <td>
                                                    <p align="center">
                                                        <asp:Button ID="AddAssociation" runat="server" Text="> Add >" OnClick="AddAssociation_OnClick" />
                                                        <asp:Button ID="RemoveAssociation" runat="server" Text="< Remove <" OnClick="RemoveAssociation_OnClick" />
                                                    </p>
                                                </td>
                                                <td>
                                                    <asp:ListBox ID="SelectedAssociationsListBox" runat="server" SelectionMode="Multiple" CssClass="bigger-listbox"></asp:ListBox></td>
                                            </tr>

                                        </tbody>
                                    </table>
                                </p>
                            </div>
                        </td>
                    </tr>
                </tbody>
            </table>


        </asp:View>
        <asp:View runat="server">


            <%--<a id="Manage-comm-btn" class="toogle-tr">Manage community permissions</a>--%>
            <table class="content-table" id="Manage-comm-perm">
                <tbody>
                    <tr>
                        <td colspan="3">
                            <hr />
                            <br />
                            <p align="center">
                                <asp:Label ID="ActionStatusPermissions2" runat="server" CssClass="Important"></asp:Label>
                            </p>
                        </td>
                    </tr>

                    <tr>
                        <td>
                            <div class="content-block">
                                <h3>Manage Users By Community</h3>
                                <p>
                                    <b>Select a Community:</b>

                                    <asp:DropDownList ID="CommunityList" runat="server" AutoPostBack="true" OnSelectedIndexChanged="CommunityList_OnSelectedIndexChanged"></asp:DropDownList>
                                </p>
                                <p>
                                    <asp:GridView ID="CommunityUserList" runat="server" AutoGenerateColumns="False" EmptyDataText="No users has permission to this community." OnRowDeleting="CommunityUserList_OnRowDeleting">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Perm.Id">
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
                                </p>
                                <br />
                                <p>
                                    <b>Select a User:</b>
                                    <asp:DropDownList ID="UserToAddList2" runat="server" DataTextField="UserName" DataValueField="UserName"></asp:DropDownList>
                                    <br />
                                    <b>and a Role:</b>
                                    <asp:DropDownList ID="RoleToAddList2" runat="server"></asp:DropDownList>
                                    <br />
                                    <asp:Button ID="AddUserToCommunity" runat="server" Text="Add user to community" OnClick="AddUserToCommunity_OnClick" />
                                </p>
                            </div>

                        </td>
                        <td>
                            <div class="content-block">
                                <h3>Manage Community Permissions By User</h3>

                                <p>
                                    <b>Select a User:</b>
                                    <asp:DropDownList ID="UserList3" runat="server" AutoPostBack="True"
                                        DataTextField="UserName" DataValueField="UserName" OnSelectedIndexChanged="UserList3_OnSelectedIndexChanged">
                                    </asp:DropDownList>
                                    <b>and a Role:</b>
                                    <asp:DropDownList ID="RoleList3" runat="server" AutoPostBack="True" OnSelectedIndexChanged="RoleList3_OnSelectedIndexChanged">
                                    </asp:DropDownList>
                                </p>
                                <p>
                                    <table>
                                        <thead>
                                            <tr>
                                                <th>Communities</th>
                                                <th></th>
                                                <th>Communities the user has permission to</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <tr>
                                                <td>
                                                    <asp:ListBox ID="CommunitiesListBox" runat="server" SelectionMode="Multiple" CssClass="bigger-listbox"></asp:ListBox>
                                                </td>
                                                <td>
                                                    <p align="center">
                                                        <asp:Button ID="AddCommunity" runat="server" Text="> Add >" OnClick="AddCommunity_OnClick" />
                                                        <asp:Button ID="RemoveCommunity" runat="server" Text="< Remove <" OnClick="RemoveCommunity_OnClick" />
                                                    </p>
                                                </td>
                                                <td>
                                                    <asp:ListBox ID="SelectedCommunitiesListBox" runat="server" SelectionMode="Multiple" CssClass="bigger-listbox"></asp:ListBox></td>
                                            </tr>

                                        </tbody>
                                    </table>
                                </p>
                            </div>
                        </td>
                    </tr>
                </tbody>
            </table>
        </asp:View>


    </asp:MultiView>


</div>
