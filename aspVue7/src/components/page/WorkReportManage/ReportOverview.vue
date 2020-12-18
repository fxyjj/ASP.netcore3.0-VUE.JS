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
            <el-date-picker v-model="prodDate" type="daterange" align="right"  unlink-panels  range-separator="至" start-placeholder="开始日期" end-placeholder="结束日期" :picker-options="pickerOptions" style="height:33px;padding-top:2px"></el-date-picker>

            <el-button type="primary" @click="query()">查询</el-button>
        </el-card>
        <el-card>
            <el-table :data="bgTab" style="width: 100%" border fit>
                <el-table-column prop="bgNo" label="报工编号" width="180"></el-table-column>
                <el-table-column prop="crtMan" label="创建人"></el-table-column>
                <el-table-column prop="crtDate" label="创建日期" width="180"></el-table-column>
                <el-table-column prop="workShop" label="所属车间"></el-table-column>
                <el-table-column prop="workUnit" label="加工单元" width="120"></el-table-column>
                <el-table-column prop="cgroup" label="工作组" width="150"></el-table-column>
                <el-table-column prop="workMan" label="加工人员"></el-table-column>
                <el-table-column prop="cls" label="所属班组"></el-table-column>
                <el-table-column prop="prodDate" label="生产日期" width="180"></el-table-column>
                <el-table-column prop="workNo" label="作业单号" width="150"></el-table-column>
                <el-table-column prop="wlNo" label="物料编号" width="150"></el-table-column>
                <el-table-column prop="wlDesc" label="物料描述" width="220"></el-table-column>
                <el-table-column prop="proceNo" label="工序号"></el-table-column>
                <el-table-column prop="proceName" label="工序名称"></el-table-column>
                <el-table-column prop="bgCate" label="报工类别"></el-table-column>
                <el-table-column prop="fixNum" label="定额件数"></el-table-column>
                <el-table-column prop="testStime" label="调试开始时间" width="180"></el-table-column>
                <el-table-column prop="testTime" label="调试时间"></el-table-column>
                <el-table-column prop="testEtime" label="调试结束时间" width="180"></el-table-column>
                <el-table-column prop="workStime" label="作业开始时间" width="180"></el-table-column>
                <el-table-column prop="workEtime" label="作业完工时间" width="180"></el-table-column>
                <el-table-column prop="planStime" label="计划停机时间" width="120"></el-table-column>
                <el-table-column prop="unplanStime" label="非计划停机时间" width="120"></el-table-column>
                <el-table-column prop="doneNum" label="完工数量"></el-table-column>
                <el-table-column prop="passNum" label="合格数量"></el-table-column>
                <el-table-column prop="lf" label="料废"></el-table-column>
                <el-table-column prop="jf" label="机废"></el-table-column>
                <el-table-column prop="df" label="调废"></el-table-column>
                <el-table-column prop="gf" label="工废"></el-table-column>
                <el-table-column prop="dcl" label="待处理"></el-table-column>
                <el-table-column prop="failNum" label="不良数量"></el-table-column>
                <el-table-column prop="rePassNum" label="返工合格数量" width="220"></el-table-column>
                <el-table-column prop="failNo" label="不合格单编号" width="220"></el-table-column>
                <el-table-column prop="status" label="状态"></el-table-column>
                <el-table-column prop="prodMan" label="生产人数"></el-table-column>
                <el-table-column prop="fixMan" label="定额人数"></el-table-column>
                <el-table-column prop="clsDate" label="班次日期" width="180"></el-table-column>
                <el-table-column fixed="right" label="操作" width="150px" >
                    <template slot-scope="scope">
                        <el-button @click="bgEdit(scope.row)" style="background:#F5F5F5;" size="small">编辑</el-button>
                        <el-button size="small" style="background:#F5F5F5;" @click="bgDel(scope.row)">删除</el-button>
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
                        <el-form-item label="创建人" prop="crtMan">
                            <el-input disabled v-model="editForm.crtMan"></el-input>
                        </el-form-item>
                    </el-col>
                    <el-col :span="6">
                        <el-form-item label="创建日期" prop="crtDate">
                            <el-input disabled v-model="editForm.crtDate"></el-input>
                        </el-form-item>
                    </el-col>
                    <el-col :span="6">
                        <el-form-item label="所属车间" prop="workShop">
                            <el-input v-model="editForm.workShop"></el-input>
                        </el-form-item>
                    </el-col>
                </el-row>
                <el-row>
                    <el-col :span="6">
                        <el-form-item label="加工单元" prop="workUnit">
                            <el-input disabled v-model="editForm.workUnit"></el-input>
                        </el-form-item>
                    </el-col>
                    <el-col :span="6">
                        <el-form-item label="工作组" prop="cgroup">
                            <el-input disabled v-model="editForm.cgroup"></el-input>
                        </el-form-item>
                    </el-col>
                    <el-col :span="6">
                        <el-form-item label="加工人员" prop="workMan">
                            <el-input v-model="editForm.workMan"></el-input>
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
                        <el-form-item label="生产日期" prop="prodDate">
                            <el-date-picker v-model="editForm.prodDate" type="datetime"></el-date-picker>
                        </el-form-item>
                    </el-col>
                    <el-col :span="6">
                        <el-form-item label="作业单号" prop="workNo">
                            <el-input disabled v-model="editForm.workNo"></el-input>
                        </el-form-item>
                    </el-col>
                    <el-col :span="6">
                        <el-form-item label="物料编号" prop="wlNo">
                            <el-input v-model="editForm.wlNo" disabled></el-input>
                        </el-form-item>
                    </el-col>
                    <el-col :span="6">
                        <el-form-item label="物料描述" prop="wlDesc" >
                            <el-input v-model="editForm.wlDesc" disabled></el-input>
                        </el-form-item>
                    </el-col>
                </el-row>
                <el-row>
                    <el-col :span="6">
                        <el-form-item label="工序号" prop="proceNo">
                            <el-input disabled v-model="editForm.proceNo"></el-input>
                        </el-form-item>
                    </el-col>
                    <el-col :span="6">
                        <el-form-item label="工序名称" prop="proceName">
                            <el-input disabled v-model="editForm.proceName"></el-input>
                        </el-form-item>
                    </el-col>
                    <el-col :span="6">
                        <el-form-item label="报工类别" prop="bgCate">
                            <el-input disabled v-model="editForm.bgCate"></el-input>
                        </el-form-item>
                    </el-col>
                    <el-col :span="6">
                        <el-form-item label="定额件数" prop="fixNum">
                            <el-input disabled v-model="editForm.fixNum"></el-input>
                        </el-form-item>
                    </el-col>
                </el-row>
                <el-row>
                    <el-col :span="6">
                        <el-form-item label="调试开始时间" prop="testStime">
                            <el-date-picker v-model="editForm.testStime" type="datetime"></el-date-picker>
                        </el-form-item>
                    </el-col>
                    <el-col :span="6">
                        <el-form-item label="调试时间" prop="testTime">
                            <el-input v-model.number="editForm.testTime"></el-input>
                        </el-form-item>
                    </el-col>
                    <el-col :span="6">
                        <el-form-item label="调试结束时间" prop="testEtime">
                            <el-date-picker type="datetime" v-model="editForm.testEtime"></el-date-picker>
                        </el-form-item>
                    </el-col>
                    <el-col :span="6">
                        <el-form-item label="作业开始时间" prop="workStime">
                            <el-date-picker type="datetime" v-model="editForm.workStime"></el-date-picker>
                        </el-form-item>
                    </el-col>
                </el-row>
                <el-row>
                    <el-col :span="6">
                        <el-form-item label="作业完工时间" prop="workEtime">
                            <el-date-picker v-model="editForm.workEtime" type="datetime"></el-date-picker>
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
                    <el-col :span="6">
                        <el-form-item label="完工数量" prop="doneNum">
                            <el-input v-model="editForm.doneNum"></el-input>
                        </el-form-item>
                    </el-col>
                </el-row>
                <el-row>
                    <el-col :span="6">
                        <el-form-item label="合格数量" prop="passNum">
                            <el-input v-model.number="editForm.passNum"></el-input>
                        </el-form-item>
                    </el-col>
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
                </el-row>
                <el-row>
                    <el-col :span="6">
                        <el-form-item label="工废" prop="gf">
                            <el-input v-model.number="editForm.gf"></el-input>
                        </el-form-item>
                    </el-col>
                    <el-col :span="6">
                        <el-form-item label="待处理" prop="dcl">
                            <el-input v-model.number="editForm.dcl"></el-input>
                        </el-form-item>
                    </el-col>
                    <el-col :span="6">
                        <el-form-item label="不良数量" prop="failNum">
                            <el-input  v-model.number="editForm.failNum"></el-input>
                        </el-form-item>
                    </el-col>
                    <el-col :span="6">
                        <el-form-item label="返工合格数量" prop="repassNum">
                            <el-input v-model.number="editForm.repassNum"></el-input>
                        </el-form-item>
                    </el-col>
                </el-row>
                <el-row>
                    <el-col :span="6">
                        <el-form-item label="不合格单编号" prop="failNo">
                            <el-input disabled v-model="editForm.failNo"></el-input>
                        </el-form-item>
                    </el-col>
                    <el-col :span="6">
                        <el-form-item label="状态" prop="status">
                            <el-input v-model.="editForm.status" disabled></el-input>
                        </el-form-item>
                    </el-col>
                    <el-col :span="6">
                        <el-form-item label="生产人数" prop="prodMan">
                            <el-input  v-model.number="editForm.prodMan"></el-input>
                        </el-form-item>
                    </el-col>
                    <el-col :span="6">
                        <el-form-item label="定额人数" prop="fixMan">
                            <el-input v-model.number="editForm.fixMan" disabled></el-input>
                        </el-form-item>
                    </el-col>
                </el-row>
                <el-form-item label="班次日期" prop="clsDate">
                    <el-date-picker type="datetime" v-model="editForm.clsDate"></el-date-picker>
                </el-form-item>
            </el-form>
            <el-button type="primary" @click="editCfm()">确认</el-button>
            <el-button type="primary" @click="editCancel()">取消</el-button>
        </el-dialog>
    </div>
