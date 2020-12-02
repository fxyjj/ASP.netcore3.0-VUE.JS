<template>
        <div id="CMchart" :style="{width:'50%',height:'320px',display:'inline-block'}"></div>
</template>

<script>
import echarts from 'echarts';
import bus from '../../../common/bus';
export default {
    name:'CMchart',
    data(){
        return {
            chart: null,
             //queryData
            starttime: null,
            endtime:null,
            dateunit:null,
            //querydata from database
            queryData:[],
             //optionCM data
            Xdata:[],
            cm1Ydata:[],
            cm2Ydata:[],
            cm3Ydata:[],
            mixYdata:[],
            mod1Ydata:[],
            mod2Ydata:[],
            optionCM: {
                title: {
                    text: 'Cooler & Module Value Line 安灯数量',
                    left: '50%',
                    textAlign: 'center'
                },
                tooltip: { 
                    trigger: 'axis',
                    axisPointer: {
                        type: 'cross',
                        label: {
                            backgroundColor: '#283b56'
                        }
                    }
                },
                legend: {
                    left:'right',
                    orient:'vertical',
                    selectMode:true,
                    data:['Cooler Line 1','Cooler Line 2','Cooler Line 3','混合管装配线','模块线1','模块线2']
                },
                xAxis: {
                    data: ["1","2","3","4","5","6","7"]
                },
                yAxis: {},
                series: [{
                            name: 'Cooler Line 1',
                            type: 'line',
                            // data: [5,20,36,10,10,20],
                                data:[3,7,12,6,5,1,9],
                            itemStyle:{
                                color:"#c0504d"
                            },
                            lineStyle:{
                                normal:{
                                    width:3
                                }
                            }
                        },
                        {
                            name: 'Cooler Line 2',
                            type: 'line',
                            data: [5,20,36,10,10,20,8],
                            itemStyle:{
                                color: ["#0070C0"]
                            },
                            lineStyle:{
                                normal:{
                                    width:3
                                }
                            }
                        },
                        {
                            name: 'Cooler Line 3',
                            type: 'line',
                            data: [5,50,21,30,13,24,7],
                            itemStyle:{
                                color: ["#AAAAFF"]
                            },
                            lineStyle:{
                                normal:{
                                    width:3
                                }
                            }
                        },
                        {
                            name: '混合管装配线',
                            type: 'line',
                            data: [15,5,1,10,23,14,6],
                            itemStyle:{
                                color: ["#ACD6FF"]
                            },
                            lineStyle:{
                                normal:{
                                    width:3
                                }
                            }
                        },
                        {
                            name: '模块线1',
                            type: 'line',
                            data: [5,25,23,19,29,17,5],
                            itemStyle:{
                                color: ["#CCFF80"]
                            },
                            lineStyle:{
                                normal:{
                                    width:3
                                }
                            }
                        },
                        {
                            name: '模块线2',
                            type: 'line',
                            data: [9,12,33,6,49,2,4],
                            itemStyle:{
                                color: ["#FFED97"]
                            },
                            lineStyle:{
                                normal:{
                                    width:3
                                }
                            }
                        }],
            },
        }
    },
    watch:{
        'queryData.length':{
            handler:function(oldVal,newVal){
                // eslint-disable-next-line no-console
                console.log(oldVal+'到'+newVal);
                this.Xdata = []
                this.cm1Ydata = []
                this.cm2Ydata = []
                this.cm3Ydata = []
                this.mixYdata = []
                this.mod1Ydata = []
                this.mod2Ydata = []
                var monthName = ['Jan','Feb','Mar','Apr','May','Jun','Jul','Aug','Sept','Oct','Nov','Dec'] 
                for(var item of this.queryData){
                    if(this.dateunit == '月'){
                        this.Xdata.push(monthName[Number(item.日期单位-1)]);
                    }else{
                        this.Xdata.push(item.日期单位);
                    }
                    // this.Xdata.push(item.日期单位);
                    this.cm1Ydata.push(item.cM1Num);
                    this.cm2Ydata.push(item.cM2Num);
                    this.cm3Ydata.push(item.cM3Num);
                    this.mixYdata.push(item.mixNum);
                    this.mod1Ydata.push(item.mod1Num);
                    this.mod2Ydata.push(item.mod2Num);
                }
                this.optionCM.xAxis.data = this.Xdata;
                this.optionCM.series[0].data = this.cm1Ydata;
                this.optionCM.series[1].data = this.cm2Ydata;
                this.optionCM.series[2].data = this.cm3Ydata;
                this.optionCM.series[3].data = this.mixYdata;
                this.optionCM.series[4].data = this.mod1Ydata;
                this.optionCM.series[5].data = this.mod2Ydata;

                var c1 = document.getElementById("CMchart");
                this.chart = echarts.init(c1);
                this.chart.setOption(this.optionCM);

            }
        }
    },
    methods:{
        getData(){
            fetch('api/Andon/CMdata',{
                method:'POST',
                headers:{
                    'Content-Type':'application/json'
                },
                body:JSON.stringify({
                    start: this.starttime,
                    end: this.endtime,
                    dateunit:this.dateunit
                })
            })
            .then(response => response.json())
            // .then(response => response.text())
            .then(data =>{
                this.queryData = data
            })
            .catch(data =>{
                alert('error!')
            })
        }
    },
    mounted(){
        var c1 = document.getElementById("CMchart");
        this.chart = echarts.init(c1);
        this.chart.setOption(this.optionCM);

        bus.$on('AndonNumquery', msg =>{
            this.queryData = [];
            this.dateunit = msg.dateunit;
            this.starttime = msg.start;
            this.endtime = msg.end;
            this.getData()
        })
    }
}
</script>