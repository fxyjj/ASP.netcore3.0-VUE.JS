<template>
	<div>
		<div id="AndonPie1" :style="{width:'25%',height:'320px',display:'inline-block'}"></div>
		<div id="AndonPie2" :style="{width:'25%',height:'320px',display:'inline-block'}"></div>
		<div id="AndonPie3" :style="{width:'25%',height:'320px',display:'inline-block'}"></div>
		<div id="AndonPie4" :style="{width:'25%',height:'320px',display:'inline-block'}"></div>
		<div id="slidepart">
			<el-divider content-position="left">移动滑块来查看每 <strong :style="{'font-size':'20px'}">{{dateunit}}</strong> 的饼图数据</el-divider>
			<div class="block">
				<el-slider :style="{'margin-left':'20%','margin-right':'20%'}" v-model="value1" :step="1"  :marks="marks" :min="sMin" :max="sMax" show-stops @change="changePie" aria-label="true"></el-slider>
			</div>
		</div>
	</div>
</template>

<script>
import echarts from 'echarts';
import bus from '../../../common/bus';
export default {
	name:"Piechart",
 data(){
		return {
			Pie1:null,
			Pie2:null,
			Pie3:null,
			Pie4:null,
			dateunit:'',
			pie1:[],
			pie2:[],
			pie3:[],
			pie4:[],
			value1:0,
			sMin:0,
			sMax:10,
			marks:{},
			optionPie1:{
				 title: {
							text: '安灯工时'
						},

				tooltip: {},
				series: [{
							type: 'pie',
							radius:'40%',
							data:[{name:'设备',value:1026},{name:'质量',value:179},{name:'仓库',value:56},{name:'工艺',value:13}],
							animation:true,
							label:{
								position:'outer',
								alignTo:'none',
								bleedMargin:5
							}
						}]
			},
			optionPie2:{
				title:{
					text:'条数分布',
				},
				tooltip: {},
				series: [{
							type: 'pie',
							radius:'40%',
							data:[{name:'设备',value:3},{name:'质量',value:7},{name:'仓库',value:12},{name:'工艺',value:6}],
							animation:true,
							label:{
								position:'outer',
								alignTo:'none',
								bleedMargin:5
							}
						}]

			},
			optionPie3:{
				title:{
					text:'响应时间',
				},
				tooltip: {},
				series: [{
							type: 'pie',
							radius:'40%',
							data: [{name:'设备',value:5},{name:'质量',value:20},{name:'仓库',value:36},{name:'工艺',value:10}],
							animation:true,
							label:{
								position:'outer',
								alignTo:'none',
								bleedMargin:5
							}
						}]

			},
			optionPie4:{
				title:{
					text:'修复时间',
				},
				tooltip: {},
				series: [{
							type: 'pie',
							radius:'40%',
							data: [{name:'设备',value:9},{name:'质量',value:12},{name:'仓库',value:33},{name:'工艺',value:6}],
							animation:true,
							label:{
								position:'outer',
								alignTo:'none',
								bleedMargin:5
							}
						}]
			}

		}
	},
	methods:{
		formatTooltip(val) {
        	return val / 100;
	  	},
	  	changePie(){
			console.log(this.value1+'this is slider value');
			this.optionPie1.series[0].data = this.pie1[this.value1];
			this.optionPie2.series[0].data = this.pie2[this.value1];
			this.optionPie3.series[0].data = this.pie3[this.value1];
			this.optionPie4.series[0].data = this.pie4[this.value1];

			this.Pie1.setOption(this.optionPie1)
			this.Pie2.setOption(this.optionPie2)
			this.Pie3.setOption(this.optionPie3)
			this.Pie4.setOption(this.optionPie4)

	  	}
	},
	mounted(){
		var c1 = document.getElementById("AndonPie1");
		this.Pie1 = echarts.init(c1);
		this.Pie1.setOption(this.optionPie1);

		var c2 = document.getElementById("AndonPie2");
		this.Pie2 = echarts.init(c2);
		this.Pie2.setOption(this.optionPie2);

		var c3 = document.getElementById("AndonPie3");
		this.Pie3 = echarts.init(c3);
		this.Pie3.setOption(this.optionPie3);

		var c4 = document.getElementById("AndonPie4");
		this.Pie4 = echarts.init(c4);
		this.Pie4.setOption(this.optionPie4);

		bus.$on('catePrm',s=>{
					this.dateunit = s.dateunit;
					// console.log(this.dateunit);
				})
		bus.$on('backData',msg=>{
			// console.log("从linechart过来的参数："+msg);
			this.pie1 = [];
			this.pie2 = [];
			this.pie3 = [];
			this.pie4 = [];
			this.marks = {};
			var monthName = ['Jan','Feb','Mar','Apr','May','Jun','Jul','Aug','Sept','Oct','Nov','Dec']
			for(var item of msg){
				var v1 = [{name:'工艺',value:item.gProcessT.toFixed(2)},{name:'设备',value:item.sProcessT.toFixed(2)},{name:'仓库',value:item.cProcessT.toFixed(2)},{name:'质量',value:item.zProcessT.toFixed(2)}]
				var v2 = [{name:'工艺',value:item.gAndonN},{name:'设备',value:item.sAndonN},{name:'仓库',value:item.cAndonN},{name:'质量',value:item.zAndonN}]
				var v3 = [{name:'工艺',value:item.gRespT.toFixed(2)},{name:'设备',value:item.sRespT.toFixed(2)},{name:'仓库',value:item.cRespT.toFixed(2)},{name:'质量',value:item.zRespT.toFixed(2)}]
				var v4 = [{name:'工艺',value:item.gRepairT.toFixed(2)},{name:'设备',value:item.sRepairT.toFixed(2)},{name:'仓库',value:item.cRepairT.toFixed(2)},{name:'质量',value:item.zRepairT.toFixed(2)}]
				if(this.dateunit == '月'){
					this.$set(this.marks,this.pie1.length,monthName[Number(item.日期单位)-1]);
				}else{
					this.$set(this.marks,this.pie1.length,item.日期单位);
				}
				this.pie1.push(v1);
				this.pie2.push(v2);
				this.pie3.push(v3);
				this.pie4.push(v4);
			}
			this.sMax = this.pie1.length-1
			console.log(this.pie1[0])
			this.optionPie1.series[0].data = this.pie1[0];
			this.optionPie2.series[0].data = this.pie2[0];
			this.optionPie3.series[0].data = this.pie3[0];
			this.optionPie4.series[0].data = this.pie4[0];

			this.Pie1.setOption(this.optionPie1)
			this.Pie2.setOption(this.optionPie2)
			this.Pie3.setOption(this.optionPie3)
			this.Pie4.setOption(this.optionPie4)

		})
		
	}
}
</script>
