export default {
    methods: {
      calculateCenterTextPosition(pdf, text) {
        const pageWidth = pdf.internal.pageSize.getWidth();
        const textWidth = pdf.getStringUnitWidth(text) * pdf.internal.getFontSize() / pdf.internal.scaleFactor;
        const x = (pageWidth - textWidth) / 2;
        return x;
      },

      dowloadCompleted() {
        setTimeout(() => {
          this.$toasted.global.defaultInfo({ msg: "download completed" });
        }, 1000);
      },

      getDateTime(date) {
        const datePart = date.toLocaleDateString("en-US");
        const timePart = date.toLocaleTimeString("en-US", { hour12: false });
        return `${datePart} ${timePart}`;
      },

      formatDate(dateString) {
        const date = new Date(dateString);
        return date.toLocaleDateString("en-US");
      },

    }
  };