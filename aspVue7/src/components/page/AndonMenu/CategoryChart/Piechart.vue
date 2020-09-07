<template>
	<div>
		<div id="AndonPie1" :style="{width:'320px',height:'320px',display:'inline-block'}"></div>
		<div id="AndonPie2" :style="{width:'320px',height:'320px',display:'inline-block'}"></div>
		<div id="AndonPie3" :style="{width:'320px',height:'320px',display:'inline-block'}"></div>
		<div id="AndonPie4" :style="{width:'320px',height:'320px',display:'inline-block'}"></div>
	</div>
</template>

<script>
import echarts from 'echarts';
export default {
	name:"Piechart",
 data(){
		return {
			Pie1:null,
			Pie2:null,
			Pie3:null,
			Pie4:null,
			optionPie1:{
				 title: {
							text: '安灯工时'
						},

				tooltip: {},
				series: [{
							type: 'pie',
							radius:'40%',
							data:[{name:'设备1',value:1026},{name:'质量',value:179},{name:'仓库',value:56},{name:'工艺',value:13}],
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
							data:[{name:'设备2',value:3},{name:'质量',value:7},{name:'仓库',value:12},{name:'工艺',value:6}],
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
							data: [{name:'设备3',value:5},{name:'质量',value:20},{name:'仓库',value:36},{name:'工艺',value:10}],
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
							data: [{name:'设备4',value:9},{name:'质量',value:12},{name:'仓库',value:33},{name:'工艺',value:6}],
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
	mounted(){
		var c1 = document.getElementById("AndonPie1");
		this.Hourchart = echarts.init(c1);
		this.Hourchart.setOption(this.optionPie1);

		var c2 = document.getElementById("AndonPie2");
		this.Hourchart = echarts.init(c2);
		this.Hourchart.setOption(this.optionPie2);

		var c3 = document.getElementById("AndonPie3");
		this.Hourchart = echarts.init(c3);
		this.Hourchart.setOption(this.optionPie3);

		var c4 = document.getElementById("AndonPie4");
		this.Hourchart = echarts.init(c4);
		this.Hourchart.setOption(this.optionPie4);
	}
}
</script>