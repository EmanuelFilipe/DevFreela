<template>
  <div>
    <h1>{{ this.action === "create" ? "Create" : "Edit" }} Project</h1>
    <b-form @submit="onSubmit">
      <b-row class="mt-3">
        <b-col md="6" sm="12">
          <b-form-group>
              <LabelInput :name="'Name:'" />
              <b-form-input
                id="input-project-name"
                v-model="project.title"
                placeholder="Enter name project"
                required
              ></b-form-input>
          </b-form-group>
        </b-col>
      </b-row>
      <b-row>
        <b-col>
            <LabelInput :name="'Description:'" /> 
            <b-form-textarea
              id="textarea"
              v-model="project.description"
              placeholder="Enter the project description..."
              rows="3"
              max-rows="6"
            ></b-form-textarea>
        </b-col>
      </b-row>
      <b-row>
        <b-col md="6" sm="12">
            <LabelInput :name="'Client:'" /> 
            <b-form-select
              id="select-project-client"
              v-model="selectedClient"
              :options="clients"
              required
            ></b-form-select>
        </b-col>
        <b-col md="6" sm="12">
            <LabelInput :name="'Freelancers:'" /> 
            <b-form-select
              id="select-project-freelancer"
              v-model="selectedFreelancer"
              :options="freelancers"
              required
            ></b-form-select>
        </b-col>
      </b-row>
      <b-row>
        <b-col md="6" sm="12">
            <LabelInput :name="'Total Cost:'" /> 
            <b-form-input
              id="project-totalCost"
              type="number"
              v-model="project.totalCost"
              required
              placeholder="Budget"
              min="0"
              @blur="preventNegative"
            />
        </b-col>
        <b-col md="6" sm="12">
          <LabelInput :name="'Status:'" :mandatory-icon="false" /> 
            <b-form-select
              id="select-project-status"
              v-model="selectedStatus"
              :options="status"
              required
              :disabled="this.action === 'create'"
            ></b-form-select>
        </b-col>
      </b-row>
      <hr />
      <b-button type="submit" variant="primary mr-2" id="btn-save"
        >Submit</b-button
      >
      <b-button @click="cancel" variant="secondary" id="btn-cancel"
        >Cancel</b-button
      >
    </b-form>
  </div>
</template>

<script>
import { showError } from "@/global";
import LabelInput from "@/components/template/LabelInput.vue";
export default {
  name: "Project",
  components: { LabelInput },
  data() {
    return {
      action: "",
      id: this.$route.params.id,
      project: {},
      clients: [],
      freelancers: [],
      status: [
        { text: "-- Please select an option --", value: null },
        { text: "Created", value: 0 },
        { text: "InProgress", value: 1 },
        { text: "Suspended", value: 2 },
        { text: "Canceled", value: 3 },
        { text: "Finished", value: 4 },
        { text: "PaymentPending", value: 5 },
      ],
      selectedFreelancer: null,
      selectedClient: null,
      selectedStatus: 1,
    };
  },
  methods: {
    onSubmit(event) {
      event.preventDefault();
      const method = this.action === "create" ? "post" : "put";
      let url = "";

      if (method === "post") url = "/projects";
      else url = `/projects/${this.id}`;

      this.$http[method](url, this.project)
        .then(() => {
          this.$toasted.global.defaultSuccess();
          this.$router.push({ name: "projects" });
        })
        .catch(showError);
    },
    loadUsers() {
      this.$http
        .get("/users")
        .then((res) => {
          const response = res.data;
          this.clients.unshift({
            text: "-- Please select an option --",
            value: null,
          });
          this.clients = response
            .filter((user) => user.role === "client" && user.active)
            .map((user) => {
              return { value: user.id, text: user.fullName };
            });

          this.freelancers.unshift({
            text: "-- Please select an option --",
            value: null,
          });
          this.freelancers = response
            .filter((user) => user.role === "freelancer" && user.active)
            .map((user) => {
              return { value: user.id, text: user.fullName };
            });
        })
        .catch(showError);
    },
    loadProject() {
      this.$http.get(`/projects/${this.id}`).then((res) => {
        this.project = res.data;
        this.selectedClient = res.data.idClient;
        this.selectedFreelancer = res.data.idFreelancer;
        this.getValueStatus(res.data.status)
      });
    },
    cancel() {
        this.$router.go(-1);
    },
    preventNegative() {
      // Verifica se o valor do project.totalCost é negativo
      if (this.project.totalCost < 0) {
        this.project.totalCost = 0; // Se for negativo, ajusta para 0
      }
    },
    getValueStatus(value) {
      switch (value) {
        case "Created":
          this.selectedStatus = 0
          break;
        case "InProgress":
          this.selectedStatus = 1
          break;
        case "Suspended":
          this.selectedStatus = 2
          break;
        case "Canceled":
          this.selectedStatus = 3
          break;
        case "Finished":
          this.selectedStatus = 4
          break;
        case "PaymentPending":
          this.selectedStatus = 5
          break;
      }
    },
  },
  watch: {
    selectedClient(value) {
      this.project.idClient = value;
    },
    selectedFreelancer(value) {
      this.project.idFreelancer = value;
    },
    selectedStatus(value) {
      this.project.status = value ? true : false;
    },
  },
  created() {
    this.action = this.$route?.params?.id ? "edit" : "create";
    this.loadUsers();

    if (this.action === "edit") {
      this.loadProject();
    } else this.selectedStatus = 0;
  },
};
</script>

<style scoped>
button {
  color: #fff;
}
</style>
