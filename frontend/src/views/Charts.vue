<template>
    <div>
        <h1>Charts</h1>
        <hr>

        <BasicBarChart />
        <br>
        <BarRotatedLabels />
        <br>
        <DonutChart />
        <br>
        <LineChart />
    </div>
</template>

<script>
import BasicBarChart from '@/components/charts/BasicBarChart.vue';
import BarRotatedLabels from '@/components/charts/BarRotatedLabels.vue';
import DonutChart from '@/components/charts/DonutChart.vue';
import LineChart from '@/components/charts/LineChart.vue';

    export default {
        name: 'Charts',
        components: {
            BasicBarChart,
            BarRotatedLabels,
            DonutChart,
            LineChart
        }
    }
</script>

<style lang="scss" scoped>

</style>