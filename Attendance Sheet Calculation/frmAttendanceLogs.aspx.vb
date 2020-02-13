Imports System.IO
Imports System.Data.OleDb
Imports Caliber.Global_asax
Imports System.Data.SqlClient
Imports System.Globalization

Public Class frmAttendanceLogs
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Status = String.Empty
        If Not IsPostBack Then
               Global.Caliber.Global_asax.PageName = "Attendance Logs"
            FillDropDown(ddlProject, "SELECT distinct STUFF((SELECT ', ' + CAST( PrjID AS VARCHAR(10)) [text()] FROM GProject WHERE PrjName = t. PrjName FOR XML PATH(''), TYPE) .value('.','NVARCHAR(MAX)'),1,2,' ') PrjID,T.PrjName FROM GProject t", "PrjName", "PrjID")
            SearchCommandLine = " SELECT PrjName,PrjID from GProject WHERE PrjName LIKE '%Keys%'"
            SearchGridView = GridView1

        End If
    End Sub
    Protected Sub btnUpload_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try

            If FileUpload1.HasFile Then
                Dim FileName As String = Path.GetFileName(FileUpload1.PostedFile.FileName)
                Dim Extension As String = Path.GetExtension(FileUpload1.PostedFile.FileName)
                Dim FolderPath As String = ConfigurationManager.AppSettings("FolderPath")

                Dim FilePath As String = Server.MapPath(FolderPath + FileName)
                FileUpload1.SaveAs(FilePath)
                Import_To_Grid(FilePath, Extension, rbHDR.SelectedItem.Text)
            End If

        Catch ex As Exception
            Status = "Danger,Error,Uploading File Error"
            DesignWarningColor(DivStatus, StatusType, lblStrongStatus, lblStatus)
        End Try
    End Sub
    Private Sub Import_To_Grid(ByVal FilePath As String, ByVal Extension As String, ByVal isHDR As String)
        Try
            Dim conStr As String = ""
            Select Case Extension
                Case ".xls"
                    'Excel 97-03
                    conStr = ConfigurationManager.ConnectionStrings("Excel03ConString") _
                               .ConnectionString
                    Exit Select
                Case ".xlsx"
                    'Excel 07
                    conStr = ConfigurationManager.ConnectionStrings("Excel07ConString") _
                              .ConnectionString
                    Exit Select
                Case ".txt"
                    conStr = ConfigurationManager.ConnectionStrings("Conn1") _
                         .ConnectionString
                    Exit Select

            End Select

            conStr = String.Format(conStr, FilePath, isHDR)

            Dim connExcel As New OleDbConnection(conStr)
            Dim cmdExcel As New OleDbCommand()
            Dim oda As New OleDbDataAdapter()
            Dim dt As New DataTable()

            cmdExcel.Connection = connExcel

            'Get the name of First Sheet
            connExcel.Open()
            Dim dtExcelSchema As DataTable
            dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, Nothing)
            Dim SheetName As String = dtExcelSchema.Rows(0)("TABLE_NAME").ToString()
            connExcel.Close()

            'Read Data from First Sheet
            connExcel.Open()
            cmdExcel.CommandText = "SELECT * From [" & SheetName & "]"
            oda.SelectCommand = cmdExcel
            oda.Fill(dt)
            connExcel.Close()

            'Bind Data to GridView
            GridView1.Caption = Path.GetFileName(FilePath)
            GridView1.DataSource = dt
            GridView1.DataBind()







            'Dim table As New DataTable()
            'table.Columns.Add("No")
            'table.Columns.Add("Mchn")
            'table.Columns.Add("EnNo")
            'table.Columns.Add("Name")
            'table.Columns.Add("DateTime") 
            'Using sr As New StreamReader(FilePath)
            '    While Not sr.EndOfStream
            '        Dim parts As String() = sr.ReadLine().Split(","c)
            '        table.Rows.Add(parts(0))
            '    End While
            'End Using
            'GridView1.DataSource = table
            'GridView1.DataBind()

        Catch ex As Exception
            Status = "Danger,Error,Problem in Reading the Sheet"
            DesignWarningColor(DivStatus, StatusType, lblStrongStatus, lblStatus)
        End Try

    End Sub
    Protected Sub PageIndexChanging(ByVal sender As Object, ByVal e As GridViewPageEventArgs)
        Dim FolderPath As String = ConfigurationManager.AppSettings("FolderPath")
        Dim FileName As String = GridView1.Caption
        Dim Extension As String = Path.GetExtension(FileName)
        Dim FilePath As String = Server.MapPath(FolderPath + FileName)

        Import_To_Grid(FilePath, Extension, rbHDR.SelectedItem.Text)
        GridView1.PageIndex = e.NewPageIndex
        GridView1.DataBind()
    End Sub
    Sub ApplyChanges()
        Try
            Dim ArryofIDsEmp = ""
            Dim Dates = ""
            '*********************************************************Section #1 ****************************************************************************************************'
            For index As Integer = 0 To GridView1.Rows.Count - 1
                ArryofIDsEmp += GridView1.Rows(index).Cells(2).Text & ","
                Dates += GridView1.Rows(index).Cells(4).Text & ","
            Next
            If ArryofIDsEmp.Count <> 0 Then
                ArryofIDsEmp = ArryofIDsEmp.Remove(ArryofIDsEmp.Length - 1)
                ArryofIDsEmp = RemoveDuplicates(ArryofIDsEmp)
            Else
                Status = "Warning,Warning,IDs are Empty"
                DesignWarningColor(DivStatus, StatusType, lblStrongStatus, lblStatus)
                Exit Sub
            End If
            If Dates.Count <> 0 Then
                Dates = Dates.Remove(Dates.Length - 1)
                Dates = RemoveDuplicates(SplitToArray(Dates))
            Else
                Status = "Warning,Warning,Dates are Empty"
                DesignWarningColor(DivStatus, StatusType, lblStrongStatus, lblStatus)
                Exit Sub
            End If
            '************************************************************************************************************************************************************************'
            '++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'
            '*********************************************************Section #2 ****************************************************************************************************'
            Dim IDz As String() = SplitToArray(ArryofIDsEmp)
            Dim Datez As String() = SplitToArray(Dates)
            Dim Timez As String()
            Dim Times = ""
            For Each Dts As String In Datez
                For index As Integer = 0 To GridView1.Rows.Count - 1
                    For Each str As String In IDz
                        If (str = GridView1.Rows(index).Cells(2).Text And Dts = GridView1.Rows(index).Cells(4).Text) Then
                            Times += GridView1.Rows(index).Cells(5).Text & ","
                        End If
                    Next
                Next
            Next
            If Times.Count <> 0 Then
                Times = RemoveDuplicates(SplitToArray(Times))
            Else
                Status = "Warning,Warning,Times are Empty"
                DesignWarningColor(DivStatus, StatusType, lblStrongStatus, lblStatus)
                Exit Sub
            End If
            If Times.Count <> 0 Then
                Times = Times.Remove(Times.Length - 1)
                Timez = SplitToArray(Times)
            Else
                Status = "Warning,Warning,Times are Empty"
                DesignWarningColor(DivStatus, StatusType, lblStrongStatus, lblStatus)
                Exit Sub
            End If
            '************************************************************************************************************************************************************************'
            FillItGrid(GridView1, IDz, Datez, Timez)
        Catch ex As Exception
            Status = "Danger,Error,Problem in Applying changes"
            DesignWarningColor(DivStatus, StatusType, lblStrongStatus, lblStatus)
        End Try
    End Sub
    Public Shared Function RemoveDuplicates(input As String) As String
        Return New String(input.ToCharArray().Distinct().ToArray())
    End Function

    Private Function SplitToArray(Value As String)
        Dim delimiterChars As Char() = {","c, ControlChars.Tab}
        Value = Value.Replace("12/30/1899", String.Empty)
        Dim words As String() = Value.Split(delimiterChars)
        Return words
    End Function
    Public Function RemoveDuplicates(inputArray As String())
        Dim Data As String = ""
        Dim distinctArray As New List(Of String)()
        For Each element As String In inputArray
            If Not distinctArray.Contains(element) Then
                distinctArray.Add(element)
            End If
        Next
        For index = 0 To distinctArray.Count - 1
            Data += distinctArray(index) & ","
        Next
        If Data.Count <> 0 Then
            Data = Data.Remove(Data.Length - 1)
        End If

        Return Data
    End Function
    Public Sub FillItGrid(Gridviews As GridView, ids As String(), dates As String(), times As String())
        Try
            Dim Count = ids.Count - 1
            Dim Emps As String() = New String(Count) {}
            For index = 0 To ids.Count - 1
                Connect()
                Dim sqlCommand1 As SqlClient.SqlCommand = New SqlClient.SqlCommand
                sqlCommand1.CommandText = "SELECT [FirstName] +' '+[LastName] as Employee FROM Person WHERE [PrsID] ='E" & ids(index) & "'"
                sqlCommand1.Connection = con
                Emps(index) = sqlCommand1.ExecuteScalar
            Next
            Dim dt As New DataTable()
            dt.Columns.Add("Date")
            dt.Columns.Add("ID")
            dt.Columns.Add("Employee Name")
            dt.Columns.Add("IN")
            dt.Columns.Add("OUT")
            dt.Columns.Add("Working Hours")

            For i = 0 To dates.Count - 1
                dt.Rows.Add()
                dt.Rows(i)("Date") = dates(i)

                For i1 = 0 To ids.Count - 1
                    dt.Rows.Add()
                    dt.Rows(i1)("ID") = ids(i1)
                Next
                For i1 = 0 To Emps.Count - 1
                    dt.Rows.Add()
                    dt.Rows(i1)("Employee Name") = Emps(i1)
                    dt.Rows(i1)("Date") = dates(i)
                Next

            Next
            For i1 = 0 To times.Count - 1 Step +2
                If (i1 + 1 = times.Count) Then
                Else
                    If (i1 - 1 < 0) Then
                        dt.Rows.Add()
                        dt.Rows(i1)("IN") = times(i1)
                        dt.Rows(i1)("OUT") = times(i1 + 1)
                        Dim h1 As String = times(i1)
                        Dim h2 As String = times(i1 + 1)
                        h1 = (DateTime.Parse(times(i1)).Hour).ToString() & "." & (DateTime.Parse(times(i1)).Minute).ToString()
                        h2 = (DateTime.Parse(times(i1 + 1)).Hour).ToString() & "." & (DateTime.Parse(times(i1 + 1)).Minute).ToString()
                        dt.Rows(i1)("Working Hours") = (Double.Parse(h2) - Double.Parse(h1)).ToString()
                    Else
                        dt.Rows.Add()
                        dt.Rows(i1 - 1)("IN") = times(i1)
                        dt.Rows(i1 - 1)("OUT") = times(i1 + 1)
                        Dim h1 As String = times(i1)
                        Dim h2 As String = times(i1 + 1)
                        h1 = (DateTime.Parse(times(i1)).Hour).ToString() & "." & (DateTime.Parse(times(i1)).Minute).ToString()
                        h2 = (DateTime.Parse(times(i1 + 1)).Hour).ToString() & "." & (DateTime.Parse(times(i1 + 1)).Minute).ToString()
                        dt.Rows(i1 - 1)("Working Hours") = (Double.Parse(h2) - Double.Parse(h1)).ToString()
                    End If
                End If
            Next
            Gridviews.DataSource = dt
            Gridviews.DataBind()
        Catch ex As Exception
            Status = "Danger,Error,Filling Grid Error Occured"
            DesignWarningColor(DivStatus, StatusType, lblStrongStatus, lblStatus)
        End Try
    End Sub
    Sub Cancel_Func()
        Response.Redirect(Request.Url.AbsoluteUri)
    End Sub

    Sub Addition_Func()
        Try
            Dim Comnd As SqlClient.SqlCommand = New SqlClient.SqlCommand
            Comnd.Connection = con
            Comnd.CommandText = "SELECT PrjID FROM GProject where GProject.DptID='" & ddlDepartment.SelectedValue & "' and PrjName='" & ddlProject.SelectedItem.Text & "'"
            Dim PrjID = Comnd.ExecuteScalar
            Dim sqlCommand1 As SqlClient.SqlCommand = New SqlClient.SqlCommand
            sqlCommand1.CommandText = " SELECT COUNT(fromDate) from [AttLogs] WHERE fromDate='" & txtFromDate.Text & "'"
            sqlCommand1.Connection = con
            Dim Dt = sqlCommand1.ExecuteScalar
            If (Dt > 0) Then
                Status = "Warning,Warning,Duplicates Occured"
                DesignWarningColor(DivStatus, StatusType, lblStrongStatus, lblStatus)
                GridView1.DataSource = Nothing
                GridView1.DataBind()
                Exit Sub
            End If
            For Each row As GridViewRow In GridView1.Rows
                If (ddlProject.SelectedValue = "NaN" Or ddlProject.SelectedValue = String.Empty Or row.Cells(0).Text = String.Empty Or row.Cells(1).Text = String.Empty Or row.Cells(2).Text = String.Empty Or row.Cells(3).Text = String.Empty Or row.Cells(4).Text = String.Empty Or row.Cells(0).Text = "&nbsp;" Or row.Cells(1).Text = "&nbsp;" Or row.Cells(2).Text = "&nbsp;" Or row.Cells(3).Text = "&nbsp;" Or row.Cells(4).Text = "&nbsp;" Or ddlDepartment.SelectedValue = "NaN" Or ddlDepartment.SelectedValue = String.Empty Or txtFromDate.Text = String.Empty Or txtToDate.Text = String.Empty) Then
                Else
                    Dim Dates As String = row.Cells(0).Text
                    Dates = DateTime.ParseExact(Dates, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd 12:00:00 ", CultureInfo.InvariantCulture)
                    Connect()
                    Dim PrsID As String = row.Cells(1).Text
                    Dim INZ As String = row.Cells(3).Text
                    Dim OUTZ As String = row.Cells(4).Text
                    Dim WH As String = row.Cells(5).Text
                    Connect()
                    Dim sqlCommand As SqlClient.SqlCommand = New SqlClient.SqlCommand
                    sqlCommand.Connection = con
                    sqlCommand.CommandText = "Insert into [AttLogs] (FromDate,ToDate,PrsID,ProjectID,fromTime,toTime,Type,CalculatedHours) values (@FromDate,@ToDate,@PrsID,@ProjectID,@fromTime,@toTime,@Type,@CalculatedHours)"
                    sqlCommand.Parameters.Clear()
                    sqlCommand.Parameters.Add(New SqlParameter("@FromDate", txtFromDate.Text))
                    sqlCommand.Parameters.Add(New SqlParameter("@ToDate", txtToDate.Text))
                    sqlCommand.Parameters.Add(New SqlParameter("@PrsID", "E" & PrsID))
                    sqlCommand.Parameters.Add(New SqlParameter("@ProjectID", PrjID))
                    sqlCommand.Parameters.Add(New SqlParameter("@fromTime", INZ))
                    sqlCommand.Parameters.Add(New SqlParameter("@toTime", OUTZ))
                    sqlCommand.Parameters.Add(New SqlParameter("@Type", "Employee"))
                    sqlCommand.Parameters.Add(New SqlParameter("@CalculatedHours", WH))
                    sqlCommand.ExecuteNonQuery()
                End If
            Next
            GridView1.DataBind()
            ddlProject.SelectedIndex = 0
            GridView1.DataSource = Nothing
            GridView1.DataBind()
            txtFromDate.TextMode = TextBoxMode.Search
            txtFromDate.Text = -String.Empty
            txtFromDate.TextMode = TextBoxMode.Date

            txtToDate.TextMode = TextBoxMode.Search
            txtToDate.Text = -String.Empty
            txtToDate.TextMode = TextBoxMode.Date
            ddlDepartment.Items.Clear()
            FillDropDown(ddlProject, "SELECT distinct STUFF((SELECT ', ' + CAST( PrjID AS VARCHAR(10)) [text()] FROM GProject WHERE PrjName = t. PrjName FOR XML PATH(''), TYPE) .value('.','NVARCHAR(MAX)'),1,2,' ') PrjID,T.PrjName FROM GProject t", "PrjName", "PrjID")
            Status = "Success,Success,Addition Succeeded"
            DesignWarningColor(DivStatus, StatusType, lblStrongStatus, lblStatus)
        Catch ex As Exception
            Status = "Danger,Error,Addition Failure"
            DesignWarningColor(DivStatus, StatusType, lblStrongStatus, lblStatus)
        End Try
        Status = "Success,Success,Addition Succeeded"
        DesignWarningColor(DivStatus, StatusType, lblStrongStatus, lblStatus)
    End Sub
    Protected Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged
        Try
            If String.IsNullOrWhiteSpace(txtSearch.Text) Then
                Exit Sub
            End If
            SearchCommandLine = SearchCommandLine.Replace("Keys", txtSearch.Text)
            Dim ds As New DataSet
            GridView1.DataSource = Nothing
            GridView1.DataBind()

            Dim sqlCommand As SqlClient.SqlCommand = New SqlClient.SqlCommand
            sqlCommand.CommandText = SearchCommandLine

            sqlCommand.Connection = con
            Connection()
            Dim da As New SqlDataAdapter(sqlCommand)
            da.Fill(ds)

            GridView1.DataSource = ds

            GridView1.DataBind()
            Dim ds2 As DataSet = GetData(SearchCommandLine)
            ''HIDE ATTENDANCE ID'
            If (ds2.Tables.Count > 0) Then
                GridView1.DataSource = ds
                GridView1.DataBind()
                HideID(GridView1)
            End If
            btnApplyChanges.Focus()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub ddlProject_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlProject.SelectedIndexChanged
        Try
            Dim arrys As String() = Split(ddlProject.SelectedValue, ",")
            For Index = 0 To arrys.Count - 1
                Dim cmd As New SqlCommand("SELECT Department.DptID ,Department.DptName FROM GProject INNER JOIN Department ON GProject.DptID = Department.DptID WHERE GProject.PrjID='" & arrys(Index) & "'", con)
                Dim reader As SqlDataReader
                Try
                    Connect()
                    Dim c = cmd.CommandText
                    reader = cmd.ExecuteReader()
                    While reader.Read()
                        Dim newItem As New ListItem()
                        newItem.Text = reader("DptName").ToString()
                        newItem.Value = reader("DptID").ToString()
                        ddlDepartment.Items.Add(newItem)
                    End While
                    reader.Close()
                Catch err As Exception
                Finally
                    con.Close()
                End Try
            Next
            RemoveDuplicateItems(ddlDepartment)
        Catch ex As Exception
        End Try

    End Sub
End Class