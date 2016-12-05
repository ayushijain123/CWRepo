

//Admin Module Script

//Added by Neha M. on 28-11-16
//Discrete bar graph script
//var chart = nv.models.multiBarChart();
//d3.select('#chart svg').datum([
//  {
//      key: "NGOs",
//      color: "#77af3b",
//      values:
//      [
//        { x: "Jan", y: 40 },
//        { x: "Feb", y: 30 },
//        { x: "Mar", y: 20 },
//        { x: "Apr", y: 40 },
//        { x: "May", y: 30 },
//        { x: "Jun", y: 20 },
//        { x: "Jul", y: 40 },
//        { x: "Aug", y: 30 },
//        { x: "Sep", y: 20 },
//        { x: "Oct", y: 40 },
//        { x: "Nov", y: 30 },
//        { x: "Dec", y: 20 }
//      ]
//  },
//{
//    key: "Users",
//    color: "#364359",
//    values:
//    [
//        { x: "Jan", y: 30 },
//        { x: "Feb", y: 30 },
//        { x: "Mar", y: 10 },
//        { x: "Apr", y: 40 },
//        { x: "May", y: 50 },
//        { x: "Jun", y: 20 },
//        { x: "Jul", y: 40 },
//        { x: "Aug", y: 30 },
//        { x: "Sep", y: 50 },
//        { x: "Oct", y: 40 },
//        { x: "Nov", y: 60 },
//        { x: "Dec", y: 20 }
//    ]
//}
//]).transition().duration(500).call(chart);


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
