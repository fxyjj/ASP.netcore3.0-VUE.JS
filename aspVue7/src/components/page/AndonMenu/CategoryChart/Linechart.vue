<template>
    <div>
    <div id="AndonCount" :style="{width:'450px',height:'320px',display:'inline-block'}"></div>
    <div id="AndonWorkHour" :style="{width:'450px',height:'320px',display:'inline-block'}"></div>
    <div id="AndonWorkRate" :style="{width:'450px',height:'320px',display:'inline-block'}"></div>
    </div>
</template>

<script>
import echarts from 'echarts';
export default {
    name:"LineChart",
    data(){
        return {
            Countchart:null,
            Hourchart:null,
            Ratechart:null,
            optionCount:{
                 title: {
                    text: '周安灯条数'
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
                    data:['工艺','设备','仓库','质量']
                },
                xAxis: {
                    data: ["1","2","3","4"]
                },
                yAxis: {},
                series: [{
                            name: '工艺',
                            type: 'line',
                            // data: [5,20,36,10,10,20],
                                data:[3,7,12,6],
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
                            name: '设备',
                            type: 'line',
                            // data: [5,20,36,10,10,20],
                                data:[5,20,36,10],
                            itemStyle:{
                                color:"#AAAAFF"
                            },
                            lineStyle:{
                                normal:{
                                    width:3
                                }
                            }
                        },
                        {
                            name: '仓库',
                            type: 'line',
                            data: [5,50,21,30],
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
                            name: '质量',
                            type: 'line',
                            data: [9,12,33,6],
                            itemStyle:{
                                color: ["#FFED97"]
                            },
                            lineStyle:{
                                normal:{
                                    width:3
                                }
                            }
                        }]
            },
             optionHour:{
                 title: {
                    text: '周安灯工时'
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
                    data:['工艺','设备','仓库','质量']
                },
                xAxis: {
                    data: ["1","2","3","4"]
                },
                yAxis: {},
                series: [{
                            name: '工艺',
                            type: 'line',
                            // data: [5,20,36,10,10,20],
                                data:[3,7,12,6],
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
                            name: '设备',
                            type: 'line',
                            // data: [5,20,36,10,10,20],
                                data:[5,20,36,10],
                            itemStyle:{
                                color:"#AAAAFF"
                            },
                            lineStyle:{
                                normal:{
                                    width:3
                                }
                            }
                        },
                        {
                            name: '仓库',
                            type: 'line',
                            data: [5,50,21,30],
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
                            name: '质量',
                            type: 'line',
                            data: [9,12,33,6],
                            itemStyle:{
                                color: ["#FFED97"]
                            },
                            lineStyle:{
                                normal:{
                                    width:3
                                }
                            }
                        }]
            },
             optionRate:{
                 title: {
                    text: '周安灯工时占排班比'
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
                    data:['工艺','设备','仓库','质量']
                },
                xAxis: {
                    data: ["1","2","3","4"]
                },
                yAxis: {},
                series: [{
                            name: '工艺',
                            type: 'line',
                            // data: [5,20,36,10,10,20],
                                data:[3,7,12,6],
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
                            name: '设备',
                            type: 'line',
                            // data: [5,20,36,10,10,20],
                                data:[5,20,36,10],
                            itemStyle:{
                                color:"#AAAAFF"
                            },
                            lineStyle:{
                                normal:{
                                    width:3
                                }
                            }
                        },
                        {
                            name: '仓库',
                            type: 'line',
                            data: [5,50,21,30],
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
                            name: '质量',
                            type: 'line',
                            data: [9,12,33,6],
                            itemStyle:{
                                color: ["#FFED97"]
                            },
                            lineStyle:{
                                normal:{
                                    width:3
                                }
                            }
                        }]
            }

        }
    },
    mounted(){
        var c1 = document.getElementById("AndonCount");
        this.Countchart = echarts.init(c1);
        this.Countchart.setOption(this.optionCount);

        var c2 = document.getElementById("AndonWorkHour");
        this.Hourchart = echarts.init(c2);
        this.Hourchart.setOption(this.optionHour);

        var c3 = document.getElementById("AndonWorkRate");
        this.Ratechart = echarts.init(c3);
        this.Ratechart.setOption(this.optionRate);
    }
    
}
</script>