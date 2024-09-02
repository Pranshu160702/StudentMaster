Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared

Public Class StudentCrystalReportViewerWebform
    Inherits System.Web.UI.Page

    Dim str As String = ConfigurationManager.ConnectionStrings("connectionString").ConnectionString
    Dim con As SqlConnection = New SqlConnection(Str)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (Not IsPostBack And CType(Session("printStu_ID"), Integer) <> 0) Then
            Label1.Visible = False
            load_report()
        Else
            Label1.Text = "This Student Does Not Exist Anymore!"
            Label1.Visible = True
        End If
    End Sub

    Protected Sub load_report()
        If con.State = ConnectionState.Open Then con.Close()
        con.Open()
        Dim cmd As SqlCommand = con.CreateCommand()
        cmd.CommandType = CommandType.StoredProcedure
        cmd.CommandText = "CrystalSelectStudent"
        cmd.Parameters.Add("@Student_ID", SqlDbType.Int).Value = CType(Session("printStu_ID"), Integer)
        Dim reader As SqlDataReader = cmd.ExecuteReader()
        Dim dt As DataTable = New DataTable()
        dt.Load(reader)
        Dim reportDocument As New ReportDocument()
        Dim reportPath As String = Server.MapPath("~/StudentCrystalReport.rpt")
        reportDocument.Load(reportPath)
        reportDocument.SetDataSource(dt)
        CrystalReportViewer1.ReportSource = reportDocument
        reportDocument.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, False, "Report")
    End Sub

End Class