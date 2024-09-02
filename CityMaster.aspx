<%@ Page Language="vb" Title="City Master" MasterPageFile="~/Site1.Master" AutoEventWireup="false" CodeBehind="CityMaster.aspx.vb" Inherits="NagarNigamVB.CityMaster" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="Stylesheet" href="css/CityMasterStyle.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:HiddenField ID="countryid" runat="server" Value="0" />
                <asp:HiddenField ID="cityid" runat="server" Value="" />
                <div>
                    <asp:Label ID="msgDisplay" runat="server" Text="" Font-Names="Times New Roman"></asp:Label><br />
                    <br />
                    <asp:Label ID="headerLbl" CssClass="header" runat="server" Text="CITY MASTER"></asp:Label>
                </div>
                <asp:Label ID="countryLbl" runat="server" Text="Country : "></asp:Label>
                <asp:DropDownList ID="countryDDL" CssClass="countryDDL" runat="server" OnSelectedIndexChanged="countryDDL_SelectedIndexChanged" AutoPostBack="true">
                </asp:DropDownList>
                <asp:Label ID="stateLbl" CssClass="stateLbl" runat="server" Text="State : "></asp:Label>
                <asp:DropDownList ID="stateDDL" CssClass="stateDDL" runat="server">
                </asp:DropDownList>
                <br />
                <p>
                    <asp:Label ID="cityLbl" CssClass="cityLbl" runat="server" Text="City : "></asp:Label>
                    <asp:TextBox ID="city" CssClass="city" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rq1" CssClass="rq1" runat="server" ErrorMessage="City cannot be empty"
                        ControlToValidate="city" ValidationGroup="SAVE"></asp:RequiredFieldValidator>
                </p>
                <br />
                <p>
                    <asp:Label ID="pinLbl" CssClass="pinLbl" runat="server" Text="Pincode : "></asp:Label>
                    <asp:TextBox ID="pin" CssClass="pin" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rq2" CssClass="rq2" runat="server" ErrorMessage="Pincode cannot be empty"
                        ControlToValidate="pin" ValidationGroup="SAVE"></asp:RequiredFieldValidator>
                </p>
                <br />
                <p>
                    <asp:Button ID="save" CssClass="save" runat="server" Text="SAVE" OnClick="save_Click" ValidationGroup="SAVE" />
                    <asp:Button ID="exit" CssClass="exit" runat="server" Text="EXIT" OnClientClick="return confirmationExit();"
                        OnClick="exit_Click" />
                </p>
                <br />
                <asp:GridView ID="records" CssClass="records" runat="server" AutoGenerateColumns="False">
                    <Columns>
                        <asp:BoundField DataField="City_ID" HeaderText="City ID" ItemStyle-Width="100px" />
                        <asp:BoundField DataField="Country_Name" HeaderText="Country Name" ItemStyle-Width="250px" />
                        <asp:BoundField DataField="State_Name" HeaderText="State Name" ItemStyle-Width="250px" />
                        <asp:BoundField DataField="City_Name" HeaderText="City" ItemStyle-Width="250px" />
                        <asp:BoundField DataField="Pincode" HeaderText="Pincode" ItemStyle-Width="250px" />
                       
                        <asp:TemplateField HeaderText="Edit Record" ItemStyle-Width="250px">
                            <ItemTemplate>
                                <asp:Button ID="edit" CssClass="editBtn" runat="server" Text="Edit" OnClick="edit_Click"
                                    CommandArgument='<%#Eval("City_ID")%>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Delete Record" ItemStyle-Width="250px">
                            <ItemTemplate>
                                <asp:Button ID="delete" CssClass="deleteBtn" runat="server" Text="Delete" OnClientClick="return confirmationDelete();"
                                    OnClick="delete_Click" CommandArgument='<%#Eval("City_ID")%>' />
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
