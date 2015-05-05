<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserHandlingControl.ascx.cs" Inherits="EventHandlingSystem.UserHandlingControl" %>


<style type="text/css">
    .user-list-table
    {
        /*background-color: aliceblue;*/
        margin: 1px auto;
        /*height: 600px;*/
        /*min-width: 900px;*/
        height: 100%;
        width: 100%;
    }

        .user-list-table td
        {
            padding: 3px;
            /* border: 1px solid lightblue; */
            /*vertical-align: top;*/
            max-width: 350px;
            /* background-color: aliceblue; */
            overflow: auto;
            /*word-break: normal;*/
            /*word-wrap: break-word;*/
        }

        .user-list-table input
        {
            width: auto;
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

    .content-table td
    {
        vertical-align: top;
    }



    .important
    {
        font-size: large;
    }
</style>


<script type="text/javascript">

    $(document).ready(function () {
        $('table.user-list-table').closest("div").css("overflow", "auto");
        $('table.user-list-table').closest("div").css("max-height", "500px");
    });


</script>
<h2>All users</h2>
<asp:GridView ID="GridViewUsers" runat="server"
    AutoGenerateColumns="False"
    OnRowCancelingEdit="GridViewUsers_OnRowCancelingEdit"
    OnRowEditing="GridViewUsers_OnRowEditing"
    OnRowDeleting="GridViewUsers_OnRowDeleting"
    OnRowUpdating="GridViewUsers_OnRowUpdating"
    OnRowUpdated="GridViewUsers_OnRowUpdated"
    CellPadding="4"
    ForeColor="#333333"
    GridLines="None"
    CssClass="user-list-table">
    <AlternatingRowStyle BackColor="White" />
    <EditRowStyle BackColor="#2461BF" />
    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
    <RowStyle BackColor="#EFF3FB" />
    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
    <SortedAscendingCellStyle BackColor="#F5F7FB" />
    <SortedAscendingHeaderStyle BackColor="#6D95E1" />
    <SortedDescendingCellStyle BackColor="#E9EBEF" />
    <SortedDescendingHeaderStyle BackColor="#4870BE" />
    <Columns>
        <asp:CheckBoxField DataField="IsOnline" HeaderText="IsOnline" ReadOnly="true" />
        <asp:BoundField DataField="UserName" HeaderText="Username" ReadOnly="true" />
        <%--<asp:BoundField DataField="Email" HeaderText="Email" />--%>
        <asp:TemplateField HeaderText="Email">
                <ItemTemplate>
                    <%# Eval("Email")  %>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtEmail" runat="server"
                        Text='<%#Eval("Email") %>'
                        TextMode="Email" CausesValidation="True" ValidationGroup="UserVali"></asp:TextBox>
                    <br/>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidatorForEmail" runat="server" ErrorMessage="Enter a valid email!"
                            ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ControlToValidate="txtEmail"  Display="Dynamic" ValidationGroup="UserVali"/>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorForEmail" runat="server" ErrorMessage="Required"
                            ControlToValidate="txtEmail" Display="Dynamic" ValidationGroup="UserVali" />
                </EditItemTemplate>
            </asp:TemplateField>
        <asp:BoundField DataField="Comment" HeaderText="Comment" />
        <asp:CheckBoxField DataField="IsApproved" HeaderText="IsApproved" />
        <%--<asp:BoundField DataField="PasswordQuestion" HeaderText="PasswordQuestion" ReadOnly="True"/>
        <asp:CheckBoxField DataField="IsLockedOut" HeaderText="IsLockedOut" ReadOnly="true" />
        <asp:BoundField DataField="LastLockoutDate" HeaderText="LastLockoutDate" ReadOnly="true" />
        <asp:BoundField DataField="CreationDate" HeaderText="CreationDate" ReadOnly="true" />
        <asp:BoundField DataField="LastLoginDate" HeaderText="LastLoginDate" ReadOnly="true" />
        <asp:BoundField DataField="LastActivityDate" HeaderText="LastActivityDate" ReadOnly="true" />
        <asp:BoundField DataField="LastPasswordChangedDate" HeaderText="LastPasswordChangedDate" ReadOnly="true" />--%>
        <asp:TemplateField>
            <ItemTemplate>
                <asp:LinkButton ID="LinkButtonEditEvent" ClientIDMode="Static" runat="server" CausesValidation="False" CommandName="Edit" Text="Edit" CssClass="edit-link"></asp:LinkButton>
                <asp:LinkButton ID="LinkButtonUpdateEvent" ClientIDMode="Static" runat="server" CausesValidation="True" CommandName="Update" Text="Update" Visible="False" CssClass="edit-mode-link" ValidationGroup="UserVali"/>
                <asp:LinkButton ID="LinkButtonCancelEdit" ClientIDMode="Static" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel" Visible="False" CssClass="edit-mode-link" />
                <asp:LinkButton ID="LinkButtonDeleteEvent" ClientIDMode="Static" runat="server" CausesValidation="False" CommandName="Delete" Text="Delete" OnClientClick="return confirm('Are you sure you want to delete this user?');" CssClass="delete-mode-link"></asp:LinkButton>
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>

<p align="center">
    <asp:Label ID="LabelDisplay" runat="server" Text="" CssClass="important"></asp:Label>
</p>
<table class="content-table">
    <thead>
        <tr>
            <th>Create user</th>
            <th>Change password</th>
            <th>Reset password</th>
        </tr>
    </thead>
    <tbody>
        <tr>
            <td>
                <asp:Panel ID="PanelCreateUser" runat="server" DefaultButton="btnCreateUser">
                    <div>
                        <b>Username</b>
                        <br />
                        <asp:TextBox ID="txtUserName" runat="server" Width="140" TextMode="SingleLine" ValidationGroup="CreateUser" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValiUserName" runat="server" ControlToValidate="txtUserName" ErrorMessage="Required Field" ValidationGroup="CreateUser" Display="Dynamic" SetFocusOnError="True"></asp:RequiredFieldValidator>
                    </div>
                    <div>
                        <b>Email</b>
                        <br />
                        <asp:TextBox ID="txtEmail" runat="server" Width="140" TextMode="Email" ValidationGroup="CreateUser" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtEmail" ErrorMessage="Required Field" ValidationGroup="CreateUser" Display="Dynamic" SetFocusOnError="True"></asp:RequiredFieldValidator>
                    </div>
                    <div>
                        <b>Password</b>
                        <br />
                        <asp:TextBox ID="txtPassword" runat="server" Width="140" TextMode="Password" ValidationGroup="CreateUser" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValiPass" runat="server" ControlToValidate="txtPassword" ErrorMessage="Required Field" ValidationGroup="CreateUser" Display="Dynamic" SetFocusOnError="True"></asp:RequiredFieldValidator>
                    </div>
                    <div>
                        <b>Confirm Password</b>
                        <br />
                        <asp:TextBox ID="txtConfirmPassword" runat="server" Width="140" TextMode="Password" ValidationGroup="CreateUser" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValiPassConfirm" runat="server" ControlToValidate="txtConfirmPassword" ErrorMessage="Required Field" ValidationGroup="CreateUser" Display="Dynamic" SetFocusOnError="True"></asp:RequiredFieldValidator>
                    </div>
                    <asp:Button ID="btnCreateUser" runat="server" Text="Create" OnClick="btnCreateUser_OnClick" ValidationGroup="CreateUser" />
                </asp:Panel>
            </td>
            <td>
                <asp:Panel ID="PanelChangePassword" runat="server" DefaultButton="BtnChangePassword">
                    <div>
                        <b>Username</b>
                        <br />
                        <asp:TextBox ID="TxtUserNameChangePassword" runat="server" Width="140" ValidationGroup="Change" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValiUserNameChange" runat="server" ControlToValidate="TxtUserNameChangePassword" ErrorMessage="Required Field" ValidationGroup="Change" Display="Dynamic" SetFocusOnError="True"></asp:RequiredFieldValidator>
                    </div>
                    <div>
                        <b>New password</b>
                        <br />
                        <asp:TextBox ID="TxtNewPassword" runat="server" Width="140" TextMode="Password" ValidationGroup="Change" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValiPassChange" runat="server" ControlToValidate="TxtNewPassword" ErrorMessage="Required Field" ValidationGroup="Change" Display="Dynamic" SetFocusOnError="True"></asp:RequiredFieldValidator>
                    </div>
                    <div>
                        <b>Confirm new password</b>
                        <br />
                        <asp:TextBox ID="TxtNewPasswordConfirm" runat="server" Width="140" TextMode="Password" ValidationGroup="Change" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValiPassChangeConfirm" runat="server" ControlToValidate="TxtNewPasswordConfirm" ErrorMessage="Required Field" ValidationGroup="Change" Display="Dynamic" SetFocusOnError="True"></asp:RequiredFieldValidator>
                    </div>
                    <asp:Button ID="BtnChangePassword" runat="server" Text="Change" OnClick="BtnChangePassword_OnClick" ValidationGroup="Change" />
                </asp:Panel>
            </td>
            <td>
                <asp:Panel ID="PanelResetPassword" runat="server" DefaultButton="BtnReset">
                    <div>
                        <b>Email</b>
                        <br />
                        <asp:TextBox ID="TxtEmailReset" runat="server" Width="140" TextMode="Email" ValidationGroup="Reset" />
                        <asp:RequiredFieldValidator ID="ReqFieldValiEmailReset" runat="server" ControlToValidate="TxtEmailReset" ErrorMessage="Required Field" ValidationGroup="Reset" Display="Dynamic" SetFocusOnError="True"></asp:RequiredFieldValidator>
                    </div>
                    <asp:Button ID="BtnReset" runat="server" Text="Reset" OnClick="BtnReset_OnClick" ValidationGroup="Reset" />
                </asp:Panel>
            </td>
        </tr>
    </tbody>
</table>

<%--<asp:Button ID="ButtonTest" runat="server" Text="Click" OnClientClick="if(!confirm('Are you sure you want to submit?')) return false;" />

<asp:Button ID="BtnEdit" runat="server" Text="Edit" OnClick="BtnEdit_OnClick" />--%>

<br />
<br />



