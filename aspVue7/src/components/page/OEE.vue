/* eslint-disable no-console */
<template>
    <div >

        <div id="bgPanel" class="container" style = "margin-top:-10px;">
            <el-row>
                <el-col :span="21"> 
                    <div class="plugins-tips" v-if="!status">
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
                    unlink-panels
                    range-separator="至"
                    start-placeholder="开始日期"
                    end-placeholder="结束日期"
                    :picker-options="pickerOptions" style="height:33px;padding-top:2px">
                </el-date-picker><!--value-format="yyyy-MM-dd"-->
                <el-button @click="startPlant" :style="{height:'33px','margin-bottom':'-12px'}">查询</el-button>

               
                <el-dialog
                title="轮播设置"
                :visible.sync="dialogVisible"
                width="30%"
                :before-close="handleClose">
                    <el-divider content-position="center" :style='{"font-size":"30px"}'><i class="el-icon-menu"></i> 当前工作组</el-divider>
                    <span ><strong :style='{"margin-left":"35%","font-size":"50px"}'>{{group}}</strong></span>
                    <el-divider content-position="center" :style='{"font-size":"30px"}'><i id="funicon" class="el-icon-s-tools"></i> 请选择想要轮播的产线</el-divider>
                    <el-checkbox :indeterminate="isIndeterminate" v-model="checkAll" @change="handleCheckAllChange">全选</el-checkbox>
                    <div style="margin: 15px 0;"></div>
                    <el-checkbox-group v-model="checkedprodLine" @change="handleCheckedLinesChange">
                        <el-checkbox v-for="line in prodLine" :label="line.name" :key="line.indexId">{{line.name}}</el-checkbox>
                    </el-checkbox-group>

                    <el-divider content-position="center" :style='{"font-size":"25px"}'><i class="el-icon-odometer"></i> 当前日期单位</el-divider>
                     <span :style='{"margin-left":"45%","font-size":"50px"}'><strong>{{dtUnit}}</strong></span>
                    <el-divider content-position="center" :style='{"font-size":"50px"}'><i class="el-icon-date"></i> 当前日期范围</el-divider>
                     <span>
                         <strong v-if="value1!=null" :style='{"margin-left":"15%","font-size":"30px"}'>{{value1[0]}} --- {{value1[1]}}</strong>
                         <strong v-else>{{value2[0]}} --- {{value2[1]}}</strong>
                     </span>
                   
                    <span slot="footer" class="dialog-footer">
                        <el-button @click="dialogVisible = false">取 消</el-button>
                        <el-button type="primary" @click="ctrlplay">确 定</el-button>
                    </span>
                </el-dialog>
            </div>
            <div v-else>
                 <el-carousel :interval="10000" type="card" height="60px" :style='{width:"80%",height:"60px","margin-bottom":"-5px","margin-top":"-5px","margin-left":"100px","text-align":"center","line-height":"60px"}' indicator-position="none" arrow="never" @change="modifyData" :autoplay="pauseS">
                    <el-carousel-item v-for="item in checkedprodLine" :key="item">
                        <!-- <h5 class="medium">工作组：{{group}}</h5> -->
                        <h5 class="medium" :style='{"font-size":"40px"}'>{{ item }}</h5>
                    </el-carousel-item>
                </el-carousel>
               

            </div>
            </el-col>
            <el-col :span="3">
                <el-row>
                    <el-col :span="12"> 
                        <el-tooltip effect="dark" :content="notice" placement="top">
                            <div  id="ctrlBtn" class="start" @click="setPlayattr">
                                <span v-if="pauseS"><i class="el-icon-video-pause"></i></span>
                                <span v-else><i class="el-icon-video-play"></i></span>
                            </div>
                        </el-tooltip>
                    </el-col>
                     <el-col :span="12"> 
                        <el-tooltip effect="dark" content="重新开始" placement="top">
                            <div  id="pauseBtn"  @click="restartCtrl">
                                <span ><i class="el-icon-refresh-right"></i></span>
                            </div>
                        </el-tooltip>
                    </el-col>
                </el-row>
               
                
               
            </el-col>
        </el-row>
         <el-divider></el-divider>
         <el-row>
             <el-col :span="12"><o-e-echart></o-e-echart></el-col>
             <el-col :span="12"><e-f-fchart></e-f-fchart></el-col>
         </el-row>
         <el-row>
             <el-col :span="12"><f-t-tchart></f-t-tchart></el-col>
             <el-col :span="12"><t-j-lchart></t-j-lchart></el-col>
         </el-row>
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
            //复选框参数
            checkAll: false,
            checkedprodLine: [],
            isIndeterminate: true,
            //弹出框和轮播按钮参数
            dialogVisible:false,
            status:false,
            //开始暂停指示值pause，true正在运行，false走马灯停止自动切换
            pauseS:false,
            notice:"开始",
            //查询参数
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
            value1: null,
            value2:new Date(),
            index:0,
            //time updater
            interval:null
        };
    },
    methods: {
        //查询方程
        setProdLineVal(indx){
            this.plName = indx;
           
        },
        setDateUnitVal(indx){
            this.dtUnit = indx;
           
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
            if(this.dtUnit == '日期单位'){
                this.$message.warning('请选择日期单位！');
                return;
            }
           
            if(this.value1 == null){
                this.$message.warning('请选择日期范围！');
                return;
            }
            
            bus.$emit("Query",{dateunit:this.dtUnit,prodline:this.plName,starttime:this.value1[0],endtime:this.value1[1]})
            // eslint-disable-next-line no-console
            // console.log(this.dtUnit+" "+this.plName+" "+this.value1[0]+" "+this.value1[1]);
        },
        setGroupVal(indx){
            this.group = indx;
            this.plName = "产线名称";
           
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
        },
        
        //轮播按钮控制方程
        setPlayattr(){
            if(this.group == '工作组'){
                this.$message.warning('请首先选择工作组！');
                return;
            }
            if(this.dtUnit == '日期单位'){
                this.$message.warning('请首先选择日期单位！');
                return;
            }
            if(this.value1 == null){
                this.$message.warning('请首先选择日期范围！');
                return;
            }
           
            if(this.status){
                this.pauseS = !this.pauseS;
                var btn = document.getElementById("ctrlBtn");
                if(this.pauseS){
                    this.notice="暂停";
                    btn.style.background="#82D900";
                }else{
                    this.notice= "开始";
                    btn.style.background="#FFD306";
                }
               
            }else{
                this.dialogVisible = true;
            }
        },
        //重新开始按钮
        restartCtrl(){
            var btn = document.getElementById("ctrlBtn");
            document.getElementById("pauseBtn").style.visibility = "hidden";
            this.pauseS = false;
            this.status = !this.status;
            btn.style.background="";
            this.startPlant();
        },
        //弹出框及轮播按钮交互控制方程
        ctrlplay(){
            if(this.checkedprodLine.length <= 1){
                this.$message.warning('请至少选择两条产线进行轮播！');
                return;
            }
            var btn = document.getElementById("ctrlBtn");
            this.dialogVisible = false;
            this.status=!this.status;
            btn.style.background= "#82D900";
            this.notice = "暂停";
            this.pauseS = true;
            document.getElementById("pauseBtn").style.visibility = "visible";
            //console.log(this.checkedprodLine)
            bus.$emit("Query",{dateunit:this.dtUnit,prodline:this.checkedprodLine[0],starttime:this.value1[0],endtime:this.value1[1]})
        },
        handleClose(done) {
            this.$confirm('确认关闭？')
            .then(_ => {
                done();
            })
            .catch(_ => {});
        },

        //弹出框参数交互方程--复选框控制
         handleCheckAllChange(val) {
            var ckl = [];
            for(var item of this.prodLine){
                ckl.push(item.name);
            }
            this.checkedprodLine= val ? ckl : [];
            this.isIndeterminate =  !this.isIndeterminate;
        },
        handleCheckedLinesChange(value) {
            let checkedCount = value.length;
            this.checkAll = checkedCount === this.checkedprodLine.length;
            this.isIndeterminate = checkedCount > 0 && checkedCount < this.prodLine.length;
        },
        //轮播更新表格数据函数
        modifyData(value){
            bus.$emit("Query",{dateunit:this.dtUnit,prodline:this.checkedprodLine[value],starttime:this.value1[0],endtime:this.value1[1]})
            console.log("走马灯"+value);
        }
    },
    watch:{
        'status':{
            handler:function(oldV,newV){
                bus.$emit("colorMod",newV);
                var panel =  document.getElementById("bgPanel");
                console.log("status"+newV);
                if(!newV){
                   panel.style.background = "#324157";
                   this.interval = setInterval(() => {
                       const nowtime = new Date()
                    //    console.log(nowtime.getDay())
                    //    if(nowtime.getDay()=='13'){
                    //        console.log("damnyes");
                    //    }
                       if((this.value1[1].getDate() < nowtime.getDate() || (this.value1[1].getMonth() < nowtime.getMonth() && this.value1[1].getDate() > nowtime.getDate())) && nowtime.getHours() == '8'){
                            // console.log("oldtme"+this.value1[0])
                            // console.log("oldtme"+this.value1[1])
                            this.value1[0]=new Date((this.value1[0]).getTime()+3600*24*1000)
                            this.value1[1]=new Date((this.value1[1]).getTime()+3600*24*1000)
                            // console.log("newtme"+this.value1[1])
                            // console.log("更新日期需要")
                       }
                   },86400000);// 
                }else{
                   panel.style.background = "";
                   clearInterval(this.interval)
                }
            }
        }
    },
    mounted(){
        const oldtime = new Date(Date.now() - 30*24*60*60*1000)
        console.log(oldtime+"dame"+Date.now())
        const newtime = new Date()
        this.value1 = [oldtime,newtime]
        this.dtUnit = "日"
        this.plName = "GEN_III_A+M"
        this.group = "EGR线"
        this.prodLine = this.EGRLine;
        console.log(oldtime);
        console.log(newtime)
        bus.$emit("Query",{dateunit:"日",prodline:"GEN_III_A+M",starttime:oldtime,endtime:newtime})
    },
    beforeDestroy(){
        this.checkedprodLine = [];
    }
  
};
</script>

