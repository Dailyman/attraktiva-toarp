<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Navigation.ascx.cs" Inherits="EventHandlingSystem.Navigation1" %>

<script type="text/javascript">
    //<![CDATA[
    jQuery(function () {
        //jQuery('#Site-navigation nav').hide();
        jQuery('#toggle-nav-btn').click(function () {
            //$("#Site-navigation nav").slideToggle(500, "swing", $(this).toggleClass("rotate-180 rotate-m180"));
            jQuery('#Site-navigation nav').toggle('slide').promise().done(function () {
                __doPostBack('SiteNavMenuList', $('#Site-navigation nav').css('display'));
            });
            jQuery('#toggle-nav-icon').toggleClass('rotate rotate-reset');
        });
    });
    //]]>
</script>

<script type="text/javascript" charset="utf-8">
    //<![CDATA[
    function pageLoad() {
        jQuery('#Site-navigation a').each(function () {
            if (jQuery(this).attr('href').toLowerCase() === (window.location.pathname + window.location.search).toLowerCase()) {
                jQuery(this).addClass('current-link');
            }
        });
    };
    //]]>
    </script>

<div id="Site-navigation">
    <div id="toggle-nav-btn" class="nav-title">

        <%--<asp:Label ID="LabelDisplay" runat="server" Text="ValuePath: "></asp:Label>--%>
        <img id="toggle-nav-icon" class="rotate-reset" src="../Images/list-32x32.png" />

    </div>
    <nav id="SiteNavMenuList" runat="server">
        <%--<hr />--%>
        <ul>
            <li><a href="/">Home</a></li>
            <li>
                <asp:UpdatePanel runat="server" ID="UpdatePanelTreeNav" UpdateMode="Always">
                    <ContentTemplate>
                        <asp:TreeView ID="TreeViewNavigation" runat="server"
                            OnTreeNodeCollapsed="TreeViewNavigation_TreeNodeCollapsed"
                            OnTreeNodeExpanded="TreeViewNavigation_TreeNodeExpanded"
                            ExpandImageUrl="/Images/icon-arrow-right-b-16.png"
                            CollapseImageUrl="/Images/icon-arrow-down-b-16.png"
                            SkipLinkText=""
                            SelectedNodeStyle-BackColor="#95a5a6"
                            SelectedNodeStyle-CssClass="selectedNodesNav">
                        </asp:TreeView>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="TreeViewNavigation" />
                    </Triggers>
                </asp:UpdatePanel>
            </li>
            <li>
                <br />
                <%--<hr />--%>
                <br />
            </li>
            <li><a href="/SiteSettings.aspx" class="text-align-left">
                <img src="/Images/cog-16x16.png" />
                Site settings</a></li>
        </ul>
    </nav>
</div>
