<template>
  <div class="mt-1">
    <b-button class="fa fa-bar-chart primary white--text" style="font-size: 18px" v-b-modal.chart-modal> Chart</b-button>
    <b-modal id="chart-modal" title="bar chart" hide-footer size="lg">
      <div class="text-center">
        <BasicBarChart />
      </div>
    </b-modal>
  </div>
</template>

<script>
import BasicBarChart from "./BasicBarChart.vue";

export default {
  name: "ModalBarChart",
  components: { BasicBarChart },
};
</script>

<style lang="scss" scoped>
</style>