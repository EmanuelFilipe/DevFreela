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
            <router-link to="/user/create" tag="a"
              class="text-right btn btn-outline-primary fa fa-plus">
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
        <b-button @click="data.toggleDetails" class="mr-2 btn btn-info" >
          <i class="fa fa-solid fa-eye"></i>
        </b-button>
        <button @click="editUser(data.item.id)" class="mr-2 btn btn-warning" title="Edit">
          <i class="fa fa-pencil"></i>
        </button>
        <button class="btn btn-danger" @click="loadUser(data.item, 'remove')" title="Remove">
          <i class="fa fa-trash"></i>
        </button>
      </template>

      <template #row-details="data">
        <b-card>
          <ul>
            <li v-for="(value, key) in data.item" :key="key"><strong>{{ key }}:</strong> {{ value }}</li>
          </ul>
        </b-card>
      </template>
    </b-table>
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
        { key: "active", label: "Active", sortable: true,
          formatter: value => value ? 'Yes' : 'No' },
        { key: "actions", label: "Actions" },
      ],
      users: [],
    };
  },
  methods: {
    loadUsers() {
      this.$http.get("/users").then((res) => {
        this.users = res.data;
      });
    },
    editUser(id){
      this.$router.push({
        name: 'user-create-id',
        params: {id}
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
</style>