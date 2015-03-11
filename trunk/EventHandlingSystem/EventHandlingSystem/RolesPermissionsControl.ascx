<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RolesPermissionsControl.ascx.cs" Inherits="EventHandlingSystem.RolesPermissionsControl" %>

<style type="text/css">
        .content-block {
            float: left;
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
    </style>
<br/>
<div class="inner-content-box">
<div class="content-block">
    <h6>Roles</h6>
    <asp:DropDownList ID="DropDownListRoles" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownListRoles_OnSelectedIndexChanged"></asp:DropDownList>
</div>
<div class="content-block">
    <h6>Users in Role</h6>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:BulletedList ID="BulletedListUsersInRoles" runat="server"></asp:BulletedList>
            <br/>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="DropDownListRoles" EventName="SelectedIndexChanged" />
        </Triggers>
    </asp:UpdatePanel>
</div>
    </div>
<br/>





