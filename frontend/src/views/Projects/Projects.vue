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
                    id="btn-detail-project"
                    @click="data.toggleDetails"
                    class="mr-2 btn btn-info">
                    <i class="fa fa-solid fa-eye"></i>
                </b-button>
                <button
                    id="btn-edit-project"
                    @click="editProject(data.item.id)"
                    class="mr-2 btn btn-warning"
                    title="Edit">
                    <i class="fa fa-pencil"></i>
                </button>
                <button
                    id="btn-start-project"
                    class="btn btn-success"
                    :disabled="data.item.status !== 'Created'"
                    @click="openStartModal(data.item.id)"
                    title="Start Project">
                <i class="fa fa-play"></i>
                </button>

                <button
                    id="btn-cancel-project"
                    class="ml-2 btn btn-danger"
                    :disabled="data.item.status !== 'InProgress'"
                    @click="openCancelModal(data.item.id)"
                    title="Cancel Project">
                <i class="fa fa-ban"></i>
                </button>
            </template>

            <template #row-details="data">
                <b-card class="details-card" id="modal-detail-project">
                    <div class="details-container">
                        <div class="details-item">
                            <span class="details-label text-right" id="modal-detail-project-id"
                                >Id:</span
                            >
                            <span class="details-value">{{ data.item.id }}</span>
                        </div>
                        <div class="details-item">
                            <span class="details-label">Title:</span>
                            <span class="details-value">{{ data.item.title }}</span>
                        </div>
                        <div class="details-item">
                            <span class="details-label">Client Name:</span>
                            <span class="details-value">{{ data.item.fullNameClient }}</span>
                        </div>
                        <div class="details-item">
                            <span class="details-label">Freelancer Name:</span>
                            <span class="details-value">{{ data.item.fullNameFreelancer }}</span>
                        </div>
                        <div class="details-item">
                            <span class="details-label">Status:</span>
                            <span class="details-value">{{ data.item.status }}</span>
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
        <b-modal
            id="startModal"
            title="Confirm Starting"
            @ok="confirmStart"
            ok-title="OK"
            cancel-title="Cancelar"
            ok-variant="primary"
            cancel-variant="danger"
            >
            <p>Are you sure you want <strong>"Start"</strong> this project?</p>
        </b-modal>

        <b-modal
            id="deleteModal"
            title="Confirm Disabling"
            @ok="confirmDelete"
            ok-title="OK"
            cancel-title="Cancelar"
            ok-variant="primary"
            cancel-variant="danger"
            >
            <p>Are you sure you want <strong>"Finished"</strong> this project?</p>
        </b-modal>

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
import { showError } from '@/global';
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
            perPage: null, // Exibir 2 registros por página no frontend -- TROCAR PELO PAGESIZE da API
            totalRows: 0, // Total de registros na API
            query: '', // Filtro opcional para a busca
            selectedId: null,
            fields: [
                { key: 'title', label: 'Title', sortable: true, 
                    thStyle: { width: '28%' },
                    tdStyle: { width: '28%' }
                },
                { key: 'fullNameClient', label: 'Name Client', sortable: true,
                    thStyle: { width: '20%' },
                    tdStyle: { width: '20%' }
                },
                { key: 'fullNameFreelancer', label: 'Name Freelancer', sortable: true,
                    thStyle: { width: '20%' },
                    tdStyle: { width: '20%' }
                },
                { key: 'status', label: 'Status', sortable: true,
                    thStyle: { width: '10%' },
                    tdStyle: { width: '10%' }
                },
                { key: "actions", label: "Actions", 
                    thStyle: { width: '12%' },
                    tdStyle: { width: '12%' }
                },
            ]
        }
    },
    methods: {
        loadProjects() {
            const params = {
                query: this.query,
                page: this.currentPage 
            };
            this.$http
            .get("/projects", { params })
                .then((res) => {
                    this.projects = null
                    const responseData = res.data
                    this.projects = responseData.data
                    this.totalRows = responseData.itemsCount
                    this.perPage = responseData.pageSize
                })
                .catch((err) => {
                    console.error("Error fetching projects:", err);
                });
        },
        editProject(id) {
            this.$router.push({
                name: 'project-edit',
                params: { id }
            })
        },
        openCancelModal(id) {
            this.selectedId = id;
            this.$bvModal.show("deleteModal");
        },
        openStartModal(id) {
            this.selectedId = id;
            this.$bvModal.show("startModal");
        },
        confirmDelete() {
            if (this.selectedId && this.selectedId > 0)
                this.$http
                    .delete(`/projects/${this.selectedId}`)
                    .then(() => {
                        this.$toasted.global.defaultSuccess();
                        this.loadProjects();
            })
            .catch(showError);
        },
        confirmStart() {
            if (this.selectedId && this.selectedId > 0)
                this.$http
                    .put(`/projects/${this.selectedId}/start`)
                    .then(() => {
                        this.$toasted.global.defaultSuccess();
                        this.loadProjects();
            })
            .catch(showError);
        },
    },
    watch: {
        currentPage() {
            this.loadProjects();
        },
        query() {
            this.currentPage = 1;
            this.loadProjects();
        }
    },
    created() {
        this.loadProjects();

    },
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
