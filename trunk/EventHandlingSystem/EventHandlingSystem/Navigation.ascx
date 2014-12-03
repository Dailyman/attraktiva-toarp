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
<div id="toggle-btn" class="arrow-left"></div>
<asp:TreeView ID="TreeViewNavigation" runat="server" OnSelectedNodeChanged="TreeViewNavigation_OnSelectedNodeChanged"></asp:TreeView>