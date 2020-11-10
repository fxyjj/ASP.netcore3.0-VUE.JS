<template>
    <div class="mainContainer">
        <el-row :gutter="20">
            <el-col :span="10">
                <el-card :style='{height:"150px",background:"#F4A460"}'>
                    <el-row>
                        <el-col :span="8">
                            <div class="lineName">EGR module</div>
                        </el-col>
                        <el-col :span="6">
                            <div class="block" :style='{height:"50px"}'>
                                <div class="light">
                                    <i class="iconfont el-icon-blinklightdengpao" style="margin-top:-15px;margin-left:-20px"></i>
                                </div>
                                <div class="modName" style="margin-top:12px">resPos</div>
                            </div>
                            <div class="block" :style='{height:"50px"}'>
                                <div class="light">
                                    <i class="iconfont el-icon-blinklightdengpao" style="margin-top:-15px;margin-left:-20px"></i>
                                </div>
                                <div class="modName" style="margin-left:10%;margin-right:20%;margin-top:12px">TGDI</div>
                            </div>
                        </el-col>
                        <el-col :span="6">
                             <div class="block" :style='{height:"50px"}'>
                                <div class="light">
                                    <i class="iconfont el-icon-blinklightdengpao" style="margin-top:-15px;margin-left:-20px"></i>
                                </div>
                                <div class="modName" style="margin-top:12px">Test Station</div>
                            </div>
                            <div class="block" :style='{height:"50px"}'>
                                 <div id="模块线2" class="light">
                                     <i class="iconfont el-icon-blinklightdengpao" style="margin-top:-15px;margin-left:-20px"></i>
                                 </div>
                                <div class="modName" style="margin-left:8%;margin-right:8%;margin-top:12px">Module2</div>
                            </div>
                        </el-col>
                        <el-col :span="4">
                            <div class="block" :style='{height:"107px"}'>
                                <div id="模块线1" class="light" >
                                    <i class="iconfont el-icon-blinklightdengpao" style="margin-top:-15px;margin-left:-20px"></i>
                                </div>
                                <div class="modName" style="width:80%;margin-left:10px;margin-right:10px;margin-top:10px">Module1</div>
                            </div>
                        </el-col>
                    </el-row>
                    
                    </el-card>
                <el-card :style='{height:"200px","margin-top":"13px",background:"#9DC7A1"}'>
                    <el-row>
                        <el-col :span="8">
                            <div class="lineName">High Voltage Coolant Heater</div>
                        </el-col>
                        <el-col :span="16">
                             <div class="block" style="height:150px">
                                 <div id="Liquid Heater Line" class="light">
                                     <i class="iconfont el-icon-blinklightdengpao" style="margin-top:-15px;margin-left:-20px"></i>
                                 </div>
                                 <div class="modName" :style='{"margin-left":"25%","margin-right":"20%","margin-top":"15%"}'>Liquid Heater</div>
                             </div>
                        </el-col>
                    </el-row>
                    
                   
                    </el-card>
                <el-card :style='{height:"180px","margin-top":"13px",background:"#FAFAD2"}'>
                    <el-row>
                        <el-col :span="8">
                            <div class="lineName">IC2</div>
                        </el-col>
                        <el-col :span="8">
                            <div class="block" :style='{height:"140px"}'>
                                <div id="Huebers 2" class="light" >
                                    <i class="iconfont el-icon-blinklightdengpao" style="margin-top:-15px;margin-left:-20px"></i>
                                </div>
                                <div class="modName" :style='{"margin-left":"25%","margin-right":"25%","margin-top":"10%"}'>Huebers2</div>
                            </div>
                        </el-col>
                        <el-col :span="8">
                            <div class="block" :style='{height:"65px"}'>
                                <div id="IPTE 2" class="light">
                                    <i class="iconfont el-icon-blinklightdengpao" style="margin-top:-15px;margin-left:-20px"></i>
                                </div>
                                <div class="modName" :style='{margin:"12%"}'>IPTE2</div>
                            </div>
                            <div class="block" :style='{height:"65px"}'>
                                 <div id="Marsilli 2" class="light" >
                                     <i class="iconfont el-icon-blinklightdengpao" style="margin-top:-15px;margin-left:-20px"></i>
                                 </div>
                                <div class="modName" :style='{margin:"12% 6%"}'>Marsilli2</div>
                            </div>
                        </el-col>
                    </el-row>
                    
                    </el-card>
                <el-card :style='{height:"340px","margin-top":"13px",background:"#85A7D7"}'>
                    <el-row>
                        <div style="text-align:center;font-weight:2px;font-size:30px;background:rgba(255,255,255,0.8);">EGR Valve</div>
                    </el-row>
                    <el-row>
                        <el-col :span="6">
                            <div class="block" :style='{height:"62px"}'>
                                <div id="柔性线" class="light">
                                    <i class="iconfont el-icon-blinklightdengpao" style="margin-top:-15px;margin-left:-20px"></i>
                                </div>
                                <div class="modName" :style='{margin:"15% 5%"}'>Flexible</div>
                            </div>
                            <div class="block" :style='{height:"62px"}'>
                                <div id="LP阀装配线" class="light">
                                    <i class="iconfont el-icon-blinklightdengpao" style="margin-top:-15px;margin-left:-20px"></i>
                                </div>
                                <div class="modName" :style='{margin:"15% 20%"}'>LP</div>
                            </div>
                            <div class="block" :style='{height:"62px"}'>
                                <div id="GEN_II阀装配线" class="light">
                                    <i class="iconfont el-icon-blinklightdengpao" style="margin-top:-15px;margin-left:-20px"></i>
                                </div>
                                <div class="modName" :style='{margin:"15% 8%"}'>GEN II</div>
                            </div>
                            <div class="block" :style='{height:"62px"}'>
                                <div id="ETV装配线" class="light">
                                    <i class="iconfont el-icon-blinklightdengpao" style="margin-top:-15px;margin-left:-20px"></i>
                                </div>
                                <div class="modName" :style='{margin:"15% 15%"}'>ETV</div>
                            </div>
                        </el-col>
                        <el-col :span="6">
                            <div class="block" :style='{height:"131px"}'>
                                <div id="GEN_III_A+M" class="light">
                                    <i class="iconfont el-icon-blinklightdengpao" style="margin-top:-15px;margin-left:-20px"></i>
                                </div>
                                <div class="modName" :style='{margin:"10% 10%"}'>GEN III A+M</div>
                            </div>
                            <div class="block" :style='{height:"131px"}'>
                                <div id="GEN_III_V+F" class="light">
                                    <i class="iconfont el-icon-blinklightdengpao" style="margin-top:-15px;margin-left:-20px"></i>
                                </div>
                                <div class="modName" :style='{margin:"10% 10%"}' >GEN III V+F</div>
                            </div>
                        </el-col>
                        <el-col :span="6">
                            <div class="block" :style='{height:"131px"}'>
                                <div  class="light">
                                    <i class="iconfont el-icon-blinklightdengpao" style="margin-top:-15px;margin-left:-20px"></i>
                                </div>
                                <div class="modName" :style='{margin:"30% 10%"}'>new</div>
                            </div>
                            <div class="block" style="height:131px">
                                <div id="GEN_III_BPV" class="light">
                                    <i class="iconfont el-icon-blinklightdengpao" style="margin-top:-15px;margin-left:-20px"></i>
                                </div>
                                <div class="modName" :style='{margin:"30% 10%"}'>BPV</div>
                            </div>
                        </el-col>
                        <el-col :span="6">
                            <div class="block" :style='{height:"269px"}'>
                                <div id="GDI阀装配线" class="light">
                                    <i class="iconfont el-icon-blinklightdengpao" style="margin-top:-15px;margin-left:-20px"></i>
                                </div>
                                <div class="modName" :style='{margin:"50% 10%"}'>GDI</div>
                            </div>
                        </el-col>
                    </el-row>
                   
                </el-card>
                </el-col>
            <el-col :span="6">
                <el-card :style='{height:"365px",background:"#CEE08A"}'>
                    <el-row>
                         <div style="text-align:center;font-weight:2px;font-size:30px;background:rgba(255,255,255,0.8)">Furnace</div>
                    </el-row>
                    <el-row>
                        <div class="block" :style='{height:"60px"}'>
                            <div id="测试台" class="light">
                                <i class="iconfont el-icon-blinklightdengpao" style="margin-top:-15px;margin-left:-20px"></i>
                            </div>
                            <div class="modName" :style='{margin:"5% 20%"}'>Cooler Testing</div>
                        </div>
                    </el-row>
                    <el-row>
                        <el-col :span="12">
                            <div class="block" :style='{height:"210px"}'>
                                <div id="钎焊炉2" class="light">
                                    <i class="iconfont el-icon-blinklightdengpao" style="margin-top:-15px;margin-left:-20px"></i>
                                </div>
                                <div class="modName" :style='{margin:"30% 23%"}'>Furnace2</div>
                            </div>
                        </el-col>
                        <el-col :span="12">
                            <div class="block" :style='{height:"210px"}' >
                                <div id="钎焊炉1" class="light">
                                    <i class="iconfont el-icon-blinklightdengpao" style="margin-top:-15px;margin-left:-20px"></i>
                                </div>
                                <div class="modName" :style='{margin:"30% 23%"}'>Furnace1</div>
                            </div>
                        </el-col>
                    </el-row>

                   
                    </el-card>
                <el-card :style='{height:"180px","margin-top":"13px",background:"#CEE08A"}'>
                    <el-row>
                         <div style="text-align:center;font-weight:2px;font-size:30px;background:rgba(255,255,255,0.8)">Cooler Assembly</div>
                    </el-row>
                    <el-row>
                        <el-col :span="8">
                            <div class="block" :style='{height:"100px"}'>
                                <div id="Cooler Line 3" class="light">
                                    <i class="iconfont el-icon-blinklightdengpao" style="margin-top:-15px;margin-left:-20px"></i>
                                </div>
                                <div class="modName" :style='{margin:"15% 20%"}'>Cooler3</div>
                            </div>
                        </el-col>
                        <el-col :span="8">
                            <div class="block" :style='{height:"100px"}'>
                                <div id="Cooler Line 2" class="light">
                                    <i class="iconfont el-icon-blinklightdengpao" style="margin-top:-15px;margin-left:-20px"></i>
                                </div>
                                <div class="modName" :style='{margin:"15% 20%"}'>Cooler2</div>
                            </div>
                        </el-col>
                        <el-col :span="8">
                            <div class="block" :style='{height:"100px"}'>
                                <div id="Cooler Line 1" class="light">
                                    <i class="iconfont el-icon-blinklightdengpao" style="margin-top:-15px;margin-left:-20px"></i>
                                </div>
                                <div class="modName" :style='{margin:"15% 20%"}'>Cooler1</div>
                            </div>
                        </el-col>
                    </el-row>
                    </el-card>
                <el-card :style='{height:"340px","margin-top":"13px",background:"#CEE08A"}'>
                    <el-row>
                        <div style="text-align:center;font-weight:2px;font-size:30px;background:rgba(255,255,255,0.8)">
                            Hybrid Tube
                        </div>
                    </el-row>
                    <el-row>
                        <el-col :span="8">
                            <div class="block" :style='{height:"120px"}'>
                                <div id="混合管装配线1" class="light">
                                    <i class="iconfont el-icon-blinklightdengpao" style="margin-top:-15px;margin-left:-20px"></i>
                                </div>
                                <div class="modName" :style='{margin:"15% 32%"}'>HB1</div>
                            </div>
                        </el-col>
                        <el-col :span="8">
                            <div class="block" :style='{height:"120px"}'>
                                <div id="混合管装配线2" class="light">
                                    <i class="iconfont el-icon-blinklightdengpao" style="margin-top:-15px;margin-left:-20px"></i>
                                </div>
                                <div class="modName" :style='{margin:"15% 32%"}'>HB2</div>
                            </div>
                        </el-col>
                        <el-col :span="8">
                            <div class="block" :style='{height:"120px"}'>
                                <div id="混合管装配线3" class="light">
                                    <i class="iconfont el-icon-blinklightdengpao" style="margin-top:-15px;margin-left:-20px"></i>
                                </div>
                                <div  class="modName" :style='{margin:"15% 32%"}'>HB3</div>
                            </div>
                        </el-col>
                    </el-row>
                    <el-row>
                        <el-col :span="8">
                            <div class="block" :style='{height:"60px"}'>
                                <div id="Fin压装1" class="light">
                                    <i class="iconfont el-icon-blinklightdengpao" style="margin-top:-15px;margin-left:-20px"></i>
                                </div>
                                <div class="modName" :style='{margin:"17% 0%"}'>Fin_1</div>
                            </div>
                        </el-col>
                        <el-col :span="8">
                            <div class="block" :style='{height:"60px"}'>
                                <div id="Fin压装2" class="light">
                                    <i class="iconfont el-icon-blinklightdengpao" style="margin-top:-15px;margin-left:-20px"></i>
                                </div>
                                <div class="modName" :style='{margin:"17% 0%"}'>Fin_2</div>
                            </div>
                        </el-col>
                        <el-col :span="8">
                            <div class="block" :style='{height:"60px"}'>
                                <div id="Fin压装3" class="light">
                                    <i class="iconfont el-icon-blinklightdengpao" style="margin-top:-15px;margin-left:-20px"></i>
                                </div>
                                <div class="modName" :style='{margin:"17% 0%"}'>Fin_3</div>
                            </div>
                        </el-col>
                    </el-row>
                    <el-row>
                        <div class="block" :style='{height:"64px"}'>
                            <div class="light">
                                <i class="iconfont el-icon-blinklightdengpao" style="margin-top:-15px;margin-left:-20px"></i>
                            </div>
                            <div class="modName" style="margin-top:20px">Ovl Tube</div>
                        </div>
                    </el-row>
                </el-card>
            </el-col>
            <el-col :span="6">
                 <el-card :style='{height:"365px",background:"#DCDCDC"}'>
                    <div style="text-align:center;font-weight:2px;font-size:30px;background:rgba(255,255,255,0.8)">IC1</div>
                    <el-row>
                        <el-col :span="12"> 
                            <div class="block" :style='{height:"280px"}'>
                                <div id="Huebers 1" class="light">
                                    <i class="iconfont el-icon-blinklightdengpao" style="margin-top:-15px;margin-left:-20px"></i>
                                </div>
                                <div  class="modName" :style='{margin:"55% 25%"}'>Huebers1</div>
                            </div>
                        </el-col>
                        <el-col :span="12">
                            <div class="block" :style='{height:"136px"}'>
                                <div id="IPTE 1" class="light">
                                    <i class="iconfont el-icon-blinklightdengpao" style="margin-top:-15px;margin-left:-20px"></i>
                                </div>
                                <div  class="modName" :style='{margin:"8% 30%"}'>IPTE_1</div>
                            </div>
                            <div class="block" :style='{height:"136px"}'>
                                <div id="Marsilli 1" class="light">
                                    <i class="iconfont el-icon-blinklightdengpao" style="margin-top:-15px;margin-left:-20px"></i>
                                </div>
                                <div  class="modName" :style='{margin:"8% 30%"}'>Marsilli1</div>
                            </div>
                        </el-col>
                    </el-row>
                   
                    
                </el-card>
               <el-row>
                   <el-col :span="12">
                        
                       <el-card :style='{height:"180px","margin-top":"13px","margin-right":"5px",background:"#DDA0DD"}'><!--#CD69C9-->
                           <div style="text-align:center;font-weight:2px;font-size:30px;background:rgba(255,255,255,0.8)">
                                Air Heater
                            </div>
                            <el-row>
                                <el-col :span="12">
                                    <div class="block" :style='{height:"100px"}'>
                                        <div id="LV Air PTC Line" class="light">
                                            <i class="iconfont el-icon-blinklightdengpao" style="margin-top:-15px;margin-left:-20px"></i>
                                        </div>
                                        <div  class="modName" :style='{margin:"15% 3%","font-size":"15px"}'>LV_PTC</div>
                                    </div>
                                </el-col>
                                <el-col :span="12">
                                    <div class="block" :style='{height:"100px"}'>
                                        <div id="HV Air PTC Line" class="light">
                                            <i class="iconfont el-icon-blinklightdengpao" style="margin-top:-15px;margin-left:-20px"></i>
                                        </div>
                                        <div  class="modName" :style='{margin:"15% 3%","font-size":"15px"}'>HV_PTC</div>
                                    </div>
                                </el-col>
                            </el-row>
                       </el-card>
                   </el-col>
                   <el-col :span="12">
                       <el-card  :style='{height:"180px","margin-top":"13px","margin-left":"5px",background:"#FFC0CB"}'>
                            <div style="text-align:center;font-weight:2px;font-size:30px;background:rgba(255,255,255,0.8)">
                               eCRV
                            </div>
                            <div class="block" :style='{height:"100px"}'>
                                <div id="eCRV Line" class="light">
                                    <i class="iconfont el-icon-blinklightdengpao" style="margin-top:-15px;margin-left:-20px"></i>
                                </div>
                                <div  class="modName" :style='{margin:"20% 5%"}'>eCRV</div>
                            </div>
                       </el-card>
                    </el-col>
                </el-row>
                 <el-card :style='{height:"340px","margin-top":"13px",background:"#D0ECFA"}'>
                    <div style="text-align:center;font-weight:2px;font-size:30px;background:rgba(255,255,255,0.8)">
                        Thermostat
                    </div>
                    <el-row>
                        <el-col :span="8">
                            <div class="block" style="height:110px">
                                <div id="Ford节温器线" class="light">
                                    <i class="iconfont el-icon-blinklightdengpao" style="margin-top:-15px;margin-left:-20px"></i>
                                </div>
                                <div class="modName" style="margin:40% 0%">Ford</div>
                            </div>
                        </el-col>
                        <el-col :span="16">
                            <div class="block" style="height:110px">
                                <div id="蜡包装配线" class="light">
                                    <i class="iconfont el-icon-blinklightdengpao" style="margin-top:-15px;margin-left:-20px"></i>
                                </div>
                                <div class="modName" style="margin:18% 10%">蜡包装配线</div>
                            </div>
                        </el-col>
                    </el-row>
                    <el-row>
                        <el-col :span="6">
                            <div class="block" style="height:150px">
                                <div id="JLR节温器线" class="light">
                                    <i class="iconfont el-icon-blinklightdengpao" style="margin-top:-15px;margin-left:-20px"></i>
                                </div>
                                <div class="modName" style="margin:35% 25%">JLR</div>
                            </div>
                        </el-col>
                        <el-col :span="6">
                            <div class="block" style="height:71px">
                                <div id="SGMM节温器线" class="light">
                                    <i class="iconfont el-icon-blinklightdengpao" style="margin-top:-15px;margin-left:-20px"></i>
                                </div>
                                <div class="modName" style="margin:5% 5%">SGMM</div>
                            </div>
                            <div class="block" style="height:71px">
                                <div id="SGME节温器线" class="light">
                                    <i class="iconfont el-icon-blinklightdengpao" style="margin-top:-15px;margin-left:-20px"></i>
                                </div>
                                <div class="modName" style="margin:5% 5%">SGME</div>
                            </div>
                        </el-col>
                        <el-col :span="6">
                            <div class="block" style="height:150px">
                                <div id="GAC节温器线" class="light">
                                    <i class="iconfont el-icon-blinklightdengpao" style="margin-top:-15px;margin-left:-20px"></i>
                                </div>
                                <div class="modName" style="margin:40% 15%">GAC</div>
                            </div>
                        </el-col>
                        <el-col :span="6">
                            <div class="block" style="height:150px">
                                <div id="HKMC节温器线" class="light">
                                    <i class="iconfont el-icon-blinklightdengpao" style="margin-top:-15px;margin-left:-20px"></i>
                                </div>
                                <div class="modName" style="margin:40% 10%">HKMC</div>
                            </div>
                        </el-col>
                    </el-row>
                    <div class="block" style="height:308px">
                        <div class="light">
                            <i class="iconfont el-icon-blinklightdengpao" style="margin-top:-15px;margin-left:-20px"></i>
                        </div>
                        <div class="modName">reservedPos</div>
                    </div>
                </el-card>
               
            </el-col>
            <el-col :span="2">
                <div style="height:912px;background:#324157">
                    <div class="lineName" >安灯类型</div>
                    <div style="height:150px">
                        <div class="redL" ><strong style="font-size:30px">{{zl}}</strong></div>
                        <div :style='{"text-align":"center",margin:"5% 5%","font-size":"20px",color:"#fff"}'>质量</div>
                    </div>
                    <div style="height:150px">
                        <div class="blueL"><strong style="font-size:30px">{{sb}}</strong></div>
                        <div :style='{"text-align":"center",margin:"5% 5%","font-size":"20px",color:"#fff"}'>设备</div>
                    </div>
                    <div style="height:150px">
                        <div class="purpleL"><strong style="font-size:30px">{{gy}}</strong></div>
                        <div :style='{"text-align":"center",margin:"5% 5%","font-size":"20px",color:"#fff"}'>工艺</div>
                    </div>
                    <div style="height:150px">
                        <div class="greenL"><strong style="font-size:30px">{{wl}}</strong></div>
                        <div :style='{"text-align":"center",margin:"5% 5%","font-size":"20px",color:"#fff"}'>物料</div>
                    </div>
                    <div style="height:150px">
                        <div class="yellowL"><strong style="font-size:30px">{{gl}}</strong></div>
                        <div :style='{"text-align":"center",margin:"5% 5%","font-size":"20px",color:"#fff"}'>TBD</div>
                    </div>
                </div>
            </el-col>
        </el-row>
   
        <el-row :gutter="20">
             <el-card  style="margin-top:8px">
                 <el-table :data="tableData" :style='{"font-size":"20px","font-weight":"bold"}'>
                    <el-table-column prop="number" label="序号" width="100" align=center></el-table-column>
                    <el-table-column prop="status" label="是否停线" width="100" align=center></el-table-column>
                    <el-table-column prop="station" label="工作中心" width="180" align=center></el-table-column>
                    <el-table-column prop="equipNum" label="设备编号" width="180" align=center></el-table-column>
                    <el-table-column prop="type" label="按灯类型" width="100" align=center></el-table-column>
                    <el-table-column prop="probDesc" label="问题描述" header-align="center"></el-table-column>
                    <el-table-column prop="andonTime" label="安灯时间" width="260" align=center></el-table-column>
                    <el-table-column prop="respTime" label="响应时间(mins)" width="100" align=center></el-table-column>
                    <el-table-column prop="time" label="持续时间" width="100" align=center></el-table-column>
                    <el-table-column prop="currStatus" label="当前状态" width="100" align=center></el-table-column>
                    <el-table-column prop="respMan" label="响应人" width="100" align=center></el-table-column>

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
            scaleSize:1,
            interval:0,
            lightBuffer:[],
            test:0,
            zl:0,
            sb:0,
            gy:0,
            wl:0,
            gl:0
            
        }
    },
    methods:{
        renewal(){
            this.test++;
            console.log('第'+this.test+'次扫描');
            fetch('api/Andon/andonStatus' ,{
                method:'POST',
                headers: {
                    'Content-Type': 'application/json'
                }
            })
            // .then(response => response.text())
            .then(response => response.json())
            .then(data => {
                console.log(data)
                this.tableData = []
                // if(data.length == 0){
                //     return;
                // }
                for(var item of data){
                    var sts = null;
                    switch(item.当前状态){
                        case 1:
                            sts = "待处理";
                            break;
                        case 2:
                            sts = "正在处理";
                            break;
                        case 3:
                            sts = "处理完成";
                            break;
                    }
                    this.tableData.push({number:item.序号,status:item.是否停线,station:item.工作中心,equipNum:item.设备编号,type:item.类别描述,probDesc:item.问题描述,andonTime:item.按灯时间,respTime:item.响应时间,time:item.持续时间,currStatus:sts,respMan:item.处理人});
                }
                if(this.lightBuffer.length == 0){
                     console.log("if-true")
                    for(var item of data){
                        this.lightBuffer.push({num:item.序号,line:item.工作中心,status:item.当前状态,type:item.类别描述})
                        switch(item.类别描述){
                             case "质量"://红灯 质量
                                this.zl+=1;
                                break;
                            case "设备"://蓝灯 设备
                                this.sb+=1;
                                break;
                            case "工艺"://紫灯 工艺
                                this.gy+=1;
                                break;
                            case "物料"://绿灯 物料
                                this.wl+=1;
                                break;
                            case "TBD"://黄灯 TBD
                                this.gl+=1;
                                break;
                        }
                    }
                }else{
                    console.log("if-false")
                    for(var gs of this.lightBuffer){
                         gs.status = 4
                    }
                    for(var i1 of data){
                        var check = 0 //if new data checked, add it into the buffer
                        for(var i2 of this.lightBuffer){
                           
                            if(i1.序号 == i2.num){
                                // console.log(i2.num)
                                i2.line = i1.工作中心
                                i2.status = i1.当前状态
                                i2.type = i1.类别描述
                                check = 1
                            }
                            // console.log(i2.status)
                        }
                        if(check==0){
                            this.lightBuffer.push({num:i1.序号,line:i1.工作中心,status:i1.当前状态,type:i1.类别描述})
                            switch(i1.类别描述){
                             case "质量"://红灯 质量
                                this.zl+=1;
                                break;
                            case "设备"://蓝灯 设备
                                this.sb+=1;
                                break;
                            case "工艺"://紫灯 工艺
                                this.gy+=1;
                                break;
                            case "物料"://绿灯 物料
                                this.wl+=1;
                                break;
                            case "TBD"://黄灯 TBD
                                this.gl+=1;
                                break;
                        }
                        }
                    }
                }
                console.log(this.lightBuffer)
                for(var l of this.lightBuffer){
                    console.log("222"+l.line)
                    var light = null;
                    if(l.line=="测试台1"||l.line=="测试台2"||l.line=="测试台3"||l.line=="测试台4"){
                        light = document.getElementById("测试台");
                    }else{
                        light = document.getElementById(l.line);
                    }
                    
                    if(l.status==1){
                        console.log("状态1")
                        light.style.background = ""
                         switch(l.type){
                            case "质量"://红灯 质量
                                light.className = "blinkRed";
                                break;
                            case "设备"://蓝灯 设备
                                light.className = "blinkBlue";
                                break;
                            case "工艺"://紫灯 工艺
                                light.className = "blinkPurple";
                                break;
                            case "物料"://绿灯 物料
                                light.className = "blinkGreen";
                                break;
                            case "TBD"://黄灯 TBD
                                light.className = "blinkYellow";
                                break;
                        }
                    }
                    else if(l.status==2){
                        console.log("状态2")
                        light.className = "lightActive"
                       switch(l.type){
                            case "质量"://红灯 质量
                                light.style.background = "#b61414";
                                break;
                            case "设备"://蓝灯 设备
                                // light.setAttribute("background","#b61414")
                                light.style.background = "#0ba0ea";
                                break;
                            case "工艺"://紫灯 工艺
                                light.style.background = "#9914e6";
                                break;
                            case "物料"://绿灯 物料
                                light.style.background = "#1aee48";
                                break;
                            case "TBD"://黄灯 TBD
                                light.style.background = "#fbff23";
                                break;
                            default:
                                light.style.background = "";
                                break;
                        }
                    }
                    else if(l.status==3){
                        light.className = "lightActive";
                        light.style.background = "#fff";
                    }else{
                        light.style.background = "";
                        light.className="light"
                    }
                    console.log("循环次数")
                }
                for(var item of this.lightBuffer){
                    if(item.status==4){
                        var light = document.getElementById(l.line);
                        light.className="light";
                        light.style.background="";

                        this.lightBuffer.splice(this.lightBuffer.indexOf(item),1)
                        switch(item.type){
                             case "质量"://红灯 质量
                                this.zl-=1;
                                break;
                            case "设备"://蓝灯 设备
                                this.sb-=1;
                                break;
                            case "工艺"://紫灯 工艺
                                this.gy-=1;
                                break;
                            case "物料"://绿灯 物料
                                this.wl-=1;
                                break;
                            case "TBD"://黄灯 TBD
                                this.gl-=1;
                                break;
                        }
                    }
                }
            }).catch(data => {
                alert(data);
            })
        }
    },
    mounted(){
       this.renewal()
       this.interval = setInterval(this.renewal,10000);   
    },
    destroyed(){
        clearInterval(this.interval);
    }
}
</script>
<style scoped>
.mainContainer{
    padding: 30px;
    background: #324157;
    border: 1px solid #ddd;
    border-radius: 5px;
}

