<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Navigation.ascx.cs" Inherits="EventHandlingSystem.Navigation1" %>
<script type="text/javascript">
    $(document).ready(function() {
        $("#toggle-btn").toggle(
        function () {
            $(this).addClass("arrow-right");
            $(this).removeClass("arrow-left");
        }, function () {
            $(this).addClass("arrow-left");
            $(this).removeClass("arrow-right");
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
<%--<div id="toggle-btn" class="arrow-left"></div>--%>
<nav>
    <ul>
        <li><a href="/">Home</a></li>
        <li><asp:TreeView ID="TreeViewNavigation" runat="server" OnSelectedNodeChanged="TreeViewNavigation_OnSelectedNodeChanged"
            ExpandImageUrl="~/Images/icon-arrow-right-b-16.png"
            CollapseImageUrl="~/Images/icon-arrow-down-b-16.png"
            SkipLinkText=""
    
            SelectedNodeStyle-BackColor="#95a5a6"
            SelectedNodeStyle-CssClass="selectedNodesNav">
</asp:TreeView></li>
        <li><br/></li>
        <li><a href="/SiteSettings.aspx"><img src="Images/cog-16x16.png" /> Site settings</a></li>
    </ul>
    
</nav>

<br/>
<%--<asp:Label ID="LabelDisplay" runat="server" Text="ValuePath: "></asp:Label>--%>