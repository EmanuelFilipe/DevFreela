<template>
    <div class="example">
      <apexcharts width="380" type="donut" :options="options" :series="series"></apexcharts>
    </div>
  </template>
  
  <script>
  import VueApexCharts from 'vue-apexcharts';
  
  export default {
    name: 'DonutChart',
    components: {
      apexcharts: VueApexCharts,
    },
    data: function() {
      return {
        options: {},
        series: [44, 55, 41, 17, 15]
      }
    }
  }
  </script>