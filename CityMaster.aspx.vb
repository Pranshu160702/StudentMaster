Imports System
Imports System.Data
Imports System.Data.SqlClient

Public Class CityMaster
    Inherits System.Web.UI.Page

    Dim str As String = ConfigurationManager.ConnectionStrings("connectionString").ConnectionString
    Dim con As SqlConnection = New SqlConnection(Str)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            CheckUserLogin()
            load_countryList()
            load_stateList()
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
        cmd.CommandText = "SelectActiveCity"
        records.DataSource = cmd.ExecuteReader()
        records.DataBind()

        save.Text = "SAVE"
        cityid.Value = ""
        countryid.Value = ""
        countryDDL.SelectedValue = 0
        stateDDL.SelectedValue = 0
        city.Text = ""
        pin.Text = ""
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

    End Sub

    Private Sub load_stateList()
        If con.State = ConnectionState.Closed Then con.Open()

        'DropDownList of Countries from tblCountry  
        Dim cmd As SqlCommand = con.CreateCommand()
        cmd.CommandType = CommandType.StoredProcedure
        cmd.CommandText = "StateList"
        cmd.Parameters.Add("@Country_ID", SqlDbType.Int).Value = countryid.Value
        stateDDL.DataSource = cmd.ExecuteReader()
        stateDDL.DataTextField = "State_Name"
        stateDDL.DataValueField = "State_ID"
        stateDDL.DataBind()
        stateDDL.Items.Insert(0, New ListItem("-- Select State --", 0))
        countryid.Value = 0
    End Sub

    Protected Sub save_Click(ByVal sender As Object, ByVal e As EventArgs)

        If countryDDL.SelectedValue = 0 Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "myScriptKey", "alert('Please Select Country !');", True)
            Exit Sub
        End If

        If stateDDL.SelectedValue = 0 Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "myScriptKey", "alert('Please Select State !');", True)
            Exit Sub
        End If

        Dim c_id As Integer = countryDDL.SelectedValue
        Dim s_id As Integer = stateDDL.SelectedValue
        Dim ci_name As String = city.Text
        Dim pin_c As String = pin.Text

        If con.State = ConnectionState.Closed Then
            con.Open()

            If cityid.Value <> "" Then
                Dim cmd As SqlCommand = con.CreateCommand()
                cmd.CommandType = CommandType.StoredProcedure
                cmd.CommandText = "ExistingCity2"
                cmd.Parameters.Add("@City_Name", SqlDbType.VarChar).Value = ci_name
                cmd.Parameters.Add("@City_ID", SqlDbType.Int).Value = cityid.Value
                cmd.Parameters.Add("@State_ID", SqlDbType.Int).Value = s_id
                Dim user As Integer = CInt(cmd.ExecuteScalar())

                If user <> 0 Then
                    msgDisplay.Text = "Cannot Update to this City Name as it already exists! Try a different City Name!"
                Else
                    Dim cmd2 As SqlCommand = con.CreateCommand()
                    cmd2.CommandType = CommandType.StoredProcedure
                    cmd2.CommandText = "UpdateCity"
                    cmd2.Parameters.Add("@City_Name", SqlDbType.VarChar).Value = ci_name
                    cmd2.Parameters.Add("@Pincode", SqlDbType.VarChar).Value = pin_c
                    cmd2.Parameters.Add("@State_ID", SqlDbType.Int).Value = s_id
                    cmd2.Parameters.Add("@City_ID", SqlDbType.Int).Value = cityid.Value
                    cmd2.ExecuteNonQuery()
                    msgDisplay.Text = "Record Updated!"
                    load_data()
                End If
            Else
                Dim cmd As SqlCommand = con.CreateCommand()
                cmd.CommandType = CommandType.StoredProcedure
                cmd.CommandText = "ExistingCity"
                cmd.Parameters.Add("@State_ID", SqlDbType.Int).Value = s_id
                cmd.Parameters.Add("@City_Name", SqlDbType.VarChar).Value = ci_name
                Dim user As Integer = CInt(cmd.ExecuteScalar())

                If user <> 0 Then
                    msgDisplay.Text = "This City Name already exists! Try a different City Name!"
                Else
                    Dim cmd2 As SqlCommand = con.CreateCommand()
                    cmd2.CommandType = CommandType.StoredProcedure
                    cmd2.CommandText = "InsertCity"
                    cmd2.Parameters.Add("@City_Name", SqlDbType.VarChar).Value = ci_name
                    cmd2.Parameters.Add("@Pincode", SqlDbType.VarChar).Value = pin_c
                    cmd2.Parameters.Add("@State_ID", SqlDbType.Int).Value = s_id
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
        cmd.CommandText = "GetCityInfo"
        cmd.Parameters.Add("@City_ID", SqlDbType.Int).Value = id
        Dim reader As SqlDataReader = cmd.ExecuteReader()

        While reader.Read()
            countryDDL.SelectedValue = reader.GetValue(0).ToString()
            countryDDL_SelectedIndexChanged(Nothing, Nothing)
            stateDDL.SelectedValue = reader.GetValue(1).ToString()
            city.Text = reader.GetValue(2).ToString()
            pin.Text = reader.GetValue(3).ToString()
            cityid.Value = id
            save.Text = "UPDATE"
        End While
        reader.Close()
    End Sub

    Protected Sub delete_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim button As Button = CType(sender, Button)
        Dim id = button.CommandArgument
        If con.State = ConnectionState.Open Then con.Close()
        con.Open()
        Dim cmd As SqlCommand = con.CreateCommand()
        cmd.CommandType = CommandType.StoredProcedure
        cmd.CommandText = "DeleteCity"
        cmd.Parameters.Add("@City_ID", SqlDbType.Int).Value = id
        cmd.ExecuteNonQuery()
        msgDisplay.Text = "Record Deleted Successfully!"
        load_data()
    End Sub

    Protected Sub countryDDL_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles countryDDL.SelectedIndexChanged
        If countryDDL.SelectedValue <> 0 Then
            countryid.Value = countryDDL.SelectedValue
        End If
        load_stateList()
    End Sub
End Class
