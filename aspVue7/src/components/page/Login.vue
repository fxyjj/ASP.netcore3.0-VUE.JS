<template>
    <div class="login-wrap">
        <img src='../../assets/img/bwlogo.png' style="width:20%;margin:20px"/>
        <div class="ms-login" id="loginPage">
            
            <div class="ms-title">用户登录</div>
            <el-form :model="param" :rules="rules" ref="login" label-width="0px" class="ms-content">
                <el-form-item prop="username">
                    <el-input v-model="param.username" placeholder="username" prefix-icon="el-icon-user"></el-input>
                </el-form-item>
                <el-form-item prop="password">
                    <el-input
                        type="password"
                        placeholder="password"
                        v-model="param.password"
                        @keyup.enter.native="submitForm()"
                        prefix-icon="el-icon-lock"
                    >
                    </el-input>
                </el-form-item>
                <el-link type="primary" style="width:20%;margin-left:80%;margin-top:-30px" @click="pwdMdfy()" >忘记密码</el-link><!---->
                <div class="login-btn">
                    <el-button type="primary" @click="submitForm()" style="margin-right:13px">登录</el-button>
                    <el-button type="primary" @click="regist()" style="margin-left:13px">注册</el-button> <!---->
                </div>
                <p class="login-tips">Tips : 这里还没想好给什么tips。</p>
            </el-form>
        </div>
        <!-- 注册页面 -->
        <div class="ms-login" style="visibility:hidden" id="registPage">
           
            <div class="ms-title"> 
                <div type="button" @click="regReturn()" style="user-select:none; cursor: pointer;display:inline-block;float:left;margin-left:5%">
                    <i class="el-icon-arrow-left" ></i>
                </div>
                <div style="width:80%;margin:0% 10%"> 在这里注册你的新帐号！</div>
            </div>
            <el-form :rules="regRule" class="ms-content" :model="regParam" ref="regForm">
            <el-form-item prop="name">
                <el-input placeholder="您的姓名" prefix-icon="el-icon-user" v-model="regParam.name"></el-input>
            </el-form-item>
            <el-form-item prop="key">
                 <el-input placeholder="您的许可令牌" prefix-icon="el-icon-key" v-model="regParam.key"  ></el-input>
            </el-form-item>
            <el-form-item prop="user">
                <el-input placeholder="您的用户名" prefix-icon="el-icon-info" v-model="regParam.user"  ></el-input>
            </el-form-item>
            <el-form-item prop="pwd">
                <el-input placeholder="您的密码" prefix-icon="el-icon-lock" v-model="regParam.pwd" type="password" @change="(val)=>checkValidate(val,'reg_pwd')"  ></el-input>
                 <p id="tips" style="font-size: 12px;line-height: 15px;">{{pwdTip}}</p>
            </el-form-item>
            <el-form-item prop="Cpwd">
                <el-input placeholder="请再次输入确认密码" prefix-icon="el-icon-refresh-right" v-model="regParam.Cpwd" type="password"  ></el-input>
            </el-form-item>
            <div class="login-btn">
                <el-button type="primary" class="login-btn" style="margin:5% 0%" @click="regComfirm()">确定</el-button>
                <p id="tips2" style="font-size: 12px;line-height: 15px;">{{comfirmTip}}</p>
            </div>
            </el-form>
        </div>
        <div class="ms-login" style="text-align:center;visibility:hidden" id="resPage">
            <div v-if="regRight==null">
                <i class="el-icon-loading" style="margin:5% 10%;font-size:100px"></i>
            </div>
            <div v-else-if="regRight">
                <i class="el-icon-circle-check" style="margin:5% 10%;font-size:100px"></i>
                <p class="login-tips">注册成功！</p>
                <div class="login-btn">
                    <el-button type="primary" style="width:50%;margin:5% 10%" @click="resReturn()">返回登陆</el-button>
                </div>
            </div>
            <div v-else>
                <i class="el-icon-warning-outline" style="margin:5% 10%;font-size:100px"></i>
                <p class="login-tips">{{failedTips}}</p>
                <div class="login-btn">
                    <el-button type="primary" style="width:40%;margin:5% 2%" @click="reReg()">重新注册</el-button>
                    <el-button type="primary" style="width:40%;margin:5% 2%" @click="resReturn()">返回登陆</el-button>
                </div>
            </div>
           
        </div>
        <!-- 修改密码-->
        <div id="mdfy" class="ms-login" style="visibility:hidden;width:420px;left:48%">
            <div class="ms-title"> 
                <div type="button" @click="mdfyReturn()" style="user-select:none; cursor: pointer;display:inline-block;float:left;margin-left:5%">
                    <i class="el-icon-arrow-left" ></i>
                </div>
                <div style="width:80%;margin:0% 10%">请重设您的密码</div>
            </div>
            <el-form ref="mdfyForm" :rules="mdfyRule" :model="mdfyForm" class="ms-content" label-width="70px">
                <el-form-item label="用户名" prop="username">
                    <el-input placeholder="请输入忘记密码的用户名" v-model="mdfyForm.username"></el-input>
                </el-form-item>
                <el-form-item label="许可证" prop="permit">
                    <el-input placeholder="请输入您的账号许可证。" v-model="mdfyForm.permit"></el-input>
                </el-form-item>
                <el-form-item label="新密码" prop="newpwd">
                    <el-input placeholder="数字，大小写字母，特殊符号组成，8-16位" v-model="mdfyForm.newpwd" type="password" show-password @change="(val)=>checkValidate(val,'mdfy_pwd')"></el-input>
                </el-form-item>
                <el-form-item label="确认密码" prop="cnewpwd">
                    <el-input placeholder="请再次输入新的密码以确认。" v-model="mdfyForm.cnewpwd" type="password" show-password></el-input>
                </el-form-item>
            </el-form>
            <div class="login-btn" style="margin-bottom:20px">
                <el-button type="primary" @click="mdfyComfirm()">确认</el-button>
                <div class="login-tips">{{mdfyTips}}</div>
            </div>
        </div>
        <div id="mdfyRes" class="ms-login" style="visibility:hidden">
             <div v-if="mdfyWaiting==null">
                 <i class="el-icon-loading" style="margin:5% 10%;font-size:100px"></i>
             </div>
            <div v-else-if="mdfyWaiting">
                修改成功
            <el-button type="primary" @click="mdfySuccReturn()">返回登陆</el-button>
            </div>
            <div v-else>
                修改失败{{mdfyFailTips}}
            <el-button type="primary" @click="reMdfy()">重新修改</el-button>
            <el-button type="primary" @click="mdfyFailReturn()">返回登陆</el-button>
            </div>
        </div>
    </div>
