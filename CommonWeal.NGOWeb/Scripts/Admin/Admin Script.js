//Admin Module Script
//Graph
$(function () {
    Highcharts.chart('container', {
        data: {
            table: 'graph-datatable'

        },
        chart: {
            type: 'column'
        },
        title: {
            text: 'NGOs Vs Registered Users'
        },
        xAxis: {
            allowDecimals: false,
            title: {
                text: 'Months'
            }
        },
        yAxis: {
            allowDecimals: false,

            title: {
                text: 'Units'
            }
        },
        tips: {
            trackMouse: true,
            width: 80,
            height: 40,
            renderer: function (storeItem, item) {
                this.setTitle(storeItem.get('name') + '<br />' + storeItem.get('data1'));
            }
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
        labels: ["Active Users", "Warned Users", "Blocked Users"],
        datasets: [
        {
            data: [parseInt($("#COAL").html()), 0, parseInt($("#COBU").html())],
            backgroundColor: [
                "#495b79",
                "#f9a94a",
                "#e45857"
            ],
            hoverBackgroundColor: [
                "#364359",
                "#f79219",
                "#dd2c2b"
            ]
        }]
    },
    options: {
        scales: {
            yAxes: false
        }
    }
});
