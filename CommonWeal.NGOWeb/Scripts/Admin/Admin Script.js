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
//Donut chart
var ctx = document.getElementById("myChart");
var myChart = new Chart(ctx, {
    type: 'doughnut',
    data: {
        labels: ["Active NGOs", "Blocked NGOs", "Active Users", "Warned Users/NGOs", "Blocked Users"],
        datasets: [
        {
            data: [12, 22, 39, 8, 3],
            backgroundColor: [
                "#90c657",
                "#EC8D8D",
                "#495b79",
                "#f9a94a",
                "#e45857"
            ],
            hoverBackgroundColor: [
                "#77af3b",
                "#8C1717",
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
