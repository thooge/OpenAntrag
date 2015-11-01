@Imports OpenAntrag

@Code
    
    Dim lstData As New List(Of PieSliceDataSDM)
    StatisticsManager.GetRepresentationCountByType(lstData)
    
    Dim jData As String = Newtonsoft.Json.JsonConvert.SerializeObject(lstData, Newtonsoft.Json.Formatting.None)
    
End Code

<script type="text/javascript">
    function init(bScroll) {        
        $('#stat-container').highcharts({
            chart: {
                type: 'pie',
                height: 400,
                events: {
                    load: function(event) {
                        if (bScroll) {
                            scrollToOffset($("#stat-header"), 500);
                        }
                    }
                } 
            },
            title: { text: '' },
            tooltip: {
                pointFormat: 'Anzahl: <b>{point.y}</b>'
            },
            plotOptions: {
                pie: {
                    allowPointSelect: true,
                    cursor: 'pointer',
                    dataLabels: {
                        enabled: true,
                        color: '#333333',
                        connectorColor: '#AAAAAA',
                        format: '{point.name}<br><b>{point.percentage:.0f}</b>% ({point.y})'
                    }
                }
            },
            series: [{
                name: 'Anzahl',
                data: @(Html.Raw(jData))
            }]
        });
    };    
</script>

<div id="stat-header" class="content content-inverse container-fluid">
    <div class="row-fluid">
        <div class="span12 box-head">
            <i class="icon-chart-bar"></i>
            <h2><span style="color:#333;">Statistik:</span><br>Verteilung Gruppentypen Parlamente</h2>
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