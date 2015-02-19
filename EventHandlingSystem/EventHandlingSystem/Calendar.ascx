<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Calendar.ascx.cs" Inherits="EventHandlingSystem.Calendar" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<style type="text/css">
    html > body .toggle-btn
    {
        background-color: white;
        border: 1px solid gainsboro;
        height: auto;
        width: auto;
        padding: 0px 7px;
        margin: 0px 20px;
        vertical-align: middle;
        font-size: 14px;
    }

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
        max-width: 160px;
        min-height: 90px;
        height: 100%;
        /*padding: 0 5px;*/
        /*position: relative;*/
    }

    html > body .table-small-cell
    {
        /*background-color: white;*/
        min-width: 30px;
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
        text-align: center;
        /*background-color: aliceblue*/
    }

    html > body .cell-week
    {
        /*padding: 5px;*/
        text-align: center;
        font-weight: bold;
        position: relative;
        top: 45%;
        font-style: italic;
        font-size: 14px;
    }

    html > body .cell-date
    {
        padding: 5px;
        font-size: 17px;
    }

    html > body .cell-today
    {
        font-weight: bold;
    }

    html > body .event-in-cell
    {
        background-color: plum;
        text-align: center;
        margin: 1px;
        padding: 2px;
        -ms-word-wrap: break-word;
        word-wrap: break-word;
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


<%--<script type="text/javascript" src="/scripts/jquery-2.1.3.js"></script>--%>

<script type="text/javascript">
    $(document).ready(function () {
        $('div.table-cell').click(function (e) {
            if ($(e.target).hasClass('event-in-cell')) return;
            location.href = 'EventCreate.aspx?d=' + $(this).data('date');
        });
    });
</script>

<script type="text/javascript">
    $(document).ready(function () {
        $('#Toggle-calendar-btn').click(function () {
            $('#Calendar').toggle("fast", function () {
                if ($('#Toggle-calendar-btn').attr("value") === "-") {
                    $('#Toggle-calendar-btn').attr("value", "+");
                } else {
                    $('#Toggle-calendar-btn').attr("value", "-");
                }
            });
        });
    });
</script>

<br />
<br />
<h1 style="display: inline; vertical-align: middle;">Calendar</h1><input type="button" id="Toggle-calendar-btn" class="toggle-btn" value="-" />
<br />
<div id="Calendar">
    <asp:HiddenField ID="hdnDate" runat="server"></asp:HiddenField>
    <asp:Button ID="btnBackArrow" runat="server" Text=" < " OnClick="btnBackArrow_OnClick" />
    <asp:Button ID="btnForwardArrow" runat="server" Text=" > " OnClick="btnForwardArrow_OnClick" />
    <asp:Label ID="lblCurrentDate" runat="server" Text=""></asp:Label>
    <br />

    <asp:GridView ID="GridViewCalendar" CssClass="cal-table" AutoGenerateColumns="False" runat="server">
        <Columns>
            <asp:BoundField DataField="Week" HtmlEncode="False" HeaderText="" />
            <asp:BoundField DataField="Mon" HtmlEncode="False" HeaderText="Mon" />
            <asp:BoundField DataField="Tue" HtmlEncode="False" HeaderText="Tue" />
            <asp:BoundField DataField="Wed" HtmlEncode="False" HeaderText="Wed" />
            <asp:BoundField DataField="Thu" HtmlEncode="False" HeaderText="Thu" />
            <asp:BoundField DataField="Fri" HtmlEncode="False" HeaderText="Fri" />
            <asp:BoundField DataField="Sat" HtmlEncode="False" HeaderText="Sat" />
            <asp:BoundField DataField="Sun" HtmlEncode="False" HeaderText="Sun" />
        </Columns>
    </asp:GridView>
</div>
