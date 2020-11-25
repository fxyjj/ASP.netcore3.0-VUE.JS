<template>
    <div>
        <el-row>
            <el-col :span="4">
                <el-card style="height:398px;margin-right:8px">
                    <div class="title" style="font-size:18px;"> 产线名：{{pLine}} </div>
                    <div>
                        <el-button class="Btns" @click="blpVis=true">不良品记录</el-button>
                        <el-button class="Btns">异常报检</el-button>
                        <el-button class="Btns">生产记录</el-button>
                        <el-button class="Btns">计划停机</el-button>
                        <el-button class="Btns">异常停机</el-button>
                        <el-button class="Btns">停机信息</el-button>
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
                <el-card style="height:290px;margin:8px 0px">
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
                                        <el-form-item label="不良数量" prop="BgFlawNum">
                                            <el-input v-model.number="BgForm.BgFlawNum"></el-input>
                                        </el-form-item>
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
                            
                            <el-card v-else-if="bgStage>=5" style="height:250px" shadow="hover">报工结束</el-card>
                        </el-col>
                        <el-col :span="1"><el-divider direction="vertical" style="height:250px;margin:0% 50%"></el-divider></el-col>
                        <el-col :span="4">
                            <el-card v-if="bgStage>=5" style="height:250px" shadow="hover">
                               <div class="fcnBtn" @click="BgComplete()">关闭报工单</div>
                            </el-card>
                        </el-col>
                    </el-row>   
                </el-card>
            </el-col>
        </el-row>
        <el-row>
            <el-col :span="4">
                <el-card style="margin-right:8px">
                    <div class="title">当班员工信息</div>
                   <el-table :data="manInfo" style="" height="500">
                        <el-table-column prop="posReg" label="岗位登记" ></el-table-column>
                        <el-table-column prop="man" label="员工" ></el-table-column>
                        <el-table-column prop="skill" label="技能" ></el-table-column>
                    </el-table>
                </el-card>
            </el-col>
            <el-col :span="20">
                <el-row>
                    <el-col :span="18">
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
                    <el-col :span="6">
                        <el-card style="height:300px;">
                            <div class="title">Andon设置</div>
                            <div id="pieBtn" style="width:280px;height:240px;margin:0% 5%"></div>
                        </el-card>
                    </el-col>
                </el-row>
                <el-row>
                    <el-card style="margin-top:8px">
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
                    <el-date-picker v-model="testForm.testEtime" type="datetime" format="yyyy-mm-dd HH:mm:ss" placeholder="选择日期" disabled></el-date-picker>
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
        <el-dialog title="不良品记录单" :visible.sync="blpVis">

        </el-dialog>
    </div>
</template>
<script>
import echart from 'echarts'
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
                        radius: ['25%', '55%'],
                        label: {
                            position:'inner'
                        },
                        labelLine: {
                            show: false
                        },
                        data: [
                            {value: 1, name: '4'},
                            {value: 1, name: '3'},
                            {value: 1, name: '2'},
                            {value: 1, name: '1'},
                            {value: 1, name: '0'}

                        ]
                    },
                    {
                        name: '',
                        type: 'pie',
                        radius: ['55%', '90%'],
                        label: {
                            position:'inner'
                        },
                        labelLine: {
                            show: false
                        },
                        data: [
                            {value: 1, name: '直达'},
                            {value: 1, name: '邮件'},
                            {value: 1, name: '联盟'},
                            {value: 1, name: '视频'},
                            {value: 1, name: '百度'},
                        ]
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
                testMan:[{required:true,message:'是谁调试的？',trigger:'blur'}],
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
            //侧边按钮弹窗参数
            blpVis:false,//不良品记录弹窗显影指示
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
                })
            })
            .then(response=>response.json())
            .then(data=>{
                console.log("长度"+data.length)
                console.log(this.creatable)
                if(data.length != 0){
                    this.creatable = true;
                    this.bgNo = data[0].bgNo
                    this.bgcrtMan = data[0].initMan
                    this.bgcrtDate = (data[0].crtDate).substr(0,10)
                    this.bgproceNo = data[0].proceNo
                    this.bgproceName = data[0].proceName
                    this.bgfixNum = data[0].fixNum
                    this.bgWunit = data[0].workUnit
                    this.bgfixMan = data[0].fixMan
                    if(data[0].workMan != ''){
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
                 console.log(this.creatable)
            }).catch(data=>{
                alert(data)
            })
        },
        // getBGDtest(){
        //      fetch('api/WorkReport/BGDtestChk',{
        //         method:'POST',
        //         headers:{
        //             'Content-Type':'application/json'
        //         },
        //         body:JSON.stringify({
        //             workNo:this.ordNo,
        //         })
        //     })
        //     .then(response=>response.json())
        //     .then(data=>{
        //         console.log(data)
        //         if(data.length != 0){
        //             this.tStime = data[0].tsTime
        //             this.tEtime = data[0].teTime
        //             this.tSman = data[0].tsMan
        //             this.tEman = data[0].teMan
        //             this.duration = data[0].duration
        //             this.bgSatge = 3;
        //         }
        //     })
        //     .catch(data=>{
        //         alert(data)
        //     })
        // },
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
                    this.ordNo = data[1].作业单号,
                    this.ordNum = data[1].订单数量,
                    this.wlNo = data[1].物料编号,
                    this.wlDesc = data[1].物料描述,
                    this.downNum = data[1].完工数量,
                    this.failNum = data[1].不合格数量,
                    this.currBGD = data[1].报工编号
                    this.pct = (data[1].合格数量/data[1].订单数量)*100
                    // this.getBGDtest();
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
                    this.scStart = this.workStime;
                    this.scDate = this.prodStime;
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
                        this.$refs['BgForm'].resetFields();
                        this.BgVis = false;
                        this.bgStage = 5;
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