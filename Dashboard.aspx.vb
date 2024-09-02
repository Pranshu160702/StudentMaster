Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports System.Web
Imports System.Web.UI

Public Class Dashboard
    Inherits System.Web.UI.Page

    Dim str As String = ConfigurationManager.ConnectionStrings("connectionString").ConnectionString
    Dim con As SqlConnection = New SqlConnection(Str)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            CheckUserLogin()
            load_analysis_tables()
        End If
    End Sub

    Protected Sub CheckUserLogin()
        If CType(Session("Login"), Integer) = 0 Then
            Response.Redirect("StudentLogin.aspx")
        End If
    End Sub

    Private Sub load_analysis_tables()
        loadgwa()
        loadcwa()
        loadswa()
        loadciwa()
    End Sub

    Private Sub loadgwa()
        If con.State = ConnectionState.Closed Then con.Open()

        Dim cmd As SqlCommand = con.CreateCommand()
        cmd.CommandType = CommandType.StoredProcedure
        cmd.CommandText = "gwastudents"
        genderTable.DataSource = cmd.ExecuteReader()
        genderTable.DataBind()
        con.Close()
    End Sub

    Private Sub loadcwa()
        If con.State = ConnectionState.Closed Then con.Open()

        Dim cmd2 As SqlCommand = con.CreateCommand()
        cmd2.CommandType = CommandType.StoredProcedure
        cmd2.CommandText = "cwastudents"
        countryTable.DataSource = cmd2.ExecuteReader()
        countryTable.DataBind()
    End Sub

    Private Sub loadswa()
        If con.State = ConnectionState.Closed Then con.Open()

        Dim cmd3 As SqlCommand = con.CreateCommand()
        cmd3.CommandType = CommandType.StoredProcedure
        cmd3.CommandText = "swastudents"
        stateTable.DataSource = cmd3.ExecuteReader()
        stateTable.DataBind()
    End Sub

    Private Sub loadciwa()
        If con.State = ConnectionState.Closed Then con.Open()

        Dim cmd4 As SqlCommand = con.CreateCommand()
        cmd4.CommandType = CommandType.StoredProcedure
        cmd4.CommandText = "ciwastudents"
        cityTable.DataSource = cmd4.ExecuteReader()
        cityTable.DataBind()

        con.Close()
    End Sub

End Class