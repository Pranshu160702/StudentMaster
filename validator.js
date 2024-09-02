function validateForm() {
    var name = document.getElementById('stu_name').value;
    var email = document.getElementById('email').value;
    var aadhar = document.getElementById('aadharno').value;
    var mobile = document.getElementById('mobile').value;
    var date = document.getElementById('stu_dob').value;
    var gender = document.querySelector('input[name="Gender"]:checked');
    var country = document.getElementById('countryDDL');
    var state = document.getElementById('stateDDL');
    var city = document.getElementById('cityDDL');
    var address = document.getElementById('address').value;
    var image = document.getElementById('HiddenFieldStudentImageUrl').value;
    var errorMessage = "";

    if (name === "") {
        errorMessage += "Name cannot be empty.\n";
    } else if (name.length < 6) {
        errorMessage += "Name must be at least 6 characters long.\n";
    }

    if (email === "") {
        errorMessage += "Email cannot be empty.\n";
    }

    if (aadhar === 0) {
        errorMessage += "Aadhar cannot be empty.\n";
    } else if (!/^\d{12}$/.test(aadhar)) {
        errorMessage += "Aadhar must be exactly 12 digits.\n";
    }

    if (mobile === 0) {
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

    document.getElementById('stu_dob').setAttribute('max', maxDate);
}

window.onload = setMaxDate;
