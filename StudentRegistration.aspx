<%@ Page Language="vb" Title="Registration" MasterPageFile="~/Site1.Master" AutoEventWireup="false" CodeBehind="StudentRegistration.aspx.vb"
    MaintainScrollPositionOnPostback="true" Inherits="NagarNigamVB.StudentRegistration" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="Stylesheet" href="css/StudentRegistrationStyle.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<br />
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="form-container">
                <asp:HiddenField ID="stateid" runat="server" Value="0" />
                <asp:HiddenField ID="countryid" runat="server" Value="0" />
                <asp:HiddenField ID="editEmail" runat="server" Value="" />
                <asp:HiddenField ID="editAadhar" runat="server" Value="" />
                <div>
                    <asp:Label ID="headerLbl" CssClass="header" runat="server" Text="STUDENT REGISTRATION"></asp:Label>
                </div>
                <div class="form-group">
                    <label>
                        Name</label>
                    <asp:TextBox ID="stu_name" runat="server" CssClass="form-control"></asp:TextBox>
                    <label>
                        Email</label>
                    <asp:TextBox ID="email" runat="server" CssClass="form-control" TextMode="Email"></asp:TextBox>
                </div>
                <div class="form-group">
                    <label>
                        Aadhar</label>
                    <asp:TextBox ID="aadharno" runat="server" CssClass="form-control" TextMode="Number"></asp:TextBox>
                    <label>
                        Mobile</label>
                    <asp:TextBox ID="mobile" runat="server" CssClass="form-control" TextMode="Number"></asp:TextBox>
                </div>
                <div class="form-group">
                    <label>
                        DOB</label>
                    <asp:TextBox ID="stu_dob" runat="server" TextMode="Date"></asp:TextBox>
                </div>
                <div class="form-group">
                    <label>
                        Gender</label>
                    <asp:RadioButton ID="male" runat="server" GroupName="Gender" Text="Male" />
                    <asp:RadioButton ID="female" runat="server" GroupName="Gender" Text="Female" />
                </div>
                <div class="form-group">
                    <label>
                        Choose Your Hobbies
                    </label>
                    <asp:CheckBox ID="hob1" runat="server" Text="Cricket" />
                    <asp:CheckBox ID="hob2" runat="server" Text="Hockey" />
                    <asp:CheckBox ID="hob3" runat="server" Text="Football" />
                </div>
                <div class="form-group">
                    <asp:Label ID="countryLbl" runat="server" Text="Country : "></asp:Label>
                    <asp:DropDownList ID="countryDDL" runat="server" AutoPostBack="true">
                    </asp:DropDownList>
                    <asp:Label ID="stateLbl" runat="server" Text="State : "></asp:Label>
                    <asp:DropDownList ID="stateDDL" runat="server" AutoPostBack="true">
                    </asp:DropDownList>
                    <asp:Label ID="cityLbl" runat="server" Text="City : "></asp:Label>
                    <asp:DropDownList ID="cityDDL" runat="server" AutoPostBack="true">
                    </asp:DropDownList>
                </div>
                <div class="form-group">
                    <label>
                        Address</label>
                    <asp:TextBox ID="address" runat="server" CssClass="address-form-control" TextMode="MultiLine"></asp:TextBox>
                </div>
                <div class="form-group">
                    <label>
                        Upload Your Image
                    </label>
                    <div class="imageControl">
                        <asp:HiddenField ID="HiddenFieldStudentImageUrl" runat="server" Value="" />
                        <asp:Image ID="ImageStudent" runat="server" Width="250px" Height="250px" ImageUrl="~/StudentImages/default.jpg" /><br />
                        <asp:FileUpload ID="stu_image" CssClass="stu_image" runat="server" /><br />
                        <asp:Button ID="ButtonUpload" CssClass="btnUpload" runat="server" Text="Upload Image" OnClick="ButtonUpload_Click" />
                        <asp:Label ID="LabelNotify" runat="server" Style="color: blue;"></asp:Label>
                    </div>
                </div>
                <div class="form-group">
                    <asp:Button ID="save" CssClass="save" runat="server" Text="Save" OnClientClick="return validation();" OnClick="save_Click" />
                    <asp:Button ID="exit" CssClass="exit" runat="server" Text="Exit" OnClientClick="return confirmationExit();" OnClick="exit_Click" />
                    <asp:Button ID="clear" CssClass="clear" runat="server" Text="Clear" OnClientClick="return confirmationClear();" OnClick="clear_Click" />
                    <asp:Button ID="fetch" CssClass="fetch" runat="server" Text="Fetch" OnClientClick="return confirmationFetch();" OnClick="fetch_Click" />
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="save" />
            <asp:PostBackTrigger ControlID="ButtonUpload" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <script type="text/javascript">

        document.getElementById('<%= aadharno.ClientID %>').addEventListener('input', function (e) {
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

        function confirmationClear() {
            return confirm("Are you sure you want to clear the entire application?")
        }

        function confirmationFetch() {
            return confirm("Are you sure you want to leave this page and fetch the records?")
        }

        function validation() {
            var name = document.getElementById('<%= stu_name.ClientID %>').value;
            var email = document.getElementById('<%= email.ClientID %>').value;
            var aadhar = document.getElementById('<%= aadharno.ClientID %>').value;
            var mobile = document.getElementById('<%= mobile.ClientID %>').value;
            var date = document.getElementById('<%= stu_dob.ClientID %>').value;
            var gender = document.querySelector('input[name="Gender"]:checked').value;
            var country = document.getElementById('<%= countryDDL.ClientID %>');
            var state = document.getElementById('<%= stateDDL.ClientID %>');
            var city = document.getElementById('<%= cityDDL.ClientID %>');
            var address = document.getElementById('<%= address.ClientID %>').value;
            var image = document.getElementById('<%= HiddenFieldStudentImageUrl.ClientID %>').value;
            var errorMessage = "";

            if (name === "") {
                errorMessage += "Name cannot be empty.\n";
            } else if (name.length < 6) {
                errorMessage += "Name must be at least 6 characters long.\n";
            }

            if (email === "") {
                errorMessage += "Email cannot be empty.\n";
            }

            if (aadhar === "") {
                errorMessage += "Aadhar cannot be empty.\n";
            } else if (!/^\d{12}$/.test(aadhar)) {
                errorMessage += "Aadhar must be exactly 12 digits.\n";
            }

            if (mobile === "") {
                errorMessage += "Mobile cannot be empty.\n";
            } else if (!/^\d{10}$/.test(mobile)) {
                errorMessage += "Mobile must be exactly 10 digits.\n";
            }

            if (date === "") {
                errorMessage += "Date cannot be empty.\n";
            }

            if (address === "") {
                errorMessage += "Address cannot be empty.\n";
            }

            if (!gender) {
                errorMessage += "Gender must be selected.\n";
            }

            if (country.selectedIndex === 0) {
                errorMessage += "Please select a Country.\n";
            }

            if (state.selectedIndex === 0) {
                errorMessage += "Please select a State.\n";
            }

            if (city.selectedIndex === 0) {
                errorMessage += "Please select a City.\n";
            }

            if (image === "") {
                errorMessage += "Image must be uploaded first.\n";
            }

            if (errorMessage !== "") {
                alert(errorMessage);
                return false;
            }

            return true;
        }

        function setMaxDate() {
            var today = new Date();
            var maxDate = today.toISOString().split('T')[0]; // Format YYYY-MM-DD

            document.getElementById('<%= stu_dob.ClientID %>').setAttribute('max', maxDate);
        }

    </script>
</asp:Content>
