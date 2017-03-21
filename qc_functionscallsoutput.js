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

        var its_extension = Xrm.Page.getAttribute("its_extension").getValue();

        if (its_extension == null || its_extension == '' || its_extension == undefined) {
            alert('La extensión es requerida...');
            return;
        }

        var its_tipodellamada = Xrm.Page.getAttribute("its_tipodellamada").getValue();

        if (its_tipodellamada == null || its_tipodellamada == '' || its_tipodellamada == undefined) {
            alert('El tipo de llamada es requerida...');
            return;
        }

        var tipo = '';

        if (its_tipodellamada == '960760000') {
            tipo = 'local'
        }
        else if (its_tipodellamada == '960760001') {
            tipo = 'celular'
        }
        else {
            alert('El código del tipo de llamada no es válido...');
            return;
        }

        console.log(id, phonenumber, its_extension, tipo);

        var userIntegracion = {
            username: '@S0p0rt3#',
            password: '#quierocasa!',
            user_agente: 'pruebasnimbus'
        };

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
                xmlhttp.open('POST', 'http://quierocasa.nimbuscc.mx/ccs/servicios.php/CLICK_TO_CALL', true);

                var sr =
                    '<?xml version="1.0" encoding="utf-8"?>' +
                    '<soapenv:Envelope ' +
                        'xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" ' +
                        'xmlns:xsd="http://www.w3.org/2001/XMLSchema" ' +
                        'xmlns:soapenv="http://schemas.xmlsoap.org/soap/envelope/">' +
                        'xmlns:ser="http://www.nimbuscc.mx/ccs/servicios.php' +
                        '<soapenv:Body>' +
                            ' <ser:CLIC_TO_CALL soapenv:encodingStyle="http://schemas.xmlsoap.org/soap/encoding/">' +
                                '<usuario xsi:type="xsd:string">' + userIntegracion.username + '</usuario>' +
                                '<contrasena xsi:type="xsd:string">' + userIntegracion.password + '</contrasena>' +
                                '<user_agente xsi:type="xsd:string">' + userIntegracion.user_agente + '</user_agente>' +
                                '<numero xsi:type="xsd:string">' + phonenumber + '</numero>' +
                                '<tipo xsi:type="xsd:string">' + tipo + '</tipo>' +
                                '<idllamada xsi:type="xsd:string">' + id + '</idllamada>' +
                                '<extension xsi:type="xsd:string">' + its_extension + '</extension>' +
                            ' </ser:CLIC_TO_CALL>' +
                        '</soapenv:Body>' +
                    '</soapenv:Envelope>';

                xmlhttp.onreadystatechange = function () {
                    if (xmlhttp.readyState == 4 && xmlhttp.status == 200) {

                        var nodeError = xmlhttp.responseXML.getElementsByTagName('error');
                        var nodeDescripcion = xmlhttp.responseXML.getElementsByTagName('descripcion');
                        var idOperacion = '';
                        var url = '';
                        var message = '';

                        if (nodeError[0].innerHTML == '0') {
                            url = xmlhttp.responseXML.getElementsByTagName('url');
                            message = 'La llamada ha sido enviada de manera correcta a la extension.';
                        }
                        else {
                            idOperacion = xmlhttp.responseXML.getElementsByTagName('id_operacion');
                            message = 'No se pudo establecer la llamada con Nimbus para el número de teléfono ' + phonenumber + '. \n Codigo error: ' + nodeError[0].innerHTML
                            + '. \n Descripción: ' + nodeDescripcion[0].innerHTML + '. \n Id de operación: ' + idOperacion[0].innerHTML;
                        }

                        alert(message);
                    }
                }

                xmlhttp.setRequestHeader('Content-Type', 'text/xml');
                xmlhttp.send(sr);
            }
            
        };

        http.send();
    }
    catch (ex) {
        alert('No se pudo establecer la llamada con Nimbus. Error: ' + ex);
    }
}