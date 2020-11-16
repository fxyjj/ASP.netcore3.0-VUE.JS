<template>
    <div>
        <el-card ><!--style="position:fixed;top:12%;z-index: 500;width:85%"style="margin-top:4%;"-->
            
            <!-- <el-pagination :hide-on-single-page="singlePage" :total="tableData.length" :page-size="pageSize" layout="prev, pager, next" @current-change="changePage($event)" style="display:inline-block"></el-pagination> -->
            <div class="search">
                <el-select v-model="selProdVal" placeholder="请选择订单产线" style="width:150px;margin-right:2%;display:inline-block">
                    <el-option v-for="item in lineMenu" :key="item" :label="item" :value="item"> </el-option>
                </el-select>
                <el-input
                    placeholder="请输入内容"
                    prefix-icon="el-icon-search"
                    v-model="searchItem"
                    @keyup.enter.native="search()"
                    style="width:200px;display:inline-block">
                </el-input>
                <el-button type="primary" @click="search('all')">搜索</el-button>
                <el-button type="primary" @click="exportExl()">导出到Excel</el-button>
                <el-button type="primary" @click="addData()">新增订单</el-button>
                <el-button type="primary" @click="delData()">关闭选中</el-button>
                <el-button type="primary" @click="xiadaData()">下达选中</el-button>
                <el-button type="primary" @click="showImport()">导入</el-button>
                <div class="cBlock" style="border:1px solid rgba(0,0,0,0.4)" @click="search('创建')">创建</div>
                <div class="cBlock" style="background: #aadde7;" @click="search('下达')">下达</div>
                <div class="cBlock" style="background: yellow " @click="search('生产中')">生产中</div>
                <div class="cBlock" style="background:#22B14C" @click="search('完工')">完工</div>

            </div>
        </el-card>
        <el-card >
            <el-table :data="tableData" style="width: 100%"   @selection-change="handleSelectionChange" :row-class-name="rowStyle" height="660"  ><!--:row-click="btnVis=true" :cell-mouse-leave="btnVis=false"tableData.slice((currPage-1)*pageSize,currPage*pageSize)-->
                 <el-table-column fixed="left" type="selection" width="55" ></el-table-column>
                <el-table-column prop="orderNo" label="订单号"  width="100"></el-table-column>
                <el-table-column prop="wlNo" label="物料号" width="120"></el-table-column>
                <el-table-column prop="wlDesc" label="物料描述" width="120"></el-table-column>
                <el-table-column prop="orderNum" label="订单数量" ></el-table-column>
                <el-table-column prop="planStime" label="计划开始日期" width="100px"></el-table-column>
                <el-table-column prop="planEtime" label="计划完工日期" width="100px"></el-table-column>
                <el-table-column prop="orderedTime" label="生产下达日期" width="100px"></el-table-column>
                <el-table-column prop="prodStime" label="生产开始日期" width="100px"></el-table-column>
                <el-table-column prop="prodEtime" label="生产完工日期" width="100px"></el-table-column>
                <el-table-column prop="passNum" label="合格数量"></el-table-column>
                <el-table-column prop="failNum" label="不合格数量"></el-table-column>
                <el-table-column prop="srcStn" label="来源车间" width="120" ></el-table-column>
                <el-table-column prop="status" label="状态" ></el-table-column>
                <el-table-column prop="ordType" label="订单类型"></el-table-column>
                <el-table-column prop="tip" label="备注"></el-table-column>
                <el-table-column fixed="right" label="操作" width="250px" >
                    <template slot-scope="scope">
                        <el-button @click="order(scope.row)" :style='{background:(scope.row.status=="创建"?"#85ce61":"#F5F5F5")}' size="small" :disabled="scope.row.status=='创建'?false:true">下达</el-button>
                        <el-button @click="handleClick(scope.row)" style="background:#F5F5F5;" size="small">编辑</el-button>
                        <el-button size="small" style="background:#F5F5F5;" @click="deleteAcc(scope.row)">关闭</el-button>
                        
                    </template>
                </el-table-column>
            </el-table>
        </el-card>
      <el-dialog
        title="新增"
        :visible.sync="dialogVisible"
        width="30%"
        :before-close="handleClose">
          <el-form ref="form" :model="form" :rules="rule" class="fromStyle" label-width="100px">
                <el-form-item label="订单号" prop="ordNo">
                    <el-input v-model="form.ordNo" ></el-input>
                </el-form-item>
                <el-form-item label="物料号" prop="wlNo">
                    <el-input v-model="form.wlNo" ></el-input>
                </el-form-item>
                <el-form-item label="物料描述" prop="wlDesc">
                    <el-input  v-model="form.wlDesc"></el-input>
                </el-form-item>
                 <el-form-item label="订单数量" prop="ordNum">
                    <el-input v-model="form.ordNum" ></el-input>
                </el-form-item>
                <el-form-item label="计划开始日期" prop="sTime">
                     <el-date-picker v-model="form.sTime" type="date" placeholder="选择日期"></el-date-picker>
                </el-form-item>
                <el-form-item label="计划完工日期" prop="eTime">
                    <el-date-picker v-model="form.eTime" type="date" placeholder="选择日期"></el-date-picker>
                </el-form-item>
                <el-form-item label="订单下达日期" prop="ordTime">
                     <el-date-picker v-model="form.ordTime" type="date" :placeholder="form.ordTime.toString()" disabled></el-date-picker>
                </el-form-item>
                 <el-form-item label="来源车间" prop="station">
                    <el-select v-model="form.station" placeholder="请选择">
                        <el-option value="EGR阀线"> EGR阀线</el-option>
                        <el-option value="Cooler/Module线"> Cooler/Module线</el-option>
                        <el-option value="电子线"> 电子线</el-option>
                    </el-select>
                </el-form-item>
                <el-form-item label="状态" prop="status">
                    <el-input v-model="form.status" :placeholder="form.status" disabled></el-input>
                </el-form-item>
                <el-form-item label="订单类型" prop="ordType">
                    <!-- <el-input  v-model="form.ordType"></el-input> -->
                    <el-select v-model="form.ordType" placeholder="请选择">
                        <el-option value="受控订单"> 受控订单</el-option>
                        <el-option value="试制订单"> 试制订单</el-option>
                    </el-select>
                </el-form-item>
                 <el-form-item label="备注" prop="tip">
                    <el-input v-model="form.tip" ></el-input>
                </el-form-item>
            </el-form>
                <span slot="footer" class="dialog-footer">
                    <el-button @click="addClose()">取 消</el-button>
                    <el-button type="primary" @click="addFinish('form')">确 定</el-button>
                </span>
          </el-dialog>

         <el-dialog
        title="编辑"
        :visible.sync="editDialog"
        width="30%"
        :before-close="editClose">
          <el-form ref="editForm" :model="editForm" :rules="editRule" class="fromStyle" label-width="110px">
                <el-form-item label="订单号" prop="ordNo">
                    <el-input v-model="editForm.ordNo" disabled></el-input>
                </el-form-item>
                <el-form-item label="物料号" prop="wlNo">
                    <el-input v-model="editForm.wlNo" ></el-input>
                </el-form-item>
                <el-form-item label="物料描述" prop="wlDesc">
                    <el-input  v-model="editForm.wlDesc"></el-input>
                </el-form-item>
                 <el-form-item label="订单数量" prop="ordNum">
                    <el-input v-model="editForm.ordNum" ></el-input>
                </el-form-item>
                <el-form-item label="计划开始日期" prop="sTime">
                     <el-date-picker v-model="editForm.sTime" type="date" placeholder="选择日期"></el-date-picker>
                </el-form-item>
                <el-form-item label="计划完工日期" prop="eTime">
                    <el-date-picker v-model="editForm.eTime" type="date" placeholder="选择日期"></el-date-picker>
                </el-form-item>
                <el-form-item label="订单下达日期" prop="ordTime">
                     <el-date-picker v-model="editForm.ordTime" type="date" disabled></el-date-picker>
                </el-form-item>
                 <el-form-item label="来源车间" prop="station">
                    <el-input v-model="editForm.station" ></el-input>
                </el-form-item>
                <el-form-item label="状态" prop="status">
                    <el-input v-model="editForm.status" :placeholder="editForm.status" disabled></el-input>
                </el-form-item>
                <el-form-item label="订单类型" prop="ordType">
                    <el-input  v-model="editForm.ordType"></el-input>
                </el-form-item>
                 <el-form-item label="备注" prop="tip">
                    <el-input v-model="editForm.tip" ></el-input>
                </el-form-item>
            </el-form>
        <span slot="footer" class="dialog-footer">
            <el-button @click="editCancel()">取 消</el-button>
            <el-button type="primary" @click="editFinish('editForm')">确 定</el-button>
        </span>
          </el-dialog>

        <el-dialog title="重要提示" :visible.sync="deleteVis" width="20%" style="padding:0 20px 30px">
            <i class="el-icon-warning-outline" style="color:#ff0000;font-size:100px;margin:4% 34%"></i>
            <h3 style="text-align:center">确认要关闭 <strong style="color:#ff0000">{{delInfo}}</strong> 该订单吗？</h3>
            <div class="cbtn">
            <el-button type="success" @click="deleteVis=false" style="width:40%">取消</el-button>
            <el-button type="danger" style="width:40%;margin-left:20%" @click="deleteComfirm()">确认</el-button>
            </div>
        </el-dialog>
        <!--批量关闭的弹窗-->
        <el-dialog title="重要提示" :visible.sync="mulCloseVis" width="20%" style="padding:0 20px 30px">
            <i class="el-icon-warning-outline" style="color:#ff0000;font-size:100px;margin:4% 34%"></i>
            <h3 style="text-align:center">确认要关闭以下订单吗？</h3>
             <div style="text-align:center" v-for="item in multipleSelection" :key="item.orderNo">{{item.orderNo}}</div>
            <div class="cbtn">
            <el-button type="success" @click="mulCloseVis=false" style="width:40%">取消</el-button>
            <el-button type="danger" style="width:40%;margin-left:20%" @click="mulcloseCfm()">确认</el-button>
            </div>
        </el-dialog>
        <el-dialog title="从Excel导入" :visible.sync="importV" width="60%" :before-close="importClose"><!--:headers="PHead"-->
            <el-upload id="uploader" class="el-upload" action="api/Upload/postExcel"  :on-success="handleUpload" accept=".xls, .xlsx" :show-file-list="false" >从Excel导入</el-upload>
            <div id="chooseTab" style="visibility:hidden">
            <div style="display:inline-block">请选择需要导入的表格</div>
            <el-select v-model="selVal" placeholder="请选择">
                <el-option v-for="item in dropMenu" :key="item.value" :label="item.label" :value="item.value"> </el-option>
            </el-select>
            <el-button type="primary" @click="preview">预览选中表格</el-button>
            <el-button type="warning" @click="reUpload">重新上传</el-button>
            </div>
             <el-divider content-position="left">数据预览</el-divider>
              <el-table :data="importedData" style="width: 100%"  height="350" ><!--tableData.slice((currPage-1)*pageSize,currPage*pageSize)-->
                <el-table-column prop="orderNo" label="订单号"></el-table-column>
                <el-table-column prop="wlNo" label="物料号"></el-table-column>
                <el-table-column prop="wlDesc" label="物料描述"></el-table-column>
                <el-table-column prop="orderNum" label="订单数量" ></el-table-column>
                <el-table-column prop="planStime" label="计划开始日期" width="100"></el-table-column>
                <el-table-column prop="planEtime" label="计划完工日期" width="100"></el-table-column>
                <el-table-column prop="orderedTime" label="生产下达日期" width="100"></el-table-column>
                <el-table-column prop="prodStime" label="生产开始日期" width="100"></el-table-column>
                <el-table-column prop="prodEtime" label="生产完工日期" width="100"></el-table-column>
                <el-table-column prop="passNum" label="合格数量"></el-table-column>
                <el-table-column prop="failNum" label="不合格数量"></el-table-column>
                <el-table-column prop="srcStn" label="来源车间" ></el-table-column>
                <el-table-column prop="status" label="状态" ></el-table-column>
                <el-table-column prop="ordType" label="订单类型"></el-table-column>
                <el-table-column prop="tip" label="备注"></el-table-column>
            </el-table>
            <el-button type="primary" @click="mulNewData()">确定</el-button>
        </el-dialog>
    </div>
