function xmlToJson(xml) {

    // Create the return object
    var obj = {};

    if (xml.nodeType == 1) { // element
        // do attributes
        if (xml.attributes.length > 0) {
            obj["@attributes"] = {};
            for (var j = 0; j < xml.attributes.length; j++) {
                var attribute = xml.attributes.item(j);
                obj["@attributes"][attribute.nodeName] = attribute.nodeValue;
            }
        }
    } else if (xml.nodeType == 3) { // text
        obj = xml.nodeValue;
    }

    // do children
    if (xml.hasChildNodes()) {
        for (var i = 0; i < xml.childNodes.length; i++) {
            var item = xml.childNodes.item(i);
            var nodeName = item.nodeName;
            if (typeof (obj[nodeName]) == "undefined") {
                obj[nodeName] = xmlToJson(item);
            } else {
                if (typeof (obj[nodeName].push) == "undefined") {
                    var old = obj[nodeName];
                    obj[nodeName] = [];
                    obj[nodeName].push(old);
                }
                obj[nodeName].push(xmlToJson(item));
            }
        }
    }
    return obj;
}

function getUserInfo() {
    try {
        var context;
        var serverUrl;
        var UserID;
        var ODataPath;
        context = Xrm.Page.context;
        serverUrl = context.getClientUrl();
        UserID = context.getUserId();
        ODataPath = serverUrl + "/XRMServices/2011/OrganizationData.svc";
        var http = new XMLHttpRequest();
        http.open("GET", ODataPath + "/SystemUserSet(guid'" + UserID + "')", true);
        http.setRequestHeader("Accept", "application/json");
        http.setRequestHeader("Content-Type", "application/json; charset=utf-8");
        http.onreadystatechange = function () {
            if (http.readyState == 4 && http.status == 200) {
                console.log('status=200');
                var retrievedUser = JSON.parse(http.responseText).d;
                var userNimbus = {
                    username: retrievedUser.its_usuarionimbus,
                    password: retrievedUser.its_contrasena
                };

                console.log(userNimbus);

                return userNimbus;
            }
        };

        http.send();
    }
    catch (ex) {
        console.log('Error');
        console.log(ex.message);
    }
}

function callWebServiceNimbus() {

    try {

        var userNimbus = getUserInfo();

        if (userNimbus.username == null || userNimbus.password == null) {
            console.log('Sin información de acceso del usuario de Nimbus');
            return;
        }

        var xmlhttp = new XMLHttpRequest();
        xmlhttp.open('POST', 'http://quierocasa.nimbuscc.mx/ccs/servicios.php/AGENT_CALL', true);

        // build SOAP request
        var sr =
            '<?xml version="1.0" encoding="utf-8"?>' +
            '<soapenv:Envelope ' +
                'xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" ' +
                'xmlns:xsd="http://www.w3.org/2001/XMLSchema" ' +
                'xmlns:soapenv="http://schemas.xmlsoap.org/soap/envelope/">' +
                'xmlns:ser="http://www.nimbuscc.mx/ccs/servicios.php' +
                '<soapenv:Body>' +
                    '<ser:AGENT_CALL soapenv:encodingStyle="http://schemas.xmlsoap.org/soap/encoding/">' +
                        '<usuario xsi:type="xsd:string">' + userNimbus.username + '</usuario>' +
                        '<contrasena xsi:type="xsd:string">' + userNimbus.password + '</contrasena>' +
                        '<user_agente xsi:type="xsd:string">test</user_agente>' +
                        '<ext_agente xsi:type="xsd:string">123456789</ext_agente>' +
                        '<numero xsi:type="xsd:string">5543866791</numero>' +
                    '</ser:AGENT_CALL>' +
                '</soapenv:Body>' +
            '</soapenv:Envelope>';

        xmlhttp.onreadystatechange = function () {
            if (xmlhttp.readyState == 4) {
                if (xmlhttp.status == 200) {
                    var jsonText = JSON.stringify(xmlToJson(xmlhttp.responseXML));
                    //console.log(jsonText);
                    var nodeError = xmlhttp.responseXML.getElementsByTagName('error');
                    console.log('codigonodeError: ' + nodeError[0].innerHTML);
                    var nodeDescripcion = xmlhttp.responseXML.getElementsByTagName('descripcion');
                    console.log('Descripcion: ' + nodeDescripcion[0].innerHTML);
                    var idOperacion = xmlhttp.responseXML.getElementsByTagName('id_operacion');
                    console.log('Id Operacion: ' + idOperacion[0].innerHTML);
                }
            }
        }
        xmlhttp.setRequestHeader('Content-Type', 'text/xml');
        xmlhttp.send(sr);
    }
    catch (ex) {
        console.log(ex);
    }
}