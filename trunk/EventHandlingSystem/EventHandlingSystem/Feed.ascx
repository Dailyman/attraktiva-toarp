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
        margin: 5px;
        padding: 20px;
        border: 2px solid blue;
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
        font-size: 16px;
        font-family: Verdana,Geneva,sans-serif;
        color: blue;
        cursor: pointer;
    }

    .feedbox-summary
    {
        font-size: 12px;
        font-family: "Palatino Linotype", "Book Antiqua", Palatino, serif;
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
    <asp:Repeater ID="RepeaterFeed" runat="server">
        <ItemTemplate>
            <table>
                <tr>
                    <td>
                        <asp:Label runat="server" ID="Label1"
                            Text='<%# Eval("Title") %>' />
                    </td>
                    <td>
                        <asp:Label runat="server" ID="Label2"
                            Text='<%# Eval("Summary") %>' />
                    </td>
                </tr>
            </table>
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

