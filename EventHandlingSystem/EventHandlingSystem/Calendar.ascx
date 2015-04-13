<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Calendar.ascx.cs" Inherits="EventHandlingSystem.Calendar" %>

<%--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">--%>

<link type="text/css" href="Content/themes/base/all.css" rel="stylesheet"/>
<link type="text/css" href="Content/themes/base/resizable.css" rel="stylesheet"/>

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
        /*background-color: aliceblue;*/
        margin:1px auto;
        /*height: 600px;*/
        /*min-width: 900px;*/
        height: 100%;
        width: 100%;
        /*I HATE TABLE CELLS!!!!! (vvv This is very important for the table cells to be the same witdh and make them into good cells! :3) */
        table-layout: fixed;
    }

    html > body .table-cell {
        background-color: white;
        -moz-min-width: 50px;
        -ms-min-width: 50px;
        -o-min-width: 50px;
        -webkit-min-width: 50px;
        min-width: 50px;
        /*max-width: 160px;*/
        min-height: 50px;
        height: 100%;
        /*padding: 0 5px;*/
        position: relative;
        width: 100%;
    }

    html > body .table-small-cell
    {
        /*background-color: white;*/
        /*min-width: 30px;
        min-height: 90px;*/
        height: 100%;
        /*padding: 0 5px;*/
        /*position: relative;*/
        height: auto;
        width: auto;
    }



    html > body .cal-table td
    {
        padding: 0;
        border: 1px solid lightblue;
        vertical-align: top;
        max-width: 150px;
        /*I HATE TABLE CELLS!!!!! (vvv This witdh is very important!) */
        width: 1000px;
        background-color: aliceblue;
    }

    html > body .cal-table th
    {
        padding-left: 5px;
        /*border: 1px solid lightblue;*/
        text-align: center;
        font-size: 16px;
        /*background-color: aliceblue*/
    }

    html > body th .cell-week-th 
    {
        width: 50px;
    }

    html > body .cell-week
    {
        /*padding: 5px;*/
        text-align: center;
        font-weight: bold;
        /*position: relative;
        top: 45%;*/
        font-style: italic;
        font-size: 14px;
        width: 30px;
        display:block;
    /*height:50%;
    width: 50%;*/
    /*margin: auto;*/
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
        margin-top: 1px;
        /*margin: 1px;*/
        /*padding: 2px;*/
        /*-ms-word-break: break-all;
        -moz-word-break: break-all;
        -o-word-break: break-all;
        word-break: break-all;*/
        line-height: 20px;
        white-space: nowrap;
        overflow: hidden !important;
        -moz-text-overflow: ellipsis;
        -ms-text-overflow: ellipsis;
        -o-text-overflow: ellipsis;
        text-overflow: ellipsis;
        /*height: 20px;*/
        font-size: 14px;
    }

    .one-day {
        color: royalblue !important;
        background-color: transparent !important;
    }

    .multiple-days {
        
    }

    .event-hover {
        background-color: rgb(230, 230, 230) !important;
    }

    

    html > body .event-pop-up {
        display: none;
        z-index: 9999;
        position: absolute;
        width: 240px;

        
    }

    .arrow-up {
        margin-left: 10px; 
	width: 0; 
	height: 0; 
	border-left: 10px solid transparent;
	border-right: 10px solid transparent;
	border-bottom: 10px solid lightpink;
}

    html > body .pop-up-text {
        background-color: lightpink;
        padding: 20px;
        -moz-border-radius: 7px;
        -webkit-border-radius: 7px;
        -ms-border-radius: 7px;
        border-radius: 7px;

       

    }

    html > body .pop-up-text p
    {
        margin: 0 auto;
 white-space: nowrap;
        overflow: hidden !important;
        -moz-text-overflow: ellipsis;
        -ms-text-overflow: ellipsis;
        -o-text-overflow: ellipsis;
        text-overflow: ellipsis;

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
            location.href = 'Contributors/EventCreate.aspx?d=' + $(this).data('date');
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

        $('table.cal-table').closest("div").css("height", $('table.cal-table').closest("div").height());
        //$('table.cal-table').closest("div").css("display", "inline");

        //$('table.cal-table').closest("div").css("overflow", "hidden");

        $('table.cal-table tbody th').first().css("width", "30px");

        $('div.table-small-cell').closest("td").css("vertical-align", "middle");
        $('div.table-small-cell').closest("td").css("border", "0");
        $('div.table-small-cell').closest("td").css("background-color", "transparent");
        $('div.table-small-cell').closest("td").css("width", "30px");
        //$('.event-in-cell').width("200px");
        

        $(".resizable").closest("div").resizable();

        $(".event-in-cell").hover(function () {
            $("#" + $(this).attr("id") + ".event-pop-up:first").stop(true, true).fadeIn(200);
            //alert("over...");

            //$("#" + $(this).attr("id")).toggleClass("event-hover");
            $("[id=\"" + $(this).attr("id") + "\"]" + "[class~=\"event-in-cell\"]").toggleClass("event-hover");
            //alert("[id=\"" + $(this).attr("id") + "\"]");

        }, function () {
            $("#" + $(this).attr("id") + ".event-pop-up:first").stop(true, true).fadeOut(100);
            //alert("...and out");

            $("[id=\"" + $(this).attr("id") + "\"]" + "[class~=\"event-in-cell\"]").toggleClass("event-hover");

        });

    });
</script>
<%--<div class="resizable" style="width: 300px; height: 250px; overflow: hidden; border:1px solid black;">--%>
<div style="display: block; padding: 1px;">
    <h1 style="display: inline; vertical-align: middle;">Calendar</h1>
    <input type="button" id="Toggle-calendar-btn" class="toggle-btn" value="-" />
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
</div>
