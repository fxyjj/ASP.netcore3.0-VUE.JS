<template>
<div class="mainpage">
    <div style="margin:0% 10% 5%">
    <el-row>
       
        <div class="title"> <img src='../../assets/img/bwlogo.png' style="float:left;margin-top:5px;"/><div style="width:80%">JS Plant MiS Panel</div></div>
    </el-row>
    <el-row>
         <el-divider style="margin:10%;background:#44546A"></el-divider>
    </el-row>
    <el-row>
        <el-col :span="2">
            <div style="visibility:hidden">这是用来填充的</div>
        </el-col>
        <el-col :span="4">
            <div id="dashboard" class="mode" style="background:#8087F6" type="button" @click="go($event)">
                <i class="iconfont el-icon-blinklightfangzi" style="line-height:150px;color:white"></i>
                <h3 class="subTitle">系统首页</h3>
            </div>
           
        </el-col>

        <el-col :span="4">
            <div id="andon" class="mode" style="background:#2ACB7B" type="button" @click="go($event)">
                <i class="iconfont el-icon-blinklightlight" style="line-height:150px;color:white"></i>
                <h3 class="subTitle">安灯系统</h3>
                </div>
        </el-col>

        <el-col :span="4">
            <div id="oee" type="button" class="mode" style="background:#FBB85F" @click="go($event)">
                <i class="iconfont el-icon-blinklighttubiao" style="line-height:150px;color:white"></i>
                <h3 class="subTitle">OEE</h3>
                </div>
        </el-col>

        <el-col :span="4">
                <div id="prodPlan" class="mode" style="background:#92D050" @click="go($event)" ><!---->
                <i class=" el-icon-folder-opened" style="line-height:150px;font-size:70px;color:white"></i>
                <h3 class="subTitle">生产计划</h3>
            </div>
        </el-col>
        <el-col :span="4">
            <div id="exit" class="mode" style="background:#BFBFBF" @click="exit()">
                <i class="iconfont el-icon-blinklighttuichu" style="line-height:150px;color:white"></i>
                <h3 class="subTitle">返回登陆</h3>
            </div>
        </el-col>
        <el-col :span="2">
            <div style="visibility:hidden">这是用来填充的</div>
        </el-col>
    </el-row>
    <el-row>
        <el-col :span="2">
            <div style="visibility:hidden">这是用来填充的</div>
        </el-col>
         <el-col :span="4"> 
             <div class="mode" style="background:#66C8CC">
                <i class="el-icon-user-solid" style="line-height:150px;font-size:70px;color:white"></i>
                <h3 class="subTitle">员工管理</h3>
            </div>
        </el-col>
        <el-col :span="4">
            <div class="mode" style="background:#7030A0">
                <i class="el-icon-setting" style="line-height:150px;font-size:70px;color:white"></i>
                <h3 class="subTitle">工艺维护</h3>
            </div>
        </el-col>
        <el-col :span="4">
            <div class="mode" style="background:#C55A11">
                <i class="iconfont el-icon-blinklightkzjh" style="line-height:150px;color:white"></i>
                <h3 class="subTitle">控制计划</h3>
            </div>
        </el-col>
        <el-col :span="4">
            <div class="mode" style="background:#006666">
                <i class="iconfont el-icon-blinklightfmea" style="line-height:150px;color:white"></i>
                <h3 class="subTitle">FMEA</h3>
            </div>
        </el-col>
         <el-col :span="4">
            <div id="workRep" class="mode" style="background:#0066FF"  @click="go($event)"><!-- -->
                <i class="el-icon-document-copy" style="line-height:150px;font-size:70px;color:white"></i>
                <h3 class="subTitle">工单管理</h3>
            </div>
        </el-col>
    </el-row>
    <el-row>
         <el-col :span="2">
            <div style="visibility:hidden">这是用来填充的</div>
        </el-col>
        <el-col :span="4">
            <div id="account" type="button" class="mode" style="background:#20D3B9" @click="accountM()" v-if="admin">
                <i class="iconfont el-icon-blinklightkey" style="line-height:150px;color:white"></i>
                 <h3 class="subTitle">账号管理</h3>
                <!-- <i class="iconfont el-icon-blinklightsuo"></i> -->
            </div>
           
        </el-col>
    </el-row>
    </div>
</div>
</template>
<script>
import bus from '../common/bus'
export default {
    name:"MainPage",
    data(){
        return{
            admin:false
        }
    },
    methods:{
        go(event){
            var toPage = event.currentTarget.id
            console.log(toPage)
            this.$router.push({
                path: '/',
                query: {
                    page: toPage
                },
            });
        },
        exit(){
            localStorage.removeItem('ms_username');
            this.$router.push('/Login');
        },
        accountM(){
            this.$router.push('/AccountManage');
        }
    },
    mounted(){
        if(localStorage.getItem("ms_username") == "Sean"){
            this.admin = true;
        }
    }
}
</script>

<style scoped>
.mainpage{
    position: relative;
    width: 100%;
    height: 100%;
    background:#D9E1F2;
   
    /* background-image: url(../../assets/img/test_bg.jpeg);
    background-size: 100%; */
}
.title{
    font-size:50px;
    /* font-weight:bold; */
    margin:2% 10%;
    text-align:center;
    line-height:50px;
}

.mode{
    height:200px;
    /* border:1px solid rgba(0,0,0,1); */
    margin:10%;
    border-radius:10%;
    text-align:center;
    cursor:pointer;
    user-select:none;
}

.mode:hover{
    background:#7FFFAA;
    box-shadow:0px 0px 10px rgba(0,255,0,1);
}

.subTitle{
    margin-top:0%;
    color:white
}
</style>