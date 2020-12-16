<template>
    <div>
        <el-card>
            <el-input placeholder="请输入内容" prefix-icon="el-icon-search" v-model="searchItem" style="width:200px;display:inline-block" @keyup.enter.native="search()"></el-input>
            <el-button type="primary" @click="search()">搜索</el-button>

            <el-button type="primary" @click="newGY()">新建</el-button>            
        </el-card>
        <el-card>
            <el-table :data="tableData" style="width: 100%"  height="860" border fit>
                <el-table-column prop="wlNo" label="物料编号"></el-table-column>
                <el-table-column prop="station" label="工作中心"></el-table-column>
                <el-table-column prop="planCT" label="PlanChangeOverTime"></el-table-column>
                <el-table-column prop="wlDesc" label="物料描述"></el-table-column>
                <el-table-column prop="opAc" label="OpAc"></el-table-column>
                <el-table-column prop="oprTxt" label="OperationShortText"></el-table-column>
                <el-table-column prop="bNum" label="BaseQuantity"></el-table-column>
                <el-table-column prop="cGroup" label="工作组"></el-table-column>
                <el-table-column prop="type" label="类型"></el-table-column>
                <el-table-column prop="stdVal" label="StdVal"></el-table-column>
                <el-table-column prop="binCap" label="BatchCapcity"></el-table-column>
                <el-table-column prop="bchNum" label="BatchQuantity"></el-table-column>
                <el-table-column prop="prt" label="print"></el-table-column>
                <el-table-column prop=subsidy label="SubsidyAmount"></el-table-column>
                <el-table-column prop="coeff" label="EquivalentCoefficient"></el-table-column>
                <el-table-column fixed="right" label="操作" width="150px" >
                    <template slot-scope="scope">
                        <!-- <el-button @click="order(scope.row)" :style='{background:(scope.row.status=="创建"?"#85ce61":"#F5F5F5")}' size="small" :disabled="scope.row.status=='创建'?false:true">下达</el-button> -->
                        <el-button @click="handleClick(scope.row)" style="background:#F5F5F5;" size="small">编辑</el-button>
                        <el-button size="small" style="background:#F5F5F5;" @click="deleteData(scope.row)">删除</el-button>
                    </template>
                </el-table-column>
            </el-table>
        </el-card>
    </div>
</template>
<script>
export default {
    data(){
        return{
            searchItem:'',
            tableData:[],
        }
    },
    methods:{
        search(){
            console.log(this.searchItem)
        },
        handClick(row){
            console.log(row)
        },
        deleteData(row){
            consol.log(row)
        },
        newGY(){
            console.log("新建工艺路径")
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
                        prt:item.print,
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