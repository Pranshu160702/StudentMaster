<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="AnalysisReportViewer.aspx.vb" Inherits="NagarNigamVB.AnalysisReportViewer" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
        <CR:CrystalReportViewer ID="AnalysisCrystalReportViewer" runat="server" AutoDataBind="true"  Width="100%" Height="600px" />
    </form>
</body>
</html>
