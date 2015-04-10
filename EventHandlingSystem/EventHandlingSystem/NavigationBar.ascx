<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NavigationBar.ascx.cs" Inherits="EventHandlingSystem.NavigationBar" %>

<style type="text/css">
    #nav-wrapper
    {
        margin: 0 auto;
        width: 1000px;
    }

    /*@media only screen and (max-width: 950px)
    {
        #nav-wrapper
        {
            margin: 0 auto;
            width: auto;
        }
    }*/



    .main_menu
    {
        -moz-min-width: 80px;
        -ms-min-width: 80px;
        -o-min-width: 80px;
        -webkit-min-width: 80px;
        min-width: 80px;
        -ms-word-break: break-all;
        -moz-word-break: break-all;
        -o-word-break: break-all;
        word-break: break-all;
        background-color: #49586c /*#4169e1*/;
        color: #fff !important;
        text-align: center;
        height: 30px;
        line-height: 30px;
        margin-top: 5px;
        margin-right: 5px;
        padding: 3px 20px !important;
        z-index: 9999;
    }

    .level_menu {
        -ms-word-break: break-all;
        -moz-word-break: break-all;
        -o-word-break: break-all;
        word-break: break-all;
        width: auto;
        background-color: #217ebb /*#008b8b*/;
        color: #fff !important;
        text-align: center;
        height: 30px;
        line-height: 30px;
        margin-top: 5px;
        margin-left: 5px;
        padding: 0px 5px;
        z-index: 9999;
        -moz-border-radius: 3px;
        -webkit-border-radius: 3px;
        -ms-border-radius: 3px;
        border-radius: 3px;
        border: 1px solid #1774b1 !important;
    }

    .main_menu.highlighted
    {
        background-color: #EB7260;
    }
    .main_menu img.icon
    {
        padding-right: 10px;
        vertical-align: baseline !important;
    }

    .level_menu.highlighted
    {
        background-color: #852B91;
    }

    .selected
    {
        background-color: #852B91;
        color: #fff !important;
    }
</style>

<div id="nav-wrapper">

    <asp:Menu ID="MenuBar" runat="server" Orientation="Horizontal" MaximumDynamicDisplayLevels="10" LevelSelectedStyles="selected">
        <StaticSelectedStyle BackColor="#EB7260" />
        <Items>
            <asp:MenuItem NavigateUrl="~/Default.aspx" Text="Home" Selectable="True" ImageUrl="/Images/Home-icon.svg" />
            <asp:MenuItem NavigateUrl="#" Text="Communities & Associations" Selectable="True" Value="CommunityFolder" />
            <asp:MenuItem NavigateUrl="#" Text="Events" Selectable="True">
                <ChildItems>
                    <asp:MenuItem NavigateUrl="~/Contributors/EventCreate.aspx" Text="New event" Selectable="True" />
                    <asp:MenuItem NavigateUrl="~/EventDetails.aspx" Text="View event" Selectable="True" />
                    <asp:MenuItem NavigateUrl="~/Contributors/EventUpdate.aspx" Text="Update event" Selectable="True" />
                </ChildItems>
            </asp:MenuItem>
            <asp:MenuItem NavigateUrl="#" Text="Administration" Selectable="True" ImageUrl="/Images/cog-16x16.png">
                <ChildItems>
                    <asp:MenuItem NavigateUrl="~/Admin/PageSettings.aspx" Text="Page settings" Selectable="True" />
                    <asp:MenuItem NavigateUrl="~/Admin/SiteSettings.aspx" Text="Site settings" Selectable="True" />
                </ChildItems>
            </asp:MenuItem>
            
        </Items>
        <LevelMenuItemStyles>
            <asp:MenuItemStyle CssClass="main_menu" />
            <asp:MenuItemStyle CssClass="level_menu" />
            <asp:MenuItemStyle CssClass="level_menu" />
            <asp:MenuItemStyle CssClass="level_menu" />
            <asp:MenuItemStyle CssClass="level_menu" />
            <asp:MenuItemStyle CssClass="level_menu" />
            <asp:MenuItemStyle CssClass="level_menu" />
            <asp:MenuItemStyle CssClass="level_menu" />
            <asp:MenuItemStyle CssClass="level_menu" />
            <asp:MenuItemStyle CssClass="level_menu" />
            <asp:MenuItemStyle CssClass="level_menu" />
            <asp:MenuItemStyle CssClass="level_menu" />
        </LevelMenuItemStyles>
    </asp:Menu>

</div>
