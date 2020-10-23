<template>
<div id="EFFchart" :style="{width:'100%',height:'315px',display:'inline-block'}"></div>
</template>
<script>
import echarts from 'echarts';
import bus from '../../common/bus';
export default {
    name:"EFFchart",
    data() {
        return{
            input2: 'i2',
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
                    text: '员工效率',
                    textStyle:{
                        color:"#000"
                    }
                },
                tooltip: {},
                legend: {
                    selectMode:true,
                    data:['员工效率','目标'],
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
                            name: '员工效率',
                            type: 'bar',
                            // data: [5,20,36,10,10,20],
                             data:[],
                            itemStyle:{
                                normal:{
                                    label:{
                                        show:true,
                                        position:'top' 
                                    },
                                    color:function(params) {
                                        // build a color map as your need.
                                        var colorList = [
                                            '#C1232B','#B5C334'
                                        ];
                                        // eslint-disable-next-line no-console
                                        // console.log("this is dataindex : "+params.data)
                                        //按条件修改柱状图颜色
                                        if(params.data['color'] != undefined){
                                            
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
                var index = {value:Number(i.员工效率.toFixed(1)),color:i.员工效率>i.目标?'#3CB371':'#C0504D'};
                this.barYdata.push(index);
                this.lineYdata.push(i.目标);
            }
            // console.log("sss"+this.lineYdata);   
            //将从后端读取到的数据赋值到表格上
            this.option.xAxis.data=this.barXdata;
            this.option.series[0].data = this.barYdata;
            this.option.series[1].data = this.lineYdata;
            // // eslint-disable-next-line no-console
            // console.log("this is the param: ");
            this.charts.setOption(this.option);
            // this.cdate = []
        },
         //后端数据请求。
        getInfor(){
            fetch('api/Chartdata/EFFchartQuery' ,{
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
            .then(data => {
                this.cdate = data;
                 // eslint-disable-next-line no-console
                // console.log("effdata from back end : "+this.cdate.length);
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
            // console.log("effthis is start time : "+this.startTime)
            //  // eslint-disable-next-line no-console
            // console.log("effthis is end time : "+this.endTime)
            //  // eslint-disable-next-line no-console
            // console.log("effthis is date unit : "+this.dateUnit)
            //  // eslint-disable-next-line no-console
            // console.log("effthis is product line : "+this.prodLine)
            });
        var jack = document.getElementById("EFFchart");
        this.charts = echarts.init(jack);
        // eslint-disable-next-line no-console
        // console.log("effthis is the param: ");
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