Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared

Public Class AnalysisReportViewer
    Inherits System.Web.UI.Page

    Dim str As String = ConfigurationManager.ConnectionStrings("connectionString").ConnectionString
    Dim con As SqlConnection = New SqlConnection(Str)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (Not IsPostBack And Session("reportToGet").ToString() <> "") Then
            Dim rtg As String = Session("reportToGet").ToString()
            If rtg = "cwa" Then
                load_cwaReport()
            ElseIf rtg = "swa" Then
                load_swaReport()
            ElseIf rtg = "ciwa" Then
                load_ciwaReport()
            ElseIf rtg = "gwa" Then
                load_gwaReport()
            End If
        End If
    End Sub

    Protected Sub load_cwaReport()
        If con.State = ConnectionState.Open Then con.Close()
        con.Open()
        Dim cmd As SqlCommand = con.CreateCommand()
        cmd.CommandType = CommandType.StoredProcedure
        cmd.CommandText = "cwastudents"
        Dim reader As SqlDataReader = cmd.ExecuteReader()
        Dim dt As DataTable = New DataTable()
        dt.Load(reader)
        Dim reportDocument As New ReportDocument()
        Dim reportPath As String = Server.MapPath("~/cwaReport.rpt")
        reportDocument.Load(reportPath)
        reportDocument.SetDataSource(dt)
        AnalysisCrystalReportViewer.ReportSource = reportDocument
        reportDocument.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, False, "CWA_REPORT")
    End Sub

    Protected Sub load_swaReport()
        If con.State = ConnectionState.Open Then con.Close()
        con.Open()
        Dim cmd As SqlCommand = con.CreateCommand()
        cmd.CommandType = CommandType.StoredProcedure
        cmd.CommandText = "swastudents"
        Dim reader As SqlDataReader = cmd.ExecuteReader()
        Dim dt As DataTable = New DataTable()
        dt.Load(reader)
        Dim reportDocument As New ReportDocument()
        Dim reportPath As String = Server.MapPath("~/swaReport.rpt")
        reportDocument.Load(reportPath)
        reportDocument.SetDataSource(dt)
        AnalysisCrystalReportViewer.ReportSource = reportDocument
        reportDocument.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, False, "SWA_REPORT")
    End Sub

    Protected Sub load_ciwaReport()
        If con.State = ConnectionState.Open Then con.Close()
        con.Open()
        Dim cmd As SqlCommand = con.CreateCommand()
        cmd.CommandType = CommandType.StoredProcedure
        cmd.CommandText = "ciwastudents"
        Dim reader As SqlDataReader = cmd.ExecuteReader()
        Dim dt As DataTable = New DataTable()
        dt.Load(reader)
        Dim reportDocument As New ReportDocument()
        Dim reportPath As String = Server.MapPath("~/ciwaReport.rpt")
        reportDocument.Load(reportPath)
        reportDocument.SetDataSource(dt)
        AnalysisCrystalReportViewer.ReportSource = reportDocument
        reportDocument.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, False, "CIWA_REPORT")
    End Sub

    Protected Sub load_gwaReport()
        If con.State = ConnectionState.Open Then con.Close()
        con.Open()
        Dim cmd As SqlCommand = con.CreateCommand()
        cmd.CommandType = CommandType.StoredProcedure
        cmd.CommandText = "gwastudents"
        Dim reader As SqlDataReader = cmd.ExecuteReader()
        Dim dt As DataTable = New DataTable()
        dt.Load(reader)
        Dim reportDocument As New ReportDocument()
        Dim reportPath As String = Server.MapPath("~/gwaReport.rpt")
        reportDocument.Load(reportPath)
        reportDocument.SetDataSource(dt)
        AnalysisCrystalReportViewer.ReportSource = reportDocument
        reportDocument.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, False, "GWA_REPORT")
    End Sub

End Class