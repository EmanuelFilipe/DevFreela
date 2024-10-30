<template>
  <div class="example">
    <apexcharts
      ref="pie"
      width="350"
      type="pie"
      :options="chartOptions"
      :series="series"
    ></apexcharts>
  </div>
</template>
  
  <script>
import VueApexCharts from "vue-apexcharts";

export default {
  name: "PieChart",
  components: {
    apexcharts: VueApexCharts,
  },
  data: function () {
    return {
      chartOptions: {
        labels: [],
      },
      series: [],
    };
  },
  methods: {
    loadProjectsGrouped() {
      this.$http.get("projects/projects-grouped").then((res) => {
        console.log(res.data)
        const labels = res.data.map((u) => u.status);
        const series = res.data.map((u) => u.quantity);

        this.chartOptions.labels = labels;
        this.series = series;

        //  atualização do gráfico
        if (this.$refs.pie && this.$refs.pie.updateOptions) {
          this.$refs.pie.updateOptions({
            labels: labels,
          });
        } else {
          console.error("Método updateOptions não está disponível.");
        }
      });
    },
  },
  created() {
    this.loadProjectsGrouped();
  },
};
</script>