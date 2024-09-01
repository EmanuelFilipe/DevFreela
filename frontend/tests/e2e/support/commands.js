const APP_URL = 'http://localhost:8080'
import { faker } from '@faker-js/faker'

Cypress.Commands.add('login', (email, password) => {
    cy.visit(APP_URL)

    cy.contains('.auth-title', 'Login')
    cy.get('#user-email').type(email)
    cy.get('#user-password').type(password)
    cy.get('#btn-login').click()

})

Cypress.Commands.add('generatePassword', () => {
    return generatePassword()
})

function generatePassword() {
    const lowercase = faker.string.alpha({ count: 1, upcase: false }); // Letra minúscula
    const uppercase = faker.string.alpha({ count: 1, upcase: true }); // Letra maiúscula
    const number = faker.datatype.number.int({ min: 0, max: 9 }) // Dígito
    const special = faker.random.arrayElement(['!', '*', '@', '#', '$', '%', '^', '&', '+', '=']); // Caractere especial
    const remainingLength = 4; // 8 (total) - 4 (1 minúscula, 1 maiúscula, 1 número, 1 especial)
    
    // Gerar caracteres adicionais para preencher a senha
    const additionalChars = faker.random.alphaNumeric(remainingLength);
    
    // Combinar todos os componentes da senha
    let password = lowercase + uppercase + number + special + additionalChars;
  
    // Embaralhar a senha para distribuir os caracteres aleatoriamente
    password = password.split('').sort(() => 0.5 - Math.random()).join('');
    
    return password;
}