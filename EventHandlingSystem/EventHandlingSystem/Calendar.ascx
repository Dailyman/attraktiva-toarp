<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Calendar.ascx.cs" Inherits="EventHandlingSystem.Calendar" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<style>
    html > body .cal-table
    {
        background-color: aliceblue;
        padding: 5px;
        border: 1px solid black;
        height: 600px;
        min-width: 900px;
    }

    html > body .table-cell
    {
        background-color: white;
        min-width: 127px;
        min-height: 90px;
        height: 100%;
        /*padding: 0 5px;*/
        /*position: relative;*/
    }

    html > body .cal-table td
    {
        padding: 0;
        border: 1px solid #3498db;
        vertical-align: top;
    }

    html > body .cal-table th
    {
        padding-left: 5px;
        border: 1px solid #3498db;
        /*background-color: aliceblue*/
    }

    html > body .cell-date {
        padding: 5px;
    }

    html > body .event-in-cell
    {
        background-color: plum;
        text-align: center;
        margin: 1px;
        padding: 2px;
    }

    html > body .table-cell a
    {
        text-decoration: none;
    }

        html > body .table-cell a:hover
        {
            color: grey;
        }
</style>


<script type="text/javascript" src="/scripts/jquery-2.1.3.js"></script>


<br />
<br />

<br />
<asp:HiddenField ID="hdnDate" runat="server"></asp:HiddenField>
<asp:Button ID="btnBackArrow" runat="server" Text=" < " OnClick="btnBackArrow_OnClick" />
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

