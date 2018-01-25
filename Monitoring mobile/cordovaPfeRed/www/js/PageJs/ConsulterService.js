
(function () {
    "use strict";
    document.addEventListener('deviceready', onDeviceReady.bind(this), true);
    function onDeviceReady() {
        // Gérer les événements de suspension et de reprise Cordova
        document.addEventListener('pause', onPause.bind(this), false);
        document.addEventListener('resume', onResume.bind(this), false);

        $.ajax({
            type: 'get',
            url: 'http://online.bt.com.tn/monitoring/Service1.svc/ConsulterService',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: true,

            success: function (data, textStatus, jqXHR) {
                var json = jQuery.parseJSON(data);
                drawTable(json.service);
            },
            error: function (a, e, d) {
                var err = a.responseText + ' ' + e + ' ' + d;
                alert(err);
            }
        });
    }

    function drawTable(data) {
        for (var i = 0; i < data.length; i++) {
            drawRow(data[i]);
        }
    }

    function drawRow(rowData) {
        var row = $("<tr />")
        $("#serviceDataTable").append(row); //this will append tr element to table... keep its reference for a while since we will add cels into it
        row.append($("<td>" + rowData.CodeService + "</td>"));
        row.append($("<td>" + rowData.type + "</td>"));
        row.append($("<td>" + rowData.operation1 + "</td>"));
        row.append($("<td>" + rowData.operation2 + "</td>"));   
        row.append($("<td>" + rowData.resultatAttendu + "</td>"));
    }

    function onPause() {
        // TODO: cette application a été suspendue. Enregistrez l'état de l'application ici.
    };

    function onResume() {
        // TODO: cette application a été réactivée. Restaurez l'état de l'application ici.
    };
})();