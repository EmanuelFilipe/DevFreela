<template>
  <div>
    <h1 >{{ this.action === "create" ? "Create" : "Edit" }} User</h1>
    <b-form @submit="onSubmit">
      <b-row class="mt-3">
        <b-col md="6" sm="12" >
          <LabelInput :name="'Name:'" />
            <b-form-input
              id="input-user-name"
              v-model="user.fullName"
              placeholder="Enter name"
              required
            ></b-form-input>
        </b-col>
        <b-col md="6" sm="12">
          <LabelInput :name="'E-mail:'" />
            <b-form-input
              id="input-user-email"
              v-model="user.email"
              type="email"
              placeholder="Enter email"
              required
            ></b-form-input>
        </b-col>
      </b-row>
      <b-row>
        <b-col md="6" sm="12">
          <LabelInput :name="'Password:'" :mandatoryIcon="action === 'create'" />
          <b-input-group>
            <b-form-input
              id="user-password"
              type="password"
              v-model="user.password"
              :required="action === 'create'"
              placeholder="Enter User Password..."
            />
            <b-input-group-append>
              <b-icon
                icon="info-circle"
                variant="primary"
                class="ml-2 mt-2"
                v-b-tooltip.hover.html="tipData"
              ></b-icon>
            </b-input-group-append>
          </b-input-group>
        </b-col>
        <b-col md="6" sm="12">
          <LabelInput :name="'Confirm Password:'" :mandatoryIcon="action === 'create'" />
            <b-form-input
              id="user-confirm-password"
              type="password"
              v-model="user.confirmPassword"
              :required="action === 'create'"
              placeholder="Confirm User Password..."
              />
              <!-- :state="user.confirmPassword == user.password" -->
        </b-col>
      </b-row>
      <b-row>
        <b-col md="2" sm="12">
            <LabelInput :name="'Birth Date:'" />
            <b-form-input
              id="user-birthDate"
              type="date"
              v-model="formattedBirthDate"
              :max="maxDate"
              required
            >
            </b-form-input>
        </b-col>
        <b-col md="2" sm="12">
          <LabelInput :name="'Role:'" />
            <b-form-select
              id="select-user-role"
              v-model="selectedRole"
              :options="roles"
              required
            ></b-form-select>
        </b-col>
        <b-col md="2" sm="12">
          <LabelInput :name="'Active:'" />
            <b-form-select
              id="select-user-active"
              v-model="selectedActive"
              :options="active"
              required
              :disabled="action === 'create'"
            ></b-form-select>
        </b-col>
      </b-row>
      <!-- <b-row v-if="action === 'create'">
        <b-col md="6" sm="12">
          <b-form-group label="Password:" label-for="user-password">
            <b-form-input
              id="user-password"
              type="password"
              v-model="user.password"
              required
              placeholder="Enter User Password..."
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
              placeholder="Confirm User Password..."
            />
          </b-form-group>
        </b-col>
      </b-row> -->
      <hr />
      <b-button type="submit" variant="primary mr-2" id="btn-save">Submit</b-button>
      <b-button @click="cancel" variant="secondary" id="btn-cancel">Cancel</b-button>
    </b-form>
  </div>
</template>
  
  <script>
import { showError } from '@/global';
import { mapGetters } from 'vuex';
import {  validateEmptyAndEmail, ValidatePassword, ValidateConfirmPassword, ValidatePasswordAndConfirmPassword } from "@/utils/validators";
import LabelInput from '@/components/template/LabelInput.vue';

export default {
  name: 'UserCreate',
  components: { LabelInput },
  data() {
    return {
      tipData: { title: 'Password must have 8 characters. <br>Include at least one number. <br>Include at least one lowercase letter. <br>Include at least one capital letter.<br>Include at least one special character.'},
      action: "",
      id: this.$route.params.id,
      birthDateFormated: '',
      user: {},
      maxDate: this.getTodayDate(),
      selectedRole: null,
      roles: [
        { text: '-- Please select an option --', value: null },
        { text: "Admin", value: "admin" },
        { text: "Client", value: "client" },
        { text: "Freelancer", value: "freelancer" },
      ],
      active: [
        { text: "Yes", value: 1 },
        { text: "No", value: 0 },
      ],
      selectedActive: 1
    };
  },
  computed: {
    ...mapGetters(['isLoggedIn']),
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
      if (this.action === 'create' || (this.user.password != null)) {
        const resultValidation = this.validationPassword()
        if (resultValidation) this.saveUser()
      } else this.saveUser()
    },
    validationPassword() {
      const resultEmail = validateEmptyAndEmail(this.user.email)
      if (resultEmail) {
        this.$toasted.global.defaultError({ msg: resultEmail })
        return false
      }

      const resultPassword = ValidatePassword(this.user.password)
      if (resultPassword) {
        this.$toasted.global.defaultError({ msg: resultPassword })
        return false
      }

      const resultConfirmPassword = ValidateConfirmPassword(this.user.password, this.user.confirmPassword)
      if (resultConfirmPassword) {
        this.$toasted.global.defaultError({ msg: resultConfirmPassword })
        return false
      }

      const resultPasswordAndConfirmPassword = 
        ValidatePasswordAndConfirmPassword(this.user.password, 
                                           this.user.confirmPassword)
        if (resultPasswordAndConfirmPassword) {
          this.$toasted.global.defaultError({ msg: resultPasswordAndConfirmPassword })
          return false
        }

      return true
    },
    saveUser() {
      const method = this.action === "create" ? "post" : "put";
      this.$http[method]("/users", this.user)
      .then(() => {
        this.$toasted.global.defaultSuccess();
        if (this.isLoggedIn) this.$router.push({ name: "users" });
        else this.$router.push({ name: "auth" });
      })
      .catch(showError)
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
    testDate() {
      if (new Date(this.user.birthDate).toLocaleDateString() > new Date().toLocaleDateString()){
        this.formattedBirthDate = new Date().toLocaleDateString()
      }
    },
    getTodayDate() {
      const today = new Date();
      const year = today.getFullYear();
      const month = String(today.getMonth() + 1).padStart(2, '0');
      const day = String(today.getDate()).padStart(2, '0');
      return `${year}-${month}-${day}`;
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
.tooltip .tooltip-inner{
  max-width: 500px !important;
  width: 400px !important;
}
</style>
