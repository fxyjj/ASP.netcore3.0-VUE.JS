<template>
    <div class="panel">
        <el-card >
            dame
            <el-form ref="form" v-model="form" class="fromStyle">
                <el-form-item label="用户名">
                    <el-input placeholder="请输入要修改密码的用户名" :model="form.username"></el-input>
                </el-form-item>
                <el-form-item label="旧密码">
                    <el-input placeholder="请输入原来的密码。" :model="form.oldpwd"></el-input>
                    <el-link type="primary" @click="askKey()">忘记密码</el-link>
                </el-form-item>
                <el-form-item label="许可证" v-if="askey">
                    <el-input placeholder="请输入您的账号许可证。" :model="form.permit"></el-input>
                </el-form-item>
                <el-form-item label="新密码">
                    <el-input placeholder="请输入新的密码。" :model="form.newpwd"></el-input>
                </el-form-item>
            </el-form>
        </el-card>
    </div>
</template>
<script>
export default {
    name:"pwdModify",
    data(){
        return{
            askey:false,//if choose forget pwd,then shuffle this value and let the admin see the request form of the certificate
            form:{
                username:'',
                permit:null,
                oldpwd:null,
                newpwd:null,
            }
        }
    },
    methods:{
        askKey(){
            this.askey = true
        }
    }
}
</script>
<style scoped>
.panel{
    margin:10px;
    border:0px; 
}
</style>