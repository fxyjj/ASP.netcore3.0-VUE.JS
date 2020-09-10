/* eslint-disable no-console */
<template>
    <div>

        <div class="container" style = "margin-top:-10px">
            
            <div class="plugins-tips" >
                <el-dropdown trigger="click">
                    <span class="el-dropdown-link">
                        <el-button>{{group}}<i class="el-icon-arrow-down el-icon--right"></i></el-button>
                    </span>
                    <el-dropdown-menu slot="dropdown">
                        <el-dropdown-item v-for="item in stationGroup" :key="item.indexId" v-bind:value="item.indexId" ><el-button type="text" :style="{color:'#808080'}" @click="setGroupVal(item.name)">{{item.name}}</el-button></el-dropdown-item>
                    </el-dropdown-menu>
                </el-dropdown>
               <el-dropdown trigger="click">
                    <span class="el-dropdown-link">
                        <el-button @click="checkline">{{plName}}<i class="el-icon-arrow-down el-icon--right"></i></el-button>
                    </span>
                    <el-dropdown-menu slot="dropdown">
                        <el-dropdown-item v-for="item in prodLine" :key="item.indexId" v-bind:value="item.indexId" ><el-button type="text" :style="{color:'#808080'}" @click="setProdLineVal(item.name)">{{item.name}}</el-button></el-dropdown-item>
                    </el-dropdown-menu>
                </el-dropdown>
                <el-dropdown trigger="click">
                    <span class="el-dropdown-link">
                        <el-button>{{dtUnit}}<i class="el-icon-arrow-down el-icon--right"></i></el-button>
                    </span>
                    <el-dropdown-menu slot="dropdown">
                        <el-dropdown-item v-for="item in dateUnit" :key="item.indexId" v-bind:value="item.indexId" ><el-button type="text" :style="{color:'#808080'}" @click="setDateUnitVal(item.name)">{{item.name}}</el-button></el-dropdown-item>
                    </el-dropdown-menu>
                </el-dropdown>
                <el-date-picker
                    v-model="value1"
                    type="daterange"
                    align="right"
                    value-format="yyyy-MM-dd"
                    unlink-panels
                    range-separator="至"
                    start-placeholder="开始日期"
                    end-placeholder="结束日期"
                    :picker-options="pickerOptions" style="height:33px;padding-top:2px">
                </el-date-picker>
                <el-button @click="startPlant" :style="{height:'33px','margin-bottom':'-12px'}">查询</el-button>
            </div>
            <div class="schart-box">
                <!-- <div class="content-title">柱状图</div>
                <schart class="schart" canvasId="bar" :options="options1"></schart> -->
                <o-e-echart></o-e-echart>
            </div>
            <div class="schart-box">
                <!-- <div class="content-title">折线图</div>
                <schart class="schart" canvasId="line" :options="options2"></schart> -->
               <e-f-fchart></e-f-fchart>
            </div>
            <div class="schart-box">
                <!-- <div class="content-title">饼状图</div>
                <schart class="schart" canvasId="pie" :options="options3"></schart> -->
                <f-t-tchart></f-t-tchart>
            </div>
            <div class="schart-box">
                <t-j-lchart></t-j-lchart>
            </div>
        </div>
    </div>
</template>

<script>
// import Schart from 'vue-schart';
import OEEchart from './OEEcharts/OEEchart.vue';
import EFFchart from './OEEcharts/EFFchart.vue';
import FTTchart from './OEEcharts/FTTchart.vue';
import TJLchart from './OEEcharts/TJLchart.vue';

