const APP_URL = 'http://localhost:8080'

describe('Home', () => {
    it('should render span in Home page', () => {
        cy.visit(APP_URL)

        cy.contains('.auth-title', 'Login')
        cy.get('#user-email').type('teste3@teste.com')
        cy.get('#user-password').type('Aa12345!')
        cy.get('#btn-login').click()

        // header
        cy.url().should('include', '/')
        cy.contains('h1', 'Welcome to the Freelancer System')
        cy.get('#title').contains('span', 'Dev')
        cy.get('#title-home').contains('span', 'Home')
        cy.get('#title-users').contains('span', 'Users')
    })
})