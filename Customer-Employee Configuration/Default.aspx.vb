Imports CRM.Global_asax
Imports System.Data.SqlClient

Public Class _Default11
    Inherits System.Web.UI.Page
    Public Command = "SELECT [PrsID],[PrsFName],[PrsLName],[PrsEmail],[CompanyName],[Address],[PrsType],[SubType] FROM [OnlineCRM].[dbo].[Person] WHERE PrsType<>'Customer'  AND PrsType<>'None'"
    Dim ActiveFlag = 0
    Dim InsuranceFlag = 0
    Dim NSSFFlag = 0
    Dim OAFlag = 0
    Dim UserCounter = 0
    Dim AdminCounter = 0
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Try
            If Not IsPostBack Then
                PageTitle = "Admin_li_StaffManagement"
                PageName = "Staff Management"
                LoadTable(Command, gvData)
                FillDropDown(ddlDep, " SELECT  DptID ,DptEnName FROM Department", "DptEnName", "DptID")
                FillDropDown(ddlImage, "SELECT [FileID],[FileName] FROM  [Files]", "FileName", "FileID")
                FillDropDown(ddlBCat, "SELECT [BCatID],[CatEnName] FROM [BusinessCategory] WHERE [isActive]=1", "CatEnName", "BCatID")
                FillDropDown(ddlCity, "SELECT [CItyID],[CEnName] FROM [City] WHERE isActive=1", "CEnName", "CItyID")
                FillDropDown(ddlZone, "SELECT [ZoneID],[ZEnName] FROM [Zone] WHERE isActive=1", "ZEnName", "ZoneID")
                Connection()
                Connect()
            End If
        Catch ex As Exception

            Status = "Danger,Error,Aborted Connection"
            DesignWarningColor(DivStatus, StatusType, lblStrongStatus, lblStatus)
        End Try
    End Sub

    Public Sub Addition_Func()
        Try
            If (ddlDep.SelectedValue = "NaN" Or ddlType.SelectedValue = "NaN" Or ddlSType.SelectedValue = "NaN" Or txtFN.Text = Nothing Or txtLN.Text = Nothing Or txtPN1.Text = Nothing Or txtEmail.Text = Nothing Or ddlGender.SelectedValue = "NaN") Then
                Status = "Warning,Warning,Empty Fields Found"
                DesignWarningColor(DivStatus, StatusType, lblStrongStatus, lblStatus)
                Exit Sub
            End If

            If (cbActive.Checked = True) Then
                ActiveFlag = 1
            Else
                ActiveFlag = 0
            End If
            If (cbInsurance.Checked = True) Then
                InsuranceFlag = 1
            Else
                InsuranceFlag = 0
            End If
            If (cbNSSF.Checked = True) Then
                NSSFFlag = 1
            Else
                NSSFFlag = 0
            End If
            If (cbOA.Checked = True) Then
                OAFlag = 1
            Else
                OAFlag = 0
            End If 
            If (CheckingDuplicates("SELECT COUNT(PrsFName + PrsLName) AS Counter FROM  Person WHERE (PrsFName LIKE '" & txtFN.Text & "') AND (PrsLName LIKE '" & txtLN.Text & "')")) = True Then
                Refresh()
                Exit Sub
            End If

            Connect()
            Dim sqlCommand As SqlClient.SqlCommand = New SqlClient.SqlCommand
            sqlCommand.Connection = con
            sqlCommand.CommandText = "INSERT INTO  Person (PrsID, PrsIntNo1, PrsIntNo2, PrsIntNo3, PrsType, SubType, PrsFName, PrsLName, PrsFPhoneNo1, PrsFPhoneNo2, PrsMobileNo, PrsEmail, PrsCommissionPer,PrsWebsite, CompanyName, MOF, PrsImgID, DOB, EnrollmentDate, Address, EndingDate, UBalance, LBalance, Gender, SocialStatus, IDCardNb, Reg, PrsFb,Salesman, Ext1, hasInsurance, InsuranceName, InsuranceClass, BCatID, hasNssf, NssfNo, AllowOA, isActive, DptID) VALUES (@PrsID, @PrsIntNo1, @PrsIntNo2, @PrsIntNo3, @PrsType, @SubType, @PrsFName, @PrsLName, @PrsFPhoneNo1, @PrsFPhoneNo2, @PrsMobileNo, @PrsEmail, @PrsCommissionPer,@PrsWebsite, @CompanyName, @MOF, @PrsImgID,@DOB, @EnrollmentDate, @Address, @EndingDate, @UBalance, @LBalance, @Gender, @SocialStatus, @IDCardNb, @Reg, @PrsFb,@Salesman, @Ext1, @hasInsurance, @InsuranceName, @InsuranceClass, @BCatID, @hasNssf, @NssfNo, @AllowOA, @isActive, @DptID) "
            If ddlType.SelectedItem.Text = "Admin" Then
                sqlCommand.Parameters.Add(New SqlParameter("@PrsIntNo1", GetNumPrsID()))
                sqlCommand.Parameters.Add(New SqlParameter("@PrsIntNo2", 0))
                sqlCommand.Parameters.Add(New SqlParameter("@PrsIntNo3", 0))
            ElseIf ddlType.SelectedItem.Text = "User" Then
                sqlCommand.Parameters.Add(New SqlParameter("@PrsIntNo2", GetNumPrsID()))
                sqlCommand.Parameters.Add(New SqlParameter("@PrsIntNo1", 0))
                sqlCommand.Parameters.Add(New SqlParameter("@PrsIntNo3", 0))
            End If
            sqlCommand.Parameters.Add(New SqlParameter("@PrsID", GetID))

            sqlCommand.Parameters.Add(New SqlParameter("@PrsType", ddlType.SelectedValue))

            sqlCommand.Parameters.Add(New SqlParameter("@SubType", ddlSType.SelectedValue))

            sqlCommand.Parameters.Add(New SqlParameter("@PrsFName", txtFN.Text))

            sqlCommand.Parameters.Add(New SqlParameter("@PrsLName", txtLN.Text))

            sqlCommand.Parameters.Add(New SqlParameter("@PrsFPhoneNo1", txtPN1.Text))

            sqlCommand.Parameters.Add(New SqlParameter("@PrsFPhoneNo2", txtPN2.Text))

            sqlCommand.Parameters.Add(New SqlParameter("@PrsMobileNo", txtMN.Text))

            sqlCommand.Parameters.Add(New SqlParameter("@PrsEmail", txtEmail.Text))

            sqlCommand.Parameters.Add(New SqlParameter("@PrsCommissionPer", txtCommission.Text))

            sqlCommand.Parameters.Add(New SqlParameter("@PrsWebsite", txtWebsite.Text))
            If txtDOB.Text = ",,,,,,,,,," Or txtDOB.Text = "," Then
                txtDOB.Text = String.Empty
            End If
            If txtED.Text = ",,,,,,,,,," Or txtED.Text = "," Then
                txtED.Text = String.Empty
            End If
            If txtENDD.Text = ",,,,,,,,,," Or txtENDD.Text = "," Then
                txtENDD.Text = String.Empty
            End If
            If txtIDCardNo.Text = ",,,,,,,,,," Or txtIDCardNo.Text = "," Then
                txtIDCardNo.Text = String.Empty
            End If
            If txtRegistrationNo.Text = ",,,,,,,,,," Or txtRegistrationNo.Text = "," Then
                txtRegistrationNo.Text = String.Empty
            End If
            If txtNSSFNo.Text = ",,,,,,,,,," Or txtNSSFNo.Text = "," Then
                txtNSSFNo.Text = String.Empty
            End If
            If txtSSN.Text = ",,,,,,,,,," Or txtSSN.Text = "," Then
                txtSSN.Text = String.Empty
            End If
            sqlCommand.Parameters.Add(New SqlParameter("@DOB", txtDOB.Text))

            sqlCommand.Parameters.Add(New SqlParameter("@EnrollmentDate", txtED.Text))

            sqlCommand.Parameters.Add(New SqlParameter("@Address", txtAddress.Text))

            sqlCommand.Parameters.Add(New SqlParameter("@EndingDate", txtENDD.Text))

            sqlCommand.Parameters.Add(New SqlParameter("@UBalance", txtUBalance.Text))

            sqlCommand.Parameters.Add(New SqlParameter("@LBalance", txtLBalance.Text))

            sqlCommand.Parameters.Add(New SqlParameter("@Gender", ddlGender.SelectedValue))

            sqlCommand.Parameters.Add(New SqlParameter("@SocialStatus", txtSSN.Text))

            sqlCommand.Parameters.Add(New SqlParameter("@IDCardNb", txtIDCardNo.Text))

            sqlCommand.Parameters.Add(New SqlParameter("@Reg", txtRegistrationNo.Text))

            sqlCommand.Parameters.Add(New SqlParameter("@PrsFb", txtFacebook.Text))

            sqlCommand.Parameters.Add(New SqlParameter("@Salesman", ddlSalesman.SelectedValue))

            sqlCommand.Parameters.Add(New SqlParameter("@Ext1", txtExtension.Text))

            sqlCommand.Parameters.Add(New SqlParameter("@InsuranceName", txtInsuranceName.Text))

            sqlCommand.Parameters.Add(New SqlParameter("@InsuranceClass", txtInsuranceClass.Text))

            sqlCommand.Parameters.Add(New SqlParameter("@DptID", ddlDep.SelectedValue))

            sqlCommand.Parameters.Add(New SqlParameter("@hasInsurance", InsuranceFlag))

            sqlCommand.Parameters.Add(New SqlParameter("@BCatID", ddlBCat.SelectedValue))

            sqlCommand.Parameters.Add(New SqlParameter("@CompanyName", txtCompany.Text))

            sqlCommand.Parameters.Add(New SqlParameter("@MOF", txtMOF.Text))

            sqlCommand.Parameters.Add(New SqlParameter("@isActive", ActiveFlag))

            If ddlImage.SelectedIndex = 0 Then
                sqlCommand.Parameters.Add(New SqlParameter("@PrsImgID", 1))
            Else
                sqlCommand.Parameters.Add(New SqlParameter("@PrsImgID", ddlImage.SelectedValue))
            End If
            sqlCommand.Parameters.Add(New SqlParameter("@hasNssf", NSSFFlag))

            sqlCommand.Parameters.Add(New SqlParameter("@NssfNo", txtNSSFNo.Text))

            sqlCommand.Parameters.Add(New SqlParameter("@AllowOA", OAFlag))

            sqlCommand.ExecuteNonQuery()
            Refresh()
            Status = "Success,Success,Added Successfully"
            DesignWarningColor(DivStatus, StatusType, lblStrongStatus, lblStatus)
        Catch ex As Exception
            Status = "Danger,Error, Bug in Adding "
            DesignWarningColor(DivStatus, StatusType, lblStrongStatus, lblStatus)
        End Try
    End Sub
    Function GetNumPrsID() As String
        Try
            Dim sqlCommand As SqlClient.SqlCommand = New SqlClient.SqlCommand
            Connect()

            If ddlType.SelectedItem.Text = "Admin" Then

                sqlCommand.CommandText = "SELECT TOP 1 PrsIntNo1 FROM Person ORDER by  PrsIntNo1 DESC;"
                sqlCommand.Connection = con
                If sqlCommand.ExecuteScalar() Is System.DBNull.Value Then
                    AdminCounter = 1
                Else : AdminCounter = sqlCommand.ExecuteScalar() + 1
                End If
                Return AdminCounter
                Exit Function

            ElseIf ddlType.SelectedItem.Text = "User" Then

                sqlCommand.CommandText = "SELECT TOP 1 PrsIntNo2 FROM Person ORDER by  PrsIntNo2 DESC;"
                sqlCommand.Connection = con
                If sqlCommand.ExecuteScalar() Is System.DBNull.Value Then
                    UserCounter = 1
                Else
                    UserCounter = sqlCommand.ExecuteScalar() + 1
                End If
                Return UserCounter
                Exit Function
            End If
        Catch ex As Exception
            Status = "Danger,Error,Error in Retrieving Person Counter"
            DesignWarningColor(DivStatus, StatusType, lblStrongStatus, lblStatus)
        End Try
    End Function
    Private Sub Refresh()
        txtAddress.Text = String.Empty
        txtCommission.Text = String.Empty
        txtCompany.Text = String.Empty
        txtDOB.Text = String.Empty
        txtED.Text = String.Empty
        txtEmail.Text = String.Empty
        txtENDD.Text = String.Empty
        txtExtension.Text = String.Empty
        txtFacebook.Text = String.Empty
        txtFN.Text = String.Empty
        txtIDCardNo.Text = String.Empty
        txtInsuranceClass.Text = String.Empty
        txtInsuranceName.Text = String.Empty
        txtLBalance.Text = String.Empty
        txtLN.Text = String.Empty
        txtMN.Text = String.Empty
        txtMOF.Text = String.Empty
        txtNSSFNo.Text = String.Empty
        txtPN1.Text = String.Empty
        txtPN2.Text = String.Empty
        txtRegistrationNo.Text = String.Empty
        txtSSN.Text = String.Empty
        txtUBalance.Text = String.Empty
        txtWebsite.Text = String.Empty
        ddlZone.SelectedIndex = 0
        ddlBCat.SelectedIndex = 0
        ddlCity.SelectedIndex = 0
        ddlDep.SelectedIndex = 0
        ddlGender.SelectedIndex = 0
        ddlImage.SelectedIndex = 0
        ddlSalesman.SelectedIndex = 0
        ddlSType.SelectedIndex = 0
        ddlType.SelectedIndex = 0 
        FillDropDown(ddlDep, " SELECT  DptID ,DptEnName FROM Department", "DptEnName", "DptID")
        FillDropDown(ddlImage, "SELECT [FileID],[FileName] FROM  [Files]", "FileName", "FileID")
        FillDropDown(ddlBCat, "SELECT [BCatID],[CatEnName] FROM [BusinessCategory] WHERE [isActive]=1", "CatEnName", "BCatID")
        FillDropDown(ddlCity, "SELECT [CItyID],[CEnName] FROM [City] WHERE isActive=1", "CEnName", "CItyID")
        FillDropDown(ddlZone, "SELECT [ZoneID],[ZEnName] FROM [Zone] WHERE isActive=1", "ZEnName", "ZoneID")
        ddlType.Enabled = True
        ddlSType.Enabled = True
        txtFN.Enabled = True
        txtLN.Enabled = True 
        LoadTable(Command, gvData)
    End Sub
    Public Sub Deletion_Func()

        Try 
            RowID = gvData.SelectedRow.Cells(1).Text

            Connect()
            Dim sqlCommand As SqlClient.SqlCommand = New SqlClient.SqlCommand
            sqlCommand.Connection = con
            sqlCommand.CommandText = "Delete from Person where PrsID='" & RowID & "'"
            sqlCommand.ExecuteNonQuery()
            Refresh()
            RowID = ""
            Status = "Success,Success,Deleted Successfully"
            DesignWarningColor(DivStatus, StatusType, lblStrongStatus, lblStatus)
        Catch ex As Exception
            Status = "Danger,Error,Aborted Connection"
            DesignWarningColor(DivStatus, StatusType, lblStrongStatus, lblStatus)
        End Try
    End Sub
    Public Sub Edition_Func()

        Try

            If (ddlDep.SelectedValue = "NaN" Or ddlType.SelectedValue = "NaN" Or ddlSType.SelectedValue = "NaN" Or txtFN.Text = Nothing Or txtLN.Text = Nothing Or txtPN1.Text = Nothing Or txtEmail.Text = Nothing Or ddlGender.SelectedValue = "NaN") Then
                Status = "Warning,Warning,Empty Fields Found"
                DesignWarningColor(DivStatus, StatusType, lblStrongStatus, lblStatus)
                Exit Sub
            End If

            RowID = gvData.SelectedRow.Cells(1).Text
            If (cbActive.Checked = True) Then
                ActiveFlag = 1
            Else
                ActiveFlag = 0
            End If
            If (cbInsurance.Checked = True) Then
                InsuranceFlag = 1
            Else
                InsuranceFlag = 0
            End If
            If (cbNSSF.Checked = True) Then
                NSSFFlag = 1
            Else
                NSSFFlag = 0
            End If
            If (cbOA.Checked = True) Then
                OAFlag = 1
            Else
                OAFlag = 0
            End If
            Connect()
            Dim sqlCommand As SqlClient.SqlCommand = New SqlClient.SqlCommand
            sqlCommand.Connection = con
            sqlCommand.CommandText = "UPDATE Person SET   PrsFPhoneNo1=@PrsFPhoneNo1, PrsFPhoneNo2=@PrsFPhoneNo2, PrsMobileNo=@PrsMobileNo, PrsEmail=@PrsEmail, PrsCommissionPer=@PrsCommissionPer,PrsWebsite=@PrsWebsite, CompanyName=@CompanyName, MOF=@MOF, PrsImgID=@PrsImgID, DOB=@DOB, EnrollmentDate=@EnrollmentDate, Address=@Address, EndingDate=@EndingDate, UBalance=@UBalance, LBalance=@LBalance, Gender=@Gender, SocialStatus=@SocialStatus, IDCardNb=@IDCardNb, Reg=@Reg, PrsFb=@PrsFb,Salesman=@Salesman, Ext1=@Ext1, hasInsurance=@hasInsurance, InsuranceName=@InsuranceName, InsuranceClass=@InsuranceClass, BCatID=@BCatID, hasNssf=@hasNssf, NssfNo=@NssfNo, AllowOA=@AllowOA, isActive=@isActive, DptID=@DptID WHERE PrsID='" & RowID & "'"
         
            sqlCommand.Parameters.Add(New SqlParameter("@PrsFPhoneNo1", txtPN1.Text))

            sqlCommand.Parameters.Add(New SqlParameter("@PrsFPhoneNo2", txtPN2.Text))

            sqlCommand.Parameters.Add(New SqlParameter("@PrsMobileNo", txtMN.Text))

            sqlCommand.Parameters.Add(New SqlParameter("@PrsEmail", txtEmail.Text))

            sqlCommand.Parameters.Add(New SqlParameter("@PrsCommissionPer", txtCommission.Text))

            sqlCommand.Parameters.Add(New SqlParameter("@PrsWebsite", txtWebsite.Text))
            If txtDOB.Text = ",,,,,,,,,," Or txtDOB.Text = "," Then
                txtDOB.Text = String.Empty
            End If
            If txtED.Text = ",,,,,,,,,," Or txtED.Text = "," Then
                txtED.Text = String.Empty
            End If
            If txtENDD.Text = ",,,,,,,,,," Or txtENDD.Text = "," Then
                txtENDD.Text = String.Empty
            End If
            If txtIDCardNo.Text = ",,,,,,,,,," Or txtIDCardNo.Text = "," Then
                txtIDCardNo.Text = String.Empty
            End If
            If txtRegistrationNo.Text = ",,,,,,,,,," Or txtRegistrationNo.Text = "," Then
                txtRegistrationNo.Text = String.Empty
            End If
            If txtNSSFNo.Text = ",,,,,,,,,," Or txtNSSFNo.Text = "," Then
                txtNSSFNo.Text = String.Empty
            End If
            If txtSSN.Text = ",,,,,,,,,," Or txtSSN.Text = "," Then
                txtSSN.Text = String.Empty
            End If
            sqlCommand.Parameters.Add(New SqlParameter("@DOB", txtDOB.Text))

            sqlCommand.Parameters.Add(New SqlParameter("@EnrollmentDate", txtED.Text))

            sqlCommand.Parameters.Add(New SqlParameter("@Address", txtAddress.Text))

            sqlCommand.Parameters.Add(New SqlParameter("@EndingDate", txtENDD.Text))

            sqlCommand.Parameters.Add(New SqlParameter("@UBalance", txtUBalance.Text))

            sqlCommand.Parameters.Add(New SqlParameter("@LBalance", txtLBalance.Text))

            sqlCommand.Parameters.Add(New SqlParameter("@Gender", ddlGender.SelectedValue))

            sqlCommand.Parameters.Add(New SqlParameter("@SocialStatus", txtSSN.Text))

            sqlCommand.Parameters.Add(New SqlParameter("@IDCardNb", txtIDCardNo.Text))

            sqlCommand.Parameters.Add(New SqlParameter("@Reg", txtRegistrationNo.Text))

            sqlCommand.Parameters.Add(New SqlParameter("@PrsFb", txtFacebook.Text))

            sqlCommand.Parameters.Add(New SqlParameter("@Salesman", ddlSalesman.SelectedValue))

            sqlCommand.Parameters.Add(New SqlParameter("@Ext1", txtExtension.Text))

            sqlCommand.Parameters.Add(New SqlParameter("@InsuranceName", txtInsuranceName.Text))

            sqlCommand.Parameters.Add(New SqlParameter("@InsuranceClass", txtInsuranceClass.Text))

            sqlCommand.Parameters.Add(New SqlParameter("@DptID", ddlDep.SelectedValue))

            sqlCommand.Parameters.Add(New SqlParameter("@hasInsurance", InsuranceFlag))

            sqlCommand.Parameters.Add(New SqlParameter("@BCatID", ddlBCat.SelectedValue))

            sqlCommand.Parameters.Add(New SqlParameter("@CompanyName", txtCompany.Text))

            sqlCommand.Parameters.Add(New SqlParameter("@MOF", txtMOF.Text))

            sqlCommand.Parameters.Add(New SqlParameter("@isActive", ActiveFlag))

            If ddlImage.SelectedIndex = 0 Then
                sqlCommand.Parameters.Add(New SqlParameter("@PrsImgID", 1))
            Else
                sqlCommand.Parameters.Add(New SqlParameter("@PrsImgID", ddlImage.SelectedValue))
            End If
            sqlCommand.Parameters.Add(New SqlParameter("@hasNssf", NSSFFlag))

            sqlCommand.Parameters.Add(New SqlParameter("@NssfNo", txtNSSFNo.Text))

            sqlCommand.Parameters.Add(New SqlParameter("@AllowOA", OAFlag))

            sqlCommand.ExecuteNonQuery()
            Refresh()
            Status = "Success,Success,Edited Successfully"
            DesignWarningColor(DivStatus, StatusType, lblStrongStatus, lblStatus)
        Catch ex As Exception
            Status = "Danger,Error, Bug in Adding "
            DesignWarningColor(DivStatus, StatusType, lblStrongStatus, lblStatus)
        End Try
    End Sub
    Sub Cancel_Func()
        Response.Redirect(Request.Url.AbsoluteUri)
    End Sub

    Protected Sub gvData_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvData.SelectedIndexChanged
        Try
            RowID = gvData.SelectedRow.Cells(1).Text
            '===================================================================================================================='
            Try
                Connection()

                Dim sqlCommand3 As SqlClient.SqlCommand = New SqlClient.SqlCommand

                sqlCommand3.CommandText = "SELECT  PrsType, SubType, PrsFName, PrsLName, PrsFPhoneNo1, PrsFPhoneNo2, PrsMobileNo, PrsEmail, PrsCommissionPer,PrsWebsite, CompanyName, MOF, PrsImgID, DOB, EnrollmentDate,ZoneID, Address, EndingDate, UBalance, LBalance, Gender, SocialStatus, IDCardNb, Reg, PrsFb,Salesman, Ext1, hasInsurance, InsuranceName, InsuranceClass, BCatID, hasNssf, NssfNo, AllowOA, isActive, DptID,CityID FROM Person  WHERE PrsID='" & RowID & "'"

                sqlCommand3.Connection = con

                Dim da As New SqlClient.SqlDataAdapter(sqlCommand3)

                Dim ds As New DataTable()
                da.Fill(ds)
                txtAddress.Text = ds(0)("Address").ToString()
                txtCommission.Text = ds(0)("PrsCommissionPer").ToString()
                txtCompany.Text = ds(0)("CompanyName").ToString()
                If ds(0)("DOB").ToString() = String.Empty Then
                Else 
                    txtDOB.Text = DateTime.Parse(ds(0)("DOB").ToString()).ToString("yyyy-MM-dd")
                End If
                If ds(0)("EnrollmentDate").ToString() = String.Empty Then
                Else
                    txtED.Text = DateTime.Parse(ds(0)("EnrollmentDate").ToString()).ToString("yyyy-MM-dd")
                End If
                If ds(0)("EndingDate").ToString() = String.Empty Then
                Else
                    txtENDD.Text = DateTime.Parse(ds(0)("EndingDate").ToString()).ToString("yyyy-MM-dd")
                End If
                txtEmail.Text = ds(0)("PrsEmail").ToString()
                txtExtension.Text = ds(0)("Ext1").ToString()
                txtFacebook.Text = ds(0)("PrsFb").ToString()
                txtFN.Text = ds(0)("PrsFName").ToString()
                txtIDCardNo.Text = ds(0)("IDCardNb").ToString()
                txtInsuranceClass.Text = ds(0)("InsuranceClass").ToString()
                txtInsuranceName.Text = ds(0)("InsuranceName").ToString()
                txtLBalance.Text = ds(0)("LBalance").ToString()
                txtLN.Text = ds(0)("PrsLName").ToString()
                txtMN.Text = ds(0)("PrsMobileNo").ToString()
                txtMOF.Text = ds(0)("MOF").ToString()
                txtNSSFNo.Text = ds(0)("NssfNo").ToString()
                txtPN1.Text = ds(0)("PrsFPhoneNo1").ToString()
                txtPN2.Text = ds(0)("PrsFPhoneNo2").ToString()
                txtRegistrationNo.Text = ds(0)("Reg").ToString()
                txtSSN.Text = ds(0)("SocialStatus").ToString()
                txtUBalance.Text = ds(0)("UBalance").ToString()
                txtWebsite.Text = ds(0)("PrsWebsite").ToString()
                ddlZone.SelectedIndex = 0
                ddlBCat.SelectedIndex = 0
                ddlCity.SelectedIndex = 0
                ddlDep.SelectedIndex = 0
                ddlGender.SelectedIndex = 0
                ddlImage.SelectedIndex = 0
                ddlSalesman.SelectedIndex = 0
                ddlSType.SelectedIndex = 0
                ddlType.SelectedIndex = 0

                ddlGender.Items.Insert(0, New ListItem(ds(0)("Gender").ToString(), ds(0)("Gender").ToString()))
                ddlSType.Items.Insert(0, New ListItem(ds(0)("SubType").ToString(), ds(0)("SubType").ToString()))
                ddlType.Items.Insert(0, New ListItem(ds(0)("PrsType").ToString(), ds(0)("PrsType").ToString()))
                Dim ACTIVE = ds(0)("isActive").ToString()
                If (ds(0)("isActive").ToString() = "True") Then
                    cbActive.Checked = True
                Else
                    cbActive.Checked = False
                End If

                Dim hasInsurance = ds(0)("hasInsurance").ToString()
                If (ds(0)("hasInsurance").ToString() = "True") Then
                    cbInsurance.Checked = True
                Else
                    cbInsurance.Checked = False
                End If

                Dim hasNssf = ds(0)("hasNssf").ToString()
                If (ds(0)("hasNssf").ToString() = "True") Then
                    cbNSSF.Checked = True
                Else
                    cbNSSF.Checked = False
                End If
                Dim AllowOA = ds(0)("AllowOA").ToString()
                If (ds(0)("AllowOA").ToString() = "True") Then
                    cbOA.Checked = True
                Else
                    cbOA.Checked = False
                End If
                '-----------------------Image-----------------------------------------------------------------------------'
                Connect()
                Dim sqlCommand1 As SqlClient.SqlCommand = New SqlClient.SqlCommand
                sqlCommand1.CommandText = "select FileName, ContentType, Content from Files where [FileID]='" & ds(0)("PrsImgID").ToString() & "'"
                sqlCommand1.Connection = con
                sqlCommand1.ExecuteNonQuery()


                Dim da2 As New SqlClient.SqlDataAdapter(sqlCommand1)
                Dim dt2 As New DataTable
                da2.Fill(dt2)
                Dim Counter = dt2.Rows.Count
                If (Counter = 0) Then
                Else
                    Dim FileName = dt2(0)("FileName").ToString()
                    If (dt2(0)("FileName").ToString() = "Empty") Then
                    Else : Dim bytes As Byte() = DirectCast(dt2.Rows(0)("Content"), Byte())
                        Dim base64String As String = Convert.ToBase64String(bytes, 0, bytes.Length)
                        img.ImageUrl = Convert.ToString("data:image/png;base64,") & base64String
                        ddlImage.Items.Insert(0, New ListItem(FileName, ds(0)("ImgID").ToString()))
                        ddlImage.SelectedIndex = 0
                    End If

                End If
                '--------------------------------------------------------------------------------------------------------------'
                '-----------------------BCatID-----------------------------------------------------------------------------' 
                Connect()
                Dim sqlCommand11 As SqlClient.SqlCommand = New SqlClient.SqlCommand
                sqlCommand11.CommandText = "SELECT [CatEnName] FROM [BusinessCategory] WHERE [isActive]=1 and BCatID='" & ds(0)("BCatID").ToString() & "'"
                sqlCommand11.Connection = con
                Dim Cat = sqlCommand11.ExecuteScalar
                ddlBCat.Items.Insert(0, New ListItem(Cat, ds(0)("BCatID").ToString()))
                ddlBCat.SelectedIndex = 0
                '--------------------------------------------------------------------------------------------------------------'
                '-----------------------DptID-----------------------------------------------------------------------------'
                Connect()
                Dim sqlCommand12 As SqlClient.SqlCommand = New SqlClient.SqlCommand
                sqlCommand12.CommandText = "SELECT  DptEnName FROM Department WHERE DptID='" & ds(0)("DptID").ToString() & "'"
                sqlCommand12.Connection = con
                Dim Dep = sqlCommand12.ExecuteScalar
                ddlDep.Items.Insert(0, New ListItem(Dep, ds(0)("DptID").ToString()))
                ddlDep.SelectedIndex = 0
                '--------------------------------------------------------------------------------------------------------------' 
                '-----------------------CItyID-----------------------------------------------------------------------------'
                Connect()
                Dim sqlCommand13 As SqlClient.SqlCommand = New SqlClient.SqlCommand
                sqlCommand13.CommandText = "SELECT [CEnName] FROM [City] WHERE isActive=1 AND CityID='" & ds(0)("CityID").ToString() & "'"
                sqlCommand13.Connection = con
                Dim city = sqlCommand13.ExecuteScalar
                ddlCity.Items.Insert(0, New ListItem(city, ds(0)("CItyID").ToString()))
                ddlCity.SelectedIndex = 0
                '--------------------------------------------------------------------------------------------------------------'

                '-----------------------CItyID-----------------------------------------------------------------------------'
                Connect()
                Dim sqlCommand14 As SqlClient.SqlCommand = New SqlClient.SqlCommand
                sqlCommand14.CommandText = "SELECT [ZEnName] FROM [Zone] WHERE isActive=1 AND ZoneID='" & ds(0)("ZoneID").ToString() & "'"
                sqlCommand14.Connection = con
                Dim Zone = sqlCommand14.ExecuteScalar
                ddlZone.Items.Insert(0, New ListItem(city, ds(0)("ZoneID").ToString()))
                ddlZone.SelectedIndex = 0
                '--------------------------------------------------------------------------------------------------------------'
                ddlType.Enabled = False
                ddlSType.Enabled = False
                txtFN.Enabled = False
                txtLN.Enabled = False

            Catch ex As Exception
                Status = "Danger,Error,Filling Error"
                DesignWarningColor(DivStatus, StatusType, lblStrongStatus, lblStatus)
            End Try
        Catch ex As Exception
            Status = "Danger,Error,Error in Selecting Row"
            DesignWarningColor(DivStatus, StatusType, lblStrongStatus, lblStatus)
        End Try
    End Sub



    Protected Sub ddlType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlType.SelectedIndexChanged
        Try
            '**************************************************************************************************************'
            If (ddlType.SelectedValue = "Admin") Then
                ddlSType.Items(0).Text = "Admin"
                ddlSType.Items(0).Value = "Admin"
                ddlSType.SelectedIndex = 0
                ddlSType.Enabled = False
            ElseIf (ddlType.SelectedValue = "User") Then
                ddlSType.Enabled = True
                ddlSType.Items(0).Text = "----------------------"
                ddlSType.Items(0).Value = "NaN"
            Else
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Function GetID()

        If (ddlType.SelectedItem.Text = "Admin") Then
            Return "A00" + GetNumPrsID()
        Else
            Return "E00" + GetNumPrsID()
        End If
    End Function

    Protected Sub txtLN_TextChanged(sender As Object, e As EventArgs) Handles txtLN.TextChanged
        If CheckingDuplicates("SELECT Count(PrsFName+' '+PrsLName) FROM Person WHERE PrsLName='" & txtLN.Text & "'and  PrsFName='" & txtFN.Text & "'") = 1 Then
            txtLN.Text = String.Empty
            txtLN.Focus()
            Status = "Warning,Warning,Duplicates Occurred"
            DesignWarningColor(DivStatus, StatusType, lblStrongStatus, lblStatus)
            Exit Sub
        Else : EnLargeText(txtLN) 
        End If 
    End Sub

    Protected Sub txtFN_TextChanged(sender As Object, e As EventArgs) Handles txtFN.TextChanged
        Try

            EnLargeText(txtFN)
            txtLN.Focus() 
        Catch ex As Exception

        End Try
    End Sub
End Class