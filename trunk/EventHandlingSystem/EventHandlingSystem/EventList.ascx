<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EventList.ascx.cs" Inherits="EventHandlingSystem.EventList" %>
<%@ Import Namespace="EventHandlingSystem" %>
<%@ Import Namespace="Microsoft.Ajax.Utilities" %>
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

    .event-list-table
    {
        /*background-color: aliceblue;*/
        margin: 1px auto;
        /*height: 600px;*/
        /*min-width: 900px;*/
        height: 100%;
        width: 100%;
    }

        .event-list-table td
        {
            padding: 5px;
            /* border: 1px solid lightblue; */
            vertical-align: top;
            max-width: 120px;
            /* background-color: aliceblue; */
            overflow: hidden;
            /*word-break: normal;*/
            /*word-wrap: break-word;*/
        }

    .input-date
    {
        width: 150px;
        margin-right: 10px;
    }

    .search-box
    {
        width: auto;
    }
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

        if ($('[type="date"]').prop('type') != 'date') {
            $('[type="date"]').datepicker({ dateFormat: 'yy-mm-dd' });
        }
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
            <th>Association</th>
            <th>Title Search</th>
            <th></th>
        </tr>
        <tr>
            <td>
                <asp:TextBox CssClass="input-date" ID="TxtStart" TextMode="Date" runat="server"></asp:TextBox>
                <asp:CustomValidator ID="CustomValiStartDate" runat="server" ControlToValidate="TxtStart" ErrorMessage="Use the right format! (e.g. 2005-06-21)" OnServerValidate="CustomValiStartDate_OnServerValidate" ValidationGroup="ValGroupFilter" Display="Dynamic" SetFocusOnError="True"></asp:CustomValidator>
            </td>
            <td>
                <asp:TextBox CssClass="input-date" ID="TxtEnd" TextMode="Date" runat="server"></asp:TextBox>
                <asp:CustomValidator ID="CustomValiEndDate" runat="server" ControlToValidate="TxtEnd" ErrorMessage="Use the right format! (e.g. 2005-06-21)" OnServerValidate="CustomValiEndDate_OnServerValidate" ValidationGroup="ValGroupFilter" Display="Dynamic" SetFocusOnError="True"></asp:CustomValidator>
            </td>
            <td>
                <asp:DropDownList ID="DropDownListAsso" runat="server"></asp:DropDownList>
            </td>
            <td>
                <asp:TextBox ID="TxtSearch" runat="server" TextMode="Search" CssClass="search-box"></asp:TextBox>
            </td>
            <td>
                <asp:Button ID="BtnFilter" runat="server" Text="Filter" OnClick="BtnFilter_OnClick" ValidationGroup="ValGroupFilter"/>
            </td>
        </tr>
    </table>

    <asp:Repeater ID="RepeaterEvents" runat="server">
        <HeaderTemplate>
            <table class="event-list-table">
                <tr>
                    <th>Start Date</th>
                    <th>End Date</th>
                    <th>Association</th>
                    <th>Title</th>
                    <th>Location</th>
                    <th></th>
                </tr>
        </HeaderTemplate>
        <ItemTemplate>
            <tr>
                <td>
                    <asp:Label runat="server" ID="StartDate"
                        Text='<%# DateTime.Parse(Eval("StartDate").ToString()).ToString("yyyy MMM d HH:mm") %>' />
                </td>
                <td>
                    <asp:Label runat="server" ID="EndDate"
                        Text='<%# DateTime.Parse(Eval("EndDate").ToString()).ToString("yyyy MMM d HH:mm") %>' />
                </td>
                <td>
                    <asp:Label runat="server" ID="Association"
                        Text='<%# WriteAllAssociations((ICollection<associations>)Eval("associations")) %>' />
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
                    <asp:HyperLink ID="HyperLink" NavigateUrl='<%# "/EventDetails?id=" + Eval("Id") %>' runat="server" ImageUrl="http://www.ric.edu/images/icons/icon_new-tab.png" Target="_blank"></asp:HyperLink>
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
                    <asp:Label runat="server" ID="EndDate"
                        Text='<%# DateTime.Parse(Eval("EndDate").ToString()).ToString("yyyy MMM d HH:mm") %>' />
                </td>
                <td bgcolor="#00FFFF">
                    <asp:Label runat="server" ID="Association"
                        Text='<%# WriteAllAssociations((ICollection<associations>)Eval("associations")) %>' />
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
                    <asp:HyperLink ID="HyperLink" NavigateUrl='<%# "/EventDetails?id=" + Eval("Id") %>' runat="server" ImageUrl="http://www.ric.edu/images/icons/icon_new-tab.png" Target="_blank"></asp:HyperLink>
                </td>
            </tr>
        </AlternatingItemTemplate>
        <FooterTemplate>
            </table>
        </FooterTemplate>
    </asp:Repeater>
</div>
