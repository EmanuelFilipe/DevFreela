<template>
  <b-container fluid>
    <b-form>
      <b-row class="mb-5">
        <b-col>
          <h1 class="display-3"><strong>Users</strong></h1>
        </b-col>
      </b-row>
      <b-row>
        <!-- <b-col style="padding: 0; margin: 0px;">
          <div >
            <button
              class="btn btn-lg fa fa-file-pdf-o fa-2x"
              @click.prevent="generatePDF"
              title="Download PDF"
            ><span class="buttons-report"><br><strong>PDF</strong></span></button>
            <button 
              class="btn btn-lg fa fa-file-excel-o fa-2x" 
              @click.prevent="generateExcel" 
              title="Download Excel"
            ><span class="buttons-report"><br><strong>Excel</strong></span>
            </button>
          </div>
        </b-col> -->
        <b-col>
          <div class="mt-1">
            <b-button variant="info" style="color: white;" v-b-modal.export-modal>Exportar Relatório</b-button>
            <b-modal id="export-modal" title="Escolha o formato" hide-footer >
              <div class="text-center">
                <button
                class="btn btn-lg fa fa-file-pdf-o fa-2x btn-pdf"
                @click.prevent="generatePDF"
                title="Download PDF"
                ><span class="buttons-report" style="color: black;"><br><strong>PDF</strong></span></button>
                <button 
                class="btn btn-lg fa fa-file-excel-o fa-2x btn-excel" 
                @click.prevent="generateExcel" 
                title="Download Excel"
                ><span class="buttons-report" style="color: black;"><br><strong>Excel</strong></span>
              </button>
            </div>
            </b-modal>
          </div>
        </b-col>
        <b-col>
          <ButtonCreate url="/user" />
        </b-col>
      </b-row>
      <!-- <b-row>
        <div>
          <b-button variant="primary" v-b-modal.export-modal>Exportar Relatório</b-button>
          <b-modal id="export-modal" title="Escolha o formato" hide-footer >
            <div class="text-center">
              <button
              class="btn btn-lg fa fa-file-pdf-o fa-2x btn-pdf"
              @click.prevent="generatePDF"
              title="Download PDF"
              ><span class="buttons-report" style="color: black;"><br><strong>PDF</strong></span></button>
              <button 
              class="btn btn-lg fa fa-file-excel-o fa-2x btn-excel" 
              @click.prevent="generateExcel" 
              title="Download Excel"
              ><span class="buttons-report" style="color: black;"><br><strong>Excel</strong></span>
            </button>
          </div>
          </b-modal>
        </div>
      </b-row> -->
      <hr />
    </b-form>

    <!-- <b-table :items="users" :busy="isBusy" class="mt-3" outlined>
      <template #table-busy>
        <div class="text-center text-danger my-2">
          <b-spinner class="align-middle"></b-spinner>
          <strong>Loading...</strong>
        </div>
      </template>
    </b-table> -->

    <b-table
      id="usersTable"
      class="table"
      hover
      striped
      :items="paginatedItems"
      :fields="fields"
      head-variant="dark"
      show-empty
    >
      <template #empty>
        <div class="text-center">No data available</div>
      </template>

      <template #cell(birthDate)="row">
        {{ row.item.birthDate | formatDate }}
      </template>
      <template #cell(actions)="data">
        <b-button
          id="btn-detail-user"
          @click="data.toggleDetails"
          class="mr-2 btn btn-info"
        >
          <i class="fa fa-solid fa-eye"></i>
        </b-button>
        <button
          id="btn-edit-user"
          @click="editUser(data.item.id)"
          class="mr-2 btn btn-warning"
          title="Edit"
        >
          <i class="fa fa-pencil"></i>
        </button>

        <button
          id="btn-disabled-user"
          class="btn btn-danger"
          :disabled="!data.item.active"
          @click="openDeleteModal(data.item.id)"
          title="Disable"
        >
          <i class="fa fa-trash"></i>
        </button>
      </template>

      <template #row-details="data">
        <!-- <b-card>
    <b-row class="mb-2">
      <b-col cols="2" class="text-right"><strong>ID:</strong></b-col>
      <b-col cols="3">{{ data.item.id }}</b-col>
    </b-row>
    <b-row class="mb-2">
      <b-col cols="2" class="text-right"><strong>Nome Completo:</strong></b-col>
      <b-col cols="3">{{ data.item.fullName }}</b-col>
    </b-row>
    <b-row class="mb-2">
      <b-col cols="2" class="text-right"><strong>Email:</strong></b-col>
      <b-col cols="3">{{ data.item.email }}</b-col>
    </b-row>
    <b-row class="mb-2">
      <b-col cols="2" class="text-right"><strong>Data de Nascimento:</strong></b-col>
      <b-col cols="3">{{ new Date(data.item.birthDate).toLocaleDateString() }}</b-col>
    </b-row>
    <b-row class="mb-2">
      <b-col cols="2" class="text-right"><strong>Ativo:</strong></b-col>
      <b-col cols="3">{{ data.item.active ? 'Sim' : 'Não' }}</b-col>
    </b-row>
  </b-card> -->

        <b-card class="details-card" id="modal-detail-user">
          <div class="details-container">
            <div class="details-item">
              <span class="details-label text-right" id="modal-detail-user-id"
                >Id:</span
              >
              <span class="details-value">{{ data.item.id }}</span>
            </div>
            <div class="details-item">
              <span class="details-label">Full Name:</span>
              <span class="details-value">{{ data.item.fullName }}</span>
            </div>
            <div class="details-item">
              <span class="details-label">Email:</span>
              <span class="details-value">{{ data.item.email }}</span>
            </div>
            <div class="details-item">
              <span class="details-label">Birth Date:</span>
              <span class="details-value">{{
                data.item.birthDate | formatDate
              }}</span>
            </div>
            <div class="details-item">
              <span class="details-label">Role:</span>
              <span class="details-value">{{ data.item.role }}</span>
            </div>
            <div class="details-item">
              <span class="details-label">Active?</span>
              <span class="details-value" id="detail-user-active-value">{{
                data.item.active ? "Yes" : "No"
              }}</span>
            </div>
          </div>
        </b-card>
      </template>
    </b-table>

    <b-modal
      id="deleteModal"
      title="Confirm Disabling"
      @ok="confirmDisable"
      ok-title="OK"
      cancel-title="Cancelar"
      ok-class="custom-ok-btn"
      cancel-class="custom-cancel-btn"
      ok-variant="primary"
      cancel-variant="danger"
    >
      <p>Are you sure you want to disable this item?</p>
    </b-modal>

    <!-- Info modal -->
    <b-modal :id="infoModal.id" :title="infoModal.title" @hide="resetInfoModal">
      <pre>{{ infoModal.content }}</pre>
    </b-modal>

    <b-pagination
      class="pagination"
      v-model="currentPage"
      :total-rows="totalRows"
      :per-page="perPage"
      aria-controls="table-users"
    >
    </b-pagination>
  </b-container>
