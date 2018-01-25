$(document).ready(function () {

    var animating = false,
        submitPhase1 = 1100,
        submitPhase2 = 400,
        logoutPhase1 = 800,
        $login = $(".login"),
        $app = $(".app");

    function ripple(elem, e) {
        $(".ripple").remove();
        var elTop = elem.offset().top,
            elLeft = elem.offset().left,
            x = e.pageX - elLeft,
            y = e.pageY - elTop;
        var $ripple = $("<div class='ripple'></div>");
        $ripple.css({ top: y, left: x });
        elem.append($ripple);
    };

    $(document).on("click", ".login__submit", function (e) {

        var login = document.getElementById("login").value;
        var password = document.getElementById("pwd").value;
        var otp = document.getElementById("otp").value;
        if (animating) return;
        animating = true;
        var that = this;
        ripple($(that), e);
        $(that).addClass("processing");
        {
            $.ajax({
                type: 'get',
                url: 'http://online.bt.com.tn/monitoring/Service1.svc/validOTP/' + login + '/' + password + '/' + otp,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: true,
                success: function (data) {

                    if (data == true) {
                        $.ajax({
                            type: 'get',
                            url: 'http://online.bt.com.tn/monitoring/Service1.svc/IdEmploye/' + login + '/' + password,
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            async: true,

                            success: function (data1) {
                                var json = jQuery.parseJSON(data1);
                                Session.set("userid", json.Table[0].userid);
                                var x = json.Table[0].userid;
                                $.ajax({
                                    type: 'get',
                                    url: 'http://online.bt.com.tn/monitoring/Service1.svc/Verif/' + x,
                                    contentType: "application/json; charset=utf-8",
                                    dataType: "json",
                                    async: true,

                                    success: function (data) {
                                        if (data == false) {
                                            $.ajax({
                                                type: 'get',
                                                url: 'http://online.bt.com.tn/monitoring/Service1.svc/consulterdroit/' + x,
                                                contentType: "application/json; charset=utf-8",
                                                dataType: "json",
                                                async: true,

                                                success: function (data1) {
                                                    var json = jQuery.parseJSON(data1);
                                                    var d = json.droit;
                                                    var url = "DashboardServeur.html?id=" + d[0].CodeServeur;
                                                    window.location.href = url;
                                                },
                                                error: function (a, e, d) {
                                                    var err = a.responseText + ' ' + e + ' ' + d;
                                                    alert(err);
                                                }
                                            });

                                        } else {
                                            window.location.href = "accueil.html";
                                        }
                                    },
                                    error: function (a, e, d) {
                                        var err = a.responseText + ' ' + e + ' ' + d;
                                        alert(err);
                                    }
                                });


                            },
                            error: function (a, e, d) {
                                var err = a.responseText + ' ' + e + ' ' + d;
                                alert(err);
                            }
                        });

                    }
                    else

                        window.location.href = "index.html";

                },
                error: function (a, e, d) {
                    var err = a.responseText + ' ' + e + ' ' + d;
                    alert(err);
                }
            });
        }
    });

    $(document).on("click", ".app__logout", function (e) {
        if (animating) return;
        $(".ripple").remove();
        animating = true;
        var that = this;
        $(that).addClass("clicked");
        setTimeout(function () {
            $app.removeClass("active");
            $login.show();
            $login.css("top");
            $login.removeClass("inactive");
        }, logoutPhase1 - 120);
        setTimeout(function () {
            $app.hide();
            animating = false;
            $(that).removeClass("clicked");
        }, logoutPhase1);
    });

});