import { validateEmptyAndEmail, ValidatePassword } from '@/utils/validators'

describe('validators utils', () => {
    it('should give a message error when try make login without fill email field', () => {
        expect(validateEmptyAndEmail()).toBe('E-mail is mandatory!')
    })

    it('should give me a invalid email message error', () => {
        expect(validateEmptyAndEmail('123')).toBe('E-mail field must be a valid e-mail!')
    })

    it('not should give me any message error when call validation email function', () => {
        expect(validateEmptyAndEmail('teste@teste.com')).toBe(undefined)
    })

    it('should give a message error when try make login without fill password field', () => {
        expect(ValidatePassword()).toBe('Password is mandatory!')
    })

    it('should give me a invalid password message error', () => {
        expect(ValidatePassword('123')).toContain('password must be have 8 caracteres')
    })

    it('not should give me any message error when call validation password function', () => {
        expect(ValidatePassword('Aa12345!')).toBe(undefined)
    })
})