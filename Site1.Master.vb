Public Class Site1
    Inherits System.Web.UI.MasterPage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        CheckUserLogin()
    End Sub

    Protected Sub CheckUserLogin()
        If CType(Session("Login"), Integer) = 1 Then
            loginLbl.Text = "Log Out"
        Else
            loginLbl.Text = "Log In"
        End If
    End Sub

    Private Sub countryWise_Click() Handles cwa.Click
        Session("reportToGet") = "cwa"
        Response.Redirect("AnalysisReportViewer.aspx")
    End Sub

    Private Sub stateWise_Click() Handles swa.Click
        Session("reportToGet") = "swa"
        Response.Redirect("AnalysisReportViewer.aspx")
    End Sub

    Private Sub cityWise_Click() Handles ciwa.Click
        Session("reportToGet") = "ciwa"
        Response.Redirect("AnalysisReportViewer.aspx")
    End Sub

    Private Sub genderWise_Click() Handles gwa.Click
        Session("reportToGet") = "gwa"
        Response.Redirect("AnalysisReportViewer.aspx")
    End Sub

End Class