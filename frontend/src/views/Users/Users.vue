<template>
  <b-container fluid>
    <b-form>
      <b-row>
        <b-col>
          <h1 class="display-2">Users</h1>
        </b-col>
      </b-row>
      <b-row>
        <b-col>
          <ButtonCreate url="/user" />
        </b-col>
      </b-row>
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
/* eslint-disable */
export default {
  name: "Users",
  components: {
    ButtonCreate
  },
  data() {
    return {
      currentPage: 1,
      perPage: 10, // Quantidade de itens por página
      totalRows: 0,
      isBusy: false,
      fields: [
        { key: "fullName", label: "Full Name", sortable: true },
        { key: "birthDate", label: "Birth Date", sortable: true,
          thStyle: { width: '15%' },
          tdStyle: { width: '15%' }
         },
        {
          key: "active",
          label: "Active",
          sortable: true,
          formatter: (value) => (value ? "Yes" : "No"),
          thStyle: { width: '15%' },
          tdStyle: { width: '15%' }
        },
        { key: "role", label: "Role", sortable: true,
          thStyle: { width: '15%' },
          tdStyle: { width: '15%' }
        },
        { key: "actions", label: "Actions", 
          thStyle: { width: '15%' },
          tdStyle: { width: '15%' }
        },
      ],
      users: [],
      infoModal: {
        id: "info-modal",
        title: "",
        content: "",
      },
      selectedId: null,
    };
  },
  methods: {
    loadUsers() {
      this.$http
        .get("/users")
        .then((res) => {
          this.users = res.data
          this.totalRows = this.users.length;
        })
        .catch((er) => {
          console.error('erro chamada user', er)
          if (er.response && er.response.status === 401) {
            // Exibe uma mensagem de erro
            alert("Session expired. Please login again.");

            // Redireciona para a tela de login
            localStorage.removeItem(userKey)
            this.$store.commit('setUser', null)
            this.$router.push({ path: '/auth'})
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
    filteredItem(item) {
      // Retorna um novo objeto sem o campo '_showDetails'
      const { _showDetails, ...filteredItem } = item;
      return filteredItem;
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
    // toggleBusy() {
    //     this.isBusy = true
    //   },
  },
  computed: {
    paginatedItems() {
      const start = (this.currentPage - 1) * this.perPage
      const end = this.currentPage * this.perPage
      return this.users.slice(start, end)
    }
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
.custom-ok-btn  {
  color: white;
}

</style>
