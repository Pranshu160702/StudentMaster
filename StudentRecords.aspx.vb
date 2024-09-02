Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports System.Web
Imports System.Web.UI
Imports System.Reflection
Imports System.Configuration
Imports Microsoft.Office.Interop.Excel
Imports System.Runtime.InteropServices

Public Class StudentRecords
    Inherits System.Web.UI.Page

    Dim str As String = ConfigurationManager.ConnectionStrings("connectionString").ConnectionString
    Dim con As SqlConnection = New SqlConnection(Str)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            CheckUserLogin()
            load_data()
            load_allLists()
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
        cmd.CommandText = "SelectActiveStudentRecord"
        records.DataSource = cmd.ExecuteReader()
        records.DataBind()

        con.Close()
    End Sub

    Private Sub load_allLists()
        If con.State = ConnectionState.Closed Then con.Open()

        'DropDownList of Countries from tblCountry  
        Dim cmd As SqlCommand = con.CreateCommand()
        cmd.CommandType = CommandType.StoredProcedure
        cmd.CommandText = "SearchCountryList"
        countryDDL.DataSource = cmd.ExecuteReader()
        countryDDL.DataTextField = "country_name"
        countryDDL.DataValueField = "ID"
        countryDDL.DataBind()
        countryDDL.Items.Insert(0, New ListItem("-- Select Country --", 0))
        countryDDL.SelectedIndex = 0
        con.Close()

        If con.State = ConnectionState.Closed Then con.Open()

        'DropDownList of Countries from tblCountry  
        Dim cmd2 As SqlCommand = con.CreateCommand()
        cmd2.CommandType = CommandType.StoredProcedure
        cmd2.CommandText = "SearchStateList"
        stateDDL.DataSource = cmd2.ExecuteReader()
        stateDDL.DataTextField = "State_Name"
        stateDDL.DataValueField = "State_ID"
        stateDDL.DataBind()
        stateDDL.Items.Insert(0, New ListItem("-- Select State --", 0))
        con.Close()

        If con.State = ConnectionState.Closed Then con.Open()

        'DropDownList of Countries from tblCountry  
        Dim cmd3 As SqlCommand = con.CreateCommand()
        cmd3.CommandType = CommandType.StoredProcedure
        cmd3.CommandText = "SearchCityList"
        cityDDL.DataSource = cmd3.ExecuteReader()
        cityDDL.DataTextField = "City_Name"
        cityDDL.DataValueField = "City_ID"
        cityDDL.DataBind()
        cityDDL.Items.Insert(0, New ListItem("-- Select City --", 0))
        con.Close()

    End Sub

    Protected Sub edit_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim button As System.Web.UI.WebControls.Button = CType(sender, System.Web.UI.WebControls.Button)
        Dim id = button.CommandArgument
        Session("editStu_ID") = id
        Response.Redirect("StudentRegistration.aspx")
    End Sub

    Protected Sub delete_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim button As System.Web.UI.WebControls.Button = CType(sender, System.Web.UI.WebControls.Button)
        Dim id = button.CommandArgument
        If con.State = ConnectionState.Open Then con.Close()
        con.Open()
        Dim cmd As SqlCommand = con.CreateCommand()
        cmd.CommandType = CommandType.StoredProcedure
        cmd.CommandText = "DeleteStudent"
        cmd.Parameters.Add("@Student_ID", SqlDbType.Int).Value = id
        cmd.ExecuteNonQuery()

        load_data()
    End Sub

    Protected Sub print_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim button As System.Web.UI.WebControls.Button = CType(sender, System.Web.UI.WebControls.Button)
        Dim id = button.CommandArgument
        Session("printStu_ID") = id
        Response.Redirect("StudentCrystalReportViewerWebform.aspx")
    End Sub

    Protected Sub search_Click(ByVal sender As Object, ByVal e As EventArgs) Handles search.Click
        Dim studentName As String = name.Text
        Dim studentEmail As String = email.Text
        Dim studentMobile As String = mobile.Text
        Dim studentAadharNumber As String = aadhar.Text
        Dim StudentCountry_ID As Integer = countryDDL.SelectedValue
        Dim StudentState_ID As Integer = stateDDL.SelectedValue
        Dim StudentCity_ID As Integer = cityDDL.SelectedValue

        If con.State = ConnectionState.Closed Then con.Open()

        Dim cmd As SqlCommand = con.CreateCommand()
        cmd.CommandType = CommandType.StoredProcedure
        cmd.CommandText = "SearchStudents"
        cmd.Parameters.Add("@StudentName", SqlDbType.VarChar).Value = studentName
        cmd.Parameters.Add("@StudentEmail", SqlDbType.VarChar).Value = studentEmail
        cmd.Parameters.Add("@StudentAadharNumber", SqlDbType.VarChar).Value = studentAadharNumber
        cmd.Parameters.Add("@StudentMobile", SqlDbType.VarChar).Value = studentMobile
        cmd.Parameters.Add("@Country_ID", SqlDbType.Int).Value = StudentCountry_ID
        cmd.Parameters.Add("@State_ID", SqlDbType.Int).Value = StudentState_ID
        cmd.Parameters.Add("@City_ID", SqlDbType.Int).Value = StudentCity_ID
        records.DataSource = cmd.ExecuteReader()
        records.DataBind()
        If records.Rows.Count = 0 Then
            noRecordsMsg.Visible = True
            noRecordsMsg.Text = "No record for such student was found!"
        Else
            noRecordsMsg.Visible = False
        End If
    End Sub

    Protected Sub export_Click(ByVal sender As Object, ByVal e As EventArgs) Handles export.Click

        ' Create a sample DataTable
        If con.State = ConnectionState.Open Then con.Close()
        con.Open()
        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alertMessage", "alert('Data Exported Succesfully!')", True)
        Dim cmd As SqlCommand = con.CreateCommand()
        cmd.CommandType = CommandType.StoredProcedure
        cmd.CommandText = "ExportData"
        Dim reader As SqlDataReader = cmd.ExecuteReader()
        Dim data As System.Data.DataTable = New System.Data.DataTable()
        data.Load(reader)

        ExportToExcel(data)
    End Sub

    Private Sub ExportToExcel(ByVal dt As System.Data.DataTable)
        Try
            ' Create a new Excel application instance
            Dim excelApp As New Application()
            Dim workbook As Workbook = excelApp.Workbooks.Add()
            Dim worksheet As Worksheet = workbook.Worksheets(1)

            ' Add column headers
            For colIndex As Integer = 1 To dt.Columns.Count
                worksheet.Cells(1, colIndex) = dt.Columns(colIndex - 1).ColumnName
            Next

            ' Add rows
            For rowIndex As Integer = 0 To dt.Rows.Count - 1
                For colIndex As Integer = 0 To dt.Columns.Count - 1
                    worksheet.Cells(rowIndex + 2, colIndex + 1) = dt.Rows(rowIndex)(colIndex).ToString()
                Next
            Next

            ' Set the response properties
            Dim tempFilePath As String = "D:\Pranshu\NagarNigamVB\NagarNigamVB\Exports\StudentRecordsExport"
            workbook.SaveAs(tempFilePath)
            workbook.Close()
            excelApp.Quit()

            ' Ensure Excel processes are released
            Marshal.ReleaseComObject(worksheet)
            Marshal.ReleaseComObject(workbook)
            Marshal.ReleaseComObject(excelApp)

            ' Send the file to the client
            Response.Clear()
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
            Response.AddHeader("Content-Disposition", "attachment; filename=Export.xlsx")
            Response.WriteFile(tempFilePath)
            Response.End()

        Catch ex As Exception
            ' Handle any errors that occurred during export
            Response.Write("Error exporting to Excel: " & ex.Message)
        Finally
            ' Clean up any remaining COM objects
            GC.Collect()
            GC.WaitForPendingFinalizers()
        End Try
    End Sub
End Class