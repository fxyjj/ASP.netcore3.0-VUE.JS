<template>
    <div class="container">
        <div class="plugins-tips" >
            <el-date-picker
                style="margin-top:-6px;margin-left:15px;"
                v-model="value1"
                type="daterange"
                align="right"
                unlink-panels
                range-separator="至"
                start-placeholder="开始日期"
                end-placeholder="结束日期"
                :picker-options="pickerOptions">
                </el-date-picker>
                 <div class="block" style = "float:left;margin-top:5px">
                    <el-radio v-model="radio" label="日">日</el-radio>
                    <el-radio v-model="radio" label="周">周</el-radio>
                    <el-radio v-model="radio" label="月">月</el-radio>
                </div>
                 <el-checkbox v-model="checked1" style="padding-left:20px">已停机</el-checkbox>
                 <el-checkbox v-model="checked2">未停机</el-checkbox>
                 <el-button @click="startPlant" :style="{'margin-left':'30px',height:'33px','margin-bottom':'-12px'}">查询</el-button>
        </div>
        <el-row>
        <div id="chart1" :style="{width:'50%',height:'500px',display:'inline-block'}"></div>
        <div id="chart2" :style="{width:'50%',height:'500px',display:'inline-block'}"></div>
        </el-row>
    </div>
</template>

<script>
import echarts from 'echarts';

