<template>
    <div>
        <el-card>新建密令：
            <el-button type="primary" @click="crtKey()">生成</el-button>
            <el-button type="primary" @click="clrKey()">清除</el-button>
            <el-button type="primary" @click="cfmKey()" :disabled="key.length==0">初始化该密令</el-button>
        </el-card>
        <el-card>
            <div class="keyStyle">{{key}}</div>
        </el-card>
        <el-card>
            <div>可用的密钥</div>
            <el-table :data="keyTab" style="width: 100%">
                <el-table-column fixed prop="key" label="密令"></el-table-column>
                <el-table-column fixed prop="crtDate" label="创建日期" ></el-table-column>
                <el-table-column fixed prop="valid" label="有效性"></el-table-column>
            </el-table>
        </el-card>
    </div>
</template>
<script>
export default {
    data(){
        return{
            key:'',
            char:['0','1','2','3','4','5','6','7','8','9','A','B','C','D','E','F','G','H','I','J','K','L','M','N','O','P','Q','R','S','T','U','V','W','X','Y','Z','a','b','c','d','e','f','g','h','i','j','k','l','m','n','o','p','q','r','s','t','u','v','w','x','y','z'],
            keyTab:[]
        }
    },
    methods:{
        crtKey(){
            var len = 6
            var pwd = ''
            for(var i = 0; i < len; i++){
                pwd += this.char[Math.floor(Math.random()*this.char.length)]
            }
           this.key=pwd;
        },
        clrKey(){
            this.key = ''
        },
        cfmKey(){
            fetch('api/AccountManage/crtKey',{
                method:'POST',
                headers:{
                    'Content-Type':'application/json'
                },
                body:JSON.stringify({
                    kstr:this.key
                })
            }).then(response=>response.json())
            .then(data=>{
                if(data[0].resSign){
                    this.$message.success("新建成功！")
                    this.key=''
                    this.tabFsh()
                }else{
                    this.$message.warning("该字符串密令已存在，请重新生成")
                }
            })
        },
        tabFsh(){
            fetch('api/AccountManage/keyTab',{
                methods:'GET',
                headers:{
                    'Content-Type':'application/json'
                }
            }).then(response=>response.json())
            .then(data=>{
                this.keyTab=[]
                if(data.length != 0){
                    for(var item of data){
                        var tmp={
                            key:item.token,
                            crtDate:item.cdate,
                            valid:item.validate
                        }
                         this.keyTab.push(tmp)
                    }
                }else{
                    this.$message.warning("当前没有可用的密令！")
                }
            })
        }
    },
    mounted(){
        this.tabFsh()
    }
}
</script>