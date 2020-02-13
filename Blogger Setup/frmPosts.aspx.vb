Imports Blogger.Global_asax
Imports System.Data.SqlClient
Imports System.IO
Public Class frmPosts
    Inherits System.Web.UI.Page
    Dim cbPC As String = "0"
    Dim PostActive As String = "0"
    Dim HasImg As String = "0"
    Dim Comments As String = "0"
    Dim CommandTextPost = "SELECT Posts.PID as _, Posts.PTitle as Title,Posts.PostDate as Posted_Date, Posts.isPActive as Active ,Posts.AllowComments , Users.FName+''+ Users.LName as Creator_Name,Posts.hasImage,Posts.imageAlt as Title,Posts.Text as Blogger FROM Blogs INNER JOIN Posts ON Blogs.BID = Posts.BlogID INNER JOIN Users ON Blogs.UserID = Users.UID AND Posts.UserID = Users.UID"
    Dim Text As String = ""
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Role = "" Then
                Response.Redirect("/error.html")
            ElseIf Role = "Admin" Then
                Connection()
                FillDropDown(ddlBlogger, "select BID,Title from blogs", "Title", "BID")
                PageName = "Post Editor"
                txtPD.Text = Now.Date
                LoadTable(CommandTextPost, gvPosts)
                imgDiv.Visible = True
                '--------------------------------------------------------------------------'
            Else
                Response.Redirect("/error.html")
            End If
        End If
    End Sub
    Protected Sub FetchImage(CommandText)

        Dim bytes As Byte() = DirectCast(CommandText, Byte())
        Dim base64String As String = Convert.ToBase64String(bytes, 0, bytes.Length)
        imgLoad.ImageUrl = Convert.ToString("data:image/png;base64,") & base64String

    End Sub

    Protected Sub btnADD_Click(sender As Object, e As ImageClickEventArgs) Handles btnADD.Click
        Text = Editor1.Content.Replace(Environment.NewLine, "<br />")
        If (cbisActive.Checked = True) Then
            PostActive = "1"
        Else
            PostActive = "0"
        End If
        If (cbisActive.Checked = True) Then
            PostActive = "1"
        Else
            PostActive = "0"
        End If
        If (cbHasImage.Checked = True) Then
            HasImg = "1"
        Else
            HasImg = "0"
        End If
        If (cbAllowComments.Checked = True) Then
            Comments = "1"
        Else
            Comments = "0"
        End If
        If (btnADD.ImageUrl = "~/img/Buttons/Save.png") Then


            If (txtTitle.Text = "" Or Editor1.Content = "" Or txtPD.Text = "") Then
                ScriptManager.RegisterClientScriptBlock(Page, GetType(Page), "ClientScript", "alert('A Field is Missing')", True)
                If HasImg = 1 Then
                    If imgUp.FileName = "" Then
                        ScriptManager.RegisterClientScriptBlock(Page, GetType(Page), "ClientScript", "alert('Image Uploader is Empty Please Select an Image')", True)
                    End If
                End If
            Else
                Try

                    Dim sqlCommand As SqlClient.SqlCommand = New SqlClient.SqlCommand
                    sqlCommand.CommandText = "INSERT INTO [Posts] (PTitle,Text,PostDate,BlogID,UserID,isPActive,hasImage,ImageID, AllowComments,imageAlt) VALUES(@PTitle,@Text,@PostDate,@BlogID,@UserID,@isPActive,@hasImage,@ImageID ,@AllowComments,@imageAlt) "
                    sqlCommand.Connection = con
                    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                    sqlCommand.Parameters.Add(New SqlParameter("@PTitle", txtTitle.Text))
                    sqlCommand.Parameters.Add(New SqlParameter("@Text", Text))
                    sqlCommand.Parameters.Add(New SqlParameter("@PostDate", txtPD.Text))
                    sqlCommand.Parameters.Add(New SqlParameter("@BlogID", ddlBlogger.SelectedItem.Value))
                    sqlCommand.Parameters.Add(New SqlParameter("@UserID", uID))
                    sqlCommand.Parameters.Add(New SqlParameter("@isPActive", PostActive))
                    sqlCommand.Parameters.Add(New SqlParameter("@hasImage", HasImg))
                    If HasImg = 1 Then
                        sqlCommand.Parameters.Add(New SqlParameter("@ImageID", InsertImage))
                        sqlCommand.Parameters.Add(New SqlParameter("@imageAlt", txtimgName.Text))
                    Else
                        sqlCommand.Parameters.Add(New SqlParameter("@ImageID", "2021"))
                        sqlCommand.Parameters.Add(New SqlParameter("@imageAlt", "NULL"))
                    End If
                    sqlCommand.Parameters.Add(New SqlParameter("@AllowComments", Comments))
                    Connect()
                    sqlCommand.ExecuteNonQuery()
                    btnADD.ImageUrl = "~/img/Buttons/Add.png"
                    cbisActive.Checked = False
                    txtimgName.Text = ""
                    txtPD.Text = Now.Date
                    txtTitle.Text = ""
                    Editor1.Content = ""
                    txtimgName.Enabled = False
                    txtTitle.Enabled = False
                    Editor1.Enabled = False
                    cbisActive.Enabled = False
                    txtTitle.Enabled = False
                    txtPD.Enabled = False
                    cbHasImage.Checked = False
                    imgDiv.Style.Clear()
                    imgDiv.Style.Add("visiblity:", "hidden")
                    imgDiv.Visible = False
                    imgLoad.ImageUrl = "~/img/loading.gif"
                    LoadTable(CommandTextPost, gvPosts)
                    ScriptManager.RegisterClientScriptBlock(Page, GetType(Page), "ClientScript", "alert('Added Successful')", True)
                    Exit Sub
                Catch ex As Exception
                    ScriptManager.RegisterClientScriptBlock(Page, GetType(Page), "ClientScript", "alert('Error in Adding')", True)
                    lbl.Text = ex.Message
                End Try
            End If
        Else
            btnADD.ImageUrl = "~/img/Buttons/Save.png"
            If btnEdit.ImageUrl = "~/img/Buttons/Save.png" Then
                btnEdit.ImageUrl = "~/img/Buttons/Edit.png"
            End If
            cbisActive.Checked = False
            txtimgName.Text = ""
            txtPD.Text = Now.Date
            txtTitle.Text = ""
            Editor1.Content = ""
            txtTitle.Text = ""
            Text = ""
            RowID = ""
            txtPD.Enabled = True
            txtimgName.Enabled = True
            Editor1.Enabled = True
            txtTitle.Enabled = True
            cbisActive.Enabled = True
            cbHasImage.Enabled = True
            ddlBlogger.Enabled = True
            cbAllowComments.Enabled = True
            Exit Sub
        End If
    End Sub
    Protected Function InsertImage()
        Dim imgID As String = ""
        Try
            Dim connection As SqlConnection = Nothing

            Dim img As FileUpload = CType(imgUp, FileUpload)
            Dim imgByte As Byte() = Nothing
            If img.HasFile AndAlso Not img.PostedFile Is Nothing Then
                'To create a PostedFile
                Dim File As HttpPostedFile = img.PostedFile
                'Create byte Array with file len
                imgByte = New Byte(File.ContentLength - 1) {}
                'force the control to load data in array
                File.InputStream.Read(imgByte, 0, File.ContentLength)
            End If
            ' Insert the employee name and image into db
            connection = con
            Dim sql As String = "INSERT INTO Images(Name,image) VALUES(@Name, @image)"
            Dim cmd As SqlCommand = New SqlCommand(sql, connection)
            cmd.Parameters.AddWithValue("@Name", txtimgName.Text.Trim & "" & Date.Now.ToString("MM/dd/yy hh:mm"))
            cmd.Parameters.AddWithValue("@image", imgByte)
            cmd.ExecuteScalar()
            '--------------------Retreive ID----------------------------------------------'
            Dim connectionimg As SqlConnection = Nothing
            Dim sqlCommand As SqlClient.SqlCommand = New SqlClient.SqlCommand
            sqlCommand.CommandText = "select ID from Images where Name='" & txtimgName.Text.Trim & "" & Date.Now.ToString("MM/dd/yy hh:mm") & "'"
            sqlCommand.Connection = con
            Connect()
            imgID = sqlCommand.ExecuteScalar
        Catch
            ScriptManager.RegisterClientScriptBlock(Page, GetType(Page), "ClientScript", "alert('Error in image insertion')", True)
        End Try
        Return imgID
    End Function
    Protected Sub btnEdit_Click(sender As Object, e As ImageClickEventArgs) Handles btnEdit.Click
        Text = Editor1.Content.Replace(Environment.NewLine, "<br />")
        If RowID = "" Then
            ScriptManager.RegisterClientScriptBlock(Page, GetType(Page), "ClientScript", "alert('Please Select A Record To Edit')", True)
            Exit Sub
        End If
        If (btnEdit.ImageUrl = "~/img/Buttons/Edit.png") Then
            If btnADD.ImageUrl = "~/img/Buttons/Save.png" Then
                btnADD.ImageUrl = "~/img/Buttons/Add.png"
            End If
            btnEdit.ImageUrl = "~/img/Buttons/Save.png"
            txtimgName.Enabled = True
            Editor1.Enabled = True
            txtTitle.Enabled = True
            cbisActive.Enabled = True
            cbHasImage.Enabled = True
            ddlBlogger.Enabled = True
            cbAllowComments.Enabled = True
            Exit Sub
        Else
            Try
                If (cbisActive.Checked = True) Then
                    PostActive = "1"
                Else
                    PostActive = "0"
                End If
                If (cbisActive.Checked = True) Then
                    PostActive = "1"
                Else
                    PostActive = "0"
                End If
                If (cbHasImage.Checked = True) Then
                    HasImg = "1"
                Else
                    HasImg = "0"
                End If
                If (cbAllowComments.Checked = True) Then
                    Comments = "1"
                Else
                    Comments = "0"
                End If
                Dim sqlCommand As SqlClient.SqlCommand = New SqlClient.SqlCommand
                sqlCommand.CommandText = "UPDATE  Posts SET [PTitle]=@PTitle,[Text]=@Text,[PostDate]=@PostDate,[BlogID]=@BlogID,[UserID]=@UserID,[isPActive]=@isPActive,[hasImage]=@hasImage,[ImageID]=@ImageID, [AllowComments]=@AllowComments, [imageAlt]=@imageAlt  WHERE PID = '" & RowID & " '"
                sqlCommand.Connection = con
                ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                sqlCommand.Parameters.Add(New SqlParameter("@PTitle", txtTitle.Text))
                sqlCommand.Parameters.Add(New SqlParameter("@Text", Text))
                sqlCommand.Parameters.Add(New SqlParameter("@PostDate", txtPD.Text))
                sqlCommand.Parameters.Add(New SqlParameter("@BlogID", ddlBlogger.SelectedItem.Value))
                sqlCommand.Parameters.Add(New SqlParameter("@UserID", uID))
                sqlCommand.Parameters.Add(New SqlParameter("@isPActive", PostActive))
                sqlCommand.Parameters.Add(New SqlParameter("@hasImage", HasImg))
                If imgUp.FileName = "" Then
                    sqlCommand.Parameters.Add(New SqlParameter("@ImageID", imgID))
                Else : sqlCommand.Parameters.Add(New SqlParameter("@ImageID", InsertImage))
                End If
                sqlCommand.Parameters.Add(New SqlParameter("@AllowComments", Comments))
                sqlCommand.Parameters.Add(New SqlParameter("@imageAlt", txtimgName.Text))
                Connect()
                sqlCommand.ExecuteNonQuery()
                btnADD.ImageUrl = "~/img/Buttons/Add.png"
                cbisActive.Checked = False
                txtimgName.Text = ""
                txtPD.Text = Now.Date
                txtTitle.Text = ""
                Editor1.Content = ""
                Text = ""
                txtimgName.Enabled = False
                txtTitle.Enabled = False
                Editor1.Enabled = False
                cbisActive.Enabled = False
                txtTitle.Enabled = False
                imgDiv.Style.Clear()
                imgDiv.Style.Add("visiblity:", "hidden")
                imgDiv.Visible = False
                imgLoad.ImageUrl = "~/img/loading.gif"
                LoadTable(CommandTextPost, gvPosts)
                Exit Sub
            Catch ex As Exception
                ScriptManager.RegisterClientScriptBlock(Page, GetType(Page), "ClientScript", "alert('Error in Editing')", True)
            End Try
        End If
    End Sub

    Protected Sub btnDelete_Click(sender As Object, e As ImageClickEventArgs) Handles btnDelete.Click
        Try
            If RowID = "" Then
                Response.Write("<script language=javascript>alert('Select Row First !');</script>")
                Exit Sub
            Else
                Dim sqlCommand As SqlClient.SqlCommand = New SqlClient.SqlCommand
                sqlCommand.CommandText = "Delete From  Posts  where PID='" & RowID & "'"
                sqlCommand.Connection = con
                sqlCommand.ExecuteNonQuery()
                LoadTable(CommandTextPost, gvPosts)
                Dim url = "frmPosts.aspx"
                Dim script = "window.onload = function(){ alert('"
                script += "Record Has Been Deleted Successfully"""
                Script += "');"
                Script += "window.location = '"
                Script += url
                Script += "'; }"
                ClientScript.RegisterStartupScript(Me.GetType(), "Redirect", script, True)
            End If
        Catch ex As Exception
            Response.Write("<script language=javascript>alert('Select Row First !, Deletion Error');</script>")
        End Try
    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As ImageClickEventArgs) Handles btnCancel.Click
        Response.Redirect("frmPosts.aspx")
    End Sub

    Protected Sub cbHasImage_CheckedChanged(sender As Object, e As EventArgs) Handles cbHasImage.CheckedChanged
        If IsPostBack Then
            If cbHasImage.Checked Then
                imgDiv.Style.Clear()
                imgDiv.Style.Add("visibility", "visible")
            ElseIf cbHasImage.Checked = False Then
                imgDiv.Style.Clear()
                imgDiv.Style.Add("visiblity:", "hidden")
                imgDiv.Visible = False
            End If
        End If
    End Sub

    Protected Sub gvPosts_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvPosts.SelectedIndexChanged
        RowID = gvPosts.SelectedRow.Cells(1).Text
        '===================================================================================================================='
        Try
            btnEdit.ImageUrl = "~/img/Buttons/Edit.png"
            btnADD.ImageUrl = "~/img/Buttons/Add.png"

            Connection()

            Dim sqlCommand3 As SqlClient.SqlCommand = New SqlClient.SqlCommand

            sqlCommand3.CommandText = " SELECT PTitle, Text, PostDate, BlogID, UserID, isPActive, hasImage, ImageID, AllowComments FROM Posts where  PID='" & RowID & "'"

            sqlCommand3.Connection = con

            Dim da As New SqlClient.SqlDataAdapter(sqlCommand3)

            Dim ds As New DataTable()
            da.Fill(ds)
            txtTitle.Text = ds(0)("PTitle").ToString()
            Editor1.Content = ds(0)("Text").ToString()
            GetDropDownName(" SELECT Title FROM Blogs where BID='" & ds(0)("BlogID").ToString() & "'", ddlBlogger)
            txtPD.TextMode = TextBoxMode.Search
            txtPD.Text = ds(0)("PostDate").ToString()
            imgID = ds(0)("ImageID").ToString()
            If ds(0)("isPActive").ToString() = "0 " Then
                cbisActive.Checked = False
            Else
                cbisActive.Checked = True
            End If
            If ds(0)("hasImage").ToString() = "0 " Then
                cbHasImage.Checked = False

                If imgID = "" Then


                Else
                    cbHasImage.Checked = True
                    Dim sqlCommand As SqlClient.SqlCommand = New SqlClient.SqlCommand
                    sqlCommand.CommandText = "SELECT ImageSize,image ,Name FROM Images where ID='" & ds(0)("ImageID").ToString() & "'"
                    sqlCommand.Connection = con

                    Dim da1 As New SqlClient.SqlDataAdapter(sqlCommand)
                    Dim dt1 As New DataTable
                    da1.Fill(dt1)
                    txtimgName.Text = dt1.Rows(0)("Name")
                    If ds(0)("ImageID") = "2021" Then
                        cbHasImage.Checked = False
                    Else
                        FetchImage(dt1.Rows(0)("image"))
                        imgDiv.Style.Clear()
                        imgDiv.Style.Add("visibility", "visible")
                    End If
                End If
                If ds(0)("AllowComments").ToString() = "0 " Then
                    cbAllowComments.Checked = False
                Else
                    cbAllowComments.Checked = True
                End If
                Connect()
            End If


        Catch ex As Exception
            Response.Write("<script language=javascript>alert('Filling Error');</script>")
        End Try

    End Sub
End Class