<template>
        <div id="ICchart" :style="{width:'50%',height:'320px',display:'inline-block'}"></div>
</template>

<script>
import echarts from 'echarts';
import bus from '../../../common/bus';
export default {
    name:'ICchart',
    data(){
        return {
            chart: null,
            //queryData
            starttime: null,
            endtime:null,
            dateunit:null,
            //querydata from database
            queryData:[],
             //option data
            Xdata:[],
            hb1Ydata:[],
            hb2Ydata:[],
            ipte1Ydata:[],
            ipte2Ydata:[],
            msl1Ydata:[],
            msl2Ydata:[],
            option: {
                title: {
                    text: 'IC Value Line 安灯数量',
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
                    data:['Huebers1','IPTE1','Marsilli1','Huebers2','IPTE2','Marsilli2']
                },
                xAxis: {
                    data: ["1","2","3","4","5","6","7"]
                },
                yAxis: {},
                series: [{
                            name: 'Huebers1',
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
                            name: 'IPTE1',
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
                            name: 'Marsilli1',
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
                            name: 'Huebers2',
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
                            name: 'IPTE2',
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
                            name: 'Marsilli2',
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
                this.hb1Ydata = []
                this.hb2Ydata = []
                this.ipte1Ydata = []
                this.ipte2Ydata = []
                this.msl1Ydata = []
                this.msl2Ydata = []
                 var monthName = ['Jan','Feb','Mar','Apr','May','Jun','Jul','Aug','Sept','Oct','Dec','Nov'] 
                for(var item of this.queryData){
                    if(this.dateunit == '月'){
                        this.Xdata.push(monthName[Number(item.日期单位-1)]);
                    }else{
                        this.Xdata.push(item.日期单位);
                    }
                    // this.Xdata.push(item.日期单位);
                    this.hb1Ydata.push(item.hb1Num);
                    this.hb2Ydata.push(item.hb2Num);
                    this.ipte1Ydata.push(item.iptE1Num);
                    this.ipte2Ydata.push(item.iptE2Num);
                    this.msl1Ydata.push(item.msl1Num);
                    this.msl2Ydata.push(item.msl2Num);
                }
                this.option.xAxis.data = this.Xdata;
                this.option.series[0].data = this.hb1Ydata;
                this.option.series[1].data = this.ipte1Ydata;
                this.option.series[2].data = this.msl1Ydata;
                this.option.series[3].data = this.hb2Ydata;
                this.option.series[4].data = this.ipte2Ydata;
                this.option.series[5].data = this.msl2Ydata;

                var c1 = document.getElementById("ICchart");
                this.chart = echarts.init(c1);
                this.chart.setOption(this.option);

            }
        }
    },
    methods:{
        getData(){
            fetch('api/Andon/ICdata',{
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
                // alert(data)
            })
            .catch(data =>{
                alert('error!')
            })
        }
    },
    mounted(){
        var c1 = document.getElementById("ICchart");
        this.chart = echarts.init(c1);
        this.chart.setOption(this.option);

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