<template>
    <div>
        <h1>Charts</h1>
        <hr>

        <BasicBarChart />
        <br>
        <BarRotatedLabels />
        <br>
        <PieChart />
        <br>
        <LineChart />
    </div>
</template>

<script>
import BasicBarChart from '@/components/charts/BasicBarChart.vue';
import BarRotatedLabels from '@/components/charts/BarRotatedLabels.vue';
import PieChart from '@/components/charts/PieChart.vue';
import LineChart from '@/components/charts/LineChart.vue';

    export default {
        name: 'Charts',
        components: {
            BasicBarChart,
            BarRotatedLabels,
            PieChart,
            LineChart
        }
    }
</script>