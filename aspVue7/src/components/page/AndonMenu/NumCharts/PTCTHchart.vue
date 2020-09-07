<template>
        <div id="PTCTHchart" :style="{width:'750px',height:'320px',display:'inline-block'}"></div>
</template>

<script>
import echarts from 'echarts';

export default {
    name:'PTCTHchart',
    data(){
        return {
            chart: null,
            option: {
                title: {
                    text: 'PTC&T/H'
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
                    data:['eCRV Line','HV Air PTC Line','LV Air PTC Line','Ford节温器线','GAC节温器线','HKMC节温器线','JLR节温器线','SGME节温器线','蜡包装配线']
                },
                xAxis: {
                    data: ["1","2","3","4","5","6","7"]
                },
                yAxis: {},
                series: [{
                            name: 'eCRV Line',
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
                            name: 'HV Air PTC Line',
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
                            name: 'LV Air PTC Line',
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
                            name: 'Ford节温器线',
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
                            name: 'GAC节温器线',
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
                            name: 'HKMC节温器线',
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
                            name: 'JLR节温器线',
                            type: 'line',
                            data: [19,5,17,23,9,21,14],
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
                            name: 'SGME节温器线',
                            type: 'line',
                            data: [1,32,3,6,29,20,10],
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
                            name: '蜡包装配线',
                            type: 'line',
                            data: [6,9,34,23,19,20,41],
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
        var c1 = document.getElementById("PTCTHchart");
        this.chart = echarts.init(c1);
        this.chart.setOption(this.option);
    }
}
</script>