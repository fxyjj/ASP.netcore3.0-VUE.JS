<template>
    <div class="panel">
        <el-card>
        <el-table :data="tableData.slice((currPage-1)*pageSize,currPage*pageSize)" style="width: 100%">
            <el-table-column fixed prop="user" label="用户名" width="100"></el-table-column>
            <el-table-column fixed prop="name" label="姓名"></el-table-column>
            <el-table-column fixed prop="certificate" label="许可证"></el-table-column>
            <el-table-column fixed prop="date" label="创建日期" ></el-table-column>
            <el-table-column fixed="right" label="操作" >
                <template slot-scope="scope">
                    <el-button @click="handleClick(scope.row)" type="primary" size="small">修改密码</el-button>
                    <el-button type="danger" size="small" @click="deleteAcc(scope.row)">注销账号</el-button>
                </template>
            </el-table-column>
        </el-table>
         <el-pagination :hide-on-single-page="singlePage" :total="tableData.length" :page-size="pageSize" layout="prev, pager, next" @current-change="changePage($event)"></el-pagination>
        </el-card>
        <el-dialog
        title="重设密码"
        :visible.sync="dialogVisible"
        width="30%"
        :before-close="handleClose">
          <el-form ref="form" :model="form" class="fromStyle" label-width="90px">
                <el-form-item label="用户名">
                    <el-input :placeholder="form.username" v-model="form.username" disabled ></el-input>
                </el-form-item>
                <el-form-item label="新密码" prop="newpwd">
                    <el-input placeholder="请输入新的密码。" v-model="form.newpwd" type="password" show-password @change="checkValidate($event)"></el-input>
                </el-form-item>
                <el-form-item label="确认新密码" prop="cnewpwd">
                    <el-input placeholder="请再次输入新的密码。" v-model="form.cnewpwd" type="password" show-password @change="pwdComfirm($event)"></el-input>
                </el-form-item>
            </el-form>
        <span slot="footer" class="dialog-footer">
            <el-button @click="close()">取 消</el-button>
            <el-button type="primary" @click="modifying()">确 定</el-button>
        </span>
        </el-dialog>
        <el-dialog title="重要提示" :visible.sync="deleteVis" width="20%" style="padding:0 20px 30px">
            <i class="el-icon-warning-outline" style="color:#ff0000;font-size:100px;margin:4% 34%"></i>
            <h3 style="text-align:center">确认要删除 <strong style="color:#ff0000">{{delInfo.username}}</strong> 该用户吗？</h3>
            <div class="cbtn">
            <el-button type="success" @click="deleteVis=false" style="width:40%">取消</el-button>
            <el-button type="danger" style="width:40%;margin-left:20%" @click="deleteComfirm()">确认</el-button>
            </div>
        </el-dialog>
    </div>
</template>
<script>
import bus from '../../common/bus'
import CryptoJS from "crypto-js"
export default {
    name:"AccountManage",
    data(){
        return{
            tableData:[],
            singlePage:true,
            currPage:1,
            pageSize:13,
            qInfo:null,
            //弹出框表单参数
            dialogVisible:false,
            form:{
                username:'',
                permit:null,
                newpwd:null,
                cnewpwd:null
            },
            deleteVis:false,
            delInfo:{
                username:null,
                token:null
            }
        }
    },
    methods:{
        handleClick(row){
            console.log(row);
            this.form.username = row.user;
            this.form.permit = row.certificate;
            this.dialogVisible = true
        },

        changePage(event){
            this.currPage = event;
            console.log(event);
        },
        getData(){
            fetch('api/AccountManage/getUsr',{
                method:'POST',
                headers:{
                    'Content-Type': 'application/json'
                },
                body:JSON.stringify({
                    queryInfo:this.qInfo
                })
            }).then(response => response.json())
            .then(data =>{
                this.tableData =[]
                for(var item of data){
                     var sub = {user:item.username,name:item.uname,certificate:item.crtficate,date:item.cdate}
                     this.tableData.push(sub)
                }
               
            }).catch(data =>{
                alert(data);
            })
        },
        handleClose(done) {
            this.$confirm('确认关闭？')
            .then(_ => {
                done();
            })
            .catch(_ => {});
        },
        //点击确定开始修改密码。
        checkValidate(p){
            // console.log(p)
            var pwdRegex = new RegExp('(?=.*[0-9])(?=.*[A-Z])(?=.*[a-z])(?=.*[^a-zA-Z0-9]).{8,16}');
            if(!pwdRegex.test(p)){
                this.$message.error("密码中必须包含数字，大小写字母和特殊字符，请确认后再修改！")
            }else{
                this.$message.success("密码合格！")
            }
        },
        pwdComfirm(p){
            if(this.form.newpwd !== p){
                this.$message.error("两次输入的密码不一致，请核验后再修改！")
            }
        },
        modifying(){
            if(this.form.newpwd !== this.form.cnewpwd){
                this.$message.error("两次输入的密码不一致，请核验后再修改!")
                return;
            }
            var pwdRegex = new RegExp('(?=.*[0-9])(?=.*[A-Z])(?=.*[a-z])(?=.*[^a-zA-Z0-9]).{8,16}');
            if(!pwdRegex.test(this.form.newpwd)){
                this.$message.error("密码中必须包含数字，大小写字母和特殊字符，请确认后再修改！")
                return;
            }
            var ciphertext = CryptoJS.AES.encrypt(this.form.newpwd,"secretkey123").toString();
            fetch('api/Login/modifyPwd',{
                method:'POST',
                headers:{
                    'Content-Type': 'application/json'
                },
                body:JSON.stringify({
                    username:this.form.username,
                    permit:this.form.permit,
                    newpwd:ciphertext
                })
            }).then(response=>response.json())
                .then(data=>{
                    // alert(data[0].result)
                    if(data[0].result == 0){
                        this.$message.error("修改失败")
                    }else{
                       this.$message.success("修改成功")
                    }
                }).catch(data=>{
                    alert(data)
                })
            this.$refs['form'].resetFields();
            this.dialogVisible = false;

        },
        close(){
            this.$refs['form'].resetFields();
            this.dialogVisible = false;
        },
        deleteAcc(row){
            this.deleteVis = true;
            this.delInfo.username = row.user
            this.delInfo.token = row.certificate
            console.log(this.delInfo)
        },
        deleteComfirm(){
            fetch('api/Login/deleteUser',{
                method:'POST',
                headers:{
                    'Content-Type': 'application/json'
                },
                body:JSON.stringify({
                    username:this.delInfo.username,
                    token:this.delInfo.token
                })
            }).then(response => response.json())
            .then(data=>{
                alert(data)
                if(data[0].result == 0){
                    this.$message.error("删除失败，此账号已被删除或者此账号不存在！（非正常错误请检查数据库数据）")
                }else{
                    this.$message.success("注销成功！")
                    if(this.delInfo.username == localStorage.getItem('ms_username')){
                        this.$message.warning("当前登录的账号已被注销，请重新登录！")
                        localStorage.removeItem('ms_username');
                        this.$router.push('/Login')
                    }
                }
            }).catch(data=>{
                alert(data)
            })
        }
    },
    mounted(){
        this.getData();
        bus.$on('srchI',msg =>{
            this.qInfo = msg;
            this.getData();
        })
    }
}
</script>
<style scoped>
.panel{
    margin:10px;
    border:0px;
}
.cbtn{
    width:90%;
    margin:5% 5%
}
</style>