<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="About.ascx.cs" Inherits="EventHandlingSystem.About" %>
<style type="text/css">
    .content-table td {
        vertical-align: top;
    }

     .logo
     {
         max-width: 300px;
         max-height: 300px;
     }

    .aside-box
    {
        display: inline-block;
        vertical-align: top;
        min-width: 300px;
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
<h1><%: Page.Title %></h1>
<br />
<table class="content-table">
    <tr>
        <td>
            <h3>About us</h3>
        </td>
    </tr>
    <tr>
        <td>
            <div>
                <h5>The standard Lorem Ipsum passage, used since the 1500s</h5>
                <p>
                    "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum."
                </p>
                <asp:Literal ID="LiteralAbout" runat="server"></asp:Literal>
            </div>
        </td>
        <td>
            <div class="aside-box">
                <asp:Image ID="ImageLogo" CssClass="logo" runat="server" Width="100%" Height="100%" />
                <h3>Aside Title</h3>
                <p>
                    Use this area to provide additional information.
                </p>
                <p>
                    Use this area to provide additional information.
                </p>
                <p>
                    Use this area to provide additional information.
                </p>
                <ul>
                    <li><a id="A1" runat="server" href="~/">Home</a></li>
                    <li><a id="A2" runat="server" href="~/EventDetails">Events</a></li>
                    <li><a id="A3" runat="server" href="~/SiteSettings">Settings</a></li>
                </ul>
            </div>
        </td>
    </tr>



</table>
