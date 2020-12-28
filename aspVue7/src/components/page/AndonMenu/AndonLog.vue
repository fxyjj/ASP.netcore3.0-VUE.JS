<template>
    <div>
        <el-card>
            <el-input placeholder="请输入内容" prefix-icon="el-icon-search" v-model="searchItem" style="width:200px;display:inline-block" @keyup.enter.native="search()"></el-input>
            <el-button type="primary" @click="search()">搜索</el-button>

            <el-button type="primary" @click="exportDate()">导出当前表格</el-button>            
        </el-card>
        <el-card>
            <el-table :data="tableData" style="width: 100%;font-size:20px"  height="860" border fit>
                <el-table-column prop="wstop" label="是否停线" width="120"></el-table-column>
                <el-table-column prop="workunit" label="工作中心" width="220"></el-table-column>
                <el-table-column prop="sbNo" label="设备编号" width="180"></el-table-column>
                <el-table-column prop="qtype" label="问题类别" width="120"></el-table-column>
                <el-table-column prop="qdesc" label="问题描述" width="320"></el-table-column>
                <el-table-column prop="adMan" label="按灯人" width="120"></el-table-column>
                
                <el-table-column prop="adTime" label="安灯时间" width="260"></el-table-column>
                <el-table-column prop="prceMan" label="处理人" width="120"></el-table-column>
                <el-table-column prop="prceTime" label="处理时间" width="260"></el-table-column>
                <el-table-column prop="tmpSolve" label="临时解决" width="120"></el-table-column>
                <el-table-column prop="failReason" label="不能解决原因" width="180"></el-table-column>
                <el-table-column prop="actionPlan" label="行动计划" width="150"></el-table-column>
                <el-table-column prop="planStime" label="计划完成日期" width="180"></el-table-column>
                <el-table-column prop="backMan" label="退回人" width="120"></el-table-column>
                <el-table-column prop="backTime" label="退回时间" width="260"></el-table-column>
                <el-table-column prop="backReason" label="退回原因" width="120"></el-table-column>
                <el-table-column prop="shitMan" label="关闭人" width="100"></el-table-column>
                <el-table-column prop="shitTime" label="关闭时间" width="260"></el-table-column>
                <el-table-column prop="cfmMan" label="确认人" width="100"></el-table-column>
                <el-table-column prop="cfmTime" label="确认时间" width="260"></el-table-column>
                <el-table-column prop="failNo" label="不合格单号" width="200"></el-table-column>
                <el-table-column prop="bgNo" label="报工单号" width="220"></el-table-column>
                <el-table-column prop="andonNo" label="安灯编号" width="200"></el-table-column>
                <el-table-column prop="status" label="当前状态" width="120"></el-table-column>
                <el-table-column prop="remind" label="是否推送" width="120"></el-table-column>
                <el-table-column prop="remindStage" label="提醒阶段" width="120"></el-table-column>
                <el-table-column prop="remindTime" label="提醒时间" width="260"></el-table-column>
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
        return {
            searchItem:'',
            tableData:[],
            //删除参数
            deladNo:'',
            delVis:false,
            //编辑参数
            editVis:false,
        }
    },
    methods:{
        search(){
            console.log(this.searchItem)
            fetch('api/Andon/logSrh',{
                method:'POST',
                headers:{
                    'Content-Type':'application/json'
                },
                body:JSON.stringify({
                    srh:this.searchItem
                })
            }).then(response=>response.json())
            .then(data=>{
                this.tableData = []
                if(data.length!=0){
                    for(var item of data){
                        var tmp={
                            wstop:item.是否停线,
                            workunit:item.工作中心,
                            sbNo:item.设备编号,
                            qtype:item.问题类别,
                            qdesc:item.问题描述,
                            adMan:item.按灯人,
                            adTime:item.按灯时间,
                            prceMan:item.处理人,
                            prceTime:item.处理时间,
                            tmpSolve:item.临时解决?1:0,
                            failReason:item.不能解决原因,
                            actionPlan:item.行动计划,
                            backMan:item.退回人,
                            backTime:item.退回时间,
                            backReason:item.退回原因,
                            shitMan:item.关闭人,
                            shitTime:item.关闭时间,
                            cfmMan:item.确认人,
                            cfmTime:item.确认时间,
                            failNo:item.不合格单号,
                            bgNo:item.报工单号,
                            andonNo:item.安灯编号,
                            status:item.当前状态,
                            remind:item.提醒阶段,
                            remindStage:item.提醒阶段,
                            remindTime:item.提醒时间
                        }
                        this.tableData.push(tmp)
                    }
                    
                }else{
                    this.$message.warning("当前搜索无匹配数据")
                }
            }).catch(data=>{
                alert(data)
            })
        },
        exportDate(){
            console.log("你想导出到excel")
            this.$message.warning("Sean已经离职了（<.*_*.>）,该功能有望下一位实习生来实现")
        },
        handleClick(row){
            console.log(row)
            if(localStorage.getItem("ms_username") == "Sean" || localStorage.getItem("ms_username") == "Chuck Yu"){
                // this.deladNo = row.andonNo;
                this.editVis = true;
                this.$message.warning("Sean还在努力开发中！敬请期待")
             }else{
                 this.$message.error("你没有权限！")
             }
        },
        deleteData(row){
            console.log(row)
            if(localStorage.getItem("ms_username") == "Sean" || localStorage.getItem("ms_username") == "Chuck Yu"){
                this.deladNo = row.andonNo;
                this.delVis = true;
                this.$message.warning("Sean已经离职了（<.*_*.>）,该功能有望下一位实习生来实现")
             }else{
                 this.$message.error("你没有权限！")
             }
        }
    }
}
</script>