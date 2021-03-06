﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EventList.ascx.cs" Inherits="EventHandlingSystem.EventList" %>
<%@ Import Namespace="System.Globalization" %>
<%@ Import Namespace="EventHandlingSystem" %>
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

     html > body .Important
    {
        font-size: large;
        color: Red;
    }

     /*ta bort*/
     .event-list-table-removed
    {
        /*background-color: aliceblue;*/
        margin: 1px auto;
        /*height: 600px;*/
        /*min-width: 900px;*/
        height: 100%;
        width: 100%;
    }
     /*ta bort*/
        .event-list-table-removed td
        {
            padding: 3px;
            /* border: 1px solid lightblue; */
            /*vertical-align: top;*/
            max-width: 350px;
            /* background-color: aliceblue; */
            overflow: auto;
            /*word-break: normal;*/
            /*word-wrap: break-word;*/
        }
        /*ta bort*/
        .event-list-table input
        {
            width: auto;
        }

    .filter-section
    {
        overflow: auto;
        display: table;
        background-color: lightblue;
        padding: 5px;
    }

    .filter-section span
    {
        display: inline-table;
        vertical-align: middle;
    }


    .filter-section span b
    {
        margin: 0 auto;
        padding: 3px;
        display: inline;
    }
    .filter-section select
    {
        height: 25px;
    }

    .event-list-table
    {
        /*background-color: aliceblue;*/
        margin: 0px auto;
        /*height: 600px;*/
        /*min-width: 900px;*/
        height: 100%;
        /*width: 100%;*/
        /*display: block;*/
        overflow: auto;
    }

    .event-list-table th
    {
        padding: 5px;
        /* border: 1px solid lightblue; */
        vertical-align: top;
        /*max-width: 120px;*/
        /* background-color: aliceblue; */
        overflow: hidden;
        /*word-break: normal;*/
        /*word-wrap: break-word;*/
    }
        .event-list-table td {
            padding: 5px;
            /* border: 1px solid lightblue; */
            vertical-align: top;
            /*max-width: 120px;*/
            /* background-color: aliceblue; */
            overflow: hidden;
            -ms-word-break: break-word;
            -moz-word-break: break-word;
            -o-word-break: break-word;
            word-break: break-word;
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

    .no-data-text-wrap
    {
        text-align: center;
    }

    .no-data-text
    {
        font-weight: bold;
        font-size: 24px;
    }
</style>
<script type="text/javascript">
    $(document).ready(function () {
        $('table.event-list-table').closest("div").css("overflow", "auto");
        $('table.event-list-table').closest("div").css("max-height", "500px");

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
    <div class="filter-section">
        <div>
            <span>
                <b>From</b><br/>
                <asp:TextBox CssClass="input-date" ID="TxtStart" TextMode="Date" runat="server" AutoPostBack="True"></asp:TextBox>
                <asp:CustomValidator ID="CustomValiStartDate" runat="server" ControlToValidate="TxtStart" ErrorMessage="Use the right format! (e.g. 2005-06-21)" OnServerValidate="CustomValiStartDate_OnServerValidate" ValidationGroup="ValGroupFilter" Display="Dynamic" SetFocusOnError="True"></asp:CustomValidator></span>
            <span>
                <b>To</b><br/>
                <asp:TextBox CssClass="input-date" ID="TxtEnd" TextMode="Date" runat="server" AutoPostBack="True"></asp:TextBox>
                <asp:CustomValidator ID="CustomValiEndDate" runat="server" ControlToValidate="TxtEnd" ErrorMessage="Use the right format! (e.g. 2005-06-21)" OnServerValidate="CustomValiEndDate_OnServerValidate" ValidationGroup="ValGroupFilter" Display="Dynamic" SetFocusOnError="True"></asp:CustomValidator></span>
            <span>
                <b>Community</b><br/>
                <asp:DropDownList ID="DropDownListComm" runat="server" OnSelectedIndexChanged="DropDownListComm_OnSelectedIndexChanged" AutoPostBack="True"></asp:DropDownList></span>
            <span>
                <b>Association</b><br/>
                <asp:DropDownList ID="DropDownListAsso" runat="server" OnSelectedIndexChanged="DropDownListAsso_OnSelectedIndexChanged" AutoPostBack="True"></asp:DropDownList></span>
            <span>
                <b>Association category</b><br/>
                <asp:DropDownList ID="DropDownListCat" runat="server" OnSelectedIndexChanged="DropDownListCat_OnSelectedIndexChanged" AutoPostBack="True"></asp:DropDownList></span>
            <span>
                <b>Event category</b><br/>
                <asp:DropDownList ID="DropDownListSubCat" runat="server" AutoPostBack="True"></asp:DropDownList></span>
            <span>
                <b>Search</b><br/>
                <asp:TextBox ID="TxtSearch" runat="server" TextMode="Search" CssClass="search-box" AutoPostBack="True"></asp:TextBox></span>
        </div>
        <%--<span>
            <asp:Button ID="BtnFilter" runat="server" Text="Filter" OnClick="BtnFilter_OnClick" ValidationGroup="ValGroupFilter" />
        </span>--%>
    </div>
    <p align="center">
            <asp:Label ID="ActionStatus" runat="server" CssClass="Important"></asp:Label>
        </p>
    <asp:GridView ID="GridViewEventList" runat="server"
    AutoGenerateColumns="False"
        EmptyDataText="No events was found."
    OnRowCancelingEdit="GridViewEventList_OnRowCancelingEdit"
        OnRowEditing="GridViewEventList_OnRowEditing"
        OnRowDeleting="GridViewEventList_OnRowDeleting"
        OnRowUpdating="GridViewEventList_OnRowUpdating"
        OnRowUpdated="GridViewEventList_OnRowUpdated"
        OnRowDataBound="GridViewEventList_OnRowDataBound"
    CellPadding="4"
    ForeColor="#333333"
    GridLines="None"
    CssClass="event-list-table">
    <AlternatingRowStyle BackColor="White" />
    <EditRowStyle BackColor="#2461BF" />
    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
    <RowStyle BackColor="#EFF3FB" />
    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
    <SortedAscendingCellStyle BackColor="#F5F7FB" />
    <SortedAscendingHeaderStyle BackColor="#6D95E1" />
    <SortedDescendingCellStyle BackColor="#E9EBEF" />
    <SortedDescendingHeaderStyle BackColor="#4870BE" />
    <Columns>
        <%--<asp:BoundField DataField="Id" HeaderText="Id" ReadOnly="true" />--%>
        <%--<asp:BoundField DataField="StartDate" HeaderText="Start Date" DataFormatString="{0:yyyy-MM-dd HH:mm}"  />--%>
        <asp:TemplateField HeaderText="Start Date">
            <ItemTemplate>
                <%# ((bool)Eval("DayEvent") ? ((DateTime) Eval("StartDate")).ToString("yyyy-MM-dd"): ((DateTime) Eval("StartDate")).ToString("yyyy-MM-dd HH:mm")) %>
            </ItemTemplate>
            <EditItemTemplate>
                <asp:TextBox ID="TextBoxStartDate" runat="server" Text='<%#(bool)Eval("DayEvent") ? ((DateTime) Eval("StartDate")).ToString("yyyy-MM-dd", new CultureInfo("sv-SE")) : ((DateTime) Eval("StartDate")).ToString("yyyy-MM-dd HH:mm", new CultureInfo("sv-SE"))%>' TextMode='<%#(bool)Eval("DayEvent") ? TextBoxMode.Date : TextBoxMode.DateTimeLocal%>'></asp:TextBox>
            </EditItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="End Date">
            <ItemTemplate>
                <%# ((bool)Eval("DayEvent") ? ((DateTime) Eval("EndDate")).ToString("yyyy-MM-dd"): ((DateTime) Eval("EndDate")).ToString("yyyy-MM-dd HH:mm")) %>
            </ItemTemplate>
            <EditItemTemplate>
                <asp:TextBox ID="TextBoxEndDate" runat="server" Text='<%#(bool)Eval("DayEvent") ? ((DateTime) Eval("EndDate")).ToString("yyyy-MM-dd", new CultureInfo("sv-SE")) : ((DateTime) Eval("EndDate")).ToString("yyyy-MM-dd HH:mm", new CultureInfo("sv-SE"))%>' TextMode='<%#(bool)Eval("DayEvent") ? TextBoxMode.Date : TextBoxMode.DateTimeLocal%>'></asp:TextBox>
            </EditItemTemplate>
        </asp:TemplateField>
        <%--<asp:BoundField DataField="EndDate" HeaderText="End Date" DataFormatString="{0:yyyy-MM-dd HH:mm}" />--%>
        <%--<asp:BoundField DataField="associations" HeaderText="Associations" />--%>
        <asp:BoundField DataField="Title" HeaderText="Title" />
        <asp:BoundField DataField="Location" HeaderText="Location" />
        <asp:TemplateField HeaderText="Associations">
            <ItemTemplate>
                <asp:Repeater ID="RepeaterAsso" runat="server" DataSource='<%# Eval("associations") %>'>
                    <ItemTemplate>
                        "<%# Eval("Name") %>"<br />
                    </ItemTemplate>
                </asp:Repeater>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:HyperLinkField 
            DataTextField="" 
            Text='<img src="http://www.ric.edu/images/icons/icon_new-tab.png" border="0" />'
            Target="_blank"
            HeaderText="View"
            DataNavigateUrlFields="Id" 
            DataNavigateUrlFormatString="EventDetails?id={0}" />
        <asp:HyperLinkField 
            DataTextField="" 
            Text='<img src="http://www.smallheathschool.org.uk/images/icons/button_icons/dark/btn_edit.png" border="0" />'
            Target="_blank"
            HeaderText="Edit"
            DataNavigateUrlFields="Id" 
            DataNavigateUrlFormatString="Contributors/EventUpdate?id={0}" />
        <%--<asp:CommandField ButtonType="Link" ShowEditButton="true" ShowDeleteButton="true" />--%>
        <%--<asp:CommandField ButtonType="Image" ShowEditButton="true" EditImageUrl="http://www.smallheathschool.org.uk/images/icons/button_icons/dark/btn_edit.png"/>--%>
        <%--<asp:TemplateField HeaderText="Delete">
            <ItemTemplate>
                <asp:HyperLink ID="HyperLinkViewEvent" ImageUrl="http://www.ric.edu/images/icons/icon_new-tab.png" NavigateUrl='EventDetails?id=<%# Eval("Id") %>' Target="_blank" ToolTip="View details" runat="server">HyperLink</asp:HyperLink>
                <asp:LinkButton ID="LinkButtonEditEvent" runat="server" CausesValidation="False" CommandName="Edit" Text='<img src="http://www.smallheathschool.org.uk/images/icons/button_icons/dark/btn_edit.png" border="0" />'></asp:LinkButton>
                <br/>
                <asp:LinkButton ID="LinkButtonDeleteEvent" runat="server" CausesValidation="False" CommandName="Delete" Text='<img src="http://www.navicat.com/manual/online_manual/en/navicat/win_manual/img/icon_delete.png" border="0" />' OnClientClick="return (confirm('Are you sure you want to DELETE this event?') === confirm('Are you sure that you want to PERMANENTLY delete this event?'));"></asp:LinkButton>
            </ItemTemplate>
        </asp:TemplateField>--%>
    </Columns>
</asp:GridView>
    <%--<asp:Repeater ID="RepeaterEvents2" runat="server">
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
    </asp:Repeater>--%>
    <%--<div class="no-data-text-wrap">
        <asp:Label ID="LabelNoData" runat="server" Text="No Data" CssClass="no-data-text"></asp:Label>

    </div>--%>
</div>
