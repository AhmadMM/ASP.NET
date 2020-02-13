<%@ Page Title="Twister | Blog" Language="vb" AutoEventWireup="false" MasterPageFile="~/Guest.Master" CodeBehind="frmHome.aspx.vb" Inherits="Blogger.frmHome" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
 
            <div id="BloggerView">
                <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true">
                </asp:ScriptManager>
                <%= Global.Blogger.Global_asax.DesignBlogger(Me.Page)%>
            </div>
</asp:Content>
