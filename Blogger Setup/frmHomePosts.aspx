<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Guest.Master" CodeBehind="frmHomePosts.aspx.vb" Inherits="Blogger.frmHomePosts" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
            <div id="PostsView">
                <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true">
                </asp:ScriptManager>
                <%= Global.Blogger.Global_asax.DesignPosts(Me.Page)%>
               
                </div> 
</asp:Content>