</template>

<script>
import CryptoJS from "crypto-js"
import shaJS from "js-sha1"
export default {
    data: function() {
        return {
            param: {
                username: "",
                password: "",
            },
            rules: {
                username: [{ required: true, message: '请输入用户名', trigger: 'blur' }],
                password: [{ required: true, message: '请输入密码', trigger: 'blur' }],
            },
            //注册参数
            regParam:{
                name:"",
                key:"",
                user:"",
                pwd:"",
                Cpwd:""
            },
            regRule:{
                name:[{required:true,message:'请输入正确格式的姓名，2-3个汉字',trigger:'blur'}],
                key:[{required:true,message:'需要令牌才能注册账号哦！',trigger:'blur'}],
                user:[{required:true,message:'用户名不能为空哦！',trigger:'blur'}],
                pwd:[{required:true,message:'需要密码！！',trigger:'blur'}],
                Cpwd:[{required:true,message:'此步骤很重要，请验证您的密码！！',trigger:'blur'}],
            },
            regRight:null,
            //提示
            pwdTip:"Tips:密码需要包含数字大写字母，小写字母以及特殊符号等字符，密码长度8-16位。",
            comfirmTip:"",
            failedTips:"",
            //修改密码参数
            mdfyWaiting:false,
            mdfyForm:{
                username:null,
                permit:null,
                newpwd:null,
                cnewpwd:null
            },
            mdfyRule:{
                username:[{required:true,message:'请输入您的用户名',trigger:'blur'}],
                permit:[{required:true,message:'需要您的账户令牌才能重设密码哦！',trigger:'blur'}],
                newpwd:[{required:true,message:'重设密码不能为空！',trigger:'blur'}],
            },
            mdfyTips:"这里是tips",
            mdfyFailTips:""
        };
    },
    methods: {
        //=============================================================================================
        //*************************************登录页面函数*********************************************
        //=============================================================================================
        submitForm() {
            if(this.param.username == "" || this.param.password == ""){
                this.$message.error('请输入账号户密码！');
            }else{
                // console.log("new pwd:"+this.param.password)
                // var ciphertext = CryptoJS.AES.encrypt(this.param.password,"secretkey1234567").toString();
                var ciphertext = shaJS(this.param.password);
                console.log(ciphertext);
                fetch('api/Login/validate',{
                            method:'POST',
                            headers: {
                                'Content-Type': 'application/json'
                            },
                            body: JSON.stringify({
                                username:this.param.username,
                                happyword:ciphertext
                            })
                        })
                        .then(response => response.json())
                        // .then(response => response.text())
                        .then(data => {

                            if(data){
                                this.$message.success('登录成功');
                                localStorage.setItem('ms_username', this.param.username);
                                this.$router.push('/MainPage');
                            }else{
                                this.$message.error('账号或密码错误，请重新输入！');
                                // eslint-disable-next-line no-console
                                console.log("submit error");
                                return false;
                            }
                        }).catch(data => {
                            alert(data);
                        })
            }  
        },
        //=============================================================================================
        //=============================================================================================

        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        //***********************************注册页面函数***********************************************
        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        regist(){
            var lp = document.getElementById("loginPage");
            var rp = document.getElementById("registPage");
            lp.style.visibility = "hidden";
            rp.style.visibility = "visible";
        },
        regReturn(){
            var lp = document.getElementById("loginPage");
            var rp = document.getElementById("registPage");
            lp.style.visibility = "visible";
            rp.style.visibility = "hidden";

             //清除注册页面提示及表单内容
            this.$refs['regForm'].resetFields();
            this.pwdTip = ""
            this.comfirmTip = ""
        },
        regComfirm(){
            var tip = document.getElementById("tips2")
            if(this.regParam.name == ""){
                tip.style.color = "#FF0000"
                this.comfirmTip = "请输入您的真实姓名！"
                return
            }
            if(this.regParam.key == ""){
                tip.style.color = "#FF0000"
                this.comfirmTip = "请输入您的注册令牌！"
                return
            }
            if(this.regParam.user == ""){
                tip.style.color = "#FF0000"
                this.comfirmTip = "请定义用户名！"
                return
            }
            if(this.regParam.pwd == ""){
                tip.style.color = "#FF0000"
                this.comfirmTip = "请定义有效的密码！"
                return
            }
            if(this.regParam.pwd != this.regParam.Cpwd){
                tip.style.color = "#FF0000"
                this.comfirmTip = "两次输入的密码不一致，请检查后重新输入！"
                return
            }
            tip.style.color = "";
            this.comfirmTip = "";
            
            
            var rp = document.getElementById("registPage");
            var resp = document.getElementById("resPage");
            rp.style.visibility = "hidden";
            resp.style.visibility = "visible";
            // this.regRight = true;
            // this.regRight = false;
            // var ciphertext = CryptoJS.AES.encrypt(this.regParam.pwd,"secretkey123").toString();
            var ciphertext = shaJS(this.regParam.pwd);
            console.log(this.regParam.name)
            console.log(this.regParam.key)
            console.log(this.regParam.user)
            console.log("ori: "+this.regParam.pwd)
            console.log("cipher: "+ciphertext)
            console.log(this.regParam.Cpwd)
            console.log(this.regRight)

            fetch('api/Login/registration',{
                            method:'POST',
                            headers: {
                                'Content-Type': 'application/json'
                            },
                            body: JSON.stringify({
                                name:this.regParam.name,
                                token:this.regParam.key,
                                username:this.regParam.user,
                                happyword:ciphertext
                            })
                        })
                        .then(response=>response.json())
                        .then(data=>{
                            console.log(data)
                            if(data[0].令牌有效性 == 1){
                                if(data[0].令牌使用情况 == 1){
                                    if(data[0].用户名使用情况 == 0){
                                        this.regRight = true;
                                    }
                                    else{
                                        this.regRight = false;
                                        this.failedTips = "注册失败，用户名已存在！"
                                    }
                                }else{
                                    this.regRight = false;
                                    this.failedTips = "注册失败，您的令牌已失效，请向管理员获取最新的有效令牌进行注册！"
                                }
                            }else{
                                this.regRight = false;
                                this.failedTips = "注册失败，您的令牌无效，如果需要注册账号，请向管理员申请注册令牌！"
                            }
                        }).catch(data =>{
                            alert(data)
                        })
            
            //清除注册页面提示及表单内容
            this.$refs['regForm'].resetFields();
            this.pwdTip = ""
            this.comfirmTip = ""

        },
        resReturn(){
            var resp = document.getElementById("resPage");
            var lp = document.getElementById("loginPage");
            resp.style.visibility = "hidden";
            lp.style.visibility = "visible";
            this.regRight = null;
        },
        reReg(){
            var resp = document.getElementById("resPage");
            var rp = document.getElementById("registPage");
            resp.style.visibility = "hidden";
            rp.style.visibility = "visible";
             this.regRight = null;
        },
        //检查密码字符类型组成
        checkValidate(val,event){
            var pwdRegex = new RegExp('(?=.*[0-9])(?=.*[A-Z])(?=.*[a-z])(?=.*[^a-zA-Z0-9]).{8,16}');
            if(event=="reg_pwd"){
                 var tip = document.getElementById("tips")
                if(!pwdRegex.test(this.regParam.pwd)){
                    tip.style.color = "#FF0000"
                    this.pwdTip = "密码中需要包含大小写字母，数字及特殊字符，长度8-16，请按要求设定！";
                }else{
                    tip.style.color = "#228B22"
                    this.pwdTip = "设定成功！"
                }
            }else if(event=="mdfy_pwd"){
                if(!pwdRegex.test(this.mdfyForm.newpwd)){
                    this.mdfyTips = "密码中需要包含数字，大写字母，小写字母以及特殊字符，请严格按照该规则设定您的密码！"
                }else{
                    this.mdfyTips = "设定成功"
                }
            }
           
        },
        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
        //************************************修改密码页面函数*******************************************
        //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
        pwdMdfy(){
            var login = document.getElementById("loginPage");
            var mdfy = document.getElementById("mdfy");
            login.style.visibility = "hidden";
            mdfy.style.visibility = "visible";
        },
        mdfyReturn(){
            var login = document.getElementById("loginPage");
            var mdfy = document.getElementById("mdfy");
            login.style.visibility = "visible";
            mdfy.style.visibility = "hidden";
            this.mdfyTips = ""
            this.$refs['mdfyForm'].resetFields()
        },
        mdfyComfirm(){
            if(this.mdfyForm.username==null){
                this.mdfyTips = "请输入有效的用户名，不能为空！"
                return;
            }
            if(this.mdfyForm.permit==null){
                this.mdfyTips = "请输入您的用户名的许可令牌，不能为空！"
                return;
            }
            if(this.mdfyForm.newpwd==null){
                this.mdfyTips = "重设的密码不能为空！"
                return;
            }
            if(this.mdfyForm.cnewpwd!==this.mdfyForm.newpwd){
                 this.mdfyTips = "请确认两次输入的密码一致！"
                return;
            }

            console.log(this.mdfyForm.username)
            console.log(this.mdfyForm.permit)
            console.log(this.mdfyForm.newpwd)

            // var ciphertext = CryptoJS.AES.encrypt(this.mdfyForm.newpwd,"secretkey123").toString();
            var ciphertext = shaJS(this.mdfyForm.newpwd);
            fetch('api/Login/modifyPwd',{
                method:'POST',
                headers:{
                    'Content-Type': 'application/json'
                },
                body:JSON.stringify({
                    username:this.mdfyForm.username,
                    permit:this.mdfyForm.permit,
                    newpwd:ciphertext
                })
            }).then(response=>response.json())
                .then(data=>{
                    alert(data[0].result)
                    if(data[0].result == 0){
                        this.mdfyWaiting = false
                        this.mdfyFailTips = "用户名不存在或令牌有误，请核对后在修改密码！"
                    }else{
                        this.mdfyWaiting = true
                    }
                }).catch(data=>{
                    alert(data)
                })
            var mdfy = document.getElementById("mdfy");
            var mdfyRes = document.getElementById("mdfyRes");
            mdfy.style.visibility = "hidden";
            mdfyRes.style.visibility = "visible";

            this.mdfyTips = ""
            this.$refs['mdfyForm'].resetFields()
        },
        mdfySuccReturn(){
            var login = document.getElementById("loginPage");
            var mdfyRes = document.getElementById("mdfyRes");
            login.style.visibility = "visible";
            mdfyRes.style.visibility = "hidden";
            this.mdfyWaiting=null
        },
        reMdfy(){
            var mdfy = document.getElementById("mdfy");
            var mdfyRes = document.getElementById("mdfyRes");
            mdfy.style.visibility = "visible";
            mdfyRes.style.visibility = "hidden";
            this.mdfyWaiting=null

        },
        mdfyFailReturn(){
            var login = document.getElementById("loginPage");
            var mdfyRes = document.getElementById("mdfyRes");
            login.style.visibility = "visible";
            mdfyRes.style.visibility = "hidden";
            this.mdfyWaiting=null
        }

        //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
        //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
    },
};
</script>

<style scoped>
.login-wrap {
    position: relative;
    width: 100%;
    height: 100%;
    /* background:#000; */
    background-image: url(../../assets/img/login-bg.jpg);
    background-size: 100%;
}
.ms-title {
    width: 100%;
    line-height: 50px;
    text-align: center;
    font-size: 20px;
    color: #000;
    border-bottom: 1px solid #ddd;
}
.ms-login {
    position: absolute;
    left: 50%;
    top: 50%;
    width: 350px;
    margin: -190px 0 0 -175px;
    border-radius: 5px;
    background: rgba(255, 255, 255, 0.3);
    overflow: hidden;
}
.ms-content {
    padding: 20px 30px;
}
.login-btn {
    /* margin-top:-12px; */
    text-align: center;
}
.login-btn button {
    width: 45%;
    height: 36px;
}
.login-tips {
    font-size: 12px;
    line-height: 15px;
    color: #000;
    /* margin:2% 10%; */
    /* width:80% */
}

.inputline{
    margin:0% 10%;
    width:80%
}


</style>