<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserHandlingControl.ascx.cs" Inherits="EventHandlingSystem.UserHandlingControl" %>
<%--<script type="text/javascript">
    function Clicked() {
        if (confirm('Do you wanna to submit?')) {
            submit();
        } else {
            return false;
        }
    }
    </script>--%>
<asp:GridView ID="GridViewUsers" runat="server" AutoGenerateSelectButton="True" AutoGenerateEditButton="True" OnRowCancelingEdit="GridViewUsers_OnRowCancelingEdit" OnRowEditing="GridViewUsers_OnRowEditing" CellPadding="4" ForeColor="#333333" GridLines="None">
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
</asp:GridView>


<asp:Button ID="ButtonTest" runat="server" Text="Click" OnClientClick="if(!confirm('Are you sure you want to submit?')) return false;" />

<asp:Button ID="BtnEdit" runat="server" Text="Edit" OnClick="BtnEdit_OnClick"/>

<asp:Label ID="LabelTest" runat="server" Text=""></asp:Label>
