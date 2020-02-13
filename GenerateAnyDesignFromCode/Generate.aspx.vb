 Public Sub Load_Chats()
        Try
            Connect()
            Connection()
            Dim str As String = "SELECT Companies.CompanyName,Companies.CompanyID  from  Companies INNER JOIN Chat ON Companies.CompanyID = Chat.CompID INNER JOIN Employee ON Chat.EmpID = Employee.FixedID Where (Employee.FixedID= '" & Session("FixedID") & "') AND (Companies.isActive = 1) AND (Employee.isRetired = 1) AND (Employee.isActive = 1)"
            Dim cmd As New SqlCommand(str, con)
            Dim da As New SqlDataAdapter(cmd)
            Dim ds As New DataSet()
            da.Fill(ds)

            dsFillChats.DataSource = ds
            dsFillChats.DataBind()
        Catch ex As Exception

        End Try
    End Sub