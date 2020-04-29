var chart;
var lastMin = -1, lastMax = -1;

function drawChart(e, name) {
    var options = {
        chart: {
            type: "candlestick",
            height: 350,
            zoom: {
                autoScaleYaxis: true
            }
        },
        series: [],
        title: {
            text: name,
            align: "center"
        },
        tooltip: {
            enabled: true
        },
        annotations: {
            xaxis: [
                {
                    x: "Oct 06 14:00",
                    borderColor: "#00E396",
                    label: {
                        borderColor: "#00E396",
                        style: {
                            fontSize: "12px",
                            color: "#fff",
                            background: "#00E396"
                        },
                        orientation: "horizontal",
                        offsetY: 7,
                        text: "Annotation Test"
                    }
                }
            ]
        },
        xaxis: {
            type: "datetime",
            labels: {
                formatter: function(val) {
                    return dayjs(val).format("MMM DD HH:mm");
                }
            }
        },
        yaxis: {
            tooltip: {
                enabled: true
            }
        },
        noData: {
            text: "Loading..."
        },
        legend: {
            show: false
        }
    };

    chart = new ApexCharts(document.querySelector("#myChart"), options);
    chart.width = 700;
    chart.render();
};

function updateChart(data) {
    chart.appendSeries({
            data: [
                {
                    x: new Date(data.time),
                    y: [data.open, data.high, data.low, data.close]
                }
            ]
        },
        false);

}

function updateOptions(data) {
    chart.updateOptions({
        noData: {
            text: data
        }
    });
}