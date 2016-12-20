//Admin Module Script
//Graph
//$(function () {
//    Highcharts.chart('container', {
//        data: {
//            table: 'graph-datatable'

//        },
//        chart: {
//            type: 'column'
//        },
//        title: {
//            text: 'NGOs Vs Registered Users'
//        },
//        xAxis: {
//            allowDecimals: false,
//            title: {
//                text: 'Months'
//            }
//        },
//        yAxis: {
//            allowDecimals: false,

//            title: {
//                text: 'Units'
//            }
//        },
//        tips: {
//            trackMouse: true,
//            width: 80,
//            height: 40,
//            renderer: function (storeItem, item) {
//                this.setTitle(storeItem.get('name') + '<br />' + storeItem.get('data1'));
//            }
//        }
//    });
//});

function GetBarChart(graphData) {
    Highcharts.chart('container', {
        chart: {
            type: 'column'
        },
        title: {
            text: 'NGOs Vs Users'
        },

        xAxis: {
            categories: [
                'Jan',
                'Feb',
                'Mar',
                'Apr',
                'May',
                'Jun',
                'Jul',
                'Aug',
                'Sep',
                'Oct',
                'Nov',
                'Dec'
            ],
            crosshair: true
        },
        yAxis: {
            min: 0,
            title: {
                text: 'No. of NGOs/Users'
            }
        },
        tooltip: {
            headerFormat: '<span style="font-size:10px">{point.key}</span><table>',
            pointFormat: '<tr><td style="color:{series.color};padding:0">{series.name}: </td>' +
                '<td style="padding:0"><b>{point.y} </b></td></tr>',
            footerFormat: '</table>',
            shared: true,
            useHTML: true
        },

        plotOptions: {
            column: {
                pointPadding: 0.2,
                borderWidth: 0
            }
        },
        series: graphData
    });

}



$(".BarGraph").on('change', function () {
    //var postid = $(this).attr('id').split('-')[1];
    //var like = true;
    var year = $(this).val();
    var graphData;
    $.post("/Admin/GetDataByMonth?year="+ year, function (result) {
        if (result != null) {
            //alert(result);
            graphData = result;
            GetBarChart(graphData)
            //$("#liketemplate-" + postid).html("");

            //$("#liketemplate-" + postid).append(result);
        }

        //  window.location.reload();
        //console.log(result);
    });


});
//Added by Neha M.
//High Charts 
$(function () {
    var graphData;
   
    var year =0;
    $.post("/Admin/GetDataByMonth?year=" + year, function (result) {
        if (result != null) {
            graphData = result;
            GetBarChart(graphData);
            
        }
    });
   
 
  
    
});



//Added by Neha M. on 30-12-16
//NGO Donut chart
var ctx = document.getElementById("myChart1");
var myChart = new Chart(ctx, {
    type: 'doughnut',
    data: {
        labels: ["Active NGOs", "Warned NGOs", "Blocked NGOs"],
        datasets: [
        {
            data: [parseInt($("#COAN").html()), parseInt($("#COWU").html()), parseInt($("#COBN").html())],

            backgroundColor: [
                "#90c657",
                "#f9a94a",
                "#EC8D8D"
            ],
            hoverBackgroundColor: [
                "#77af3b",
                "#f79219",
                "#8C1717"
            ]
        }]
    },
    options: {
        scales: {
            yAxes: false
        }
    }
});

//Users Donut Chart
var ctx = document.getElementById("myChart2");
var myChart = new Chart(ctx, {
    type: 'doughnut',
    data: {
        labels: ["Active Users", "Blocked Users","Spam Users"],
        datasets: [
        {
            data: [parseInt($("#COAL").html()), parseInt($("#COBU").html()),0],
            backgroundColor: [
                "#495b79",
                "#e45857",
                "#ffae1a"
            ],
            hoverBackgroundColor: [
                "#364359",
                "#dd2c2b",
                "#e69500"
            ]
        }]
    },
    options: {
        scales: {
            yAxes: false
        }
    }
});


