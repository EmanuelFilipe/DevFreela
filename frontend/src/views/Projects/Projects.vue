<template>
    <b-container fluid>
        <b-form>
            <b-row>
                <b-col>
                    <h1 class="display-2">Projects</h1>
                </b-col>
            </b-row>
            <b-row>
                <b-col sm="6" md="4">
                    <b-form-input
                        id="input-filter"
                        v-model="query"
                        placeholder="Filter by Title or Description">
                    </b-form-input>
                </b-col>
                <b-col>
                    <ButtonCreate url="/project" />
                </b-col>
            </b-row>
            <hr>
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
                    id="btn-detail-user"
                    @click="data.toggleDetails"
                    class="mr-2 btn btn-info">
                    <i class="fa fa-solid fa-eye"></i>
                </b-button>
                <button
                    id="btn-edit-user"
                    @click="editUser(data.item.id)"
                    class="mr-2 btn btn-warning"
                    title="Edit">
                    <i class="fa fa-pencil"></i>
                </button>

                <button
                    id="btn-disabled-user"
                    class="btn btn-danger"
                    @click="openDeleteModal(data.item.id)"
                    title="Disable">
                <i class="fa fa-trash"></i>
                </button>
            </template>

            <template #row-details="data">
                <b-card class="details-card" id="modal-detail-user">
                    <div class="details-container">
                        <div class="details-item">
                            <span class="details-label text-right" id="modal-detail-user-id"
                                >Id:</span
                            >
                            <span class="details-value">{{ data.item.id }}</span>
                        </div>
                        <div class="details-item">
                            <span class="details-label">Title:</span>
                            <span class="details-value">{{ data.item.title }}</span>
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
import ButtonCreate from '@/components/template/ButtonCreate.vue';

export default {
    name: 'Projects',
    components: {
        ButtonCreate
    },
    data() {
        return {
            projects: [], // Todos os projetos retornados da API
            paginatedProjects: [], // Projetos paginados no frontend
            currentPage: 1, // Página atual no frontend
            //perPage: 4, // Exibir 2 registros por página no frontend -- TROCAR PELO PAGESIZE da API
            totalRows: 0, // Total de registros na API
            query: '', // Filtro opcional para a busca
            fields: [
                { key: 'title', label: 'Title', sortable: true },
                { key: 'createdAt', label: 'Created At', sortable: true,
                    thStyle: { width: '15%' },
                    tdStyle: { width: '15%' }
                },
                { key: "actions", label: "Actions", 
                    thStyle: { width: '15%' },
                    tdStyle: { width: '15%' }
                },
            ]
        }
    },
    methods: {
        loadProjects() {
            const params = {
                query: this.query,
                page: this.currentPage // Envia o número da página correta para a API
            };
            // const params = {
            //     page: this.currentPage, // Envia o número da página para a API
            //     perPage: this.perPage   // Envia quantos registros por página queremos
            // };
            // const params = {
            //     page: this.currentPage, // Envia o número da página para a API
            // };
            console.log('url', `/projects?Page=${this.currentPage}`)
            this.$http
            .get("/projects", { params })
                .then((res) => {
                    this.projects = null
                    const responseData = res.data
                    this.projects = responseData.data // Todos os projetos retornados da API
                    this.totalRows = responseData.itemsCount // Total de registros
                    //this.perPage = responseData.pageSize
                    console.log('response API', res.data)
                })
                .catch((err) => {
                    console.error("Error fetching projects:", err);
                });
        },
        // paginateProjects() {
        //     // Paginando os projetos no frontend com base na `currentPage` e `perPage`
        //     const start = (this.currentPage - 1) * this.perPage;
        //     const end = start + this.perPage;
        //     console.log('paginatedProjects',this.projects)
        //     console.log('this.projects.slice(start, end);',this.projects.slice(start, end))
        //     this.paginatedProjects = this.projects.slice(start, end);
        //     console.log('this.paginatedProjects;', this.paginatedProjects)
        // }
    },
    watch: {
        currentPage() {
            this.loadProjects();
        },
        query() {
            this.currentPage = 1; // Reinicia para a primeira página ao mudar o filtro
            this.loadProjects();
        }
    },
    created() {
        this.loadProjects();
    }
}
</script>



<style scoped>
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

.action-column {
    width: 15%;
}
</style>