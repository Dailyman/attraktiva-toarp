<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CommunityAssociationControl.ascx.cs" Inherits="EventHandlingSystem.CommunityAssociationControl" %>
<div class="inner-content-box">
    
    <asp:MultiView ID="MultiViewSelectComm" runat="server" ActiveViewIndex="0">
        <asp:View ID="ViewSelectComm" runat="server">
            <div class="view-select-community">
                <span>Select a community</span>
                <br />
                <br />
                <asp:DropDownList ID="DropDownListCommunity"
                    runat="server"
                    OnSelectedIndexChanged="DropDownListCommunity_OnSelectedIndexChanged"
                    EnableViewState="True"
                    AutoPostBack="True">
                </asp:DropDownList><br />
                <div class="btn-align-right">
                    <asp:Button ID="ButtonCreateNewComm" CssClass="btn-blue" runat="server" Text="Create New Community" OnClick="ButtonCreateNewComm_OnClick" />
                </div>
            </div>
        </asp:View>
    </asp:MultiView>
   <br />
    <asp:MultiView ID="MultiViewCommDetails" runat="server" ActiveViewIndex="-1">
        <asp:View ID="ViewCommDetails" runat="server">
            <div class="view-community-details">
                <h3>Community Details</h3><br />
                    <asp:HyperLink ID="HyperLinkLogoCommunity" runat="server">
                        <%--Just nu har alla communities samma bild--%>
                        <asp:Image ID="ImageLogoCommunity" 
                            ImageUrl="~/Images/Community.jpg" 
                            ImageAlign="Right"
                            runat="server" />
                    </asp:HyperLink>
                <span><b>Name: </b>
                    <asp:TextBox ID="TextBoxCommName" Text="" runat="server"></asp:TextBox></span><br />
                <span><b>Description: </b><br />
                    <asp:TextBox ID="TextBoxCommDescript" 
                        TextMode="MultiLine" 
                        Text="This is a very friendly community..." 
                        ForeColor="gray"
                        runat="server"></asp:TextBox>
                </span><br />
                <span><b>Logo URL: </b>
                    <asp:TextBox ID="TextBoxLogoImgUrl" Text="~/Images/Community.jpg" ForeColor="gray" runat="server"></asp:TextBox>
                </span>
                <br />
                <br />
                <asp:Label ID="LabelCreated" runat="server" Text=""></asp:Label><br />
                <asp:Label ID="LabelCreatedBy" runat="server" Text=""></asp:Label><br />
                <br />
                <div class="btn-align-right">
                    <asp:Button ID="ButtonCommSave" CssClass="btn-blue" runat="server" Text="Save Changes" OnClick="ButtonCommSave_OnClick"/>
                </div>
                <asp:Label ID="LabelCommSave" runat="server" Text=""></asp:Label>
            </div><br />

            <div class="view-community-details-association">
                <b><asp:Label ID="LabelAssoInComm" runat="server" Text=""></asp:Label></b>
                <br /><br />
                <asp:ListBox ID="ListBoxAsso" OnSelectedIndexChanged="ListBoxAsso_OnSelectedIndexChanged" Width="200px" runat="server" AutoPostBack="True"></asp:ListBox>
                <br />
                <br />
                <div class="btn-align-right">
