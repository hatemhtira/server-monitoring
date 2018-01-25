/* --------------------------------------------------------
 Flot Charts
 -----------------------------------------------------------*/
 
//Line Chart
$(function () {
    if ($('#line-charta')[0]) {
      // var k1 = $('<%hfServerValue.ClientId%>').val()
       
        var x1 = $("#x1").val();
        var y1 = $("#y1").val();
        var x2 = $("#x2").val();
        var y2 = $("#y2").val();
        var x3 = $("#x3").val();
        var y3 = $("#y3").val();
        var x4 = $("#x4").val();
        var y4 = $("#y4").val();
        var x5 = $("#x5").val();
        var y5 = $("#y5").val();

        someFuncx = function (val, axis) {
            return "scan : " + val   
        }
        someFuncy = function (val, axis) {
            if (val == 2)
                return "reussi"
            else if (val == 1)
                return "lent"
            else if (val == 0)
                return "echec"
            else
                return " "
        }
       
        var d1 = [[x1, y1], [x2, y2], [x3, y3], [x4, y4], [x5, y5]];
       // var d1 = [[1, y2], [2, y1], [3, y3], [x4, y4]];
       // var d1 = [[new Date(2016, 1, 1), 14], [new Date(2016, 2, 1), k1], [new Date(2016, 3, 1), k2]];


        $.plot('#line-charta', [ {
            data: d1,
           
            label: "Data",
           

        },],

            {
                series: {
                    lines: {
                        show: true,
                        lineWidth: 1,
                        fill: 0.25,
                    },

                    color: 'rgba(255,255,255,0.7)',
                    shadowSize: 0,
                    points: {
                        show: true,
                    }
                },

                yaxis: {
                    min: 0,
                    max: 4,
                   // tickSize:50,
                    tickColor: 'rgba(255,255,255,0.15)',
                    tickDecimals: 0,
                    tickFormatter: someFuncy,
                    font :{
                        lineHeight: 13,
                        style: "normal",
                        color: "rgba(255,255,255,0.8)",
                    },
                    shadowSize: 0,
                },
                xaxis: {
                    tickColor: 'rgba(255,255,255,0)',
                    tickDecimals: 0,
                    tickFormatter: someFuncx,
                    font :{
                        lineHeight: 13,
                        style: "normal",
                        color: "rgba(255,255,255,0.8)",
                    }
                },
                grid: {
                    borderWidth: 1,
                    borderColor: 'rgba(255,255,255,0.25)',
                    labelMargin:10,
                    hoverable: true,
                    clickable: true,
                    mouseActiveRadius:6,
                },
                legend: {
                    show: false
                }
            });
        $("#line-charta").bind("plotclick", function (event, pos, item) {
            if (item) {
                window.location.href = "DashboardScan.aspx?id=" + item.datapoint[0];
            }
        });

        //$("#line-chart").bind("plothover", function (event, pos, item) {
        //    if (item) {
        //        var x = item.datapoint[0].toFixed(2),
        //            y = item.datapoint[1].toFixed(2);
        //        $("#linechart-tooltip").html(item.series.label + " of " + x + " = " + y).css({top: item.pageY+5, left: item.pageX+5}).fadeIn(200);
        //    }
        //    else {
        //        $("#linechart-tooltip").hide();
        //    }
        //});
       

        $("<div id='linechart-tooltip' class='chart-tooltip'></div>").appendTo("body");
    }

});

//Bar Chart 
////Pie Chart 

//Dynamic Chart
$(function() {
    if ($('#dynamic-chart')[0]) {
        var data = [],
            totalPoints = 300;

        function getRandomData() {
            if (data.length > 0)
                data = data.slice(1);

            while (data.length < totalPoints) {
                var prev = data.length > 0 ? data[data.length - 1] : 50,
                    y = prev + Math.random() * 10 - 5;
                if (y < 0) {
                    y = 0;
                } else if (y > 90) {
                    y = 90;
                }

                data.push(y);
            }

            var res = [];
            for (var i = 0; i < data.length; ++i) {
                res.push([i, data[i]])
            }

            return res;
        }


        var updateInterval = 30;
        var plot = $.plot("#dynamic-chart", [ getRandomData() ], {
            series: {
                label: "Data",
                lines: {
                    show: true,
                    lineWidth: 1,
                    fill: 0.25,
                },

                color: 'rgba(255,255,255,0.2)',
                shadowSize: 0,
            },
            yaxis: {
                min: 0,
                max: 100,
                tickColor: 'rgba(255,255,255,0.15)',
                font :{
                    lineHeight: 13,
                    style: "normal",
                    color: "rgba(255,255,255,0.8)",
                },
                shadowSize: 0,

            },
            xaxis: {
                tickColor: 'rgba(255,255,255,0.15)',
                show: true,
                font :{
                    lineHeight: 13,
                    style: "normal",
                    color: "rgba(255,255,255,0.8)",
                },
                shadowSize: 0,
                min: 0,
                max: 250
            },
            grid: {
                borderWidth: 1,
                borderColor: 'rgba(255,255,255,0.25)',
                labelMargin:10,
                hoverable: true,
                clickable: true,
                mouseActiveRadius:6,
            },
            legend: {
                show: false
            }
        });

        function update() {
            plot.setData([getRandomData()]);
            // Since the axes don't change, we don't need to call plot.setupGrid()

            plot.draw();
            setTimeout(update, updateInterval);
        }

        update();

        $("#dynamic-chart").bind("plothover", function (event, pos, item) {
            if (item) {
                var x = item.datapoint[0].toFixed(2),
                    y = item.datapoint[1].toFixed(2);
                $("#dynamic-chart-tooltip").html(item.series.label + " of " + x + " = " + y).css({top: item.pageY+5, left: item.pageX+5}).fadeIn(200);
            }
            else {
                $("#dynamic-chart-tooltip").hide();
            }
        });

        $("<div id='dynamic-chart-tooltip' class='chart-tooltip'></div>").appendTo("body");
    }
});




