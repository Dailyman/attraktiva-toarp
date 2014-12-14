<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Navigation.ascx.cs" Inherits="EventHandlingSystem.Navigation1" %>

<script type="text/javascript">
    //<![CDATA[
    jQuery(function () {
        jQuery('#Site-navigation nav').hide();
        jQuery('#toggle-nav-btn').click(function () {
            //$("#Site-navigation nav").slideToggle(500, "swing", $(this).toggleClass("rotate-180 rotate-m180"));
            jQuery('#Site-navigation nav').toggle('slide', 200);
            jQuery(this).toggleClass('rotate-180 rotate-m180');
        });
    });
    //]]>
</script>

<script type="text/javascript" charset="utf-8">
    //<![CDATA[
    jQuery(function () {
        jQuery('#Site-navigation a').each(function () {
            if (jQuery(this).attr('href') === window.location.pathname + window.location.search ) {
                jQuery(this).addClass('current-link');
            }
        });
    });
    //]]>
</script>

<div id="Site-navigation">
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
</div>

<div class="nav-title">
    <img id="toggle-nav-btn" class="rotate-m180" src="../Images/list-32x32.png" />
</div>

<%--<asp:Label ID="LabelDisplay" runat="server" Text="ValuePath: "></asp:Label>--%>