import bus from '../common/bus'
export default {
    name: 'OEEpage',
    components: {
        OEEchart,
        EFFchart,
        FTTchart,
        TJLchart
    },
    data() {
        return {
            group:"工作组",
            plName: "产线名称",
            dtUnit: "日期单位",
            prodLine:[],
            EGRLine:[{indexId:1,name:"GEN_III_A+M"},
                      {indexId:2,name:"GEN_III_V+F"},
                      {indexId:3,name:"GEN_III_BPV"},
                      {indexId:4,name:"GDI阀装配线"},
                      {indexId:5,name:"LP阀装配线"},
                      {indexId:6,name:"柔性线"},
                      {indexId:7,name:"ETV装配线"},
                      {indexId:8,name:"GEN_II阀装配线"},
                      {indexId:9,name:"EGR_Group"}],
            CMLine:[{indexId:1,name:"混合管装配线1"},
                      {indexId:2,name:"混合管装配线2"},
                      {indexId:3,name:"Cooler Line 1"},
                      {indexId:4,name:"Cooler Line 2"},
                      {indexId:5,name:"Cooler Line 3"},
                      {indexId:6,name:"Cooler Line 4"},
                      {indexId:7,name:"钎焊炉1"},
                      {indexId:8,name:"钎焊炉2"},
                      {indexId:9,name:"测试台1"},
                      {indexId:10,name:"测试台2"},
                      {indexId:11,name:"测试台3"},
                      {indexId:12,name:"测试台4"},
                      {indexId:13,name:"模块线1"}],
            ELELine:[{indexId:1,name:"HV Air PTC Line"},
                      {indexId:2,name:"LV Air PTC Line"},
                      {indexId:3,name:"Liquid Heater Line"},
                      {indexId:4,name:"eCRV Line"},
                      {indexId:5,name:"Marsilli 1"},
                      {indexId:6,name:"Marsilli 2"},
                      {indexId:7,name:"IPTE 1"},
                      {indexId:8,name:"IPTE 2"},
                      {indexId:9,name:"Huebers 1"},
                      {indexId:10,name:"Huebers 2"},
                      {indexId:11,name:"Ford节温器线"},
                      {indexId:12,name:"HKMC节温器线"},
                      {indexId:13,name:"GAC节温器线"},
                      {indexId:14,name:"JLR节温器线"},
                      {indexId:15,name:"蜡包装配线"},
                      {indexId:16,name:"SGMM节温器线"},
                      {indexId:17,name:"SGME节温器线"}],

            dateUnit:[{indexId:1,name:"日"},
                       {indexId:2,name:"周"},
                       {indexId:3,name:"月"}],
            stationGroup:[{indexId:1,name:"EGR线"},
                          {indexId:2,name:"Cooler/Module线"},
                          {indexId:3,name:"电子线"}],
            
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
            value1: null
        };
    },
    methods: {
        setProdLineVal(indx){
            this.plName = indx;
            // eslint-disable-next-line no-console
            console.log(indx)
        },
        setDateUnitVal(indx){
            this.dtUnit = indx;
            // eslint-disable-next-line no-console
            console.log(indx)
        },
        startPlant(){
            if(this.group == '工作组'){
                this.$message.warning('请选择工作组！');
                return;
            }
            if(this.plName == '产线名称'){
                this.$message.warning('请选择产线名称！');
                return;
            }
            if(this. dtUnit == '时间单位'){
                this.$message.warning('请选择日期单位！');
                return;
            }
           
            if(this.value1 == null){
                this.$message.warning('请选择日期范围！');
                return;
            }
            
            bus.$emit("Query",{dateunit:this.dtUnit,prodline:this.plName,starttime:this.value1[0],endtime:this.value1[1]})
            // eslint-disable-next-line no-console
            console.log(this.dtUnit+" "+this.plName+" "+this.value1[0]+" "+this.value1[1]);
        },
        setGroupVal(indx){
            this.group = indx;
            this.plName = "产线名称";
            // eslint-disable-next-line no-console
            console.log(indx);
            if(indx == 'EGR线'){
                this.prodLine = this.EGRLine;
            }else if(indx == 'Cooler/Module线'){
                this.prodLine = this.CMLine;
            }else{
                this.prodLine = this.ELELine;
            }
        },
        checkline(){
            if(this.prodLine.length == 0){
                this.$message.warning('请首先选择工作组！！');
            }
        }
    }
};
</script>

<style scoped>
.schart-box {
    display: inline-block;
    margin-left: 20px;
    margin-right: 20px;
    margin-top: 0px;
    margin-bottom: -20px;
}
.schart {
    width: 500px;
    height: 400px;
}
.content-title {
    clear: both;
    font-weight: 400;
    line-height: 50px;
    margin: 10px 0;
    font-size: 22px;
    color: #1f2f3d;
}
</style>