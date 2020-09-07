<template>
    <div id="Equipchart" name = "jack" :style="{width:'750px',height:'320px',display:'inline-block'}"></div>
</template>
<script>
import echarts from 'echarts';
export default {
    name:'EquipChart',
    data(){
        return {
            chart:null,
           
            option:{
                title:{
                    text:'安灯工时产线分布'
                },
                tooltip:{
                    trigger: 'axis',
                    axisPointer: {
                        type: 'cross',
                        label: {
                            backgroundColor: '#283b56'
                        }
                    }
                },
                 legend: {
                    selectMode:true,
                    data:['安灯工时', '修复时间','安灯次数']
                },
                yAxis: [{
                            type: 'value',
                            scale: true,
                            name: 'Min',
                            max: 160,
                            min: 0,
                            boundaryGap: [0.2, 0.2]
                        },
                        {
                            type: 'value',
                            scale: true,
                            name: '',
                            max: 4,
                            min: 0,
                            boundaryGap: [0.2, 0.2]
                        }
                    ],
                xAxis:[{
                            type: 'category',
                            data:['CL1_OP80','CL2_OP20','U11','FL_OP50','GEN3_V20_1','CL2_OP160','EGRTubeOP10','CL3_OP90_9','GEN3_B10','CL2_Tester','HKMC_OP10',
                                  'CL3_OP10_1','CL1_OP50','GEN3_B30_4','JLR_OP10','CL2_OP150_2','GAC_OP20_1','JLR_OP20','CL3_OP50','GEN3_B50_1','GEN3_V10','HKMC_OP50',
                                  'M20_S07','CL1_OP90','CL3_OP70_1','SGMM_OP50','IPTE','CL2_OP60','CL2_OP30'],
                             axisLabel: {
                                interval: 0,
                                rotate: 30
                            },

                        }],
                 series: [{
                            name: '安灯工时',
                            type: 'bar',
                            data: [3,7,12,6,5,1,9,5,20,36,10,10,20,5,15,5,1,10,23,14,6,36,10,10,20,5,20,5,50],
                            itemStyle:{
                                color: ["#0070C0"]
                            },
                           
                        },
                        {
                            name: '修复时间',
                            type: 'line',
                            data: [5,20,36,10,10,20,5,25,23,19,29,17,5,9,12,33,6,49,2,4,2,20,13,16,29,8,3,20,90],
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
                            name: '安灯次数',
                            type: 'line',
                            data: [3,7,12,6,5,1,9,5,20,36,10,10,20,8,5,50,21,30,13,24,7,15,5,1,10,23,14,6,31],
                            itemStyle:{
                                color: ["#FFED97"]
                            },
                            lineStyle:{
                                normal:{
                                    width:3
                                }
                            }
                        }
                    ]
            }

        }
    },
    mounted(){
        var c1 = document.getElementById("Equipchart");
        
        this.chart = echarts.init(c1);
        this.chart.setOption(this.option);
    }
}
</script>