<%--                    <asp:Button ID="ButtonEditAsso" CssClass="btn-blue" runat="server" Text="Show Association Details" OnClick="ButtonEditAsso_OnClick" />--%>
                    <asp:Button ID="ButtonCreateNewAsso" CssClass="btn-blue" runat="server" Text="Create New Association" OnClick="ButtonCreateNewAsso_OnClick" /><br />
                </div>
                <asp:Label ID="LabelErrorMessage" runat="server" Text=""></asp:Label><br />
            </div>
        </asp:View>
    </asp:MultiView>
    
    <asp:MultiView ID="MultiViewAssoDetails" runat="server" ActiveViewIndex="-1">
        <asp:View ID="ViewAssoDetails" runat="server">
            <br />
            <div class="view-association-details">
                <h3>Association Details</h3><br />
                <asp:HyperLink ID="HyperLinkLogoAssociation" runat="server">
                        <%--Just nu har alla associations samma bild--%>
                        <asp:Image ID="ImageLogoAssociation" 
                            ImageUrl="~/Images/Association.jpg"
                            ImageAlign="Right"
                            runat="server" />
                    </asp:HyperLink>
                <span><b>Name: </b>
                    <asp:TextBox ID="TextBoxAssoName" Text="" runat="server"></asp:TextBox></span><br /><br />
                <span><b>Community: </b>
                    <asp:DropDownList ID="DropDownListCommunityInAsso" runat="server"></asp:DropDownList></span><br /><br />
                <span><b>Parent Association: </b>
                    <asp:DropDownList ID="DropDownListParentAsso" runat="server"></asp:DropDownList></span><br /><br />
                
                <b>Categories in the Association: </b>
                
                    <asp:DropDownList ID="DropDownListCategories" AutoPostBack="True" runat="server"></asp:DropDownList>
                    <asp:Button ID="ButtonCatAdd" runat="server" Text="Add Category" CssClass="smallButton" OnClick="ButtonCatAdd_OnClick"/>
                    <asp:Button ID="ButtonCatRemove" runat="server" Text="Remove" CssClass="smallButton" OnClick="ButtonCatRemove_OnClick"/>
                    <br/>
                    <asp:ListBox ID="ListBoxCatInAsso" AutoPostBack="True" Width="200px" runat="server" SelectionMode="Multiple"></asp:ListBox>
               

                <%--<span><b>Select Category: </b>
                    <asp:ListBox ID="ListBoxCategories" AutoPostBack="True" Width="200px" runat="server"></asp:ListBox>
                    </span><br /><br />--%>
                <%--<h5>Categories in the Association</h5>
                <asp:CheckBoxList ID="CheckBoxListAssoType" runat="server"></asp:CheckBoxList>
                <asp:BulletedList ID="BulletedListAssoType" runat="server"></asp:BulletedList><br /><br />--%>
                
                <br/><br/>
                    <asp:Label ID="LabelCreatedAsso" runat="server" Text=""></asp:Label><br />
                    <asp:Label ID="LabelCreatedByAsso" runat="server" Text=""></asp:Label><br />
                    
                <br /><br />
                <h5>Sub Associations</h5>
                <asp:BulletedList ID="BulletedListSubAssociations" runat="server"></asp:BulletedList>
                <br />
                <div class="btn-align-right">
                    <asp:Button ID="ButtonUpdateAsso" CssClass="btn-blue" runat="server" Text="Update" OnClick="ButtonUpdateAsso_OnClick"/>
                    <asp:Button ID="ButtonDeleteAsso" CssClass="btn-blue" runat="server" Text="Delete this Association" OnClick="ButtonDeleteAsso_OnClick"/>
                </div>
                <asp:Label ID="LabelUpdateAsso" runat="server" Text=""></asp:Label>
            </div>
        </asp:View>
    </asp:MultiView>
    <br />
    
    <asp:MultiView ID="MultiViewAssoCreate" runat="server" ActiveViewIndex="-1">
        <asp:View ID="ViewAssoCreate" runat="server">
            <div class="view-association-create">
                <h3>Create a new Association</h3><br />
                <span><b>Name: </b>
                    <asp:TextBox ID="TextBoxCreateAssoName" runat="server"></asp:TextBox>
                </span><br />
                <span><b>Community: </b>
                    <asp:DropDownList ID="DropDownListCommunityCreateAsso"
                        runat="server"
                        OnSelectedIndexChanged="DropDownListCommCreateAsso_OnSelectedIndexChanged"
                        EnableViewState="True"
                        AutoPostBack="True">
                    </asp:DropDownList>
                </span><br />
                <span><b>Parent Association: </b>
                    <asp:DropDownList ID="DropDownListCreateParAsso"
                        runat="server"
                        OnSelectedIndexChanged="DropDownListCreateParAsso_OnSelectedIndexChanged">
                    </asp:DropDownList>
                </span><br />
                <span><b>Association Type: </b>
                    <asp:DropDownList ID="DropDownListCreateAssoType"
                        runat="server"
                        OnSelectedIndexChanged="DropDownListCreateAssoType_OnSelectedIndexChanged">
                    </asp:DropDownList>
                </span><br />
                <br />
                <div class="btn-align-right">
                    <asp:Button ID="ButtonCreateAssoCancel" CssClass="btn-blue" runat="server" Text="Cancel" OnClick="ButtonCreateAssoCancel_OnClick" />
                    <asp:Button ID="ButtonCreateAsso" CssClass="btn-blue" runat="server" Text="Create" OnClick="ButtonCreateAsso_OnClick" />
                </div>
            </div>
        </asp:View>
    </asp:MultiView>
    
</div>
