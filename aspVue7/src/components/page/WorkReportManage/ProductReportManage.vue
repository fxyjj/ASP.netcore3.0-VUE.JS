<template>
    <div>
        <el-row>
            <el-col :span="4">
                <el-card style="height:398px;margin-right:8px">
                    <div class="title" style="font-size:18px;"> 产线名：{{pLine}} </div>
                    <div>
                        <el-button class="Btns" @click="blpClick()" type="primary">不良品记录</el-button>
                        <!-- <el-button class="Btns">异常报检</el-button> -->
                        <!-- <el-button class="Btns">生产记录</el-button> -->
                        <el-button class="Btns" @click="planS()" :disabled="wtherStop" type="primary">计划停机</el-button>
                        <el-button class="Btns" @click="unplanS()" :disabled="wtherStop" type="primary">异常停机</el-button>
                        <el-button class="Btns" @click="stopLog()" type="primary">停机信息</el-button>
                        <el-button class="Btns">打印标签</el-button>
                    </div>
                </el-card>
            </el-col>
            <el-col :span="20">
                <el-card>
                    <el-steps :active="bgStage-1" style="margin:0px 50px">
                        <el-step title="初始报工单" icon="el-icon-edit"></el-step>
                        <el-step title="产品调试" icon="el-icon-upload"></el-step>
                        <el-step title="开始生产" icon="el-icon-picture"></el-step>
                        <el-step title="生产报工" icon="el-icon-picture"></el-step>
                        <el-step title="报工完毕" icon="el-icon-picture"></el-step>
                    </el-steps>
                </el-card>
                <el-card v-if="bgStage==0" style="height:290px;margin:8px 0px"> 当前还没有报工单，您可以创建新的报工单！</el-card>
                
                <el-card v-else-if="!wtherStop" style="height:290px;margin:8px 0px">
                    <el-row>
                        <el-col :span="4">
                            <el-card shadow="hover" style="height:250px;">
                                <div v-if="bgStage>=1">
                                    <div class="bgd">报工编号:{{bgNo}}</div>
                                    <div class="bgd">创建人:{{bgcrtMan}}</div>
                                    <div class="bgd">创建日期:{{bgcrtDate}}</div>
                                    <div class="bgd">工序号:{{bgproceNo}}</div>
                                    <div class="bgd">工序名称:{{bgproceName}}</div>
                                    <div class="bgd">报工类别:{{bgCate}}</div>
                                    <div class="bgd">定额件数:{{bgfixNum}}</div>
                                    <div class="bgd">定额人数:{{bgfixMan}}</div>
                                    <div class="bgd">加工单元:{{bgWunit}}</div>
                                    
                                </div>
                            </el-card>
                        </el-col>
                        <el-col :span="1" ><el-divider style="height:250px;margin:0% 50%" direction="vertical"></el-divider></el-col>
                        <el-col :span="4">
                            <!-- 调试完成后显示 -->
                            <el-card v-if="bgStage>=3" style="height:250px" shadow="hover">
                                <div class="bgd">调试开始时间:</div>
                                <div class="bgd">{{tStime}} </div>
                                <div class="bgd">调试结束时间:</div>
                                <div class="bgd">{{tEtime}} </div>
                                <div class="bgd">调试开始人:{{tSman}}</div>
                                <div class="bgd">调试结束人:{{tEman}}</div>
                                <div class="bgd">调试时间:{{duration}}</div>
                            </el-card>
                            <el-card v-else-if="bgStage==2" style="height:250px" shadow="hover">
                                <el-input v-model="testMan" placeholder="调试员"></el-input>
                                <el-input v-model="Cgroup" placeholder="班组"></el-input>
                                <el-date-picker v-model="testStart" type="datetime" placeholder="选择开始时间" style="width:203px"></el-date-picker>
                                <el-row>
                                    <el-col :span="18">
                                        <div class="clock" >{{Hr}}:{{Min}}:{{Sec}}</div>
                                    </el-col>
                                    <el-col :span="6">
                                        <div class="clockCtrl" @click="tBegin">
                                            <i v-if="!begin" class="el-icon-video-play" style="background:rgba(0,0,0,0.5);border-radius:50%;color:#fff"></i>
                                            <i v-else class="el-icon-video-pause" style="background:rgba(0,0,0,0.5);border-radius:50%;color:#fff"></i>
                                        </div>
                                        <div class="clockCtrl" @click="tStop">
                                            <div style="background:rgba(0,0,0,0.5);height:15px;width:15px;margin:8px 5px 5px 8px;"></div>
                                        </div>
                                    </el-col>
                                </el-row>
                            </el-card>
                            <div v-else-if="bgStage==1">
                                <div class="fcnBtn" @click="Ts()">开始调试</div>
                            </div>
                        </el-col>
                        <el-col :span="1"><el-divider direction="vertical" style="height:250px;margin:0% 50%"></el-divider></el-col>
                        <el-col :span="4">  
                            <div v-if="bgStage==3">
                                    <div v-if="prodVis" class="fcnBtn" @click="Sc()">开始生产</div>
                                    <el-card v-else style="height:250px" shadow="hover" >
                                        <el-input v-model="workMan" placeholder="加工人员"></el-input>
                                        <el-input v-model="prodCgp" placeholder="所属班组"></el-input>
                                        <el-date-picker v-model="prodStime" type="date" placeholder="生产日期" style="width:203px"></el-date-picker>
                                        <el-date-picker v-model="workStime" type="datetime" placeholder="作业开始时间" style="width:203px"></el-date-picker>
                                        <el-button type="primary" @click="prodComfirm()">确认</el-button>
                                    </el-card>
                            </div>
                            <el-card v-else-if="bgStage>=4" style="height:250px" shadow="hover" >
                                <div class="bgd">加工人员:{{scworkMan}}</div>
                                <div class="bgd">所属班组:{{scBZ}}</div>
                                <div class="bgd">作业开始时间</div>
                                <div class="bgd">{{scStart}}</div>
                                <div class="bgd">生产日期</div>
                                <div class="bgd">{{scDate}}</div>
                            </el-card>
                        </el-col>
                        <el-col :span="1"><el-divider direction="vertical" style="height:250px;margin:0% 50%"></el-divider></el-col>
                        <el-col :span="4">
                            <div v-if="bgStage==4">
                                <div class="fcnBtn" @click="Bg()">开始报工</div>
                                <el-dialog title="报工记录" :visible.sync="BgVis" :before-close="BgClose">
                                    <el-form ref="BgForm" :rules="BgRule" :model="BgForm" label-width="120px">
                                         <el-form-item label="作业单号" prop="BgworkNo">
                                            <el-input  v-model="BgForm.BgworkNo" disabled></el-input>
                                        </el-form-item>
                                        <el-form-item label="工序号" prop="BgproceNo">
                                            <el-input  v-model="BgForm.BgproceNo" disabled></el-input>
                                        </el-form-item>
                                        <el-form-item label="报工日期" prop="BgTime">
                                            <el-date-picker v-model="BgForm.BgTime" type="date" style="width:203px"></el-date-picker>
                                        </el-form-item>
                                        <el-form-item label="生产人数" prop="BgprodNum">
                                            <el-input  v-model.number="BgForm.BgprodNum" ></el-input>
                                        </el-form-item>
                                        <el-form-item label="合格数量" prop="BgpassNum">
                                            <el-input  v-model.number="BgForm.BgpassNum"></el-input>
                                        </el-form-item>
                                        <el-form-item  label="返工合格数量" prop="BgRepassNum">
                                            <el-input v-model.number="BgForm.BgRepassNum"></el-input>
                                        </el-form-item>
                                        <!-- <el-form-item label="不良数量" prop="BgFlawNum">
                                            <el-input v-model.number="BgForm.BgFlawNum"></el-input>
                                        </el-form-item> -->
                                            <el-form-item label="料废" prop="BgLf">
                                            <el-input v-model.number="BgForm.BgLf" ></el-input>
                                        </el-form-item>
                                        <el-form-item label="机废" prop="BgJf">
                                            <el-input v-model.number="BgForm.BgJf"></el-input>
                                        </el-form-item>
                                        <el-form-item label="调废" prop="BgDf">
                                            <el-input v-model.number="BgForm.BgDf" ></el-input>
                                        </el-form-item>
                                        <el-form-item label="工废" prop="BgGf">
                                            <el-input v-model.number="BgForm.BgGf"></el-input>
                                        </el-form-item>
                                        <el-form-item label="待处理" prop="BgDcl">
                                            <el-input v-model.number="BgForm.BgDcl"></el-input>
                                        </el-form-item>
                                        <el-form-item label="不合格数量" prop="BgFailNum">
                                            <el-input v-model.number="BgForm.BgFailNum"></el-input>
                                        </el-form-item>
                                    </el-form>
                                    <el-button type="primary" @click="BgComfirm('BgForm')">确认</el-button>
                                    <el-button type="primary" @click="BgCancel()">取消</el-button>
                                </el-dialog>   
                            </div>
                            
                            <el-card v-else-if="bgStage>=5" style="height:250px" shadow="hover">
                                <div class="bgd">完工时间:{{pBgdownTime}}</div>
                                <div class="bgd">合格数量:{{pBgpassNum}}</div>
                                <div class="bgd">料废:{{pBgLf}}</div>
                                <div class="bgd">机废:{{pBgJf}}</div>
                                <div class="bgd">调废:{{pBgDf}}</div>
                                <div class="bgd">工废:{{pBgGf}}</div>
                                <div class="bgd">待处理:{{pBgDcl}}</div>
                                <div class="bgd">不合格数量:{{pBgfailNum}}</div>
                                <div class="bgd">返工合格数量:{{pBgRepassNum}}</div>
                            </el-card>
                        </el-col>
                        <el-col :span="1"><el-divider direction="vertical" style="height:250px;margin:0% 50%"></el-divider></el-col>
                        <el-col :span="4">
                            <el-card v-if="bgStage>=5" style="height:250px" shadow="hover">
                               <div class="fcnBtn" @click="BgComplete()">关闭报工单</div>
                            </el-card>
                        </el-col>
                    </el-row>   
                </el-card>
                <el-card v-else style="height:290px;margin:8px 0px">
                    停机了
                    <div>停机类型：{{pauseType}}</div>
                    <div>停机描述：{{pauseDesc}}</div>
                    <div>停机人：{{pauseMan}}</div>
                    <el-button type="primary" @click="prodContinue()" :disabled="wtherZLP">立即开始</el-button>
                </el-card>
            </el-col>
        </el-row>
        <el-row>
             <el-col :span="18">
                <el-row>
                     <el-col :span="5">
                          <el-card style="margin-right:8px">
                                <div class="title">当班员工信息</div>
                                <el-table :data="manInfo" style="" height="220">
                                    <el-table-column prop="posReg" label="岗位登记" ></el-table-column>
                                    <el-table-column prop="man" label="员工" ></el-table-column>
                                    <el-table-column prop="skill" label="技能" ></el-table-column>
                                </el-table>
                            </el-card>
                    </el-col>
                    <el-col :span="19">
                        <el-card style="margin-right:8px;height:300px;">
                            <div class="title" >当前订单</div>
                            <div class="tgs">订单编号：{{ordNo}}</div>
                            <div class="tgs">物料编号：{{wlNo}}</div>
                            <div class="tgs">物料描述：{{wlDesc}}</div>
                            <div class="tgs">订单数量：{{ordNum}}</div>
                            <div class="tgs" style="margin-right:1%;width:184px" >完工数量：{{downNum}}</div>
                            <div class="tgs" style="margin-left:1%;width:184px" >不合格数量：{{failNum}}</div>
                            <div class="tgs">当前报工单：{{currBGD}}</div>
                            <el-row style="margin:1% 2% 2%">
                                <el-col :span="3"><div style="font-size:25px">订单进度</div></el-col>
                                <el-col :span="21"><el-progress :stroke-width="25" :percentage="pct" color="blue" style="margin-top:6px;"></el-progress></el-col>
                            </el-row>
                            
                        </el-card>
                    </el-col>
                </el-row>
                <el-row>
                    <el-card style="margin-top:8px;margin-right:8px">
                        <div class="title">所有订单</div>
                        <el-table :data="allOrder" style="" height="190" border>
                            <el-table-column prop="workNo" label="作业单号" width="120"></el-table-column>
                            <el-table-column prop="wlNo" label="物料编号" width="150"></el-table-column>
                            <el-table-column prop="wlDesc" label="物料描述" width="200"></el-table-column>
                            <el-table-column prop="proceNo" label="工序" width="60" ></el-table-column>
                            <el-table-column prop="proceName" label="工序名称" width="80"></el-table-column>
                            <el-table-column prop="sTime" label="开始日期" width="160"></el-table-column>
                            <el-table-column prop="eTime" label="完工日期" width="160"></el-table-column>
                            <el-table-column prop="ordNum" label="订单数量" ></el-table-column>
                            <el-table-column prop="unfnsNum" label="未完工" ></el-table-column>
                            <el-table-column prop="leftHr" label="剩余工时" ></el-table-column>
                            <el-table-column prop="testHr" label="调试计划用时" width="100"></el-table-column>
                            <el-table-column  label="操作" fixed="right" width="160">
                                <template slot-scope="scope">
                                    <el-button @click="create(scope.row)" :disabled="creatable">创建</el-button><!--:style='{background:(scope.row.status=="创建"?"#85ce61":"#F5F5F5")}' size="small" :disabled="scope.row.status=='创建'?false:true"-->
                                    <el-button @click="close(scope.row)">关闭</el-button>
                                </template>
                            </el-table-column>

                        </el-table>
                    </el-card>
                </el-row>
            </el-col>
            <el-col :span="6">
                <el-card style="height:580px;">
                    <div class="title">Andon设置</div>
                    <div id="pieBtn" style="width:100%;height:370px;margin:15% 0%;text-align:center"></div><!--width:370px;-->
                </el-card>
            </el-col>
           
        </el-row>
        <el-dialog title="创建报工单" :visible.sync="crtVis" :before-close="crtClose" >
             <el-form ref="crtForm" :model="crtForm" :rules="crtRule" label-width="80px">
                <el-form-item label="作业单号" prop="workNo">
                    <el-input v-model="crtForm.workNo" disabled></el-input>
                </el-form-item>
                <el-form-item label="物料编号" prop="wlNo">
                    <el-input  v-model="crtForm.wlNo" disabled></el-input>
                </el-form-item>
                 <el-form-item label="物料描述" prop="wlDesc">
                    <el-input v-model="crtForm.wlDesc" disabled></el-input>
                </el-form-item>
                <el-form-item label="工序号" prop="proceNo">
                     <el-input v-model="crtForm.proceNo" disabled></el-input>
                </el-form-item>
                <el-form-item label="工序名称" prop="proceName" >
                  <el-input v-model="crtForm.proceName" disabled></el-input>
                </el-form-item>
                <el-form-item label="定额件数" prop="fixNum">
                     <el-input v-model="crtForm.fixNum" disabled></el-input>
                </el-form-item>
                 <el-form-item label="定额人数" prop="fixMan">
                     <el-input v-model="crtForm.fixMan" disabled></el-input>
                </el-form-item>
                <el-form-item label="生产单元" prop="pUnit">
                    <el-input v-model="crtForm.pUnit" disabled></el-input>
                </el-form-item>
                <el-form-item label="工作组" prop="station">
                    <el-input  v-model="crtForm.station" disabled></el-input>
                </el-form-item>
                 <el-form-item label="报工类别" prop="bgCate">
                    <el-input v-model="crtForm.bgCate" disabled></el-input>
                </el-form-item>
                 <el-form-item label="创建人" prop="crtMan">
                    <el-input v-model="crtForm.crtMan" ></el-input>
                </el-form-item>
                <el-form-item label="创建日期" prop="crtDate">
                   <el-date-picker v-model="crtForm.crtDate" type="date" placeholder="选择日期"></el-date-picker>
                </el-form-item>
            </el-form>
            <el-button type="success" @click="crtCancel()" style="width:40%">取消</el-button>
            <el-button type="danger" style="width:40%;margin-left:20%" @click="crtconfirm('crtForm')">确认</el-button>
        </el-dialog>
        <el-dialog title="调试结束提示" :visible.sync="testVis" :before-close="testClose">
            <div>你现在点击了停止调试，如果确认操作无误，请核对一下调试信息，如需重新调试，点击取消按钮返回上一界面继续调试，如确认调试结束，点击确认。</div>
             <el-form ref="testForm" :model="testForm" :rules="testRule" label-width="120px">
                <el-form-item label="作业单号" prop="workNo">
                    <el-input v-model="testForm.workNo" disabled></el-input>
                </el-form-item>
                <el-form-item label="调试结束时间" prop="testEtime">
                    <el-date-picker v-model="testForm.testEtime" type="datetime"  placeholder="选择日期" disabled></el-date-picker>
                </el-form-item>
                 <el-form-item label="调试累计用时" prop="testTime">
                    <el-input v-model="testForm.testTime" disabled></el-input>
                </el-form-item>
                <el-form-item label="调试结束人员" prop="testMan">
                     <el-input v-model="testForm.testMan"></el-input>
                </el-form-item>
                <el-form-item label="备注" prop="tips">
                     <el-input v-model="testForm.tips"></el-input>
                </el-form-item>
             </el-form>
            <el-button type="primary" @click="testComfirm('testForm')">确认</el-button>
            <el-button type="primary" @click="testCancel()">取消</el-button>
        </el-dialog>
        <!-- 不良品弹窗 -->
        <el-dialog title="不良品记录单" :visible.sync="blpVis" :before-close="blpClose">
            <el-form ref="blpForm" :model="blpForm" :rules="blpRule" label-width="100px">
                <el-row>
                    <el-col :span="8">
                        <el-form-item label="作业单号" prop="blpworkNo" >
                            <el-input v-model="blpForm.blpworkNo" :disabled="inputDis"></el-input>
                        </el-form-item>
                    </el-col>
                    <el-col :span="8">
                        <el-form-item label="生产线" prop="blpPline">
                            <el-input v-model="blpForm.blpPline" :disabled="inputDis"></el-input>
                        </el-form-item>
                    </el-col>
                    <el-col :span="8">
                        <el-form-item label="发现部门" prop="blpDpart">
                            <el-input v-model="blpForm.blpDpart" :disabled="inputDis"></el-input>
                        </el-form-item>
                    </el-col>
                </el-row>
                 <el-row>
                    <el-col :span="8">
                        <el-form-item label="主物料号" prop="blpMwlNo">
                            <el-input v-model="blpForm.blpMwlNo" :disabled="inputDis"></el-input>
                        </el-form-item>
                    </el-col>
                    <el-col :span="8">
                        <el-form-item label="填写人" prop="blpMan">
                            <el-input v-model="blpForm.blpMan" :disabled="inputDis"></el-input>
                        </el-form-item>
                    </el-col>
                    <el-col :span="8">
                        <el-form-item label="发现班组" prop="blpCgroup">
                            <el-input v-model="blpForm.blpCgroup" :disabled="inputDis"></el-input>
                        </el-form-item>
                    </el-col>
                </el-row>
                 <el-row>
                    <el-col :span="8">
                        <el-form-item label="主物料描述" prop="blpMwldesc">
                            <el-input v-model="blpForm.blpMwldesc" :disabled="inputDis"></el-input>
                        </el-form-item>
                    </el-col>
                    <el-col :span="8">
                        <el-form-item label="填写日期" prop="blpDate">
                            <!-- <el-input v-model="blpForm.blpDate" :disabled="inputDis"></el-input> -->
                            <el-date-picker v-model="blpForm.blpDate" type="date" placeholder="选择日期" :disabled="inputDis"></el-date-picker>
                        </el-form-item>
                    </el-col>
                    <el-col :span="8">
                        <el-form-item label="报工编号" prop="blpbgNo">
                            <el-input v-model="blpForm.blpbgNo" :disabled="inputDis"></el-input>
                        </el-form-item>
                    </el-col>
                </el-row>
                 <el-row>
                    <el-col :span="8">
                        <el-form-item label="工序号" prop="blpProceNo">
                            <el-input v-model.number="blpForm.blpProceNo" :disabled="inputDis"></el-input>
                        </el-form-item>
                    </el-col>
                    <el-col :span="8">
                        <el-form-item label="工序描述" prop="blpProceDesc">
                            <el-input v-model="blpForm.blpProceDesc" :disabled="inputDis"></el-input>
                        </el-form-item>
                    </el-col>
                </el-row>
                <!-- 不合格记录信息 -->
                <el-row>
                    <el-col :span="6">
                        <el-form-item label="序号" prop="blpIDNo">
                            <el-input v-model.number="blpForm.blpIDNo"  :disabled="inputDis"></el-input>
                        </el-form-item>
                    </el-col>
                    <el-col :span="6">
                        <el-form-item label="子物料号" prop="blpZwlNo">
                            <el-input v-model="blpForm.blpZwlNo"></el-input>
                        </el-form-item>
                    </el-col>
                    <el-col :span="6">
                        <el-form-item label="子物料描述" prop="blpZwlDesc">
                            <el-input v-model="blpForm.blpZwlDesc"></el-input>
                        </el-form-item>
                    </el-col>
                    <el-col :span="6">
                        <el-form-item label="数量" prop="blpNum">
                            <el-input v-model.number="blpForm.blpNum" ></el-input>
                        </el-form-item>
                    </el-col>
                </el-row>
                <el-row>
                    <el-col :span="6">
                        <el-form-item label="工位" prop="blpPos">
                            <el-input v-model="blpForm.blpPos"></el-input>
                        </el-form-item>
                    </el-col>
                    <el-col :span="6">
                        <el-form-item label="不良描述" prop="blpFDesc">
                            <el-input v-model="blpForm.blpFDesc"></el-input>
                        </el-form-item>
                    </el-col>
                    <el-col :span="6">
                        <el-form-item label="返工合格数量" prop="blpRPsNum" label-width="120px">
                            <el-input v-model.number="blpForm.blpRPsNum" ></el-input>
                        </el-form-item>
                    </el-col>
                    <el-col :span="6">
                        <el-form-item label="合格原因" prop="blpPreason">
                            <el-input v-model="blpForm.blpPreason"></el-input>
                        </el-form-item>
                    </el-col>
                </el-row>
                <el-form-item label="备注" prop="blpTip">
                    <el-input v-model="blpForm.blpTip"></el-input>
                </el-form-item>
            </el-form>
             <el-table :data="blpTab" style="width: 100%" height="300px">
                <el-table-column prop="tabID" label="序号" width="100"></el-table-column>
                <el-table-column prop="tabwlNo" label="物料编号" width="100"></el-table-column>
                <el-table-column prop="tabwlDesc" label="物料描述" width="100"></el-table-column>
                <el-table-column prop="tabNum" label="数量" width="100"></el-table-column>
                <el-table-column prop="tabPos" label="工位" width="100"></el-table-column>
                <el-table-column prop="tabfDesc" label="不良描述" width="100"></el-table-column>
                <el-table-column prop="tabRpsNum" label="返工合格数量" width="100"></el-table-column>
                <el-table-column prop="tabRsn" label="合格原因" width="100"></el-table-column>
                <el-table-column prop="tabTip" label="备注" width="100"></el-table-column>
             </el-table>
            <el-button type="primary" @click="blpAdd()" v-if="!storeVis">添加</el-button>
            <el-button v-if="storeVis" type="primary" @click="blpStore()">保存</el-button>
            <el-button v-else type="primary" @click="blpComfirm()">确认</el-button>
        </el-dialog>

        <!-- 计划停机弹窗 -->
        <el-dialog title="计划停机表单" :visible.sync="planVis" :before-close="planClose">
              <el-form ref="planForm" :model="planForm" :rules="planRule" label-width="120px">
                <el-form-item label="作业单号" prop="workNo">
                    <el-input v-model="planForm.workNo" disabled></el-input>
                </el-form-item>
                <el-form-item label="停机类型" prop="planStype">
                    <el-select v-model="planForm.planStype" placeholder="请选择" @change="typeC">
                        <el-option v-for="item in Stype" :key="item.label" :value="item.value">{{item.value}}</el-option>
                    </el-select>
                </el-form-item>
                 <el-form-item label="停机开始" prop="planSstime">
                   <el-date-picker v-model="planForm.planSstime" type="datetime"  placeholder="选择日期" @change="defEndT"></el-date-picker>
                </el-form-item>
                <el-form-item label="停机结束" prop="planSetime">
                    <el-date-picker v-model="planForm.planSetime" type="datetime"  placeholder="选择日期" :disabled="wtherFix"></el-date-picker>
                </el-form-item>
                <el-form-item label="填写人" prop="planMan">
                     <el-input v-model="planForm.planMan"></el-input>
                </el-form-item>
             </el-form>
            <el-button type="primary" @click="planComfirm('planForm')">确认</el-button>
            <el-button type="primary" @click="planCancel()">取消</el-button>

        </el-dialog>

        <!-- 非计划停机弹窗 -->
        <el-dialog title="非计划停机表单" :visible.sync="unplanVis" :before-close="unplanClose">
             <el-form ref="unplanForm" :model="unplanForm" :rules="unplanRule" label-width="120px">
                <el-form-item label="作业单号" prop="workNo">
                    <el-input v-model="unplanForm.workNo" disabled></el-input>
                </el-form-item>
                <el-form-item label="停机类型" prop="unplanStype">
                    <el-input v-model="unplanForm.unplanStype" disabled></el-input>
                    <!-- <el-date-picker v-model="unplanForm.unplanStype" type="datetime"  placeholder="选择日期" disabled></el-date-picker> -->
                </el-form-item>
                <el-form-item label="停机描述" prop="unplanSDesc">
                    <el-input v-model="unplanForm.unplanSDesc"></el-input>
                    <!-- <el-date-picker v-model="unplanForm.unplanStype" type="datetime"  placeholder="选择日期" disabled></el-date-picker> -->
                </el-form-item>
                 <el-form-item label="停机小类" prop="unplanStypem">
                   <el-select v-model="unplanForm.unplanStypem" placeholder="请选择">
                        <el-option v-for="item in unStypem" :key="item.label" :value="item.value">{{item.value}}</el-option>
                    </el-select>
                </el-form-item>
                 <el-form-item label="设备编号" prop="unplanSqNo">
                    <el-input v-model="unplanForm.unplanSqNo"></el-input>
                    <!-- <el-date-picker v-model="unplanForm.unplanStype" type="datetime"  placeholder="选择日期" disabled></el-date-picker> -->
                </el-form-item>
                 <el-form-item label="停机开始" prop="unplanSstime">
                   <el-date-picker v-model="unplanForm.unplanSstime" type="datetime"  placeholder="选择日期"></el-date-picker>
                </el-form-item>
                
                <el-form-item label="填写人" prop="unplanMan">
                     <el-input v-model="unplanForm.unplanMan"></el-input>
                </el-form-item>
             </el-form>
            <el-button type="primary" @click="unplanComfirm('unplanForm')">确认</el-button>
            <el-button type="primary" @click="unplanCancel()">取消</el-button>

        </el-dialog>

        <!-- 停机信息弹窗 -->
        <el-dialog title="停机情况" :visible.sync="sInfoVis">
            <el-table :data="Stab" style="width: 100%">
                <el-table-column prop="sType" label="停机类型" width="180"></el-table-column>
                <el-table-column prop="sDesc" label="停机描述" width="180"></el-table-column>
                <el-table-column prop="sStart" label="停机开始" width="180"></el-table-column>
                <el-table-column prop="sEnd" label="停机结束" width="180"></el-table-column>
                <el-table-column prop="testPause" label="调试暂停" width="180"></el-table-column>
                <el-table-column prop="sMan" label="填写人" width="180"></el-table-column>
                <el-table-column prop="sTime" label="停机时间" width="180"></el-table-column>
                <el-table-column prop="sqNo" label="设备编号" width="180"></el-table-column>
                <el-table-column prop="status" label="状态" width="180"></el-table-column>
            </el-table> 
        </el-dialog>

        <!-- 添加安灯记录 -->
        <el-dialog title="新安灯记录" :visible.sync="newAndonVis" :before-close="zlAndonClose">
            <el-form v-if="zlVis==0" ref="zlForm" :model="zlForm" :rules="zlRule" label-width="120px">
                <el-row>
                    <el-col :span="8">
                        <el-form-item label="序号" prop="no">
                            <el-input v-model.number="zlForm.no" disabled></el-input>
                        </el-form-item>
                    </el-col>
                     <el-col :span="8">
                        <el-form-item label="作业单号" prop="zlworkNo">
                            <el-input v-model="zlForm.zlworkNo" disabled></el-input>
                        </el-form-item>
                    </el-col>
                     <el-col :span="8">
                        <el-form-item label="填写人" prop="zlMan">
                            <el-input v-model="zlForm.zlMan" :disabled="zlInpVis"></el-input>
                        </el-form-item>
                    </el-col>
                </el-row>
                <el-row>
                    <el-col :span="8">
                        <el-form-item label="发现部门" prop="zlDpt">
                            <el-input v-model="zlForm.zlDpt" :disabled="zlInpVis"></el-input>
                        </el-form-item>
                    </el-col>
                     <el-col :span="8">
                        <el-form-item label="发现班组" prop="zlCls">
                            <el-input v-model="zlForm.zlCls" :disabled="zlInpVis"></el-input>
                        </el-form-item>
                    </el-col>
                     <el-col :span="8">
                        <el-form-item label="数量" prop="zlNum">
                            <el-input v-model.number="zlForm.zlNum"></el-input>
                        </el-form-item>
                    </el-col>
                </el-row>
                 <el-row>
                    <el-col :span="8">
                        <el-form-item label="物料号" prop="zlwlNo">
                            <el-input v-model="zlForm.zlwlNo" ></el-input>
                        </el-form-item>
                    </el-col>
                     <el-col :span="8">
                        <el-form-item label="物料描述" prop="zlwlDesc">
                            <el-input v-model="zlForm.zlwlDesc"></el-input>
                        </el-form-item>
                    </el-col>
                     <el-col :span="8">
                        <el-form-item label="工位" prop="zlPos">
                            <el-input v-model="zlForm.zlPos"></el-input>
                        </el-form-item>
                    </el-col>
                </el-row>
                <el-row>
                    <el-col :span="8">
                        <el-form-item label="问题描述" prop="zlQdesc">
                            <el-input v-model="zlForm.zlQdesc" ></el-input>
                        </el-form-item>
                    </el-col>
                     <el-col :span="8">
                        <el-form-item label="返工合格数量" prop="zlRPsNum">
                            <el-input v-model.number="zlForm.zlRPsNum"></el-input>
                        </el-form-item>
                    </el-col>
                     <el-col :span="8">
                        <el-form-item label="设备编号" prop="zlsbNo">
                            <el-input v-model="zlForm.zlsbNo"></el-input>
                        </el-form-item>
                    </el-col>
                </el-row>
                <el-row>
                    <el-col :span="8">
                        <el-form-item label="合格原因" prop="zlRea">
                            <el-input v-model="zlForm.zlRea" ></el-input>
                        </el-form-item>
                    </el-col>
                     <el-col :span="8">
                        <el-form-item label="备注" prop="zlTip">
                            <el-input v-model="zlForm.zlTip"></el-input>
                        </el-form-item>
                    </el-col>
                     <el-col :span="8">
                        <el-form-item label="安灯人" prop="zlAndonMan">
                            <el-input v-model="zlForm.zlAndonMan"></el-input>
                        </el-form-item>
                    </el-col>
                </el-row>
                <!-- <el-row>
                    <el-col :span="8">
                        <el-form-item label="是否停线" prop="zlwhrStop">
                            <el-input v-model="zlForm.zlwthrStop" disabled></el-input>
                        </el-form-item>
                    </el-col>
                </el-row> -->
            </el-form> 
            <el-form v-else-if="zlVis==1" ref="sbForm" :model="sbForm" :rules="sbRule" label-width="120px">
                <el-form-item label="作业单号" prop="sbworkNo">
                    <el-input v-model="sbForm.sbworkNo" disabled></el-input>
                </el-form-item>
                <el-row>
                    <el-col :span="8">
                        <el-form-item label="是否停线" prop="sbStop">
                            <el-select v-model="sbForm.sbStop" :disabled="sbInputVis">
                                <el-option  v-for="item in sbP" :key="item.value" :value="item.value">{{item.value}}</el-option>
                            </el-select>
                        </el-form-item>
                    </el-col>
                    <el-col :span="8">
                        <el-form-item label="优先级" prop="sbYxj">
                            <el-select v-model="sbForm.sbYxj">
                                <el-option  v-for="item in priority" :key="item.value" :value="item.value">{{item.value}}</el-option>
                            </el-select>
                        </el-form-item>
                    </el-col>
                    <el-col :span="8">
                        <el-form-item label="工作中心" prop="sbStation">
                            <el-input v-model="sbForm.sbStation" disabled></el-input>
                        </el-form-item>
                    </el-col>
                </el-row>
                <el-row>
                    <el-col :span="8">
                        <el-form-item label="报修部门" prop="sbDpt">
                            <el-input v-model="sbForm.sbDpt"></el-input>
                        </el-form-item>
                    </el-col>
                    <el-col :span="8">
                        <el-form-item label="报修人员" prop="sbMan">
                            <el-input v-model="sbForm.sbMan"></el-input>
                        </el-form-item>
                    </el-col>
                    <el-col :span="8">
                        <el-form-item label="设备编号" prop="sbNo">
                            <el-input v-model="sbForm.sbNo"></el-input>
                        </el-form-item>
                    </el-col>
                </el-row>
                <el-row>
                    <el-col :span="8">
                        <el-form-item label="设备名称" prop="sbName">
                            <el-input v-model="sbForm.sbName"></el-input>
                        </el-form-item>
                    </el-col>
                    <el-col :span="8">
                        <el-form-item label="故障部位" prop="sbQpart">
                            <el-input v-model="sbForm.sbQpart"></el-input>
                        </el-form-item>
                    </el-col>
                     <el-col :span="8">
                        <el-form-item label="故障类型" prop="sbQtype">
                            <el-input v-model="sbForm.sbQtype"></el-input>
                        </el-form-item>
                    </el-col>
                </el-row>
                <el-form-item label="故障描述" prop="sbQdesc">
                    <el-input v-model="sbForm.sbQdesc"></el-input>
                </el-form-item>
                
                <el-form-item label="故障描述" prop="sbQdesc">
                    <el-select v-model="sbForm.sbSafe">
                        <el-option  value="涉及安全">涉及安全</el-option>
                        <el-option  value="不涉及安全">不涉及安全</el-option>
                    </el-select>
                </el-form-item>
            </el-form>
            <el-form v-else ref="otherform" :model="otherForm" :rules="otherRule" label-width="120px">
                 <el-form-item label="作业单号" prop="othworkNo">
                    <el-input v-model="otherForm.othworkNo" disabled></el-input>
                </el-form-item>
                 <el-row>
                    <el-col :span="8">
                        <el-form-item label="按灯类别" prop="othAndonT">
                            <el-input v-model="otherForm.othAndonT" disabled></el-input>
                        </el-form-item>
                    </el-col>
                    <el-col :span="8">
                        <el-form-item label="工作中心" prop="othStation">
                            <el-input v-model="otherForm.othStation" disabled></el-input>
                        </el-form-item>
                    </el-col>
                     <el-col :span="8">
                        <el-form-item label="是否停线" prop="othStop">
                            <el-select v-model="otherForm.othStop" :disabled="othInputVis">
                                <el-option  v-for="item in sbP" :key="item.value" :value="item.value">{{item.value}}</el-option>
                            </el-select>
                        </el-form-item>
                    </el-col>
                </el-row>
                <el-row>
                    <el-col :span="8">
                        <el-form-item label="设备编号" prop="othsbNo">
                            <el-input v-model="otherForm.othsbNo"></el-input>
                        </el-form-item>
                    </el-col>
                    <el-col :span="8">
                        <el-form-item label="安灯人" prop="othMan">
                            <el-input v-model="otherForm.othMan"></el-input>
                        </el-form-item>
                    </el-col>
                </el-row>
                <el-form-item label="问题描述" prop="othQdesc"><el-input v-model="otherForm.othQdesc"></el-input></el-form-item>
            </el-form>
            <el-button type="primary" @click="zlAndonCfm()">确认</el-button>
            <el-button type="primary" @click="zlAndonCancel()">取消</el-button>
        </el-dialog>
        <!-- 安灯记录 -->
        <el-dialog title="安灯记录" :visible.sync="andonLogVis">
            <el-row>
                <el-col :span="8"><div class="">安灯类型:{{currAdT}}</div></el-col>
                <el-col :span="8"><div class="">状态:{{currAdSts}}</div></el-col>
                <el-col :span="8"><div class="">安灯编号:{{currAdNo}}</div></el-col>
               
            </el-row>
            <el-row>
                <el-col :span="8"><div class="">安灯时间:{{currAdtime}}</div></el-col>
                <el-col :span="8"><div class="">问题描述:{{currAdDesc}}</div></el-col>
                <el-col :span="8"><div class="">按灯人:{{currAdMan}}</div></el-col>
            </el-row>
            <el-input v-model="AdRespMan" placeholder="响应人"></el-input>
            <el-button type="primary" @click="AdResp()" :disabled="AdLife=0">响应</el-button>
            <el-button type="primary" @click="AdResolve()" :disabled="AdLife=1">解决</el-button>
            <el-button type="primary" @click="AdCfm()" :disabled="AdLife=2">确认</el-button>

            <el-table :data="Adtab" style="width: 100%">
                <el-table-column prop="AdMan" label="安灯人" width="180"></el-table-column>
                <el-table-column prop="AdNo" label="序号" width="180"></el-table-column>
                <el-table-column prop="AdsbNo" label="设备编号" width="180"></el-table-column>
                <el-table-column prop="AdqDesc" label="问题描述" width="180"></el-table-column>
                <el-table-column prop="AdTime" label="按灯时间" width="180"></el-table-column>
                <el-table-column prop="AdprcsTime" label="处理时间" width="180"></el-table-column>
                <el-table-column prop="AdprcsMan" label="处理人" width="180"></el-table-column>
                <el-table-column prop="AdshutMan" label="关闭人" width="180"></el-table-column>
                <el-table-column prop="AdshutTime" label="关闭时间" width="180"></el-table-column>
                <el-table-column prop="AdStation" label="工作中心" width="180"></el-table-column>
                <el-table-column prop="AdDuration" label="处理用时" width="180"></el-table-column>
                <el-table-column prop="AdStop" label="是否停线" width="180"></el-table-column>
                <el-table-column prop="AdFns" label="是否解决" width="180"></el-table-column>
                <el-table-column prop="AdPlan" label="行动计划" width="180"></el-table-column>
                <el-table-column prop="AdFreason" label="不能解决原因" width="180"></el-table-column>
                <el-table-column prop="AdfnsDate" label="计划完成日期" width="180"></el-table-column>
            </el-table> 
        </el-dialog>

    </div>
