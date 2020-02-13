<%@ Page Title="Order Reports" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="frmOrderReports.aspx.cs" Inherits="BCCApp.frmOrderReports" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %> 
<%@ Register TagPrefix="ajax" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="msg" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server"> 
    <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Width="680px" Height="700px">
        <LocalReport ReportPath="Report1.rdlc">
            <DataSources>
                <rsweb:ReportDataSource Name="OrdersDet" DataSourceId="SqlDataSource1"></rsweb:ReportDataSource>
            </DataSources> 
        </LocalReport>
    </rsweb:ReportViewer>
    <asp:SqlDataSource runat="server" ID="SqlDataSource1" ConnectionString='<%$ ConnectionStrings:BookShopConnectionString %>' SelectCommand="GETOrderDetails" SelectCommandType="StoredProcedure">
        <SelectParameters> 
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:ObjectDataSource runat="server" SelectMethod="GetData" TypeName="BCCApp.BookShopDataSetTableAdapters.BooksTableAdapter" ID="ObjectDataSource1"></asp:ObjectDataSource>
</asp:Content>

