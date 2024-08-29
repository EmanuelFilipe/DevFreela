const APP_URL = 'http://localhost:8080'

describe('Home', () => {
    it('should render span in Home page', () => {
        cy.visit(APP_URL)
        cy.contains('h1', 'Bem-vindo ao Sistema de Freelancers')
    })
})