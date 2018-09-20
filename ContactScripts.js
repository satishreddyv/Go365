

function ButtonClick()
{
    alert("Function call from button")
}

function OnContactLoad() {

    var accountid = Xrm.Page.data.entity.attributes.get("parentcustomerid").getValue()[0].id;
   var accid = accountid.replace("{", "").replace("}", "");

    var req = new XMLHttpRequest();
    req.open("GET", Xrm.Page.context.getClientUrl() + "/api/data/v8.2/accounts(" + accid +")?$select=accountnumber,address1_city,address1_line1,address1_postalcode,name", true);
    req.setRequestHeader("OData-MaxVersion", "4.0");
    req.setRequestHeader("OData-Version", "4.0");
    req.setRequestHeader("Accept", "application/json");
    req.setRequestHeader("Content-Type", "application/json; charset=utf-8");
    req.setRequestHeader("Prefer", "odata.include-annotations=\"*\"");
    req.send(null);
    req.onreadystatechange = function () {
        if (this.readyState === 4) {
            req.onreadystatechange = null;
            if (this.status === 200) {
                var result = JSON.parse(this.response);
                var accountnumber = result["accountnumber"];
                var address1_city = result["address1_city"];
                var address1_line1 = result["address1_line1"];
                var address1_postalcode = result["address1_postalcode"];
                var name = result["name"];

                Xrm.Page.ui.setFormNotification("Account Number: " + accountnumber + "\n" + "Address :" + address1_city + "," + address1_line1, "INFO", "2");


            } else {
                Xrm.Utility.alertDialog(this.statusText);
            }
        }
    };
   
}

function OnLastNameChange() {

}

function OnPhoneChange() {
    var telephone = Xrm.Page.getAttribute("telephone1").getValue();

    if (isNaN(telephone)) {
        Xrm.Page.getControl("telephone1").setNotification("Enter proper phone number", "1");
    }
    else {
        Xrm.Page.getControl("telephone1").clearNotification("1");
    }
}

function OnShippingMethodChange() {
    var method = Xrm.Page.getAttribute("address1_shippingmethodcode").getText();

    if (method == "DHL") {
        Xrm.Page.getControl("address1_freighttermscode").setDisabled(true);
        Xrm.Page.ui.setFormNotification("Freight Terms are only available for DHL for the moment. Please contact administrator for more information.", "INFO", "2");
            
    } else {
        Xrm.Page.getControl("address1_freighttermscode").setDisabled(false);
        Xrm.Page.ui.clearFormNotification("2");
    }

}