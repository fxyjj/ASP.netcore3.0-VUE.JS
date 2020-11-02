/* eslint-disable no-console */
<template>
    <div>
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
                         <strong v-if="value1!=null" :style='{"margin-left":"15%","font-size":"30px"}'>{{dateToString(value1[0])}} --- {{dateToString(value1[1])}}</strong>
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

            <el-dialog :visible.sync="showTab" title="日报工单总览">
               
                <!-- <h3>{{month}}月{{day}}日的{{type}}报工单总览</h3> -->
                <el-menu :default-active="activeIndex" class="el-menu-demo" mode="horizontal" @select="handleSelect" style="margin-top:-40px">
                    <el-menu-item v-for="item in menus" :key="item.label" :index="item.label">{{item.timeCate}}:{{item.name}}</el-menu-item>
                </el-menu>
                 <div v-if="wIcon">
                    <i class="el-icon-loading"></i>
                </div>
                <div class="dialogDisp" v-else>
                    <div class="basicInfo">
                        <el-row style="border-bottom: 1px solid #000">
                            <el-col :span="8" >
                                <div class="tip"> <h3>报工编号：{{menus[disNo].bgNo}}</h3></div>
                            </el-col>
                        </el-row> 
                        <el-row style="border-bottom: 1px solid #000">
                            <el-col :span="8"><div class="tip"><h3>生产日期：{{menus[disNo].birth}}</h3></div></el-col>
                        </el-row>
                        <el-row style="border-bottom: 1px solid #000">
                            <el-col :span="6"><div class="tip"><h3>加工人：{{menus[disNo].maniMan}}</h3></div></el-col>
                            <el-col :span="6"><div class="tip"><h3>所属班组：{{menus[disNo].bclass}}</h3></div></el-col>
                            <el-col :span="6"><div class="tip"><h3>作业单号：{{menus[disNo].workNo}}</h3></div></el-col>
                        </el-row>
                        <el-row style="border-bottom: 1px solid #000">
                            <el-col :span="6"><div class="tip"><h3>物料号：{{menus[disNo].wlNo}}</h3></div></el-col>
                            <el-col :span="9"><div class="tip"><h3>物料名称：{{menus[disNo].name}}</h3></div></el-col>
                        </el-row>
                        <el-row style="border-bottom: 1px solid #000">
                            <el-col :span="6"><div class="tip"><h3>工序：{{menus[disNo].procedure}} {{menus[disNo].proceName}}</h3></div></el-col>
                        </el-row>
                        <el-row style="border-bottom: 1px solid #000">
                            <el-col :span="6"><div class="tip"><h3>完工数量：{{menus[disNo].doneNo}}</h3></div></el-col>
                            <el-col :span="6"><div class="tip"><h3>合格数量：{{menus[disNo].passNo}}</h3></div></el-col>
                            <el-col :span="6"><div class="tip"><h3>返工合格数量：{{menus[disNo].rePassNo}}</h3></div></el-col>
                            <el-col :span="6"><div class="tip"><h3>待处理：{{menus[disNo].wHandle}}</h3></div></el-col>
                        </el-row>
                        <el-row style="border-bottom: 1px solid #000">
                            <el-col :span="6"><div class="tip"><h3>不合格数量：{{menus[disNo].failNo}}</h3></div></el-col>
                            <el-col :span="4"><div class="tip"><h3>料废：{{menus[disNo].lf}}</h3></div></el-col>
                            <el-col :span="4"><div class="tip"><h3>机废：{{menus[disNo].jf}}</h3></div></el-col>
                            <el-col :span="4"><div class="tip"><h3>调废：{{menus[disNo].df}}</h3></div></el-col>
                            <el-col :span="4"><div class="tip"><h3>工废：{{menus[disNo].gf}}</h3></div></el-col>

                        </el-row>

                    </div>
                    <h3>不合格记录</h3>
                    <el-table :data="failedRec" border style="width: 100%;margin:2% 0%" height="150">
                        <el-table-column fixed prop="label" label="序号" width="100"> </el-table-column>
                        <el-table-column prop="txdate" label="填写日期" width="150"></el-table-column>
                        <el-table-column prop="pline" label="生产线" width="120"></el-table-column>
                        <el-table-column prop="sbNo" label="设备编号" width="120"> </el-table-column>
                        <el-table-column prop="failDesc" label="不合格描述" width="300"> </el-table-column>
                        <el-table-column prop="failNum" label="不良数量" width="120"> </el-table-column>
                        <el-table-column prop="shitNo" label="报废数" width="120"></el-table-column>
                        <el-table-column prop="rePassNo" label="返工合格数量" width="120"></el-table-column>
                        <el-table-column prop="writeMan" label="填写人" width="120"> </el-table-column>
                        <el-table-column prop="failNo" label="不合格单号" width="120"> </el-table-column>
                        <el-table-column prop="workNo" label="作业单号" width="120"> </el-table-column>
                        <el-table-column prop="wlNo" label="物料编号" width="120"> </el-table-column>
                        <el-table-column prop="wlDesc" label="物料描述" width="120"> </el-table-column>
                        <el-table-column prop="downNum" label="降级数" width="120"></el-table-column>
                        <el-table-column prop="sftNum" label="偏差数" width="120"></el-table-column>
                        <el-table-column prop="passNum" label="合格数" width="120"> </el-table-column>
                        <el-table-column prop="status" label="状态" width="120"> </el-table-column>
                        <el-table-column prop="bgNo" label="报工编号" width="150"> </el-table-column>
                        <!-- <el-table-column
                        fixed="right"
                        label="操作"
                        width="100">
                        <template slot-scope="scope">
                            <el-button @click="handleClick(scope.row)" type="text" size="small">查看</el-button>
                            <el-button type="text" size="small">编辑</el-button>
                        </template> 
                        </el-table-column>-->
                    </el-table>
                    <h3>停机记录</h3>
                    <el-table :data="stopRec" border style="width: 100%;margin:2% 0%" height="150">
                        <el-table-column fixed prop="stopType" label="停机类型" width="100"> </el-table-column>
                        <el-table-column prop="sbNo" label="设备编号" width="120"></el-table-column>
                        <el-table-column prop="stopDesc" label="停机描述" width="300"></el-table-column>
                        <el-table-column prop="stopBegin" label="停机开始" width="180"> </el-table-column>
                        <el-table-column prop="stopEnd" label="停机结束" width="180"> </el-table-column>
                        <el-table-column prop="testPause" label="调试暂停" width="120"> </el-table-column>
                        <el-table-column prop="writeMan" label="填写人" width="120"> </el-table-column>
                        <el-table-column prop="stopTime" label="停机时间" width="120"> </el-table-column>
                        <!-- <el-table-column
                        fixed="right"
                        label="操作"
                        width="100">
                        <template slot-scope="scope">
                            <el-button @click="handleClick(scope.row)" type="text" size="small">查看</el-button>
                            <el-button type="text" size="small">编辑</el-button>
                        </template> 
                        </el-table-column>-->
                    </el-table>
                </div>
            </el-dialog>
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
            //弹出框导航栏参数
            activeIndex:"0",
            menus:[],
            failedRec: [],
            stopRec: [],
            disNo:0,
            wIcon:true,
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
            interval:null,
            //弹窗参数
            showTab:false,
            month:'',
            day:'',
            type:'',
            //选定的柱子当前日期
            currDate:null
        };
    },
    methods: {
        //工具方程，日期转字符串
        dateToString(date){  
            var  year = date.getFullYear();  
            var  month =(date.getMonth() + 1).toString();  
            var  day = (date.getDate()).toString();   
            if(month.length == 1){  
                month = "0" + month;  
            }  
            if(day.length == 1){  
                day = "0" + day;  
            } 
            var dateTime = year + "-" + month + "-" + day; 
            return dateTime;  
        },
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
        },
        handleSelect(key, keyPath) {
            this.disNo = parseInt(key);
            console.log(key+"和"+keyPath);
            this.getTab1Data(this.menus[this.disNo].bgNo)
            this.getTab2Data(this.menus[this.disNo].bgNo)
        },
        handleClick(row) {
            console.log(row);
        },
        getTab1Data(prm){
            this.failedRec = [];
            fetch('api/Chartdata/tab1',{
                method:'POST',
                headers:{
                    'Content-Type': 'application/json',
                },
                body:JSON.stringify({
                    qDate:this.currDate,
                    bgbh:prm
                })
            }).then(response=>response.json())
            .then(data=>{
                if(data.length==0){
                    return;
                }
                for(var item of data){
                    var indx = {
                        label:item.序号,
                        txdate:item.填写日期,
                        pline:item.生产线,
                        sbNo:item.设备编号,
                        failDesc:item.不合格描述,
                        failNum:item.不合格数量,
                        shitNo:item.报废数,
                        rePassNo:item.返工合格数,
                        writeMan:item.填写人,
                        failNo:item.不合格单号,
                        workNo:item.作业单号,
                        wlNo:item.物料编号,
                        wlDesc:item.物料描述,
                        downNum:item.降级数,
                        sftNum:item.偏差数,
                        passNum:item.合格数,
                        status:item.状态,
                        bgNo:item.报工编号
                    }
                    this.failedRec.push(indx)
                }

            }).catch(data=>{
                alert(data)
            })
        },
        getTab2Data(prm){
             this.stopRec = [];
            fetch('api/Chartdata/tab2',{
                method:'POST',
                headers:{
                    'Content-Type': 'application/json',
                },
                body:JSON.stringify({
                    qDate:this.currDate,
                    bgbh:prm
                })
            }).then(response=>response.json())
            .then(data=>{
                if(data.length==0){
                    return;
                }
                for(var item of data){
                    var indx = {
                        stopType:item.停机类型,
                        sbNo:item.设备编号,
                        stopDesc:item.停机描述,
                        stopBegin:item.停机开始,
                        stopEnd:item.停机结束,
                        testPause:item.调试暂停,
                        writeMan:item.填写人,
                        stopTime:item.停机
                    }
                    this.stopRec.push(indx)
                }

            }).catch(data=>{
                alert(data)
            })
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
                       if((this.value1[1].getDate() < nowtime.getDate() || (this.value1[1].getMonth() < nowtime.getMonth() && this.value1[1].getDate() > nowtime.getDate())) && nowtime.getHours() == 8){
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
        bus.$on("pillInfo",msg=>{
            if(this.status){
                return;
            }
            this.showTab = true;
            this.month = msg.month;
            this.day = msg.day;
            this.type = msg.type;
            var qdate = new Date();
            if(this.value1[0].getFullYear()==this.value1[1].getFullYear()){
                qdate.setFullYear(this.value1[0].getFullYear(),parseInt(this.month)-1,parseInt(this.day));
                console.log("一样的 "+qdate);
            }else{
                if(parseInt(this.month) >= this.value1[0].getMonth()){
                    qdate.setFullYear(this.value1[0].getFullYear(),parseInt(this.month)-1,parseInt(this.day));
                    console.log("去年的 "+qdate)
                }else{
                     qdate.setFullYear(this.value1[1].getFullYear(),parseInt(this.month)-1,parseInt(this.day));
                      console.log("今年的 "+qdate)
                }
            }
           console.log("最后的日期： "+qdate)
           this.currDate = qdate;
            fetch('api/Chartdata/dialog1',{
                method:'POST',
                headers:{
                    'Content-Type': 'application/json',
                },
                body:JSON.stringify({
                    qDate:qdate,
                    prodLine:this.plName
                })
            }).then(response=>response.json())
            .then(data=>{
                if(data.length==0){
                    return;
                }
                var c = 0
                this.menus = []
                 var doneTime = "";
                for(var item of data){
                    if(parseInt((item.作业开始时间).substr(11,2))>16){
                        doneTime = "中班"
                    }else if(parseInt((item.作业开始时间).substr(11,2))>8){
                        doneTime = "早班"
                    }else{
                        doneTime = "晚班"
                    }
                    var stc = {
                        label:c.toString(), 
                        timeCate:doneTime,
                        name:item.物料描述,
                        bgNo:item.报工编号,
                        birth:item.生产日期,
                        maniMan:item.加工人员,
                        bclass:item.所属班组,
                        workNo:item.作业单号,
                        wlNo:item.物料编号,
                        procedure:item.工序号,
                        proceName:item.工序名称,
                        doneNo:item.完工数量,
                        passNo:item.合格数量,
                        rePassNo:item.返工合格数量,
                        wHandle:item.待处理,
                        failNo:item.不良数量,
                        lf:item.料废,
                        jf:item.机废,
                        df:item.调废,
                        gf:item.工废
                    };
                    this.menus.push(stc);
                    c+=1;
                }
                console.log(this.menus)
                console.log("this is BGBH: "+ this.menus[0].bgNo);
                //加载表格1
                this.getTab1Data(this.menus[0].bgNo);
                this.getTab2Data(this.menus[0].bgNo);
                // alert(data)
                //加载小图标消失
                this.wIcon = false;
            }).catch(data=>{
                alert(data);
            })

           
            // //加载表格2
            // this.getTab2Data();
        })
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
.tip{
    margin:5px;
    /* border-bottom:5px double rgba(0,0,0,0.5); */
}
</style>