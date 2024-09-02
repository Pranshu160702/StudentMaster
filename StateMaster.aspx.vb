Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration
Imports System.Linq

Public Class StateMaster
    Inherits System.Web.UI.Page

    Dim str As String = ConfigurationManager.ConnectionStrings("connectionString").ConnectionString
    Dim con As SqlConnection = New SqlConnection(str)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            CheckUserLogin()
            load_countryList()
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
        cmd.CommandText = "SelectActiveState"
        records.DataSource = cmd.ExecuteReader()
        records.DataBind()

        save.Text = "SAVE"
        stateid.Value = ""
        state.Text = ""
        shortn.Text = ""
        con.Close()
    End Sub

    Private Sub load_countryList()
        If con.State = ConnectionState.Closed Then con.Open()

        'DropDownList of Countries from tblCountry  
        Dim cmd As SqlCommand = con.CreateCommand()
        cmd.CommandType = CommandType.StoredProcedure
        cmd.CommandText = "CountryList"
        countryDDL.DataSource = cmd.ExecuteReader()
        countryDDL.DataTextField = "country_name"
        countryDDL.DataValueField = "ID"
        countryDDL.DataBind()
        countryDDL.Items.Insert(0, New ListItem("-- Select Country --", 0))
        con.Close()

    End Sub

    Protected Sub save_Click(ByVal sender As Object, ByVal e As EventArgs)

        If countryDDL.SelectedValue = 0 Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "myScriptKey", "alert('Please Select Country!');", True)
            Exit Sub
        End If

        Dim c_id As Integer = countryDDL.SelectedValue
        Dim s_name As String = state.Text
        Dim s_short As String = shortn.Text

        If con.State = ConnectionState.Closed Then
            con.Open()

            If stateid.Value <> "" Then
                Dim cmd As SqlCommand = con.CreateCommand()
                cmd.CommandType = CommandType.StoredProcedure
                cmd.CommandText = "ExistingState2"
                cmd.Parameters.Add("@State_Name", SqlDbType.VarChar).Value = s_name
                cmd.Parameters.Add("@Country_ID", SqlDbType.Int).Value = c_id
                cmd.Parameters.Add("@State_ID", SqlDbType.Int).Value = stateid.Value
                Dim user As Integer = CInt(cmd.ExecuteScalar())

                If user <> 0 Then
                    msgDisplay.Text = "Cannot Update to this State Name as it already exists! Try a different State Name!"
                Else
                    Dim cmd2 As SqlCommand = con.CreateCommand()
                    cmd2.CommandType = CommandType.StoredProcedure
                    cmd2.CommandText = "UpdateState"
                    cmd2.Parameters.Add("@State_Name", SqlDbType.VarChar).Value = s_name
                    cmd2.Parameters.Add("@Short_Name", SqlDbType.VarChar).Value = s_short
                    cmd2.Parameters.Add("@Country_ID", SqlDbType.Int).Value = c_id
                    cmd2.Parameters.Add("@State_ID", SqlDbType.Int).Value = stateid.Value
                    cmd2.ExecuteNonQuery()
                    msgDisplay.Text = "Record Updated!"
                    load_data()
                End If
            Else
                Dim cmd As SqlCommand = con.CreateCommand()
                cmd.CommandType = CommandType.StoredProcedure
                cmd.CommandText = "ExistingState"
                cmd.Parameters.Add("@Country_ID", SqlDbType.Int).Value = c_id
                cmd.Parameters.Add("@State_Name", SqlDbType.VarChar).Value = s_name
                Dim user As Integer = CInt(cmd.ExecuteScalar())

                If user <> 0 Then
                    msgDisplay.Text = "This State Name already exists! Try a different State Name!"
                Else
                    Dim cmd2 As SqlCommand = con.CreateCommand()
                    cmd2.CommandType = CommandType.StoredProcedure
                    cmd2.CommandText = "InsertState"
                    cmd2.Parameters.Add("@State_Name", SqlDbType.VarChar).Value = s_name
                    cmd2.Parameters.Add("@Short_Name", SqlDbType.VarChar).Value = s_short
                    cmd2.Parameters.Add("@Country_ID", SqlDbType.Int).Value = c_id
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
        cmd.CommandText = "GetStateInfo"
        cmd.Parameters.Add("@State_ID", SqlDbType.Int).Value = id
        Dim reader As SqlDataReader = cmd.ExecuteReader()

        While reader.Read()
            countryDDL.SelectedValue = reader.GetValue(0).ToString()
            state.Text = reader.GetValue(1).ToString()
            shortn.Text = reader.GetValue(2).ToString()
            stateid.Value = id
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
        cmd.CommandText = "DeleteState"
        cmd.Parameters.Add("@State_ID", SqlDbType.Int).Value = id
        cmd.ExecuteNonQuery()
        msgDisplay.Text = "Record Deleted Successfully!"
        load_data()
    End Sub
End Class