</template>

<script>
export default {
    data(){
        return{
            editVis:false,
            prodDate:null,
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
                crtMan:'',
                crtDate:'',
                workShop:'',
                workUnit:'',
                cgroup:'',
                workMan:'',
                cls:'',
                prodDate:'',
                workNo:'',
                wlNo:'',
                wlDesc:'',
                proceNo:null,
                proceName:'',
                bgCate:'',
                fixNum:null,
                testStime:'',
                testTime:0,
                testEtime:'',
                workStime:'',
                workEtime:'',
                planStime:null,
                unplanStime:null,
                doneNum:null,
                passNum:null,
                lf:null,
                jf:null,
                df:null,
                gf:null,
                dcl:null,
                failNum:null,
                repassNum:null,
                faiNo:'',
                status:0,
                prodMan:null,
                fixMan:null,
                clsDate:''

            },
            editRule:{
                workShop:[{required:true,message:'车间不能为空',trigger:'blur'}],
                workUnit:[{required:true,message:'加工单元是哪个',trigger:'blur'}],
                cgroup:[{required:true,message:'工作组不能为空',trigger:'blur'}],
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
            }
        }
    },
    methods:{
        query(){
            console.log(this.prodDate);
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
                            crtMan:item.创建人,
                            crtDate:item.创建日期,
                            workShop:item.所属车间,
                            workUnit:item.加工单元,
                            cgroup:item.工作组,
                            workMan:item.加工人员,
                            cls:item.所属班组,
                            prodDate:item.生产日期,
                            workNo:item.作业单号,
                            wlNo:item.物料编号,
                            wlDesc:item.物料描述,
                            proceNo:item.工序号,
                            proceName:item.工序名称,
                            bgCate:item.报工类别,
                            fixNum:item.定额件数,
                            testStime:item.调试开始时间,
                            testTime:item.调试时间,
                            testEtime:item.调试结束时间,
                            workStime:item.作业开始时间,
                            workEtime:item.作业完工时间,
                            planStime:item.计划停机时间,
                            unplanStime:item.非计划停机时间,
                            doneNum:item.完工数量,
                            passNum:item.合格数量,
                            lf:item.料废,
                            jf:item.机废,
                            df:item.调废,
                            gf:item.工废,
                            dcl:item.待处理,
                            failNum:item.不良数量,
                            rePassNum:item.返工合格数量,
                            failNo:item.不合格单编号,
                            status:item.状态,
                            prodMan:item.生产人数,
                            fixMan:item.定额人数,
                            clsDate:item.班次日期,
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
            this.line='',
            console.log(event);
            if(event=="EGR阀装配线"){
                this.pLine=this.EGRLine;
            }else if(event=="Cooler/Module线"){
                this.pLine=this.CMLine;
            }else if(event=="电子线"){
                this.pLine=this.ELELine;
            }
        },
        bgEdit(row){
             if(localStorage.getItem("ms_username") == "Sean"){
                // this.editForm.bgNo = row.bgNo;
                // this.editForm.crtMan = row.crtMan;
                // this.editForm.crtDate = row.crtDate;
                // this.editForm.workShop = row.workShop;
                // this.editForm.workUnit = row.workUnit;
                // this.editForm.cgroup=row.cgroup
                // this.editForm.workMan=row.workMan
                // this.editForm.cls=row.cls
                // this.editForm.prodDate=row.prodDate
                // this.editForm.workNo=row.workNo
                // this.editForm.wlNo=row.wlNo
                // this.editForm.wlDesc=row.wlDesc
                // this.editForm.proceNo=row.proceNo
                // this.editForm.proceName=row.proceName
                // this.editForm.bgCate=row.bgCate
                // this.editForm.fixNum=row.fixNum
                // this.editForm.testStime=row.testStime
                // this.editForm.testTime=row.testTime
                // this.editForm.testEtime=row.testEtime
                // this.editForm.workStime=row.workStime
                // this.editForm.workEtime=row.workEtime
                // this.editForm.planStime=row.planStime
                // this.editForm.unplanStime=row.unplanStime
                // this.editForm.doneNum=row.doneNum
                // this.editForm.passNum=row.passNum
                // this.editForm.lf=row.lf
                // this.editForm.jf=row.jf
                // this.editForm.df=row.df
                // this.editForm.gf=row.gf
                // this.editForm.dcl=row.dcl
                // this.editForm.failNum=row.failNum
                // this.editForm.repassNum=row.repassNum
                // this.editForm.faiNo=row.faiNo
                // this.editForm.status=row.status
                // this.editForm.prodMan=row.prodMan
                // this.editForm.fixMan=row.fixMan
                // this.editForm.clsDate=row.clsDate

                // this.editVis = true;
                this.$message.warning("Sean还在努力开发中！敬请期待")
             }else{
                 this.$message.error("你没有权限！")
             }
        },
        bgDel(row){
             if(localStorage.getItem("ms_username") == "Sean"){
                 this.$message.warning("Sean还在努力开发中！敬请期待")
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
            this.$refs[formName].validate((valid) => {
                if (valid) {
                    this.editVis=false
                    this.$refs['editForm'].resetFields();
                }else{
                    return false;
                }
            })
        },
        editCancel(){
            this.editVis=false
            this.$refs['editForm'].resetFields();
        }
    }
}
</script>