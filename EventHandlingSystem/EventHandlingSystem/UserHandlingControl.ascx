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

   

</style>


<script type="text/javascript">

    $(document).ready(function() {
        $('table.user-list-table').closest("div").css("overflow", "auto");
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
        <asp:BoundField DataField="UserName" HeaderText="UserName" ReadOnly="true" />
        <asp:BoundField DataField="Email" HeaderText="Email" />
        <asp:BoundField DataField="Comment" HeaderText="Comment" />
        <asp:CheckBoxField DataField="IsApproved" HeaderText="IsApproved" />
        <%--<asp:BoundField DataField="PasswordQuestion" HeaderText="PasswordQuestion" ReadOnly="True"/>
        <asp:CheckBoxField DataField="IsLockedOut" HeaderText="IsLockedOut" ReadOnly="true" />
        <asp:BoundField DataField="LastLockoutDate" HeaderText="LastLockoutDate" ReadOnly="true" />
        <asp:BoundField DataField="CreationDate" HeaderText="CreationDate" ReadOnly="true" />
        <asp:BoundField DataField="LastLoginDate" HeaderText="LastLoginDate" ReadOnly="true" />
        <asp:BoundField DataField="LastActivityDate" HeaderText="LastActivityDate" ReadOnly="true" />
        <asp:BoundField DataField="LastPasswordChangedDate" HeaderText="LastPasswordChangedDate" ReadOnly="true" />--%>
        <asp:CommandField ButtonType="Link" ShowEditButton="true" ShowDeleteButton="true" />
    </Columns>
</asp:GridView>
<br />
<asp:Label ID="LabelDisplay" runat="server" Text=""></asp:Label>
<br />
<h2>Create user</h2>
<table style="border-collapse: collapse">
    <thead>
        <tr>
            <th style="width: 150px">UserName:
            </th>
            <th style="width: 150px">Email:
            </th>
            <th style="width: 150px">Password:
            </th>
            <th style="width: 150px">Confirm Password:
            </th>
            <th style="width: 100px"></th>
        </tr>
    </thead>
    <tbody>
        <tr>
            <td style="width: 150px">
                <asp:TextBox ID="txtUserName" runat="server" Width="140" TextMode="SingleLine" ValidationGroup="CreateUser" />
                <asp:RequiredFieldValidator ID="RequiredFieldValiUserName" runat="server" ControlToValidate="txtUserName" ErrorMessage="< Required Field" ValidationGroup="CreateUser" Display="Dynamic" SetFocusOnError="True"></asp:RequiredFieldValidator>

            </td>
            <td style="width: 150px">
                <asp:TextBox ID="txtEmail" runat="server" Width="140" TextMode="Email" ValidationGroup="CreateUser" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtEmail" ErrorMessage="< Required Field" ValidationGroup="CreateUser" Display="Dynamic" SetFocusOnError="True"></asp:RequiredFieldValidator>
            </td>
            <td style="width: 150px">
                <asp:TextBox ID="txtPassword" runat="server" Width="140" TextMode="Password" ValidationGroup="CreateUser" />
                <asp:RequiredFieldValidator ID="RequiredFieldValiPass" runat="server" ControlToValidate="txtPassword" ErrorMessage="< Required Field" ValidationGroup="CreateUser" Display="Dynamic" SetFocusOnError="True"></asp:RequiredFieldValidator>
            </td>
            <td style="width: 150px">
                <asp:TextBox ID="txtConfirmPassword" runat="server" Width="140" TextMode="Password" ValidationGroup="CreateUser" />
                <asp:RequiredFieldValidator ID="RequiredFieldValiPassConfirm" runat="server" ControlToValidate="txtConfirmPassword" ErrorMessage="< Required Field" ValidationGroup="CreateUser" Display="Dynamic" SetFocusOnError="True"></asp:RequiredFieldValidator>
            </td>
            <td style="width: 100px">
                <asp:Button ID="btnCreateUser" runat="server" Text="Create" OnClick="btnCreateUser_OnClick" ValidationGroup="CreateUser" />
            </td>
        </tr>
    </tbody>
</table>


<h2>Reset password</h2>
<table style="border-collapse: collapse">
    <thead>
        <tr>
            <th style="width: 150px">Email:
            </th>
            <th style="width: 100px"></th>
        </tr>
    </thead>
    <tbody>
        <tr>
            <td style="width: 150px">
                <asp:TextBox ID="TxtEmailReset" runat="server" Width="140" TextMode="Email" ValidationGroup="Reset" />
                <asp:RequiredFieldValidator ID="ReqFieldValiEmailReset" runat="server" ControlToValidate="TxtEmailReset" ErrorMessage="< Required Field" ValidationGroup="Reset" Display="Dynamic" SetFocusOnError="True"></asp:RequiredFieldValidator>

            </td>
            <td style="width: 100px">
                <asp:Button ID="BtnReset" runat="server" Text="Reset" OnClick="BtnReset_OnClick" ValidationGroup="Reset" />
            </td>
        </tr>
    </tbody>
</table>


<%--<asp:Button ID="ButtonTest" runat="server" Text="Click" OnClientClick="if(!confirm('Are you sure you want to submit?')) return false;" />

<asp:Button ID="BtnEdit" runat="server" Text="Edit" OnClick="BtnEdit_OnClick" />--%>

<br />
<br />