export default {
    name:'AndonMenu',
    data(){
        return {
            checked1:true,
            checked2:true,
            charts1: null,
            charts2: null,
            queryData:[],
            cxAxis:[],
            c1yAxis1:[],
            c1yAxis2:[],
            c2yAxis1:[],
            c2yAxis2:[],
            c2yAxis3:[],
            option1: {
                title: {
                    text: '安灯工时损失及占比',
                    left: '50%',
                    textAlign: 'center'
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
                    orient:'horizontal',
                    selectMode:true,
                    data:['损失工时','安灯工时占比']
                },
                xAxis: {
                    data: ["1","2","3","4","5","6"]
                },
                yAxis:  [{
                            type: 'value',
                            scale: true,
                            name: 'Hour',
                            min:0,
                            boundaryGap: [0.2, 0.2]
                        },
                        {
                            type: 'value',
                            scale: true,
                            name: 'Rate',
                            min:0,
                            max:20,
                            axisLabel: {  
                                show: true,  
                                interval: 'auto',  
                                formatter: '{value} %'  
                            },  
                            boundaryGap: [0.2, 0.2]
                        }
                    ],
                series: [{
                            name: '损失工时',
                            type: 'line',
                            // data: [5,20,36,10,10,20],
                            data:[3,7,12,6,5,1],
                            yAxisIndex: 0,
                            itemStyle:{
                                normal:{
                                     label:{
                                        show:true,
                                        position:'top'
                                    },
                                },
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
                            yAxisIndex: 1,
                            data: [1,4,7,2,2,4],
                            itemStyle:{
                                 normal:{
                                     label:{
                                        show:true,
                                        position:'top'
                                    },
                                },
                                color: ["#FFD306"]
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
                    text: '安灯条数，修复及响应',
                    left: '50%',
                    textAlign: 'center'
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
                    data:['安灯条数','MTTR修复时间','平均响应时间']
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
                                 normal:{
                                     label:{
                                        show:true,
                                        position:'top'
                                    },
                                },
                                color:"#0070C0"
                            },
                            lineStyle:{
                                normal:{
                                    width:3
                                }
                            }
                        },
                        {
                            name: 'MTTR修复时间',
                            type: 'line',
                            data: [5,20,36,10,10,20],
                            itemStyle:{
                                 normal:{
                                     label:{
                                        show:true,
                                        position:'top'
                                    },
                                },
                                color: ["#c0504d"]
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
                                 normal:{
                                     label:{
                                        show:true,
                                        position:'top'
                                    },
                                },
                                color: ["#C0C0C0"]
                            },
                            lineStyle:{
                                normal:{
                                    width:3
                                }
                            }
                        }],
            },
             pickerOptions: {
                shortcuts: [{
                    text: '最近一周',
                    onClick(picker) {
                    const end = new Date();
                    const start = new Date();
                    start.setTime(start.getTime() - 3600 * 1000 * 24 * 7);
                    picker.$emit('pick', [start, end]);
                    }
                }, {
                    text: '最近一个月',
                    onClick(picker) {
                    const end = new Date();
                    const start = new Date();
                    start.setTime(start.getTime() - 3600 * 1000 * 24 * 30);
                    picker.$emit('pick', [start, end]);
                    }
                }, {
                    text: '最近三个月',
                    onClick(picker) {
                    const end = new Date();
                    const start = new Date();
                    start.setTime(start.getTime() - 3600 * 1000 * 24 * 90);
                    picker.$emit('pick', [start, end]);
                    }
                }]
            },
            value1: null,
            radio:''
        }
    },
    watch: {
        'queryData.length':{
            handler:function(newval,oldval){
                if(newval !== oldval){
                    // eslint-disable-next-line no-console
                    console.log(newval+" 到 "+oldval)
                    this.cxAxis = [];

                    this.c1yAxis1 = [];
                    this.c1yAxis2 = [];

                    this.c2yAxis1 = [];
                    this.c2yAxis2 = [];
                    this.c2yAxis3 = [];
                    var monthName = ['Jan','Feb','Mar','Apr','May','Jun','Jul','Aug','Sept','Oct','Dec','Nov']
                    for(var res of this.queryData){
                        if(this.radio == '月'){
                            this.cxAxis.push(monthName[Number(res.dateunit)-1]);
                        }else{
                            this.cxAxis.push(res.dateunit);
                        }
                        // this.cxAxis.push(res.dateunit);

                        this.c1yAxis1.push(res.planTime.toFixed(2));
                        this.c1yAxis2.push(res.procssTime.toFixed(2));

                        this.c2yAxis1.push(res.andonNum);
                        this.c2yAxis2.push(res.repairTime.toFixed(2));
                        this.c2yAxis3.push(res.avgRpsTime.toFixed(2));
                        
                        }
                    // eslint-disable-next-line no-console
                    console.log(this.cyAxis1);
                    this.option1.xAxis.data = this.cxAxis;
                    this.option1.series[0].data = this.c1yAxis1;
                    this.option1.series[1].data = this.c1yAxis2;
                    var c1 = document.getElementById("chart1");
                    this.charts1 = echarts.init(c1);
                    this.charts1.setOption(this.option1);

                    this.option2.xAxis.data = this.cxAxis;
                    this.option2.series[0].data = this.c2yAxis1;
                    this.option2.series[1].data = this.c2yAxis2;
                    this.option2.series[2].data = this.c2yAxis3;

                    var c2 = document.getElementById("chart2");
                    this.charts2 = echarts.init(c2);
                    this.charts2.setOption(this.option2);
                }    
        },
        deep:true//对象内部的属性监听，也叫深度监听
      },
    },
    methods:{
        startPlant(){
            if(this.radio == ''){
                this.$message.warning('请选择日期单位！');
                return;
            }
            if(this.value1 == null){
                this.$message.warning('请选择日期范围！');
                return;
            }
            if(!this.checked1 && !this.checked2){
                this.$message.warning('请选择设备状态！');
                return;
            }
            this.queryData = []
            console.log("jack "+this.radio)
            var status = '';
            if(this.checked1 && this.checked2){
                status = '全部';
            }else if(this.checked1){
                status = '已停机';
            }else{
                status = '未停机';
            }
            fetch('api/Andon/hourRate',{
                method:'POST',
                headers:{
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify({
                    start: this.value1[0],
                    end: this.value1[1],
                    dateunit: String(this.radio),
                    stus:status

                })
            })
            .then(response => response.json())
            // .then(response => response.text())
            .then(data =>{
                this.queryData = data;
                // alert(data);
                // eslint-disable-next-line no-console
                console.log(this.queryData)
            })
            .catch(data =>{
                alert('error')
            })
           
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
