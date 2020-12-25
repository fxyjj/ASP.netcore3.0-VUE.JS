<template>
    <div>
        <el-card>
            <el-input placeholder="请输入内容" prefix-icon="el-icon-search" v-model="searchItem" style="width:200px;display:inline-block" @keyup.enter.native="search()"></el-input>
            <el-button type="primary" @click="search()">搜索</el-button>

            <el-button type="primary" @click="newGY()">新建</el-button>            
        </el-card>
        <el-card>
            <el-table :data="tableData" style="width: 100%"  height="860" border fit>
                <el-table-column prop="wlNo" label="物料编号" width="120"></el-table-column>
                <el-table-column prop="station" label="工作中心" width="150"></el-table-column>
                <el-table-column prop="planCT" label="PlanChangeOverTime" width="150"></el-table-column>
                <el-table-column prop="wlDesc" label="物料描述" width="220"></el-table-column>
                <el-table-column prop="opAc" label="OpAc"></el-table-column>
                <el-table-column prop="oprTxt" label="OperationShortText" width="150"></el-table-column>
                <el-table-column prop="bNum" label="BaseQuantity" width="100"></el-table-column>
                <el-table-column prop="cGroup" label="工作组" width="120"></el-table-column>
                <el-table-column prop="type" label="类型"></el-table-column>
                <el-table-column prop="stdVal" label="StdVal"></el-table-column>
                <el-table-column prop="binCap" label="BatchCapcity" width="100"></el-table-column>
                <el-table-column prop="bchNum" label="BatchQuantity" width="120"></el-table-column>
                <el-table-column prop="prt" label="print"></el-table-column>
                <el-table-column prop=subsidy label="SubsidyAmount" width="120"></el-table-column>
                <el-table-column prop="coeff" label="EquivalentCoefficient" width="150"></el-table-column>
                <el-table-column fixed="right" label="操作" width="150px" >
                    <template slot-scope="scope">
                        <!-- <el-button @click="order(scope.row)" :style='{background:(scope.row.status=="创建"?"#85ce61":"#F5F5F5")}' size="small" :disabled="scope.row.status=='创建'?false:true">下达</el-button> -->
                        <el-button @click="editData(scope.row)" style="background:#F5F5F5;" size="small">编辑</el-button>
                        <el-button size="small" style="background:#F5F5F5;" @click="deleteData(scope.row)">删除</el-button>
                    </template>
                </el-table-column>
            </el-table>
        </el-card>
        <el-dialog title="确认删除" :visible.sync="delVis" width="300px">
            <div > 确认要删除 {{delwlNo}} 吗？</div>
            <el-button @click="delCfm()" type="primary">确定</el-button>
            <el-button @click="delCancel()">取消</el-button>
        </el-dialog>
        <el-dialog title="编辑" :visible.sync="editVis" :before-close="editClose" width="1300px">
            <el-form  ref="editForm" :model="editForm" :rules="editRule" label-width="120px">
                <el-row>
                    <el-col :span="6">
                        <el-form-item prop="wlNo" label="物料编号">
                            <el-input v-model="editForm.wlNo" disabled></el-input>
                        </el-form-item>
                    </el-col>
                    <el-col :span="6">
                        <el-form-item prop="station" label="工作中心">
                            <el-input v-model="editForm.station" ></el-input>
                        </el-form-item>
                    </el-col>
                    <el-col :span="6">
                        <el-form-item prop="planCT" label="PCOT">
                            <el-input v-model.number="editForm.planCT" ></el-input>
                        </el-form-item>
                    </el-col>
                    <el-col :span="6">
                        <el-form-item prop="wlDesc" label="物料描述">
                            <el-input v-model="editForm.wlDesc"></el-input>
                        </el-form-item>
                    </el-col>
                </el-row>   
                <el-row>
                    <el-col :span="6">
                        <el-form-item prop="opAc" label="OpAc">
                            <el-input v-model.number="editForm.opAc" ></el-input>
                        </el-form-item>
                    </el-col>
                    <el-col :span="6">
                        <el-form-item prop="oprTxt" label="操作简介">
                            <el-input v-model="editForm.oprTxt" ></el-input>
                        </el-form-item>
                    </el-col>
                    <el-col :span="6">
                        <el-form-item prop="bNum" label="基础数量">
                            <el-input v-model.number="editForm.bNum" ></el-input>
                        </el-form-item>
                    </el-col>
                    <el-col :span="6">
                        <el-form-item prop="cGroup" label="工作组">
                            <el-input v-model="editForm.cGroup" ></el-input>
                        </el-form-item>
                    </el-col>
                </el-row>
                <el-row>
                    <el-col :span="6">
                        <el-form-item prop="type" label="类型">
                            <el-input v-model="editForm.type"></el-input>
                        </el-form-item>
                    </el-col>
                    <el-col :span="6">
                        <el-form-item prop="stdVal" label="stdVal">
                            <el-input v-model.number="editForm.stdVal" ></el-input>
                        </el-form-item>
                    </el-col>
                    <el-col :span="6">
                        <el-form-item prop="binCap" label="额定容量">
                            <el-input v-model.number="editForm.binCap" ></el-input>
                        </el-form-item>
                    </el-col>
                    <el-col :span="6">
                        <el-form-item prop="bchNum" label="额定数量">
                            <el-input v-model.number="editForm.bchNum" ></el-input>
                        </el-form-item>
                    </el-col>
                </el-row>
                <el-row>
                    <el-col :span="6">
                        <el-form-item prop="prt" label="print">
                            <el-input v-model.number="editForm.prt"></el-input>
                        </el-form-item>
                    </el-col>
                    <el-col :span="6">
                        <el-form-item prop="subsidy" label="subsidy">
                            <el-input v-model.number="editForm.subsidy" ></el-input>
                        </el-form-item>
                    </el-col>
                    <el-col :span="6">
                        <el-form-item prop="coeff" label="eqvCoeff">
                            <el-input v-model.number="editForm.coeff" ></el-input>
                        </el-form-item>
                    </el-col>
                </el-row>
                <el-button @click="editCfm()">确认</el-button>
                <el-button @click="editCancel()">取消</el-button>
            </el-form>            
        </el-dialog>
        <!--新增-->
        <el-dialog title="新增" :visible.sync="newVis" :before-close="newClose" width="1300px">
            <el-form  ref="newForm" :model="newForm" :rules="newRule" label-width="120px">
                <el-row>
                    <el-col :span="6">
                        <el-form-item prop="wlNo" label="物料编号">
                            <el-input v-model="newForm.wlNo" ></el-input>
                        </el-form-item>
                    </el-col>
                    <el-col :span="6">
                        <el-form-item prop="station" label="工作中心">
                            <el-input v-model="newForm.station" ></el-input>
                        </el-form-item>
                    </el-col>
                    <el-col :span="6">
                        <el-form-item prop="planCT" label="PCOT">
                            <el-input v-model.number="newForm.planCT" ></el-input>
                        </el-form-item>
                    </el-col>
                    <el-col :span="6">
                        <el-form-item prop="wlDesc" label="物料描述">
                            <el-input v-model="newForm.wlDesc"></el-input>
                        </el-form-item>
                    </el-col>
                </el-row>   
                <el-row>
                    <el-col :span="6">
                        <el-form-item prop="opAc" label="OpAc">
                            <el-input v-model.number="newForm.opAc" ></el-input>
                        </el-form-item>
                    </el-col>
                    <el-col :span="6">
                        <el-form-item prop="oprTxt" label="操作简介">
                            <el-input v-model="newForm.oprTxt" ></el-input>
                        </el-form-item>
                    </el-col>
                    <el-col :span="6">
                        <el-form-item prop="bNum" label="基础数量">
                            <el-input v-model.number="newForm.bNum" ></el-input>
                        </el-form-item>
                    </el-col>
                    <el-col :span="6">
                        <el-form-item prop="cGroup" label="工作组">
                            <el-input v-model="newForm.cGroup" ></el-input>
                        </el-form-item>
                    </el-col>
                </el-row>
                <el-row>
                    <el-col :span="6">
                        <el-form-item prop="type" label="类型">
                            <el-input v-model="newForm.type"></el-input>
                        </el-form-item>
                    </el-col>
                    <el-col :span="6">
                        <el-form-item prop="stdVal" label="stdVal">
                            <el-input v-model.number="newForm.stdVal" ></el-input>
                        </el-form-item>
                    </el-col>
                    <el-col :span="6">
                        <el-form-item prop="binCap" label="额定容量">
                            <el-input v-model.number="newForm.binCap" ></el-input>
                        </el-form-item>
                    </el-col>
                    <el-col :span="6">
                        <el-form-item prop="bchNum" label="额定数量">
                            <el-input v-model.number="newForm.bchNum" ></el-input>
                        </el-form-item>
                    </el-col>
                </el-row>
                <el-row>
                    <el-col :span="6">
                        <el-form-item prop="prt" label="print">
                            <el-input v-model.number="newForm.prt"></el-input>
                        </el-form-item>
                    </el-col>
                    <el-col :span="6">
                        <el-form-item prop="subsidy" label="subsidy">
                            <el-input v-model.number="newForm.subsidy" ></el-input>
                        </el-form-item>
                    </el-col>
                    <el-col :span="6">
                        <el-form-item prop="coeff" label="eqvCoeff">
                            <el-input v-model.number="newForm.coeff" ></el-input>
                        </el-form-item>
                    </el-col>
                </el-row>
                <el-button @click="newCfm()">确认</el-button>
                <el-button @click="newCancel()">取消</el-button>
            </el-form>
        </el-dialog>
    </div>
