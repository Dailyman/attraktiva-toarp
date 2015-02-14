<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Calendar.ascx.cs" Inherits="EventHandlingSystem.Calendar" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<style>
    .table-event {
        background-color: #2DB075;
        width: 162px;
        height: 100px;
        margin: 0 auto;
    }
    td {
        padding: 1px;
    }
</style>

<asp:TextBox ID="txtMonth" runat="server">Feb</asp:TextBox>
<asp:TextBox ID="txtYear" runat="server">2015</asp:TextBox>
<asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Button" />
<br />
<br />
<asp:GridView ID="GridView1" AutoGenerateColumns="False" runat="server">
    <Columns>
        <asp:BoundField DataField="Mon" HtmlEncode="False" HeaderText="Mon" />
        <asp:BoundField DataField="Tue" HtmlEncode="False" HeaderText="Tue" />
        <asp:BoundField DataField="Wed" HtmlEncode="False" HeaderText="Wed"/>
        <asp:BoundField DataField="Thu" HtmlEncode="False" HeaderText="Thu" />
        <asp:BoundField DataField="Fri" HtmlEncode="False" HeaderText="Fri"/>
        <asp:BoundField DataField="Sat" HtmlEncode="False" HeaderText="Sat"/>
        <asp:BoundField DataField="Sun" HtmlEncode="False" HeaderText="Sun"/>
    </Columns>
</asp:GridView>

