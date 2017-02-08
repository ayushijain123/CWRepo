//Admin Module Script

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
            allowDecimals: false,
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
    $.post("/Admin/GetDataByMonth?year=" + year, function (result) {
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
    var year = 0;
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
        labels: ["Active NGOs", "Warned NGOs", "Blocked/Declined NGOs"],
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
        labels: ["Active Users", "Blocked/Abused Users"],
        datasets: [
        {
            data: [parseInt($("#COAL").html()), parseInt($("#COBS").html()), 0],
            backgroundColor: [
                "#495b79",
                "#e45857"

            ],
            hoverBackgroundColor: [
                "#364359",
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

//Pie Chart
var ob= [
            "Red",
            "Blue",
            "Yellow",
            "Techy"
]
$(function () {
    var ngodata; 
    $.post("/Admin/DonationRequest", function (result) {
        if (result != null) {
            console.log(result);
            ngodata = result;
            NGODonutChart(ngodata);

        }
    });
});
function  NGODonutChart(ngodata)
{
var ctx = document.getElementById("pieChart");


var myPieChart = new Chart(ctx, {
    type: 'pie',
    data: ngodata,
    options: {
        scales: {
            yAxes: false
        }
    }

});
}






