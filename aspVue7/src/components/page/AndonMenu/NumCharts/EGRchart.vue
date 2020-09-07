<template>
        <div id="EGRchart" :style="{width:'750px',height:'320px',display:'inline-block'}"></div>
</template>

<script>
import echarts from 'echarts';

export default {
    name:'EGRchart',
    data(){
        return {
            chart: null,
            option: {
                title: {
                    text: 'EGR'
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
                    data:['GEN_III_BPV','GEN_III_V+F','柔性线','GDI阀装配线','GEN_III_A+M','ETV装配线','LP阀装配线']
                },
                xAxis: {
                    data: ["1","2","3","4","5","6","7"]
                },
                yAxis: {},
                series: [{
                            name: 'GEN_III_BPV',
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
                            name: 'GEN_III_V+F',
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
                            name: '柔性线',
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
                            name: 'GDI阀装配线',
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
                            name: 'GEN_III_A+M',
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
                            name: 'ETV装配线',
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
                        },
                        {
                            name: 'LP阀装配线',
                            type: 'line',
                            data: [2,20,13,16,29,8,3],
                            itemStyle:{
                                color: ["#C07AB8"]
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
        var c1 = document.getElementById("EGRchart");
        this.chart = echarts.init(c1);
        this.chart.setOption(this.option);
    }
}
</script>