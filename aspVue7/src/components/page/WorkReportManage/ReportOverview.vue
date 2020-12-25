<template>
    <div>
        <el-card>
            <el-select v-model="group" @change="lineChge($event)">
                <el-option v-for="item in pGroup" :key="item" :value="item">{{item}}</el-option>
            </el-select>
            <el-select v-model="line">
                <el-option v-for="item in pLine" :key="item" :value="item">{{item}}</el-option>
            </el-select>
            <!-- <el-date-picker v-model="" type="date" placeholder="报工日期"></el-date-picker> -->
            <el-date-picker v-model="prodDate" value-format='yyyy-MM-dd HH:mm:ss' type="daterange" align="right"  unlink-panels  range-separator="至" start-placeholder="开始日期" end-placeholder="结束日期" :picker-options="pickerOptions" style="height:33px;padding-top:2px"></el-date-picker>

            <el-button type="primary" @click="query()">查询</el-button>
            <el-button type="primary" @click="expTab()">导出当前工单表</el-button>
        </el-card>
        <el-card style="height:900px">
            <el-table :data="bgTab" style="width: 100%;font-size:20px" border fit height="800px">
                <el-table-column prop="bgNo" label="报工编号" width="220" align=center ></el-table-column>
                <el-table-column prop="prodDate" label="生产日期" width="220" align=center></el-table-column>
                <el-table-column prop="clsDate" label="班次日期" width="220" align=center></el-table-column>
                <el-table-column prop="workUnit" label="加工单元" width="180" align=center></el-table-column>
                <el-table-column prop="cls" label="所属班组" width="160" align=center></el-table-column>
                <el-table-column prop="workMan" label="加工人员" width="160" align=center></el-table-column>
                <el-table-column prop="workNo" label="作业单号" width="200" align=center></el-table-column>
                <el-table-column prop="wlNo" label="物料编号" width="200" align=center></el-table-column>
                <el-table-column prop="wlDesc" label="物料描述" width="280" align=center></el-table-column>
                <el-table-column prop="proceNo" label="工序号" width="100" align=center></el-table-column>
                <el-table-column prop="proceName" label="工序名称" width="160" align=center></el-table-column>
                <el-table-column prop="fixNum" label="定额件数" width="160" align=center></el-table-column>
                <el-table-column prop="testStime" label="调试开始时间" width="220" align=center></el-table-column>
                <el-table-column prop="testTime" label="调试时间" width="160" align=center></el-table-column>
                <el-table-column prop="workStime" label="作业开始时间" width="220" align=center></el-table-column>
                <el-table-column prop="workEtime" label="作业完工时间" width="220" align=center></el-table-column>
                <el-table-column prop="planStime" label="计划停机时间" width="220" align=center></el-table-column>
                <el-table-column prop="unplanStime" label="非计划停机时间" width="250" align=center></el-table-column>
                <el-table-column prop="doneNum" label="完工数量" width="160" align=center></el-table-column>
                <el-table-column prop="passNum" label="合格数量" width="160" align=center></el-table-column>
                <el-table-column prop="failNum" label="不良数量" width="160" align=center></el-table-column>
                <el-table-column prop="lf" label="料废" align=center></el-table-column>
                <el-table-column prop="jf" label="机废" align=center></el-table-column>
                <el-table-column prop="df" label="调废" align=center></el-table-column>
                <el-table-column prop="gf" label="工废" align=center></el-table-column>
                <el-table-column prop="dcl" label="待处理" width="100" align=center></el-table-column>
                <el-table-column prop="thryTime" label="理论时间" width="160" align=center></el-table-column>
                <el-table-column prop="pWorkTime" label="计划工作时间" width="220" align=center></el-table-column>
                <el-table-column prop="opsTime" label="操作时间" width="160" align=center></el-table-column>
                <el-table-column prop="fixProdNum" label="定额产量" width="160" align=center></el-table-column>
                <el-table-column prop="passRate" label="合格率" width="160" align=center></el-table-column>
                <el-table-column prop="manEff" label="员工效率" width="160" align=center></el-table-column>
                <el-table-column prop="eqpEff" label="设备开动率" width="150" align=center></el-table-column>
                <el-table-column prop="prodMan" label="生产人数" width="160" align=center></el-table-column>
                <el-table-column prop="fixMan" label="定额人数" width="160" align=center></el-table-column>
                <el-table-column prop="oee" label="OEE" align=center></el-table-column>
                <el-table-column prop="cgroup" label="工作组" width="150" align=center></el-table-column>
                <el-table-column prop="status" label="状态" align=center></el-table-column>
                <el-table-column prop="bgCate" label="报工类别" width="160" align=center></el-table-column>
                <el-table-column prop="testEtime" label="调试结束时间" width="220" align=center></el-table-column>
                <el-table-column prop="test2done" label="调试到完工" width="150" align=center></el-table-column>
                <el-table-column prop="work2done" label="作业到完工" width="150" align=center></el-table-column>
                <el-table-column prop="ordType" label="订单类型" width="160" align=center></el-table-column>
                <el-table-column prop="repassNum" label="返工合格数量" width="150" align=center></el-table-column>
                <el-table-column prop="ftt" label="一次合格率" width="150" align=center></el-table-column>
                <el-table-column prop="tjl" label="停机率" width="160" align=center></el-table-column>
                <el-table-column fixed="right" label="操作" width="220px" >
                    <template slot-scope="scope">
                        <el-button @click="bgEdit(scope.row)" style="background:#F5F5F5;" >编辑</el-button>
                        <el-button @click="bgView(scope.row)" style="background:#F5F5F5;" >总览</el-button>
                        <el-button style="background:#F5F5F5;" @click="bgDel(scope.row)">删除</el-button>
                    </template>
                </el-table-column>
            </el-table>
        </el-card>
        <el-dialog title="编辑" :visible.sync="editVis" :before-close="editClose" width="1400px">
            <el-form  ref="editForm" :model="editForm" :rules="editRule" label-width="120px">
                <el-row>
                    <el-col :span="6">
                        <el-form-item label="报工编号" prop="bgNo">
                            <el-input disabled v-model="editForm.bgNo"></el-input>
                        </el-form-item>
                    </el-col>
                    <el-col :span="6">
                       <el-form-item label="生产日期" prop="prodDate">
                            <el-date-picker v-model="editForm.prodDate" value-format="yyyy-MM-dd" type="datetime"></el-date-picker>
                        </el-form-item>
                    </el-col>
                    <el-col :span="6">
                        <el-form-item label="班次日期" prop="clsDate">
                            <el-date-picker type="datetime" v-model="editForm.clsDate" value-format="yyyy-MM-dd"></el-date-picker>
                        </el-form-item>
                    </el-col>
                    <el-col :span="6">
                         <el-form-item label="所属班组" prop="cls">
                            <el-input v-model="editForm.cls"></el-input>
                        </el-form-item>
                    </el-col>
                </el-row>
                <el-row>
                    <el-col :span="6">
                       <el-form-item label="加工人员" prop="workMan">
                            <el-input v-model="editForm.workMan"></el-input>
                        </el-form-item>
                    </el-col>
                    <el-col :span="6">
                         <el-form-item label="生产人数" prop="prodMan">
                            <el-input  v-model.number="editForm.prodMan"></el-input>
                        </el-form-item>
                        
                    </el-col>
                    <el-col :span="6">
                        <el-form-item label="调试开始时间" prop="testStime">
                            <el-date-picker v-model="editForm.testStime" type="datetime" value-format="yyyy-MM-ddTHH:mm:ss"></el-date-picker>
                        </el-form-item>
                    </el-col>
                    <el-col :span="6">
                        <el-form-item label="调试结束时间" prop="testEtime">
                            <el-date-picker type="datetime" v-model="editForm.testEtime" value-format="yyyy-MM-ddTHH:mm:ss"></el-date-picker>
                        </el-form-item>
                    </el-col>
                </el-row>
                <el-row>
                    <el-col :span="6">
                        <el-form-item label="作业开始时间" prop="workStime">
                            <el-date-picker type="datetime" v-model="editForm.workStime" value-format="yyyy-MM-ddTHH:mm:ss"></el-date-picker>
                        </el-form-item>
                        
                    </el-col>
                    <el-col :span="6">
                        <el-form-item label="作业完工时间" prop="workEtime">
                            <el-date-picker v-model="editForm.workEtime" type="datetime" value-format="yyyy-MM-ddTHH:mm:ss"></el-date-picker>
                        </el-form-item>
                        
                    </el-col>
                    <el-col :span="6">
                        <el-form-item label="计划停机时间" prop="planStime">
                            <el-input v-model.number="editForm.planStime"></el-input>
                        </el-form-item>
                        
                    </el-col>
                    <el-col :span="6">
                        <el-form-item label="非计划停机时间" prop="unplanStime">
                            <el-input  v-model.number="editForm.unplanStime"></el-input>
                        </el-form-item>
                        
                    </el-col>
                </el-row>
                <el-row>
                    <el-col :span="6">
                        <el-form-item label="完工数量" prop="doneNum">
                            <el-input v-model="editForm.doneNum"></el-input>
                        </el-form-item>
                        
                    </el-col>
                    <el-col :span="6">
                        <el-form-item label="合格数量" prop="passNum">
                            <el-input v-model.number="editForm.passNum"></el-input>
                        </el-form-item>
                        
                    </el-col>
                    <el-col :span="6">
                        <el-form-item label="返工合格数量" prop="repassNum">
                            <el-input v-model.number="editForm.repassNum"></el-input>
                        </el-form-item>
                        
                    </el-col>
                    <el-col :span="6">
                        <el-form-item label="不良数量" prop="failNum">
                            <el-input  v-model.number="editForm.failNum"></el-input>
                        </el-form-item>
                        
                    </el-col>
                </el-row>
                <el-row>
                    <el-col :span="6">
                        <el-form-item label="料废" prop="lf">
                            <el-input v-model.number="editForm.lf"></el-input>
                        </el-form-item>
                        
                    </el-col>
                    <el-col :span="6">
                        <el-form-item label="机废" prop="jf">
                            <el-input  v-model.number="editForm.jf"></el-input>
                        </el-form-item>
                        
                    </el-col>
                    <el-col :span="6">
                        <el-form-item label="调废" prop="df">
                            <el-input v-model.number="editForm.df"></el-input>
                        </el-form-item>
                        
                    </el-col>
                    <el-col :span="6">
                        <el-form-item label="工废" prop="gf">
                            <el-input v-model.number="editForm.gf"></el-input>
                        </el-form-item>
                    </el-col>
                </el-row>
                <el-row>
                    <el-col :span="6">
                       <el-form-item label="待处理" prop="dcl">
                            <el-input v-model.number="editForm.dcl"></el-input>
                        </el-form-item>
                    </el-col>
                </el-row>
            </el-form>
            <el-button type="primary" @click="editCfm()">确认</el-button>
            <el-button type="primary" @click="editCancel()">取消</el-button>
        </el-dialog>
        <el-dialog title="删除提示" :visible.sync="bgDelVis" :before-close="delClose">
            <div> 确定删除工单 <strong>{{delBGD}}</strong> 吗？</div>
            <el-button type="primary" @click="delCfm()">确定</el-button>
            <el-button @click="delCancel()">取消</el-button>
        </el-dialog>
        <!--日报工单总览-->
         <el-dialog :visible.sync="showTab" title="日报工单总览" :key="timer" width="1550px" :before-close="gdzlClose">
                <div class="dialogDisp">
                    <div class="basicInfo" style="color:#000"><!--background:#BFBFBF;border:1px solid #012468-->
                        <el-row>
                            <el-col :span="18">
                                <el-row>
                                    <el-col :span="6" ><div class="tip"> <p>报工编号：{{bgNo}}</p></div> </el-col>
                                    <el-col :span="4"><div class="tip"><p>加工人：{{maniMan}}</p></div></el-col>
                                    <el-col :span="9"><div class="tip"><p>物料名称：{{name}}</p></div></el-col>
                                    <el-col :span="5"><div class="tip"><p>一次合格率(FTT)：{{ftt}}%</p></div></el-col>
                                </el-row> 
                                <el-row>
                                    <el-col :span="6"><div class="tip"><p>生产日期：{{birth}}</p></div></el-col>
                                    <el-col :span="4"><div class="tip"><p>所属班组：{{bclass}}</p></div></el-col>
                                    <el-col :span="9"><div class="tip"><p>作业单号：{{workNo}}</p></div></el-col>
                                    <el-col :span="5"><div class="tip"><p>设备开动率(A)：{{a}}%</p></div></el-col>
                                </el-row>
                                <el-row>
                                    <el-col :span="6"><div class="tip"><p>物料号：{{wlNo}}</p></div></el-col>
                                    <el-col :span="4"><div class="tip"><p>工序：{{procedure}} {{proceName}}</p></div></el-col>
                                    <el-col :span="9"><div class="tip"><p>待处理：{{wHandle}}</p></div></el-col>
                                    <el-col :span="5"><div class="tip"><p>员工效率(P)：{{p}}%</p></div></el-col>
                                </el-row>
                                <el-row>
                                    <el-col :span="6"><div class="tip"><p>完工数量：{{doneNo}}</p></div></el-col>
                                    <el-col :span="4"><div class="tip"><p>合格数量：{{passNo}}</p></div></el-col>
                                    <el-col :span="9"><div class="tip"><p>返工合格数量：{{rePassNo}}</p></div></el-col>
                                    <el-col :span="5"><div class="tip"><p>合格率(Q)：{{q}}%</p></div></el-col>
                                    
                                </el-row>
                                <el-row>
                                    <el-col :span="6"><div class="tip"><p>不合格数量：{{failNo}}</p></div></el-col>
                                    <el-col :span="4"><div class="tip"><p>料废：{{lf}}</p></div></el-col>
                                    <el-col :span="3"><div class="tip"><p>机废：{{jf}}</p></div></el-col>
                                    <el-col :span="3"><div class="tip"><p>调废：{{df}}</p></div></el-col>
                                    <el-col :span="3"><div class="tip"><p>工废：{{gf}}</p></div></el-col>
                                    <el-col :span="5"><div class="tip"><p>OEE(A*P*Q)：{{apq}}%</p></div></el-col>
                                </el-row>
                            </el-col>
                            <!-- <el-col :span="6">
                                <div id="pieChart" :style='{width:"100%",height:"150px"}'></div>
                                <i class="el-icon-info" style="-user-selection:none;cursor:pointer;"  @click="oeeFcn()"></i>
                            </el-col> -->
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
                    </el-table>
                </div>
            </el-dialog>
    </div>
