<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Feed.ascx.cs" Inherits="EventHandlingSystem.Feed" %>

<style>
    .feedbox {
        background-color: aliceblue;
        margin: 5px;
        padding: 20px;
        border: 2px solid blue;
    }
    .feedbox-eventdate {
        background-color: black;
        color: white;
        border: 1px dotted white;
        padding: 10px;
        text-decoration: wavy;
        font-size: 14px;
    }
    .feedbox-title {
        text-decoration: none;
        font-size: 16px;
        font-family: Verdana,Geneva,sans-serif;
        color: blue;
        cursor: pointer;
    }
    .feedbox-summary {
        font-size: 12px;
        font-family: "Palatino Linotype", "Book Antiqua", Palatino, serif
    }
</style>

<asp:Repeater ID="RepeaterFeed" runat="server">
   <ItemTemplate>
       <table> 
       <tr>
              <td>
                <asp:Label runat="server" ID="Label1" 
                    text='<%# Eval("Title") %>' />
              </td>
              <td>
                  <asp:Label runat="server" ID="Label2" 
                      text='<%# Eval("Summary") %>' />
              </td>
          </tr>
           </table>
    </ItemTemplate>
</asp:Repeater>

<%--<asp:LinkButton ID="lnkbtnEventDate" runat="server" CssClass="feedbox-eventdate"></asp:LinkButton>
        <br />
        <br />
        <asp:HyperLink ID="hlnkEventTitle" runat="server" CssClass="feedbox-title"></asp:HyperLink>
        <br />
        <br />
        <asp:Label ID="lbEventSummary" runat="server" CssClass="feedbox-summary" Text=""></asp:Label>
        <br />
        <br />
        <asp:Image ID="imgEventImage" runat="server" />--%>
    
        
   

