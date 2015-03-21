<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WebPageComponentControl.ascx.cs" Inherits="EventHandlingSystem.WebPageComponentControl" %>
<style>
    .bull-list-comm,
    .bull-list-asso
    {
        vertical-align: text-top;
        width: 250px;
    }

    .tb-small {
        font-size: 14px;
        width: 50px;
    }
</style>


<div class="inner-content-box">

    <asp:MultiView ID="MultiViewWebPage" runat="server" ActiveViewIndex="0">
        <asp:View ID="ViewWebPage" runat="server">
            <div class="view-webpage">
                <table>
                    <tr>
                        <td class="bull-list-comm">
                            <b>
                                <asp:Label ID="lbCommWebPage" runat="server" Text="Community Webpages"></asp:Label></b>
                            <asp:BulletedList ID="bullListCommWebpages" runat="server"
                                OnClick="bullListCommWebpages_OnClick"
                                DisplayMode="LinkButton" >
                            </asp:BulletedList>
                        </td>
                        <td class="bull-list-asso">
                            <b>
                                <asp:Label ID="lbAssoWebPage" runat="server" Text="Association Webpages" Visible="False"></asp:Label></b>
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
                <h3><asp:Label ID="lbWebpageDetail" runat="server" Text="Web Page Details"></asp:Label></h3><br />
                <span><b>Title: </b>
                    <asp:TextBox ID="tbWebpageTitle" runat="server"></asp:TextBox></span><br /><br />
                <span>
                    <asp:Label ID="lbCommAssoName" runat="server"></asp:Label>
                    <asp:HyperLink ID="hlnkCommAssoName" runat="server" Target="_blank"></asp:HyperLink></span>
                <br /><br />
                <span><b>Layout: </b>
                    <asp:TextBox ID="tbLayout" runat="server"></asp:TextBox></span><br />
                <span><b>Style: </b>
                    <asp:TextBox ID="tbStyle" runat="server"></asp:TextBox></span><br />
                <div class="btn-align-right">
                    <asp:Button ID="btnWebpageUpdate" CssClass="btn-blue" runat="server" Text="Update"
                        OnClick="btnWebpageUpdate_OnClick" />
                </div>
                <asp:Label ID="lbWebPageUpdate" runat="server" Text=""></asp:Label>
                <br/><br/>
                <h3><asp:Label ID="lbComponentDetails" runat="server" Text="Components"></asp:Label></h3><br />

                <asp:Repeater ID="RepeaterComponents" runat="server" OnItemCreated="RepeaterComponents_OnItemCreated">
                    <ItemTemplate>
                        <asp:DropDownList ID="ddlOrderingNO" runat="server" 
                            Enabled="True" AppendDataBoundItems="True"
                            SelectedValue='<%# Eval("Id")%>'
                            ></asp:DropDownList>
                        <asp:DropDownList ID="ddlComControls" runat="server"
                            Enabled="True" AppendDataBoundItems="True"
                            SelectedValue='<%# Eval("Id")%>'
                            ></asp:DropDownList>
                        <br/>
                    </ItemTemplate>
                </asp:Repeater>

                <asp:DropDownList ID="ddlAddOrderingNO" runat="server"></asp:DropDownList>

                <asp:TextBox ID="tbAddOrderingNumber" runat="server" 
                    type="number" 
                    CssClass="tb-small"
                    Text="1" min="1"></asp:TextBox>
                <asp:DropDownList ID="ddlAddComControls" runat="server"></asp:DropDownList>
                <asp:Button ID="AddControl" runat="server" 
                    Text="Add New Control" 
                    CssClass="btn-small" 
                    OnClick="AddControl_OnClick"/>
                
                <%--<asp:Button ID="btnAddControl" runat="server" Text="+" OnClick="btnAddControl_OnClick"/>
                <asp:Button ID="btnRemoveControl" runat="server" Text="-" OnClick="btnRemoveControl_OnClick"/>--%>
            </div>
        </asp:View>
    </asp:MultiView>
</div>