</template>

<script>
import ButtonCreate from "@/components/template/ButtonCreate.vue";
import { showError, userKey } from "@/global";
import jsPDF from "jspdf";
import "jspdf-autotable";
import ExcelJS from 'exceljs';
import { saveAs } from 'file-saver';

export default {
  name: "Users",
  components: {
    ButtonCreate,
  },
  data() {
    return {
      currentPage: 1,
      perPage: 10, // Quantidade de itens por página
      totalRows: 0,
      isBusy: false,
      fields: [
        { key: "fullName", label: "Full Name", sortable: true },
        {
          key: "birthDate",
          label: "Birth Date",
          sortable: true,
          thStyle: { width: "15%" },
          tdStyle: { width: "15%" },
        },
        {
          key: "active",
          label: "Active",
          sortable: true,
          formatter: (value) => (value ? "Yes" : "No"),
          thStyle: { width: "15%" },
          tdStyle: { width: "15%" },
        },
        {
          key: "role",
          label: "Role",
          sortable: true,
          thStyle: { width: "15%" },
          tdStyle: { width: "15%" },
        },
        {
          key: "actions",
          label: "Actions",
          thStyle: { width: "15%" },
          tdStyle: { width: "15%" },
        },
      ],
      users: [],
      infoModal: {
        id: "info-modal",
        title: "",
        content: "",
      },
      selectedId: null,
      json_fields: {
        Id: "id",
        "Full Name": "fullName",
        "Birth Date": {
          field: "birthDate",
          callback: (value) => {
            return this.$options.filters.formatDate(value);
          },
        },
        "E-mail": "email",
        Role: "role",
        "Active?": "active",
      },
      json_meta: [
        [
          {
            key: "charset",
            value: "utf8",
          },
        ],
      ],
      header: [
        "Relatório de Usuários",
        `Gerado por: ${this.$store.state.user.name}`,
        `Data: ${new Date().toLocaleDateString()}`,
        `Quantidade de páginas: ${this.totalPages()}`,
        ""
      ],
      footer: [
        "Fim do Relatório"
      ],
      excelStyles: {
        'thead th': {
          'text-align': 'left', // Alinha o texto do cabeçalho à esquerda
          'background-color': '#DFF0D8', // Cor de fundo do cabeçalho (verde claro)
          'color': 'black' // Cor do texto do cabeçalho
        },
        'header': {
          'text-align': 'left' // Alinha o texto do cabeçalho à esquerda
        }
      }
    };
  },
  methods: {
    loadUsers() {
      this.$http
        .get("/users")
        .then((res) => {
          this.users = res.data;
          this.totalRows = this.users.length;
        })
        .catch((er) => {
          console.error("erro chamada user", er);
          if (er.response && er.response.status === 401) {
            // Exibe uma mensagem de erro
            alert("Session expired. Please login again.");

            // Redireciona para a tela de login
            localStorage.removeItem(userKey);
            this.$store.commit("setUser", null);
            this.$router.push({ path: "/auth" });
          } else {
            // Exibe outros erros, se houver
            alert("Ocorreu um erro ao carregar os usuários.");
          }
        });
    },
    editUser(id) {
      this.$router.push({
        name: "user-edit",
        params: { id },
      });
    },
    info(item, index, button) {
      this.infoModal.title = `Row index: ${index}`;
      this.infoModal.content = this.buildMessage(item);
      this.$root.$emit("bv::show::modal", this.infoModal.id, button);
    },
    buildMessage(item) {
      return `
      Nome: ${item.fullName} 
      E-mail: ${item.email}`;
    },
    resetInfoModal() {},
    openDeleteModal(id) {
      this.selectedId = id;
      this.$bvModal.show("deleteModal");
    },
    confirmDisable() {
      if (this.selectedId && this.selectedId > 0)
        this.$http
          .delete(`/users/${this.selectedId}`)
          .then(() => {
            this.$toasted.global.defaultSuccess();
            this.loadUsers();
          })
          .catch(showError);
    },

    generatePDF() {
      const pdf = new jsPDF({ orientation: "landscape" });
      const columns = [
        { title: "Full Name", dataKey: "fullName" },
        { title: "Birth Date", dataKey: "birthDate" },
        { title: "Active", dataKey: "active" },
        { title: "Role", dataKey: "role" },
      ];

      const formatDate = (dateString) => {
        const date = new Date(dateString);
        return date.toLocaleDateString("en-GB");
      };

      const totalPages = this.totalPages()
      const date = new Date().toLocaleDateString();

      // Adiciona o título e cabeçalho na primeira página
      pdf.setFontSize(18);
      pdf.text("Relatório de Usuários Cadastrados", 14, 22);
      pdf.setFontSize(12);
      pdf.text(`Data: ${date}`, 14, 30);

      for (let i = 1; i <= totalPages; i++) {
        this.currentPage = i;
        const start = (i - 1) * this.perPage;
        const end = start + this.perPage;
        const pageData = this.totalRows > 0 ? this.users.slice(start, end) : [];

        if (i > 1) {
          pdf.addPage();
          pdf.setFontSize(18);
          pdf.text("Relatório de Usuários Cadastrados", 14, 22);
          pdf.setFontSize(12);
          pdf.text(`Data: ${date}`, 14, 30);
        }

        pdf.autoTable({
          head: [columns.map((col) => col.title)],
          body: pageData.map((row) => [
            row.fullName,
            formatDate(row.birthDate),
            row.active ? "Yes" : "No",
            row.role,
          ]),
          startY: 50,
          theme: "grid",
          headStyles: { fillColor: [0, 0, 0] },
          styles: { fontSize: 10 },
        });

        // Adiciona o número da página no rodapé
        pdf.text(
          `Página ${i} de ${totalPages}`,
          pdf.internal.pageSize.width / 2,
          pdf.internal.pageSize.height - 10,
          { align: "center" }
        );
      }

      pdf.save("users.pdf");
      this.$bvModal.hide('export-modal');
    },
    async fetchData() {
      return this.users;
    },
    totalPages() {
      return this.totalRows > 0 ? Math.ceil(this.totalRows / this.perPage) : 1;
    },
    async generateExcel() {
      const workbook = new ExcelJS.Workbook();
      const worksheet = workbook.addWorksheet('Relatório');

      // Adiciona o título
      worksheet.mergeCells('A1', 'F1');
      const titleCell = worksheet.getCell('A1');
      titleCell.value = 'Relatório de Usuários';
      titleCell.font = { size: 20, bold: true };
      titleCell.alignment = { vertical: 'middle', horizontal: 'center' };

      // Adiciona informações adicionais
      worksheet.addRow([`Gerado por: ${this.$store.state.user.name}`]);
      worksheet.addRow([`Data: ${new Date().toLocaleDateString()}`]);
      worksheet.addRow([`Quantidade de itens: ${this.users.length}`]);
      worksheet.addRow([]); // Linha em branco

      // Adiciona cabeçalho do grid
      const headerRow = worksheet.addRow(['Id', 'Full Name', 'Birth Date', 'E-mail', 'Role', 'Active?']);
      headerRow.eachCell((cell) => {
        cell.font = { bold: true };
        cell.fill = {
          type: 'pattern',
          pattern: 'solid',
          fgColor: { argb: 'DFF0D8' }
        };
      });

      // Adiciona dados
      this.users.forEach((item) => {
        worksheet.addRow([item.id, item.fullName, this.$options.filters.formatDate(item.birthDate), item.email, item.role, (item.active ? 'Yes' : 'No')]);
      });

      // Define a largura das colunas individualmente
      worksheet.getColumn(1).width = 5;  // Id
      worksheet.getColumn(2).width = 30; // Full Name
      worksheet.getColumn(3).width = 13; // Birth Date
      worksheet.getColumn(4).width = 35; // E-mail
      worksheet.getColumn(5).width = 15; // Role
      worksheet.getColumn(6).width = 10; // Active?

      // Salva o arquivo
      const buffer = await workbook.xlsx.writeBuffer();
      saveAs(new Blob([buffer]), 'relatorio_usuarios.xlsx');

      this.$bvModal.hide('export-modal');
    }
  },
  computed: {
    paginatedItems() {
      const start = (this.currentPage - 1) * this.perPage;
      const end = this.currentPage * this.perPage;
      return this.users.slice(start, end);
    },
  },
  mounted() {
    this.loadUsers();
  },
};
</script>

<style scoped>
.pagination {
  display: flex;
  flex-direction: row;
  justify-content: center;
}
ul {
  list-style-type: none;
}

.card-body {
  padding: 20px 50px 20px 50px;
  font-size: 14px;
  width: 50%;
}

.details-card {
  border: none;
  border-radius: 10px;
  box-shadow: 0 4px 8px rgba(0, 0, 0, 0.15);
  background-color: #fff;
  align-items: center;
}

.details-container {
  display: flex;
  flex-direction: column;
  gap: 0px;
}

.details-item {
  display: flex;
  justify-content: space-between;
  padding: 5px 0;
  border-bottom: 1px solid #ddd;
}

.details-label {
  font-weight: bold;
  color: #333;
}

.details-value {
  color: #555;
}
.custom-cancel-btn,
.custom-ok-btn {
  color: white;
}

.buttons-report {
  font-size: 20px;
}

#deleteModal > .btn {
  color: white !important;
}

.btn-pdf {
color: #FA0F00;
}

.btn-excel {
  color: #10793f;
}

</style>
