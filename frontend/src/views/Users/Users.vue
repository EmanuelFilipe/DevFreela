<template>
  <div class="user w-75">
    <b-form>
      <b-row>
        <b-col>
          <h1 class="text-center">Users</h1>
        </b-col>
      </b-row>
      <b-row>
        <b-col>
          <div class="d-flex justify-content-end">
            <!-- <p>
              <a class="text-right btn btn-outline-primary fa fa-plus"> New</a>
            </p> -->
            <router-link to="/user/create" tag="a"
              class="text-right btn btn-outline-primary fa fa-plus">
              <a> New</a>
            </router-link>
          </div>
        </b-col>
      </b-row>
      <hr />
    </b-form>

    <b-table
      class="table"
      hover
      striped
      :items="users"
      :fields="fields"
      style="color: white"
    >
      <template #cell(actions)="data">
        <button @click="loadUser(data.item)" class="mr-2 btn btn-info" title="Detail" >
          <i class="fa fa-solid fa-eye"></i>
        </button>
        <button @click="loadUser(data.item)" class="mr-2 btn btn-warning" title="Edit">
          <i class="fa fa-pencil"></i>
        </button>
        <button class="btn btn-danger" @click="loadUser(data.item, 'remove')" title="Remove">
          <i class="fa fa-trash"></i>
        </button>
      </template>
    </b-table>
  </div>
</template>

<script>
export default {
  name: "Users",
  data() {
    return {
      fields: [
        { key: "id", label: "Code", sortable: true },
        { key: "fullName", label: "Full Name", sortable: true },
        { key: "birthDate", label: "Birth Date", sortable: true },
        { key: "active", label: "Active", sortable: true },
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
  },
  mounted() {
    this.loadUsers();
  },
};
</script>

<style scoped>
.sr-only {
  color: white !important;
}
</style>
