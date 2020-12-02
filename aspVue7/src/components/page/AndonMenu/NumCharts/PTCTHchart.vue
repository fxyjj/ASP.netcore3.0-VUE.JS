<template>
        <div id="PTCTHchart" :style="{width:'50%',height:'320px',display:'inline-block'}"></div>
</template>

<script>
import echarts from 'echarts';
import bus from '../../../common/bus';
export default {
    name:'PTCTHchart',
    data(){
        return {
            chart: null,
            //queryData
            starttime: null,
            endtime:null,
            dateunit:null,
            //querydata from database
            queryData:[],
             //optionPTC data
            Xdata:[],
            ecrvYdata:[],
            hvYdata:[],
            lvYdata:[],
            fordYdata:[],
            gacYdata:[],
            hkmcYdata:[],
            jlrYdata:[],
            sgmeYdata:[],
            laYdata:[],
            optionPTC: {
                title: {
                   text: 'PTC/TH Value Line 安灯数量',
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
                    data:['eCRV Line','HV Air PTC Line','LV Air PTC Line','Ford节温器线','GAC节温器线','HKMC节温器线','JLR节温器线','SGME节温器线','蜡包装配线']
                },
                xAxis: {
                    data: ["1","2","3","4","5","6","7"]
                },
                yAxis: {},
                series: [{
                            name: 'eCRV Line',
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
                            name: 'HV Air PTC Line',
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
                            name: 'LV Air PTC Line',
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
                            name: 'Ford节温器线',
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
                            name: 'GAC节温器线',
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
                            name: 'HKMC节温器线',
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
                            name: 'JLR节温器线',
                            type: 'line',
                            data: [19,5,17,23,9,21,14],
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
                            name: 'SGME节温器线',
                            type: 'line',
                            data: [1,32,3,6,29,20,10],
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
                            name: '蜡包装配线',
                            type: 'line',
                            data: [6,9,34,23,19,20,41],
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
                this.Xdata = []
                this.ecrvYdata = []
                this.hvYdata = []
                this.lvYdata = []
                this.fordYdata = []
                this.gacYdata = []
                this.hkmcYdata = []
                this.jlrYdata = []
                this.sgmeYdata = []
                this.laYdata = []
                 var monthName = ['Jan','Feb','Mar','Apr','May','Jun','Jul','Aug','Sept','Oct','Nov','Dec'] 
                for(var item of this.queryData){
                    if(this.dateunit == '月'){
                        this.Xdata.push(monthName[Number(item.日期单位-1)]);
                    }else{
                        this.Xdata.push(item.日期单位);
                    }
                    // this.Xdata.push(item.日期单位);
                    this.ecrvYdata.push(item.ecrvNum);
                    this.hvYdata.push(item.hvNum);
                    this.lvYdata.push(item.lvNum);
                    this.fordYdata.push(item.fordNum);
                    this.gacYdata.push(item.gacNum);
                    this.hkmcYdata.push(item.hkmcNum);
                    this.jlrYdata.push(item.jlrNum);
                    this.sgmeYdata.push(item.sgmeNum);
                    this.laYdata.push(item.laNum);

                }
                this.optionPTC.xAxis.data = this.Xdata;
                this.optionPTC.series[0].data = this.ecrvYdata;
                this.optionPTC.series[1].data = this.hvYdata;
                this.optionPTC.series[2].data = this.lvYdata;
                this.optionPTC.series[3].data = this.fordYdata;
                this.optionPTC.series[4].data = this.gacYdata;
                this.optionPTC.series[5].data = this.hkmcYdata;
                this.optionPTC.series[6].data = this.jlrYdata;
                this.optionPTC.series[7].data = this.sgmeYdata;
                this.optionPTC.series[8].data = this.laYdata;

                var c1 = document.getElementById("PTCTHchart");
                this.chart = echarts.init(c1);
                this.chart.setOption(this.optionPTC);

            }
        }
    },
    methods:{
        getData(){
            fetch('api/Andon/PTCTHdata',{
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
        var c1 = document.getElementById("PTCTHchart");
        this.chart = echarts.init(c1);
        this.chart.setOption(this.optionPTC);

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