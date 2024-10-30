<template>
  <div class="example">
    <apexcharts
      ref="chart"
      width="500"
      height="350"
      type="bar"
      :options="chartOptions"
      :series="series"
    ></apexcharts>
  </div>
</template>

<script>
import VueApexCharts from "vue-apexcharts";

export default {
  name: "BarChart",
  components: {
    apexcharts: VueApexCharts,
  },
  data: function () {
    return {
      chartOptions: {
        chart: {
          id: "basic-bar",
        },
        xaxis: {
          categories: [],
        },
      },
      series: [
        {
          name: "Quantity of Users",
          data: [],
        },
      ],
    };
  },
  methods: {
    loadUsersGrouped() {
      this.$http
        .get("users/get-users-grouped")
        .then((res) => {
          const categories = res.data.map((u) => u.role);
          const data = res.data.map((u) => u.quantity);

          // Atualiza os dados do gráfico de forma reativa
          this.chartOptions.xaxis.categories = categories;
          this.series[0].data = data;

          //  atualização do gráfico
          if (this.$refs.chart && this.$refs.chart.updateOptions) {
            this.$refs.chart.updateOptions({
              xaxis: {
                categories: categories,
              },
            });
          } else {
            console.error("Método updateOptions não está disponível.");
          }
        })
        .catch((error) => {
          console.error("Erro ao carregar dados:", error);
        });
    },
  },
  created() {
    this.loadUsersGrouped();
  },
};
</script>

<style scoped>
</style>