//Bar chart Jquery
//$(function () {
//    $('#chart').barChart({
//        bars: [{
//            name: 'Dataset 1',
//            values: [[1450569600, 40], [1450656000, 30], [1450742400, 15], [1450828800, 75], [1450915200, 129], [1451001600, 3.3], [1451088000, 57], [1451174400, 50], [1451260800, 30], [1451347200, 31], [1451433600, 24], [1451520000, 24], [1451606400, 24], [1451692800, 29], [1451779200, 39], [1451865600, 52], [1451952000, 36], [1452038400, 60], [1452124800, 39], [1452211200, 44], [1452297600, 36], [1452384000, 44], [1452470400, 64], [1452556800, 28], [1452643200, 58], [1452729600, 58], [1452816000, 50], [1452902400, 44], [1452988800, 57], [1453075200, 45], [1453161600, 7]]
//        },
//    {
//        name: 'Dataset 2',
//        values: [[1450569600, 45], [1450656000, 33], [1450742400, 49], [1450828800, 25], [1450915200, 29], [1451001600, 33], [1451088000, 5.7], [1451174400, 50], [1451260800, 30], [1451347200, 10], [1451433600, 24], [1451520000, 24], [1451606400, 24], [1451692800, 29], [1451779200, 39], [1451865600, 52], [1451952000, 36], [1452038400, 60], [1452124800, 39], [1452211200, 44], [1452297600, 36], [1452384000, 44], [1452470400, 64], [1452556800, 28], [1452643200, 58], [1452729600, 58], [1452816000, 50], [1452902400, 44], [1452988800, 57], [1453075200, 45], [1453161600, 7]]
//    }],
//        vertical: false,
//        bars: [],
//        hiddenBars: [],
//        milestones: [],
//        colors: [
//          "#f44336", "#e91e63", "#9c27b0", "#673ab7", "#3f51b5",
//            "#2196f3", "#03a9f4", "#00bcd4", "#009688", "#4caf50",
//            "#8bc34a", "#cddc39", "#ffeb3b", "#ffc107", "#ff9800",
//            "#ff5722", "#795548", "#9e9e9e", "#607d8b", "#263238"
//        ],
//        barColors: {},
//        dateFormat: 'DD.MM.YYYY HH:mm',
//        barGap: 5,
//        totalSumHeight: 25,
//        defaultWidth: 40,
//        defaultColumnWidth: 65
//    });
//});


//Chart.js 

//var ctx = document.getElementById("myChart3");
//var myBarChart = new Chart(ctx, {
//    type: 'bar',
//    data: {
//        labels: ["Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"],
//        datasets: [
//            {                
//                label: "NGOs",
//                backgroundColor: [
//                    'rgba(255, 99, 132, 0.2)',
//                    'rgba(54, 162, 235, 0.2)',
//                    'rgba(255, 206, 86, 0.2)',
//                    'rgba(75, 192, 192, 0.2)',
//                    'rgba(153, 102, 255, 0.2)',
//                    'rgba(255, 159, 64, 0.2)'
//                ],
//                borderColor: [
//                    'rgba(255,99,132,1)',
//                    'rgba(54, 162, 235, 1)',
//                    'rgba(255, 206, 86, 1)',
//                    'rgba(75, 192, 192, 1)',
//                    'rgba(153, 102, 255, 1)',
//                    'rgba(255, 159, 64, 1)'
//                ],
//                borderWidth: 1,
//                data: [65, 59, 80, 81, 56, 55, 40, 65, 59, 80, 81, 56],
//            }
//        ]
//    },
//    options: {
//        scales: {
//            xAxes: [{
//                stacked: true
//            }],
//            yAxes: [{
//                stacked: true,
//                ticks: {
//                    beginAtZero: true
//                }
//            }]
//        }
//    }
//});

//var ctx = document.getElementById("myChart4");
//var myBarChart = new Chart(ctx, {
//    type: 'bar',
//    data: {
//        labels: ["Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"],
//        datasets: [
//            {
//                label: "Users",
//                backgroundColor: [
//                    'rgba(255, 99, 132, 0.2)',
//                    'rgba(54, 162, 235, 0.2)',
//                    'rgba(255, 206, 86, 0.2)',
//                    'rgba(75, 192, 192, 0.2)',
//                    'rgba(153, 102, 255, 0.2)',
//                    'rgba(255, 159, 64, 0.2)'
//                ],
//                borderColor: [
//                    'rgba(255,99,132,1)',
//                    'rgba(54, 162, 235, 1)',
//                    'rgba(255, 206, 86, 1)',
//                    'rgba(75, 192, 192, 1)',
//                    'rgba(153, 102, 255, 1)',
//                    'rgba(255, 159, 64, 1)'
//                ],
//                borderWidth: 1,
//                data: [65, 59, 80, 81, 56, 55, 40, 65, 59, 80, 81, 56],
//            }
//        ]
//    },
//    options: {
//        scales: {
//            xAxes: [{
//                stacked: true
//            }],
//            yAxes: [{
//                stacked: true,
//                ticks: {
//                    beginAtZero: true
//                }
//            }]
//        }
//    }
//});

//var data = {
//    labels: ["January", "February", "March", "April", "May", "June", "July"],
//    datasets: [
//        {
//            label: "My First dataset",
//            backgroundColor: [
//                'rgba(255, 99, 132, 0.2)',
//                'rgba(54, 162, 235, 0.2)',
//                'rgba(255, 206, 86, 0.2)',
//                'rgba(75, 192, 192, 0.2)',
//                'rgba(153, 102, 255, 0.2)',
//                'rgba(255, 159, 64, 0.2)'
//            ],
//            borderColor: [
//                'rgba(255,99,132,1)',
//                'rgba(54, 162, 235, 1)',
//                'rgba(255, 206, 86, 1)',
//                'rgba(75, 192, 192, 1)',
//                'rgba(153, 102, 255, 1)',
//                'rgba(255, 159, 64, 1)'
//            ],
//            borderWidth: 1,
//            data: [65, 59, 80, 81, 56, 55, 40],
//        }
//    ]
//};