<style scoped>
.start {
    width:40px;
    height:40px;
    font-size:36px;
    text-align:center;
    /* background; */
    border-radius: 25px;
    /* display:inline-block; */
    box-shadow: 0 2px 12px 0 rgba(0, 0, 0, 0.1);
    /* float:right; */
    margin-left:30px;
    margin-top:-10px;
    cursor:pointer;    
}

.content-title {
    clear: both;
    font-weight: 400;
    line-height: 50px;
    margin: 10px 0;
    font-size: 22px;
    color: #1f2f3d;
}

 .el-carousel__item h3 {
    color: #475669;
    font-size: 14px;
    opacity: 0.75;
    line-height: 30px;
    margin: 0;
  }
  
  .el-carousel__item:nth-child(2n) {
    background-color: #99a9bf;
  }
  
  .el-carousel__item:nth-child(2n+1) {
    background-color: #d3dce6;
  }

#funicon {
  -webkit-animation: spin 5s linear infinite;
  animation: spin 5s linear infinite;
}
@keyframes spin {
  from {
    transform: rotate(0deg);
  }
  to {
    transform: rotate(360deg);
    transition: all 5s;
  }
}

#pauseBtn{
    background:#CC3300;
    width:40px;
    height:40px;
    font-size:36px;
    border-radius:25px;
    cursor:pointer;
    margin-top:-10px;
    visibility:hidden;
}

</style>