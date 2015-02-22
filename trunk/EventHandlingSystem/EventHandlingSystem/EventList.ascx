<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EventList.ascx.cs" Inherits="EventHandlingSystem.EventList" %>
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

    .input-date {
        width: 150px;
        margin-right: 10px;
    }

    /*.feedbox
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
    }*/
</style>
<script type="text/javascript">
    $(document).ready(function () {
        $('#Toggle-list-btn').click(function () {
            $('#Event-list').toggle("fast", function () {
                if ($('#Toggle-list-btn').attr("value") === "-") {
                    $('#Toggle-list-btn').attr("value", "+");
                } else {
                    $('#Toggle-list-btn').attr("value", "-");
                }
            });
        });
    });
</script>
<br />
<h1 style="display: inline; vertical-align: middle;">Event List</h1>
<input type="button" id="Toggle-list-btn" class="toggle-btn" value="-" />
<br />
<div id="Event-list">
    <table>
        <tr>
            <th>Start</th>
            <th>End</th>
        </tr>
        <tr>
            <td>
                <asp:TextBox CssClass="input-date" ID="TxtStart" TextMode="Date" runat="server"></asp:TextBox>
            </td>
            <td>
                 <asp:TextBox CssClass="input-date" ID="TxtEnd" TextMode="Date" runat="server"></asp:TextBox>
            </td>
            <td>
                <asp:Button ID="BtnFilter" runat="server" Text="Filter" OnClick="BtnFilter_OnClick" /></td>
        </tr>
    </table>
    <%--<asp:DropDownList ID="DropDownList1" runat="server"></asp:DropDownList>--%>
    <asp:Repeater ID="RepeaterEvents" runat="server">
        <HeaderTemplate>
            <table>
                <tr>
                    <th>Start Date</th>
                    <th>End Date</th>
                    <th>Title</th>
                    <th>Location</th>
                    <th>Open</th>
                </tr>
        </HeaderTemplate>
        <ItemTemplate>
            <tr>
                <td>
                    <asp:Label runat="server" ID="StartDate"
                        Text='<%# DateTime.Parse(Eval("StartDate").ToString()).ToString("yyyy MMM d HH:mm") %>' />
                </td>
                <td>
                    <asp:Label runat="server" ID="Label1"
                        Text='<%# DateTime.Parse(Eval("EndDate").ToString()).ToString("yyyy MMM d HH:mm") %>' />
                </td>
                <td>
                    <asp:Label runat="server" ID="Title"
                        Text='<%# Eval("Title") %>' />
                </td>
                <td>
                    <asp:Label runat="server" ID="Location"
                        Text='<%# Eval("Location") %>' />
                </td>
                <td>
                    <asp:HyperLink ID="HyperLink" NavigateUrl='<%# "/EventDetails?id=" + Eval("Id") %>' runat="server" ImageUrl="http://www.ric.edu/images/icons/icon_new-tab.png"></asp:HyperLink>
                </td>
            </tr>
        </ItemTemplate>
        <AlternatingItemTemplate>
            <tr>
                <td bgcolor="#00FFFF">
                    <asp:Label runat="server" ID="StartDate"
                        Text='<%# DateTime.Parse(Eval("StartDate").ToString()).ToString("yyyy MMM d HH:mm") %>' />
                </td>
                <td bgcolor="#00FFFF">
                    <asp:Label runat="server" ID="Label1"
                        Text='<%# DateTime.Parse(Eval("EndDate").ToString()).ToString("yyyy MMM d HH:mm") %>' />
                </td>
                <td bgcolor="#00FFFF">
                    <asp:Label runat="server" ID="Title"
                        Text='<%# Eval("Title") %>' />
                </td>
                <td bgcolor="#00FFFF">
                    <asp:Label runat="server" ID="Location"
                        Text='<%# Eval("Location") %>' />
                </td>
                <td bgcolor="#00FFFF">
                    <asp:HyperLink ID="HyperLink" NavigateUrl='<%# "/EventDetails?id=" + Eval("Id") %>' runat="server" ImageUrl="http://www.ric.edu/images/icons/icon_new-tab.png"></asp:HyperLink>
                </td>
            </tr>
        </AlternatingItemTemplate>
        <FooterTemplate>
            </table>
        </FooterTemplate>
    </asp:Repeater>
</div>
