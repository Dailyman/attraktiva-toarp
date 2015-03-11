<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WebPageComponentControl.ascx.cs" Inherits="EventHandlingSystem.WebPageComponentControl" %>
<style>
    .bull-list-comm, 
    .bull-list-asso
     {
        vertical-align: text-top;
        width: 250px;
     }
</style>


<div class="inner-content-box">

    <asp:MultiView ID="MultiViewWebPage" runat="server" ActiveViewIndex="0">
        <asp:View ID="ViewWebPage" runat="server">
            <div class="view-webpage">
                <table>
                    <tr>
                        <td class="bull-list-comm">
                            <b><asp:Label ID="lbCommWebPage" runat="server" Text="Community Webpages"></asp:Label></b>
                            <asp:BulletedList ID="bullListCommWebpages" runat="server" 
                                OnClick="bullListCommWebpages_OnClick"
                                DisplayMode="LinkButton">
                            </asp:BulletedList>
                        </td>
                        <td class="bull-list-asso">
                            <b><asp:Label ID="lbAssoWebPage" runat="server" Text="Association Webpages" Visible="False"></asp:Label></b>
                            <asp:BulletedList ID="bullListAssoWebpages" runat="server"
                                Visible="False"
                                OnClick="bullListAssoWebpages_OnClick"
                                DisplayMode="LinkButton">
                            </asp:BulletedList>
                        </td>
                    </tr>
                </table>
                
                
            </div>
        </asp:View>
    </asp:MultiView>
    <br />

    <asp:HiddenField ID="hdnfWebpageId" runat="server" />
    <asp:MultiView ID="MultiViewWebPageDetails" runat="server" ActiveViewIndex="-1">
        <asp:View ID="ViewWebPageDetails" runat="server">
            <div class="view-webpage">
                <h3><asp:Label ID="lbWebpageDetail" runat="server" Text="Web Page Details"></asp:Label></h3><br/>
                <span><b>Title: </b>
                    <asp:Label ID="lbWebPageTitle" runat="server"></asp:Label></span>
                <br/><br/>
                <span><asp:Label ID="lbWebpageCommId" runat="server" Visible="False"></asp:Label>
                    <asp:Label ID="lbWebpageAssoId" runat="server" Visible="False"></asp:Label></span><br/><br/>
                <span><b>Layout: </b>
                    <asp:TextBox ID="tbLayout" runat="server"></asp:TextBox></span><br/>
                <span><b>Style: </b>
                    <asp:TextBox ID="tbStyle" runat="server"></asp:TextBox></span><br/>
                
                <div class="btn-align-right">
                    <asp:Button ID="btnWebpageUpdate" CssClass="btn-blue" runat="server" Text="Update" 
                        OnClick="btnWebpageUpdate_OnClick"/>
                </div>
                <asp:Label ID="lbWebPageUpdate" runat="server" Text=""></asp:Label>
            </div>
        </asp:View>
    </asp:MultiView>
</div>
