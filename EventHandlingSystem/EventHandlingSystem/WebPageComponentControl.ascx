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
                        <td class="bull-list-comm"><b>Community WebPages: </b>
                            <asp:BulletedList ID="bullListCommWebpages" runat="server" 
                    OnClick="bullListCommWebpages_OnClick" 
                    DisplayMode="LinkButton"></asp:BulletedList></td>
                        <td class="bull-list-asso" ><b>Association WebPages: </b>
                            <asp:BulletedList ID="bullListAssoWebpages" runat="server" 
                    OnClick="bullListAssoWebpages_OnClick"
                    DisplayMode="LinkButton"></asp:BulletedList></td>
                    </tr>
                </table>
                
                
            </div>
        </asp:View>
    </asp:MultiView>
    <br />
    
    <asp:MultiView ID="MultiViewComponent" runat="server" ActiveViewIndex="0">
        <asp:View ID="ViewComponent" runat="server">
            <div class="view-component">
                Hejdå
            </div>
        </asp:View>
    </asp:MultiView>
</div>
