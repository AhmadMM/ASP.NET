Imports System.Web.SessionState
Imports System.Data.SqlClient
Imports System.IO

Public Class Global_asax
    Inherits System.Web.HttpApplication
    Public Shared FirstName As String
    Public Shared LastName As String
    Public Shared PageName As String
    Public Shared Role As String
    Public Shared con As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
    Public Shared RowID As String
    Public Shared uID As String
    Public Shared BloggerCount As Integer
    Public Shared PostCount As Integer
    Public Shared GalleryCount As Integer
    Public Shared BloggerID As String
    Public Shared PostsID As String
    Public Shared HtmlBlogger As String = ""
    Public Shared HtmlPosts As String = ""
    Public Shared imgID As String = ""
    Public Shared Function Connection()
        Try
            Dim rootWebConfig As System.Configuration.Configuration
            rootWebConfig = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("/Web")
            Dim connString As System.Configuration.ConnectionStringSettings
            If (rootWebConfig.ConnectionStrings.ConnectionStrings.Count > 0) Then
                connString = rootWebConfig.ConnectionStrings.ConnectionStrings("ConnectionString")
                Return connString.ConnectionString
            End If

        Catch ex As Exception
            Blogger.My.Response.Write("<script language=javascript>alert('No Connection to Database');</script>")
        End Try
    End Function 
    Public Shared Function DesignBlogger(Pager As Page)
        Try
            Dim sqlCommand As SqlClient.SqlCommand = New SqlClient.SqlCommand
            Connect()
            If con.State = ConnectionState.Open Then

                If Role = "Admin" Then
                    sqlCommand.CommandText = "SELECT BID,Title, Subtitle, Type, CreatedDate, UserID, isBActive from Blogs"
                Else
                    sqlCommand.CommandText = "SELECT BID, Title, Subtitle, Type, CreatedDate, UserID, isBActive from Blogs where isBActive<>0"
                End If
                sqlCommand.Connection = con
                Dim da As New SqlClient.SqlDataAdapter(sqlCommand)
                Dim dt1 As New DataTable
                da.Fill(dt1)
                BloggerCount = dt1.Rows.Count
                For index As Integer = 0 To BloggerCount
                    If dt1.Rows(index)("isBActive") = 0 Then
                        If Role = "Admin" Then
                            HtmlBlogger = "<div style='background-color:#e6e6e6; border-style: ridge;'><h1 style='font-size:150%; text-align:center;'>This Blog is Disabled Since (Flag is OFF) User Can't See This !</h1><br/><h2 style='font: helvetica ; font-size: 200%; text-transform:capitalize; padding-top: 50px;line-height: 20%;margin-top:-1%;' ><a ID='" & "lnkBlogger" & index & "' runat='server' > " & dt1.Rows(index)("Title") & "</a >" & "</h2> <br /> <h3 style='font: helvetica; font-size:120%; margin-top:-1%; '>" & dt1.Rows(index)("Subtitle") & "</h3><br /><h4 style='font: helvetica; font-size:90%; margin-top:-1%;'>Creation Date:" & dt1.Rows(index)("CreatedDate") & "</h4> </div><hr>"
                        Else
                        End If
                    Else
                        HtmlBlogger = "<br/ ><br /><div style='Margin-left:2%;'><h2 style='font: helvetica ; font-size: 200%; text-transform:capitalize; ' ><a data-ID='" & dt1.Rows(index)("BID") & "' runat='server' href='frmHomePosts.aspx' onclick='showID(this)'>" & dt1.Rows(index)("Title") & "</a >" & "</h2> <br /> <h3 style='font: helvetica; font-size:120%; margin-top:-1%; '>" & dt1.Rows(index)("Subtitle") & "</h3><br /><h4 style='font: helvetica; font-size:90%; margin-top:-1%;'>Creation Date:" & dt1.Rows(index)("CreatedDate") & "</h4></div><br /> <hr><br />"
                    End If
                    Pager.DataBind()
                    Pager.Response.Write(HtmlBlogger)
                Next

            End If
        Catch ex As Exception
        End Try
    End Function
    Public Shared Function DesignPosts(Pager As Page)
        Try
            Dim sqlCommand As SqlClient.SqlCommand = New SqlClient.SqlCommand
            Connect() 
            If con.State = ConnectionState.Open Then

                If Role = "Admin" Then
                    sqlCommand.CommandText = " SELECT  PID, PTitle, PostDate, Text, BlogID, UserID, isPActive, hasImage, ImageID, AllowComments,imageAlt  FROM Posts where BlogID='" & BloggerID & "'"
                Else
                    sqlCommand.CommandText = "SELECT  PID, PTitle, PostDate, Text, BlogID, UserID, isPActive, hasImage, ImageID, AllowComments,imageAlt  FROM Posts where BlogID='" & BloggerID & "'"
                End If
                sqlCommand.Connection = con
                Dim da As New SqlClient.SqlDataAdapter(sqlCommand)
                Dim dt1 As New DataTable
                da.Fill(dt1)
                PostCount = dt1.Rows.Count 
                For index As Integer = 0 To PostCount


                    If dt1.Rows(index)("isPActive") = 0 Then
                        If Role = "Admin" Then
                            If dt1.Rows(index)("hasImage") = 0 Then
                                HtmlPosts = "<div style='background-color:#e6e6e6; border-style: ridge;'><h1 style='font-size:150%; text-align:center;'>This Post is Disabled Since (Flag is OFF) User Can't See This !</h1><br/><h2 style='font: helvetica ; font-size: 200%; text-transform:capitalize; padding-top: 50px;line-height: 20%;margin-top:-1%;' ><p ID='" & dt1.Rows(index)("PID") & "' runat='server'> " & dt1.Rows(index)("PTitle") & "</p>" & "</h2> <br /> <Table ><tr><td style='width:60%;'><h3 style='font: helvetica; font-size:120%; margin-top:-1%; '>" & dt1.Rows(index)("Text") & "</h3><br /><h4 style='font: helvetica; font-size:90%; margin-top:-1%;'>Post Date:" & dt1.Rows(index)("PostDate") & "</h4></div><br /> <br /></td><td style='width:40%;'><hr>"

                            Else
                                HtmlPosts = "<div style='background-color:#e6e6e6; border-style: ridge;'><h1 style='font-size:150%; text-align:center;'>This Post is Disabled Since (Flag is OFF) User Can't See This !</h1><br/><h2 style='font: helvetica ; font-size: 200%; text-transform:capitalize; padding-top: 50px;line-height: 20%;margin-top:-1%;' ><p ID='" & dt1.Rows(index)("PID") & "' runat='server'> " & dt1.Rows(index)("PTitle") & "</p>" & "</h2> <br /> <Table ><tr><td style='width:60%;'><h3 style='font: helvetica; font-size:120%; margin-top:-1%; '>" & dt1.Rows(index)("Text") & "</h3><br /><h4 style='font: helvetica; font-size:90%; margin-top:-1%;'>Post Date:" & dt1.Rows(index)("PostDate") & "</h4></div><br /> <br /></td><td style='width:40%;'><asp:image ID='img" & dt1.Rows(index)("PID") & "' runat='server' ImageUrl='" & "~/ShowImage.ashx?id=" & dt1.Rows(index)("ImageID") & "' style='widht:500px; height:500px;' title='" & dt1.Rows(index)("imageAlt") & "'/></td></tr></table><hr>"
                            End If
                        Else
                        End If
                    Else
                        If dt1.Rows(index)("hasImage") = 0 Then
                            HtmlPosts = "<br/ ><br /><div style='Margin-left:2%;'><h2 style='font: helvetica ; font-size: 200%; text-transform:capitalize; ' ><p ID='" & dt1.Rows(index)("PID") & "' runat='server'> " & dt1.Rows(index)("PTitle") & "</p>" & "</h2> <br /> <Table ><tr><td style='width:60%;'><h3 style='font: helvetica; font-size:120%; margin-top:-1%; '>" & dt1.Rows(index)("Text") & "</h3><br /><h4 style='font: helvetica; font-size:90%; margin-top:-1%;'>Post Date:" & dt1.Rows(index)("PostDate") & "</h4></div><br /> <br /></td></tr></table><hr>"

                        Else
                            HtmlPosts = "<br/ ><br /><div style='Margin-left:2%;'><h2 style='font: helvetica ; font-size: 200%; text-transform:capitalize; ' ><p ID='" & dt1.Rows(index)("PID") & "' runat='server'> " & dt1.Rows(index)("PTitle") & "</p>" & "</h2> <br /> <div><p style='float: left;'><h3 style='font: helvetica; font-size:120%; margin-top:-1%; '>" & dt1.Rows(index)("Text") & "</h3><br /><h4 style='font: helvetica; font-size:90%; margin-top:-1%;'>Post Date:" & dt1.Rows(index)("PostDate") & "</h4></div><br /> <br /></p><p style:'float: right;' align='center;'><img ID='img" & dt1.Rows(index)("PID") & "' runat='server' src='ShowImage.ashx?id=" & dt1.Rows(index)("ImageID") & "' style='width:50%; height:50%;'title='" & dt1.Rows(index)("imageAlt") & "' /></div><hr>"

                        End If
                    End If
                    Pager.DataBind()
                    Pager.Response.Write(HtmlPosts)
                Next

            End If
        Catch ex As Exception
        End Try
    End Function
    Public Shared Function DesignGallery(Pager As Page)
        Try
            Dim sqlCommand As SqlClient.SqlCommand = New SqlClient.SqlCommand
            Connect()
            If con.State = ConnectionState.Open Then
 
                sqlCommand.CommandText = "SELECT ID,ImageSize,image ,Name FROM Images"

                sqlCommand.Connection = con
                Dim da As New SqlClient.SqlDataAdapter(sqlCommand)
                Dim dt1 As New DataTable
                da.Fill(dt1)
                GalleryCount = dt1.Rows.Count
                For index As Integer = 0 To GalleryCount 
                    HtmlPosts = "<fieldset> <legend>" & dt1.Rows(index)("Name") & "</legend><img ID='img" & dt1.Rows(index)("ID") & "' runat='server' src='ShowImage.ashx?id='" & dt1.Rows(index)("ID") & "' style='width:50px; height:50px;' /> </fieldset>"
                    Pager.DataBind()
                    Pager.Response.Write(HtmlPosts)
                Next

            End If
        Catch ex As Exception
        End Try
    End Function
    Public Shared Sub Connect()
        If con.State = ConnectionState.Open Then
            con.Close()
        End If
        If con.State = ConnectionState.Closed Then
            con.Open()
        End If
    End Sub
    Public Shared Sub LoadTable(CommandText As String, GridV As GridView)
        Try
            Connect()
            Dim con As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlCommand As SqlClient.SqlCommand = New SqlClient.SqlCommand

            sqlCommand.CommandText = CommandText
            sqlCommand.Connection = con
            Dim dt As New DataTable
            Dim da As New SqlClient.SqlDataAdapter(sqlCommand)
            da.Fill(dt)
            GridV.DataSource = dt
            GridV.DataBind()
            HideID(GridV)

        Catch ex As Exception

        End Try

    End Sub
    Public Shared Sub HideID(GridV As GridView)
        Try
            If GridV.Columns.Count > 0 Then
                GridV.Columns(1).Visible = False
            Else
                GridV.HeaderRow.Cells(1).Visible = False
                For Each gvr In GridV.Rows
                    gvr.Cells(1).Visible = False
                Next
            End If
        Catch ex As Exception
            Exit Sub
        End Try
    End Sub
    Public Shared Sub FillDropDown(ddl As DropDownList, CommandText As String, CommandName As String, CommandiD As String)
        Try
            Dim cmd As New SqlCommand(CommandText, con)
            Dim reader As SqlDataReader
            ddl.Items.Clear()
            Try
                Connect()
                reader = cmd.ExecuteReader()
                While reader.Read()
                    Dim newItem As New ListItem()
                    newItem.Text = reader(CommandName).ToString()
                    newItem.Value = reader(CommandiD).ToString()
                    ddl.Items.Add(newItem)
                End While
                reader.Close()
                ddl.Items.Insert(0, New ListItem("---------------", "NaN"))
            Catch err As Exception
            Finally
                con.Close()
            End Try

        Catch ex As Exception
        End Try
    End Sub
    Public Shared Function FindData(CommandString As String)
        Try
            Dim sqlCommand As SqlClient.SqlCommand = New SqlClient.SqlCommand
            con.Open()
            sqlCommand.CommandText = CommandString
            sqlCommand.Connection = con
            Return sqlCommand.ExecuteScalar()
            con.Close()
        Catch ex As Exception
        End Try
    End Function
    Public Shared Sub Search(CommandText As String, GridV As GridView)
        Try
            Dim ds As New DataSet

            Dim sqlCommand As SqlClient.SqlCommand = New SqlClient.SqlCommand
            sqlCommand.CommandText = CommandText

            sqlCommand.Connection = con
            Connection()
            Dim da As New SqlDataAdapter(sqlCommand)
            da.Fill(ds)

            GridV.DataSource = ds

            GridV.DataBind()
            Dim ds2 As DataSet = GetData(CommandText)
            ''HIDE ATTENDANCE ID'
            If (ds2.Tables.Count > 0) Then
                GridV.DataSource = ds
                GridV.DataBind()
                HideID(GridV)
            End If
            'If ds2.Tables.Count Then

        Catch ex As Exception

        End Try

    End Sub
    Public Shared Function GetData(ByVal queryString As String) As DataSet
        Dim ds As New DataSet()
        Try
            Dim adapter As New SqlDataAdapter(queryString, con)
            adapter.Fill(ds)
        Catch ex As Exception
        End Try
        Return ds
    End Function
    Public Shared Sub Upload(img As FileUpload, Pri As Integer)
        Dim Count As Integer = 0
        Dim Num1 As Integer = 0
        Dim Num2 As Integer = 0

        If Not img.HasFile Then

        Else
            If Pri = 1 Then
                Num1 = GetNum(Count)
                Dim length As Integer = img.PostedFile.ContentLength
                Dim pic As Byte() = New Byte(length - 1) {}
                img.PostedFile.InputStream.Read(pic, 0, length)
                Connection()
                'calling connection method
                'inserting uploaded image query
                Dim com As New SqlCommand("insert into Images " + "(image,ID) values (@image,@ID)", con)
                com.Parameters.AddWithValue("@image", pic)
                com.Parameters.AddWithValue("@ID", Num1)
                com.ExecuteNonQuery()
            End If
            If Pri = 2 Then
                Num2 = GetNum(Count)
                Dim length As Integer = img.PostedFile.ContentLength
                Dim pic As Byte() = New Byte(length - 1) {}
                img.PostedFile.InputStream.Read(pic, 0, length)
                Connection()
                'calling connection method
                'inserting uploaded image query
                Dim com As New SqlCommand("insert into Image " + "(image,ID) values (@image,@ID)", con)
                com.Parameters.AddWithValue("@image", pic)
                com.Parameters.AddWithValue("@ID", Num2)
                com.ExecuteNonQuery()
            End If
        End If
    End Sub
    Public Shared Function GetNum(Counter As Integer) As String
        Try

            Dim sqlCommand As SqlClient.SqlCommand = New SqlClient.SqlCommand
            Connect()
            If con.State = ConnectionState.Open Then

                sqlCommand.CommandText = "SELECT TOP 1 ID FROM Image ORDER by  ID DESC;"
                sqlCommand.Connection = con
                If sqlCommand.ExecuteScalar() Is System.DBNull.Value Then
                    Counter = 1
                Else : Counter = sqlCommand.ExecuteScalar() + 1
                End If

            End If
            Return Counter
        Catch ex As Exception
        End Try
    End Function
    Public Shared Function GetimgID(img As FileUpload)
        Dim Counter As Integer
        If Not img.HasFile Then
            Return 0
        Else : Return GetNum(Counter)
        End If
    End Function
    Public Shared Function GetDropDownName(CommandText As String, Dll As DropDownList)
        Dim sqlCommand As SqlClient.SqlCommand = New SqlClient.SqlCommand
        Connection()
        Connect()
        sqlCommand.CommandText = CommandText
        sqlCommand.Connection = con
        Dim Value As String = ""
        Value = sqlCommand.ExecuteScalar()
        Dll.Items.Add(New ListItem(Value, "0", True))
        Return Value
    End Function
    Sub Application_Start(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires when the application is started
    End Sub
     Sub Session_Start(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires when the session is started
    End Sub

    Sub Application_BeginRequest(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires at the beginning of each request
    End Sub

    Sub Application_AuthenticateRequest(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires upon attempting to authenticate the use
    End Sub

    Sub Application_Error(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires when an error occurs
    End Sub

    Sub Session_End(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires when the session ends
    End Sub

    Sub Application_End(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires when the application ends
    End Sub

End Class