function drawChart(e, data) {
    var options = {
        series: [{
            data: data
        }],
        chart: {
            type: 'candlestick',
            height: 350
        },
        title: {
            text: 'CandleStick Chart',
            align: 'left'
        },
        xaxis: {
            type: 'datetime'
        },
        yaxis: {
            tooltip: {
                enabled: true
            }
        }
    };

    var chart = new ApexCharts(document.querySelector("#myChart"), options);
    chart.width = 700;
    chart.render();
};