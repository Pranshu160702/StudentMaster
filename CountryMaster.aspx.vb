Imports System
Imports System.Data
Imports System.Data.SqlClient

Public Class CountryMaster
    Inherits System.Web.UI.Page

    Dim str As String = ConfigurationManager.ConnectionStrings("connectionString").ConnectionString
    Dim con As SqlConnection = New SqlConnection(Str)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            CheckUserLogin()
            load_data()
        End If
    End Sub

    Protected Sub CheckUserLogin()
        If CType(Session("Login"), Integer) = 0 Then
            Response.Redirect("StudentLogin.aspx")
        End If
    End Sub

    Private Sub load_data()
        If con.State = ConnectionState.Closed Then con.Open()
        Dim cmd As SqlCommand = con.CreateCommand()
        cmd.CommandType = CommandType.StoredProcedure
        cmd.CommandText = "SelectActiveUsers"
        records.DataSource = cmd.ExecuteReader()
        records.DataBind()
        save.Text = "SAVE"
        userid.Value = ""
        name.Text = ""
        code.Text = ""
        con.Close()
    End Sub

    Protected Sub save_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim c_name As String = name.Text
        Dim c_code As String = code.Text

        If con.State = ConnectionState.Closed Then
            con.Open()

            If userid.Value <> "" Then
                Dim cmd As SqlCommand = con.CreateCommand()
                cmd.CommandType = CommandType.StoredProcedure
                cmd.CommandText = "ExistingUser2"
                cmd.Parameters.Add("@Country_Name", SqlDbType.VarChar).Value = c_name
                cmd.Parameters.Add("@Country_ID", SqlDbType.Int).Value = userid.Value
                Dim user As Integer = CInt(cmd.ExecuteScalar())

                If user <> 0 Then
                    msgDisplay.Text = "Cannot Update to this Country Name as it already exists! Try a different Country Name!"
                Else
                    Dim cmd2 As SqlCommand = con.CreateCommand()
                    cmd2.CommandType = CommandType.StoredProcedure
                    cmd2.CommandText = "UpdateCountry"
                    cmd2.Parameters.Add("@Country_Name", SqlDbType.VarChar).Value = c_name
                    cmd2.Parameters.Add("@Country_Code", SqlDbType.VarChar).Value = c_code
                    cmd2.Parameters.Add("@Country_ID", SqlDbType.Int).Value = userid.Value
                    cmd2.ExecuteNonQuery()
                    msgDisplay.Text = "Record Updated!"
                    load_data()
                End If
            Else
                Dim cmd As SqlCommand = con.CreateCommand()
                cmd.CommandType = CommandType.StoredProcedure
                cmd.CommandText = "ExistingUser"
                cmd.Parameters.Add("@Country_Name", SqlDbType.VarChar).Value = c_name
                Dim user As Integer = CInt(cmd.ExecuteScalar())

                If user <> 0 Then
                    msgDisplay.Text = "This Country Name already exists! Try a different Country Name!"
                Else
                    Dim cmd2 As SqlCommand = con.CreateCommand()
                    cmd2.CommandType = CommandType.StoredProcedure
                    cmd2.CommandText = "InsertCountry"
                    cmd2.Parameters.Add("@Country_Name", SqlDbType.VarChar).Value = c_name
                    cmd2.Parameters.Add("@Country_Code", SqlDbType.VarChar).Value = c_code
                    cmd2.ExecuteNonQuery()
                    msgDisplay.Text = "New Record Inserted"
                    load_data()
                End If
            End If
        Else
            Response.Write("<script> alert('Connection Error 404') </script>")
        End If
    End Sub

    Protected Sub exit_Click(ByVal sender As Object, ByVal e As EventArgs)
        Response.Redirect("~/StudentLogin.aspx")
    End Sub

    Protected Sub edit_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim button As Button = CType(sender, Button)
        Dim id = button.CommandArgument
        If con.State = ConnectionState.Open Then con.Close()
        con.Open()
        Dim cmd As SqlCommand = con.CreateCommand()
        cmd.CommandType = CommandType.StoredProcedure
        cmd.CommandText = "GetCountryInfo"
        cmd.Parameters.Add("@Country_ID", SqlDbType.Int).Value = id
        Dim reader As SqlDataReader = cmd.ExecuteReader()

        While reader.Read()
            name.Text = reader.GetValue(0).ToString()
            code.Text = reader.GetValue(1).ToString()
            userid.Value = id
            save.Text = "UPDATE"
        End While
    End Sub

    Protected Sub delete_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim button As Button = CType(sender, Button)
        Dim id = button.CommandArgument
        If con.State = ConnectionState.Open Then con.Close()
        con.Open()
        Dim cmd As SqlCommand = con.CreateCommand()
        cmd.CommandType = CommandType.StoredProcedure
        cmd.CommandText = "DeleteCountry"
        cmd.Parameters.Add("@Country_ID", SqlDbType.Int).Value = id
        cmd.ExecuteNonQuery()
        load_data()
    End Sub
End Class
