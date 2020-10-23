<template>
    <div class="login-wrap">
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
                <div class="login-btn">
                    <el-button type="primary" @click="submitForm()" style="margin-right:13px">登录</el-button>
                    <el-button type="primary" @click="regist()" style="margin-left:13px">注册</el-button> <!---->
                </div>
                <p class="login-tips">Tips : 这里还没想好给什么tips。</p>
            </el-form>
        </div>
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
                <el-input placeholder="您的密码" prefix-icon="el-icon-lock" v-model="regParam.pwd" type="password" @change="checkValidate()"  ></el-input>
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
    </div>
</template>

<script>
import CryptoJS from "crypto-js"
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
            failedTips:""
        };
    },
    methods: {
        submitForm() {
            if(this.param.username == "" || this.param.password == ""){
                this.$message.error('请输入账号户密码！');
            }else{
                // console.log("new pwd:"+this.param.password)
                var ciphertext = CryptoJS.AES.encrypt(this.param.password,"secretkey123").toString();
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
            var ciphertext = CryptoJS.AES.encrypt(this.regParam.pwd,"secretkey123").toString();
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
        checkValidate(){
            var pwdRegex = new RegExp('(?=.*[0-9])(?=.*[A-Z])(?=.*[a-z])(?=.*[^a-zA-Z0-9]).{8,16}');
            var tip = document.getElementById("tips")
            if(!pwdRegex.test(this.regParam.pwd)){
                tip.style.color = "#FF0000"
                this.pwdTip = "密码中需要包含大小写字母，数字及特殊字符，长度8-16，请按要求设定！";
            }else{
                tip.style.color = "#228B22"
                this.pwdTip = "设定成功！"
            }
        }
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
    padding: 30px 30px;
}
.login-btn {
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