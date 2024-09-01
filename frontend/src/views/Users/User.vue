<template>
  <div>
    <h1 >{{ this.action === "create" ? "Create" : "Edit" }} User</h1>
    <b-form @submit="onSubmit">
      <b-row class="mt-3">
        <b-col md="6" sm="12" >
          <b-form-group label="Name:" label-for="user-name">
            <b-form-input
              id="input-user-name"
              v-model="user.fullName"
              placeholder="Enter name"
              required
            ></b-form-input>
          </b-form-group>
        </b-col>
        <b-col md="6" sm="12">
          <b-form-group
            label="Email:"
            label-for="user-email"
            description="We'll never share your email with anyone else."
          >
            <b-form-input
              id="input-user-email"
              v-model="user.email"
              type="email"
              placeholder="Enter email"
              required
            ></b-form-input>
          </b-form-group>
        </b-col>
      </b-row>
      <b-row>
        <b-col md="2" sm="12">
          <b-form-group label="Birth Date" label-for="user-birthDate">
            <b-form-input
              id="user-birthDate"
              type="date"
              v-model="formattedBirthDate"
            >
            </b-form-input>
          </b-form-group>
        </b-col>
        <b-col md="4" sm="12">
          <b-form-group label="Role:" label-for="user-role">
            <b-form-select
              id="select-user-role"
              v-model="selectedRole"
              :options="roles"
              required
            ></b-form-select>
          </b-form-group>
        </b-col>
        <b-col md="4" sm="12">
          <b-form-group label="Active:" label-for="user-active">
            <b-form-select
              id="select-user-active"
              v-model="selectedActive"
              :options="active"
              required
              :disabled="action === 'create'"
            ></b-form-select>
          </b-form-group>
        </b-col>
      </b-row>
      <b-row>
        <b-col md="6" sm="12">
          <b-form-group label="Password:" label-for="user-password">
            <b-form-input
              id="user-password"
              type="password"
              v-model="user.password"
              required
              placeholder="Informe a Senha do Usuário..."
            />
          </b-form-group>
        </b-col>
        <b-col md="6" sm="12">
          <b-form-group
            label="Confirm Password:"
            label-for="user-confirm-password"
          >
            <b-form-input
              id="user-confirm-password"
              type="password"
              v-model="user.confirmPassword"
              required
              placeholder="Confirme a Senha do Usuário..."
            />
          </b-form-group>
        </b-col>
      </b-row>
      <hr />
      <b-button type="submit" variant="primary mr-2" id="btn-save">Submit</b-button>
      <b-button @click="cancel" variant="secondary" id="btn-cancel">Cancel</b-button>
    </b-form>
  </div>
</template>
  
  <script>
export default {
  name: 'UserCreate',
  data() {
    return {
      action: "",
      id: this.$route.params.id,
      birthDateFormated: '',
      user: {},
      selectedRole: null,
      roles: [
        { text: '-- Please select an option --', value: null },
        { text: "Admin", value: "admin" },
        { text: "Client", value: "client" },
      ],
      active: [
        { text: "Yes", value: 1 },
        { text: "No", value: 0 },
      ],
      selectedActive: 1
    };
  },
  computed: {
    formattedBirthDate: {
      get() {
        return this.user.birthDate ? this.user.birthDate.split('T')[0] : '';
      },
      set(newValue) {
        this.user.birthDate = newValue;
      },
    },
  },
  methods: {
    onSubmit(event) {
      event.preventDefault();
      const method = this.action === "create" ? "post" : "put";
      this.$http[method]("/users", this.user).then(() => {
        this.$toasted.global.defaultSuccess();
        this.$router.push({ name: "auth" });
      });
    },
    cancel() {
      this.$router.go(-1);
    },
    loadUser() {
      const url = `/users/${this.id}`;
      this.$http.get(url).then((res) => {
        this.user = res.data;
        this.selectedRole = this.user.role
        this.selectedActive = this.user.active ? 1 : 0
      });
    },
  },
  created() {
    this.action = this.$route?.params?.id ? "edit" : "create";

    if (this.action === "edit") this.loadUser();
  },
  watch: {
    selectedRole(newVal) {
      this.user.role = newVal;
    },
    selectedActive(value) {
      this.user.active = value ? true : false
    }
  },
};
</script>

<style scoped>
button {
  color: #fff;
}
</style>
