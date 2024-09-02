<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site1.Master" CodeBehind="StudentLogin.aspx.vb" Inherits="NagarNigamVB.StudentLogin" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="Stylesheet" href="css/StudentLoginStyle.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="background">
        <div class="shape"></div>
        <div class="shape"></div>
    </div>
    <div class="form-container">
        <label class="label">Email ID</label>
        <asp:TextBox ID="email" CssClass="input" runat="server" placeholder="Student's Email" required></asp:TextBox>
        <label class="label">Password</label>
        <asp:TextBox ID="pass" CssClass="input" runat="server" placeholder="Password" TextMode="Password" required></asp:TextBox>
        <asp:Button ID="loginBtn" CssClass="loginBtn" runat="server" Text="LOG IN" />
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
