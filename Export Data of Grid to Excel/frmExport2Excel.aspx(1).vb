
Imports System.IO
Imports System.Data
Imports System.Drawing
Imports System.Data.SqlClient
Imports System.Configuration 
Imports Caliber.Global_asax
Public Class frmExport2Excel
    Inherits System.Web.UI.Page
    Public CommandItems As String = "SELECT   Items.BarCode as [Barcode], Items.ItemDesc as [Item Description],Department.DptName as [Department], Items.Note  FROM  Items INNER JOIN Department ON Items.DptID = Department.DptID"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (Not IsPostBack) Then
            LoadTableWithoutHIDEID(CommandItems, gvData)
            LoadTableWithoutHIDEID(CommandItems, gvExcel)
            PageName = "Export2Excel"
        End If
    End Sub
    Protected Sub OnPageIndexChanging(sender As Object, e As GridViewPageEventArgs)
        gvData.PageIndex = e.NewPageIndex
    End Sub

    Protected Sub ExportToExcel(sender As Object, e As EventArgs) Handles btnExport.Click
        Response.Clear()
        Response.Buffer = True
        Response.AddHeader("content-disposition", "attachment;filename=Tw" & PageName & ".xlsx")
        Response.Charset = ""
        Response.ContentType = "application/vnd.ms-excel"
        Using sw As New StringWriter()
            Dim hw As New HtmlTextWriter(sw)

            'To Export all pages
            gvExcel.AllowPaging = False

            gvExcel.HeaderRow.BackColor = Color.White
            gvExcel.AutoGenerateSelectButton = False

            For Each cell As TableCell In gvExcel.HeaderRow.Cells
                cell.BackColor = gvExcel.HeaderStyle.BackColor
            Next
            For Each row As GridViewRow In gvExcel.Rows
                row.BackColor = Color.White
                For Each cell As TableCell In row.Cells
                    If row.RowIndex Mod 2 = 0 Then
                        cell.BackColor = gvExcel.AlternatingRowStyle.BackColor
                    Else
                        cell.BackColor = gvExcel.RowStyle.BackColor
                    End If
                    cell.CssClass = "textmode"
                Next
            Next
            gvExcel.RenderControl(hw) 
            Response.Output.Write(sw.ToString())
            Response.Flush()
            Response.[End]()
        End Using
    End Sub

    Public Overrides Sub VerifyRenderingInServerForm(control As Control)
        ' Verifies that the control is rendered
    End Sub 
End Class