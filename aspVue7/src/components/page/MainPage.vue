<template>
<div class="mainpage">
    <div style="margin:0% 10% 5%">
    <el-row>
       
        <div class="title"> <img src='../../assets/img/bwlogo.png' style="float:left;margin-top:5px;"/>BorgWarner Mis System Panel</div>
    </el-row>
    <el-row>
         <el-divider style="margin:10%;"><i class="el-icon-mobile-phone"></i></el-divider>
    </el-row>
    <el-row>
        <el-col :span="3">
            <div id="dashboard" class="mode" type="button" @click="go($event)">
                <h3 class="subTitle">系统首页</h3>
                <i class="iconfont el-icon-blinklightfangzi"></i>
                </div>
           
        </el-col>

        <el-col :span="3">
            <div id="andon" class="mode" type="button" @click="go($event)">
                <h3 class="subTitle">安灯系统</h3>
                <i class="iconfont el-icon-blinklightlight"></i>
                </div>
        </el-col>

        <el-col :span="3">
            <div id="oee" type="button" class="mode"  @click="go($event)">
                <h3 class="subTitle">OEE</h3>
                <i class="iconfont el-icon-blinklighttubiao"></i>
                </div>
        </el-col>

        <el-col :span="3">
            <div id="account" type="button" class="mode"  @click="accountM()">
                <h3 class="subTitle">账号管理</h3>
                <i class="iconfont el-icon-blinklightkey"></i>
                <!-- <i class="iconfont el-icon-blinklightsuo"></i> -->
                </div>
        </el-col>

        <el-col :span="3">
            <div class="mode"><h3 class="subTitle">保留页1</h3></div>
        </el-col>

        <el-col :span="3">
            <div class="mode"><h3 class="subTitle">保留页2</h3></div>
        </el-col>

        <el-col :span="3">
            <div class="mode"><h3 class="subTitle">保留页3</h3></div>
        </el-col>

        <el-col :span="3">
            <div id="exit" class="mode" @click="exit()">
                <h3 class="subTitle">返回登陆</h3>
                <i class="iconfont el-icon-blinklighttuichu"></i>
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
    }
}
</script>

<style scoped>
.mainpage{
    position: relative;
    width: 100%;
    height: 100%;
    /* background:#000; */
   
    background-image: url(../../assets/img/test_bg.jpeg);
    background-size: 100%;
}
.title{
    font-size:50px;
    font-weight:bold;
    margin:2% 10%;
    text-align:center;
    line-height:50px;
}

.mode{
    height:150px;
    border:1px solid rgba(0,0,0,1);
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
    margin-top:10%;
}
</style>