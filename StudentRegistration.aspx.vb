Imports System
Imports System.IO
Imports System.Web.UI
Imports System.Data
Imports System.Data.SqlClient

Public Class StudentRegistration
    Inherits System.Web.UI.Page

    Dim str As String = ConfigurationManager.ConnectionStrings("connectionString").ConnectionString
    Dim con As SqlConnection = New SqlConnection(Str)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If CType(Session("editStu_ID"), Integer) = 0 Then
                CheckUserLogin()
                load_countryList()
                load_stateList()
                load_cityList()
            Else
                load_countryList()
                load_stateList()
                load_cityList()
                load_Editor()
            End If
        End If
    End Sub

    Protected Sub CheckUserLogin()
        If CType(Session("Login"), Integer) = 0 Then
            Response.Redirect("StudentLogin.aspx")
        End If
    End Sub

    Protected Sub load_Editor()
        Dim editStudent_ID As Integer = CType(Session("editStu_ID"), Integer)
        If con.State = ConnectionState.Open Then con.Close()
        con.Open()
        Dim cmd As SqlCommand = con.CreateCommand()
        cmd.CommandType = CommandType.StoredProcedure
        cmd.CommandText = "GetEditedStudentInfo"
        cmd.Parameters.Add("@Student_ID", SqlDbType.Int).Value = editStudent_ID
        Dim reader As SqlDataReader = cmd.ExecuteReader()

        While reader.Read()
            stu_name.Text = reader.GetValue(0).ToString()
            email.Text = reader.GetValue(1).ToString()
            aadharno.Text = reader.GetValue(2).ToString()
            mobile.Text = reader.GetValue(3).ToString()
            Dim myDate As DateTime = reader.GetValue(4)
            stu_dob.Text = myDate.ToString("yyyy-MM-dd")
            Dim gender As String = reader.GetValue(5).ToString()
            Select Case gender
                Case "male"
                    male.Checked = True
                Case "female"
                    female.Checked = True
            End Select
            Dim hobbies As String = reader.GetValue(6).ToString()
            Dim hobbyList As List(Of String) = hobbies.Split(New String() {" "}, StringSplitOptions.RemoveEmptyEntries).ToList()
            For Each hobby In hobbyList
                Select Case hobby
                    Case "Cricket"
                        hob1.Checked = True
                    Case "Hockey"
                        hob2.Checked = True
                    Case "Football"
                        hob3.Checked = True
                End Select
            Next
            countryDDL.SelectedValue = reader.GetValue(7).ToString()
            countryDDL_SelectedIndexChanged(Nothing, Nothing)
            stateDDL.SelectedValue = reader.GetValue(8).ToString()
            stateDDL_SelectedIndexChanged(Nothing, Nothing)
            cityDDL.SelectedValue = reader.GetValue(9).ToString()
            address.Text = reader.GetValue(10).ToString()
            HiddenFieldStudentImageUrl.Value = reader.GetValue(11).ToString()
            ImageStudent.ImageUrl = HiddenFieldStudentImageUrl.Value
            save.Text = "UPDATE"
        End While
        reader.Close()
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

    Private Sub load_cityList()
        If con.State = ConnectionState.Closed Then con.Open()

        'DropDownList of Countries from tblCountry  
        Dim cmd As SqlCommand = con.CreateCommand()
        cmd.CommandType = CommandType.StoredProcedure
        cmd.CommandText = "CityList"
        cmd.Parameters.Add("@State_ID", SqlDbType.Int).Value = stateid.Value
        cityDDL.DataSource = cmd.ExecuteReader()
        cityDDL.DataTextField = "City_Name"
        cityDDL.DataValueField = "City_ID"
        cityDDL.DataBind()
        cityDDL.Items.Insert(0, New ListItem("-- Select City --", 0))
        stateid.Value = 0
    End Sub

    Protected Sub save_Click(ByVal sender As Object, ByVal e As EventArgs)

        Dim studentName As String = stu_name.Text
        Dim studentEmail As String = email.Text
        Dim studentMobile As String = mobile.Text
        Dim studentAadharNumber As String = aadharno.Text
        Dim studentDateOfBirth As String = stu_dob.Text
        Dim studentGender As String = If(male.Checked, "male", If(female.Checked, "female", ""))
        Dim studentHobbies As String = (If(hob1.Checked, "Cricket ", "")) & (If(hob2.Checked, "Hockey ", "")) & (If(hob3.Checked, "Football ", ""))
        Dim StudentCity_ID As Integer = cityDDL.SelectedValue
        Dim studentAddress As String = address.Text
        Dim studentImage As String = HiddenFieldStudentImageUrl.Value

        If con.State = ConnectionState.Closed Then
            con.Open()

            If Session("editStu_ID") = 0 Then
                Dim cmd As SqlCommand = con.CreateCommand()
                cmd.CommandType = CommandType.StoredProcedure
                cmd.CommandText = "ExistingStudentRecord"
                cmd.Parameters.Add("@StudentEmail", SqlDbType.VarChar).Value = studentEmail
                cmd.Parameters.Add("@StudentAadharNumber", SqlDbType.VarChar).Value = studentAadharNumber
                Dim user As Integer = CInt(cmd.ExecuteScalar())

                If user = 0 Then
                    Dim cmd2 As SqlCommand = con.CreateCommand()
                    cmd2.CommandType = CommandType.StoredProcedure
                    cmd2.CommandText = "InsertStudentRecord"
                    cmd2.Parameters.Add("@StudentName", SqlDbType.VarChar).Value = studentName
                    cmd2.Parameters.Add("@StudentEmail", SqlDbType.VarChar).Value = studentEmail
                    cmd2.Parameters.Add("@StudentMobile", SqlDbType.VarChar).Value = studentMobile
                    cmd2.Parameters.Add("@StudentAadharNumber", SqlDbType.VarChar).Value = studentAadharNumber
                    cmd2.Parameters.Add("@StudentDateOfBirth", SqlDbType.VarChar).Value = studentDateOfBirth
                    cmd2.Parameters.Add("@StudentGender", SqlDbType.VarChar).Value = studentGender
                    cmd2.Parameters.Add("@StudentHobbies", SqlDbType.VarChar).Value = studentHobbies
                    cmd2.Parameters.Add("@City_ID", SqlDbType.Int).Value = StudentCity_ID
                    cmd2.Parameters.Add("@StudentAddress", SqlDbType.VarChar).Value = studentAddress
                    cmd2.Parameters.Add("@StudentImage", SqlDbType.VarChar).Value = studentImage
                    cmd2.ExecuteNonQuery()
                    Dim script As String = "alert('Record Inserted Successfully!'); window.location.href = 'StudentRegistration.aspx';"
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "RedirectScript", script, True)
                Else
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alertMessage", "alert('Student Already Exists (Enter a different Email or Aadhar Number)')", True)
                End If
            Else
                Dim cmd As SqlCommand = con.CreateCommand()
                cmd.CommandType = CommandType.StoredProcedure
                cmd.CommandText = "ExistingStudentRecord2"
                cmd.Parameters.Add("@StudentEmail", SqlDbType.VarChar).Value = studentEmail
                cmd.Parameters.Add("@StudentAadharNumber", SqlDbType.VarChar).Value = studentAadharNumber
                cmd.Parameters.Add("@Student_ID", SqlDbType.Int).Value = CType(Session("editStu_ID"), Integer)
                Dim user As Integer = CInt(cmd.ExecuteScalar())

                If user = 0 Then
                    Dim cmd2 As SqlCommand = con.CreateCommand()
                    cmd2.CommandType = CommandType.StoredProcedure
                    cmd2.CommandText = "UpdateStudentRecord"
                    cmd2.Parameters.Add("@Student_ID", SqlDbType.Int).Value = CType(Session("editStu_ID"), Integer)
                    cmd2.Parameters.Add("@StudentName", SqlDbType.VarChar).Value = studentName
                    cmd2.Parameters.Add("@StudentEmail", SqlDbType.VarChar).Value = studentEmail
                    cmd2.Parameters.Add("@StudentMobile", SqlDbType.VarChar).Value = studentMobile
                    cmd2.Parameters.Add("@StudentAadharNumber", SqlDbType.VarChar).Value = studentAadharNumber
                    cmd2.Parameters.Add("@StudentDateOfBirth", SqlDbType.VarChar).Value = studentDateOfBirth
                    cmd2.Parameters.Add("@StudentGender", SqlDbType.VarChar).Value = studentGender
                    cmd2.Parameters.Add("@StudentHobbies", SqlDbType.VarChar).Value = studentHobbies
                    cmd2.Parameters.Add("@City_ID", SqlDbType.Int).Value = StudentCity_ID
                    cmd2.Parameters.Add("@StudentAddress", SqlDbType.VarChar).Value = studentAddress
                    cmd2.Parameters.Add("@StudentImage", SqlDbType.VarChar).Value = studentImage
                    cmd2.ExecuteNonQuery()
                    Session("editStu_ID") = 0
                    Dim script As String = "alert('Record Updated Successfully!'); window.location.href = 'StudentRegistration.aspx';"
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "RedirectScript", script, True)
                Else
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alertMessage", "alert('This Student Already Exists!')", True)
                End If
            End If
        Else
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alertMessage", "alert('Connection Error 404')", True)
        End If
    End Sub

    Protected Sub exit_Click(ByVal sender As Object, ByVal e As EventArgs)
        Response.Redirect("~/StudentLogin.aspx")
    End Sub

    Protected Sub countryDDL_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles countryDDL.SelectedIndexChanged
        If countryDDL.SelectedValue <> 0 Then
            countryid.Value = countryDDL.SelectedValue
        End If
        load_stateList()
        load_cityList()
    End Sub

    Protected Sub stateDDL_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles stateDDL.SelectedIndexChanged
        If stateDDL.SelectedValue <> 0 Then
            stateid.Value = stateDDL.SelectedValue
        End If
        load_cityList()
    End Sub

    Protected Sub clear_Click(ByVal sender As Object, ByVal e As EventArgs) Handles clear.Click
        Session("editStu_ID") = 0
        Response.Redirect("StudentRegistration.aspx")
    End Sub

    Protected Sub fetch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles fetch.Click
        Response.Redirect("StudentRecords.aspx")
    End Sub

    Protected Sub ButtonUpload_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ButtonUpload.Click
        If stu_image.HasFile = True Then
            Dim fileName As String = Path.GetFileName(stu_image.PostedFile.FileName)
            Dim filePath As String = Server.MapPath("~/StudentImages/") & fileName

            stu_image.SaveAs(filePath)
            LabelNotify.Text = "File Uploaded Successfully!"

            ImageStudent.ImageUrl = "~/StudentImages/" & fileName
            HiddenFieldStudentImageUrl.Value = ImageStudent.ImageUrl
        Else
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alertMessage", "alert('Please Choose a image first before uploading')", True)
        End If
    End Sub
End Class