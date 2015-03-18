<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RolesPermissionsControl.ascx.cs" Inherits="EventHandlingSystem.RolesPermissionsControl" %>

<style type="text/css">
    .content-block
    {
        vertical-align: top;
        display: inline;
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
    }
</style>
<br />
<div class="inner-content-box">
    <div class="content-block">
        <p align="center">
            <asp:Label ID="ActionStatus" runat="server" CssClass="Important"></asp:Label>
        </p>
        <h3>Manage Roles By User</h3>

        <p>
            <b>Select a User:</b>
            <asp:DropDownList ID="UserList" runat="server" AutoPostBack="True"
                DataTextField="UserName" DataValueField="UserName" OnSelectedIndexChanged="UserList_SelectedIndexChanged">
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
    <br/>
    <br/>
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
                            <asp:Label runat="server" ID="UserNameLabel"
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
     <asp:Button ID="AddUserToRoleButton" runat="server" Text="Add User to Role" OnClick="AddUserToRoleButton_Click"/> 

</p>

    </div>
</div>
<br />





