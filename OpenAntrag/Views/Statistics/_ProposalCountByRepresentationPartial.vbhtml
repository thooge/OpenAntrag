@Imports OpenAntrag

@Code
    
    Dim lstCategories As New List(Of String)
    Dim lstData As New List(Of BarColumnDataSDM)
    StatisticsManager.GetProposalCountByRepresentationData(lstCategories, lstData)
    
    Dim jCategories As String = Newtonsoft.Json.JsonConvert.SerializeObject(lstCategories)
    Dim jData As String = Newtonsoft.Json.JsonConvert.SerializeObject(lstData)
    
    Dim intHeight As Integer = lstCategories.Count * 50
    
End Code

<script type="text/javascript">
    function init(bScroll) {        
        $('#stat-container').highcharts({
            chart: {
                type: 'bar',
                height: @(intHeight),
                spacing: [0, 10, 20, 0],
                events: {
                    load: function(event) {
                        if (bScroll) {
                            scrollToOffset($("#stat-header"), 500);
                        }
                    }
                } 
            },
            title: { text: '' },
            xAxis: {
                categories: @(Html.Raw(jCategories))
            },
            yAxis: {
                title: { text: '' },
                maxPadding: 0
            },
            legend: {
                enabled: false
            },
            plotOptions: {
                series: {
                    stacking: 'normal',
                    //pointWidth: 40,
                    //pointPadding: 0,
                    groupPadding: 0,
                    borderWidth: 0,
                    animation: { duration: 1000 },
                    cursor: 'pointer',
                    point: {
                        events: {
                            click: function () {
                                location.href = this.options.url;
                            }
                        }
                    },
                    dataLabels: {
                        enabled: true,
                        align: "left",
                        style: {
                            fontWeight:'normal'
                        },                        
                        x: 10,
                        color: '#ffffff'
                    }  
                }
            },
            series: [{
                name: 'Anträge',
                data: @(Html.Raw(jData))
            }]
        });        
    };
</script>

<div id="stat-header" class="content content-inverse container-fluid">
    <div class="row-fluid">
        <div class="span12 box-head">
            <i class="icon-chart-bar"></i>
            <h2><span style="color:#333;">Statistik:</span><br>Anzahl Anträge je Parlament</h2>
        </div>
    </div>
</div>

<div class="content container-fluid">
    <div class="row-fluid">
        <div class="span12">
            <div id="stat-container" style="margin: 0 auto"></div>
        </div>
    </div>
</div>