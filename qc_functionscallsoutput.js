function callWebServiceNimbus() {

    try {

        var id = Xrm.Page.data.entity.getId();

        if (id == null || id == '' || id == undefined) {
            alert('Es necesario Guardar la llamada...');
            return;
        }

        var phonenumber = Xrm.Page.getAttribute("phonenumber").getValue();

        if (phonenumber == null || phonenumber == '' || phonenumber == undefined) {
            alert('El número de teléfono es requerido...');
            return;
        }

        var userNimbus = {
            username: '',
            password: ''
        };

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
                
                var retrievedUser = JSON.parse(http.responseText).d;

                userNimbus = {
                    username: retrievedUser.its_usuarionimbus,
                    password: retrievedUser.its_contrasena
                };
                
                if (userNimbus.username == null || userNimbus.password == null) {
                    alert('Sin información de acceso del usuario de Nimbus');
                    return;
                }

                var xmlhttp = new XMLHttpRequest();
                xmlhttp.open('POST', 'http://quierocasa.nimbuscc.mx/ccs/servicios.php/AGENT_CALL', true);

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
                                '<numero xsi:type="xsd:string">' + phonenumber + '</numero>' +
                            '</ser:AGENT_CALL>' +
                        '</soapenv:Body>' +
                    '</soapenv:Envelope>';

                xmlhttp.onreadystatechange = function () {
                    if (xmlhttp.readyState == 4) {
                        if (xmlhttp.status == 200) {
                            var nodeError = xmlhttp.responseXML.getElementsByTagName('error');
                            var nodeDescripcion = xmlhttp.responseXML.getElementsByTagName('descripcion');
                            var idOperacion = xmlhttp.responseXML.getElementsByTagName('id_operacion');
                            var message = '';

                            if (nodeError[0].innerHTML == '0') {
                                message = 'Se establecio la llamada con Nimbus de forma exitosa.';
                            }
                            else {
                                message = 'No se pudo establecer la llamada con Nimbus para el número de teléfono ' + phonenumber + '. \n Codigo error: ' + nodeError[0].innerHTML
                                + '. \n Descripción: ' + nodeDescripcion[0].innerHTML + '. \n Id de operación: ' + idOperacion[0].innerHTML;
                            }

                            alert(message);
                        }
                    }
                }
                xmlhttp.setRequestHeader('Content-Type', 'text/xml');
                xmlhttp.send(sr);

            }
        };

        http.send();
    }
    catch (ex) {
        console.log(ex);
    }
}