</template>
<script>
import echart from 'echarts'
import bus from '../../common/bus';
export default {
    data(){
        return {
            bgReport:[],
            manInfo:[],
            allOrder:[],
            pct:10,
            //andon圆形按钮参数
            piebtn:null,
            pieOpt:{
                animation:false,
                series: [{
                        type: 'pie',
                        radius: ['25%', '60%'],
                        label: {
                            position:'inner'
                        },
                        labelLine: {
                            show: false
                        },
                        data: [
                            {mark:'inner',value: 1, name: '0',colorB:'#b61414',colorT:'#000',tip:1},
                            {mark:'inner',value: 1, name: '1',colorB:'#0ba0ea',colorT:'#000',tip:2},
                            {mark:'inner',value: 1, name: '0',colorB:'#9914e6',colorT:'#000',tip:3},
                            {mark:'inner',value: 1, name: '0',colorB:'#1aee48',colorT:'#000',tip:4},
                            {mark:'inner',value: 1, name: '0',colorB:'#fbff23',colorT:'#000',tip:5}
                        ],
                        itemStyle:{
                            normal:{
                                label:{
                                    show:true,
                                    position:'inner',
                                    textStyle: { //图例文字的样式
                                        color: '#000',
                                        fontSize: 10
                                    },
                                },
                                color:function(params) {
                                    if(params.data['name']!='0'){
                                        return params.data['colorB'];
                                    }else{
                                        return '#C0C0C0'
                                    }
                                }, 
                            }
                        }
                        
                    },
                    {
                        name: '',
                        type: 'pie',
                        radius: ['60%', '100%'],
                        label: {
                            position:'inner'
                        },
                        labelLine: {
                            show: false
                        },
                        data: [
                            {mark:'outter',value: 1, name: '质量',color:'#b61414',tip:1},
                            {mark:'outter',value: 1, name: '设备',color:'#0ba0ea',tip:2},
                            {mark:'outter',value: 1, name: '工艺',color:'#9914e6',tip:3},
                            {mark:'outter',value: 1, name: '物料',color:'#1aee48',tip:4},
                            {mark:'outter',value: 1, name: 'TBD',color:'#fbff23',tip:5},
                        ],
                        itemStyle:{
                            normal:{
                                label:{
                                    show:true,
                                    position:'inner',
                                    textStyle: { //图例文字的样式
                                        color: '#000',
                                        fontSize: 16
                                    },
                                },
                                color:function(params) {
                                    return params.data['color'];
                                }, 
                            }
                        }
                    }
                ]
            },
            //当前订单参数
            pLine:'GDI阀装配线',
            ordNo:'',
            ordNum:'',
            wlNo:'',
            wlDesc:'',
            downNum:0,
            failNum:0,
            currBGD:'',
            //创建报工单参数
            crtVis:false,
            crtForm:{
                workNo:'',
                wlNo:'',
                wlDesc:'',
                proceNo:0,
                proceName:'',
                fixNum:0,
                fixMan:'',
                pUnit:0,
                station:0,
                bgCate:0,
                crtMan:'',
                crtDate:new Date()
            },
            crtRule:{
                crtMan:[{required:true,message:'是谁创建的？',trigger:'blur'}],
                crtDate:[{required:true,message:'创建日期是必须的！',trigger:'blur'}],
            },
            //工单是否可创建
            creatable:false,
            //工单进程
            bgStage:0,
            //当前工单信息
            bgNo:'',
            bgcrtMan:'',
            bgcrtDate:'',
            bgproceNo:'',
            bgproceName:'',
            bgCate:0,
            bgfixNum:0,
            bgWunit:'',
            bgfixMan:'',
            //调试卡片参数
            testMan:'',
            Cgroup:'',
            testStart:'',
            //计时器参数
            Hr:0,
            Min:0,
            Sec:0,
            begin:false,
            counter:null,
            //调试弹窗可见性
            testVis:false,
            testForm:{
                workNo:'',
                testEtime:new Date(),
                testTime:0,
                testMan:'',
                tips:''
            },
            testRule:{
                testMan:[{required:true,max:9,message:'是谁调试的？',trigger:'blur'}],
            },
            //测试结束显示测试参数
            tStime:null,
            tEtime:null,
            tSman:'',
            tEman:'',
            duration:0,
            //生产参数
            prodVis:true,
            workMan:'',
            prodCgp:'',
            prodStime:'',
            workStime:'',
            //生产结果参数
            scworkMan:'',
            scBZ:'',
            scStart:'',
            scDate:'',
            //生产报工参数
            BgVis:false,//弹窗显影指示
            BgForm:{
                BgworkNo:'',
                BgproceNo:'',
                BgTime:new Date(),
                BgprodNum:null,
                BgpassNum:null, 
                BgRepassNum:null,
                BgFlawNum:null,
                BgLf:null,
                BgJf:null,
                BgDf:null,
                BgGf:null,
                BgDcl:null,
                BgFailNum:null
            },
            BgRule:{
                BgTime:[{required:true,message:'请输入报工日期！',trigger:'blur'}],
                BgprodNum:[{required:true,type:'number',message:'请输入数字！',trigger:'blur'}],
                BgpassNum:[{required:true,type:'number',message:'请输入数字！',trigger:'blur'}],
                BgRepassNum:[{required:true,type:'number',message:'请输入数字！',trigger:'blur'}],
                BgFlawNum:[{required:true,type:'number',message:'请输入数字！',trigger:'blur'}],
                BgLf:[{required:true,type:'number',message:'请输入数字！',trigger:'blur'}],
                BgJf:[{required:true,type:'number',message:'请输入数字！',trigger:'blur'}],
                BgDf:[{required:true,type:'number',message:'请输入数字！',trigger:'blur'}],
                BgGf:[{required:true,type:'number',message:'请输入数字！',trigger:'blur'}],
                BgDcl:[{required:true,type:'number',message:'请输入数字！',trigger:'blur'}],
                BgFailNum:[{required:true,type:'number',message:'请输入数字！',trigger:'blur'}]
            },
            pBgdownTime:'',
            pBgpassNum:null,
            pBgLf:null,
            pBgJf:null,
            pBgDf:null,
            pBgGf:null,
            pBgDcl:null,
            pBgfailNum:null,
            pBgRepassNum:null,
            //侧边按钮弹窗参数
           
            planVis:false,//计划停机弹窗显影指示
            //停机挡板
            wtherStop:false,
            //计划停机表单参数
            wtherFix:false,
            planForm:{
                workNo:'',
                planStype:'',
                planSstime:'',
                planSetime:'',
                planMan:''
            },
            planRule:{
                planSstime:[{required:true,message:'开始时间为必须项！',trigger:'blur'}],
                planSetime:[{required:true,message:'结束时间为必须项！',trigger:'blur'}],
                planMan:[{required:true,max:10,message:'停机填写人是谁？',trigger:'blur'}]
            },
            Stype:[{label:'1',value:'晨餐'},
                   {label:'2',value:'午餐'},
                   {label:'3',value:'午休'},
                   {label:'4',value:'晚餐'},
                   {label:'5',value:'晚间休息'},
                   {label:'6',value:'夜宵'},
                   {label:'7',value:'周TMP'},
                   {label:'8',value:'培训'},
                   {label:'9',value:'前道工序库存不足'},
                   {label:'10',value:'会议'},
                   {label:'11',value:'IC线换料'}],
            unplanVis:false,//非计划停机弹窗显影指示
            unplanForm:{
                workNo:'',
                unplanStype:'非计划停机',
                unplanStypem:'',
                unplanSDesc:'',
                unplanSqNo:'',
                unplanSstime:'',
                unplanMan:''
            },
            unplanRule:{
                unplanSstime:[{required:true,message:'开始时间为必须项！',trigger:'blur'}],
                unplanStypem:[{required:true,message:'停机小类为必须项！',trigger:'blur'}],
                unplanSqNo:[{required:true,message:'停机设备编号是多少？',trigger:'blur'}],
                planMan:[{required:true,max:10,message:'停机填写人是谁？',trigger:'blur'}]
            },
            unStypem:[{label:'1',value:'管理停机'},
                      {label:'2',value:'物料停机'},
                      {label:'3',value:'工艺停机'},
                    //   {label:'4',value:'质量停机'},
                      {label:'5',value:'设备停机'},
                      {label:'6',value:'缺料停机'},
                      {label:'7',value:'其他停机'}],
            sInfoVis:false,//停机信息弹窗显影指示
            Stab:[], 
            //停机开始时停机挡板显示的参数
            pauseType:'',
            pauseDesc:'',
            pauseTime:'',
            pauseMan:'',
            //不良品表单参数
            blpVis:false,//不良品记录弹窗显影指示
            blpForm:{
                //part1
                blpworkNo:'',
                blpPline:'',
                blpDpart:'',
                blpMwlNo:'',
                blpMan:'',//填写人
                blpCgroup:'',
                blpMwldesc:'',
                blpDate:'',
                blpbgNo:'',
                blpProceNo:null,
                blpProceDesc:'',
                //part2
                blpIDNo:null,
                blpZwlNo:'',
                blpZwlDesc:'',
                blpNum:null,
                blpPos:'',//工位
                blpFDesc:'',//不良描述
                blpRPsNum:null,
                blpPreason:'',
                blpTip:''
            },
            blpRule:{
                //part1
                blpworkNo:[{required:true,message:'作业单号不能为空！',trigger:'blur'}],
                blpPline:[{required:true,message:'生产线不能为空！',trigger:'blur'}],
                blpDpart:[{required:true,message:'哪个部门发现的？',trigger:'blur'}],
                blpMwlNo:[{required:true,message:'物料编号不能为空！',trigger:'blur'}],
                blpMan:[{required:true,message:'谁填写的？',trigger:'blur'}],//填写人
                blpCgroup:[{required:true,message:'那个班组发现的？',trigger:'blur'}],
                blpMwldesc:[{required:true,message:'物料描述是什么？',trigger:'blur'}],
                blpDate:[{required:true,message:'填写日期是什么时候？',trigger:'blur'}],
                blpbgNo:[{required:true,message:'报工编号不能为空！',trigger:'blur'}],
                blpProceNo:[{required:true,message:'工序号不能为空！',trigger:'blur'}],
                blpProceDesc:[{required:true,message:'工序描述是什么？',trigger:'blur'}],
                //part2
                blpIDNo:[{required:true,message:'序号不能为空！',trigger:'blur'}],
                blpZwlNo:[{required:true,message:'物料编号不能为空！',trigger:'blur'}],
                blpZwlDesc:[{required:true,message:'物料描述不能为空！',trigger:'blur'}],
                blpNum:[{required:true,message:'数量是是多少？',trigger:'blur'}],
                blpPos:[{required:true,message:'哪个工位？',trigger:'blur'}],//工位
                blpFDesc:[{required:true,message:'简短描述下呗！',trigger:'blur'}],//不良描述
                blpRPsNum:[{required:true,message:'此处不能为空！',trigger:'blur'}],
                blpPreason:[{required:true,message:'(!*_*!)为啥就过了？',trigger:'blur'}]
            },
            blpTab:[],
            //保存键显影指示
            storeVis:true,
            //已赋值的输入框不再可用控制器
            inputDis:false,
            //饼状图点击区域识别标签
            whr:'',
            //新安灯记录弹窗参数
            newAndonVis:false,
            //安灯记录弹窗参数
            andonLogVis:false,
            //质量安灯的表单显示,0为质量，1为设备，2为其他三种按灯
            zlVis:0,
            //质量安灯表格参数
            zlForm:{
                no:1,
                zlworkNo:'',
                zlMan:'',
                zlDpt:'',
                zlCls:'',
                zlNum:null,
                zlwlNo:'',
                zlwlDesc:'',
                zlPos:'',
                zlQdesc:'',
                zlRPsNum:null,
                zlsbNo:'',
                zlRea:'',
                zlTip:'',
                zlAndonMan:'',
                // zlwthrStop:'',
            },
            zlRule:{
                zlMan:[{required:true,message:'不良品记录人不能为空！',trigger:'blur'}],
                zlDpt:[{required:true,message:'发现部门不能为空！',trigger:'blur'}],
                zlCls:[{required:true,message:'发现班组不能为空！',trigger:'blur'}],
                zlNum:[{required:true,message:'此处不能为空！',trigger:'blur'}],
                zlwlNo:[{required:true,message:'物料编号不能为空！',trigger:'blur'}],
                zlwlDesc:[{required:true,message:'物料描述不能为空！',trigger:'blur'}],
                zlPos:[{required:true,message:'工位不能为空！',trigger:'blur'}],
                zlRPsNum:[{required:true,message:'返工合格数量？',trigger:'blur'}],
                zlsbNo:[{required:true,message:'设备编号不能为空！',trigger:'blur'}],
                zlAndonMan:[{required:true,message:'安灯人是谁？',trigger:'blur'}],
            },
            //如已存在不合格记录，则不用填写部分信息
            zlInpVis:false,
            //设备按灯表
            priority:[{label:0,value:'特急'},
                      {label:1,value:'急'},
                      {label:2,value:'一般'},
                      {label:3,value:'不急'}],
            sbP:[{label:0,value:'未停线'},
                 {label:0,value:'已停线'}],
            sbForm:{
                sbworkNo:'',
                sbStop:'',
                sbYxj:'',
                sbStation:'',
                sbDpt:'',
                sbMan:'',
                sbNo:'',
                sbName:'',
                sbQpart:'',
                sbQtype:'',
                sbQdesc:'',
                sbSafe:''
            },
            sbRule:{
                sbYxj:[{required:true,message:'该报修记录的优先级是？',trigger:'blur'}],
                sbDpt:[{required:true,message:'报修部门不能为空！',trigger:'blur'}],
                sbMan:[{required:true,message:'报修人员不能为空！',trigger:'blur'}],
                sbNo:[{required:true,message:'设备编号不能为空！',trigger:'blur'}],
                sbQpart:[{required:true,message:'哪里坏了？',trigger:'blur'}],
                sbQtype:[{required:true,message:'报修类型是？',trigger:'blur'}],
            },
            sbInputVis:false,//如果在停机状态按灯，这停机状态不可更改
            //除质量和设备以外的其他按灯记录表
            otherForm:{
                othworkNo:'',
                othAndonT:'',
                othStation:'',
                othStop:'',
                othsbNo:'',
                othMan:'',
                othQdesc:''
            },
            otherRule:{
                othMan:[{required:true,message:'按灯人员不能为空！',trigger:'blur'}],
                othsbNo:[{required:true,message:'设备编号不能为空！',trigger:'blur'}],
                othQdesc:[{required:true,message:'请稍微描述一下！',trigger:'blur'}],
            },
            othInputVis:false,//如果在停机状态按灯，这停机状态不可更改
            //如为质量停机，则不可主动开始
            wtherZLP:false,
            //如果有按灯正在进行，则不能再新建按灯
            wtherAD:false,
            //按灯记录参数
            currAdT:'',
            currAdSts:'',
            currAdNo:'',
            currAdtime:'',
            currAdDesc:'',
            currAdMan:'',
            //按灯记录表格
            Adtab:[],
            //安灯状态指示
            AdLife:3,


        }
    },
    methods:{
         //工具方程，日期转字符串
        dateToString(date){  
            var  year = date.getFullYear();  
            var  month =(date.getMonth() + 1).toString();  
            var  day = (date.getDate()).toString();   
            var hour = (date.getHours()).toString();
            var min = (date.getMinutes()).toString();
            var sec = (date.getSeconds()).toString();
            if(month.length == 1){  
                month = "0" + month;  
            }  
            if(day.length == 1){  
                day = "0" + day;  
            } 
            if(hour.length == 1){  
                hour = "0" + hour;  
            }  
            if(min.length == 1){  
                min = "0" + min;  
            } 
            if(sec.length == 1){  
                sec = "0" + sec;  
            }  

            var dateTime = year + "-" + month + "-" + day+" "+hour+":"+min+":"+sec; 
            return dateTime;  
        },
        create(row){
            fetch('api/WorkReport/BGDInfo',{
                method:'POST',
                headers:{
                    'Content-Type':'application/json'
                },
                body:JSON.stringify({
                    workNo:row.workNo
                })
            })
            .then(response=>response.json())
            .then(data=>{
                this.crtForm.workNo = data[0].作业单号;
                this.crtForm.wlNo = data[0].物料编号;
                this.crtForm.wlDesc = data[0].物料描述;
                this.crtForm.proceNo = data[0].工序号
                this.crtForm.proceName = data[0].工序名称
                this.crtForm.fixNum = data[0].定额件数;
                this.crtForm.fixMan = data[0].定额人数;
                this.crtForm.pUnit = data[0].生产单元;
                this.crtForm.station = data[0].工作组;
                this.crtForm.bgCate = data[0].报工类别
                this.crtVis = true
                this.getOrder();
            })
            .catch(data=>{
                alert(data)
            })
            
        },
        close(row){
            console.log(row);
        },
        //创建弹窗叉叉按钮
        crtClose(done){
             this.$confirm('确认关闭？')
            .then(_ => {
                this.$refs['crtForm'].resetFields();
                done();
            })
            .catch(_ => {});
        },
        //创建报工单取消按钮
        crtCancel(){
            this.crtVis = false;
            this.$refs['crtForm'].resetFields();
        },
        //创建报工单确认按钮
        crtconfirm(formname){
            this.$refs[formname].validate((valid) => {
                if (valid) {
                    if(localStorage.getItem("ms_username") == "Sean" || localStorage.getItem("ms_username") == "Chuck Yu" || localStorage.getItem("ms_username") == "eying" || localStorage.getItem("ms_username") == "sophia" || localStorage.getItem("ms_username") == "oliver" || localStorage.getItem("ms_username") == "Aron"){
                        fetch('api/WorkReport/crtBGD',{
                            method:'POST',
                            headers:{
                                'Content-Type':'application/json'
                            },
                            body:JSON.stringify({
                                workNo:this.crtForm.workNo,
                                crtMan:this.crtForm.crtMan,
                                crtDate:this.crtForm.crtDate
                            })
                        })
                        .then(response=>response.json())
                        .then(data=>{
                            if(data[0].resSign){
                                this.$message.success("创建成功")
                                this.getBGD()
                                this.getOrder()
                            }else{
                                this.$message.error("创建失败,报工单已存在")
                            }
                        }).catch(data=>{
                            alert(data)
                        })
                        this.crtVis = false;
                        this.$refs[formname].resetFields();
                    }
                }else{
                    console.log('error submit!!');
                    return false;
                }
            })
        },
        //初始页面加载报工单信息，如无信息，则可以创建报工单
        getBGD(){
            fetch('api/WorkReport/BGDstart',{
                method:'POST',
                headers:{
                    'Content-Type':'application/json'
                },
                body:JSON.stringify({
                    workunit:this.pLine,
                    // workNo:this.ordNo
                })
            })
            .then(response=>response.json())
            .then(data=>{
                console.log("长度"+data.length)
                console.log(this.creatable)
                console.log(data)
                if(data.length != 0){
                    if(data[0].sMan != null){
                        this.wtherStop = true;
                        this.pauseType = data[0].sType;
                        this.pauseDesc = data[0].sDesc;
                        this.pauseMan = data[0].sMan;
                        if(this.pauseType!="计划停机" || this.pauseType=="非计划停机"){
                            this.wtherZLP = true
                        }
                    }
                    this.creatable = true;
                    this.bgNo = data[0].bgNo
                    this.bgcrtMan = data[0].initMan
                    this.bgcrtDate = (data[0].crtDate).substr(0,10)
                    this.bgproceNo = data[0].proceNo
                    this.bgproceName = data[0].proceName
                    this.bgfixNum = data[0].fixNum
                    this.bgWunit = data[0].workUnit
                    this.bgfixMan = data[0].fixMan
                    if(data[0].wgTime != null){
                        console.log("报工完成");
                        this.pBgdownTime =(data[0].wgTime).substr(0,10);
                        this.pBgpassNum = data[0].passNum;
                        this.pBgLf = data[0].lf;
                        this.pBgJf = data[0].jf;
                        this.pBgDf = data[0].df;
                        this.pBgGf = data[0].gf;
                        this.pBgDcl = data[0].dcl;
                        this.pBgfailNum = data[0].failNum;
                        this.pBgRepassNum = data[0].rePassNum;
                         //调试数据
                        this.tStime = data[0].tsTime
                        this.tEtime = data[0].teTime
                        this.tSman = data[0].tsMan
                        this.tEman = data[0].teMan
                        this.duration = data[0].duration
                        //生产数据
                        this.scworkMan = data[0].workMan
                        this.scBZ = data[0].workBZ
                        this.scDate = data[0].prodDate.substr(0,10)
                        this.scStart = data[0].workStime
                        //阶段指示
                        this.bgStage = 5;
                    }else{
                        if(data[0].workMan != null){
                            console.log('生产已经开始');
                            //调试数据
                            this.tStime = data[0].tsTime
                            this.tEtime = data[0].teTime
                            this.tSman = data[0].tsMan
                            this.tEman = data[0].teMan
                            this.duration = data[0].duration
                            //生产数据
                            this.scworkMan = data[0].workMan
                            this.scBZ = data[0].workBZ
                            this.scDate = data[0].prodDate
                            this.scStart = data[0].workStime
                            //阶段指示
                            this.bgStage = 4;
                        }else if(data[0].duration != 0){
                            console.log("调试了")
                            this.tStime = data[0].tsTime
                            this.tEtime = data[0].teTime
                            this.tSman = data[0].tsMan
                            this.tEman = data[0].teMan
                            this.duration = data[0].duration
                            this.bgStage = 3;
                        }else{
                            console.log("没有调试")
                            this.bgStage = 1
                        }
                    }
                   
                }
                 console.log(this.creatable)
            }).catch(data=>{
                alert(data)
            })
        },
        getOrder(){
            //当前订单数据加载
            fetch('api/WorkReport/currOrd',{
                method:'POST',
                headers:{
                    'Content-Type':'application/json'
                },
                body:JSON.stringify({
                    prodLine:this.pLine
                })
            }).then(response=>response.json())
            .then(data=>{
                if(data.length != 0){
                    this.ordNo = data[1].作业单号
                    this.ordNum = data[1].订单数量
                    this.wlNo = data[1].物料编号
                    this.wlDesc = data[1].物料描述
                    this.downNum = data[1].完工数量
                    this.failNum = data[1].不合格数量
                    this.currBGD = data[1].报工编号
                    this.pct = (data[1].合格数量/data[1].订单数量)*100
                    // this.getBGDtest();
                }else{
                    this.ordNo = ''
                    this.ordNum = 0
                    this.wlNo = ''
                    this.wlDesc = ''
                    this.downNum = 0
                    this.failNum = 0
                    this.currBGD = ''
                    this.pct = 0
                }
            
            }).catch(data=>{
                alert(data);
            })
        },
        //调试方程
        Ts(){
            this.bgStage = 2
        },
        //生产方程
        Sc(){
            this.prodVis = false;
        },
        Bg(){
            this.BgVis = true;
            this.BgForm.BgworkNo = this.ordNo
            this.BgForm.BgproceNo = this.bgproceNo
        },
        //生产开始确认
        prodComfirm(){
            if(this.workMan==''){
                this.$message.error("生产是谁开始的？")
                return;
            }
            if(this.prodCgp==''){
                this.$message.error("生产班组属于？")
                return;
            }
            if(this.prodStime==''){
                this.$message.error("生产日期是？")
                return;
            }
             if(this.workStime==''){
                this.$message.error("生产何时开始的？")
                return;
            }
            fetch('api/WorkReport/BGDprod',{
                method:'POST',
                headers:{
                    'Content-Type':'application/json'
                },
                body:JSON.stringify({
                    workNo:this.ordNo,
                    workMan:this.workMan,
                    cgroup:this.prodCgp,
                    workStime:this.workStime,
                    prodStime:this.prodStime
                })
            }).then(response=>response.json())
            .then(data=>{
                if(data[0].resSign){
                    this.$message.success("生产已开始！")
                    this.scworkMan = this.workMan;
                    this.scBZ = this.prodCgp;
                    this.scStart = dateToString(this.workStime);
                    this.scDate = dateToString(this.prodStime);
                    this.workMan = '';
                    this.prodCgp = '';
                    this.workStime = '';
                    this.prodStime = '';
                    this.bgStage = 4;
                }else{
                    this.$message.error("开始失败！");
                }
            })
        },
        tBegin(){
            this.begin = !this.begin;
            if(this.counter != null){
                 clearInterval(this.counter);
            }
            this.counter = setInterval(() => {
                if(this.begin){
                    if(this.Sec == 59){
                        this.Sec = 0;
                        if(this.Min == 59){
                            this.Min = 0;
                            this.Hr+=1;
                        }else{
                            this.Min+=1;
                        }
                    }else{
                        this.Sec+=1;
                    } 
                }
            },1000);
        },
        tStop(){
            if(this.testMan == ''){
                this.$message.error("调试是谁开始的？")
                return;
            }
            if(this.Cgroup == ''){
                this.$message.error("报工单所属班组是？")
                return;
            }
            if(this.testStart == ''){
                this.$message.error("调试是什么时候开始的？")
                return;
            }
            if(this.Hr != 0 || this.Min != 0 || this.Sec != 0){
                this.testForm.workNo = this.ordNo;
                this.testForm.testEtime = new Date();
                this.testForm.testTime = this.Hr*60 + this.Min + this.Sec/60;
                this.begin = false;
                clearInterval(this.counter);
                this.testVis = true
                this.Sec = 0;
                this.Min = 0;
                this.Hr = 0;
            }
        },
        //调试弹窗函数
        testComfirm(formName){
            this.$refs[formName].validate((valid) => {
                if (valid) {
                    if(localStorage.getItem("ms_username") == "Sean" || localStorage.getItem("ms_username") == "Chuck Yu" || localStorage.getItem("ms_username") == "eying" || localStorage.getItem("ms_username") == "sophia" || localStorage.getItem("ms_username") == "oliver" || localStorage.getItem("ms_username") == "Aron"){
                        fetch('api/WorkReport/BGDtest',{
                            method:'POST',
                            headers:{
                                'Content-Type':'application/json'
                            },
                            body:JSON.stringify({
                                prodline:this.pLine,
                                workNo:this.testForm.workNo,
                                sTime:this.testStart,
                                eTime:this.testForm.testEtime,
                                testTime:this.testForm.testTime,
                                sMan:this.testMan,
                                eMan:this.testForm.testMan,
                                tip:this.testForm.tips,
                                cgroup:this.Cgroup
                            })
                        }).then(response=>response.json())
                        .then(data=>{
                            console.log(data)
                            if(data[0].resSign){
                                this.$message.success("调试已结束！");
                                this.testVis=false;
                                this.tStime = this.dateToString(this.testStart);
                                this.tEtime = this.dateToString(this.testForm.testEtime);
                                this.tSman = this.testMan;
                                this.tEman = this.testForm.testMan;
                                this.duration = this.testForm.testTime;
                                this.testMan = '',
                                this.Cgroup = '',
                                this.testStart = null,
                                this.$refs[formName].resetFields();
                                this.bgStage = 3;
                                console.log(this.tStime)
                                console.log(this.tEtime)
                            }else{
                                this.$message.warning("该报工单已存在调试记录，现在还不能撤销调试操作！")
                            }
                        }).catch(data=>{
                            alert(data)
                        })
                    }else{
                        this.$message.error("你没有权限");
                    }
                }else{
                    console.log('error submit!!');
                    return false;
                }
            })
        },
        testCancel(){
            this.testVis=false;
            this.$refs['testForm'].resetFields();
        },
        testClose(done){
            this.$confirm('确认关闭？')
            .then(_ => {
                this.$refs['testForm'].resetFields();
                done();
            })
            .catch(_ => {});
        },
        //报工弹窗函数
        BgComfirm(formName){
            this.$refs[formName].validate((valid) => {
                if (valid) {
                    if(localStorage.getItem("ms_username") == "Sean" || localStorage.getItem("ms_username") == "Chuck Yu" || localStorage.getItem("ms_username") == "eying" || localStorage.getItem("ms_username") == "sophia" || localStorage.getItem("ms_username") == "oliver" || localStorage.getItem("ms_username") == "Aron"){
                        fetch('api/WorkReport/ProdBG',{
                            method:'POST',
                            headers:{
                                'Content-Type':'application/json'
                            },
                            body:JSON.stringify({
                                workNo:this.BgForm.BgworkNo,
                                bgTime:this.BgForm.BgTime,
                                passNum:this.BgForm.BgpassNum,
                                Lf:this.BgForm.BgLf,
                                Jf:this.BgForm.BgJf,
                                Df:this.BgForm.BgDf,
                                Gf:this.BgForm.BgGf,
                                Dcl:this.BgForm.BgDcl,
                                failNum:this.BgForm.BgFailNum,
                                rePassNum:this.BgForm.BgRepassNum,
                                pManNum:this.BgForm.BgprodNum,
                                proceNo:this.BgForm.BgproceNo

                            })
                        }).then(response=>response.json())
                        .then(data=>{
                            if(data[0].resSign){
                                this.$message.success("报工完成！");
                                this.pBgdownTime = this.dateToString(this.BgForm.BgTime);
                                this.pBgpassNum = this.BgForm.BgpassNum;
                                this.pBgLf = this.BgForm.BgLf;
                                this.pBgJf = this.BgForm.BgJf;
                                this.pBgDf = this.BgForm.BgDf;
                                this.pBgGf = this.BgForm.BgGf;
                                this.pBgDcl = this.BgForm.BgDcl;
                                this.pBgfailNum = this.BgForm.BgFailNum;
                                this.pBgRepassNum = this.BgForm.BgRepassNum;
                                this.$refs['BgForm'].resetFields();
                                this.BgVis = false;
                                this.bgStage = 5;
                            }else{
                                this.$message.error("报工单不存在或者该报工单不属于订单报工！请核实");
                            }
                        })
                       
                    }else{
                        this.$message.error("你没有权限！")
                    }
                }else{
                    console.log('error submit!!');
                    return false;
                }
            })
           
        },
        BgCancel(){
            this.$refs['BgForm'].resetFields();
            this.BgVis = false;
        },
        BgClose(done){
            this.$confirm('确认关闭？')
            .then(_ => {
                this.$refs['BgForm'].resetFields();
                done();
            })
            .catch(_ => {});
        },
        //关闭报工单操作
        BgComplete(){
            fetch('api/WorkReport/BGClose',{
                method:'POST',
                headers:{
                    'Content-Type':'application/json'
                },
                body:JSON.stringify({
                    workNo:this.bgNo
                })
            }).then(response=>response.json())
            .then(data=>{
                if(data[0].resSign){
                    this.$message.success("报工结束！")
                    this.creatable = false;
                    this.bgStage = 0;
                    this.getOrder()
                }else{
                    this.$message.error("出现了意外的情况！")
                }
            })
        },
        //计划停机弹窗参数
        planS(){
            if(this.bgStage!=0){
                this.planVis=true
                this.planForm.workNo = this.ordNo
            }
           
        },
        planComfirm(formName){
            this.$refs[formName].validate((valid) => {
                if (valid) {
                    if(localStorage.getItem("ms_username") == "Sean" || localStorage.getItem("ms_username") == "Chuck Yu" || localStorage.getItem("ms_username") == "eying" || localStorage.getItem("ms_username") == "sophia" || localStorage.getItem("ms_username") == "oliver" || localStorage.getItem("ms_username") == "Aron"){
                        fetch('api/WorkReport/planStop',{
                            method:'POST',
                            headers:{
                                'Content-Type':'application/json'
                            },
                            body:JSON.stringify({
                                workNo:this.planForm.workNo,
                                stopType:this.planForm.planStype,
                                stopBegin:this.planForm.planSstime,
                                stopEnd:this.planForm.planSetime,
                                sMan:this.planForm.planMan
                            })
                        }).then(response=>response.json())
                        .then(data=>{
                            if(data[0].resSign){
                                this.pauseType = "计划停机";
                                this.pauseDesc = this.planForm.planStype;
                                this.pauseMan = this.planForm.planMan;
                                this.$refs['planForm'].resetFields();
                                this.planVis = false;
                                this.wtherStop = true;
                                this.$message.success("计划停机记录成功！")
                            }else{
                                this.$message.error("报工单不存在，请核实")
                            }
                        }).catch(data=>{
                            alert(data);
                        })
                    }else{
                        this.$message.error("你没有权限")
                    }
                }else{
                    return false;
                }
            })
            
        },
        planCancel(){
            this.$refs['planForm'].resetFields();
            this.planVis = false;
        },
        planClose(done){
             this.$confirm('确认关闭？')
            .then(_ => {
                this.$refs['planForm'].resetFields();
                done();
            })
            .catch(_ => {});
        },
        //非计划停机弹窗参数
        unplanS(){
            if(this.bgStage!=0){
                this.unplanVis=true
                this.unplanForm.workNo = this.ordNo
            }
           
        },
        unplanComfirm(formName){
            this.$refs[formName].validate((valid) => {
                if (valid) {
                    if(localStorage.getItem("ms_username") == "Sean" || localStorage.getItem("ms_username") == "Chuck Yu" || localStorage.getItem("ms_username") == "eying" || localStorage.getItem("ms_username") == "sophia" || localStorage.getItem("ms_username") == "oliver" || localStorage.getItem("ms_username") == "Aron"){
                        fetch('api/WorkReport/unPlanStop',{
                            method:'POST',
                            headers:{
                                'Content-Type':'application/json'
                            },
                            body:JSON.stringify({
                                workNo:this.unplanForm.workNo,
                                stopType:this.unplanForm.unplanStype,
                                stopsType:this.unplanForm.unplanStypem,
                                sbNo:this.unplanForm.unplanSqNo,
                                stopDesc:this.unplanForm.unplanSDesc,
                                sMan:this.unplanForm.unplanMan,
                                stopBegin:this.unplanForm.unplanSstime
                            })
                        }).then(response=>response.json())
                        .then(data=>{
                            if(data[0].resSign){
                                this.$message.success("已停机！")
                                this.pauseType = this.unplanForm.unplanStype;
                                this.pauseDesc = this.unplanForm.unplanSDesc;
                                this.pauseMan = this.unplanForm.unplanMan;
                                this.$refs['unplanForm'].resetFields();
                                this.unplanVis = false;
                                this.wtherStop = true;
                            }else{
                                this.$message.error("需要关闭的报工单不存在，请核实！")
                            }
                        })
                      
                    }else{
                        this.$message.error("你没有权限")
                    }
                }else{
                    return false;
                }
            })
            
        },
        unplanCancel(){
            this.$refs['unplanForm'].resetFields();
            this.unplanVis = false;
        },
        unplanClose(done){
            this.$confirm('确认关闭？')
            .then(_ => {
                this.$refs['unplanForm'].resetFields();
                done();
            })
            .catch(_ => {});
        },
        //停机信息表格查看
        stopLog(){
            if(this.bgStage==0){
                return;
            }
            this.sInfoVis = true
            fetch('api/WorkReport/stopLog',{
                method:'POST',
                headers:{
                    'Content-Type':'application/json'
                },
                body:JSON.stringify({
                    workNo:this.ordNo
                })
            }).then(response=>response.json())
            .then(data=>{
                this.Stab = []
                if(data.length != 0){
                    for(var item of data){
                        var tmp = {
                            sType:item.停机类型,
                            sDesc:item.停机描述,
                            sStart:item.停机开始,
                            sEnd:item.停机结束,
                            testPause:item.调试暂停,
                            sMan:item.填写人,
                            sTime:item.停机时间,
                            sqNo:item.设备编号,
                            status:item.状态
                        }
                        this.Stab.push(tmp)
                    }
                }else{
                    this.$message.warning("没有停机记录！")
                }
            }).catch(data=>{
                alert(data)
            })
        },
        //停机类型改变
        typeC(){
            this.planForm.planSstime = '';
            this.planForm.planSetime = '';
            this.wtherFix = false;

        },
        //计划停机时选择完开始时间后定义结束时间
        defEndT(){
            if(!(this.planForm.planStype =="培训" || this.planForm.planStype =="会议" || this.planForm.planStype =="前道工序库存不足" || this.planForm.planStype =="IC线换料")){
                this.wtherFix = true
                var n = 0
                if(this.planForm.planStype =="午休"|| this.planForm.planStype =="晚间休息"){
                    n = 15
                }else if(this.planForm.planStype =="午餐" || this.planForm.planStype =="晚餐" || this.planForm.planStype =="夜宵" || this.planForm.planStype =="周TMP"){
                    n = 30
                }else if(this.planForm.planStype =="晨餐"){
                    n = 45
                }
                console.log(n);
                const tmpT = new Date(this.planForm.planSstime)
                // console.log(new Date(tmpT.setMinutes(tmpT.getMinutes()+n)))
                this.planForm.planSetime = new Date(tmpT.setMinutes(tmpT.getMinutes()+n));
            }else{
                this.wtherFix = false;
            }
        },
        prodContinue(){
            fetch('api/WorkReport/pContinue',{
                method:'POST',
                headers:{
                    'Content-Type':'application/json'
                },
                body:JSON.stringify({
                    workNo:this.ordNo
                })
            }).then(response=>response.json())
            .then(data=>{
                if(data[0].resSign){
                    this.pauseType = '';
                    this.pauseDesc = '';
                    this.pauseMan = '';
                    this.wtherStop = false;
                    this.$message.success("生产继续")
                }else{
                    this.$message.error("停机失败")
                }
            }).catch(data=>{
                alert(data)
            }) 
        },
        //不良品记录按钮点击
        blpClick(){
            if(this.bgStage!=0){
                this.blpVis=true;
                fetch('api/WorkReport/BLPopen',{
                    method:'POST',
                    headers:{
                        'Content-Type':'application/json'
                    },
                    body:JSON.stringify({
                        workNo:this.ordNo
                    })
                }).then(response=>response.json())
                .then(data=>{
                    if(data.length!=0){
                        this.blpForm.blpworkNo = data[0].作业单号
                        this.blpForm.blpMwlNo = data[0].物料编号
                        this.blpForm.blpMwldesc = data[0].物料描述
                        this.blpForm.blpPline = data[0].生产线
                        this.blpForm.blpMan = data[0].填写人
                        this.blpForm.blpDate = data[0].填写日期
                        this.blpForm.blpDpart = data[0].发现部门
                        this.blpForm.blpCgroup = data[0].发现班组
                        this.blpForm.blpbgNo = data[0].bgNo
                        this.blpForm.blpProceNo = data[0].proceNo
                        this.blpForm.blpProceDesc = data[0].proceName
                        this.blpForm.blpIDNo = data[0].blpId
                        //保存按钮
                        this.storeVis = false;
                        //已赋值的输入框不再可用
                        this.inputDis = true;
                        this.renewlTab()
                    }else{
                        //所有输入框都可用
                        this.inputDis = false;
                        this.blpForm.blpworkNo = this.ordNo
                        this.blpForm.blpbgNo = this.bgNo
                        this.blpForm.blpProceNo = this.bgproceNo
                        this.blpForm.blpProceDesc = this.bgproceName
                        this.blpForm.blpMwlNo = this.wlNo
                        this.blpForm.blpMwldesc = this.wlDesc
                        this.blpForm.blpPline = this.pLine
                        //确认按钮
                        this.storeVis = true;
                    }
                }).catch(data=>{
                    alert(data)
                })
                
            }
        },
        //不良品弹窗函数，保存按钮函数
        blpStore(){
            this.$refs['blpForm'].validate((valid) => {
                if (valid) {
                    if(localStorage.getItem("ms_username") == "Sean" || localStorage.getItem("ms_username") == "Chuck Yu" || localStorage.getItem("ms_username") == "eying" || localStorage.getItem("ms_username") == "sophia" || localStorage.getItem("ms_username") == "oliver" || localStorage.getItem("ms_username") == "Aron"){
                        fetch('api/WorkReport/BLPstore',{
                            method:'POST',
                            headers:{
                                'Content-Type':'application/json'
                            },
                            body:JSON.stringify({
                                workNo:this.blpForm.blpworkNo,
                                wlNo:this.blpForm.blpMwlNo,
                                wlDesc:this.blpForm.blpMwldesc,
                                pLine:this.blpForm.blpPline,
                                man:this.blpForm.blpMan,
                                date:this.blpForm.blpDate ,
                                Dpart:this.blpForm.blpDpart,
                                Cgroup:this.blpForm.blpCgroup,
                                bgNo:this.blpForm.blpbgNo,
                                tabID:this.blpForm.blpIDNo,
                                tabwlNo:this.blpForm.blpZwlNo,
                                tabwlDesc:this.blpForm.blpZwlDesc,
                                tabNum:this.blpForm.blpNum,
                                tabPos:this.blpForm.blpPos,
                                tabFDesc:this.blpForm.blpFDesc,
                                tabRPsNum:this.blpForm.blpRPsNum,
                                tabPreason:this.blpForm.blpPreason,
                                tabTip:this.blpForm.blpTip
                            })
                        }).then(response=>response.json())
                        .then(data=>{
                            if(data[0].resSign){
                                // this.$refs['blpForm'].resetFields();
                                // this.blpVis = false;
                                this.storeVis = false;
                                this.inputDis = true;
                                this.renewlTab();
                            }else{
                                this.$message.error("找不到作业单对应的报工单！")
                            }
                        })
                        
                    }else{
                        this.$message.error("你没有权限")
                    }
                }else{
                    return false;
                }
            })
            
        },
        //不良品确认按钮
        blpComfirm(){
            this.$refs['blpForm'].resetFields();
            this.blpTab=[]
            this.blpVis = false;
        },
        //不良品添加记录
        blpAdd(){
            this.$refs['blpForm'].validate((valid) => {
                if (valid) {
                    if(localStorage.getItem("ms_username") == "Sean" || localStorage.getItem("ms_username") == "Chuck Yu" || localStorage.getItem("ms_username") == "eying" || localStorage.getItem("ms_username") == "sophia" || localStorage.getItem("ms_username") == "oliver" || localStorage.getItem("ms_username") == "Aron"){
                        fetch('api/WorkReport/BLPAdd',{
                            method:'POST',
                            headers:{
                                'Content-Type':'application/json'
                            },
                            body:JSON.stringify({
                                workNo:this.blpForm.blpworkNo,
                                tabID:this.blpForm.blpIDNo,
                                tabwlNo:this.blpForm.blpZwlNo,
                                tabwlDesc:this.blpForm.blpZwlDesc,
                                tabNum:this.blpForm.blpNum,
                                tabPos:this.blpForm.blpPos,
                                tabFDesc:this.blpForm.blpFDesc,
                                tabRPsNum:this.blpForm.blpRPsNum,
                                tabPreason:this.blpForm.blpPreason,
                                tabTip:this.blpForm.blpTip
                            })
                        }).then(response=>response.json())
                        .then(data=>{
                            if(data[0].resSign){
                                this.$message.success("添加成功！")
                                this.renewlTab();
                                this.blpForm.blpIDNo+=1;
                                this.blpForm.blpZwlNo = ''
                                this.blpForm.blpZwlDesc = ''
                                this.blpForm.blpNum = null
                                this.blpForm.blpPos = ''
                                this.blpForm.blpFDesc=''
                                this.blpForm.blpRPsNum=null
                                this.blpForm.blpPreason=''
                                this.blpForm.blpTip=''
                            }else{
                                this.$message.error("添加失败")
                            }
                        })
                    }else{
                        this.$message.error("你没有权限")
                    }
                }else{
                    return false;
                }
            })

        },
        blpClose(done){
            this.$confirm('确认关闭？')
            .then(_ => {
                this.$refs['blpForm'].resetFields();
                this.blpTab=[]
                done();
            })
            .catch(_ => {});
        },
        renewlTab(){
            fetch('api/WorkReport/getTab',{
                method:'POST',
                headers:{
                    'Content-Type':'application/json'
                },
                body:JSON.stringify({
                    workNo:this.blpForm.blpworkNo
                })
            }).then(response=>response.json())
            .then(data=>{
                this.blpTab = []
                for(var item of data){
                    var tmp = {
                        tabID:item.id,
                        tabwlNo:item.wlNo,
                        tabwlDesc:item.wlDesc,
                        tabNum:item.failNum,
                        tabPos:item.position,
                        tabfDesc:item.failDesc,
                        tabRpsNum:item.repassNum,
                        tabRsn:item.reason,
                        tabTip:item.tips
                    }
                    this.blpTab.push(tmp)
                }
            }).catch(data=>{
                alert(data)
            })
        },
        //质量安灯开启
        zlOpen(){
             fetch('api/WorkReport/zlAndonOpen',{
                method:'POST',
                headers:{
                    'Content-Type':'application/json'
                },
                body:JSON.stringify({
                    workNo:this.ordNo
                })
            }).then(response=>response.json())
            .then(data=>{
                if(data.length!=0){
                    this.zlForm.no = data[0].序号
                    this.zlForm.zlDpt = data[0].发现部门
                    this.zlForm.zlCls = data[0].发现班组
                    this.zlForm.zlMan = data[0].填写人
                    this.zlInpVis = true;
                }else{
                    this.zlInpVis = false;

                }
            }).catch(data=>{
                alert(data)
            })
             
        },
        //质量安灯确认
        zlAndonCfm(){
            if(this.zlVis==0){
                this.$refs['zlForm'].validate((valid) => {
                    if (valid) {
                        if(localStorage.getItem("ms_username") == "Sean" || localStorage.getItem("ms_username") == "Chuck Yu" || localStorage.getItem("ms_username") == "eying" || localStorage.getItem("ms_username") == "sophia" || localStorage.getItem("ms_username") == "oliver" || localStorage.getItem("ms_username") == "Aron"){
                            fetch('api/WorkReport/zlAndonCfm',{
                                method:'POST',
                                headers:{
                                    'Content-Type':'application/json'
                                },
                                body:JSON.stringify({
                                    workNo:this.zlForm.zlworkNo,
                                    order:this.zlForm.no,
                                    fillMan:this.zlForm.zlMan,
                                    dpt:this.zlForm.zlDpt,
                                    cls:this.zlForm.zlCls,
                                    num:this.zlForm.zlNum,
                                    wlNo:this.zlForm.zlwlNo,
                                    wlDesc:this.zlForm.zlwlDesc,
                                    pos:this.zlForm.zlPos,
                                    qDesc:this.zlForm.zlQdesc,
                                    rePsnum:this.zlForm.zlRPsNum,
                                    sbNo:this.zlForm.zlsbNo,
                                    reason:this.zlForm.zlRea,
                                    tips:this.zlForm.zlTip,
                                    andonMan:this.zlForm.zlAndonMan,
                                })
                            }).then(response=>response.json())
                            .then(data=>{
                                if(data[0].resSign){
                                    this.$message.error("按灯成功，现在已停机待检");
                                    this.newAndonVis = false;
                                    //停机状态打开
                                    this.wtherStop = true;
                                    this.pauseType = "质量停机"
                                    this.pauseDesc = this.zlForm.zlQdesc;
                                    this.pauseMan = this.zlForm.zlAndonMan;
                                    this.wtherZLP = true;
                                    this.$refs['zlForm'].resetFields();
                                    this.reNewADLog()
                                }else{
                                    this.$message.error("出错了");
                                }
                            })
                        
                        }else{
                            this.$message.error("你没有权限！")
                        }
                    }else{
                        return false;
                    }
                })
            }else if(this.zlVis==1){
                this.$refs['sbForm'].validate((valid) => {
                    if (valid) {
                        if(localStorage.getItem("ms_username") == "Sean" || localStorage.getItem("ms_username") == "Chuck Yu" || localStorage.getItem("ms_username") == "eying" || localStorage.getItem("ms_username") == "sophia" || localStorage.getItem("ms_username") == "oliver" || localStorage.getItem("ms_username") == "Aron"){
                            fetch('api/WorkReport/sbAndonCfm',{
                                method:'POST',
                                headers:{
                                    'Content-Type':'application/json'
                                },
                                body:JSON.stringify({
                                    workNo:this.sbForm.sbworkNo,
                                    priority:this.sbForm.sbYxj,
                                    sbStop:this.sbForm.sbStation,
                                    sbMan:this.sbForm.sbMan,
                                    sbDpt:this.sbForm.sbDpt,
                                    sbNo:this.sbForm.sbNo,
                                    sbName:this.sbForm.sbName,
                                    station:this.sbForm.sbStation,
                                    qPart:this.sbForm.sbQpart,
                                    qType:this.sbForm.sbQtype,
                                    qDesc:this.sbForm.sbQdesc,
                                    sbSafe:this.sbForm.sbSafe
                                })
                            }).then(response=>response.json())
                            .then(data=>{
                                if(data[0].resSign){
                                    if(this.wtherStop && this.sbForm.sbStop=="已停线"){
                                        //停机状态打开
                                        this.wtherStop = true;
                                        this.pauseType = "设备停机"
                                        this.pauseDesc = this.sbForm.sbQdesc;
                                        this.pauseMan = this.sbForm.sbMan;
                                        this.wtherZLP = true;
                                        this.$refs['sbForm'].resetFields();
                                        this.newAndonVis = false;
                                        this.$message.success("设备安灯成功,现已停线。")
                                    }else{
                                        this.$refs['sbForm'].resetFields();
                                        this.newAndonVis = false;
                                        this.wtherZLP = true;
                                        this.$message.success("设备安灯成功")
                                    }
                                    this.reNewADLog();
                                }else{
                                    this.$message.error("按灯失败")
                                }
                            }).catch(data=>{
                                alert(data)
                            })
                        }else{
                            this.$message.error("你没有权限！");
                        }
                    }else{
                        return false;
                    }
                })

            }else{
                this.$refs['otherForm'].validate((valid) => {
                    if (valid) {
                        if(localStorage.getItem("ms_username") == "Sean" || localStorage.getItem("ms_username") == "Chuck Yu" || localStorage.getItem("ms_username") == "eying" || localStorage.getItem("ms_username") == "sophia" || localStorage.getItem("ms_username") == "oliver" || localStorage.getItem("ms_username") == "Aron"){
                            fetch('api/WorkReport/othAndonCfm',{
                                method:'POST',
                                headers:{
                                    'Content-Type':'application/json'
                                },
                                body:JSON.stringify({
                                    workNo:this.otherForm.othworkNo,
                                    adType:this.otherForm.othAndonT,
                                    adStation:this.otherForm.othStation,
                                    adStop:this.otherForm.othStop,
                                    adMan:this.otherForm.othMan,
                                    adQdesc:this.otherForm.othQdesc,
                                    adsbNo:this.otherForm.othsbNo
                                })
                            }).then(response=>response.json())
                            .then(data=>{
                                if(data[0].resSign){
                                    if(this.wtherStop && this.otherForm.othStop=="已停线"){
                                        //停机状态打开
                                        this.wtherStop = true;
                                        this.pauseType = this.otherForm.othAndonT+"停机"
                                        this.pauseDesc = this.otherForm.othQdesc;
                                        this.pauseMan = this.otherForm.othMan;
                                        this.wtherZLP = true;
                                        this.$refs['otherForm'].resetFields();
                                        this.newAndonVis = false;
                                        this.$message.success(this.otherForm.othAndonT+"安灯成功,现已停线。")
                                    }else{
                                        this.$refs['otherForm'].resetFields();
                                        this.newAndonVis = false;
                                        this.wtherZLP = true;
                                        this.$message.success(this.otherForm.othAndonT+"安灯成功")
                                    }
                                    this.reNewADLog()
                                }else{
                                    this.$message.error("按灯失败")
                                }
                            })
                        }else{
                            this.$message.error("你没有权限！")
                        }
                    }else{
                        return false;
                    }
                })
            }
           
        },
        //质量安灯取消
        zlAndonCancel(){
            var tmpName = null
            if(this.zlVis==0){
                tmpName = "zlForm"
            }else if(this.zlVis==1){
                tmpName = "sbForm"
            }else{
                tmpName = "otherForm"
            }
            this.newAndonVis = false;
            this.$refs[tmpName].resetFields();
        },
        //质量安灯表单关闭
        zlAndonClose(done){
             this.$confirm('确认关闭？')
            .then(_ => {
                if(this.zlVis==0){
                   this.$refs['zlForm'].resetFields();
                }else if(this.zlVis==1){
                   this.$refs['sbForm'].resetFields();
                }else{
                    this.$refs['otherForm'].resetFields();
                }
                done();
            })
            .catch(_ => {});
        },
        //刷新按灯记录，内圈数据
        reNewADLog(){
            fetch('api/WorkReport/reNewADLog',{
                method:'POST',
                headers:{
                    'Content-Type':'application/json'
                },
                body:JSON.stringify({
                    workNo:this.ordNo,
                })
            }).then(response=>response.json())
            .then(data=>{
                if(data[0].resSign){
                    this.pieOpt.series[0].data[0].name = data[0].zL.toString()
                    this.pieOpt.series[0].data[1].name = data[0].sB.toString()
                    this.pieOpt.series[0].data[2].name = data[0].gY.toString()
                    this.pieOpt.series[0].data[3].name = data[0].wL.toString()
                    this.pieOpt.series[0].data[4].name = data[0].tBD.toString()
                    this.piebtn.setOption(this.pieOpt);
                    var sm = data[0].zL+data[0].sB+data[0].gY+data[0].wL+data[0].tBD;
                    if(sm!=0){
                        this.wtherAD = true;
                    }else{
                        this.wtherAD = false;
                    }
                }
            }).catch(data=>{
                alert(data)
            })
        },
        //按灯响应
        AdResp(){
            this.AdLife = 1;
        },
        //按灯解决
        AdResolve(){
            this.AdLife = 2;
        },
        AdCfm(){
            this.AdLife = 3
        },
        //安灯记录打开时触发函数
        AdLogOpen(tip){
            fetch('api/WorkReport/AdLogOpen',{
                method:'POST',
                headers:{
                    'Content-Type':'application/json'
                },
                body:JSON.stringify({
                    workNo:this.ordNo,
                    qType:tip,
                })
            }).then(response=>response.json())
            .then(data=>{
                if(data.length !=0){
                    switch (data[0].adType){
                        case 1:
                            this.currAdT = "质量按灯";
                            break;
                        case 2:
                            this.currAdT = "设备按灯";
                            break;
                        case 3:
                            this.currAdT = "工艺按灯";
                            break;  
                        case 4:
                            this.currAdT = "物料按灯";
                            break; 
                        case 5:
                            this.currAdT = "TBD按灯";
                            break; 
                    }
                    this.currAdSts = data[0].adSts;
                    this.currAdNo = data[0].adNo;
                    this.currAdtime = data[0].adTime;
                    this.currAdMan = data[0].adMan;
                    this.currAdDesc = data[0].adqDesc;
                    this.renewAdtab()

                }else{
                    this.AdLife = 3
                }
                this.renewAdtab(tip)
            }).catch(data=>{
                alert(data)
            })
        },
        //刷新按灯记录表格
        renewAdtab(tip){
            fetch('api/WorkReport/renewAdtab',{
                method:'POST',
                headers:{
                    'Content-Type':'application/json'
                },
                body:JSON.stringify({
                    workNo:this.ordNo,
                    qType:tip
                })
            }).then(response=>response.json())
            .then(data=>{
                if(data.length !=0){
                    this.Adtab = []
                    var tmp = {
                        AdMan:data[0].按灯人,
                        AdNo:data[0].序号,
                        AdsbNo:data[0].设备编号,
                        AdqDesc:data[0].问题描述,
                        AdTime:data[0].按灯时间,
                        AdprcsTime:data[0].处理时间,
                        AdprcsMan:data[0].处理人,
                        AdshutMan:data[0].关闭人,
                        AdshutTime:data[0].关闭时间,
                        AdStation:data[0].工作中心,
                        AdStop:data[0].是否停线,
                        AdFns:data[0].临时解决,
                        AdPlan:data[0].行动计划,
                        AdFreasom:data[0].不能解决原因,
                        AdfnsDate:data[0].计划完成日期
                    }
                    this.Adtab.push(tmp)
                }
            }).catch(data=>{
                alert(data)
            })
        }
    },
    mounted(){
        // this.bgStage = 3;
        this.getOrder();
        this.getBGD();
        
        console.log("进度"+this.bgStage);

        var andonBtn = document.getElementById("pieBtn");
        this.piebtn = echart.init(andonBtn);
        this.piebtn.setOption(this.pieOpt);
        this.piebtn.on('click',function(params){
           bus.$emit('area',{mk:params.data.mark,name:params.data.name,tip:params.data.tip});
        });
        bus.$on('area',msg=>{
            // console.log(msg)
            if(this.bgStage == 0){
                return;
            }
            
            if(msg.mk == "inner"){
                this.andonLogVis = true;
                this.AdLogOpen(msg.tip);
            }else{
                if(this.wtherStop && this.wtherZLP){
                    this.$message.error("现已是"+this.pauseType+"状态，不可进行新的按灯！请先处理停机按灯");
                    return;
                }
                if(this.wtherStop && this.pauseType=="计划停机"){
                    this.$message.error("现已是 计划停机状态，不可进行按灯");
                    return;
                }
                if(this.wtherAD){
                    this.$message.error("正在进行按灯，不能再新建按灯了")
                }
                 
                if(msg.name=="质量"){
                    if(this.wtherStop){
                        this.$message.error("现在是停机状态，质量安灯需要在生产过程中进行，安灯完毕后会自动停机！")
                    }else{
                        this.newAndonVis = true;
                        this.zlVis = 0;
                        this.zlForm.zlworkNo = this.ordNo
                        
                        this.zlOpen();
                    }
                }else if(msg.name=="设备"){
                    this.newAndonVis = true;
                    this.zlVis = 1;
                    this.sbForm.sbworkNo = this.ordNo
                    this.sbForm.sbStation = this.pLine;
                    if(this.wtherStop){
                        this.sbForm.sbStop = "已停线"
                        this.sbInputVis = true;
                    }
                   
                }else{
                    this.newAndonVis = true;
                    this.zlVis = 2;
                    this.otherForm.othworkNo = this.ordNo;
                    this.otherForm.othAndonT = msg.name;
                    this.otherForm.othStation = this.pLine;
                    if(this.wtherStop){
                        this.otherForm.othStop = "已停线"
                        this.othInputVis = true;
                    }
                }
            }
        })
        
        
        //所有订单数据表格加载
        fetch('api/WorkReport/allOrdDisp',{
            method:'POST',
            headers:{
                'Content-Type':'application/json'
            },
            body:JSON.stringify({
                prodLine:this.pLine
            })
        }).then(response=>response.json())
        .then(data=>{
            this.allOrder = []
            for(var item of data){
                var tmp = {
                    workNo:item.作业单号,
                    wlNo:item.物料编号,
                    wlDesc:item.物料描述,
                    proceNo:item.工序号,
                    proceName:item.工序名称,
                    sTime:item.开始日期,
                    eTime:item.完工日期,
                    ordNum:item.订单数量,
                    unfnsNum:item.未完工数量,
                    leftHr:(item.剩余工时).toFixed(2),
                    testHr:item.调试计划用时
                };
                this.allOrder.push(tmp);
            }
        }).catch(data=>{
            alert(data)
        })
    }
}
</script>
<style scoped>
.title{
    height:40px;
    background:lightblue;
    color:white;
    font-size:30px;
    text-align:left;
    line-height:40px;
}
.Btns{
    width:100px;
    border:1px solid rgba(220,220,220,0.5);
    margin:2%;
}
.tgs{
    font-size:25px;
    display:inline-block;
    width:40%;
    /* border:1px solid #000; */
    margin:1% 2%;
}
.bgd{
    height:23px;
    line-height:23px;
    font-size:13px;
}
.fcnBtn{
    height:250px;
    font-size:20px;
    text-align:center;
    line-height:250px;
    background:lightgreen;
    cursor:pointer;
    user-select: none;
    /* border-radius:10%; */
}
.clock{
    border-radius:10%;
    border:1px solid #000;
    height:80px;
    width:80px;
    line-height:80px;
    text-align:center
}
.clockCtrl{
    border-radius:50%;
    width:30px;
    height:30px;
    font-size:25px;
    margin:45% 25%;
    line-height:30px;
    border: 1px solid rgba(0,0,0,0.5);
    text-align:center;
    cursor:pointer;
    user-select: none;
}

</style>