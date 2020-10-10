<template>
    <div class="container" style="background:#324157"><!--:style='{background:"#324157"}'-->
     

     <!-- <div class="block" style="border:1px solid #000"> -->
    <!-- <img src="../../../assets/logo.png"  style="width: 100px; height: 100px;border:2px" /> -->
  <!-- </div> -->

        <el-row :gutter="20">
            <el-col :span="7">
                <!-- <div class="" style="width: 100%; height: 900px;border:2px;border:1px solid #000"></div> -->
                 <el-card shadow="hover" style="width: 100%; height: 900px;border:1px solid #000">>
                     鼠标悬浮时显示
                 </el-card>
            </el-col>
            <el-col :span="8">
                <el-card shadow="hover" style="width: 100%; height: 130px;box-shadow: 0 1px 2px #fff"> 
                    <el-row>
                        <el-col :span="10">EGR Module</el-col>
                        <el-col :span="5">
                            <div id="d11" :style='{border:"1px solid rgba(0,0,0,1)",height:"40px"}'></div>
                            <div id="d11" :style='{border:"1px solid rgba(0,0,0,1)",height:"40px"}'></div>
                        </el-col>
                        <el-col :span="5">
                            <div id="d11" :style='{border:"1px solid rgba(0,0,0,1)",height:"40px"}'></div>
                            <div id="d11" :style='{border:"1px solid rgba(0,0,0,1)",height:"40px"}'></div>
                        </el-col>
                        <el-col :span="4">
                             <div id="d11" :style='{border:"1px solid rgba(0,0,0,1)",height:"100px"}'></div>
                        </el-col>
                    </el-row>
                </el-card>
                <el-card shadow="hover" style="width: 100%; height: 200px;box-shadow: 0 1px 2px #fff;margin-top:8px;margin-bottom:8px">High Voltage Coolant Heater <div id="d12" :style='{border:"1px solid rgba(0,0,0,1)",width:"100%",height:"150px"}'></div></el-card>
                <el-card shadow="hover" style="width: 100%; height: 200px;box-shadow: 0 1px 2px #fff;margin-top:8px;margin-bottom:8px">Ignition Coil <div id="d13" :style='{border:"1px solid rgba(0,0,0,1)",width:"100%",height:"150px"}'></div></el-card>
                <el-card shadow="hover" style="width: 100%; height: 340px;box-shadow: 0 1px 2px #fff">EGR Valve 
                    <el-row>
                        <el-col :span="10">
                            <div id="d11" :style='{border:"1px solid rgba(0,0,0,1)",height:"40px"}'></div>
                            <div id="d11" :style='{border:"1px solid rgba(0,0,0,1)",height:"40px"}'></div>
                            <div id="d11" :style='{border:"1px solid rgba(0,0,0,1)",height:"40px"}'></div>
                            <div id="d11" :style='{border:"1px solid rgba(0,0,0,1)",height:"40px"}'></div>
                        </el-col>
                        <el-col :span="4">
                            <div id="d11" :style='{border:"1px solid rgba(0,0,0,1)",height:"240px"}'></div>
                        </el-col>
                        <el-col :span="5">
                            <div id="d11" :style='{border:"1px solid rgba(0,0,0,1)",height:"100px"}'></div>
                        </el-col>
                        <el-col :span="5">
                             <div id="d11" :style='{border:"1px solid rgba(0,0,0,1)",height:"200px"}'></div>
                        </el-col>
                    </el-row>
                </el-card>
            </el-col>
            <el-col :span="5">
                <el-card shadow="hover" style="width: 100%; height: 280px;box-shadow: 0 1px 2px #fff">Cooler testing Furnace<div id="d11" :style='{border:"1px solid rgba(0,0,0,1)",height:"40px"}'></div></el-card>
                <el-card shadow="hover" style="width: 100%; height: 250px;box-shadow: 0 1px 2px #fff;margin-top:13px;margin-bottom:13px">Cooler Assembly 
                    <el-row>
                        <el-col :span="8"><div id="d11" :style='{border:"1px solid rgba(0,0,0,1)",height:"200px"}'></div></el-col>
                        <el-col :span="8"><div id="d11" :style='{border:"1px solid rgba(0,0,0,1)",height:"200px"}'></div></el-col>
                        <el-col :span="8"><div id="d11" :style='{border:"1px solid rgba(0,0,0,1)",height:"200px"}'></div></el-col>
                    </el-row>
                </el-card>
                <el-card shadow="hover" style="width: 100%; height: 340px;box-shadow: 0 1px 2px #fff">Hybrid Tube
                    <el-row>
                        <el-col :span="8"><div id="d11" :style='{border:"1px solid rgba(0,0,0,1)",height:"90px"}'></div></el-col>
                        <el-col :span="8"><div id="d11" :style='{border:"1px solid rgba(0,0,0,1)",height:"90px"}'></div></el-col>
                        <el-col :span="8"><div id="d11" :style='{border:"1px solid rgba(0,0,0,1)",height:"90px"}'></div></el-col>
                    </el-row>
                    <el-row>
                        <div id="d11" :style='{border:"1px solid rgba(0,0,0,1)",height:"30px"}'></div>
                    </el-row>
                    <el-row>
                        <div id="d11" :style='{border:"1px solid rgba(0,0,0,1)",height:"120px"}'></div>
                    </el-row>
                </el-card>
            </el-col>
            <el-col :span="4">
                 <el-card shadow="hover" style="width: 100%; height: 300px;box-shadow: 0 1px 2px #fff">Routing Mark <div id="d31"></div></el-card>
                <el-card shadow="hover" style="width: 100%; height: 230px;box-shadow: 0 1px 2px #fff;margin-top:13px;margin-bottom:13px">Air Heater ECRV <div id="d32"></div></el-card>
                <el-card shadow="hover" style="width: 100%; height: 340px;box-shadow: 0 1px 2px #fff">Reservered For Cooler <div id="d33"></div></el-card>
            </el-col>
        </el-row>
        <el-row :gutter="20">
             <el-card shadow="hover" style="width: 100%;box-shadow: 0 1px 2px #fff;margin-top:8px;">
                 <el-table :data="tableData">
                    <el-table-column prop="number" label="序号" width="180"   :style='{background:"#409EFF"}'></el-table-column>
                    <el-table-column prop="status" label="是否停线" width="180"></el-table-column>
                    <el-table-column prop="station" label="工作中心"></el-table-column>
                    <el-table-column prop="equipNum" label="设备编号" width="180"></el-table-column>
                    <el-table-column prop="type" label="按灯类型" width="180"></el-table-column>
                    <el-table-column prop="probDesc" label="问题描述"></el-table-column>
                    <el-table-column prop="andonTime" label="安灯时间" width="180"></el-table-column>
                    <el-table-column prop="respTime" label="响应时间(mins)" width="180"></el-table-column>
                    <el-table-column prop="time" label="持续时间"></el-table-column>
                    <el-table-column prop="currStatus" label="当前状态" width="180"></el-table-column>

                </el-table>
             </el-card>
            
        </el-row>
    </div>
