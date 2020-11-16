<template>
    <div>
        <el-row>
            <el-col :span="4">
                <el-card style="width:95%;">
                    <el-input placeholder=">订单号/物料号<" prefix-icon="el-icon-search" v-model="searchItem" @keyup.enter.native="search()" style="width:200px"> </el-input>
                    <el-button style="margin:10px 50px 0px;width:100px;" type="primary" @click="search()">搜索</el-button>           
                </el-card>
                <el-card style="height:854px;width:95%;margin-top:15px;">
                    <div class="infinite-list-wrapper" style="height:800px;overflow:auto"> 
                       <el-menu default-active="2" class="el-menu-vertical-demo" @open="handleOpen" @close="handleClose">
                            <el-submenu index="1">
                                <template slot="title">
                                    <i class="el-icon-orange"></i>
                                    <span style="font-weight:bold">EGR阀线</span>
                                </template>
                                <el-menu-item-group>
                                    <el-menu-item v-for="item in EGRItems" :key="item.label" :index="item.label" @click="srhData(item.value)">{{item.value}}</el-menu-item>
                                </el-menu-item-group>
                            </el-submenu>
                            <el-submenu index="2">
                                <template slot="title">
                                <i class="el-icon-coin"></i>
                                <span style="font-weight:bold">Cooler/Module线</span>
                                </template>
                                 <el-menu-item-group>
                                    <el-menu-item  v-for="item in CMItems" :key="item.label" :index="item.label" @click="srhData(item.value)">{{item.value}}</el-menu-item>
                                    
                                </el-menu-item-group>
                            </el-submenu>
                            <el-submenu index="3">
                                <template slot="title">
                                <i class="el-icon-lightning"></i>
                                <span style="font-weight:bold">电子线</span>
                                </template>
                                <el-menu-item-group>
                                    <el-menu-item v-for="item in EleItems" :key="item.label" :index="item.label" @click="srhData(item.value)">{{item.value}}</el-menu-item>
                                </el-menu-item-group>
                            </el-submenu>
                        </el-menu>
                    </div>
                </el-card>
            </el-col>
            <el-col :span="20">
                <el-card >
                    <el-table :data="trackData1" height="320" @row-click="showtab3" style="font-size:16px"><!-- @selection-change="handleSelectionChange" tableData.slice((currPage-1)*pageSize,currPage*pageSize)-->
                        <!-- <el-table-column type="selection" width="55" ></el-table-column> -->
                        <el-table-column prop="orderNo" label="顺序号" width="120" align=center></el-table-column>
                        <el-table-column prop="workNo" label="作业单号" width="120" align=center></el-table-column>
                        <el-table-column prop="wlNo" label="物料编号"  width="140" align=center></el-table-column>
                        <el-table-column prop="wlDesc" label="物料描述" width="220" align=center></el-table-column>
                        <el-table-column prop="proceNo" label="工序号" width="100" align=center></el-table-column>
                        <el-table-column prop="proceName" label="工序名称" width="100" align=center></el-table-column>
                        <el-table-column prop="startDate" label="开始日期" width="180" align=center></el-table-column>
                        <el-table-column prop="endDate" label="完工日期" width="180" align=center></el-table-column>
                        <el-table-column prop="leftHr" label="剩余工时" width="100" align=center></el-table-column>
                        <el-table-column prop="unfnsNum" label="未完工数量" width="100" align=center></el-table-column>
                        <el-table-column prop="testHr" label="调试计划用时" width="120" align=center></el-table-column>
                        <el-table-column fixed="right" label="操作" >
                            <template slot-scope="scope">
                                <el-button @click="modify(scope.row)" type="success" size="small">修改</el-button>
                            </template>
                        </el-table-column>
                    </el-table>
                </el-card>

                <el-card style="margin-top:15px;">
                    <el-table :data="trackData2" style="width: 100%;height:260px;font-size:16px"  ><!-- @selection-change="handleSelectionChange" tableData.slice((currPage-1)*pageSize,currPage*pageSize)-->
                        <!-- <el-table-column type="selection" width="55" ></el-table-column> -->
                        <el-table-column  prop="bgNo" label="报工编号" width="200" align=center></el-table-column>
                        <el-table-column  prop="workNo" label="作业单号"  width="150" align=center></el-table-column>
                        <el-table-column  prop="initMan" label="创建人" align=center></el-table-column>
                        <el-table-column  prop="initDate" label="创建日期" width="180" align=center></el-table-column>
                        <el-table-column  prop="wlNo" label="物料编号" width="150" align=center></el-table-column>
                        <el-table-column  prop="wlDesc" label="物料描述" width="200" align=center></el-table-column>
                        <el-table-column  prop="proceNo" label="工序号" align=center></el-table-column>
                        <el-table-column  prop="proceName" label="工序名称" width="100" align=center></el-table-column>
                        <el-table-column  prop="bgType" label="报工类型" width="100" align=center></el-table-column>
                        <el-table-column  prop="fixNum" label="定额件数" width="100" align=center></el-table-column>
                        <el-table-column  prop="prcsUnit" label="加工单元" width="120" align=center></el-table-column>
                        <el-table-column  prop="prcsMan" label="打工人员" width="100" align=center></el-table-column>
                        <el-table-column  prop="birth" label="生产日期" width="180" align=center></el-table-column>
                        <el-table-column  prop="testStime" label="调试开始时间" width="180" align=center></el-table-column>
                        <el-table-column  prop="testTime" label="调试时间" width="100" align=center></el-table-column>
                        <el-table-column  prop="testPause" label="调试暂停" width="100" align=center></el-table-column>
                        <el-table-column  prop="testEtime" label="调试结束时间" width="180" align=center></el-table-column>
                        <el-table-column  prop="workStime" label="作业开始时间" width="180" align=center></el-table-column>
                        <el-table-column  prop="planEtime" label="计划停机时间" width="150" align=center></el-table-column>
                        <el-table-column  prop="uxpcEtime" label="非计划停机时间" width="150" align=center></el-table-column>
                        <el-table-column  prop="downtime" label="作业完工时间" width="180" align=center></el-table-column>
                        <el-table-column  prop="downNum" label="完工数量" width="100" align=center></el-table-column>
                        <el-table-column  prop="badNum" label="不良数量" width="100" align=center></el-table-column>
                        <el-table-column  prop="badNo" label="不合格编号" width="100" align=center></el-table-column>
                        <el-table-column  prop="status" label="状态" align=center></el-table-column>
                        <el-table-column  prop="fixmanNum" label="定额人数" width="100" align=center></el-table-column>
                        <el-table-column  prop="workGp" label="工作组" width="150" align=center></el-table-column>
                    </el-table>
                </el-card>

                <el-card style="margin-top:15px;">
                    <el-table :data="trackData3" style="width: 100%;height:240px;font-size:16px"  ><!-- @selection-change="handleSelectionChange" tableData.slice((currPage-1)*pageSize,currPage*pageSize)-->
                        <!-- <el-table-column type="selection" width="55" ></el-table-column> -->
                        <el-table-column prop="workNo" label="作业单号" align=center></el-table-column>
                        <el-table-column prop="wlNo" label="物料编号" width="150" align=center></el-table-column>
                        <el-table-column prop="wlDesc" label="物料描述" width="220" align=center></el-table-column>
                        <el-table-column prop="proceNo" label="工序号" align=center></el-table-column>
                        <el-table-column prop="proceName" label="工序名称" align=center></el-table-column>
                        <el-table-column prop="downNum" label="完工数量" align=center></el-table-column>
                        <el-table-column prop="nhdNum" label="待处理数量" align=center></el-table-column>
                        <el-table-column prop="damnNum" label="报废数量" align=center></el-table-column>
                        <el-table-column prop="degradeNum" label="降级数量" align=center></el-table-column>
                        <el-table-column prop="passNum" label="合格数量" align=center></el-table-column>
                        <el-table-column prop="status" label="状态" align=center></el-table-column>
                    </el-table>
                </el-card>
            </el-col>
        </el-row>
    <el-dialog title="修改" :visible.sync="modifyVis" :before-close="modifyClose">
         <el-form ref="modForm" :model="modForm" :rules="modRule" class="fromStyle" label-width="110px">
                <el-form-item label="顺序号" prop="orderNo">
                    <el-input v-model="modForm.orderNo" disabled></el-input>
                </el-form-item>
                <el-form-item label="作业单号" prop="workNo">
                    <el-input v-model="modForm.workNo" disabled></el-input>
                </el-form-item>
                <el-form-item label="物料编号" prop="wlNo">
                    <el-input  v-model="modForm.wlNo" disabled></el-input>
                </el-form-item>
                 <el-form-item label="物料描述" prop="wlDesc">
                    <el-input v-model="modForm.wlDesc" ></el-input>
                </el-form-item>
                <el-form-item label="工序号" prop="proceNo">
                     <el-input v-model="modForm.proceNo" disabled></el-input>
                </el-form-item>
                <el-form-item label="工序名称" prop="proceName" >
                  <el-input v-model="modForm.proceName" disabled></el-input>
                </el-form-item>
                <el-form-item label="开始日期" prop="startDate">
                     <el-date-picker v-model="modForm.startDate" type="date" placeholder="选择日期"></el-date-picker>
                </el-form-item>
                 <el-form-item label="结束日期" prop="endDate">
                     <el-date-picker v-model="modForm.endDate" type="date" placeholder="选择日期"></el-date-picker>
                </el-form-item>
                <el-form-item label="剩余时间" prop="leftHr">
                    <el-input v-model="modForm.leftHr" disabled></el-input>
                </el-form-item>
                <el-form-item label="未完工数量" prop="unfnsNum">
                    <el-input  v-model="modForm.unfnsNum" disabled></el-input>
                </el-form-item>
                 <el-form-item label="调试用时" prop="testHr">
                    <el-input v-model="modForm.testHr" disabled></el-input>
                </el-form-item>
            </el-form>
            <el-button type="success" @click="modifyCancel()" style="width:40%">取消</el-button>
            <el-button type="danger" style="width:40%;margin-left:20%" @click="modifyconfirm('modForm')">确认</el-button>
    </el-dialog>
    </div>
