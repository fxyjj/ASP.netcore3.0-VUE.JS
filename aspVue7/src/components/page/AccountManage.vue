<template>
<div class="mainpage">
    <el-row>
        <el-col :span="6">
            <div class="panel" style="height:300px;margin:30px 10px 0px 30px">
                <h3>管理员信息:<strong>{{admin}}</strong></h3>
                <h3>姓名:<strong>{{name}}</strong></h3>
                <h3>令牌:<strong>{{certificate}}</strong></h3>
            </div>
            <div class="panel" style="height:710px;margin:10px 10px 0px 30px">
                选项面板
                <router-link to="/UserManage">
                    <div class="man-btn" type="button">用户管理</div>
                </router-link>

                 <router-link to="/PwdModify">
                    <div class="man-btn" type="button">修改密码(已经弃用了，修改密码在用户管理处)</div>
                </router-link>

                 <router-link to="/CreateKey">
                     <div class="man-btn" type="button">新建许可令牌</div>
                </router-link>
                
               
              
                <div class="man-btn" type="button" @click="back()">返回</div>
            </div>
        </el-col>
        <el-col :span="18">
            <div class="panel" style="height:200px;margin:30px 30px 0px 10px">
                操作面板
                <div class="search">
                    搜索
                    <el-input
                        placeholder="请输入内容"
                        prefix-icon="el-icon-search"
                        v-model="searchItem"
                        @keyup.enter.native="search()"
                        style="width:200px;display:inline-block">
                        
                    </el-input>
                    <el-button type="primary" @click="search()">搜索</el-button>
                </div>
                </div>
            <div class="panel" style="height:810px;margin:10px 30px 0px 10px">
                表格界面
                <transition name="guodu" mode="out-in">
                    <keep-alive :include="routeList">
                        <router-view></router-view>
                    </keep-alive>
                </transition>
                </div>
        </el-col>
    </el-row>
</div>
</template>

<script>
import bus from "../common/bus"
export default {
    name:"AccountMangae",
    data(){
        return {
            admin:'Sean',
            name:'xsl',
            certificate:'123456',
            searchItem:null,
            routeList:['UM','PM','CK']
        }
    },
    methods:{
        back(){
            this.$router.push('/MainPage');
        },
        search(){
            bus.$emit('srchI',this.searchItem);
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
   
    background-image: url(../../assets/img/accBG.jpg);
    background-size: 100%;
}
.panel{
    border-radius:10px;
    border:1px solid rgba(0,0,0,1);
    background:rgba(255,255,255,0.5)
}

.man-btn{
    height:120px;
    border:1px solid rgba(0,0,0,1);
    margin:10%;
    border-radius:10px;
    text-align:center;
    line-height:120px;
    cursor:pointer;
    user-select:none;
    color:rgba(0,0,0,0.8)
}

.man-btn:hover{
    box-shadow:0px 0px 10px rgba(0,255,255,0.8);
}

.guodu-enter-active, .guodu-leave-active {
  transition: opacity .5s;
}
.guodu-enter, .guodu-leave-to /* .fade-leave-active below version 2.1.8 */ {
  opacity: 0;
}

.search{
    width:300px;
}

</style>