</template>

<script>
import XLSX from 'xlsx'
export default {
    data(){
        return{
            bgDelVis:false,//删除订单弹窗控制器
            delBGD:'',//删除的报工单编号
            editVis:false,
            prodDate:'',
            bgTab:[],
            //选择产线的控制器
            group:'EGR阀装配线',
            pGroup:['EGR阀装配线','Cooler/Module线','电子线'],

            //当前订单参数
            line:'GDI阀装配线',
            pLine:["GEN_III_A+M",
                      "GEN_III_V+F",
                      "GEN_III_BPV",
                      "GDI阀装配线",
                      "LP阀装配线",
                      "柔性线",
                      "ETV装配线",
                      "GEN_II阀装配线",
                      ],
            EGRLine:["GEN_III_A+M",
                      "GEN_III_V+F",
                      "GEN_III_BPV",
                      "GDI阀装配线",
                      "LP阀装配线",
                      "柔性线",
                      "ETV装配线",
                      "GEN_II阀装配线",
                      ],//"EGR_Group"
            CMLine:["混合管装配线1",
                      "混合管装配线2",
                      "Cooler Line 1",
                      "Cooler Line 2",
                      "Cooler Line 3",
                      "Cooler Line 4",
                      "钎焊炉1",
                      "钎焊炉2",
                      "测试台1",
                      "测试台2",
                      "测试台3",
                      "测试台4",
                      "模块线1"],
            ELELine:["HV Air PTC Line",
                      "LV Air PTC Line",
                      "Liquid Heater Line",
                      "eCRV Line",
                      "Marsilli 1",
                      "Marsilli 2",
                      "IPTE 1",
                      "IPTE 2",
                      "Huebers 1",
                      "Huebers 2",
                      "Ford节温器线",
                      "HKMC节温器线",
                      "GAC节温器线",
                      "JLR节温器线",
                      "蜡包装配线",
                      "SGMM节温器线",
                      "SGME节温器线"],
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
            editForm:{
                bgNo:'',
                prodDate:'',
                clsDate:'',
                cls:'',
                workMan:'',
                prodMan:null,
                testStime:'',
                testEtime:'',
                workStime:'',
                workEtime:'',
                planStime:null,
                unplanStime:null,
                doneNum:null,
                passNum:null,
                repassNum:null,
                failNum:null,
                lf:null,
                jf:null,
                df:null,
                gf:null,
                dcl:null,
            },
            editRule:{
                // workShop:[{required:true,message:'车间不能为空',trigger:'blur'}],
                // workUnit:[{required:true,message:'加工单元是哪个',trigger:'blur'}],
                workMan:[{required:true,message:'加工人员是谁',trigger:'blur'}],
                cls:[{required:true,message:'班组是哪个',trigger:'blur'}],
                prodDate:[{required:true,message:'生产日期日期不能为空',trigger:'blur'}],
                testStime:[{required:true,message:'调试时间不能为空',trigger:'blur'}],
                testTime:[{required:true,type:'number',message:'调试累计时间不能为空',trigger:'blur'}],
                testEtime:[{required:true,message:'调试结束时间不能为空',trigger:'blur'}],
                workStime:[{required:true,message:'作业开始时间不能为空',trigger:'blur'}],
                workEtime:[{required:true,message:'作业完工不能为空',trigger:'blur'}],
                planStime:[{required:true,type:'number',message:'计划停机时间不能为空',trigger:'blur'}],
                unplanStime:[{required:true,type:'number',message:'非计划停机时间不能为空',trigger:'blur'}],
                doneNum:[{required:true,type:'number',message:'完工数量不能为空',trigger:'blur'}],
                passNum:[{required:true,type:'number',message:'合格数量不能为空',trigger:'blur'}],
                lf:[{required:true,type:'number',message:'料废不能为空',trigger:'blur'}],
                jf:[{required:true,type:'number',message:'机废不能为空',trigger:'blur'}],
                df:[{required:true,type:'number',message:'调废不能为空',trigger:'blur'}],
                gf:[{required:true,type:'number',message:'工废不能为空',trigger:'blur'}],
                dcl:[{required:true,type:'number',message:'待处理苏良不能为空',trigger:'blur'}],
                failNum:[{required:true,type:'number',message:'不良不能为空',trigger:'blur'}],
                repassNum:[{required:true,type:'number',message:'反攻合格数量不能为空',trigger:'blur'}],
                clsDate:[{required:true,message:'班次日期不能为空',trigger:'blur'}],
                prodMan:[{required:true,message:'生产人数不能为空！',trigger:'blur'}]
            },
            //工单总览弹窗控制器
            showTab:false,
            //工单纵览弹窗参数
            bgNo:'',
            maniMan:'',
            name:'',
            ftt:null,
            birth:'',
            bclass:'',
            workNo:'',
            a:null,
            wlNo:'',
            procedure:null,
            proceName:'',
            wHandle:null,
            p:null,
            doneNo:null,
            passNo:null,
            rePassNo:null,
            q:null,
            failNo:null,
            lf:null,
            jf:null,
            gf:null,
            df:null,
            apq:null,
            //不合格记录表格
            failedRec:[],
            //停机记录
            stopRec:[],



        }
    },
    methods:{
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
            var dateTime = year + "_" + month + "_" + day; 
            return dateTime;  
        },
        query(){
            if(this.group==''){
                this.$message.error("请选择产线")
                return false;
            }
            if(this.line==''){
                this.$message.error("请选择加工单元")
                return false;
            }
            if(this.prodDate==null){
                this.prodDate=[new Date(),new Date()]
            }
            fetch('api/WorkReport/BGDView',{
                method:'POST',
                headers:{
                    'Content-Type':'application/json'
                },
                body:JSON.stringify({
                    line:this.line,
                    pdateS:this.prodDate[0],
                    pdateE:this.prodDate[1]
                })
            }).then(response=>response.json())
            .then(data=>{
                if(data.length != 0){
                    this.bgTab = []
                    for(var item of data){
                        var tmp={
                            bgNo:item.报工编号,
                            prodDate:item.生产日期,
                            clsDate:item.班次日期,
                            workUnit:item.加工单元,
                            cls:item.所属班组,
                            workMan:item.加工人员,
                            workNo:item.作业单号,
                            wlNo:item.物料编号,
                            wlDesc:item.物料描述,
                            proceNo:item.工序号,
                            proceName:item.工序名称,
                            fixNum:item.定额件数,
                            testStime:item.调试开始时间,
                            testTime:(item.调试时间).toFixed(2),
                            workStime:item.作业开始时间,
                            workEtime:item.作业完工时间,
                            planStime:item.计划停机时间,
                            unplanStime:item.非计划停机时间,
                            doneNum:item.完工数量,
                            passNum:item.合格数量,
                            failNum:item.不良数量,
                            lf:item.料废,
                            jf:item.机废,
                            df:item.调废,
                            gf:item.工废,
                            dcl:item.待处理,
                            thryTime:(item.理论时间).toFixed(2),
                            pWorkTime:(item.计划工作时间).toFixed(2),
                            opsTime:(item.操作时间).toFixed(2),
                            fixProdNum:(item.定额产量).toFixed(2),
                            passRate:(item.合格率*100).toFixed(2),
                            manEff:(item.员工效率*100).toFixed(2),
                            eqpEff:(item.设备开动率*100).toFixed(2),
                            prodMan:item.生产人数,
                            fixMan:item.定额人数,
                            oee:item.oee?1:0,
                            cgroup:item.工作组,
                            status:item.状态?1:0,
                            bgCate:item.报工类别,
                            testEtime:item.调试结束时间,
                            test2done:item.调试到完工,
                            work2done:item.作业到完工,
                            ordType:item.订单类型,
                            repassNum:item.返工合格数量,
                            ftt:(item.一次合格率*100).toFixed(2),
                            tjl:(item.停机率*100).toFixed(2)
                        }
                        this.bgTab.push(tmp)
                    }
                }else{
                    this.$message.warning("当前筛选条件下无数据");
                    this.bgTab=[]
                }
            }).catch(data=>{
                alert(data)
            })
        },
        lineChge(event){
            this.line=''
            if(event=="EGR阀装配线"){
                this.pLine=this.EGRLine;
            }else if(event=="Cooler/Module线"){
                this.pLine=this.CMLine;
            }else if(event=="电子线"){
                this.pLine=this.ELELine;
            }
        },
        bgEdit(row){
             if(localStorage.getItem("ms_username") == "Sean" || localStorage.getItem("ms_username") == "Chuck Yu"){
                this.editForm.bgNo=row.bgNo
                this.editForm.prodDate=row.prodDate
                this.editForm.clsDate=row.clsDate
                this.editForm.cls=row.cls
                this.editForm.workMan=row.workMan
                this.editForm.testStime=row.testStime
                this.editForm.workStime=row.workStime
                this.editForm.workEtime=row.workEtime
                this.editForm.planStime=row.planStime
                this.editForm.unplanStime=row.unplanStime
                this.editForm.doneNum=row.doneNum
                this.editForm.passNum=row.passNum
                this.editForm.failNum=row.failNum
                this.editForm.lf=row.lf
                this.editForm.jf=row.jf
                this.editForm.df=row.df
                this.editForm.gf=row.gf
                this.editForm.dcl=row.dcl
                this.editForm.prodMan=row.prodMan
                this.editForm.testEtime=row.testEtime
                this.editForm.repassNum=row.repassNum

                this.editVis = true;
                // this.$message.warning("Sean还在努力开发中！敬请期待")
             }else{
                 this.$message.error("你没有权限！")
             }
        },
        bgDel(row){
             if(localStorage.getItem("ms_username") == "Sean" || localStorage.getItem("ms_username") == "Chuck Yu"){
                 this.bgDelVis = true;
                 this.delBGD = row.bgNo
                //  this.$message.warning("Sean还在努力开发中！敬请期待")
             }else{
                 this.$message.error("你没有权限！")
             }
        },
        editClose(done){
             this.$confirm('确认关闭？')
            .then(_ => {
                this.$refs['editForm'].resetFields();
                done();
            })
            .catch(_ => {});
        },
        editCfm(){
            this.$refs['editForm'].validate((valid) => {
                if (valid) {
                    fetch('api/WorkReport/editCfm',{
                        method:'POST',
                        headers:{
                            'Content-Type':'application/json'
                        },
                        body:JSON.stringify({
                            bgNo:this.editForm.bgNo,
                            prodDate:this.editForm.prodDate,
                            clsDate:this.editForm.clsDate,
                            cls:this.editForm.cls,
                            workMan:this.editForm.workMan,
                            testStime:this.editForm.testStime,
                            workStime:this.editForm.workStime,
                            workEtime:this.editForm.workEtime,
                            planStime:this.editForm.planStime,
                            unplanStime:this.editForm.unplanStime,
                            doneNum:this.editForm.doneNum,
                            passNum:this.editForm.passNum,
                            failNum:this.editForm.failNum,
                            lf:this.editForm.lf,
                            jf:this.editForm.jf,
                            df:this.editForm.df,
                            gf:this.editForm.gf,
                            dcl:this.editForm.dcl,
                            prodMan:this.editForm.prodMan,
                            testEtime:this.editForm.testEtime,
                            repassNum:this.editForm.repassNum
                        }) 
                    }).then(response=>response.json())
                    .then(data=>{
                        if(data[0].resSign){
                            this.$message.success("修改成功！")
                            this.editVis=false
                            this.$refs['editForm'].resetFields();
                        }else{
                            this.$message.error("找不到报工单，无法修改！")
                        }
                    }).catch(data=>{
                        alert(data)
                    })
                }else{
                    return false;
                }
            })
        },
        editCancel(){
            this.editVis=false
            this.$refs['editForm'].resetFields();
        },
        expTab(){
            if(this.bgTab.length == 0){
                this.$message.warning("当前表格中无信息，请选择合适筛选条件已找到想要导出的表格信息。")
            }else{
                 //创建book
                var wb = XLSX.utils.book_new();
                //json转sheet
                var ws = XLSX.utils.json_to_sheet(this.bgTab, {header:["bgNo","prodDate","clsDate","workUnit","cls","workMan","workNo","wlNo","wlDesc","proceNo","proceName","fixNum",
                                                                        "testStime","testTime","workStime","workEtime","planStime","unplanStime","doneNum","passNum","failNum","lf","jf",
                                                                        "df","gf","dcl","thryTime","pWorkTime","opsTime","fixProdNum","passRate","manEff","eqpEff","prodMan","fixMan","oee",
                                                                        "cgroup","status","bgCate","testEtime","test2done","work2done","ordType","repassNum","ftt","tjl"], skipHeader:false});
                //设置列宽
                ws['!cols']= [
                    {width: 15},
                    {width: 15},
                    {width: 15},
                    {width: 15},
                    {width: 15},
                    {width: 15},
                    {width: 15},
                    {width: 15},
                    {width: 15},
                    {width: 15},
                    {width: 15},
                    {width: 15},
                    {width: 15},
                    {width: 15},
                    {width: 15},
                    {width: 15},
                    {width: 15},
                    {width: 15},
                    {width: 15},
                    {width: 15},
                    {width: 15},
                    {width: 15},
                    {width: 15},
                    {width: 15},
                    {width: 15},
                    {width: 15},
                    {width: 15},
                    {width: 15},
                    {width: 15},
                    {width: 15},
                    {width: 15},
                    {width: 15},
                    {width: 15},
                    {width: 15},
                    {width: 15},
                    {width: 15},
                    {width: 15},
                    {width: 15},
                    {width: 15},
                    {width: 15},
                    {width: 15},
                    {width: 15},
                    {width: 15},
                    {width: 15},
                    {width: 15},
                    {width: 15}
                ];
                var timestamp = this.dateToString(new Date())
                //sheet写入book
                XLSX.utils.book_append_sheet(wb, ws, "file");
                //输出
                XLSX.writeFile(wb,"报工单表格_"+timestamp+".xlsx");
            }
        },
        delCfm(){
            this.bgDelVis = false;
            fetch('api/WorkReport/delBGD',{
                method:'POST',
                headers:{
                    'Content-Type':'application/json'
                },
                body:JSON.stringify({
                    bgNo:this.delBGD
                })
            }).then(response=>response.json())
            .then(data=>{
                if(data[0].resSign){
                    this.$message.success("删除成功");
                    this.bgDelVis=false;
                }else{
                    this.$message.error("删除失败")
                }
            }).catch(data=>{
                alert(data)
            })
        },
        delCancel(){
            this.bgDelVis = false;
        },
        delClose(done){
            this.$confirm('确认关闭？')
            .then(_ => {
                done();
            })
            .catch(_ => {});
        },
        bgView(row){
            this.showTab = true;
            this.bgNo=row.bgNo
            this.maniMan=row.workMan
            this.name=row.wlDesc
            this.ftt=row.ftt
            this.birth=row.prodDate
            this.bclass=row.cls
            this.workNo=row.workNo
            this.a=row.eqpEff
            this.wlNo=row.wlNo
            this.procedure=row.proceNo
            this.proceName=row.proceName
            this.wHandle=row.dcl
            this.p=row.manEff
            this.doneNo=row.doneNum
            this.passNo=row.passNum
            this.rePassNo=row.repassNum
            this.q=row.passRate
            this.failNo=row.failNum
            this.lf=row.lf
            this.jf=row.jf
            this.gf=row.gf
            this.df=row.df
            this.apq=(((row.eqpEff/100)*(row.manEff/100)*(row.passRate/100))*100).toFixed(2)
            this.zlgetTab1(row.bgNo)
            this.zlgetTab2(row.bgNo)
        },
        zlgetTab1(prm){
            this.failedRec = [];
            fetch('api/WorkReport/getFailLog',{
                method:'POST',
                headers:{
                    'Content-Type': 'application/json',
                },
                body:JSON.stringify({
                    bgNo:prm
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
        zlgetTab2(prm){
            this.stopRec = [];
            fetch('api/WorkReport/getStopLog',{
                method:'POST',
                headers:{
                    'Content-Type': 'application/json',
                },
                body:JSON.stringify({
                    bgNo:prm
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
        },
        gdzlClose(done){
            this.$confirm('确认关闭？')
            .then(_ => {
                done();
            })
            .catch(_ => {});
        }
    }
}
</script>
<style scoped>
.tip{
    margin:5px;
    font-size:18px;
    color:rgba(0,0,0,0.7)
    /* border-bottom:5px double rgba(0,0,0,0.5); */
}

</style>