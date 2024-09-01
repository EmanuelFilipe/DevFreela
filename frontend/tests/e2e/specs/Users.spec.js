const APP_URL = 'http://localhost:8080'

describe('<User /> Test without login', () => {
    it('should show create user page', () => {
        cy.visit(APP_URL)
        cy.contains('.auth-title', 'Login')
        cy.get('#signup').click()

        cy.url().should('include', '/user')
        cy.contains('h1', 'Create User')
    })

    it('should create a new user with success', () => {
        cy.visit(APP_URL)
        cy.contains('.auth-title', 'Login')
        cy.get('#signup').click()

        cy.url().should('include', '/user')
        cy.contains('h1', 'Create User')

        cy.get('#input-user-name').type('teste')
    })
})
describe('<Users />', () => {
    beforeEach(() => {
        cy.login('teste3@teste.com', 'Aa12345!')
        cy.wait(1000)
    })

    it.skip('should be able to log in', () => {
    })

    it.skip('should render users page', () => {
        cy.get('#title-users').click()
        cy.contains('h1', 'Users')
    })

    it.skip('should render modal detail of the record on grid', () => {
        cy.get('#title-users').click()
        cy.contains('h1', 'Users')

        cy.get('#btn-detail-user').click()
        cy.get('#modal-detail-user')
        cy.get('#modal-detail-user-id')

        cy.url().should('include', '/users')
    })

    it.skip('should render warning delete modal after click on disable-button', () => {
        cy.get('#title-users').click()
        cy.wait(1000)

        cy.get('#btn-disabled-user').click()
        cy.get('#deleteModal')

        cy.contains('#deleteModal___BV_modal_title_', 'Confirm Disabling')
        cy.contains('p', 'Are you sure you want to disable this item?')
    })

    it.skip('should render warning delete modal after click on disable-button', () => {
        cy.get('#title-users').click()
        cy.wait(1000)

        cy.get('#btn-disabled-user').click()
        cy.get('#deleteModal')

        cy.contains('#deleteModal___BV_modal_title_', 'Confirm Disabling')
        cy.contains('p', 'Are you sure you want to disable this item?')

        cy.get('#btn-disabled-user').click()
        cy.get('#deleteModal')
        cy.get('.btn-primary').click()
    })

    it.skip('should render different value when the status is desabled', () => {
        let oldStatus
        let newStatus
        cy.get('#title-users').click()
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
