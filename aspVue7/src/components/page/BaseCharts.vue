/* eslint-disable no-console */
<template>
    <div>
        <div class="crumbs">
                <el-dropdown trigger="click">
                    <span class="el-dropdown-link">
                        <el-button>{{plName}}<i class="el-icon-arrow-down el-icon--right"></i></el-button>
                    </span>
                    <el-dropdown-menu slot="dropdown">
                        <el-dropdown-item v-for="item in prodLine" :key="item.indexId" v-bind:value="item.indexId" ><a @click="setProdLineVal(item.name)">{{item.name}}</a></el-dropdown-item>
                    </el-dropdown-menu>
                </el-dropdown>
                <el-dropdown trigger="click">
                    <span class="el-dropdown-link">
                        <el-button>{{dtUnit}}<i class="el-icon-arrow-down el-icon--right"></i></el-button>
                    </span>
                    <el-dropdown-menu slot="dropdown">
                        <el-dropdown-item v-for="item in dateUnit" :key="item.indexId" v-bind:value="item.indexId" ><a @click="setDateUnitVal(item.name)">{{item.name}}</a></el-dropdown-item>
                    </el-dropdown-menu>
                </el-dropdown>
                <el-date-picker
                    v-model="value2"
                    type="daterange"
                    align="right"
                    value-format="yyyy-MM-dd"
                    unlink-panels
                    range-separator="至"
                    start-placeholder="开始日期"
                    end-placeholder="结束日期"
                    :picker-options="pickerOptions">
                </el-date-picker>
                <el-button @click="startPlant">开始渲染</el-button>

            <el-breadcrumb separator="/">
                <el-breadcrumb-item>
                    <i class="el-icon-pie-chart"></i> schart图表
                </el-breadcrumb-item>
            </el-breadcrumb>
        </div>
        <div class="container">
            <div class="plugins-tips">
                vue-schart：vue.js封装sChart.js的图表组件。
                访问地址：
                <a
                    href="https://github.com/lin-xin/vue-schart"
                    target="_blank"
                >vue-schart</a>
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
                <div class="content-title">环形图</div>
                <schart class="schart" canvasId="ring" :options="options4"></schart>
            </div>
        </div>
    </div>
</template>

<script>
import Schart from 'vue-schart';
import OEEchart from './charts/OEEchart.vue';
import EFFchart from './charts/EFFchart.vue';
import FTTchart from './charts/FTTchart.vue';

import bus from '../common/bus'
export default {
    name: 'basecharts',
    components: {
        Schart,
        OEEchart,
        EFFchart,
        FTTchart
    },
    data() {
        return {
            plName: "产线名称",
            dtUnit: "日期单位",
            prodLine:[{indexId:1,name:"GEN_III_A+M"},
                      {indexId:2,name:"GEN_III_V+F"},
                      {indexId:3,name:"GEN_III_BPV"},
                      {indexId:4,name:"GDI阀装配线"},
                      {indexId:5,name:"LP阀装配线"},
                      {indexId:6,name:"柔性线"},
                      {indexId:7,name:"ETV装配线"},
                      {indexId:8,name:"GEN_II阀装配线"},
                      {indexId:9,name:"EGR_Group"}],

            dateUnit:[{indexId:1,name:"日"},
                       {indexId:2,name:"周"},
                       {indexId:3,name:"月"}],
            
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
            value2: '',


            options1: {
                type: 'bar',
                title: {
                    text: '最近一周各品类销售图'
                },
                bgColor: '#fbfbfb',
                labels: ['周一', '周二', '周三', '周四', '周五'],
                datasets: [
                    {
                        label: '家电',
                        fillColor: 'rgba(241, 49, 74, 0.5)',
                        data: [234, 278, 270, 190, 230]
                    },
                    {
                        label: '百货',
                        data: [164, 178, 190, 135, 160]
                    },
                    {
                        label: '食品',
                        data: [144, 198, 150, 235, 120]
                    }
                ]
            },
            options2: {
                type: 'line',
                title: {
                    text: '最近几个月各品类销售趋势图'
                },
                bgColor: '#fbfbfb',
                labels: ['6月', '7月', '8月', '9月', '10月'],
                datasets: [
                    {
                        label: '家电',
                        data: [234, 278, 270, 190, 230]
                    },
                    {
                        label: '百货',
                        data: [164, 178, 150, 135, 160]
                    },
                    {
                        label: '食品',
                        data: [114, 138, 200, 235, 190]
                    }
                ]
            },
            options3: {
                type: 'pie',
                title: {
                    text: '服装品类销售饼状图'
                },
                legend: {
                    position: 'left'
                },
                bgColor: '#fbfbfb',
                labels: ['T恤', '牛仔裤', '连衣裙', '毛衣', '七分裤', '短裙', '羽绒服'],
                datasets: [
                    {
                        data: [334, 278, 190, 235, 260, 200, 141]
                    }
                ]
            },
            options4: {
                type: 'ring',
                title: {
                    text: '环形三等分'
                },
                tooltip:{},
                showValue: false,
                legend: {
                    selectMode:true,
                    position: 'bottom',
                    bottom: 40
                },
                bgColor: '#fbfbfb',
                labels: ['vue', 'react', 'angular'],
                datasets: [
                    {
                        data: [500, 500, 500]
                    }
                ]
            }
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
           
            bus.$emit("Query",{dateunit:this.dtUnit,prodline:this.plName,starttime:this.value2[0],endtime:this.value2[1]})
            // eslint-disable-next-line no-console
            console.log(this.dtUnit+" "+this.plName+" "+this.value2[0]+" "+this.value2[1]);
        }
    }
};
</script>

<style scoped>
.schart-box {
    display: inline-block;
    margin: 20px;
}
.schart {
    width: 600px;
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