<template>
        <div id="EGRchart" :style="{width:'50%',height:'320px',display:'inline-block'}"></div>
</template>

<script>
import echarts from 'echarts';
import bus from '../../../common/bus';
export default {
    name:'EGRchart',
    data(){
        return {
            chart: null,
            //queryData
            starttime: null,
            endtime:null,
            dateunit:null,
            //querydata from database
            queryData:[],
            //optionEGR data
            Xdata:[],
            bpvYdata:[],
            vfYdata:[],
            softYdata:[],
            gdiYdata:[],
            amYdata:[],
            etvYdata:[],
            lpYdata:[],
            optionEGR: {
                title: {
                    text: 'EGR Value Line 安灯数量',
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
                    data:['GEN_III_BPV','GEN_III_V+F','柔性线','GDI阀装配线','GEN_III_A+M','ETV装配线','LP阀装配线']
                },
                xAxis: {
                    data: ["1","2","3","4","5","6","7"]
                },
                yAxis: {},
                series: [{
                            name: 'GEN_III_BPV',
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
                            name: 'GEN_III_V+F',
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
                            name: '柔性线',
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
                            name: 'GDI阀装配线',
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
                            name: 'GEN_III_A+M',
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
                            name: 'ETV装配线',
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
                        },
                        {
                            name: 'LP阀装配线',
                            type: 'line',
                            data: [2,20,13,16,29,8,3],
                            itemStyle:{
                                color: ["#C07AB8"]
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
                this.Xdata = []
                this.bpvYdata = []
                this.vfYdata = []
                this.softYdata = []
                this.gdiYdata = []
                this.amYdata = []
                this.etvYdata = []
                this.lpYdata = []
                var monthName = ['Jan','Feb','Mar','Apr','May','Jun','Jul','Aug','Sept','Oct','Dec','Nov'] 
                for(var item of this.queryData){
                    if(this.dateunit == '月'){
                        this.Xdata.push(monthName[Number(item.日期单位-1)]);
                    }else{
                        this.Xdata.push(item.日期单位);
                    }
                    // this.Xdata.push(item.日期单位);
                    this.bpvYdata.push(item.bpvNum);
                    this.vfYdata.push(item.vfNum);
                    this.softYdata.push(item.柔性线Num);
                    this.gdiYdata.push(item.gdiNum);
                    this.amYdata.push(item.amNum);
                    this.etvYdata.push(item.etvNum);
                    this.lpYdata.push(item.lpNum);
                }
                this.optionEGR.xAxis.data = this.Xdata;
                this.optionEGR.series[0].data = this.bpvYdata;
                this.optionEGR.series[1].data = this.vfYdata;
                this.optionEGR.series[2].data = this.softYdata;
                this.optionEGR.series[3].data = this.gdiYdata;
                this.optionEGR.series[4].data = this.amYdata;
                this.optionEGR.series[5].data = this.etvYdata;
                this.optionEGR.series[6].data = this.lpYdata;

                var c1 = document.getElementById("EGRchart");
                this.chart = echarts.init(c1);
                this.chart.setOption(this.optionEGR);

            }
        }
    },
    methods:{
        getData(){
            fetch('api/Andon/EGRdata',{
            method:'POST',
            headers:{
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({
                    start: this.starttime,
                    end: this.endtime,
                    dateunit:this.dateunit
                })
            })
            .then(response => response.json())
            // .then(response => response.text())
            .then(data =>{
                this.queryData = data
            }).catch(data =>{
                alert('error')
            })
        }
    },
    mounted(){
        var c1 = document.getElementById("EGRchart");
        this.chart = echarts.init(c1);
        this.chart.setOption(this.optionEGR);

        bus.$on('AndonNumquery',msg =>{
            this.queryData = [];
            this.dateunit = msg.dateunit;
            this.starttime = msg.start;
            this.endtime = msg.end;
            this.getData()
        })
    }
}
</script>