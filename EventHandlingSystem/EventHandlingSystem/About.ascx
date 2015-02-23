<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="About.ascx.cs" Inherits="EventHandlingSystem.About" %>
<style type="text/css">
    .content-table td
    {
        vertical-align: top;
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
    <tr>
        <td>
            <h3>About us</h3>
            <div>
                <h5>The standard Lorem Ipsum passage, used since the 1500s</h5>
                <p>
                    "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum."
                </p>
                <asp:Literal ID="LiteralAbout" runat="server"></asp:Literal>
            </div>
        </td>

    </tr>
    <tr>
        <td>
            <h3>Contact</h3>
            <div>
                <h5>The standard Lorem Ipsum passage, used since the 1500s</h5>
                <p>
                    "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum."
                </p>
            </div>
        </td>
    </tr>

</table>
