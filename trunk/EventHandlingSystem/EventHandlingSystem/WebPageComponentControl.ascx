<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WebPageComponentControl.ascx.cs" Inherits="EventHandlingSystem.WebPageComponentControl" %>
<style>
    .bull-list-comm,
    .bull-list-asso
    {
        vertical-align: text-top;
        width: 250px;
    }

    .tb-small
    {
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
                                DisplayMode="LinkButton">
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
                <h3>
                    <asp:Label ID="lbWebpageDetail" runat="server" Text="Web Page Details"></asp:Label></h3>
                <br />
                <span><b>Title: </b>
                    <asp:TextBox ID="tbWebpageTitle" runat="server"></asp:TextBox></span><br />
                <br />
                <span>
                    <asp:Label ID="lbCommAssoName" runat="server"></asp:Label>
                    <asp:HyperLink ID="hlnkCommAssoName" runat="server" Target="_blank"></asp:HyperLink></span>
                <br />
                <br />
                <span><b>Layout: </b>
                    <asp:TextBox ID="tbLayout" runat="server"></asp:TextBox></span><br />
                <span><b>Style: </b>
                    <asp:TextBox ID="tbStyle" runat="server"></asp:TextBox></span><br />
                <div class="btn-align-right">
                    <asp:Button ID="btnWebpageUpdate" CssClass="btn-blue" runat="server" Text="Update"
                        OnClick="btnWebpageUpdate_OnClick" />
                </div>
                <asp:Label ID="lbWebPageUpdate" runat="server" Text=""></asp:Label>
                <br />
                <br />
                <h3>
                    <asp:Label ID="lbComponentDetails" runat="server" Text="Components"></asp:Label></h3>
                <br />

                <%--<asp:Repeater ID="RepeaterComponents" runat="server" OnItemCreated="RepeaterComponents_OnItemCreated">
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
                </asp:Repeater>--%>

                
                <asp:Label ID="LabelActionStatus" runat="server"></asp:Label>

                <asp:GridView ID="GridViewComponentList" runat="server"
                    AutoGenerateColumns="False"
                    OnRowCancelingEdit="GridViewComponentList_OnRowCancelingEdit"
                    OnRowEditing="GridViewComponentList_OnRowEditing"
                    OnRowUpdating="GridViewComponentList_OnRowUpdating"
                    OnRowUpdated="GridViewComponentList_OnRowUpdated"
                    OnRowDeleting="GridViewComponentList_OnRowDeleting"
                    CellPadding="4"
                    ForeColor="#333333"
                    GridLines="None"
                    CssClass="user-list-table">
                    <AlternatingRowStyle BackColor="White" />
                    <EditRowStyle BackColor="#2461BF" />
                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="#EFF3FB" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <SortedAscendingCellStyle BackColor="#F5F7FB" />
                    <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                    <SortedDescendingCellStyle BackColor="#E9EBEF" />
                    <SortedDescendingHeaderStyle BackColor="#4870BE" />
                    <Columns>
                        <%--<asp:BoundField DataField="Item1" HeaderText="Ordering Number" />--%>
                        <asp:BoundField DataField="Item1" HeaderText="Id" ReadOnly="True"/>
                        <asp:TemplateField HeaderText="Ordering Number">
                            <ItemTemplate>
                                <%# Eval("Item2")  %>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBoxOrderingNO" runat="server" 
                                    Text='<%#Eval("Item2") %>' 
                                    TextMode="Number"></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Control Name">
                             <ItemTemplate>
                                <%# Eval("Item3")  %>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:DropDownList ID="DropDownListControlName" runat="server"  
                                    DataSourceID="DataSource"
                                    DataValueField="Value"
                                    DataTextField="Text"
                                    Text='<%#Eval("Item4") %>'></asp:DropDownList>
                                <asp:ObjectDataSource runat="server"
                                    ID="DataSource"
                                    SelectMethod="GetAllControlsListItems"
                                    DataObjectTypeName="System.Web.UI.WebControls.ListItem"
                                    TypeName="EventHandlingSystem.WebPageComponentControl"
                                    ></asp:ObjectDataSource>
                            </EditItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="LinkButtonEditEvent" ClientIDMode="Static" runat="server" CausesValidation="False" CommandName="Edit" Text="Edit"></asp:LinkButton>
                                <asp:LinkButton ID="LinkButtonUpdateEvent" ClientIDMode="Static" runat="server" CausesValidation="False" CommandName="Update" Text="Update" Visible="False" CssClass="edit-mode-link" />
                                <asp:LinkButton ID="LinkButtonCancelEdit" ClientIDMode="Static" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel" Visible="False" CssClass="edit-mode-link" />
                                <asp:LinkButton ID="LinkButtonDeleteEvent" ClientIDMode="Static" runat="server" CausesValidation="False" 
                                    CommandName="Delete" Text="Remove" 
                                    OnClientClick="return confirm('Are you sure you want to remove this component?');" 
                                    CssClass="delete-mode-link"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <%--<asp:CommandField ShowEditButton="True" ShowDeleteButton="True"/>--%>

                        <%--<asp:BoundField DataField="PasswordQuestion" HeaderText="PasswordQuestion" ReadOnly="True"/>
        <asp:CheckBoxField DataField="IsLockedOut" HeaderText="IsLockedOut" ReadOnly="true" />
        <asp:BoundField DataField="LastLockoutDate" HeaderText="LastLockoutDate" ReadOnly="true" />
        <asp:BoundField DataField="CreationDate" HeaderText="CreationDate" ReadOnly="true" />
        <asp:BoundField DataField="LastLoginDate" HeaderText="LastLoginDate" ReadOnly="true" />
        <asp:BoundField DataField="LastActivityDate" HeaderText="LastActivityDate" ReadOnly="true" />
        <asp:BoundField DataField="LastPasswordChangedDate" HeaderText="LastPasswordChangedDate" ReadOnly="true" />--%>
                        
                    </Columns>
                </asp:GridView>

                <%--<asp:DropDownList ID="ddlAddOrderingNO" runat="server"></asp:DropDownList>--%>

                <asp:TextBox ID="tbAddOrderingNumber" runat="server"
                    TextMode="Number" 
                    CssClass="tb-small"
                    Text="1" min="1"></asp:TextBox>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1"
                    runat="server" 
                    ErrorMessage="Use Numbers Only!!! (^O^)v"
                    ControlToValidate="tbAddOrderingNumber"
                    ValidationExpression="^[0-9]+$"
                    Display="Dynamic"
                    SetFocusOnError="True">
                </asp:RegularExpressionValidator>
                <asp:DropDownList ID="ddlAddComControls" runat="server"></asp:DropDownList>
                <asp:Button ID="AddControl" runat="server"
                    Text="Add New Control"
                    CssClass="btn-small"
                    OnClick="AddControl_OnClick" />

                <%--<asp:Button ID="btnAddControl" runat="server" Text="+" OnClick="btnAddControl_OnClick"/>
                <asp:Button ID="btnRemoveControl" runat="server" Text="-" OnClick="btnRemoveControl_OnClick"/>--%>
            </div>
        </asp:View>
    </asp:MultiView>
</div>
