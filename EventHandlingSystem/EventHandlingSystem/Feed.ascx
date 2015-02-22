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
        margin: 15px;
        padding: 20px;
        border: 2px solid blue;
        max-width: 900px;
        width: auto;
    }

    .feedbox-eventdate
    {
        background-color: black;
        color: white;
        border: 1px dotted white;
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
        height: 260px;
        width: 300px;
        /*max-height: 250px;
        max-width: 300px;*/
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
<div id="Feed">
    <asp:Repeater ID="RepeaterFeed" runat="server" >
        <ItemTemplate>
            <div id="feedtable" class="feedbox" runat="server">
                <table>
                    <tr>
                        <td rowspan="5">
                            <asp:HyperLink runat="server" NavigateUrl='<%# "/EventDetails?id=" + Eval("Id") %>' Target="_blank">
                                <asp:Image runat="server" ID="Image1" CssClass="feedbox-image"
                                ImageUrl='<%# Eval("ImageUrl") %>' />
                            </asp:HyperLink>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label runat="server" ID="lbStartDate" CssClass="feedbox-eventdate"
                                Text='<%# Convert.ToDateTime(Eval("StartDate")).ToString("dd MMMM") %>' />
                            <br />
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td class='<%# ((bool)Eval("DayEvent") ? "hide" : "")  %>' >
                            <asp:Label runat="server" ID="lbTime" 
                                       Text='<%# Convert.ToDateTime(Eval("StartDate")).ToShortTimeString() + " - " 
                                                 + Convert.ToDateTime(Eval("EndDate")).ToShortTimeString() %>' 
                                     Visible='<%# !(bool)Eval("DayEvent") %>'
                                 /> 
                            <br />
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:HyperLink runat="server" ID="lbTitle" CssClass="feedbox-title" Target="_blank"
                                Text='<%# Eval("Title") %>'
                                NavigateUrl='<%# "/EventDetails?id=" + Eval("Id") %>' />
                            <br />
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label runat="server" ID="lbSummary"
                                Text='<%# Eval("Summary") %>' />
                            <br />
                            <br />
                        </td>
                    </tr>
                    <%--<tr>
                        <td>
                            <asp:Image runat="server" ID="imgEventImage" CssClass="feedbox-image"
                                ImageUrl='<%# Eval("ImageUrl") %>' />
                        </td>
                    </tr>--%>
                </table>
            </div>
        </ItemTemplate>
    </asp:Repeater>

    <%--<asp:LinkButton ID="lnkbtnEventDate" runat="server" CssClass="feedbox-eventdate"></asp:LinkButton>
        <br />
        <br />
        <asp:HyperLink ID="hlnkEventTitle" runat="server" CssClass="feedbox-title"></asp:HyperLink>
        <br />
        <br />
        <asp:Label ID="lbEventSummary" runat="server" CssClass="feedbox-summary" Text=""></asp:Label>
        <br />
        <br />
        <asp:Image ID="imgEventImage" runat="server" />--%>
</div>

