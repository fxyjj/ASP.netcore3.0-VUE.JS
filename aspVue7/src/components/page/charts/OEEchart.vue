<template>
 <div id="OEEchart" :style="{width:'600px',height:'320px'}"></div> 

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
    data() {
        return{
            input2: 'i2',
            jack : new HTMLDivElement(),
            cdate : [],
            stime:{
                dateunit:'',
                prodline:'',
                starttime: new Date(),
                endtime:new Date()
            },
            charts : echarts.init(this.chart),
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
                    text: 'OEE'
                },
                tooltip: {},
                legend: {
                    selectMode:true,
                    data:['OEE','目标']
                },
                xAxis: {
                    data: ["1","2","3","4","5","6"]
                },
                yAxis: {},
                series: [{
                            name: 'OEE',
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
                
                var index = {value:Number(i.oee.toFixed(1)),color:i.oee>i.目标?'#B5C334':'#C1232B'};
                this.barYdata.push(index);
                this.lineYdata.push(i.目标);
            }
            // console.log("sss"+this.lineYdata);   
            //将从后端读取到的数据赋值到表格上
            this.option.xAxis.data=this.barXdata;
            this.option.series[0].data = this.barYdata;
            this.option.series[1].data = this.lineYdata;

            this.jack = document.getElementById("OEEchart");
            this.charts = echarts.init(this.jack);
            // eslint-disable-next-line no-console
            console.log("this is the param: ");
            this.charts.setOption(this.option);
        },
         //后端数据请求。
        getInfor(){
            fetch('api/Chartdata/OEEchartQuery' ,{
                method:'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify({
                    start: this.startTime,
                    end: this.endTime,
                    dateUnit: this.dataUnit,
                    prodLine: this.prodLine
                })
            })
            .then(response => response.json())
            // .then(response => response.text())
            .then(data => {
                this.cdate = data;
                // eslint-disable-next-line no-console
                console.log("data from back end : "+this.cdate[0].oee);
                // eslint-disable-next-line no-console
                console.log("data from back end : "+this.cdate[0].目标);
                // eslint-disable-next-line no-console
                console.log("data from back end : "+this.cdate.length);
                // alert('jack'+data);
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
            console.log("this is start time : "+this.startTime)
             // eslint-disable-next-line no-console
            console.log("this is end time : "+this.endTime)
             // eslint-disable-next-line no-console
            console.log("this is date unit : "+this.dateUnit)
             // eslint-disable-next-line no-console
            console.log("this is product line : "+this.prodLine)
            });
    },
    watch: {
        cdate(val){
            // eslint-disable-next-line no-console
            console.log('jack'+val);
        }
    }
}
</script>