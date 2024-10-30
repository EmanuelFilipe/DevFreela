<template>
    <div class="mt-1">
      <b-button class="fa fa-bar-chart primary white--text" style="font-size: 18px" v-b-modal.chart-modal> Chart</b-button>
      <b-modal id="chart-modal" title="bar chart" hide-footer size="md">
        <div class="text-center">
          <PieChart />
        </div>
      </b-modal>
    </div>
  </template>

<script>
import PieChart from './PieChart.vue';
    export default {
        name: 'ModalPieChart',
        components: { PieChart }
    }
</script>