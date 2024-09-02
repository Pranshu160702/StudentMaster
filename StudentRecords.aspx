<%@ Page Language="vb" Title="Student Records" MasterPageFile="~/Site1.Master" AutoEventWireup="false" CodeBehind="StudentRecords.aspx.vb"
    Inherits="NagarNigamVB.StudentRecords" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="Stylesheet" href="css/StudentRecordsStyle.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <br /><br />
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div>
                    <asp:Panel ID="SearchPanel" CssClass="searchPanel" runat="server">
                        <div>
                            <asp:Label ID="Label1" CssClass="Labels" runat="server" Text="Name : "></asp:Label>
                            <asp:TextBox ID="name" CssClass="Inputs" runat="server"></asp:TextBox>
                            <asp:Label ID="Label2" CssClass="Labels" runat="server" Text="Email"></asp:Label>
                            <asp:TextBox ID="email" CssClass="Inputs" runat="server"></asp:TextBox>
                        </div>
                        <div>
                            <asp:Label ID="Label3" CssClass="Labels" runat="server" Text="Aadhar : "></asp:Label>
                            <asp:TextBox ID="aadhar" CssClass="Inputs" runat="server" TextMode="Number"></asp:TextBox>
                            <asp:Label ID="Label4" CssClass="Labels" runat="server" Text="Mobile : "></asp:Label>
                            <asp:TextBox ID="mobile" CssClass="Inputs" runat="server" TextMode="Number"></asp:TextBox>
                        </div>
                        <div>
                            <asp:Label ID="Label5" CssClass="Labels" runat="server" Text="Country : "></asp:Label>
                            <asp:DropDownList CssClass="ddls" ID="countryDDL" runat="server">
                            </asp:DropDownList>
                            <asp:Label ID="Label6" CssClass="Labels" runat="server" Text="State : "></asp:Label>
                            <asp:DropDownList CssClass="ddls" ID="stateDDL" runat="server">
                            </asp:DropDownList>
                        </div>
                        <div>
                            <asp:Label ID="Label7" CssClass="Labels" runat="server" Text="City : "></asp:Label>
                            <asp:DropDownList CssClass="ddls" ID="cityDDL" runat="server">
                            </asp:DropDownList>
                            <asp:Button ID="search" CssClass="search" runat="server" Text="SEARCH" OnClick="search_Click" />
                            <asp:Button ID="export" CssClass="export" runat="server" Text="EXPORT" OnClick="export_Click" />
                        </div>
                    </asp:Panel>
                </div>
                <br />
                <asp:HiddenField ID="countryid" runat="server" Value="0" />
                <asp:HiddenField ID="cityid" runat="server" Value="0" />
                <div>
                    <asp:Label ID="headerLbl" CssClass="header" runat="server" Text="STUDENT RECORDS"></asp:Label>
                </div>
                <br />
                <asp:Label ID="noRecordsMsg" CssClass="norecords" runat="server" Text="Label" Visible="false"></asp:Label>
                <asp:GridView ID="records" runat="server" AutoGenerateColumns="False" CssClass="gridview">
                    <Columns>
                        <asp:BoundField DataField="Student_ID" HeaderText="ID" />
                        <asp:BoundField DataField="StudentImg" HeaderText="Path" Visible="false" />
                        <asp:TemplateField HeaderText="Profile">
                            <ItemTemplate>
                                <asp:Image runat="server" ImageUrl='<%# Eval("StudentImg") %>' Width="100px" Height="100px">
                                </asp:Image>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Student_Name" HeaderText="Student Name" />
                        <asp:BoundField DataField="Email" HeaderText="Email ID" />
                        <asp:BoundField DataField="Mobile" HeaderText="Mobile No." />
                        <asp:BoundField DataField="Aadhar" HeaderText="Aadhar No." />
                        <asp:BoundField DataField="Student_DOB" HeaderText="D.O.B." />
                        <asp:BoundField DataField="Gender" HeaderText="Gender" />
                        <asp:BoundField DataField="Hobbies" HeaderText="Hobby" />
                        <asp:BoundField DataField="City_Name" HeaderText="City" />
                        <asp:BoundField DataField="State_Name" HeaderText="State" />
                        <asp:BoundField DataField="Country_Name" HeaderText="Country" />
                        <asp:TemplateField HeaderText="Edit Record" ItemStyle-Width="250px">
                            <ItemTemplate>
                                <asp:Button ID="edit" CssClass="editBtn" runat="server" Text="Edit" CommandArgument='<%#Eval("Student_ID")%>'
                                    OnClick="edit_Click" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Delete Record" ItemStyle-Width="250px">
                            <ItemTemplate>
                                <asp:Button ID="delete" CssClass="deleteBtn" runat="server" Text="Delete" OnClientClick="return confirmationDelete();"
                                    OnClick="delete_Click" CommandArgument='<%#Eval("Student_ID")%>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Print Record" ItemStyle-Width="250px">
                            <ItemTemplate>
                                <asp:Button ID="print" CssClass="printBtn" runat="server" Text="Print" OnClick="print_Click"
                                    CommandArgument='<%#Eval("Student_ID")%>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </ContentTemplate>
        </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <script type="text/javascript">
        document.getElementById('<%= aadhar.ClientID %>').addEventListener('input', function (e) {
            if (this.value.length > 12) {
                this.value = this.value.slice(0, 12);
            }
        });

        document.getElementById('<%= mobile.ClientID %>').addEventListener('input', function (e) {
            if (this.value.length > 10) {
                this.value = this.value.slice(0, 10);
            }
        });

        function confirmationDelete() {
            return confirm("Are you sure you want to delete?")
        }
        function confirmationExit() {
            return confirm("Are you sure you want to exit the application?")
        }
    </script>
</asp:Content>
