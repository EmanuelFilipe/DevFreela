<template>
  <div class="mt-1">
    <b-button variant="info" style="color: white" v-b-modal.export-modal
      >Download Reports</b-button
    >
    <b-modal id="export-modal" title="Choose the format" hide-footer>
      <div class="text-center">
        <button
          class="btn btn-lg fa fa-file-pdf-o fa-2x btn-pdf"
          @click.prevent="callPDFGenerator"
          title="Download PDF"
        >
          <span class="buttons-report" style="color: black"
            ><br /><strong>PDF</strong></span
          >
        </button>
        <button
          class="btn btn-lg fa fa-file-excel-o fa-2x btn-excel"
          @click.prevent="callExcelGenerator"
          title="Download Excel"
        >
          <span class="buttons-report" style="color: black"
            ><br /><strong>Excel</strong></span
          >
        </button>
      </div>
    </b-modal>
  </div>
</template>

<script>
export default {
  name: "ReportButton",
  methods: {
    callPDFGenerator() {
      this.$emit("callPDFGenerator");
    },
    callExcelGenerator() {
      this.$emit("callExcelGenerator");
    },
  },
};
</script>

<style lang="scss" scoped>
</style>