/* --------------------------------------------------------
 Sparkline
 -----------------------------------------------------------*/
(function(){
    //Bar
    $("#stats-bar-1").sparkline([6,4,8,6,5,6,7,8,3,5,9,5,8,3], {
        type: 'bar',
        height: '50px',
        barWidth: 4,
        barColor: '#fff',
        barSpacing: 3,
    });

    $("#stats-bar-2").sparkline([4,7,6,2,5,3,8,6], {
        type: 'bar',
        height: '50px',
        barWidth: 4,
        barColor: '#fff',
        barSpacing: 3
    });

    $("#stats-bar-3").sparkline([4,7,6,2,5,3,8,6], {
        type: 'bar',
        height: '50px',
        barWidth: 4,
        barColor: '#fff',
        barSpacing: 3
    });

    //Line
    $("#stats-line").sparkline([9,4,6,5,6,4,5,7,9,3,6,5], {
        type: 'line',
        width: '100%',
        height: '65',
        lineColor: 'rgba(255,255,255,0.4)',
        fillColor: 'rgba(0,0,0,0.2)',
        lineWidth: 1.25,
    });

    $("#stats-line-2").sparkline([5,6,3,9,7,5,4,6,5,6,4,9], {
        type: 'line',
        width: '100%',
        height: '65',
        lineColor: 'rgba(255,255,255,0.4)',
        fillColor: 'rgba(0,0,0,0.2)',
        lineWidth: 1.25,
    });

    $("#stats-line-3").sparkline([8,11,9,12,10,9,13,11,12,11,10], {
        type: 'line',
        height: '65',
        width: '100%',
        lineColor: 'rgba(255,255,255,0.4)',
        lineWidth: 1.25,
        fillColor: 'rgba(0,0,0,0.2)',
        barWidth: 5,
        barColor: '#C5CED6',

    });

    $("#stats-line-4").sparkline([4,6,7,8,9,5,3,6,5,6,7,5,7,2,7], {
        type: 'line',
        height: '65',
        width: '100%',
        lineColor: 'rgba(255,255,255,0.4)',
        lineWidth: 1.25,
        fillColor: 'rgba(0,0,0,0.2)',
        barWidth: 5,
        barColor: '#C5CED6',

    });

    $("#stats-line-5").sparkline([10,9,12,10,9,13,11,12,11,10,12], {
        type: 'line',
        height: '65',
        width: '80',
        lineColor: 'rgba(255,255,255,0.4)',
        fillColor: 'rgba(0,0,0,0.2)',
        lineWidth: 1.25,
        barWidth: 5,
        barColor: '#C5CED6',

    });

    $("#stats-line-6").sparkline([7,2,7,5,7,6,5,6,3,5,9,8,6,7,4], {
        type: 'line',
        height: '65',
        width: '80',
        lineColor: 'rgba(255,255,255,0.85)',
        fillColor: 'rgba(0,0,0,0.2)',
        barWidth: 5,
        barColor: '#C5CED6',
        lineWidth: 1.25,
    });

    //Tristate
    $("#stats-tristate").sparkline([2,2,-2,2,-2,-2,2,2,2,2,2], {
        type: 'tristate',
        width: '100%',
        height: '52',
        barWidth: 4,
        barSpacing: 3,
        zeroAxis: false,
        posBarColor: '#fff',
        negBarColor: '#fff',
        zeroBarColor: '#fff',
        lineWidth: 0,
        lineColor: 'rgba(0,0,0,0)',
    });
})();


/* --------------------------------------------------------
 Map
 -----------------------------------------------------------*/

    
    //World Map
 
/* --------------------------------------------------------
 Easy Pie Charts
 -----------------------------------------------------------*/
$(function() {
    $('.pie-chart-tiny').easyPieChart({
        easing: 'easeOutBounce',
        barColor: 'rgba(255,255,255,0.75)',
        trackColor: 'rgba(0,0,0,0.3)',
        scaleColor: 'rgba(255,255,255,0.3)',
        lineCap: 'square',
        lineWidth: 4,
        size: 100,
        animate: 3000,
        onStep: function(from, to, percent) {
            $(this.el).find('.percent').text(Math.round(percent));
        }
    });

    //var chart = window.chart = $('.pie-chart-tiny').data('easyPieChart');
    //$('.pie-chart-tiny .pie-title > i').on('click', function() {
    //    $(this).closest('.pie-chart-tiny').data('easyPieChart').update(Math.random() * 200 - 100);
    //});
    //setInterval(function () {
    //    $('.pie-chart-tiny .pie-title > i').trigger('click');
    //},5000);
});