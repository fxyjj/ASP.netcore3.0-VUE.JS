<template>
        <div id="PTCTHchart" :style="{width:'750px',height:'320px',display:'inline-block'}"></div>
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
             //option data
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
            option: {
                title: {
                   text: 'PTCTH Value Line 安灯数量',
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
                // eslint-disable-next-line no-console
                console.log(oldVal+'到'+newVal);
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
                for(var item of this.queryData){
                    this.Xdata.push(item.日期单位);
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
                this.option.xAxis.data = this.Xdata;
                this.option.series[0].data = this.ecrvYdata;
                this.option.series[1].data = this.hvYdata;
                this.option.series[2].data = this.lvYdata;
                this.option.series[3].data = this.fordYdata;
                this.option.series[4].data = this.gacYdata;
                this.option.series[5].data = this.hkmcYdata;
                this.option.series[6].data = this.jlrYdata;
                this.option.series[7].data = this.sgmeYdata;
                this.option.series[8].data = this.laYdata;

                var c1 = document.getElementById("PTCTHchart");
                this.chart = echarts.init(c1);
                this.chart.setOption(this.option);

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
                // alert(data)
            })
            .catch(data =>{
                alert('error!')
            })
        }
    },
    mounted(){
        var c1 = document.getElementById("PTCTHchart");
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