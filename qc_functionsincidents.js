function changeOwnerCase() {
    var entityName = Xrm.Page.data.entity.getEntityName();
    var ownerid = Xrm.Page.getAttribute("ownerid").getValue();
    var ticketnumber = Xrm.Page.getAttribute("ticketnumber").getValue();
    var casocreadopor = Xrm.Page.getAttribute("its_casocreadopor").getValue();

    if (entityName == "incident") {
        var setUservalue = new Array();
        setUservalue[0] = new Object();
        setUservalue[0].id = Xrm.Page.context.getUserId();
        setUservalue[0].entityType = 'systemuser';
        setUservalue[0].name = Xrm.Page.context.getUserName();

        if (ticketnumber != null && (casocreadopor == '947980000' || casocreadopor == '947980002')) {
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
    var nimbusIframe = Xrm.Page.ui.controls.get("IFRAME_nimbus");
    var urlNimbus = Xrm.Page.getAttribute("its_urldegrabacion").getValue();

    console.log('URL: ' + urlNimbus);

    if (urlNimbus != null) {
        nimbusIframe.setSrc(urlNimbus);
    }
    
}


function Iframe_OnReadyStateComplete () {
    var iFrame = Xrm.Page.ui.controls.get('IFRAME_docmgmt');
    var url = iFrame.getSrc();
    if (url.indexOf("blank") != -1) {
        setiFrameUrl();
}
}

function setiFrameUrl() {
    var url = 'http://www.google.com';
    Xrm.Page.getControl('IFRAME_docmgmt').setSrc(url);
}