<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Feed.ascx.cs" Inherits="EventHandlingSystem.Feed" %>

<style>
    .toggle-btn
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

    .feedbox
    {
        background-color: aliceblue;
        margin: 5px 0 5px 0; 
        padding: 10px;
        border: 2px solid blue;
        max-width: 900px;
        width: auto;
    }

    .feedbox-eventdate
    {
        background-color: black;
        -ms-border-radius: 5px;
        -moz-border-radius: 5px;
        -webkit-border-radius: 5px;
        border-radius: 5px;
        color: white;
        padding: 10px;
        text-decoration: wavy;
        font-size: 14px;
    }

    .feedbox-title
    {
        text-decoration: none;
        font-weight: bold;
        font-size: 20px;
        font-family: Verdana,Geneva,sans-serif;
        color: blue;
        cursor: pointer;
    }

    .feedbox-summary
    {
        font-size: 12px;
        font-family: "Palatino Linotype", "Book Antiqua", Palatino, serif;
    }

    .feedbox-image {
        /*height: auto;
        width: auto;*/
        height: auto;
        width: auto;
        max-height: 160px;
        max-width: 200px;
    }
    .image-box {
        text-align: center;
        width: 200px;
        height: 160px;
    }
    
    .image-td {
        vertical-align: top;
    }
    
    .hide {
        display: none;
    }
</style>

<script type="text/javascript">
    $(document).ready(function () {
        $('#Toggle-feed-btn').click(function () {
            $('#Feed').toggle("fast", function () {
                if ($('#Toggle-feed-btn').attr("value") === "-") {
                    $('#Toggle-feed-btn').attr("value", "+");
                } else {
                    $('#Toggle-feed-btn').attr("value", "-");
                }
            });
        });
    });
</script>

<br />
<h1 style="display: inline; vertical-align: middle;">Feed</h1>
<input type="button" id="Toggle-feed-btn" class="toggle-btn" value="-" />
<br />

<asp:HiddenField ID="hdfFeedLimit" runat="server" />
<div id="Feed">
    <asp:Repeater ID="RepeaterFeed" runat="server" >
        <ItemTemplate>
            <div id="feedtable" class="feedbox" runat="server">
                <table>
                    <tr>
                        <td rowspan="5" class="image-td">
                            <asp:HyperLink runat="server" NavigateUrl='<%# "/EventDetails?id=" + Eval("Id") %>' Target="_blank">
                                <div class="image-box"><asp:Image runat="server" ID="Image1" CssClass="feedbox-image"
                                ImageUrl='<%# Eval("ImageUrl") %>' /></div>
                            </asp:HyperLink>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label runat="server" ID="lbStartDate" CssClass="feedbox-eventdate"
                                Text='<%# Convert.ToDateTime(Eval("StartDate")).ToString("dd MMMM") %>' />
                            <br/>
                        </td>
                    </tr>
                    <tr>
                        <td class='<%# ((bool)Eval("DayEvent") ? "hide" : "")  %>' >
                            <asp:Label runat="server" ID="lbTime" 
                                       Text='<%# Convert.ToDateTime(Eval("StartDate")).ToShortTimeString() + " - " 
                                                 + Convert.ToDateTime(Eval("EndDate")).ToShortTimeString() %>' 
                                     Visible='<%# !(bool)Eval("DayEvent") %>' /> 
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:HyperLink runat="server" ID="lbTitle" CssClass="feedbox-title" Target="_blank"
                                Text='<%# Eval("Title") %>'
                                NavigateUrl='<%# "/EventDetails?id=" + Eval("Id") %>' />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label runat="server" ID="lbSummary"
                                Text='<%# Eval("Summary") %>' />
                        </td>
                    </tr>
                </table>
            </div>
        </ItemTemplate>
    </asp:Repeater>

    <asp:Button ID="btnShowMore" runat="server" Text="Show more" OnClick="btnShowMore_OnClick"/>
    
</div>

