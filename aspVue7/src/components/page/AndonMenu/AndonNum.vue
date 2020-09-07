<template>
    <div class="container">
        <div class="plugins-tips">here is the option fields.</div>
        <vegr></vegr>
        <vcm></vcm>
        <vic></vic>
        <vptcth></vptcth>
        
    </div>
</template>

<script>
import echarts from 'echarts';
import EGRchart from './NumCharts/EGRchart.vue';
import CMchart from './NumCharts/CMchart.vue';
import ICchart from './NumCharts/ICchart.vue';
import PTCTHchart from './NumCharts/PTCTHchart.vue';

export default {
    name:'andonNumber',
    components:{
        'vegr':EGRchart,
        'vcm':CMchart,
        'vic':ICchart,
        'vptcth':PTCTHchart
    },
    data(){
        return {
        }
    },
    mounted(){
        
    }
}
</script>