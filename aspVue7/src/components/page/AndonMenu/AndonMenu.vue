<template>
    <div class="container">
        <div class="plugins-tips">
            here is the option fields.
        </div>
        <el-row>
        <div id="chart1" :style="{width:'550px',height:'320px',display:'inline-block'}"></div> 
        </el-row>
        <el-row>
            <div id="chart2" :style="{width:'550px',height:'320px',display:'inline-block'}"></div> 
        </el-row>
    </div>
</template>

<script>
import echarts from 'echarts';
export default {
    name:'AndonMenu',
    data(){
        return {
            charts1: null,
            charts2: null,
            option1: {
                title: {
                    text: '安灯工时损失及占比'
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
                    data:['损失工时','安灯工时占比']
                },
                xAxis: {
                    data: ["1","2","3","4","5","6"]
                },
                yAxis: {},
                series: [{
                            name: '损失工时',
                            type: 'line',
                            // data: [5,20,36,10,10,20],
                                data:[3,7,12,6,5,1],
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
                            name: '安灯工时占比',
                            type: 'line',
                            data: [5,20,36,10,10,20],
                            itemStyle:{
                                color: ["#0070C0"]
                            },
                            lineStyle:{
                                normal:{
                                    width:3
                                }
                            }
                        }],
            },
            option2: {
                title: {
                    text: '安灯条数，修复及响应'
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
                    data:['安灯条数','HTTR修复时间','平均响应时间']
                },
                xAxis: {
                    data: ["1","2","3","4","5","6"]
                },
                yAxis: {},
                series: [{
                            name: '安灯条数',
                            type: 'line',
                            // data: [5,20,36,10,10,20],
                                data:[3,7,12,6,5,1],
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
                            name: 'HTTR修复时间',
                            type: 'line',
                            data: [5,20,36,10,10,20],
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
                            name: '平均响应时间',
                            type: 'line',
                            data: [1.5,2.0,3.6,1.0,1.0,2.0],
                            itemStyle:{
                                color: ["#C0C0C0"]
                            },
                            lineStyle:{
                                normal:{
                                    width:3
                                }
                            }
                        }],
            }
        }
    },
    mounted(){
        var c1 = document.getElementById("chart1");
        this.charts1 = echarts.init(c1);
        this.charts1.setOption(this.option1);

        var c2 = document.getElementById("chart2");
        this.charts2 = echarts.init(c2);
        this.charts2.setOption(this.option2);
    }
}
</script>
