<template>
    <div>
        <div id="Equipchart" name = "jack" :style="{width:'100%',height:'400px','margin-top':'5px'}"></div>

        <el-slider :style="{'margin-left':'20%','margin-right':'20%'}" v-model="valSlide" :step="1"  :marks="marks" :min="sMin" :max="sMax" show-stops @change="changeData" aria-label="true"></el-slider>
    </div>
</template>
<script>
import echarts from 'echarts';
import bus from '../../../common/bus'
export default {
    name:'EquipChart',
    data(){
        return {
            chart:null,
            valSlide:0,
            marks:{},
            sMin:0,
            sMax:10,
            //param from bus
            start:null,
            end:null,
            du:null,
            status:null,

            //返回结果
            queryRes:[],
            queryEquip:[],

            //chartdata
            qxAxis:[],
            qc1y:[],
            qc2y:[],
            qc3y:[],
           
            option:{
                title:{
                    text:'安灯工时设备分布'
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
                    data:['安灯工时', '修复时间','安灯次数'],
                    left:'right'
                },
                yAxis: [{
                            type: 'value',
                            scale: true,
                            name: 'Min',
                            // max: 160,
                            min: 0,
                            boundaryGap: [0.2, 0.2]
                        },
                        {
                            type: 'value',
                            scale: true,
                            name: '',
                            max: 30,
                            min: 0,
                            boundaryGap: [0.2, 0.2]
                        }
                    ],
                xAxis:{
                            type: 'category',
                            data:[],/*['CL1_OP80','CL2_OP20','U11','FL_OP50','GEN3_V20_1','CL2_OP160','EGRTubeOP10','CL3_OP90_9','GEN3_B10','CL2_Tester','HKMC_OP10',
                                  'CL3_OP10_1','CL1_OP50','GEN3_B30_4','JLR_OP10','CL2_OP150_2','GAC_OP20_1','JLR_OP20','CL3_OP50','GEN3_B50_1','GEN3_V10','HKMC_OP50',
                                  'M20_S07','CL1_OP90','CL3_OP70_1','SGMM_OP50','IPTE','CL2_OP60','CL2_OP30'],*/
                             axisLabel: {
                                interval: 0,
                                rotate: 30
                            },

                        },
                toolbox: {
                    left: 'center',
                    feature: {
                        dataZoom: {
                            yAxisIndex: 0
                        },
                        restore: {},
                        saveAsImage: {}
                    }
                },
                dataZoom: [{
                                startValue: 'CL1_OP80'
                            }, {
                                type: 'inside'
                            }],
                 series: [{
                            name: '安灯工时',
                            type: 'bar',
                           
                            data: [3,7,12,6,5,1,9,5,20,36,10,10,20,5,15,5,1,10,23,14,6,36,10,10,20,5,20,5,50],
                            itemStyle:{
                                color: ["#6495ED"]
                            },
                           
                        },
                         {
                            name: '安灯次数',
                            type: 'line',
                            yAxisIndex: 1,
                            data: [3,7,2,6,5,1,9,5,2,36,1,1,2,8,5,5,1,3,3,4,7,5,5,1,0,2,4,6,3],
                            itemStyle:{
                                color: ["#B22222"]
                            },
                            lineStyle:{
                                normal:{
                                    width:3
                                }
                            }
                        },
                        {
                            name: '修复时间',
                            type: 'line',
                            yAxisIndex: 1,
                            data: [5,2,6,1,0,2,5,2,3,9,2,7,5,9,2,3,6,4,2,4,2,2,3,6,9,8,3,2,9],
                            itemStyle:{
                                color: ["#C0C0C0"]
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
    methods:{
        changeData(){
            this.getEquipData()
        },
        getSlideData(){
            fetch('api/Andon/topissue',{
                method:'POST',
                headers:{
                    'Content-Type': 'application/json'
                },
                body:JSON.stringify({
                    start: this.start,
                    end: this.end,
                    dateunit: this.du
                })
            })
            .then(response=> response.json())
            // .then(response=> response.text())
            .then(data=>{
                this.queryRes = data;
                this.marks = {}
                var count = 0
                for(var item of this.queryRes){
                    this.$set(this.marks,count,item.dateunit)
                    count++
                    console.log("jack"+item.dateunit)
                }
                this.sMin = 0
                this.sMax = count-1
                console.log("camu"+this.marks[0])
                 this.getEquipData()
            })
            .catch(data =>{
                alert('error')
            })
        },
        getEquipData(){
            fetch('api/Andon/equipChart',{
                method:'POST',
                headers:{
                    'Content-Type': 'application/json'
                },
                body:JSON.stringify({
                    date: this.marks[this.valSlide],
                    dateunit: this.du,
                    status: this.status
                })
            })
            .then(response=> response.json())
            // .then(response=> response.text())
            .then(data=>{
                this.queryEquip = data
                this.qxAxis = [];
                this.qc1y = [];
                this.qc2y = [];
                this.qc3y = [];
                for(var item of this.queryEquip){
                    this.qxAxis.push(item.equipNum);
                    this.qc1y.push(item.processTime);
                    this.qc2y.push(item.andonNum)
                    this.qc3y.push(item.rate)
                }

                this.option.xAxis.data = this.qxAxis;
                this.option.series[0].data = this.qc1y;
                this.option.series[1].data = this.qc2y;
                this.option.series[2].data = this.qc3y;

                 this.chart.setOption(this.option);

            })
            .catch(data =>{
                alert('error')
            })
        }
    },
    mounted(){
        var c1 = document.getElementById("Equipchart");
        
        this.chart = echarts.init(c1);
        this.chart.setOption(this.option);

        bus.$on("TopIssuePrm",msg =>{
            this.du = msg.dateunit
            this.start = msg.starttime
            this.end = msg.endtime
            this.status = msg.status
            this.getSlideData()
        })
    }
}
</script>