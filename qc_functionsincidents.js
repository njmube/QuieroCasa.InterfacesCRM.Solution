function changeOwnerCase() {
    var entityName = Xrm.Page.data.entity.getEntityName();
    var ownerid = Xrm.Page.getAttribute("ownerid").getValue();
    var ticketnumber = Xrm.Page.getAttribute("ticketnumber").getValue();

    if (entityName == "incident") {
        var setUservalue = new Array();
        setUservalue[0] = new Object();
        setUservalue[0].id = Xrm.Page.context.getUserId();
        setUservalue[0].entityType = 'systemuser';
        setUservalue[0].name = Xrm.Page.context.getUserName();

        if ((ticketnumber != null && ownerid[0].id.toString() == "{B9CF4419-BEAA-E611-8122-0050568A0735}") && (setUservalue[0].id.toString() != "{B9CF4419-BEAA-E611-8122-0050568A0735}")) {
            Xrm.Page.getAttribute("ownerid").setValue(setUservalue);
            Xrm.Page.ui.setFormNotification("Se actualizo el propietario del caso", "Nimbus", "1");
        }
    }
}
function filterPhoneNumbersIdentifiedContacts() {
    var identifiedPhone = Xrm.Page.getAttribute("its_telefono").getValue();
    var input = document.getElementById('numerostelefonicosidentificados_findCriteria');

    if (input != null) {
        input.value = identifiedPhone;
        var button = document.getElementById('numerostelefonicosidentificados_findCriteriaButton');

        if (button != null) {
            Xrm.Page.ui.setFormNotification("boton encontrado", "Nimbus", "1");
            button.click();
        }
        else {
            Xrm.Page.ui.setFormNotification("boton fallido", "Nimbus", "1");
        }
    }
    else {
        Xrm.Page.ui.setFormNotification("input fallido", "Nimbus", "1");
    }
}

function filterContacts() {

    var identifiedPhoneValue = Xrm.Page.getAttribute("its_telefono").getValue();

    if (identifiedPhoneValue != null) {
        var list_filter = "&lt;filter type='and'&gt;" + "&lt;condition attribute='qc_telefonodecontacto' operator='eq' value='" + identifiedPhoneValue + "' /&gt;" + "&lt;/filter&gt;";
        Xrm.Page.getControl("numerostelefonicosidentificados").addCustomFilter(list_filter, "contact");
    }
}