<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site1.Master"
    CodeBehind="Dashboard.aspx.vb" Inherits="NagarNigamVB.Dashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="Stylesheet" href="css/DashboardStyle.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
    <br />
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:Label ID="Label1" CssClass="header" runat="server" Text="STUDENT DASHBOARD"></asp:Label>
            <div class="gridsContainer">
            <div class="gridHolder">
            <asp:GridView ID="genderTable" runat="server" AutoGenerateColumns="False" CssClass="gridview">
                <Columns>
                    <asp:BoundField DataField="Gender" HeaderText="GENDER" ItemStyle-Width="250px" ItemStyle-Height="80px"/>
                    <asp:BoundField DataField="Students" HeaderText="STUDENTS" ItemStyle-Width="250px" ItemStyle-Height="80px" />
                </Columns>
            </asp:GridView>
            <asp:GridView ID="countryTable" runat="server" AutoGenerateColumns="False" CssClass="gridview">
                <Columns>
                    <asp:BoundField DataField="country_name" HeaderText="COUNTRY" ItemStyle-Width="250px" ItemStyle-Height="52px" />
                    <asp:BoundField DataField="Students" HeaderText="STUDENTS" ItemStyle-Width="250px" ItemStyle-Height="52px"/>
                </Columns>
            </asp:GridView>
            </div>
            <div class="gridHolder">
            <asp:GridView ID="stateTable" runat="server" AutoGenerateColumns="False" CssClass="gridview">
                <Columns>
                    <asp:BoundField DataField="State_Name" HeaderText="STATE" ItemStyle-Width="250px" ItemStyle-Height="30px"/>
                    <asp:BoundField DataField="Students" HeaderText="STUDENTS" ItemStyle-Width="250px" ItemStyle-Height="30px"/>
                </Columns>
            </asp:GridView>
            <asp:GridView ID="cityTable" runat="server" AutoGenerateColumns="False" CssClass="gridview">
                <Columns>
                    <asp:BoundField DataField="City_Name" HeaderText="CITY" ItemStyle-Width="250px" ItemStyle-Height="25px"/>
                    <asp:BoundField DataField="Students" HeaderText="STUDENTS" ItemStyle-Width="250px" ItemStyle-Height="25px"/>
                </Columns>
            </asp:GridView>
            </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
