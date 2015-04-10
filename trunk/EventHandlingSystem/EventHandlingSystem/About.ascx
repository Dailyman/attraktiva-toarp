<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="About.ascx.cs" Inherits="EventHandlingSystem.About" %>
<style type="text/css">
     .content-table {
        width: 100%;
     }
     .content-table td
     {
         vertical-align: top;
         padding: 0;
     }

    .logo-box
    {
        max-width: 300px;
        max-height: 300px;
    }

    .logo
    {
        width: 100%;
        height: 100%;
    }

    .aside-box {
        display: inline-block;
        vertical-align: top;
        -moz-min-width: 300px;
        -ms-min-width: 300px;
        -o-min-width: 300px;
        -webkit-min-width: 300px;
        min-width: 300px;
        width: 100%;
    }
</style>
<script type="text/javascript">
    $(document).ready(function () {
        var attr = $('img.logo').attr('src');
        // For some browsers, `attr` is undefined; for others,
        // `attr` is false.  Check for both.
        if (typeof attr !== typeof "undefined" && attr !== false) {
            $('img.logo').hide();
        }
    });
</script>
<table class="content-table">
    <tr>
        <td>
            <h1><%: Page.Title %></h1>
        </td>
        <td rowspan="3">
            <div class="aside-box">
                <div class="logo-box">
                    <asp:Image ID="ImageLogo" CssClass="logo" runat="server" />
                </div>
                <h3>Shortcuts</h3>
                <ul>
                    <li><a id="HomeLink" runat="server" href="~/">Home</a></li>
                    <li><a id="EventsLink" runat="server" href="~/EventDetails.aspx?">Events</a></li>
                    <li><a id="SettingsLink" runat="server" href="~/Admin/PageSettings.aspx?">Page settings</a></li>
                </ul>
            </div>
        </td>
    </tr>
    <tr>
        <td>
            <h3>About us</h3>
            <div>
                <h5>Description:</h5>
                <asp:Literal ID="LiteralDescription" runat="server" Text="There is no description"></asp:Literal>
                <br/> <br/> <br/>
            </div>
        </td>
    </tr>
    <tr>
        <td>
            <h3>Contact Us</h3>
            <div>
                <asp:Repeater ID="RepeaterContacts" runat="server">
                    <ItemTemplate>
                        <div style="float: left; padding: 10px;" >
                            <b><asp:Label ID="lbContactFirstName" runat="server" Text='<%# Eval("FirstName") %>'></asp:Label>
                            <asp:Label ID="lbContactSurName" runat="server" Text='<%# Eval("SurName") %>'></asp:Label></b><br />
                            Email: <asp:HyperLink ID="hlnk" runat="server" 
                                NavigateUrl=' <%# "mailto: " + Eval("Email") %>'
                                Text='<%# Eval("Email") %>'></asp:HyperLink><br />
                            Phone: <asp:Label ID="lbContactPhone" runat="server" Text='<%# Eval("Phone") %>'></asp:Label><br />
                            <br />
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
                </div>
            <div>
               <asp:Label ID="lbContactMessage" runat="server" Text=""></asp:Label>
            </div>
        </td>
    </tr>
</table>