</template>

<script>
export default {
    name:"andonstatus",
    data(){
        return {
            //TODO...
            src:'',
            tableData: [],
            test:0,
            // d_status11:-1,
            // d_status12:-1,
            // d_status13:-1,
            // d_status14:-1,
            // d_status21:-1,
            // d_status22:-1,
            // d_status23:-1,
            // d_status31:-1,
            // d_status32:-1,
            // d_status33:-1,
        }
    },
    methods:{
        testd(){
            if(this.test == 0){
                 this.test = 1
                document.getElementById("lanturn1").className= 'lanturn1'
                console.log(this.test)
            }else{
                this.test = 0
                 document.getElementById("lanturn1").className= 'lanturn2'
            }
           
        }
    },
    mounted(){
        fetch('api/Andon/andonStatus' ,{
                method:'POST',
                headers: {
                    'Content-Type': 'application/json'
                }
            })
            .then(response => response.text())
            // .then(response => response.json())
            .then(data => {
                // this.cdate = data;
                alert(data)
                console.log(data)
            }).catch(data => {
                alert(data);
            })
    }
}
</script>
<style scoped>

@keyframes breatheBlue {
	0% {
        opacity: .4;
        box-shadow: 0 1px 2px rgba(0, 147, 223, 0.4), 0 1px 1px rgba(0, 147, 223, 0.1) inset;
        /* background: #fff; */
    }

    100% {
        opacity: 1;
        border: 1px solid rgba(59, 235, 235, 0.7);
        box-shadow: 0 1px 30px #0093df, 0 1px 20px #0093df inset;
        /* background: #0093df; */
    }
}

@keyframes breatheRed {
	0% {
        opacity: .4;
        box-shadow: 0 1px 2px rgba(0, 147, 223, 0.4), 0 1px 1px rgba(0, 147, 223, 0.1) inset;
        background: rgb(146, 145, 145);
    }

    100% {
        opacity: 1;
        border: 1px solid rgba(59, 235, 235, 0.7);
        box-shadow: 0 1px 30px #b61414, 0 1px 20px #f71515 inset;
        background: #b61414;
    }
}

.lanturn1{
    width: 40px;
    height: 40px;
    border-radius: 50%;
   
    animation-timing-function: ease-in-out;
    animation-name: breatheRed;
    animation-duration: 1500ms;
    animation-iteration-count: infinite;
    animation-direction:alternate;
}

.lanturn2{
    width: 40px;
    height: 40px;
    border-radius: 50%;
   
    animation-timing-function: ease-in-out;
    animation-name: breatheBlue;
    animation-duration: 1500ms;
    animation-iteration-count: infinite;
    animation-direction:alternate;
}

/* .block{
    border:1px solid #000
} */

</style>