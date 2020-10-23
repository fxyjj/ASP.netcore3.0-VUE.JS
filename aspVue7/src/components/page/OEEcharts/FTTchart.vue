<template>
<div id="FTTchart" :style="{width:'100%',height:'315px',display:'inline-block'}"></div>

</template>
<script>
import echarts from 'echarts';
import bus from '../../common/bus';
// queryData:{
//     dateunit:'';
//     oee:0.0;
//     目标:0.0
// }

export default {
    name:"FTTchart",
    data() {
        return{
            input2: 'i2',
            // jack : new HTMLDivElement(),
            cdate : [],
            stime:{
                dateunit:'',
                prodline:'',
                starttime: new Date(),
                endtime:new Date()
            },
            charts : null,
            barXdata : [],
            clr_val: {value:0,color:''},
            barYdata : [],
            lineYdata: [],
            //通过bus传过来的数据
            startTime:new Date(),
            endTime:new Date(),
            dataUnit: "",
            prodLine:"",
            option : {
                title: {
                    text: '一次合格率',
                    textStyle:{
                        color:"#000"
                    }
                },
                tooltip: {},
                legend: {
                    selectMode:true,
                    data:['一次合格率','目标'],
                    textStyle:{
                        color:"#000"
                    }
                },
                xAxis: {
                    data: [],
                    axisLine:{
                        lineStyle:{
                            color:'#000',
                            // width:8,//这里是为了突出显示加上的
                        }
                    }
                },
                yAxis: {
                    axisLine:{
                        lineStyle:{
                            color:'#000',
                            // width:8,//这里是为了突出显示加上的
                        }
                    }
                },
                series: [{
                            name: '一次合格率',
                            type: 'bar',
                            // data: [5,20,36,10,10,20],
                          data:[],
                            itemStyle:{
                                normal:{
                                    label:{
                                        show:true,
                                        position:'top',
                                        // textStyle:{}
                                    },
                                    // color:function(params: { dataIndex: string | number;data: number; }) {
                                    color:function(params) {
                                        // build a color map as your need.
                                        var colorList = [
                                            '#C1232B','#B5C334'
                                        ];
                                        //console.log(params);
                                        // eslint-disable-next-line no-console
                                        // console.log("this is dataindex : "+params.data)
                                        
                                        //console.log(colorList[params.dataIndex])
                                        //按条件修改柱状图颜色
                                        if(params.data['color'] != undefined){
                                            // console.log("this one is larger than 10 : "+params.data);
                                            return params.data['color'];
                                        }else{
                                            return colorList[1]
                                        }
                                    }, 
                                }
                            }
                        },
                        {
                            name: '目标',
                            type: 'line',
                            data: [],
                            itemStyle:{
                                color: ["#0070C0"]
                            },
                            lineStyle:{
                                normal:{
                                    width:3
                                }
                            }
                        }
                        ],
            }
        }
    },
    watch: {
        'cdate.length':{
            handler:function(newval,oldval){
                if(newval !== oldval){
                    // eslint-disable-next-line no-console
                    // console.log(newval+" 到 "+oldval)
                    this.getPage()
                }
                
        },
        deep:true//对象内部的属性监听，也叫深度监听
      },

    },
    methods: {
        //初始化表格横纵坐标
        //检测cdate数组长度发生改变是执行echart横坐标index改变，同时清空原来的横坐标，
        getPage() {
            // console.log(this.cdate);
            //清空上一次搜索日期的数据
            this.barXdata = [];
            this.barYdata = [];
            this.lineYdata = [];
            for(var i of this.cdate){
                this.barXdata.push(i.dateunit);
                
                var index = {value:Number(i.一次合格率.toFixed(1)),color:i.一次合格率>i.目标?'#3CB371':'#C0504D'};
                this.barYdata.push(index);
                this.lineYdata.push(i.目标);
            }
            // console.log("sss"+this.lineYdata);   
            //将从后端读取到的数据赋值到表格上
            this.option.xAxis.data=this.barXdata;
            this.option.series[0].data = this.barYdata;
            this.option.series[1].data = this.lineYdata;

            // this.jack = document.getElementById("FTTchart");
            // this.charts = echarts.init(this.jack);
            // // eslint-disable-next-line no-console
            // console.log("this is the param: ");
            this.charts.setOption(this.option);
        },
         //后端数据请求。
        getInfor(){
            fetch('api/Chartdata/FTTchartQuery' ,{
                method:'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify({
                    start: this.startTime,
                    end: this.endTime,
                    dateUnit: this.dateUnit,
                    prodLine: this.prodLine
                })
            })
            .then(response => response.json())
            // .then(response => response.text())
            .then(data => {
                this.cdate = data;
                // eslint-disable-next-line no-console
                // console.log("data from back end : "+this.cdate[0].oee);
                // eslint-disable-next-line no-console
                // console.log("data from back end : "+this.cdate[0].目标);
                // eslint-disable-next-line no-console
                // console.log("fttdata from back end : "+this.cdate.length);
                // alert('jack'+this.cdate);
            }).catch(data => {
                alert(data);
            })
        }
    },
    
     mounted(){
        bus.$on("Query", (stime) =>{
            this.cdate = []
            this.startTime = stime.starttime;
            this.endTime = stime.endtime;
            this.dateUnit = stime.dateunit;
            this.prodLine = stime.prodline;
            this.getInfor()
            // // eslint-disable-next-line no-console
            // console.log("fttthis is start time : "+this.startTime)
            //  // eslint-disable-next-line no-console
            // console.log("fttthis is end time : "+this.endTime)
            //  // eslint-disable-next-line no-console
            // console.log("fttthis is date unit : "+this.dateUnit)
            //  // eslint-disable-next-line no-console
            // console.log("fttthis is product line : "+this.prodLine)
            });
        var jack = document.getElementById("FTTchart");
        this.charts = echarts.init(jack);
        // eslint-disable-next-line no-console
        // console.log("this is the param: ");
        this.charts.setOption(this.option);

        bus.$on("colorMod",sts =>{
            if(sts){
                this.option.title.textStyle.color = "#000";
                this.option.legend.textStyle.color = "#000";
                this.option.xAxis.axisLine.lineStyle.color = "#000";
                this.option.yAxis.axisLine.lineStyle.color = "#000";
                this.charts.setOption(this.option);
            }else{
                this.option.title.textStyle.color = "#fff";
                this.option.legend.textStyle.color = "#fff";
                this.option.xAxis.axisLine.lineStyle.color = "#fff";
                this.option.yAxis.axisLine.lineStyle.color = "#fff";
                this.charts.setOption(this.option);
            }
        })
    }

    
}
</script>