<template>
    <div class="container">
        <div class="plugins-tips">here is the option fields.</div>
        <vequip></vequip>
    </div>
</template>

<script>
import Equipchart from './TopIssuechart/Equipchart.vue'
export default {
    name:'TopIssue',
    components:{
        'vequip':Equipchart
    }
}
</script>