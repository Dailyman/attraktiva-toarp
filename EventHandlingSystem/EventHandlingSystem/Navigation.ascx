<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Navigation.ascx.cs" Inherits="EventHandlingSystem.Navigation1" %>

<%--<script type="text/javascript">
    $(document).ready(function () {
        $("#toggle-nav-btn").toggle(
        function () {
            $("#Site-navigation nav").addClass("collapsed", 400, "easeOutExpo");
            $(this).addClass("rotate-90", 100, "easeOutQuart");
        }, function () {
            $("#Site-navigation nav").removeClass("collapsed", 400, "easeOutExpo");
            $(this).removeClass("rotate-90", 100, "easeOutQuart");
        });
    });
</script>--%>

<script type="text/javascript">
    $(document).ready(function () {
        $("#toggle-nav-btn").click(function () {
            $("#Site-navigation nav").slideToggle(500, "swing", $(this).toggleClass("rotate-90 rotate-m90"));
        });

    });
</script>

<script type="text/javascript" charset="utf-8">
    //<![CDATA[
    jQuery(function () {
        jQuery('#Site-navigation a').each(function () {
            if (jQuery(this).attr('href') === window.location.pathname + window.location.search) {
                jQuery(this).addClass('current-link');
            }
        });
    });
    //]]>
</script>

<div class="nav-title">
    <img id="toggle-nav-btn" class="rotate-m90" src="../Images/list-32x32.png" /></div>
<hr />
<nav>
    <ul>
        <li><a href="/">Home</a></li>
        <li>
            <asp:TreeView ID="TreeViewNavigation" runat="server" OnSelectedNodeChanged="TreeViewNavigation_OnSelectedNodeChanged"
                ExpandImageUrl="~/Images/icon-arrow-right-b-16.png"
                CollapseImageUrl="~/Images/icon-arrow-down-b-16.png"
                SkipLinkText=""
                SelectedNodeStyle-BackColor="#95a5a6"
                SelectedNodeStyle-CssClass="selectedNodesNav">
            </asp:TreeView>
        </li>
        <li>
            <br />
            <hr />
            <br />
        </li>
        <li><a href="/SiteSettings.aspx" class="text-align-left">
            <img src="../Images/cog-16x16.png" />
            Site settings</a></li>
    </ul>

</nav>

<%--<asp:Label ID="LabelDisplay" runat="server" Text="ValuePath: "></asp:Label>--%>