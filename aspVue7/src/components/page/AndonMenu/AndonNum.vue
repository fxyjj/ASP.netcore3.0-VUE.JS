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
                 <el-button @click="startPlant" :style="{'margin-left':'30px',height:'33px','margin-bottom':'-12px'}">查询</el-button></div>
        <vegr></vegr>
        <vcm></vcm>
        <vic></vic>
        <vptcth></vptcth>
        
    </div>
</template>

<script>
import echarts from 'echarts';
import EGRchart from './NumCharts/EGRchart.vue';
import CMchart from './NumCharts/CMchart.vue';
import ICchart from './NumCharts/ICchart.vue';
import PTCTHchart from './NumCharts/PTCTHchart.vue';
import bus from '../../common/bus'

export default {
    name:'andonNumber',
    components:{
        'vegr':EGRchart,
        'vcm':CMchart,
        'vic':ICchart,
        'vptcth':PTCTHchart
    },
    data(){
        return {
            radio:'',
            value1:null,
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
            }
        }
    },
    methods:{
        startPlant(){
            // eslint-disable-next-line no-console
            console.log(this.value1)
             if(this.radio == ''){
                this.$message.warning('请选择日期单位！');
                return;
            }
            if(this.value1 == null){
                this.$message.warning('请选择日期范围！');
                return;
            }
            bus.$emit('AndonNumquery',{dateunit:this.radio,start:this.value1[0],end:this.value1[1]})
            // eslint-disable-next-line no-console
            console.log(this.radio); 
            // eslint-disable-next-line no-console
            console.log(this.value1[0]); 
            // eslint-disable-next-line no-console
            console.log(this.value1[1]); 
        }
    },
    mounted(){
        
    }
}
</script>