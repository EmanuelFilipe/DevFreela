<template>
  <b-container fluid>
    <b-form>
      <b-row class="mb-5">
        <b-col>
          <h1 class="display-3"><strong>Projects</strong></h1>
        </b-col>
      </b-row>
      <b-row >
        <b-col v-if="projects.length && user.isAdmin">
          <ReportButton 
            @callPDFGenerator="generatePDF"
            @callExcelGenerator="generateExcel"
          />
        </b-col>
        <b-col v-if="projects.length && user.isAdmin">
          <ModalPieChart />

          <!-- <div class="mt-1">
            <b-button variant="info" style="color: white" v-b-modal.export-modal
              >Download Report</b-button
            >
            <b-modal id="export-modal" title="Choose the format" hide-footer>
              <div class="text-center">
                <button
                  class="btn btn-lg fa fa-file-pdf-o fa-2x btn-pdf"
                  @click.prevent="generatePDF"
                  title="Download PDF"
                >
                  <span class="buttons-report" style="color: black"
                    ><br /><strong>PDF</strong></span
                  >
                </button>
                <button
                  class="btn btn-lg fa fa-file-excel-o fa-2x btn-excel"
                  @click.prevent="generateExcel"
                  title="Download Excel"
                >
                  <span class="buttons-report" style="color: black"
                    ><br /><strong>Excel</strong></span
                  >
                </button>
              </div>
            </b-modal>
          </div> -->
        </b-col>
      </b-row>
      <b-row>
        <b-col sm="6" md="4" class="mt-1">
          <b-form-input
            id="input-filter"
            v-model="query"
            placeholder="Filter by Title or Description"
          >
          </b-form-input>
        </b-col>
        <b-col>
          <ButtonCreate url="/project" />
        </b-col>
      </b-row>

      <hr />
    </b-form>

    <b-table
      class="table"
      hover
      striped
      :items="projects"
      :fields="fields"
      head-variant="dark"
      show-empty
    >
      <template #empty>
        <div class="text-center">No data available</div>
      </template>

      <template #cell(createdAt)="row">
        {{ row.item.createdAt | formatDate }}
      </template>

      <template #cell(actions)="data">
        <b-button
          id="btn-detail-project"
          @click="data.toggleDetails"
          class="mr-2 btn btn-info"
        >
          <i class="fa fa-solid fa-eye"></i>
        </b-button>
        <button
          id="btn-edit-project"
          @click="editProject(data.item.id)"
          class="mr-2 btn btn-warning"
          title="Edit"
        >
          <i class="fa fa-pencil"></i>
        </button>
        <button
          id="btn-start-project"
          class="btn btn-success"
          :disabled="data.item.status !== 'Created'"
          @click="openStartModal(data.item.id)"
          title="Start Project"
        >
          <i class="fa fa-play"></i>
        </button>

        <button
          id="btn-cancel-project"
          class="ml-2 btn btn-danger"
          :disabled="data.item.status !== 'InProgress'"
          @click="openCancelModal(data.item.id)"
          title="Cancel Project"
        >
          <i class="fa fa-ban"></i>
        </button>
      </template>

      <template #row-details="data">
        <b-card class="details-card" id="modal-detail-project">
          <div class="details-container">
            <div class="details-item">
              <span
                class="details-label text-right"
                id="modal-detail-project-id"
                >Id:</span
              >
              <span class="details-value">{{ data.item.id }}</span>
            </div>
            <div class="details-item">
              <span class="details-label">Title:</span>
              <span class="details-value">{{ data.item.title }}</span>
            </div>
            <div class="details-item">
              <span class="details-label">Client Name:</span>
              <span class="details-value">{{ data.item.fullNameClient }}</span>
            </div>
            <div class="details-item">
              <span class="details-label">Freelancer Name:</span>
              <span class="details-value">{{
                data.item.fullNameFreelancer
              }}</span>
            </div>
            <div class="details-item">
              <span class="details-label">Status:</span>
              <span class="details-value">{{ data.item.status }}</span>
            </div>
            <div class="details-item">
              <span class="details-label">Created At:</span>
              <span class="details-value">{{
                data.item.createdAt | formatDateTime
              }}</span>
            </div>
          </div>
        </b-card>
      </template>
    </b-table>
    <!-- <b-modal
            id="startModal"
            title="Confirm Starting"
            @ok="confirmStart"
            ok-title="OK"
            cancel-title="Cancelar"
            ok-variant="primary"
            cancel-variant="danger"
            >
            <p>Are you sure you want <strong>"Start"</strong> this project?</p>
        </b-modal> -->

    <b-modal
      id="startModal"
      title="Confirm Starting"
      @ok="confirmStart"
      ok-title="OK"
      cancel-title="Cancelar"
      ok-class="custom-ok-btn"
      cancel-class="custom-cancel-btn"
      ok-variant="primary"
      cancel-variant="danger"
    >
      <p>Are you sure you want <strong>"Start"</strong> this project?</p>
      <template v-slot:modal-footer="{ ok, cancel }">
        <button
          type="button"
          class="btn btn-danger"
          style="color: white"
          @click="cancel()"
        >
          Cancelar
        </button>
        <button
          type="button"
          class="btn btn-primary"
          style="color: white"
          @click="ok()"
        >
          OK
        </button>
      </template>
    </b-modal>

    <!-- <b-modal
            id="deleteModal"
            title="Confirm Disabling"
            @ok="confirmDelete"
            ok-title="OK"
            cancel-title="Cancelar"
            ok-variant="primary"
            cancel-variant="danger"
            >
            <p>Are you sure you want <strong>"Finished"</strong> this project?</p>
        </b-modal> -->

    <b-modal
      id="deleteModal"
      title="Confirm Cancel Project"
      @ok="confirmDelete"
      ok-title="OK"
      cancel-title="Cancelar"
      ok-class="custom-ok-btn"
      cancel-class="custom-cancel-btn"
      ok-variant="primary"
      cancel-variant="danger"
    >
      <p>Are you sure you want <strong>"Cancel"</strong> this project?</p>
      <template v-slot:modal-footer="{ ok, cancel }">
        <button
          type="button"
          class="btn btn-danger"
          style="color: white"
          @click="cancel()"
        >
          Cancelar
        </button>
        <button
          type="button"
          class="btn btn-primary"
          style="color: white"
          @click="ok()"
        >
          OK
        </button>
      </template>
    </b-modal>

    <b-pagination
      class="pagination"
      v-model="currentPage"
      :total-rows="totalRows"
      :per-page="perPage"
      aria-controls="table-projects"
    >
    </b-pagination>
  </b-container>
