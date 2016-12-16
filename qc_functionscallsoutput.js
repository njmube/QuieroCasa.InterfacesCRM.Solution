function createCORSRequest(method, url) {
    var xhr = new XMLHttpRequest();
    if ("withCredentials" in xhr) {
        xhr.open(method, url, true);
    } else if (typeof XDomainRequest != "undefined") {
        xhr = new XDomainRequest();
        xhr.open(method, url);
    } else {
        xhr = null;
    }
    return xhr;
}

function callWSNimbus() {
    try {

        var url = "http://www.webservicex.net/globalweather.asmx/GetCitiesByCountry";
        var http = createCORSRequest('POST', url);

        if (!http) {
            console.log('CORS not supported');
            return;
        }

        var params = "CountryName=Mexico";
        http.setRequestHeader("Content-type", "application/x-www-form-urlencoded");
        http.setRequestHeader('Access-Control-Allow-Origin', '*');
        http.setRequestHeader('Access-Control-Allow-Methods', 'GET, PUT, POST, DELETE, OPTIONS');
        http.setRequestHeader('Access-Control-Allow-Headers', 'Content-Type, Content-Range, Content-Disposition, Content-Description');

        http.onreadystatechange = function () {
            if (http.readyState == 4 && http.status == 200) {
                console.log(http.responseText);
            }
        }

        http.send(params);
    }
    catch (ex) {
        console.log(ex.message);
    }
}