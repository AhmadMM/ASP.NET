Imports Blogger.Global_asax
Public Class frmHome
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        PageName = "Blogger"
    End Sub
    <System.Web.Services.WebMethod()> _
    Public Shared Function GetID(ByVal ID As String) As String
        Global_asax.BloggerID = ID
    End Function
End Class