Imports ASPSnippets.GoogleAPI
Imports System.Web.Script.Serialization

Partial Class VB
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        GoogleConnect.ClientId = "<Google Client ID>"
        GoogleConnect.ClientSecret = "<Google Client Secret>"
        GoogleConnect.RedirectUri = Request.Url.AbsoluteUri.Split("?"c)(0)

        If Not String.IsNullOrEmpty(Request.QueryString("code")) Then
            Dim code As String = Request.QueryString("code")
            Dim json As String = GoogleConnect.Fetch("me", code)
            Dim profile As GoogleProfile = New JavaScriptSerializer().Deserialize(Of GoogleProfile)(json)
            lblId.Text = profile.Id
            lblName.Text = profile.DisplayName
            lblEmail.Text = profile.Emails.Find(Function(email) email.Type = "account").Value
            lblGender.Text = profile.Gender
            lblType.Text = profile.ObjectType
            ProfileImage.ImageUrl = profile.Image.Url
            pnlProfile.Visible = True
            btnLogin.Enabled = False
        End If
        If Request.QueryString("error") = "access_denied" Then
            ClientScript.RegisterClientScriptBlock(Me.GetType(), "alert", "alert('Access denied.')", True)
        End If
    End Sub

    Protected Sub Login(sender As Object, e As EventArgs)
        GoogleConnect.Authorize("profile", "email")
    End Sub
    Protected Sub Clear(sender As Object, e As EventArgs)
        GoogleConnect.Clear(Request.QueryString("code"))
    End Sub

    Public Class GoogleProfile
        Public Property Id() As String
            Get
                Return m_Id
            End Get
            Set(value As String)
                m_Id = value
            End Set
        End Property
        Private m_Id As String
        Public Property DisplayName() As String
            Get
                Return m_DisplayName
            End Get
            Set(value As String)
                m_DisplayName = value
            End Set
        End Property
        Private m_DisplayName As String
        Public Property Image() As Image
            Get
                Return m_Image
            End Get
            Set(value As Image)
                m_Image = value
            End Set
        End Property
        Private m_Image As Image
        Public Property Emails() As List(Of Email)
            Get
                Return m_Emails
            End Get
            Set(value As List(Of Email))
                m_Emails = value
            End Set
        End Property
        Private m_Emails As List(Of Email)
        Public Property Gender() As String
            Get
                Return m_Gender
            End Get
            Set(value As String)
                m_Gender = value
            End Set
        End Property
        Private m_Gender As String
        Public Property ObjectType() As String
            Get
                Return m_ObjectType
            End Get
            Set(value As String)
                m_ObjectType = value
            End Set
        End Property
        Private m_ObjectType As String
    End Class

    Public Class Email
        Public Property Value() As String
            Get
                Return m_Value
            End Get
            Set(value As String)
                m_Value = value
            End Set
        End Property
        Private m_Value As String
        Public Property Type() As String
            Get
                Return m_Type
            End Get
            Set(value As String)
                m_Type = value
            End Set
        End Property
        Private m_Type As String
    End Class

    Public Class Image
        Public Property Url() As String
            Get
                Return m_Url
            End Get
            Set(value As String)
                m_Url = value
            End Set
        End Property
        Private m_Url As String
    End Class

End Class
