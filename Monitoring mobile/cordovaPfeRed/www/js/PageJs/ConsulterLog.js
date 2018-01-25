(function () {
    "use strict";
    document.addEventListener('deviceready', onDeviceReady.bind(this), true);  
    function onDeviceReady() {
        // Gérer les événements de suspension et de reprise Cordova
        document.addEventListener('pause', onPause.bind(this), false);
        document.addEventListener('resume', onResume.bind(this), false);

            $.ajax({
                type: 'get',
                url: 'http://online.bt.com.tn/monitoring/Service1.svc/consulterlog',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: true,
               
                success: function (data, textStatus, jqXHR) {
                    var json = jQuery.parseJSON(data);
                    drawTable(json.log);   
                },
                error: function (a, e, d) {
                    var err = a.responseText + ' ' + e + ' ' + d;
                    alert(  err);
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
            $("#logDataTable").append(row); //this will append tr element to table... keep its reference for a while since we will add cels into it

            if (rowData.CodeServeur == 0) {
                var a = 'etat global'
                row.append($("<td>" + a + "</td>"));
            } else
                row.append($("<td>" + rowData.CodeServeur + "</td>"));

            
            if (rowData.CodeService == 0 && rowData.CodeServeur>0) {
                var a = 'etat de serveur'
                row.append($("<td>" + a + "</td>"));
            } else
                row.append($("<td>" + rowData.CodeService + "</td>"));

            row.append($("<td>" + rowData.Messagelog + "</td>"));
            row.append($("<td>" + rowData.Temps + "</td>"));
          
            var t = rowData.Reussi;
            if(t==0)
                row.append($("<td><img style='height: 21px;'  src='../img/red.png' /></td>"));
           else if (t == 1)
               row.append($("<td><img style='height: 21px;' src='../img/jaune.png' /></td>"));
           else if (t == 2)
               row.append($("<td><img style='height: 21px;' src='../img/vert.png' /></td>"));
        }
    function onPause() {
        // TODO: cette application a été suspendue. Enregistrez l'état de l'application ici.
    };

    function onResume() {
        // TODO: cette application a été réactivée. Restaurez l'état de l'application ici.
    };
})();