</template>

<script>
export default {
    data(){
        return{
            EGRItems:[{label:"11",value:"GEN_II阀装配线"},
                      {label:"12",value:"GEN_III_A+M"},
                      {label:"13",value:"GEN_III_V+F"},
                      {label:"14",value:"GEN_III_BPV"},
                      {label:"15",value:"GDI阀装配线"},
                      {label:"16",value:"LP阀装配线"},
                      {label:"17",value:"柔性线"},
                      {label:"18",value:"ETV装配线"}],

            CMItems:[{label:"21",value:"混合管装配线1"},
                      {label:"22",value:"混合管装配线2"},
                      {label:"23",value:"Cooler Line 1"},
                      {label:"24",value:"Cooler Line 2"},
                      {label:"25",value:"Cooler Line 3"},
                      {label:"26",value:"Cooler Line 4"},
                      {label:"27",value:"钎焊炉1"},
                      {label:"28",value:"钎焊炉2"},
                      {label:"29",value:"测试台1"},
                      {label:"210",value:"测试台2"},
                      {label:"211",value:"测试台3"},
                      {label:"212",value:"测试台4"},
                      {label:"213",value:"模块线1"},
                      {label:"214",value:"模块线2"}],

            EleItems:[{label:"31",value:"HV Air PTC Line"},
                      {label:"32",value:"LV Air PTC Line"},
                      {label:"33",value:"Liquid Heater Line"},
                      {label:"34",value:"eCRV Line"},
                      {label:"35",value:"Marsilli 1"},
                      {label:"36",value:"Huebers 1"},
                      {label:"37",value:"IPTE 1"},
                      {label:"38",value:"Marsilli 2"},
                      {label:"39",value:"Huebers 2"},
                      {label:"310",value:"IPTE 2"}],
            trackData1:[],
            trackData2:[],
            trackData3:[],
            currLine:null,
            searchItem:'',
            //修改弹出框参数
            modifyVis:false,
            modForm:{
                orderNo:'',
                workNo:'',
                wlNo:'',
                wlDesc:'',
                proceNo:0,
                proceName:'',
                startDate:null,
                endDate:null,
                leftHr:0,
                unfnsNum:0,
                testHr:0
            },
            modRule:{
                startDate:[{required:true,message:'开始日期是？',trigger:'blur'}],
                endDate:[{required:true,message:'结束日期呢？',trigger:'blur'}],
            },

        }
    },
    methods:{
        handleOpen(key, keyPath) {
            console.log(key, keyPath);
        },
        handleClose(key, keyPath) {
            console.log(key, keyPath);
        },
        srhData(workName){
            this.currLine = workName;
            console.log(workName);
            

            fetch('api/OrderRelease/ordTrack1',{
                method:'POST',
                headers:{
                    'Content-Type':'application/json'
                },
                body:JSON.stringify({
                    workUnit:workName
                })
            }).then(response=>response.json())
            .then(data=>{
                this.trackData1 = []
                this.trackData3 = []
                for(var item of data){
                    var tmp = {orderNo:item.顺序号,
                                workNo:item.作业单号,
                                wlNo:item.物料编号,
                                wlDesc:item.物料描述,
                                proceNo:item.工序号,
                                proceName:item.工序名称,
                                startDate:item.开始日期,
                                endDate:item.完工日期,
                                leftHr:(item.剩余工时).toFixed(2),
                                testHr:item.调试计划用时,
                                unfnsNum:item.未完工
                    }
                    this.trackData1.push(tmp);
                }
               
            }).catch(data=>{
                alert(data)
            });

            fetch('api/OrderRelease/ordTrack2',{
                method:'POST',
                headers:{
                    'Content-Type':'application/json'
                },
                body:JSON.stringify({
                    workUnit:workName
                })
            }).then(response=>response.json())
            .then(data=>{
                this.trackData2 = []
                for(var item of data){
                    var tmp = {bgNo:item.报工编号,
                                workNo:item.作业单号,
                                initMan:item.创建人,
                                initDate:item.创建日期,
                                wlNo:item.物料编号,
                                wlDesc:item.物料描述,
                                proceNo:item.工序号,
                                proceName:item.工序名称,
                                bgType:item.报工类别,
                                fixNum:item.定额件数,
                                prcsUnit:item.加工单元,
                                prcsMan:item.加工人员,
                                birth:item.生产日期,
                                testStime:item.调试开始时间,
                                testTime:item.调试时间,
                                testPause:item.调试暂停,
                                testEtime:item.调试结束时间,
                                workStime:item.作业开始时间,
                                planEtime:item.计划停机时间,
                                uxpcEtime:item.非计划停机时间,
                                downtime:item.作业完工时间,
                                downNum:item.完工数量,
                                downNum:item.不良数量,
                                badNum:item.不合格单编号,
                                status:item.状态,
                                fixmanNum:item.定额人数,
                                workGp:item.工作组
                    }
                    this.trackData2.push(tmp);
                }
                console.log(data)
            }).catch(data=>{
                alert(data)
            });
        },
        search(){
            fetch('api/OrderRelease/otSearch',{
                method:'POST',
                headers:{
                    'Content-Type':'application/json'
                },
                body:JSON.stringify({
                    workUnit:this.currLine,
                    srhCont:this.searchItem
                })
            }).then(response=>response.json())
            .then(data=>{
                this.trackData1 = []
                this.trackData3 = []
                for(var item of data){
                    var tmp = {orderNo:item.顺序号,
                                workNo:item.作业单号,
                                wlNo:item.物料编号,
                                wlDesc:item.物料描述,
                                proceNo:item.工序号,
                                proceName:item.工序名称,
                                startDate:item.开始日期,
                                endDate:item.完工日期,
                                leftHr:(item.剩余工时).toFixed(2),
                                testHr:item.调试计划用时,
                                unfnsNum:item.未完工
                    }
                    this.trackData1.push(tmp);
                }
            }).catch(data=>{
                alert(data)
            })
        },
        showtab3(row){
            console.log(row.workNo);
             fetch('api/OrderRelease/ordTrack3',{
                method:'POST',
                headers:{
                    'Content-Type':'application/json'
                },
                body:JSON.stringify({
                    workUnit:row.workNo
                })
            }).then(response=>response.json())
            .then(data=>{
                this.trackData3 = []
                for(var item of data){
                    var tmp = {
                                workNo:item.作业单号,
                                wlNo:item.物料编号,
                                wlDesc:item.物料描述,
                                proceNo:item.工序号,
                                proceName:item.工序名称,
                                downNum:item.完工数量,
                                nhdNum:item.待处理数量,
                                damnNum:item.报废数量,
                                degradeNum:item.降级数量,
                                passNum:item.合格数量,
                                status:item.状态
                               
                    }
                    this.trackData3.push(tmp);
                }
            }).catch(data=>{
                alert(data)
            })
        },
        modify(row){
            this.modForm.orderNo = row.orderNo;
            this.modForm.workNo = row.workNo;
            this.modForm.wlNo = row.wlNo;
            this.modForm.wlDesc = row.wlDesc;
            this.modForm.proceNo = row.proceNo;
            this.modForm.proceName = row.proceName;
            this.modForm.startDate = row.startDate;
            this.modForm.endDate = row.endDate;
            this.modForm.leftHr = row.leftHr;
            this.modForm.unfnsNum = row.unfnsNum;
            this.modForm.testHr = row.testHr;
            this.modifyVis = true;

        },
        modifyconfirm(formName){
            console.log(this.modForm.startDate);
            console.log(this.modForm.endDate);
            this.$refs[formName].validate((valid) => {
                if (valid) {
                    if(localStorage.getItem("ms_username") == "Sean" || localStorage.getItem("ms_username") == "Chuck Yu" || localStorage.getItem("ms_username") == "eying" || localStorage.getItem("ms_username") == "sophia" || localStorage.getItem("ms_username") == "oliver" || localStorage.getItem("ms_username") == "Aron"){
                        fetch('api/OrderRelease/mdfyDate',{
                            method:'POST',
                            headers:{
                                'Content-Type':'application/json'
                            },
                            body:JSON.stringify({
                                workNo:this.modForm.workNo,
                                sDate:this.modForm.startDate,
                                eDate:this.modForm.endDate
                            })
                        }).then(response=>response.json())
                        .then(data=>{
                            console.log(data[0].resSign);
                            if(data[0].resSign){
                                this.$message.success("修改成功！");
                                this.modifyVis = false;
                            }else{
                                this.$message.error("修改失败，完工日期须晚于开始日期！");
                            }
                        }).catch(data=>{
                            alert(data)
                        })
                    }else{
                        this.$message.error('你没有权限!');
                    }
                }else{
                    console.log('error submit!!');
                    return false;
                }
            })
        },
        modifyClose(done) {
            this.$confirm('确认关闭？')
            .then(_ => {
                this.$refs['modForm'].resetFields();
                done();
            })
            .catch(_ => {});
        },
        modifyCancel(){
            this.modifyVis = false;
            this.$refs['modForm'].resetFields();
        }
    }
}
</script>