//$.urlParam = function (name) {
//    var results = new RegExp('[\?&]' + name + '=([^&#]*)').exec(window.location.href);
//    return results[1] || 0;
//}
//var x = $.urlParam('id');
//var x = 1;

function Privilege() {
    $(document).on("click", "#alogout", function (e) {
        Session.set("userid", null);
        window.location.href = "index.html";
        
    });
   
    var x = Session.get("userid");
    if (x == null) window.location.href = "index.html";
    //var elem = document.getElementById('1');
    //elem.parentNode.removeChild(elem);
  
    $.ajax({
        type: 'get',
        url: 'http://online.bt.com.tn/monitoring/Service1.svc/Verif/' + x,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: true,

        success: function (data) {
            //var x = Session.get("a");
            $.ajax({
                type: 'get',
                url: 'http://online.bt.com.tn/monitoring/Service1.svc/consulterdroit/' + x,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: true,

                success: function (data1) {
                    var json = jQuery.parseJSON(data1);
                    var d = json.droit;
                    for (var i = 0; i < d.length; i++) {
                        var url = "DashboardServeur.html?id=" + d[i].CodeServeur;
                        var text = " id serveur " + d[i].CodeServeur;

                        $("#aa").append('<li><a href="' + url + '">' + text + '</a></li>');

                    }

                },
                error: function (a, e, d) {
                    //var err = a.responseText + ' ' + e + ' ' + d;
                    //alert(err);
                }
            });
            
            var json = jQuery.parseJSON(data);
            if(data==false){
              
                var elem = document.getElementById('aConsulterEmploye');
                elem.parentNode.removeChild(elem);
                var elem = document.getElementById('aConsulterLog');
                elem.parentNode.removeChild(elem);
                var elem = document.getElementById('aConsulterService');
                elem.parentNode.removeChild(elem);
            }
            else{
              
                var elem = document.getElementById('aConsulterLogParServeur');
                elem.parentNode.removeChild(elem);
                var elem = document.getElementById('aConsulterServiceParServeur');
                elem.parentNode.removeChild(elem);
                
            }
            //var d = json.serveur;
            //for (var i = 0; i < d.length; i++) {
            //    var url = "index.html?id=" + d[i].CodeServeur;
            //    var text = d[i].CodeServeur + " serveur " + d[i].nom;
               
            //    $("#aa").append('<li><a href="' +url + '">' + text+ '</a></li>');

               
            //}

        },
        
        
        error: function (a, e, d) {
            //var err = a.responseText + ' ' + e + ' ' + d;
            //alert(err);
        }
    });
}

 