.lineName{
    text-align:center;
    font-weight:2px;
    font-size:25px;
    background:rgba(255,255,255,0.8);
    /* margin-top:15%;
    margin-bottom:15%; */
}

.modName{
    text-align:center;
   
    font-weight:bold;
    font-size:20px;
    /* background:rgba(255,255,255,0.8); */
    display:inline-block;
}

.block{
    border-radius:10px;
    border:1px solid rgba(0,0,0,0.3);
    margin:5px;
    
}

.light{
    width:30px;
    height:30px;
    border-radius:50%;
    /* border:1px solid rgba(0,0,0,1); */
    margin:5px;
    float:left;
    visibility:hidden;
    /* font-size:50px; */
    opacity:0.8
}

.lightActive{
    width:30px;
    height:30px;
    border-radius:50%;
    /* border:1px solid rgba(0,0,0,1); */
    margin:5px;
    float:left;
    /* font-size:50px; */
    opacity:0.8
}


.redL{
    border-radius:50%;
    /* background:#b61414; */
    border:5px solid #b61414;
    height:50px;
    width:50px;
    box-shadow:0px 0px 10px #b61414;
    margin:20% 30%;
    /* float:left; */
    text-align:center;
    line-height:50px;
    color:#fff
}

.blueL{
    border-radius:50%;
    /* background :#0ba0ea; */
    border:5px solid #0ba0ea;
    height:50px;
    width:50px;
    box-shadow:0px 0px 10px #0ba0ea;
    margin:20% 30%;
    /* float:left; */
    text-align:center;
    line-height:50px;
    color:#fff
}

