<template>
        <div id="CMchart" :style="{width:'750px',height:'320px',display:'inline-block'}"></div>
</template>

<script>
import echarts from 'echarts';

export default {
    name:'CMchart',
    data(){
        return {
            chart: null,
            option: {
                title: {
                    text: 'CM'
                },
                tooltip: { 
                    trigger: 'axis',
                    axisPointer: {
                        type: 'cross',
                        label: {
                            backgroundColor: '#283b56'
                        }
                    }
                },
                legend: {
                    left:'right',
                    orient:'vertical',
                    selectMode:true,
                    data:['Cooler Line1','Cooler Line2','Cooler Line3','混合管装配线','模块线1','模块线2']
                },
                xAxis: {
                    data: ["1","2","3","4","5","6","7"]
                },
                yAxis: {},
                series: [{
                            name: 'Cooler Line1',
                            type: 'line',
                            // data: [5,20,36,10,10,20],
                                data:[3,7,12,6,5,1,9],
                            itemStyle:{
                                color:"#c0504d"
                            },
                            lineStyle:{
                                normal:{
                                    width:3
                                }
                            }
                        },
                        {
                            name: 'Cooler Line2',
                            type: 'line',
                            data: [5,20,36,10,10,20,8],
                            itemStyle:{
                                color: ["#0070C0"]
                            },
                            lineStyle:{
                                normal:{
                                    width:3
                                }
                            }
                        },
                        {
                            name: 'Cooler Line3',
                            type: 'line',
                            data: [5,50,21,30,13,24,7],
                            itemStyle:{
                                color: ["#AAAAFF"]
                            },
                            lineStyle:{
                                normal:{
                                    width:3
                                }
                            }
                        },
                        {
                            name: '混合管装配线',
                            type: 'line',
                            data: [15,5,1,10,23,14,6],
                            itemStyle:{
                                color: ["#ACD6FF"]
                            },
                            lineStyle:{
                                normal:{
                                    width:3
                                }
                            }
                        },
                        {
                            name: '模块线1',
                            type: 'line',
                            data: [5,25,23,19,29,17,5],
                            itemStyle:{
                                color: ["#CCFF80"]
                            },
                            lineStyle:{
                                normal:{
                                    width:3
                                }
                            }
                        },
                        {
                            name: '模块线2',
                            type: 'line',
                            data: [9,12,33,6,49,2,4],
                            itemStyle:{
                                color: ["#FFED97"]
                            },
                            lineStyle:{
                                normal:{
                                    width:3
                                }
                            }
                        }],
            },
        }
    },
    mounted(){
        var c1 = document.getElementById("CMchart");
        this.chart = echarts.init(c1);
        this.chart.setOption(this.option);
    }
}
</script>