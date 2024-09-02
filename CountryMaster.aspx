<%@ Page Language="vb" Title="Country Master" MasterPageFile="~/Site1.Master" AutoEventWireup="false" CodeBehind="CountryMaster.aspx.vb"
    Inherits="NagarNigamVB.CountryMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="Stylesheet" href="css/CountryMasterStyle.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:HiddenField ID="userid" runat="server" Value="" />
                <div>
                    <asp:Label ID="msgDisplay" runat="server" Text="" Font-Names="Times New Roman"></asp:Label><br />
                    <br />
                    <asp:Label ID="headerLbl" CssClass="header" runat="server" Text="COUNTRY MASTER"></asp:Label>
                </div>
                <br />
                <p>
                    <asp:Label ID="nameLbl" runat="server" Text="Name : "></asp:Label>
                    <asp:TextBox ID="name" runat="server" CssClass="name"></asp:TextBox>
                    <asp:RequiredFieldValidator CssClass="rq1" ID="rq1" runat="server" ErrorMessage="Name cannot be empty"
                        ControlToValidate="name" ValidationGroup="SAVE"></asp:RequiredFieldValidator>
                </p>
                <br />
                <p>
                    <asp:Label ID="codeLbl" runat="server" Text="Code : "></asp:Label>
                    <asp:TextBox ID="code" runat="server" CssClass="code"></asp:TextBox>
                    <asp:RequiredFieldValidator CssClass="rq2" ID="rq2" runat="server" ErrorMessage="Code cannot be empty"
                        ControlToValidate="code" ValidationGroup="SAVE"></asp:RequiredFieldValidator>
                </p>
                <br />
                <p>
                    <asp:Button ID="save" runat="server" CssClass="save" Text="SAVE" OnClick="save_Click" ValidationGroup="SAVE" />
                    <asp:Button ID="exit" runat="server" CssClass="exit" Text="EXIT" OnClientClick="return confirmationExit();"
                        OnClick="exit_Click" />
                </p>
                <br />
                <asp:GridView ID="records" CssClass="records" runat="server" AutoGenerateColumns="False">
                    <Columns>
                        <asp:BoundField DataField="ID" HeaderText="ID" ItemStyle-Width="100px" />
                        <asp:BoundField DataField="country_name" HeaderText="Country Name" ItemStyle-Width="250px" />
                        <asp:BoundField DataField="country_code" HeaderText="Country Code" ItemStyle-Width="250px" />
                        <asp:TemplateField HeaderText="Edit Record" ItemStyle-Width="250px">
                            <ItemTemplate>
                                <asp:Button ID="edit" CssClass="editBtn" runat="server" Text="Edit" OnClick="edit_Click"
                                    CommandArgument='<%#Eval("ID")%>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Delete Record" ItemStyle-Width="250px">
                            <ItemTemplate>
                                <asp:Button ID="delete" CssClass="deleteBtn" runat="server" Text="Delete" OnClientClick="return confirmationDelete();"
                                    OnClick="delete_Click" CommandArgument='<%#Eval("ID")%>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </ContentTemplate>
        </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <script type="text/javascript">
        function confirmationDelete() {
            return confirm("Are you sure you want to delete?")
        }
        function confirmationExit() {
            return confirm("Are you sure you want to exit the application?")
        }
    </script>
</asp:Content>