</template>
<script>
import XLSX from 'xlsx'
export default {
    data(){
        return {
            //产线选择
            lineMenu:["EGR阀线","电子线","Cooler/Module线","全部"],
            selProdVal:null,
            importV:false,
            tableData:[],
            importedData:[],
            singlePage:true,
            currPage:1,
            pageSize:8,
            qInfo:null,
            //弹出框表单参数
            dialogVisible:false,
            //新建表单
            form:{
                ordNo:'',
                wlNo:'',
                wlDesc:'',
                ordNum:0,
                sTime:'',
                eTime:'',
                ordTime:new Date(),
                station:'',
                status:'创建',
                ordType:'',
                tip:''
            },
            rule:{
                ordNo:[{required:true,message:'请输入订单编号',trigger:'blur'}],
                wlNo:[{required:true,message:'物料编号也要输入的哦!',trigger:'blur'}],
                ordNum:[{required:true,message:'订单数量有多少呢？',trigger:'blur'}],
                station:[{required:true,message:'车间在哪？',trigger:'blur'}],
                ordType:[{required:true,message:'我是什么类型的订单呀？',trigger:'blur'}],
            },
            deleteVis:false,
            delInfo:null,
            searchItem:'',
            multipleSelection:[],
            //下拉菜单
            dropMenu:[],
            selVal:null,
            filePath:null,
            //编辑表单
            editForm:{
                ordNo:'',
                wlNo:'',
                wlDesc:'',
                ordNum:null,
                sTime:'',
                eTime:'',
                ordTime:'',
                station:'',
                status:'',
                ordType:'',
                tip:''
            },
            editDialog:false,
            editRule:{
                sTime:[{required:true,message:'日期不能为空',trigger:'blur'}],
                eTime:[{required:true,message:'日期不能为空',trigger:'blur'}],
                wlNo:[{required:true,message:'物料编号也要输入的哦!',trigger:'blur'}],
                ordNum:[{required:true,message:'订单数量有多少呢？',trigger:'blur'}],
                station:[{required:true,message:'车间在哪？',trigger:'blur'}],
                ordType:[{required:true,message:'我是什么类型的订单呀？',trigger:'blur'}],
            },
            //操作按钮显影
            btnVis:false,
            //批量关闭参数
            mulCloseVis:false,
        }
    },
    methods:{
        //表格格式函数
        rowStyle({row}){
            if(row.status == '生产中'){
                 return 'product-row';
            }else if(row.status == '完工'){
                 return 'finished-row';
            }else if(row.status == '下达'){
                 return 'ordered-row';
            }else{
                return '';
            }
           
        },        
        //表格数据按钮
        //下达
        order(row){
            console.log(row);
            if(localStorage.getItem("ms_username") == "Sean" || localStorage.getItem("ms_username") == "Chuck Yu" || localStorage.getItem("ms_username") == "eying" || localStorage.getItem("ms_username") == "sophia" || localStorage.getItem("ms_username") == "oliver" || localStorage.getItem("ms_username") == "Aron"){
                fetch('api/OrderRelease/xiadaOrder',{
                    method:'POST',
                    headers:{
                        'Content-Type':'application/json'
                    },
                    body:JSON.stringify({
                        ordNo:row.orderNo,
                        wlNo:row.wlNo,
                        workNo:row.orderNo,
                        ordNum:row.orderNum,
                        sTime:row.planStime,
                        eTime:row.planEtime,
                        station:row.srcStn
                    })
                }).then(response=>response.json())
                .then(data=>{
                    if(data[0].resSign){
                        this.$message.success("下达成功！")
                        this.search('all');
                    }else{
                        this.$message.error("物料编号与所属车间不对应或者物料编号有误，我无法找到该订单物料编号对应的工艺路径，请核对后重新编辑该订单的物料编号或所属车间")
                    }
                }).catch(data=>{
                    alert(data);
                })

            }else{
                this.$message.error("你没有权限！"); 
            }
            
        },
        //批量下达
        xiadaData(){
            if(this.multipleSelection.length == 0){
                this.$message.warning("当前无任何勾选数据，如需批量下达，请勾选对应数据，如需单独下达，请点击下达按妞！")
               return;
            }
            for(var item of this.multipleSelection){
                if(item.status != '创建'){
                    this.$message.error("勾选的订单包含生产中，已下达或者已完工的订单，下达操作只适用于新创建的订单！")
                    return;
                }
            }
            for(var item of this.multipleSelection){
               if(localStorage.getItem("ms_username") == "Sean" || localStorage.getItem("ms_username") == "Chuck Yu" || localStorage.getItem("ms_username") == "eying" || localStorage.getItem("ms_username") == "sophia" || localStorage.getItem("ms_username") == "oliver" || localStorage.getItem("ms_username") == "Aron"){
                    fetch('api/OrderRelease/xiadaOrder',{
                        method:'POST',
                        headers:{
                            'Content-Type': 'application/json'
                        },
                        body:JSON.stringify({
                            ordNo:item.orderNo,
                            wlNo:item.wlNo,
                            workNo:item.orderNo,
                            ordNum:item.orderNum,
                            sTime:item.planStime,
                            eTime:item.planEtime,
                            station:item.srcStn

                        })
                    })
                    .then(response=>response.text())
                    .then(data=>{
                        console.log(data)
                         if(data[0].resSign){
                            this.$message.success("下达成功！")
                              this.deleteVis = false;
                            this.search('all');
                        }else{
                            this.$message.error("物料编号"+item.wlNo+"与所属车间不对应或者物料编号有误，我无法找到该订单物料编号对应的工艺路径，请核对后重新编辑该订单的物料编号或所属车间")
                        }
                    })
                    .catch(data=>{
                        alert(data)
                    })
                   
                }else{
                    this.$message.error("你没有权限！")
                }
            } 
        },
        //删除单行
        deleteAcc(row){
            this.deleteVis = true;
            this.delInfo = row.orderNo;
            console.log(row.orderNo);
        },
        //删除提示确认
        deleteComfirm(){
            if(localStorage.getItem("ms_username") == "Sean" || localStorage.getItem("ms_username") == "Chuck Yu" || localStorage.getItem("ms_username") == "eying" || localStorage.getItem("ms_username") == "sophia" || localStorage.getItem("ms_username") == "oliver" || localStorage.getItem("ms_username") == "Aron"){
                 fetch('api/OrderRelease/delOrder',{
                    method:'POST',
                    headers:{
                        'Content-Type': 'application/json'
                    },
                    body:JSON.stringify({
                        ordNo:this.delInfo,
                    })
                })
                .then(response=>response.text())
                .then(data=>{
                    console.log(data)
                    this.deleteVis = false;
                    this.$message.success("关闭成功！")
                })
                .catch(data=>{
                    alert(data)
                })
            }else{
                this.$message.error("你没有权限！")
            }
           
        },
        //编辑单行
        handleClick(row){
            console.log(row);
            this.editForm.ordNo = row.orderNo;
            this.editForm.wlNo = row.wlNo;
            this.editForm.wlDesc = row.wlDesc;
            this.editForm.ordNum = row.orderNum;
            this.editForm.sTime = row.planStime;
            this.editForm.eTime = row.planEtime;
            this.editForm.ordTime = row.orderedTime;
            this.editForm.station = row.srcStn;
            this.editForm.status = row.status;
            this.editForm.ordType = row.ordType;
            this.editForm.tip = row.tip;
            this.editDialog = true
        },
        //新增模态框关闭确认
        handleClose(done) {
            this.$confirm('确认关闭？')
            .then(_ => {
                this.$refs['form'].resetFields();
                done();
            })
            .catch(_ => {});
        },
        //编辑模态框关闭
        editClose(done) {
            this.$confirm('确认关闭？')
            .then(_ => {
                this.$refs['editForm'].resetFields();
                done();
            })
            .catch(_ => {});
        },
        editCancel(){
            this.$refs['editForm'].resetFields();
            this.editDialog = false;
        },
        editFinish(formName){
            this.$refs[formName].validate((valid) => {
                if (valid) {
                    if(localStorage.getItem("ms_username") == "Sean" || localStorage.getItem("ms_username") == "Chuck Yu" || localStorage.getItem("ms_username") == "eying" || localStorage.getItem("ms_username") == "sophia" || localStorage.getItem("ms_username") == "oliver" || localStorage.getItem("ms_username") == "Aron"){
                        fetch('api/OrderRelease/editOrder',{
                            method:'POST',
                            headers:{
                                'Content-Type': 'application/json'
                            },
                            body:JSON.stringify({
                                ordNo:this.editForm.ordNo,
                                wlNo:this.editForm.wlNo,
                                wlDesc:this.editForm.wlDesc,
                                ordNum:Number(this.editForm.ordNum),
                                sTime: this.editForm.sTime,
                                eTime: this.editForm.eTime,
                                ordTime: this.editForm.ordTime,
                                station:this.editForm.station,
                                status:this.editForm.status,
                                ordType:this.editForm.ordType,
                                tip:this.editForm.tip
                            })
                        })
                        .then(response=>response.text())
                        .then(data=>{
                            console.log(data) 
                            this.$refs['editForm'].resetFields();
                           this.editDialog = false;
                        })
                        .catch(data=>{
                            alert(data)
                        })
                    }else{
                        this.$message.error("你没有该操作的权限！")
                    }
                } else {
                    console.log('error submit!!');
                    return false;
                }
            })
        },
        //分页函数，暂未启用
        // changePage(event){
        //     this.currPage = event;
        // },
        //搜索按钮，暂未开发
        search(str){
            // console.log('you want to search '+str);
            if(this.selProdVal == null){
                this.$message.error("你还米有选择产线呢！");
                return;
            }
            var srhItem = '';
            if(str == 'all'){
                srhItem = this.searchItem
            }else{
                srhItem = str;
            }
            // console.log(srhItem);
            fetch('api/OrderRelease/getOrder',{
                method:'POST',
                headers:{
                     'Content-Type': 'application/json'
                },
                body:JSON.stringify({
                    pline:this.selProdVal,
                    srhCont:srhItem
                })
            }) .then(response=>response.json())
                .then(data=>{
                    console.log(data)
                    this.tableData = []
                    for(var item of data){
                        var tmpData = {
                            orderNo:item.作业单号,
                            wlNo:item.物料号,
                            wlDesc:item.物料描述,
                            orderNum:item.订单数量,
                            planStime:item.计划开始日期.substr(0,10),
                            planEtime:item.计划完工日期.substr(0,10),
                            orderedTime:item.生产下达日期.substr(0,10),
                            prodStime:item.生产开始日期.substr(0,10),
                            prodEtime:item.生产完工日期.substr(0,10),
                            passNum:item.合格数量,
                            failNum:item.不合格数量,
                            srcStn:item.来源车间,
                            status:item.状态,
                            ordType:item.订单类型,
                            tip:item.备注
                        }
                        this.tableData.push(tmpData);
                    }
                })
                .catch(data=>{
                    alert(data);
                })
        },
        //选择示踪，用于数据的批量处理
        handleSelectionChange(val) {
        this.multipleSelection = val;
        console.log(this.multipleSelection);
         console.log(this.multipleSelection.length);
      },
      //表格上传成功后的操作
       handleUpload(response){
           document.getElementById("uploader").style.pointerEvents = "none";
           document.getElementById("chooseTab").style.visibility="visible";
           console.log("EXCEL")
           console.log(response[0]);
           this.dropMenu = [];
           for(var i=0; i<response.length-1; i++){
               var ind = {label:0,value:response[i].sname};
               this.dropMenu.push(ind);
           }
           this.filePath = response[response.length-1].sname;

       },
       //预览表格及选项的显示
       showImport(){
           this.importV = true;
       },
       //
       uploadMtd(param){
           console.log("this is Upload");
           console.log(param)
       },
       //预览设置
       preview(){
           if(this.selVal == null){
               this.$message.warning("请选择表名！");
               return;
           }
           fetch('api/Upload/getExcel',{
               method:'POST',
               headers:{
                   'Content-Type': 'application/json'
               },
               body:JSON.stringify({
                   sn:this.selVal,
                   fs:this.filePath
               })
           })
           .then(response=>response.json())
           .then(data=>{
               console.log(data)
               this.importedData = []
               if(data[0].length == 1){
                    this.$message.error("表格无数据，请核对");
                    return;
               }
               if(data[0].length != 11){
                   this.$message.error("表格格式与订单表格不匹配，"+(data[0][0]+1)+",第"+data[0][1]+"行,第"+(data[0][2]+1)+"列字段非法，请选择其他表格或重新上传excel文件！")
                   
                   return;
               }
               for(var item of data){
                   var sub = {orderNo:item[0],wlNo:item[1], wlDesc:item[2],orderNum:item[3],planStime:item[4],planEtime:item[5],orderedTime:item[6],prodStime:null,prodEtime:null,passNum:null,failNum:null,srcStn:item[7],status:item[8],ordType:item[9],tip:item[10]};
                   this.importedData.push(sub);
               }
           })
           .catch(data=>{
               alert(data)
           })
           
       },
       //重新上传
       reUpload(){
           document.getElementById("uploader").style.pointerEvents = "auto";
           document.getElementById("chooseTab").style.visibility="hidden";
           this.dropMenu = [];
           this.selVal = null;
           fetch('api/Upload/delExcel',{
               method:'POST',
               headers:{
                   'Content-Type': 'application/json'
               },
               body:JSON.stringify({
                   sname:this.filePath
               })
           })
           this.importedData = []
       },
       //导出到excel
       exportExl(){
           if(this.multipleSelection.length == 0){
               this.$message.warning("当前无任何勾选数据，如需导出表格所有数据，请勾选所有！")
               return;
           }
           //创建book
            var wb = XLSX.utils.book_new();
            //json转sheet
            var ws = XLSX.utils.json_to_sheet(this.multipleSelection, {header:["orderNo","wlNo","wlDesc","orderNum","planStime","planEtime","orderedTime","prodStime","prodEtime","passNum","failNum","srcStn","status","ordType","tip"], skipHeader:false});
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
                {width: 15}
            ];
            var timestamp = (new Date()).getTime();
            //sheet写入book
            XLSX.utils.book_append_sheet(wb, ws, "file");
            //输出
            XLSX.writeFile(wb,"file"+timestamp+".xlsx");
       },
       //新增数据
       addData(){
           this.dialogVisible = true;
       },
       //批量关闭弹出
       delData(){
           if(this.multipleSelection.length == 0){
                this.$message.warning("当前无任何勾选数据，如需批量关闭，请勾选对应数据，如需单行删除，请点击删除按妞！")
               return;
           }
            this.mulCloseVis=true;
           
           
       },
       //批量关闭确认
        mulcloseCfm(){
            for(var item of this.multipleSelection){
                if(localStorage.getItem("ms_username") == "Sean" || localStorage.getItem("ms_username") == "Chuck Yu" || localStorage.getItem("ms_username") == "eying" || localStorage.getItem("ms_username") == "sophia" || localStorage.getItem("ms_username") == "oliver" || localStorage.getItem("ms_username") == "Aron"){
                    fetch('api/OrderRelease/delOrder',{
                        method:'POST',
                        headers:{
                            'Content-Type': 'application/json'
                        },
                        body:JSON.stringify({
                            ordNo:item.orderNo,
                        })
                    })
                    .then(response=>response.text())
                    .then(data=>{
                        console.log(data)
                        this.mulCloseVis = false;
                        this.$message.success("关闭成功！")
                    })
                    .catch(data=>{
                        alert(data)
                    })
                    
                }else{
                    this.$message.error("你没有权限！")
                }
            } 
        },
       //新增弹窗确认关闭
       addFinish(formName){
            this.$refs[formName].validate((valid) => {
                if (valid) {
                    if(localStorage.getItem("ms_username") == "Sean" || localStorage.getItem("ms_username") == "Chuck Yu" || localStorage.getItem("ms_username") == "eying" || localStorage.getItem("ms_username") == "sophia" || localStorage.getItem("ms_username") == "oliver" || localStorage.getItem("ms_username") == "Aron"){
                        fetch('api/OrderRelease/newOrder',{
                            method:'POST',
                            headers:{
                                'Content-Type': 'application/json'
                            },
                            body:JSON.stringify({
                                ordNo:this.form.ordNo,
                                wlNo:this.form.wlNo,
                                wlDesc:this.form.wlDesc,
                                ordNum:Number(this.form.ordNum),
                                sTime: this.form.sTime,
                                eTime: this.form.eTime,
                                ordTime: this.form.ordTime,
                                station:this.form.station,
                                status:this.form.status,
                                ordType:this.form.ordType,
                                tip:this.form.tip
                            })
                        })
                        .then(response=>response.text())
                        .then(data=>{
                            console.log(data) 
                            this.$refs['form'].resetFields();
                            this.$message.success("创建成功！")
                            this.dialogVisible = false;
                        })
                        .catch(data=>{
                            alert(data)
                        })
                        console.log(this.form.ordType)
                       
                    }else{
                        this.$message.error("你没有该操作的权限！")
                    }
                   
                } else {
                    console.log('error submit!!');
                    return false;
                }
            });
           
       },
       //新增弹窗取消关闭
       addClose(){
           this.$refs['form'].resetFields();
           this.dialogVisible = false;
       },
       //批量上传确认按钮
       mulNewData(){
           if(localStorage.getItem("ms_username") == "Sean" || localStorage.getItem("ms_username") == "Chuck Yu" || localStorage.getItem("ms_username") == "eying" || localStorage.getItem("ms_username") == "sophia" || localStorage.getItem("ms_username") == "oliver" || localStorage.getItem("ms_username") == "Aron"){
                fetch('api/Upload/createData',{
                    method:'POST',
                    headers:{
                        'Content-Type': 'application/json'
                     },
                    body:JSON.stringify({
                        sn:this.selVal,
                        fs:this.filePath,
                        pline:this.selProdVal,
                        srhCont:this.searchItem

                    })
                })
                .then(response=>response.json())
                .then(data=>{
                    this.tableData = []
                    for(var item of data){
                        var tmpData = {
                            orderNo:item.作业单号,
                            wlNo:item.物料号,
                            wlDesc:item.物料描述,
                            orderNum:item.订单数量,
                            planStime:item.计划开始日期.substr(0,10),
                            planEtime:item.计划完工日期.substr(0,10),
                            orderedTime:item.生产下达日期.substr(0,10),
                            prodStime:item.生产开始日期.substr(0,10),
                            prodEtime:item.生产完工日期.substr(0,10),
                            passNum:item.合格数量,
                            failNum:item.不合格数量,
                            srcStn:item.来源车间,
                            status:item.状态,
                            ordType:item.订单类型,
                            tip:item.备注
                        }
                        this.tableData.push(tmpData);
                    }
                document.getElementById("chooseTab").style.visibility="hidden";
                document.getElementById("uploader").style.pointerEvents = "auto";
                this.dropMenu = []
                this.selVal = ''
                this.importedData = []
                this.importV = false;
                this.$message.success("导入成功！");
                 fetch('api/Upload/delExcel',{
                        method:'POST',
                        headers:{
                            'Content-Type': 'application/json'
                        },
                        body:JSON.stringify({
                            sname:this.filePath
                        })
                    })
                }).catch(data=>{
                    alert(data);
                })
               
           }else{
               this.$message.error("你没有权限！");
           }
       },
       importClose(done){
           this.$confirm('该操作可能会使得已上传的文件被清除，确认关闭吗？')
            .then(_ => {
                document.getElementById("chooseTab").style.visibility="hidden";
                document.getElementById("uploader").style.pointerEvents = "auto";
                this.dropMenu =[];
                this.selVal = null;
                this.importedData = [];
                fetch('api/Upload/delExcel',{
                        method:'POST',
                        headers:{
                            'Content-Type': 'application/json'
                        },
                        body:JSON.stringify({
                            sname:this.filePath
                        })
                    })
                done();
            })
            .catch(_ => {});
       }

    },
    mounted(){
        
    }
}
</script>
<style scoped>
/deep/.el-upload--text{
    background-color:#fff;
    border: 1px solid #DCDFE6;
    font-size:30px;
    color:#606266;
    border-radius:3px;
    -webkit-box-sizing:border-box;
    box-sizing:border-box;
    width:1100px;
    height:100px;
    text-align:center;
    cursor:pointer;
    line-height:100px;
    user-select:none;

}
/deep/.el-upload--text:hover{
    color:#409eff;
    border-color:#c6e2ff;
    background-color:#ecf5ff;
}

.search{
    margin-right:5%;
    /* float:right; */
}

/deep/.el-table .product-row {
    background:yellow;
}

/deep/.el-table .finished-row {
    background: #22B14C;
}
/deep/.el-table .ordered-row{
      background: #aadde7;
}
.cBlock{
    width:50px;
    height:30px;
    margin:0% 2%;
    display:inline-block;
    border-radius:10%;
    line-height:30px;
    text-align:center;
    -moz-user-select: none;
    cursor: pointer;
}
</style>