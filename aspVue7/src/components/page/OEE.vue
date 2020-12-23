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

            <el-dialog :visible.sync="showTab" title="日报工单总览" :key="timer" width="1550px">
               
                <!-- <h3>{{month}}月{{day}}日的{{type}}报工单总览</h3> -->
                <el-menu :default-active="activeIndex" class="el-menu-demo" mode="horizontal" @select="handleSelect" style="margin-top:-35px;background:#fff;" >
                    <el-menu-item v-for="item in menus" :key="item.label" :index="item.label" style="color:#000;font-size:18px" >{{item.timeCate}}:{{item.name}}</el-menu-item><!--#BFBFBF-->
                </el-menu>
                 <div v-if="wIcon">
                    <i class="el-icon-loading"></i>
                </div>
                <div class="dialogDisp" v-else>
                    <div class="basicInfo" style="color:#000"><!--background:#BFBFBF;border:1px solid #012468-->
                        <el-row>
                            <el-col :span="18">
                                <el-row>
                                    <el-col :span="6" ><div class="tip"> <p>报工编号：{{menus[disNo].bgNo}}</p></div> </el-col>
                                    <el-col :span="4"><div class="tip"><p>加工人：{{menus[disNo].maniMan}}</p></div></el-col>
                                    <el-col :span="9"><div class="tip"><p>物料名称：{{menus[disNo].name}}</p></div></el-col>
                                    <el-col :span="5"><div class="tip"><p>一次合格率(FTT)：{{menus[disNo].ftt}}%</p></div></el-col>
                                </el-row> 
                                <el-row>
                                    <el-col :span="6"><div class="tip"><p>生产日期：{{menus[disNo].birth}}</p></div></el-col>
                                    <el-col :span="4"><div class="tip"><p>所属班组：{{menus[disNo].bclass}}</p></div></el-col>
                                    <el-col :span="9"><div class="tip"><p>作业单号：{{menus[disNo].workNo}}</p></div></el-col>
                                    <el-col :span="5"><div class="tip"><p>设备开动率(A)：{{menus[disNo].a}}%</p></div></el-col>
                                </el-row>
                                <el-row>
                                    <el-col :span="6"><div class="tip"><p>物料号：{{menus[disNo].wlNo}}</p></div></el-col>
                                    <el-col :span="4"><div class="tip"><p>工序：{{menus[disNo].procedure}} {{menus[disNo].proceName}}</p></div></el-col>
                                    <el-col :span="9"><div class="tip"><p>待处理：{{menus[disNo].wHandle}}</p></div></el-col>
                                    <el-col :span="5"><div class="tip"><p>员工效率(P)：{{menus[disNo].p}}%</p></div></el-col>
                                </el-row>
                                <el-row>
                                    <el-col :span="6"><div class="tip"><p>完工数量：{{menus[disNo].doneNo}}</p></div></el-col>
                                    <el-col :span="4"><div class="tip"><p>合格数量：{{menus[disNo].passNo}}</p></div></el-col>
                                    <el-col :span="9"><div class="tip"><p>返工合格数量：{{menus[disNo].rePassNo}}</p></div></el-col>
                                    <el-col :span="5"><div class="tip"><p>合格率(Q)：{{menus[disNo].q}}%</p></div></el-col>
                                    
                                </el-row>
                                <el-row>
                                    <el-col :span="6"><div class="tip"><p>不合格数量：{{menus[disNo].failNo}}</p></div></el-col>
                                    <el-col :span="4"><div class="tip"><p>料废：{{menus[disNo].lf}}</p></div></el-col>
                                    <el-col :span="3"><div class="tip"><p>机废：{{menus[disNo].jf}}</p></div></el-col>
                                    <el-col :span="3"><div class="tip"><p>调废：{{menus[disNo].df}}</p></div></el-col>
                                    <el-col :span="3"><div class="tip"><p>工废：{{menus[disNo].gf}}</p></div></el-col>
                                    <el-col :span="5"><div class="tip"><p>OEE(A*P*Q)：{{menus[disNo].apq}}%</p></div></el-col>
                                </el-row>
                            </el-col>
                            <el-col :span="6">
                                <div id="pieChart" :style='{width:"100%",height:"150px"}'></div>
                                <i class="el-icon-info" style="-user-selection:none;cursor:pointer;"  @click="oeeFcn()"></i><!---->
                            </el-col>
                        </el-row>
                    </div>

                    <h3>不合格记录</h3>
                    <el-table :data="failedRec" border style="width: 100%;margin:1% 0% ;font-size:16px" height="240" highlight-current-row>
                        <el-table-column fixed prop="label" label="序号" width="100" > </el-table-column>
                        <el-table-column prop="txdate" label="填写日期" width="200"></el-table-column>
                        <el-table-column prop="pline" label="生产线" width="200"></el-table-column>
                        <el-table-column prop="sbNo" label="设备编号" width="150"> </el-table-column>
                        <el-table-column prop="failDesc" label="不合格描述" width="300"> </el-table-column>
                        <el-table-column prop="failNum" label="不良数量" width="120"> </el-table-column>
                        <el-table-column prop="shitNo" label="报废数" width="120"></el-table-column>
                        <el-table-column prop="rePassNo" label="返工合格数量" width="120"></el-table-column>
                        <el-table-column prop="writeMan" label="填写人" width="120"> </el-table-column>
                        <el-table-column prop="failNo" label="不合格单号" width="200"> </el-table-column>
                        <el-table-column prop="workNo" label="作业单号" width="120"> </el-table-column>
                        <el-table-column prop="wlNo" label="物料编号" width="120"> </el-table-column>
                        <el-table-column prop="wlDesc" label="物料描述" width="120"> </el-table-column>
                        <el-table-column prop="downNum" label="降级数" width="120"></el-table-column>
                        <el-table-column prop="sftNum" label="偏差数" width="120"></el-table-column>
                        <el-table-column prop="passNum" label="合格数" width="120"> </el-table-column>
                        <el-table-column prop="status" label="状态" width="120"> </el-table-column>
                        <el-table-column prop="bgNo" label="报工编号" width="200"> </el-table-column>
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
                    <el-table :data="stopRec" border style="width: 100%;margin:1% 0%;font-size:16px" height="240" highlight-current-row>
                        <el-table-column fixed prop="stopType" label="停机类型" width="100"> </el-table-column>
                        <el-table-column prop="sbNo" label="设备编号" width="130"></el-table-column>
                        <el-table-column prop="stopDesc" label="停机描述" width="300"></el-table-column>
                        <el-table-column prop="stopBegin" label="停机开始" width="300"> </el-table-column>
                        <el-table-column prop="stopEnd" label="停机结束" width="300"> </el-table-column>
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
        <el-dialog title="OEE公式" :visible.sync="oeeFcnVis" width="1300px" >
            <div class="fcnRow">
                <div class="subtitle">操作时间</div>
                <div class="fcnPrm">
                    <p>作业完工时间</p>
                    <p>(({{menus[disNo].workDtime}} -</p>
                </div>
                <div class="fcnPrm">
                    <p>作业开始时间</p>
                    <p>{{menus[disNo].workStime}})</p>
                </div>
                <div class="fcnPrm">
                    <p>结果1</p>
                    <p>= {{TimeDifference(menus[disNo].workStime,menus[disNo].workDtime)}})</p>
                </div>
                <div class="fcnPrm">
                    <p>计划停机时间</p>
                    <p>- {{menus[disNo].planStime}}</p>
                </div>
                <div class="fcnPrm">
                    <p>非计划停机时间</p>
                    <p>- {{menus[disNo].unplanStime}}</p>
                </div>
                <div class="fcnPrm">
                    <p>结果2</p>
                    <p>= {{TimeDifference(menus[disNo].workStime,menus[disNo].workDtime)-menus[disNo].planStime-menus[disNo].unplanStime}}</p>
                </div>
            </div>
            <div class="fcnRow">
                <div class="subtitle">计划工作时间</div>
                <div class="fcnPrm">
                    <p>作业完工时间</p>
                    <p>({{menus[disNo].workDtime}} - </p>
                </div>
                <div class="fcnPrm">
                    <p>调试开始时间</p>
                    <p>{{menus[disNo].testStime}})</p>
                </div>
                <div class="fcnPrm">
                    <p>结果1</p>
                    <p> = {{TimeDifference(menus[disNo].testStime,menus[disNo].workDtime)}}</p>
                </div>
                <div class="fcnPrm">
                    <p>计划停机时间</p>
                    <p>- {{menus[disNo].planStime}}</p>
                </div>
                
                <div class="fcnPrm">
                    <p>结果2</p>
                    <p> = {{TimeDifference(menus[disNo].testStime,menus[disNo].workDtime)-menus[disNo].planStime}}</p></div>
            </div>
            <div class="fcnRow">
                <div class="subtitle"> 设备开动率(A)</div>
                <div class="fcnPrm">
                    <p>操作时间</p>
                    <p>{{TimeDifference(menus[disNo].workStime,menus[disNo].workDtime)-menus[disNo].planStime-menus[disNo].unplanStime}} / </p>
                </div>
                <div class="fcnPrm">
                    <p>计划工作时间</p>
                    <p>{{TimeDifference(menus[disNo].testStime,menus[disNo].workDtime)-menus[disNo].planStime}}</p>
                </div>
                <div class="fcnPrm"> = {{((TimeDifference(menus[disNo].workStime,menus[disNo].workDtime)-menus[disNo].planStime-menus[disNo].unplanStime)*100/(TimeDifference(menus[disNo].testStime,menus[disNo].workDtime)-menus[disNo].planStime)).toFixed(2)}}%</div>
            </div>
            <div class="fcnRow">
                <div class="subtitle">员工效率(P)</div>
                <div class="fcnPrm">
                    <p>完工数量</p>
                    <p>({{menus[disNo].doneNo}} /</p>
                </div>
                <div class="fcnPrm">
                    <p>操作时间</p>
                    <p>(({{TimeDifference(menus[disNo].workStime,menus[disNo].workDtime)-menus[disNo].planStime-menus[disNo].unplanStime}}/60) *</p></div>
                <div class="fcnPrm">
                    <p>定额数量</p>
                    <p>{{menus[disNo].fixNum}})</p>
                </div>
                 <div class="fcnPrm">
                     <p>结果</p>
                     <p> = {{(menus[disNo].doneNo*100/((TimeDifference(menus[disNo].workStime,menus[disNo].workDtime)-menus[disNo].planStime-menus[disNo].unplanStime)*menus[disNo].fixNum/60)).toFixed(2)}}%</p></div>
                <div class="fcnPrm">备注</div>
                <div class="fcnPrm">
                    <p>定额人数:{{menus[disNo].fixMan}}</p>
                    <!-- <el-divider></el-divider> -->
                    <p>生产人数:{{menus[disNo].prodMan}}</p>
                </div>
                <div class="fcnPrm">
                </div>
            </div>
             <div class="fcnRow">
                <div class="subtitle">合格率(Q)</div>
                <div class="fcnPrm">
                    <p>不良数量</p>
                    <p>100% - ({{menus[disNo].failNo}} / </p>
                </div>
                <div class="fcnPrm">
                    <p>完工数量</p>
                    <p>{{menus[disNo].doneNo}})</p>
                </div>
                
                <div class="fcnPrm">
                    <p>结果</p>
                    <p> = {{((menus[disNo].doneNo-menus[disNo].failNo)*100/menus[disNo].doneNo).toFixed(2)}}%</p></div>
            </div>
            <div class="fcnRow">
                <div class="subtitle">OEE(A*P*Q)</div>
                <div class="fcnPrm">A:{{((TimeDifference(menus[disNo].workStime,menus[disNo].workDtime)-menus[disNo].planStime-menus[disNo].unplanStime)*100/(TimeDifference(menus[disNo].testStime,menus[disNo].workDtime)-menus[disNo].planStime)).toFixed(2)}}%</div>
                <div class="fcnPrm"> * P:{{(menus[disNo].doneNo*100/((TimeDifference(menus[disNo].workStime,menus[disNo].workDtime)-menus[disNo].planStime-menus[disNo].unplanStime)*menus[disNo].fixNum/60)).toFixed(2)}}%</div>
                <div class="fcnPrm"> * Q:{{((menus[disNo].doneNo-menus[disNo].failNo)*100/menus[disNo].doneNo).toFixed(2)}}</div>
                <div class="fcnPrm"> = {{(((TimeDifference(menus[disNo].workStime,menus[disNo].workDtime)-menus[disNo].planStime-menus[disNo].unplanStime)/(TimeDifference(menus[disNo].testStime,menus[disNo].workDtime)-menus[disNo].planStime))*(menus[disNo].doneNo/((TimeDifference(menus[disNo].workStime,menus[disNo].workDtime)-menus[disNo].planStime-menus[disNo].unplanStime)*menus[disNo].fixNum/60))*((menus[disNo].doneNo-menus[disNo].failNo)/menus[disNo].doneNo)).toFixed(4)*100}}%</div>
            </div>
        </el-dialog>
    </div>
</template>

<script>
// import Schart from 'vue-schart';
import OEEchart from './OEEcharts/OEEchart.vue';
import EFFchart from './OEEcharts/EFFchart.vue';
import FTTchart from './OEEcharts/FTTchart.vue';
import TJLchart from './OEEcharts/TJLchart.vue';
import echarts from 'echarts';
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
            menus:[{
                    label:null,
                    txdate:null,
                    pline:null,
                    sbNo:null,
                    failDesc:null,
                    failNum:null,
                    shitNo:null,
                    rePassNo:null,
                    writeMan:null,
                    failNo:null,
                    workNo:null,
                    wlNo:null,
                    wlDesc:null,
                    downNum:null,
                    sftNum:null,
                    passNum:null,
                    status:null,
                    bgNo:null,
                    ftt:null,
                    a:null,
                    p:null,
                    q:null,
                    apq:0,
                    //公式数据
                    workDtime:'',
                    workStime:'',
                    planStime:'',
                    unplanStime:'',
                    testStime:'',
                    fixNum:0,
                    fixMan:0,
                    prodMan:0
                    }],
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
                      ],//{indexId:9,name:"EGR_Group"}
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
            currDate:null,
            //实时刷新我们的模态框
            timer:null,
            //饼状图参数
            piec:null,
            pieOptions:{
				tooltip: {
                    formatter: '{a}  {b} : {c}',
                },
				series: [{
                    name:'oee',
                    type: 'pie',
                    radius:'80%',
                    data:[{value:0,itemStyle:{ color:'#00FF00'}},{value:100,itemStyle:{ color:'#696969'}}],
                    animation:true,
                    label:{
                        show:false,
                        position:'outer',
                        alignTo:'none',
                        bleedMargin:5
                    }
				}]
            },
            oeeFcnVis:false,
        }
    },
    methods: {
        //工具方程,计算日期相差分钟数
        TimeDifference(start,end)
        {
            //判断开始时间是否大于结束日期
            if(start>end)
            {
                this.$message.error("开始时间不能大于结束时间！");
                return false;
            }
            var begin1=start.substr(0,10).split("-");
            var end1=end.substr(0,10).split("-");
            //将拆分的数组重新组合，并实例成化新的日期对象
            var date1=new Date(begin1[1] + - + begin1[2] + - + begin1[0]);
            var date2=new Date(end1[1] + - + end1[2] + - + end1[0]);
            //得到两个日期之间的差值m，以分钟为单位
            //Math.abs(date2-date1)计算出以毫秒为单位的差值
            //Math.abs(date2-date1)/1000得到以秒为单位的差值
            //Math.abs(date2-date1)/1000/60得到以分钟为单位的差值
            var m=parseInt(Math.abs(date2-date1)/1000/60);
            //小时数和分钟数相加得到总的分钟数
            //time1.substr(11,2)截取字符串得到时间的小时数
            //parseInt(time1.substr(11,2))*60把小时数转化成为分钟
            var min1=parseInt(start.substr(11,2))*60+parseInt(start.substr(14,2));
            var min2=parseInt(end.substr(11,2))*60+parseInt(end.substr(14,2));
            //两个分钟数相减得到时间部分的差值，以分钟为单位
            var n=min2-min1;
            //将日期和时间两个部分计算出来的差值相加，即得到两个时间相减后的分钟数
            var minutes=m+n;
            console.log(min1)
            console.log(min2)
            // console.log()
            return minutes;
        },
        oeeFcn(){
            this.oeeFcnVis = true;
            this.TimeDifference(this.menus[this.disNo].workStime,this.menus[this.disNo].workDtime)
        },
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
             console.log("fff")
            this.piec = echarts.init(document.getElementById("pieChart"));
            this.pieOptions.series[0].data = [];
            this.pieOptions.series[0].data.push({value:this.menus[this.disNo].apq,itemStyle:{color:'#00FF00'}});
            this.pieOptions.series[0].data.push({value:100-this.menus[this.disNo].apq,itemStyle:{color:'#696969'}});

            this.piec.setOption(this.pieOptions);
            this.getTab1Data(this.menus[this.disNo].bgNo)
            this.getTab2Data(this.menus[this.disNo].bgNo)
        },
        init_pie(){
            this.piec = echarts.init(document.getElementById("pieChart"));
            this.pieOptions.series[0].data = [];
            this.pieOptions.series[0].data.push({value:this.menus[this.disNo].apq,itemStyle:{color:'#00FF00'}});
            this.pieOptions.series[0].data.push({value:100-this.menus[this.disNo].apq,itemStyle:{color:'#696969'}});
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
                        failNum:item.不良数量,
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
                       const nowtime = new Date();
                    //    console.log("日期"+nowtime.getDate())
                    //    console.log("月"+nowtime.getMonth())
                    //    console.log("日"+nowtime.getDay())
                    //    console.log("时"+nowtime.getHours())
                    //    console.log(this.value1[1].getDate() < nowtime.getDate());
                    //    console.log(this.value1[1].getMonth() < nowtime.getMonth());
                    //    console.log(this.value1[1].getDate() > nowtime.getDate());
                    //    console.log(nowtime.getHours() == 8);

                    //    console.log(nowtime.getDay())
                    //    if(nowtime.getDay()=='13'){
                    //        console.log("damnyes");
                    //    }
                       if((this.value1[1].getDate() < nowtime.getDate() || (this.value1[1].getMonth() < nowtime.getMonth() && this.value1[1].getDate() > nowtime.getDate())) && nowtime.getHours() == 8){
                            // console.log("oldtme"+this.value1[0])
                            // console.log("oldtme"+this.value1[1])
                            this.value1[0]=new Date((this.value1[0]).getTime()+3600*24*1000)
                            this.value1[1]=new Date((this.value1[1]).getTime()+3600*24*1000)
                            // console.log("newtme"+this.value1[0])
                            // console.log("newtme"+this.value1[1])
                            console.log("更新日期需要")
                       }
                   },3600000);//86400000 
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
        bus.$emit("Query",{dateunit:"日",prodline:"GEN_III_A+M",starttime:oldtime,endtime:newtime});
       
        bus.$on("pillInfo",msg=>{
            if(this.status && this.pauseS){
                return;
            }
            this.wIcon = true;
            this.disNo = 0;
            this.activeIndex = "0"
            this.timer=new Date().getTime();
            this.showTab = true;
            // this.piec = echarts.init(document.getElementById("pieChart"));
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
                if(data.length==0){console.log();
                    return;
                }
                var c = 0
                this.menus = []
                 var doneTime = "";
                for(var item of data){
                    console.log(parseInt((item.调试开始时间).substr(11,2)));
                    if(parseInt((item.调试开始时间).substr(11,2))>=16){
                        doneTime = "中班"
                    }else if(parseInt((item.调试开始时间).substr(11,2))>=8){
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
                        gf:item.工废,
                        ftt:(item.一次合格率*100).toFixed(2),
                        a:(item.设备开动率*100).toFixed(2),
                        p:(item.员工效率*100).toFixed(2),
                        q:(item.合格率*100).toFixed(2),
                        apq:(item.设备开动率*item.员工效率*item.合格率*100).toFixed(2),
                        //公式数据
                        workDtime:item.作业完工时间,
                        workStime:item.作业开始时间,
                        planStime:item.计划停机时间,
                        unplanStime:item.非计划停机时间,
                        testStime:item.调试开始时间,
                        fixNum:item.定额件数,
                        fixMan:item.定额人数,
                        prodMan:item.生产人数
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
    font-size:18px;
    color:rgba(0,0,0,0.7)
    /* border-bottom:5px double rgba(0,0,0,0.5); */
}
/*修改导航栏菜单激活时的背景色*/ 
.el-menu-item.is-active {
    color:#fff !important;
    background-color:#012468 !important;
}
/*修改表格选中某一行后的背景色 */
.el-table__body tr.current-row>td{
  background-color:#000 !important;
  color: #000;
}
.subtitle{
    display:inline-block;
    font-size:40px;
    margin:25px 5px;
}
.fcnPrm{
    display:inline-block;
    font-size:25px;
    margin:2px 5px;
}
.fcnRow{
    height:100px;
    border-bottom: 2px solid rgba(0,0,0,0.4);
}

</style>