.purpleL{
    border-radius:50%;
    /* background: #9914e6; */
    border:5px solid #9914e6;
    height:50px;
    width:50px;
    box-shadow:0px 0px 10px #9914e6;
    margin:20% 30%;
    /* float:left; */
    text-align:center;
    line-height:50px;
    color:#fff
}

.greenL{
    border-radius:50%;
    /* background:#1aee48; */
    border:5px solid #1eea48;
    height:50px;
    width:50px;
    box-shadow:0px 0px 10px #1aee48;
    margin:20% 30%;
    /* float:left; */
    text-align:center;
    line-height:50px;
    color:#fff
}

.yellowL{
    border-radius:50%;
    /* background:#fbff23; */
    border:5px solid #fbff23;
    height:50px;
    width:50px;
    box-shadow:0px 0px 10px #fbff23;
    margin:20% 30%;
    /* float:left; */
    text-align:center;
    line-height:50px;
    color:#fff
}

@keyframes breatheBlue {
	0% {
        opacity: .4;
        box-shadow: 0 1px 2px rgba(0, 147, 223, 0.4), 0 1px 1px rgba(0, 147, 223, 0.1) inset;
        background:  rgb(146, 145, 145);
    }

    100% {
        opacity: 1;
        border: 1px solid rgba(59, 235, 235, 0.7);
        box-shadow: 0 1px 30px #0093df, 0 1px 20px #0093df inset;
        background: #0ba0ea;
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

@keyframes breathePurple {
	0% {
        opacity: .4;
        box-shadow: 0 1px 2px rgba(0, 147, 223, 0.4), 0 1px 1px rgba(0, 147, 223, 0.1) inset;
        background: rgb(146, 145, 145);
    }

    100% {
        opacity: 1;
        border: 1px solid rgba(59, 235, 235, 0.7);
        box-shadow: 0 1px 30px #c06fe0, 0 1px 20px #8a0af3 inset;
        background: #9914e6;
    }
}

@keyframes breatheGreen {
	0% {
        opacity: .4;
        box-shadow: 0 1px 2px rgba(0, 147, 223, 0.4), 0 1px 1px rgba(0, 147, 223, 0.1) inset;
        background: rgb(146, 145, 145);
    }

    100% {
        opacity: 1;
        border: 1px solid rgba(59, 235, 235, 0.7);
        box-shadow: 0 1px 30px #76ecab, 0 1px 20px #0cf32b inset;
        background: #1aee48;
    }
}

@keyframes breatheYellow {
	0% {
        opacity: .4;
        box-shadow: 0 1px 2px rgba(0, 147, 223, 0.4), 0 1px 1px rgba(0, 147, 223, 0.1) inset;
        background: rgb(146, 145, 145);
    }

    100% {
        opacity: 1;
        border: 1px solid rgba(59, 235, 235, 0.7);
        box-shadow: 0 1px 30px #e0de4b, 0 1px 20px #fffb00 inset;
        background: #fbff23;
    }
}

.blinkRed{
    width:30px;
    height:30px;
    border-radius:50%;
    border:1px solid rgba(0,0,0,1);
    margin:5px;
    float:left;
    font-size:20px;
    animation-timing-function: ease-in-out;
    animation-name: breatheRed;
    animation-duration: 1500ms;
    animation-iteration-count: infinite;
    animation-direction:alternate;
}

.blinkBlue{
     width:30px;
    height:30px;
    border-radius:50%;
    border:1px solid rgba(0,0,0,1);
    margin:5px;
    float:left;
    font-size:25px;
    /* color:rgba(255,255,255,0.5); */
    animation-timing-function: ease-in-out;
    animation-name: breatheBlue;
    animation-duration: 1500ms;
    animation-iteration-count: infinite;
    animation-direction:alternate;
}

.blinkPurple{
     width:30px;
    height:30px;
    border-radius:50%;
    border:1px solid rgba(0,0,0,1);
    margin:5px;
    float:left;
    font-size:20px;
    animation-timing-function: ease-in-out;
    animation-name: breathePurple;
    animation-duration: 1500ms;
    animation-iteration-count: infinite;
    animation-direction:alternate;
}

.blinkGreen{
     width:30px;
    height:30px;
    border-radius:50%;
    border:1px solid rgba(0,0,0,1);
    margin:5px;
    float:left;
    font-size:20px;
    animation-timing-function: ease-in-out;
    animation-name: breatheGreen;
    animation-duration: 1500ms;
    animation-iteration-count: infinite;
    animation-direction:alternate;
}

.blinkYellow{
     width:30px;
    height:30px;
    border-radius:50%;
    border:1px solid rgba(0,0,0,1);
    margin:5px;
    float:left;
    font-size:20px;
    animation-timing-function: ease-in-out;
    animation-name: breatheYellow;
    animation-duration: 1500ms;
    animation-iteration-count: infinite;
    animation-direction:alternate;
}

</style>