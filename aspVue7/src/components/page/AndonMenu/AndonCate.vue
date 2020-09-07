<template>
    <div class="container">
        <div class="plugins-tips">option fields</div>
        <v-line-chart></v-line-chart>
        <v-pie-chart></v-pie-chart>
        
    </div>

</template>

<script>
import LineChart from './CategoryChart/Linechart.vue';
import PieChart from './CategoryChart/Piechart.vue';
import echarts from 'echarts';
export default {
    name:"AndonCategory",
    components:{
        'vLineChart':LineChart,
        'vPieChart':PieChart
    },
    data(){
        return {
        }
    },
    mounted(){
    }
}
</script>