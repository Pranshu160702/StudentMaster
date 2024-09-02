<%@ Page Language="vb" Title="State Master" MasterPageFile="~/Site1.Master" AutoEventWireup="false" CodeBehind="StateMaster.aspx.vb" Inherits="NagarNigamVB.StateMaster" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="Stylesheet" href="css/StateMasterStyle.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:HiddenField ID="stateid" runat="server" Value="" />
                <div>
                    <asp:Label ID="msgDisplay" runat="server" Text="" Font-Names="Times New Roman"></asp:Label><br />
                    <br />
                    <asp:Label ID="headerLbl" CssClass="header" runat="server" Text="STATE MASTER"></asp:Label>
                </div>
                <asp:Label ID="countryLbl" CssClass="countryLbl" runat="server" Text="Choose Country : "></asp:Label>
                <asp:DropDownList CssClass="countryDDL" ID="countryDDL" runat="server">
                </asp:DropDownList>
                <br />
                <p>
                    <asp:Label ID="stateNameLbl" runat="server" Text="State Name : "></asp:Label>
                    <asp:TextBox ID="state" CssClass="state" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rq1" CssClass="rq1" runat="server" ErrorMessage="State name cannot be empty"
                        ControlToValidate="state" ValidationGroup="SAVE"></asp:RequiredFieldValidator>
                </p>
                <br />
                <p>
                    <asp:Label ID="shortNameLbl" runat="server" Text="Short Name : "></asp:Label>
                    <asp:TextBox ID="shortn" CssClass="shortn" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rq2" CssClass="rq2" runat="server" ErrorMessage="Short name cannot be empty"
                        ControlToValidate="shortn" ValidationGroup="SAVE"></asp:RequiredFieldValidator>
                </p>
                <br />
                <p>
                    <asp:Button ID="save" CssClass="save" runat="server" Text="SAVE" OnClick="save_Click" ValidationGroup="SAVE" />
                    <asp:Button ID="exit" CssClass="exit" runat="server" Text="EXIT" OnClientClick="return confirmationExit();"
                        OnClick="exit_Click" />
                </p>
                <br />
                <asp:GridView ID="records" runat="server" AutoGenerateColumns="False" CssClass="records">
                    <Columns>
                        <asp:BoundField DataField="State_ID" HeaderText="State ID" ItemStyle-Width="100px" />
                        <asp:BoundField DataField="Country_Name" HeaderText="Country Name" ItemStyle-Width="250px" />
                        <asp:BoundField DataField="State_Name" HeaderText="State Name" ItemStyle-Width="250px" />
                        <asp:BoundField DataField="Short_Name" HeaderText="Short Name" ItemStyle-Width="250px" />
                       
                        <asp:TemplateField HeaderText="Edit Record" ItemStyle-Width="250px">
                            <ItemTemplate>
                                <asp:Button ID="edit" CssClass="editBtn" runat="server" Text="Edit" OnClick="edit_Click"
                                    CommandArgument='<%#Eval("State_ID")%>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Delete Record" ItemStyle-Width="250px">
                            <ItemTemplate>
                                <asp:Button ID="delete" CssClass="deleteBtn" runat="server" Text="Delete" OnClientClick="return confirmationDelete();"
                                    OnClick="delete_Click" CommandArgument='<%#Eval("State_ID")%>' />
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

