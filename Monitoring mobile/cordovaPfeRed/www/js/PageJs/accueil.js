//$.urlParam = function (name) {
//    var results = new RegExp('[\?&]' + name + '=([^&#]*)').exec(window.location.href);
//    return results[1] || 0;
//}
//var x = $.urlParam('id');
function myFunction() {
    var x = Session.get("userid");
    if (x == null) window.location.href = "index.html";
    $.ajax({
        type: 'get',
        url: 'http://online.bt.com.tn/monitoring/Service1.svc/consulterlog',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: true,
        success: function (data, textStatus, jqXHR) {


            var json = jQuery.parseJSON(data);
            var d = json.log;
                    for (var i = 0; i <5; i++) {
                        $("#notif").append('<div class="media p-l-5"><div class="media-body"><small class="text-muted">IdScan : ' + d[i].idscan + ',Serveur : ' + d[i].CodeServeur + ',Service : ' + d[i].CodeService + ' ,   Temps : ' + d[i].Temps + '</small><br /><a class="t-overflow" href="#">' + d[i].Messagelog + '</a></div></div>');
                    }
        },
        error: function (a, e, d) {
            //var err = a.responseText + ' ' + e + ' ' + d;
            //alert(err);
        }
    });   
    $.ajax({
        type: 'get',
        url: 'http://online.bt.com.tn/monitoring/Service1.svc/alert',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: true,

        success: function (data, textStatus, jqXHR) {

            var json = jQuery.parseJSON(data);
            var d = json.log;
            for (var i = 0; i < 5; i++) {               
                $("#alert").append('<div class="media p-l-5"><div class="media-body"><small class="text-muted">IdScan : ' + d[i].idscan + ',Serveur : ' + d[i].CodeServeur + ',Service : ' + d[i].CodeService + ' ,   Temps : ' + d[i].Temps + '</small><br /><a class="t-overflow" href="#">' + d[i].Messagelog + '</a></div></div>');

            }
        },
        error: function (a, e, d) {
            //var err = a.responseText + ' ' + e + ' ' + d;
            //alert(err);
        }
    });
    $.ajax({
        type: 'get',
        url: 'http://online.bt.com.tn/monitoring/Service1.svc/statAdmin',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: true,
        success: function (data) {        
            var t = new Date();
            var json = jQuery.parseJSON(data);
            var t = json.lastdown[0].Temps;
            var lastdownd = document.getElementById("lastdownd");
            var lastdownt = document.getElementById("lastdownt");
            lastdownd.innerText = t.slice(0, 10);
            lastdownt.innerText = t.slice(11, 19);
        
            var t2= json.lastup[0].Temps;
            var lastup = document.getElementById("lastupd");
            var lastup = document.getElementById("lastupt");
            lastupd.innerText = t2.slice(0, 10);
            lastupt.innerText = t2.slice(11, 19);
            var t3 = json.lastscan[0].Temps;
            var lastscan = document.getElementById("lastscand");
            var lastscan = document.getElementById("lastscant");
            lastscand.innerText = t3.slice(0, 10);
            lastscant.innerText = t3.slice(11, 19);
            var t4 = json.lastscan[0].reussi;
            var etat = document.getElementById("etat");
            if (t4 == "2") {
                etat.innerText = "Bon";
            }else if(t4=="1") {
                etat.innerText = "Lent";
            } else {
                etat.innerText = "Echec";
            }
        },
        error: function (a, e, d) {
            //var err = a.responseText + ' ' + e + ' ' + d;
            //alert(err);
        }
    });
  //  var x = 1;
   
}

 