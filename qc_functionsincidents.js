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
function preFilterClient() {
    Xrm.Page.getControl("customerid").addPreSearch(function () { addLookupFilterContacts(); });
}
function addLookupFilterContacts() {
    var identifiedPhone = Xrm.Page.getAttribute("its_telefono").getValue();

    console.log(identifiedPhone);

    if (identifiedPhone != null) {
        var fetchXml = '';
        fetchXml += "<filter type='or'>"
        fetchXml += "<condition attribute='qc_telefonodecontacto' operator='eq' value='" + identifiedPhone + "' />";
        fetchXml += "<condition attribute='mobilephone' operator='eq' value='" + identifiedPhone + "' />";
        fetchXml += "<condition attribute='qc_otrotelefono' operator='eq' value='" + identifiedPhone + "' />";
        fetchXml += "<condition attribute='qc_telefonodeoficina' operator='eq' value='" + identifiedPhone + "' />";
        fetchXml += "</filter>";

        console.log(fetchXml);

        Xrm.Page.getControl("customerid").addCustomFilter(fetchXml, "contact");
    }
}

function updateNimbusIFrame() {
    var urlNimbus = Xrm.Page.getAttribute("description").getValue();
    if (urlNimbus != null || urlNimbus != '' || urlNimbus != undefined) {
        var nimbusIframe = Xrm.Page.ui.controls.get("IFRAME_nimbus");
        var newUrl = urlNimbus;
        nimbusIframe.setSrc(newUrl);
    }
}