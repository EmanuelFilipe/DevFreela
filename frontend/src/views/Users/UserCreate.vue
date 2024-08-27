<template>
    <div id="app">
      <b-container style="margin-top: 20px">
        <b-form>
          <b-row>
            <b-col cols="12" md="6" sm="12">
              <b-form-group label="Nome Completo">
                <b-form-input
                  v-model="nome"
                  placeholder="Digite seu nome completo"
                  required
                ></b-form-input>
              </b-form-group>
            </b-col>
            <b-col cols="12" md="3" sm="12">
              <b-form-group label="Email">
                <b-form-input
                  v-model="email"
                  type="email"
                  placeholder="Digite seu e-mail"
                  required
                ></b-form-input>
              </b-form-group>
            </b-col>
          </b-row>
          <b-row>
            <b-col cols="12" md="3" sm="12">
              <b-form-group label="Telefone">
                <b-form-input
                  v-model="telefone"
                  placeholder="Digite seu Telefone"
                  required
                ></b-form-input>
              </b-form-group>
            </b-col>
            <b-col cols="12" md="3">
              <b-form-group id="sexo" label="Sexo:">
                <b-form-select
                  id="sexo"
                  v-model="sexo"
                  :options="sexos"
                  required
                ></b-form-select>
              </b-form-group>
            </b-col>
          </b-row>
          <b-row>
            <b-col>
              <b-form-group>
                <b-form-checkbox class="mb-2 mr-sm-2 mb-sm-0"
                  >Receber Informações por Email</b-form-checkbox
                >
              </b-form-group>
            </b-col>
          </b-row>
          <b-button type="submit" variant="primary" class="mr-3">Cadastrar</b-button>
          <b-button type="reset" variant="danger">Resetar</b-button>
        </b-form>
      </b-container>
    </div>
   </template>
 
   <script>
   export default {
    name: "App",
    data() {
      return {
        email: null,
        nome: null,
        sexo: null,
        telefone: null,
        sexos: [
          { text: "Selecione", value: null },
          "Masculino",
          "Feminino",
          "Outros",
        ],
      };
    },
    methods: {
      cadastrar() {
        console.log("Método para cadastrar o formulário");
      },
      reset() {
        this.email = null;
        this.nome = null;
        this.sexo = null;
        this.telefone = null;
      },
   },
   };
   </script>