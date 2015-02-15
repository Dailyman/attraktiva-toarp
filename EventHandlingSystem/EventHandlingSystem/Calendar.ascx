<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Calendar.ascx.cs" Inherits="EventHandlingSystem.Calendar" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<style>
    .cal-table {
        background-color: aliceblue;
        padding: 5px;
        border: 1px solid black;
    }
     .table-event {
         background-color: white;
         width: 127px;
         height: 90px;
         margin: 0 auto;
         padding: 5px;
     }
     .cal-table td {
        padding: 0;
        border: 1px solid #3498db;
    }
     .cal-table th {
        padding-left: 5px;
        border: 1px solid #3498db;
    }
</style>

<br />
<br />

<br />
<asp:HiddenField ID="hdnDate" runat="server"></asp:HiddenField>
<asp:Button ID="btnBackArrow" runat="server" Text=" < " OnClick="btnBackArrow_OnClick"/>
<asp:Button ID="btnForwardArrow" runat="server" Text=" > " OnClick="btnForwardArrow_OnClick" />
<asp:Label ID="lblCurrentDate" runat="server" Text=""></asp:Label>
<br />
<asp:GridView ID="GridView1" CssClass="cal-table" AutoGenerateColumns="False" runat="server">
    <Columns>
        <asp:BoundField DataField="Mon" HtmlEncode="False" HeaderText="Mon" />
        <asp:BoundField DataField="Tue" HtmlEncode="False" HeaderText="Tue" />
        <asp:BoundField DataField="Wed" HtmlEncode="False" HeaderText="Wed" />
        <asp:BoundField DataField="Thu" HtmlEncode="False" HeaderText="Thu" />
        <asp:BoundField DataField="Fri" HtmlEncode="False" HeaderText="Fri" />
        <asp:BoundField DataField="Sat" HtmlEncode="False" HeaderText="Sat" />
        <asp:BoundField DataField="Sun" HtmlEncode="False" HeaderText="Sun" />
    </Columns>
</asp:GridView>

