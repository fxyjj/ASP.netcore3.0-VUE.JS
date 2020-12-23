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
        <el-card>
            <el-table :data="bgTab" style="width: 100%" border fit>
                <el-table-column prop="bgNo" label="报工编号" width="180"></el-table-column>
                <el-table-column prop="prodDate" label="生产日期" width="180"></el-table-column>
                <el-table-column prop="clsDate" label="班次日期" width="180"></el-table-column>
                <el-table-column prop="workUnit" label="加工单元" width="120"></el-table-column>
                <el-table-column prop="cls" label="所属班组"></el-table-column>
                <el-table-column prop="workMan" label="加工人员"></el-table-column>
                <el-table-column prop="workNo" label="作业单号" width="150"></el-table-column>
                <el-table-column prop="wlNo" label="物料编号" width="150"></el-table-column>
                <el-table-column prop="wlDesc" label="物料描述" width="220"></el-table-column>
                <el-table-column prop="proceNo" label="工序号"></el-table-column>
                <el-table-column prop="proceName" label="工序名称"></el-table-column>
                <el-table-column prop="fixNum" label="定额件数"></el-table-column>
                <el-table-column prop="testStime" label="调试开始时间" width="180"></el-table-column>
                <el-table-column prop="testTime" label="调试时间"></el-table-column>
                <el-table-column prop="workStime" label="作业开始时间" width="180"></el-table-column>
                <el-table-column prop="workEtime" label="作业完工时间" width="180"></el-table-column>
                <el-table-column prop="planStime" label="计划停机时间" width="120"></el-table-column>
                <el-table-column prop="unplanStime" label="非计划停机时间" width="120"></el-table-column>
                <el-table-column prop="doneNum" label="完工数量"></el-table-column>
                <el-table-column prop="passNum" label="合格数量"></el-table-column>
                <el-table-column prop="failNum" label="不良数量"></el-table-column>
                <el-table-column prop="lf" label="料废"></el-table-column>
                <el-table-column prop="jf" label="机废"></el-table-column>
                <el-table-column prop="df" label="调废"></el-table-column>
                <el-table-column prop="gf" label="工废"></el-table-column>
                <el-table-column prop="dcl" label="待处理"></el-table-column>
                <el-table-column prop="thryTime" label="理论时间"></el-table-column>
                <el-table-column prop="pWorkTime" label="计划工作时间" width="120"></el-table-column>
                <el-table-column prop="opsTime" label="操作时间"></el-table-column>
                <el-table-column prop="fixProdNum" label="定额产量"></el-table-column>
                <el-table-column prop="passRate" label="合格率"></el-table-column>
                <el-table-column prop="manEff" label="员工效率"></el-table-column>
                <el-table-column prop="eqpEff" label="设备开动率" width="100"></el-table-column>
                <el-table-column prop="prodMan" label="生产人数"></el-table-column>
                <el-table-column prop="fixMan" label="定额人数"></el-table-column>
                <el-table-column prop="oee" label="OEE"></el-table-column>
                <el-table-column prop="cgroup" label="工作组" width="150"></el-table-column>
                <el-table-column prop="status" label="状态"></el-table-column>
                <el-table-column prop="bgCate" label="报工类别"></el-table-column>
                <el-table-column prop="testEtime" label="调试结束时间" width="180"></el-table-column>
                <el-table-column prop="test2done" label="调试到完工" width="100"></el-table-column>
                <el-table-column prop="work2done" label="作业到完工" width="100"></el-table-column>
                <el-table-column prop="ordType" label="订单类型"></el-table-column>
                <el-table-column prop="repassNum" label="返工合格数量" width="120"></el-table-column>
                <el-table-column prop="ftt" label="一次合格率" width="100"></el-table-column>
                <el-table-column prop="tjl" label="停机率"></el-table-column>
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
            }
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
            his.$confirm('确认关闭？')
            .then(_ => {
                done();
            })
            .catch(_ => {});
        }
    }
}
</script>