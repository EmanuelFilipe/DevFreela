<template>
  <b-container fluid>
    <b-form>
      <b-row>
        <b-col>
          <h1 class="text-center">Users</h1>
        </b-col>
      </b-row>
      <b-row>
        <b-col>
          <div class="d-flex justify-content-end">
            <router-link
              to="/user/create"
              tag="a"
              class="text-right btn btn-outline-primary fa fa-plus"
            >
              <a> New</a>
            </router-link>
          </div>
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
      :items="users"
      :fields="fields"
      head-variant="dark"
    >
      <template #cell(actions)="data">
        <b-button @click="data.toggleDetails" class="mr-2 btn btn-info">
          <i class="fa fa-solid fa-eye"></i>
        </b-button>
        <button
          @click="editUser(data.item.id)"
          class="mr-2 btn btn-warning"
          title="Edit"
        >
          <i class="fa fa-pencil"></i>
        </button>

        <button
          class="btn btn-danger"
          @click="openDeleteModal(data.item.id)"
          title="Remove"
        >
          <i class="fa fa-trash"></i>
        </button>

        <!-- <b-button
          size="sm"
          @click="info(data.item, data.index, $event.target)"
          class="mr-1"
        >
          Info modal
        </b-button> -->
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

        <b-card class="details-card">
          <div class="details-container">
            <div class="details-item">
              <span class="details-label text-right">Id:</span>
              <span class="details-value">{{ data.item.id }}</span>
            </div>
            <div class="details-item">
              <span class="details-label">Nome Completo:</span>
              <span class="details-value">{{ data.item.fullName }}</span>
            </div>
            <div class="details-item">
              <span class="details-label">Email:</span>
              <span class="details-value">{{ data.item.email }}</span>
            </div>
            <div class="details-item">
              <span class="details-label">Data de Nascimento:</span>
              <span class="details-value">{{
                new Date(data.item.birthDate).toLocaleDateString()
              }}</span>
            </div>
            <div class="details-item">
              <span class="details-label">Role:</span>
              <span class="details-value">{{ data.item.role }}</span>
            </div>
            <div class="details-item">
              <span class="details-label">Ativo:</span>
              <span class="details-value">{{
                data.item.active ? "Sim" : "Não"
              }}</span>
            </div>
          </div>
        </b-card>
      </template>
    </b-table>

    <b-modal
      id="deleteModal"
      title="Confirmar Remoção"
      @ok="confirmDelete"
      ok-title="OK"
      cancel-title="Cancelar"
    >
      <p>Você tem certeza que deseja remover este item?</p>
    </b-modal>

    <!-- Info modal -->
    <b-modal :id="infoModal.id" :title="infoModal.title" @hide="resetInfoModal">
      <pre>{{ infoModal.content }}</pre>
    </b-modal>
  </b-container>
</template>

<script>
/* eslint-disable */
export default {
  name: "Users",
  data() {
    return {
      isBusy: false,
      fields: [
        { key: "id", label: "Code", sortable: true },
        { key: "fullName", label: "Full Name", sortable: true },
        { key: "birthDate", label: "Birth Date", sortable: true },
        {
          key: "active",
          label: "Active",
          sortable: true,
          formatter: (value) => (value ? "Yes" : "No"),
        },
        { key: "actions", label: "Actions" },
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
      this.$http.get("/users").then((res) => {
        this.users = res.data;
      });
    },
    editUser(id) {
      this.$router.push({
        name: "user-create-id",
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
    confirmDelete() {
      if (this.selectedId && this.selectedId > 0)
        this.$http.delete(`/users/${this.selectedId}`)
                  .then(() => {
                    this.loadUsers()
                  })
    },
    // toggleBusy() {
    //     this.isBusy = true
    //   },
  },
  mounted() {
    this.loadUsers();
  },
};
</script>

<style scoped>
ul {
  list-style-type: none;
}

.card-body {
  padding: 20px 50px 20px 50px;
  font-size: 14px;
}

.details-card {
  border: none;
  border-radius: 10px;
  box-shadow: 0 4px 8px rgba(0, 0, 0, 0.15);
  background-color: #fff;
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
</style>
