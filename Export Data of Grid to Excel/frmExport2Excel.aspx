<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Caliber.Master" CodeBehind="frmExport2Excel.aspx.vb" Inherits="Caliber.frmExport2Excel" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="msg" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="body" runat="server">
     <asp:UpdatePanel ID="UpdatePanel5" runat="server">
         <ContentTemplate>
             <asp:GridView ID="gvData" runat="server" RowStyle-Wrap="false" CssClass="table-overflow"
                 AutoGenerateColumns="true" AutoGenerateSelectButton="True" ForeColor="Black" AllowPaging="True" AllowSorting="True" PageSize="10">
                 <EmptyDataTemplate>No Record Available</EmptyDataTemplate>
                  <PagerStyle CssClass="PagerCSS" />
             </asp:GridView>
         </ContentTemplate>
         <Triggers>
         </Triggers>
     </asp:UpdatePanel>

    <asp:Button ID="btnExport" runat="server" Text="Export To Excel" OnClick="ExportToExcel" />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
         <ContentTemplate>
             <asp:GridView ID="gvExcel" runat="server" RowStyle-Wrap="false" CssClass="table-overflow" style="visibility:hidden"
                 AutoGenerateColumns="true" ForeColor="Black" AllowSorting="True">  
                 <EmptyDataTemplate>No Record Available</EmptyDataTemplate>
                  <PagerStyle CssClass="PagerCSS" />
             </asp:GridView>
         </ContentTemplate>
         <Triggers>
         </Triggers>
     </asp:UpdatePanel>
</asp:Content>
