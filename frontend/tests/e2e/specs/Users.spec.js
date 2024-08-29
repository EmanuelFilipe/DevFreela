const APP_URL = 'http://localhost:8080'

describe('<Users />', () => {
    it.skip('should render span "Users" on top page', () => {
        cy.visit('/users')
        cy.contains('h1', 'Users')
    })

    it.skip('should render modal detail of the record on grid', () => {
        cy.visit(`${APP_URL}/users`)
        cy.wait(1000)

        cy.get('#btn-detail-user').click()
        cy.get('#modal-detail-user')
        cy.get('#modal-detail-user-id')

        cy.url().should('include', '/users')
    })

    it.skip('should render warning delete modal after click on disable-button', () => {
        cy.visit(`${APP_URL}/users`)
        cy.wait(1000)

        cy.get('#btn-disabled-user').click()
        cy.get('#deleteModal')

        cy.contains('#deleteModal___BV_modal_title_', 'Confirmar Remoção')
        cy.contains('p', 'Você tem certeza que deseja remover este item?')
    })

    it('should render different value when the status is desabled', () => {
        let oldStatus
        let newStatus
        cy.visit(`${APP_URL}/users`)
        cy.wait(1000)

        cy.get('#btn-detail-user').click()
        cy.get('#modal-detail-user')

        cy.get('#detail-user-active-value')
            .then((el) => {
                oldStatus = el[0].innerHTML

                cy.get('#btn-disabled-user').click()
                cy.get('#deleteModal')
                cy.get('.btn-primary').click()

                cy.wait(1000)
                //cy.visit(`${APP_URL}/users`)
                //cy.wait(2000)

                cy.get('#btn-detail-user').click()
                cy.get('#modal-detail-user')

                cy.get('#detail-user-active-value')
                    .then((el1) => {
                        newStatus = el1[0].innerHTML
                        expect(oldStatus).to.not.equal(newStatus)
                        console.log('oldStatus', oldStatus)
                        console.log('newStatus', newStatus)
                    })
            })
    })
})