</template>
<script>
export default {
    data(){
        return{
            searchItem:'',
            tableData:[],
            delVis:false,
            delwlNo:'',
            //编辑弹窗参数
            editVis:false,
            editForm:{
                wlNo:'',
                station:'',
                planCT:null,
                wlDesc:'',
                opAc:null,
                oprTxt:'',
                bNum:null,
                cGroup:'',
                type:'',
                stdVal:null,
                binCap:null,
                bchNum:null,
                prt:null,
                subsidy:null,
                coeff:null

            },
            editRule:{
                wlNo:[{required:true,message:'必填',trigger:'blur'}],
                station:[{required:true,message:'必填',trigger:'blur'}],
                cGroup:[{required:true,message:'必填',trigger:'blur'}],
                planCT:[{required:true,type:'number',message:'必填',trigger:'blur'}],
                opAc:[{required:true,type:'number',message:'必填',trigger:'blur'}],
                bNum:[{required:true,type:'number',message:'必填',trigger:'blur'}],
                stdVal:[{required:true,type:'number',message:'必填',trigger:'blur'}],
                binCap:[{required:true,type:'number',message:'必填',trigger:'blur'}],
                bchNum:[{required:true,type:'number',message:'必填',trigger:'blur'}],
                prt:[{required:true,type:'number',message:'必填',trigger:'blur'}],
                subsidy:[{required:true,type:'number',message:'必填',trigger:'blur'}],
                coeff:[{required:true,type:'number',message:'必填',trigger:'blur'}],
            },
            //新增弹窗参数
            newVis:false,
            newForm:{
                wlNo:'',
                station:'',
                planCT:null,
                wlDesc:'',
                opAc:null,
                oprTxt:'',
                bNum:null,
                cGroup:'',
                type:'',
                stdVal:null,
                binCap:null,
                bchNum:null,
                prt:0,
                subsidy:null,
                coeff:null

            },
            newRule:{
                wlNo:[{required:true,message:'必填',trigger:'blur'}],
                station:[{required:true,message:'必填',trigger:'blur'}],
                cGroup:[{required:true,message:'必填',trigger:'blur'}],
                planCT:[{required:true,type:'number',message:'必填',trigger:'blur'}],
                opAc:[{required:true,type:'number',message:'必填',trigger:'blur'}],
                bNum:[{required:true,type:'number',message:'必填',trigger:'blur'}],
                stdVal:[{required:true,type:'number',message:'必填',trigger:'blur'}],
                binCap:[{required:true,type:'number',message:'必填',trigger:'blur'}],
                bchNum:[{required:true,type:'number',message:'必填',trigger:'blur'}],
                prt:[{required:true,type:'number',message:'必填',trigger:'blur'}],
                subsidy:[{required:true,type:'number',message:'必填',trigger:'blur'}],
                coeff:[{required:true,type:'number',message:'必填',trigger:'blur'}],
            },
        }
    },
    methods:{
        search(){
            console.log(this.searchItem)
            fetch('api/GYMaintance/srh',{
                method:'POST',
                headers:{
                    'Content-Type':'application/json'
                },
                body:JSON.stringify({
                    srhcont:this.searchItem
                })
            }).then(response=>response.json())
            .then(data=>{
                this.tableData = []
                if(data.length != 0){
                    for(var item of data){
                        var tmp={
                            wlNo:item.wlNo,
                            station:item.station,
                            planCT:item.planCT,
                            wlDesc:item.wlDesc,
                            opAc:item.opAc,
                            oprTxt:item.oprTxt,
                            bNum:item.bNum,
                            cGroup:item.cGroup,
                            type:item.type,
                            stdVal:item.stdVal,
                            binCap:item.binCap,
                            bchNum:item.bchNum,
                            prt:item.print?1:0,
                            subsidy:item.subsidy,
                            coeff:item.coeff,
                        }
                        this.tableData.push(tmp)
                    }                    
                }else{
                    this.$message.warning("找不到该搜索条件下匹配的任何数据")
                }
            })
        },
        editData(row){
            // console.log(row),
            if(localStorage.getItem("ms_username") == "Sean" || localStorage.getItem("ms_username") == "Chuck Yu"){
                this.editForm.wlNo=row.wlNo
                this.editForm.station=row.station
                this.editForm.planCT=row.planCT
                this.editForm.wlDesc=row.wlDesc
                this.editForm.opAc=row.opAc
                this.editForm.oprTxt=row.oprTxt
                this.editForm.bNum=row.bNum
                this.editForm.cGroup=row.cGroup
                this.editForm.type=row.type
                this.editForm.stdVal=row.stdVal
                this.editForm.binCap=row.binCap
                this.editForm.bchNum=row.bchNum
                this.editForm.prt=row.prt
                this.editForm.subsidy=row.subsidy
                this.editForm.coeff=row.coeff
                this.editVis = true;
             }else{
                 this.$message.error("你没有权限！")
             }
        },
        deleteData(row){
            if(localStorage.getItem("ms_username") == "Sean" || localStorage.getItem("ms_username") == "Chuck Yu"){
                this.delwlNo = row.wlNo;
                this.delVis = true;
                //  this.$message.warning("Sean还在努力开发中！敬请期待")
             }else{
                 this.$message.error("你没有权限！")
             }
            
        },
        delCfm(){
            // console.log(row.wlNo)
            fetch('api/GYMaintance/GYdelete',{
                method:'POST',
                headers:{
                    'Content-Type':'application/json'
                },
                body:JSON.stringify({
                    wlNo:this.delwlNo
                })
            }).then(response=>response.json())
            .then(data=>{
                // console.log(data)
                // alert(data)
                if(data[0].resSign){
                    this.$message.success("删除成功")
                    this.delVis=false;
                }else{
                    this.$message.error("删除失败")
                }
            }).catch(data=>{
                alert(data)
            })
        },
        delCancel(){
            this.delVis=false;
        },
        newGY(){
            console.log("新建工艺路径")
             if(localStorage.getItem("ms_username") == "Sean" || localStorage.getItem("ms_username") == "Chuck Yu"){
                this.newVis = true;
             }else{
                 this.$message.error("你没有权限！")
             }
        },
        editCfm(){
            this.$refs['editForm'].validate((valid) => {
                if (valid) {
                    fetch('api/GYMaintance/editTab',{
                        method:'POST',
                        headers:{
                            'Content-Type':'application/json'
                        },
                        body:JSON.stringify({
                            wlNo:this.editForm.wlNo,
                            station:this.editForm.station,
                            planCT:this.editForm.planCT,
                            wlDesc:this.editForm.wlDesc,
                            opAc:this.editForm.opAc,
                            oprTxt:this.editForm.oprTxt,
                            bNum:this.editForm.bNum,
                            cGroup:this.editForm.cGroup,
                            type:this.editForm.type,
                            stdVal:this.editForm.stdVal,
                            binCap:this.editForm.binCap,
                            bchNum:this.editForm.bchNum,
                            prt:this.editForm.prt,
                            subsidy:this.editForm.subsidy,
                            coeff:this.editForm.coeff
                        })
                    }).then(response=>response.json())
                    .then(data=>{
                        if(data[0].resSign){
                            this.$message.success("修改成功")
                            this.editVis = false
                            this.$refs['editForm'].resetFields();
                        }else{
                            this.$message.error("修改失败")
                        }
                    })
                }else{
                    return false;
                }
            })
        },
        editCancel(){
            this.editVis = false
            this.$refs['editForm'].resetFields();
        },
        editClose(done){
            this.$confirm('确认关闭？')
            .then(_ => {
                this.$refs['editForm'].resetFields();
                done();
            })
            .catch(_ => {});
        },
        newCfm(){
            this.$refs['newForm'].validate((valid) => {
                if (valid) {
                    fetch('api/GYMaintance/newTab',{
                        method:'POST',
                        headers:{
                            'Content-Type':'application/json'
                        },
                        body:JSON.stringify({
                            wlNo:this.newForm.wlNo,
                            station:this.newForm.station,
                            planCT:this.newForm.planCT,
                            wlDesc:this.newForm.wlDesc,
                            opAc:this.newForm.opAc,
                            oprTxt:this.newForm.oprTxt,
                            bNum:this.newForm.bNum,
                            cGroup:this.newForm.cGroup,
                            type:this.newForm.type,
                            stdVal:this.newForm.stdVal,
                            binCap:this.newForm.binCap,
                            bchNum:this.newForm.bchNum,
                            prt:this.newForm.prt,
                            subsidy:this.newForm.subsidy,
                            coeff:this.newForm.coeff
                        })
                    }).then(response=>response.json())
                    .then(data=>{
                        if(data[0].resSign){
                            this.$message.success("修改成功")
                            this.newVis = false
                            this.$refs['newForm'].resetFields();
                        }else{
                            this.$message.error("修改失败")
                        }
                    })
                }else{
                    return false;
                }
            })
        },
        newCancel(){
            this.newVis = false
            this.$refs['newForm'].resetFields();
        },
        newClose(done){
            this.$confirm('确认关闭？')
            .then(_ => {
                this.$refs['newForm'].resetFields();
                done();
            })
            .catch(_ => {});
        }
    },
    mounted(){
        fetch('api/GYMaintance/getData',{
            method:'GET',
            headers:{
                'Content-Type':'application/json'
            }
        }).then(response=>response.json())
        .then(data=>{
            this.tableData = []
            if(data.legnth !=0){
                for(var item of data){
                    var tmp={
                        wlNo:item.wlNo,
                        station:item.station,
                        planCT:item.planCT,
                        wlDesc:item.wlDesc,
                        opAc:item.opAc,
                        oprTxt:item.oprTxt,
                        bNum:item.bNum,
                        cGroup:item.cGroup,
                        type:item.type,
                        stdVal:item.stdVal,
                        binCap:item.binCap,
                        bchNum:item.bchNum,
                        prt:item.print?1:0,
                        subsidy:item.subsidy,
                        coeff:item.coeff,
                    }
                    this.tableData.push(tmp)
                }
            }
        }).catch(data=>{
            alert(data)
        })
    }
}
</script>