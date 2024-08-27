<template>
    <b-container fluid>
      <!-- Main table element -->
      <b-table
        :items="items"
        :fields="fields"
        :current-page="currentPage"
        :per-page="perPage"
        :filter="filter"
        :filter-included-fields="filterOn"
        :sort-by.sync="sortBy"
        :sort-desc.sync="sortDesc"
        :sort-direction="sortDirection"
        stacked="md"
        show-empty
        small
        @filtered="onFiltered"
      >
        <template #cell(name)="row">
          {{ row.value.first }} {{ row.value.last }}
        </template>
  
        <template #cell(actions)="row">
          <b-button size="sm" @click="info(row.item, row.index, $event.target)" class="mr-1">
            Info modal
          </b-button>
        </template>
  
        <template #row-details="row">
          <b-card>
            <ul>
              <li v-for="(value, key) in row.item" :key="key">{{ key }}: {{ value }}</li>
            </ul>
          </b-card>
        </template>
      </b-table>
  
      <!-- Info modal -->
      <b-modal :id="infoModal.id" :title="infoModal.title" ok-only @hide="resetInfoModal">
        <pre>{{ infoModal.content }}</pre>
      </b-modal>
    </b-container>
  </template>
  
  <script>
  /* eslint-disable */
    export default {
      data() {
        return {
          items: [
            { isActive: true, age: 40, name: { first: 'Dickerson', last: 'Macdonald' } },
            { isActive: false, age: 21, name: { first: 'Larsen', last: 'Shaw' } },
            {
              isActive: false,
              age: 9,
              name: { first: 'Mini', last: 'Navarro' },
              _rowVariant: 'success'
            },
            { isActive: false, age: 89, name: { first: 'Geneva', last: 'Wilson' } },
            { isActive: true, age: 38, name: { first: 'Jami', last: 'Carney' } },
            { isActive: false, age: 27, name: { first: 'Essie', last: 'Dunlap' } },
            { isActive: true, age: 40, name: { first: 'Thor', last: 'Macdonald' } },
            {
              isActive: true,
              age: 87,
              name: { first: 'Larsen', last: 'Shaw' },
              _cellVariants: { age: 'danger', isActive: 'warning' }
            },
            { isActive: false, age: 26, name: { first: 'Mitzi', last: 'Navarro' } },
            { isActive: false, age: 22, name: { first: 'Genevieve', last: 'Wilson' } },
            { isActive: true, age: 38, name: { first: 'John', last: 'Carney' } },
            { isActive: false, age: 29, name: { first: 'Dick', last: 'Dunlap' } }
          ],
          fields: [
            { key: 'name', label: 'Person full name', sortable: true, sortDirection: 'desc' },
            { key: 'age', label: 'Person age', sortable: true, class: 'text-center' },
            {
              key: 'isActive',
              label: 'Is Active',
              formatter: (value, key, item) => {
                return value ? 'Yes' : 'No'
              },
              sortable: true,
              sortByFormatted: true,
              filterByFormatted: true
            },
            { key: 'actions', label: 'Actions' }
          ],
          totalRows: 1,
          currentPage: 1,
          perPage: 5,
          pageOptions: [5, 10, 15, { value: 100, text: "Show a lot" }],
          sortBy: '',
          sortDesc: false,
          sortDirection: 'asc',
          filter: null,
          filterOn: [],
          infoModal: {
            id: 'info-modal',
            title: '',
            content: ''
          }
        }
      },
      computed: {
        sortOptions() {
          // Create an options list from our fields
          return this.fields
            .filter(f => f.sortable)
            .map(f => {
              return { text: f.label, value: f.key }
            })
        }
      },
      mounted() {
        // Set the initial number of items
        this.totalRows = this.items.length
      },
      methods: {
        info(item, index, button) {
          this.infoModal.title = `Row index: ${index}`
          this.infoModal.content = JSON.stringify(item, null, 2)
          this.$root.$emit('bv::show::modal', this.infoModal.id, button)
        },
        resetInfoModal() {
          this.infoModal.title = ''
          this.infoModal.content = ''
        },
        onFiltered(filteredItems) {
          // Trigger pagination to update the number of buttons/pages due to filtering
          this.totalRows = filteredItems.length
          this.currentPage = 1
        }
      }
    }
  </script>