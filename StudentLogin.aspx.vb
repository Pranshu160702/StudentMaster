Imports System
Imports System.IO
Imports System.Web.UI
Imports System.Data
Imports System.Data.SqlClient

Public Class StudentLogin
    Inherits System.Web.UI.Page

    Dim str As String = ConfigurationManager.ConnectionStrings("connectionString").ConnectionString
    Dim con As SqlConnection = New SqlConnection(str)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Session("Login") = 0
    End Sub

    Protected Sub loginBtn_Click(ByVal sender As Object, ByVal e As EventArgs) Handles loginBtn.Click
        If con.State = ConnectionState.Closed Then
            con.Open()
        End If
        Dim cmd As SqlCommand = con.CreateCommand()
        cmd.CommandType = CommandType.StoredProcedure
        cmd.CommandText = "LoginStudent"
        cmd.Parameters.Add("@Email", SqlDbType.VarChar).Value = email.Text
        cmd.Parameters.Add("@Password", SqlDbType.VarChar).Value = pass.Text
        Dim user As Integer = CInt(cmd.ExecuteScalar())

        If user = 0 Then
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alertMessage", "alert('Wrong Credentials Entered!! \n Enter correct credentials or Register!')", True)
        Else
            Session("Login") = 1
            Response.Redirect("Dashboard.aspx")
        End If
    End Sub

End Class