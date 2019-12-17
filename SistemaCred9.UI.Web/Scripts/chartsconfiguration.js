var _charts = {

    applyText: function () {
        Chart.pluginService.register({
            afterDraw: function (chart) {
                if (chart.config.options.elements.center) {
                   
                    var helpers = Chart.helpers;
                    var centerX = (chart.chartArea.left + chart.chartArea.right) / 2;
                    var centerY = (chart.chartArea.top + chart.chartArea.bottom) / 2;

                    var ctx = chart.chart.ctx;
                    ctx.restore();
                    var fontSize = (chart.chart.height / 80).toFixed(2);
                    //var fontStyle = helpers.getValueOrDefault(chart.config.options.elements.center.fontStyle, Chart.defaults.global.defaultFontStyle);
                    //var fontFamily = helpers.getValueOrDefault(chart.config.options.elements.center.fontFamily, Chart.defaults.global.defaultFontFamily);
                    //var font = helpers.fontString(fontSize, fontStyle, fontFamily);
                    ctx.font = fontSize + "em sans-serif";
                    ctx.fillStyle = '#000000';
                    ctx.textAlign = 'center';
                    ctx.textBaseline = 'middle';

                    //var text = texto,
                    //textX = Math.round((chart.chart.width - ctx.measureText(text).width) / 2),
                    //textY = chart.chart.height / 2;

                    ctx.fillText(chart.config.options.elements.center.text, centerX, centerY);
                    ctx.save();
                }
            }
        });
    },

    applyText2: function(texto){
        Chart.pluginService.register({
            beforeDraw: function (chart) {
                var width = chart.chart.width,
                    height = chart.chart.height,
                    ctx = chart.chart.ctx;

                ctx.restore();
                var fontSize = (chart.chart.height / 80).toFixed(2);
                ctx.font = fontSize + "em sans-serif";
                ctx.textBaseline = "middle";

                var text = texto,
                    textX = Math.round((chart.chart.width - ctx.measureText(text).width) / 2),
                    textY = chart.chart.height / 2;

                ctx.fillText(text, textX, textY);
                ctx.save();
            }
        });
    },

    aplyTextChart: function (ctx, texto){
        ctx.restore();
        var fontSize = (height / 80).toFixed(2);
        ctx.font = fontSize + "em sans-serif";
        ctx.textBaseline = "middle";

        var text = texto,
            textX = Math.round((width - ctx.measureText(text).width) / 2),
            textY = height / 2;

        ctx.fillText(text, textX, textY);
        ctx.save();
    },

    CreateChartOSFechadasPorMeses: function(str) {
  
        //-------------
        //- PIE CHART -
        //-------------
        // Get context with jQuery - using jQuery's .get() method.
        var pieChartCanvas = $('#'+str).get(0).getContext('2d')
        var pieChart = new Chart(pieChartCanvas)
        var PieData = [
          {
              value: 700,
              color: '#f56954',
              highlight: '#f56954',
              label: 'Chrome'
          },
          {
              value: 500,
              color: '#00a65a',
              highlight: '#00a65a',
              label: 'IE'
          },
          {
              value: 400,
              color: '#f39c12',
              highlight: '#f39c12',
              label: 'FireFox'
          },
          {
              value: 600,
              color: '#00c0ef',
              highlight: '#00c0ef',
              label: 'Safari'
          },
          {
              value: 300,
              color: '#3c8dbc',
              highlight: '#3c8dbc',
              label: 'Opera'
          },
          {
              value: 100,
              color: '#d2d6de',
              highlight: '#d2d6de',
              label: 'Navigator'
          }
        ]

        var pieOptions = {
            //Boolean - Whether we should show a stroke on each segment
            segmentShowStroke: true,
            //String - The colour of each segment stroke
            segmentStrokeColor: '#fff',
            //Number - The width of each segment stroke
            segmentStrokeWidth: 2,
            //Number - The percentage of the chart that we cut out of the middle
            percentageInnerCutout: 50, // This is 0 for Pie charts
            //Number - Amount of animation steps
            animationSteps: 100,
            //String - Animation easing effect
            animationEasing: 'easeOutBounce',
            //Boolean - Whether we animate the rotation of the Doughnut
            animateRotate: true,
            //Boolean - Whether we animate scaling the Doughnut from the centre
            animateScale: false,
            //Boolean - whether to make the chart responsive to window resizing
            responsive: true,
            // Boolean - whether to maintain the starting aspect ratio or not when responsive, if set to false, will take up entire container
            maintainAspectRatio: true,
            //String - A legend template
            legendTemplate: '<ul class="<%=name.toLowerCase()%>-legend"><% for (var i=0; i<segments.length; i++){%><li><span style="background-color:<%=segments[i].fillColor%>"></span><%if(segments[i].label){%><%=segments[i].label%><%}%></li><%}%></ul>'
        }
        //Create pie or douhnut chart
        // You can switch between pie and douhnut using the method below.
      
        pieChart.Doughnut(PieData, pieOptions);
    },

    CreateChart: function (str, arr) {
        var pieChartCanvas = $('#' + str).get(0).getContext('2d');

        if (arr == null || arr.TipoOSPorMes == null) {
            return;
        }
        
        var arr_nome_os = [];
        arr_nome_os = arr.TipoOSPorMes.map(function (os) {
            return os.nome;
        });

        var arr_data_os = [];
        arr_data_os = arr.TipoOSPorMes.map(function (os) {
            return os.quantidade;
        });

        var pieChart = new Chart(pieChartCanvas, {
            type: 'doughnut',
            data: {
                labels: arr_nome_os,
                datasets: [{
                    label: 'Tipo Ordem de Serviço',
                    data: arr_data_os,
                    backgroundColor: [
                        'rgba(54, 162, 235, 1)',
                        'rgba(255, 99, 132, 1)',
                        'rgba(255, 206, 86, 1)',
                        'rgba(75, 192, 192, 1)',
                        'rgba(153, 102, 255, 1)',
                        'rgba(255, 159, 64, 1)',
                        'rgba(0, 128, 128, 1)',
                        'rgba(70, 130, 180, 1)'
                    ],
                    borderColor: [
                        'rgba(54, 162, 235, 1)',
                        'rgba(255,99,132,1)',
                        'rgba(255, 206, 86, 1)',
                        'rgba(75, 192, 192, 1)',
                        'rgba(153, 102, 255, 1)',
                        'rgba(255, 159, 64, 1)',
                        'rgba(0, 128, 128, 1)',
                        'rgba(70, 130, 180, 1)'
                    ],
                    borderWidth: 1
                }]
            },
            options: {
                legend: {
                    display: false
                },
                elements: {
                    center: {
                        text: arr.total
                    }
                }
            }
        });
        
        $('#' + str + "-legend").html(pieChart.generateLegend());

        for (var i = 0; i < (16 - $('#' + str + '-legend li').length)/2; i++) {
            $('#' + str + "-legend ul").append("<li><span></span></li>");
        }

    },

    CreateLineChart: function (str, arr) {
        var lineChartCanvas = $('#' + str).get(0).getContext('2d');
        debugger;
        //if (arr == null || arr.TipoOSPorMes == null) {
        //    return;
        //};


        var lineChart = new Chart($('#' + str).get(0), {
            type: 'line',
            data: {
                labels: ["Janeiro", "Fevereiro", "Março", "Abril", "Maio", "Junho"],
                datasets: [{
                    data: [86, 114, 106, 106, 107, 111, 133, 221, 783, 2478],
                    label: "Africa",
                    borderColor: "#3e95cd",
                    fill: false
                }, {
                    data: [282, 350, 411, 502, 635, 809, 947, 1402, 3700, 5267],
                    label: "Asia",
                    borderColor: "#8e5ea2",
                    fill: false
                }, {
                    data: [168, 170, 178, 190, 203, 276, 408, 547, 675, 734],
                    label: "Europe",
                    borderColor: "#3cba9f",
                    fill: false
                }, {
                    data: [40, 20, 10, 16, 24, 38, 74, 167, 508, 784],
                    label: "Latin America",
                    borderColor: "#e8c3b9",
                    fill: false
                }, {
                    data: [6, 3, 2, 2, 7, 26, 82, 172, 312, 433],
                    label: "North America",
                    borderColor: "#c45850",
                    fill: false
                }
                ]
            },
            options: {
                title: {
                    display: true,
                    text: 'Ordens de Serviço Abertas em 2017'
                }
            }
        });
    }
};