</template>

<script>
import ButtonCreate from "@/components/template/ButtonCreate.vue";
import { showError } from "@/global";
import jsPDF from "jspdf";
import ExcelJS from "exceljs";
import { saveAs } from "file-saver";
import utilsMixin from "@/mixin/utilsMixin";
import ReportButton from "@/components/template/ReportButton.vue";
import ModalPieChart from "@/components/charts/ModalPieChart.vue";
import { mapState } from 'vuex';

export default {
  name: "Projects",
  components: {
    ButtonCreate,
    ReportButton,
    ModalPieChart
  },
  mixins: [ utilsMixin ],
  data() {
    return {
      projects: [], // Todos os projetos retornados da API
      paginatedProjects: [], // Projetos paginados no frontend
      currentPage: 1, // Página atual no frontend
      perPage: null, // Exibir 2 registros por página no frontend -- TROCAR PELO PAGESIZE da API
      totalRows: 0, // Total de registros na API
      query: "", // Filtro opcional para a busca
      selectedId: null,
      fields: [
        {
          key: "title",
          label: "Title",
          sortable: true,
          thStyle: { width: "28%" },
          tdStyle: { width: "28%" },
        },
        {
          key: "fullNameClient",
          label: "Name Client",
          sortable: true,
          thStyle: { width: "20%" },
          tdStyle: { width: "20%" },
        },
        {
          key: "fullNameFreelancer",
          label: "Name Freelancer",
          sortable: true,
          thStyle: { width: "20%" },
          tdStyle: { width: "20%" },
        },
        {
          key: "status",
          label: "Status",
          sortable: true,
          thStyle: { width: "10%" },
          tdStyle: { width: "10%" },
        },
        {
          key: "actions",
          label: "Actions",
          thStyle: { width: "12%" },
          tdStyle: { width: "12%" },
        },
      ],
    };
  },
  methods: {
    loadProjects() {
      const params = {
        query: this.query,
        page: this.currentPage,
      };
      this.$http
        .get("/projects", { params })
        .then((res) => {
          this.projects = null;
          const responseData = res.data;
          this.projects = this.filterProjectsByTypeUser(responseData.data);
          this.totalRows = responseData.itemsCount;
          this.perPage = responseData.pageSize;
        })
        .catch((err) => {
          console.error("Error fetching projects:", err);
        });
    },
    filterProjectsByTypeUser(source) {
      const isAdmin = this.$store.state.user.isAdmin;
      let listaRetorno = source;
      if (!isAdmin) {
        if (this.$store.state.user.role === "client")
          listaRetorno = source.filter(
            (p) => p.idClient === this.$store.state.user.id
          );
        else if (this.$store.state.user.role === "freelancer")
          listaRetorno = source.filter(
            (p) => p.idFreelancer === this.$store.state.user.id
          );
      }
      return listaRetorno;
    },
    editProject(id) {
      this.$router.push({
        name: "project-edit",
        params: { id },
      });
    },
    openCancelModal(id) {
      this.selectedId = id;
      this.$bvModal.show("deleteModal");
    },
    openStartModal(id) {
      this.selectedId = id;
      this.$bvModal.show("startModal");
    },
    confirmDelete() {
      if (this.selectedId && this.selectedId > 0)
        this.$http
          .delete(`/projects/${this.selectedId}`)
          .then(() => {
            this.$toasted.global.defaultSuccess();
            this.loadProjects();
          })
          .catch(showError);
    },
    confirmStart() {
      if (this.selectedId && this.selectedId > 0)
        this.$http
          .put(`/projects/${this.selectedId}/start`)
          .then(() => {
            this.$toasted.global.defaultSuccess();
            this.loadProjects();
          })
          .catch(showError);
    },
    totalPages() {
      return this.totalRows > 0 ? Math.ceil(this.totalRows / this.perPage) : 1;
    },
    generatePDF() {
      try {
        const pdf = new jsPDF({ orientation: "landscape" });
        const columns = [
          { title: "Id", dataKey: "id" },
          { title: "Title", dataKey: "title" },
          { title: "Created At", dataKey: "createdAt" },
          { title: "Client Name", dataKey: "fullNameClient" },
          { title: "Freelancer Name", dataKey: "fullNameFreelancer" },
          { title: "Status", dataKey: "status" },
        ];

        const totalPages = this.totalPages()
        const date = new Date()
        const title = "Registered Projects Report"

        // Adiciona o título e cabeçalho na primeira página
        pdf.setFontSize(18);
        pdf.text(title, this.calculateCenterTextPosition(pdf, title), 22);
        pdf.setFontSize(12);
        pdf.text(`Generate by: ${this.$store.state.user.name}`, 14, 30);
        pdf.setFontSize(12);
        pdf.text(`Date: ${this.getDateTime(date)}`, 14, 38);

        for (let i = 1; i <= totalPages; i++) {
          this.currentPage = i;
          const start = (i - 1) * this.perPage;
          const end = start + this.perPage;
          const pageData =
            this.totalRows > 0 ? this.projects.slice(start, end) : [];

          if (i > 1) {
            pdf.addPage();
            pdf.setFontSize(18);
            pdf.text(title, this.calculateCenterTextPosition(pdf, title), 22);
            pdf.setFontSize(12);
            pdf.text(`Generate by: ${this.$store.state.user.name}`, 14, 30);
            pdf.setFontSize(12);
            pdf.text(`Date: ${this.getDateTime(date)}`, 14, 38);
          }

          pdf.autoTable({
            head: [columns.map((col) => col.title)],
            body: pageData.map((row) => [
              row.id,
              row.title,
              this.formatDate(row.createdAt),
              row.fullNameClient,
              row.fullNameFreelancer,
              row.status,
            ]),
            startY: 50,
            theme: "grid",
            headStyles: { fillColor: [0, 0, 0] },
            styles: { fontSize: 10 },
          });

          // Adiciona o número da página no rodapé
          pdf.text(
            `Page ${i} of ${totalPages}`,
            pdf.internal.pageSize.width / 2,
            pdf.internal.pageSize.height - 10,
            { align: "center" }
          );
        }

        pdf.save("projects.pdf");
        this.dowloadCompleted();
        this.$bvModal.hide("export-modal");
      } catch (error) {
        this.$toasted.global.defaultError({ msg: error });
      }
    },
    async generateExcel() {
      try {
        const date = new Date()
        const workbook = new ExcelJS.Workbook();
        const worksheet = workbook.addWorksheet("Report");

        // Adiciona o título
        worksheet.mergeCells("A1", "F1");
        const titleCell = worksheet.getCell("A1");
        titleCell.value = "Registered Projects Report";
        titleCell.font = { size: 20, bold: true };
        titleCell.alignment = { vertical: "middle", horizontal: "center" };

        // Adiciona informações adicionais
        worksheet.addRow([`Created by: ${this.$store.state.user.name}`]);
        worksheet.addRow([`Date: ${this.getDateTime(date)}`]);
        worksheet.addRow([`Amount of items: ${this.projects.length}`]);
        worksheet.addRow([]); // Linha em branco

        // Adiciona cabeçalho do grid
        const headerRow = worksheet.addRow([
          "Id",
          "Title",
          "Created At",
          "Client Name",
          "Freelancer Name",
          "Status",
        ]);
        headerRow.eachCell((cell) => {
          cell.font = { bold: true };
          cell.fill = {
            type: "pattern",
            pattern: "solid",
            fgColor: { argb: "DFF0D8" },
          };
        });

        // Adiciona dados
        this.projects.forEach((item) => {
          worksheet.addRow([
            item.id,
            item.title,
            this.$options.filters.formatDate(item.createdAt),
            item.fullNameClient,
            item.fullNameFreelancer,
            item.status,
          ]);
        });

        // Define a largura das colunas individualmente
        worksheet.getColumn(1).width = 5; // Id
        worksheet.getColumn(2).width = 30; // Title
        worksheet.getColumn(3).width = 13; // Created At
        worksheet.getColumn(4).width = 35; // Client Name
        worksheet.getColumn(5).width = 35; // Freelancer Name
        worksheet.getColumn(6).width = 15; // Status

        // Salva o arquivo
        const buffer = await workbook.xlsx.writeBuffer();
        saveAs(new Blob([buffer]), "projects_report.xlsx");
        this.dowloadCompleted();
        this.$bvModal.hide("export-modal");
      } catch (error) {
        this.$toasted.global.defaultError({ msg: error });
      }
    },
  },
  computed:{
    ...mapState(['user'])
  },
  watch: {
    currentPage() {
      this.loadProjects();
    },
    query() {
      this.currentPage = 1;
      this.loadProjects();
    },
  },
  created() {
    this.loadProjects();
  },
};
</script>