<template>
    <div class="container">
        <div class="plugins-tips">
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
        <v-line-chart></v-line-chart>
        <v-pie-chart></v-pie-chart>
    </div>
</template>

<script>
import LineChart from './CategoryChart/Linechart.vue';
import PieChart from './CategoryChart/Piechart.vue';
import echarts from 'echarts';
import bus from '../../common/bus'
export default {
    name:"AndonCategory",
    components:{
        'vLineChart':LineChart,
        'vPieChart':PieChart
    },
    data(){
        return {
            checked1:false,
            checked2:false,
            value1:null,
            radio:'',
            charts1:null,
            charts2:null,
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
        }
    },
    methods:{
        startPlant(){
            if(this.radio == ''){
                this.$message.warning("请选择日期单位！")
                return;
            }
            if(this.value1 == null){
                this.$message.warning("请选择日期范围！")
                return;
            }
            var status = ''
            if(this.checked1 && this.checked2){
                status = '全部'
            }else if(this.checked1){
                status = '已停机'
            }else if(this.checked2){
                status = '未停机'
            }else{
                this.$message.warning("请选择设备状态！");
                return;
            }

            bus.$emit('catePrm',{dateunit:this.radio,starttime:this.value1[0],endtime:this.value1[1],status:status});
            
            // console.log("status:"+status)
          
            // console.log("radio:"+this.radio)
            // console.log("start:"+this.value1[0])
            // console.log("end:"+this.value1[1])
        }
    },
    mounted(){
    }
}
</script>