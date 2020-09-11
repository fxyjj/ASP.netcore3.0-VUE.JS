<template>
    <div>
    <div id="AndonCount" :style="{width:'450px',height:'320px',display:'inline-block'}"></div>
    <div id="AndonWorkHour" :style="{width:'450px',height:'320px',display:'inline-block'}"></div>
    <div id="AndonWorkRate" :style="{width:'450px',height:'320px',display:'inline-block'}"></div>
    </div>
</template>

<script>
import echarts from 'echarts';
import bus from '../../../common/bus';
export default {
    name:"LineChart",
    data(){
        return {
            Countchart:null,
            Hourchart:null,
            Ratechart:null,
            //results from the back end
            queryData:[],
            //prms from the bus
            dateunit:null,
            start:null,
            end:null,
            status:null,
            //optionparam
            xdata:[],
            c1yg:[],
            c1ys:[],
            c1yc:[],
            c1yz:[],

            c2yg:[],
            c2ys:[],
            c2yc:[],
            c2yz:[],

            c3yg:[],
            c3ys:[],
            c3yc:[],
            c3yz:[],
            optionCount:{
                 title: {
                    text: '周安灯条数'
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
                    data:['工艺','设备','仓库','质量']
                },
                xAxis: {
                    data: ["1","2","3","4"]
                },
                yAxis: {},
                series: [{
                            name: '工艺',
                            type: 'line',
                            // data: [5,20,36,10,10,20],
                                data:[3,7,12,6],
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
                            name: '设备',
                            type: 'line',
                            // data: [5,20,36,10,10,20],
                                data:[5,20,36,10],
                            itemStyle:{
                                color:"#AAAAFF"
                            },
                            lineStyle:{
                                normal:{
                                    width:3
                                }
                            }
                        },
                        {
                            name: '仓库',
                            type: 'line',
                            data: [5,50,21,30],
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
                            name: '质量',
                            type: 'line',
                            data: [9,12,33,6],
                            itemStyle:{
                                color: ["#FFED97"]
                            },
                            lineStyle:{
                                normal:{
                                    width:3
                                }
                            }
                        }]
            },
             optionHour:{
                 title: {
                    text: '周安灯工时'
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
                    data:['工艺','设备','仓库','质量']
                },
                xAxis: {
                    data: ["1","2","3","4"]
                },
                yAxis: {},
                series: [{
                            name: '工艺',
                            type: 'line',
                            // data: [5,20,36,10,10,20],
                                data:[3,7,12,6],
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
                            name: '设备',
                            type: 'line',
                            // data: [5,20,36,10,10,20],
                                data:[5,20,36,10],
                            itemStyle:{
                                color:"#AAAAFF"
                            },
                            lineStyle:{
                                normal:{
                                    width:3
                                }
                            }
                        },
                        {
                            name: '仓库',
                            type: 'line',
                            data: [5,50,21,30],
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
                            name: '质量',
                            type: 'line',
                            data: [9,12,33,6],
                            itemStyle:{
                                color: ["#FFED97"]
                            },
                            lineStyle:{
                                normal:{
                                    width:3
                                }
                            }
                        }]
            },
             optionRate:{
                 title: {
                    text: '周安灯工时占排班比'
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
                    data:['工艺','设备','仓库','质量']
                },
                xAxis: {
                    data: ["1","2","3","4"]
                },
                yAxis: {},
                series: [{
                            name: '工艺',
                            type: 'line',
                            // data: [5,20,36,10,10,20],
                                data:[3,7,12,6],
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
                            name: '设备',
                            type: 'line',
                            // data: [5,20,36,10,10,20],
                                data:[5,20,36,10],
                            itemStyle:{
                                color:"#AAAAFF"
                            },
                            lineStyle:{
                                normal:{
                                    width:3
                                }
                            }
                        },
                        {
                            name: '仓库',
                            type: 'line',
                            data: [5,50,21,30],
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
                            name: '质量',
                            type: 'line',
                            data: [9,12,33,6],
                            itemStyle:{
                                color: ["#FFED97"]
                            },
                            lineStyle:{
                                normal:{
                                    width:3
                                }
                            }
                        }]
            }

        }
    },
    watch:{
        'queryData.length':{
            handler:function(oldVal,newVal){
                if(oldVal !== newVal){
                    console.log('进来啦');
                this.xdata=[]
                this.c1yg=[]
                this.c1ys=[]
                this.c1yc=[]
                this.c1yz=[]

                this.c2yg=[]
                this.c2ys=[]
                this.c2yc=[]
                this.c2yz=[]

                this.c3yg=[]
                this.c3ys=[]
                this.c3yc=[]
                this.c3yz=[]

                for(var i of this.queryData){
                    this.xdata.push(i.日期单位);

                    this.c1yg.push(i.gAndonN);
                    this.c1ys.push(i.sAndonN);
                    this.c1yc.push(i.cAndonN);
                    this.c1yz.push(i.zAndonN);

                    this.c2yg.push(i.gProcessT);
                    this.c2ys.push(i.sProcessT);
                    this.c2yc.push(i.cProcessT);
                    this.c2yz.push(i.zProcessT);

                    this.c3yg.push(i.gWorkRate);
                    this.c3ys.push(i.sWorkRate);
                    this.c3yc.push(i.cWorkRate);
                    this.c3yz.push(i.zWorkRate);
                }

                this.optionCount.xAxis.data = this.xdata;
                this.optionCount.series[0].data = this.c1yg;
                this.optionCount.series[1].data = this.c1ys;
                this.optionCount.series[2].data = this.c1yc;
                this.optionCount.series[3].data = this.c1yz;

                this.optionHour.xAxis.data = this.xdata;
                this.optionHour.series[0].data = this.c2yg;
                this.optionHour.series[1].data = this.c2ys;
                this.optionHour.series[2].data = this.c2yc;
                this.optionHour.series[3].data = this.c2yz;

                this.optionRate.xAxis.data = this.xdata;
                this.optionRate.series[0].data = this.c3yg;
                this.optionRate.series[1].data = this.c3ys;
                this.optionRate.series[2].data = this.c3yc;
                this.optionRate.series[3].data = this.c3yz;

                this.Countchart.setOption(this.optionCount);
                this.Hourchart.setOption(this.optionHour);
                this.Ratechart.setOption(this.optionRate);
                }
                
            }
        }
    },
    methods:{
        getData(){
            //TODO..
            fetch('api/Andon/category',{
                method:'POST',
                headers:{
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify({
                    start: this.start,
                    end: this.end,
                    dateunit: this.dateunit,
                    stus:this.status
                })
            })
            .then(response => response.json())
            // .then(response => response.text())
            .then(data =>{
                this.queryData = data;
                bus.$emit('backData',data);
                // alert(data);
                // eslint-disable-next-line no-console
                // console.log(this.queryData)
            })
            .catch(data =>{
                alert('error')
            })
        }
    },
    mounted(){
        var c1 = document.getElementById("AndonCount");
        this.Countchart = echarts.init(c1);
        this.Countchart.setOption(this.optionCount);

        var c2 = document.getElementById("AndonWorkHour");
        this.Hourchart = echarts.init(c2);
        this.Hourchart.setOption(this.optionHour);

        var c3 = document.getElementById("AndonWorkRate");
        this.Ratechart = echarts.init(c3);
        this.Ratechart.setOption(this.optionRate);
        bus.$on('catePrm',msg=>{
            this.dateunit = msg.dateunit
            this.start = msg.starttime
            this.end = msg.endtime
            this.status = msg.status
            // console.log("bus过来的参数:"+msg.dateunit);
            // console.log("bus过来的参数:"+msg.starttime);
            // console.log("bus过来的参数:"+msg.endtime);
            // console.log("bus过来的参数:"+msg.status);
            this.queryData = [];

            this.getData();

        })
    }
    
}
</script>