<template>
 <div id="EFFchart" :style="{width:'600px',height:'320px'}"></div> 

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
    name:"EFFchart",
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
                    text: 'EFF'
                },
                tooltip: {},
                legend: {
                    selectMode:true,
                    data:['EFF','目标']
                },
                xAxis: {
                    data: ["1","2","3","4","5","6"]
                },
                yAxis: {},
                series: [{
                            name: 'EFF',
                            type: 'bar',
                            // data: [5,20,36,10,10,20],
                            data:[{value:5,color:'#000'},
                                {value:20,color:'#C1232B'},
                                {value:36,color:'#C1232B'},
                                {value:10,color:'#C1232B'},
                                {value:10,color:'#C1232B'},
                                {value:20,color:'#C1232B'}],
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
                                        console.log("this is dataindex : "+params.data)
                                        
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
                            data: [5,20,36,10,10,20],
                            itemStyle:{
                                color: ["#000"]
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
                    console.log(newval+" 到 "+oldval)
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
                
                var index = {value:Number(i.员工效率.toFixed(1)),color:i.员工效率>i.目标?'#B5C334':'#C1232B'};
                this.barYdata.push(index);
                this.lineYdata.push(i.目标);
            }
            // console.log("sss"+this.lineYdata);   
            //将从后端读取到的数据赋值到表格上
            this.option.xAxis.data=this.barXdata;
            this.option.series[0].data = this.barYdata;
            this.option.series[1].data = this.lineYdata;

            // this.jack = document.getElementById("EFFchart");
            // this.charts = echarts.init(this.jack);
            // eslint-disable-next-line no-console
            console.log("this is the param: ");
            this.charts.setOption(this.option);
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
            // .then(response => response.text())
            .then(data => {
                this.cdate = data;
                // eslint-disable-next-line no-console
                // console.log("data from back end : "+this.cdate[0].oee);
                // eslint-disable-next-line no-console
                // console.log("data from back end : "+this.cdate[0].目标);
                // eslint-disable-next-line no-console
                console.log("data from back end : "+this.cdate.length);
                // alert('jack'+this.cdate);
            }).catch(data => {
                alert(data);
            })
        }
    },
    
     mounted(){
        bus.$on("Query", (stime) =>{
            this.startTime = stime.starttime;
            this.endTime = stime.endtime;
            this.dateUnit = stime.dateunit;
            this.prodLine = stime.prodline;
            this.getInfor()
            // eslint-disable-next-line no-console
            console.log("effthis is start time : "+this.startTime)
             // eslint-disable-next-line no-console
            console.log("effthis is end time : "+this.endTime)
             // eslint-disable-next-line no-console
            console.log("effthis is date unit : "+this.dateUnit)
             // eslint-disable-next-line no-console
            console.log("effthis is product line : "+this.prodLine)
            });
            var jack = document.getElementById("EFFchart");
            this.charts = echarts.init(jack);
            // eslint-disable-next-line no-console
            console.log("effthis is the param: ");
            this.charts.setOption(this.option);
    }

    
}
</script>