<template>
    <div>
        <el-row>
            <el-col :span="6">
                <el-card>
                    <el-input placeholder=">订单号/物料号<" prefix-icon="el-icon-search" v-model="searchItem" @keyup.enter.native="search()" style="width:200px;display:inline-block"> </el-input>
                    <el-button type="primary" @click="search()">搜索</el-button>           
                </el-card>
                <el-card>
                    <div class="infinite-list-wrapper" style="overflow:auto">
                        <ul  class="list" v-infinite-scroll="load" infinite-scroll-disabled="disabled">
                            <li v-for="i in listItems" :key="i" class="list-item">{{ i }}</li>
                        </ul>
                    </div>
                </el-card>
            </el-col>
            <el-col :span="18">
                <el-card >
                    <el-table :data="tableData" style="width: 100%;height:320px"  ><!-- @selection-change="handleSelectionChange" tableData.slice((currPage-1)*pageSize,currPage*pageSize)-->
                        <!-- <el-table-column type="selection" width="55" ></el-table-column> -->
                        <el-table-column prop="orderNo" label="顺序号" width="100"></el-table-column>
                        <el-table-column prop="workNo" label="作业单号"></el-table-column>
                        <el-table-column prop="wlNo" label="物料编号"></el-table-column>
                        <el-table-column prop="wlDesc" label="物料描述" ></el-table-column>
                        <el-table-column prop="proceNo" label="工序号" width="100"></el-table-column>
                        <el-table-column prop="proceName" label="工序名称"></el-table-column>
                        <el-table-column prop="startDate" label="开始日期"></el-table-column>
                        <el-table-column prop="endDate" label="完工日期" ></el-table-column>
                        <el-table-column prop="leftHr" label="剩余工时"></el-table-column>
                        <el-table-column prop="testHr" label="调试计划用时"></el-table-column>
                        <el-table-column fixed="right" label="操作" >
                            <template slot-scope="scope">
                                <el-button @click="order(scope.row)" type="success" size="small">修改</el-button>
                            </template>
                        </el-table-column>
                    </el-table>
                </el-card>

                <el-card style="margin-top:2%">
                    <el-table :data="tableData" style="width: 100%;height:260px"  ><!-- @selection-change="handleSelectionChange" tableData.slice((currPage-1)*pageSize,currPage*pageSize)-->
                        <!-- <el-table-column type="selection" width="55" ></el-table-column> -->
                        <el-table-column  prop="bgNo" label="报工编号" width="100"></el-table-column>
                        <el-table-column  prop="workNo" label="作业单号"></el-table-column>
                        <el-table-column  prop="initMan" label="创建人"></el-table-column>
                        <el-table-column  prop="initDate" label="创建日期" ></el-table-column>
                        <el-table-column  prop="wlNo" label="物料编号" width="100"></el-table-column>
                        <el-table-column  prop="wlDesc" label="物料描述"></el-table-column>
                        <el-table-column  prop="proceNo" label="工序号"></el-table-column>
                        <el-table-column  prop="proceName" label="工序名称" ></el-table-column>
                        <el-table-column  prop="bgType" label="报工类型" width="100"></el-table-column>
                        <el-table-column  prop="fixNum" label="定额件数"></el-table-column>
                        <el-table-column  prop="prcsUnit" label="加工单元"></el-table-column>
                        <el-table-column  prop="prcsMan" label="打工人员" ></el-table-column>
                        <el-table-column  prop="birth" label="生产日期" width="100"></el-table-column>
                        <el-table-column  prop="testStime" label="调试开始时间"></el-table-column>
                        <el-table-column  prop="testTime" label="调试时间"></el-table-column>
                        <el-table-column  prop="testPause" label="调试暂停" ></el-table-column>
                        <el-table-column  prop="testEtime" label="调试结束时间" width="100"></el-table-column>
                        <el-table-column  prop="workStime" label="作业开始时间"></el-table-column>
                        <el-table-column  prop="planEtime" label="计划停机时间"></el-table-column>
                        <el-table-column  prop="uxpcEtime" label="非计划停机时间" ></el-table-column>
                        <el-table-column  prop="downtime" label="作业完工时间" width="100"></el-table-column>
                        <el-table-column  prop="downNum" label="完工数量"></el-table-column>
                        <el-table-column  prop="badNum" label="不良数量"></el-table-column>
                        <el-table-column  prop="badNo" label="不合格编号" ></el-table-column>
                        <el-table-column  prop="status" label="状态"></el-table-column>
                        <el-table-column  prop="fixmanNum" label="定额人数"></el-table-column>
                        <el-table-column  prop="workGp" label="工作组" ></el-table-column>
                        <el-table-column fixed="right" label="操作" >
                            <template slot-scope="scope">
                                <el-button @click="order(scope.row)" type="success" size="small">下达</el-button>
                                <el-button @click="handleClick(scope.row)" type="primary" size="small">编辑</el-button>
                                <el-button type="danger" size="small" @click="deleteAcc(scope.row)">删除</el-button>
                                
                            </template>
                        </el-table-column>
                    </el-table>
                </el-card>

                <el-card style="margin-top:2%">
                    <el-table :data="tableData" style="width: 100%;height:220px"  ><!-- @selection-change="handleSelectionChange" tableData.slice((currPage-1)*pageSize,currPage*pageSize)-->
                        <!-- <el-table-column type="selection" width="55" ></el-table-column> -->
                        <el-table-column prop="user" label="用户名" width="100"></el-table-column>
                        <el-table-column prop="name" label="姓名"></el-table-column>
                        <el-table-column prop="certificate" label="许可证"></el-table-column>
                        <el-table-column prop="date" label="创建日期" ></el-table-column>
                        <el-table-column fixed="right" label="操作" >
                            <template slot-scope="scope">
                                <el-button @click="order(scope.row)" type="success" size="small">下达</el-button>
                                <el-button @click="handleClick(scope.row)" type="primary" size="small">编辑</el-button>
                                <el-button type="danger" size="small" @click="deleteAcc(scope.row)">删除</el-button>
                            </template>
                        </el-table-column>
                    </el-table>
                </el-card>
            </el-col>
        </el-row>
    
    </div>
</template>

<script>
export default {
    data(){
        return{
            listItems:["list1","list2","list3","list4"]
        }
    }
}
</script>