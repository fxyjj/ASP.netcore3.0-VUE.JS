<template>
    <div>
        <el-card>
            
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
                <el-button type="primary" @click="search()">搜索</el-button>
                <el-button type="primary" @click="exportExl()">导出到Excel</el-button>
                <el-button type="primary" @click="addData()">新增订单</el-button>
                <el-button type="primary" @click="delData()">删除选中</el-button>
                <el-button>下达选中</el-button>
                <el-button type="primary" @click="showImport()">导入</el-button>
            </div>
        </el-card>
        <el-card>
            <el-table :data="tableData" style="width: 100%"   @selection-change="handleSelectionChange"><!--tableData.slice((currPage-1)*pageSize,currPage*pageSize)-->
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
                        <el-button @click="order(scope.row)" type="success" size="small">下达</el-button>
                        <el-button @click="handleClick(scope.row)" type="primary" size="small">编辑</el-button>
                        <el-button type="danger" size="small" @click="deleteAcc(scope.row)">删除</el-button>
                        
                    </template>
                </el-table-column>
            </el-table>
        </el-card>
      <el-dialog
        title="新增"
        :visible.sync="dialogVisible"
        width="30%"
        :before-close="handleClose">
          <el-form ref="form" :model="form" class="fromStyle" label-width="90px">
                <el-form-item label="用户名">
                    <el-input :placeholder="form.username" v-model="form.username" disabled ></el-input>
                </el-form-item>
                <el-form-item label="新密码" prop="newpwd">
                    <el-input placeholder="请输入新的密码。" v-model="form.newpwd" ></el-input>
                </el-form-item>
                <el-form-item label="确认新密码" prop="cnewpwd">
                    <el-input placeholder="请再次输入新的密码。" v-model="form.cnewpwd"></el-input>
                </el-form-item>
            </el-form>
        <span slot="footer" class="dialog-footer">
            <el-button @click="addClose()">取 消</el-button>
            <el-button type="primary" @click="addFinish()">确 定</el-button>
        </span>
          </el-dialog>
         <!--<el-dialog title="重要提示" :visible.sync="deleteVis" width="20%" style="padding:0 20px 30px">
            <i class="el-icon-warning-outline" style="color:#ff0000;font-size:100px;margin:4% 34%"></i>
            <h3 style="text-align:center">确认要删除 <strong style="color:#ff0000">{{delInfo.username}}</strong> 该用户吗？</h3>
            <div class="cbtn">
            <el-button type="success" @click="deleteVis=false" style="width:40%">取消</el-button>
            <el-button type="danger" style="width:40%;margin-left:20%" @click="deleteComfirm()">确认</el-button>
            </div>
        </el-dialog> -->
        <el-dialog title="从Excel导入" :visible.sync="importV" width="60%"><!--:headers="PHead"-->
            <el-upload class="el-upload" action="api/Upload/postExcel"  :on-success="handleUpload" accept=".xls, .xlsx" :show-file-list="false" >从Excel导入</el-upload>
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
            <el-button type="primary">确定</el-button>
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
            selProdVal:"EGR阀线",
            importV:false,
            tableData:[],
            importedData:[],
            singlePage:true,
            currPage:1,
            pageSize:8,
            qInfo:null,
            //弹出框表单参数
            dialogVisible:false,
            form:{
                username:'',
                permit:null,
                newpwd:null,
                cnewpwd:null
            },
            deleteVis:false,
            delInfo:{
                username:null,
                token:null
            },
            searchItem:'',
            multipleSelection:[],
            //下拉菜单
            dropMenu:[],
            selVal:null,
            filePath:null
        }
    },
    methods:{
        //表格数据按钮
        //下达
        order(row){
            console.log(row);
        },
        //删除单行
        deleteAcc(row){
            console.log(row);
        },
        //编辑单行
        handleClick(row){
            console.log(row);
            this.form.username = row.user;
            this.form.permit = row.certificate;
            this.dialogVisible = true
        },
        //模态框关闭确认
        handleClose(done) {
            this.$confirm('确认关闭？')
            .then(_ => {
                done();
            })
            .catch(_ => {});
        },
        //分页函数，暂未启用
        // changePage(event){
        //     this.currPage = event;
        // },
        //搜索按钮，暂未开发
        search(){
            console.log('you want to search sth!');
            fetch('api/OrderRelease/getOrder',{
                method:'POST',
                headers:{
                     'Content-Type': 'application/json'
                },
                body:JSON.stringify({
                    pline:this.selProdVal,
                    srhCont:this.searchItem
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
      },
      //表格上传成功后的操作
       handleUpload(response){
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
               for(var item of data){
                   var sub = {orderNo:item[0],wlNo:item[1], wlDesc:item[2],orderNum:item[3],planStime:item[4],planEtime:item[5],orderedTime:item[6],prodStime:item[7],prodEtime:item[8],passNum:item[9],failNum:item[10],srcStn:item[11],status:item[12],ordType:item[13],tip:item[14]};
                   this.importedData.push(sub);
               }
           })
           .catch(data=>{
               alert(data)
           })
       },
       //重新上传
       reUpload(){
           document.getElementById("chooseTab").style.visibility="hidden";
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
           //创建book
            var wb = XLSX.utils.book_new();
            //json转sheet
            var ws = XLSX.utils.json_to_sheet(this.tableData, {header:["orderNo","wlNo","wlDesc","orderNum","planStime","planEtime","orderedTime","prodStime","prodEtime","passNum","failNum","srcStn","status","ordType","tip"], skipHeader:false});
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
       //批量删除
       delData(){

       },
       //新增弹窗确认关闭
       addFinish(){
           this.dialogVisible = false;
       },
       //新增弹窗取消关闭
       addClose(){
           this.dialogVisible = false;
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
  </style>