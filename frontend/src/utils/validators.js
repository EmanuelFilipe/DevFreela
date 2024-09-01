export function validateEmptyAndEmail(value) {
    if (!value)
        return 'E-mail is mandatory!'

    const isValid = /^[a-z0-9.]+@[a-z0-9]+\.[a-z]+(\.[a-z]+)?$/i.test(value)

    if (!isValid) {
        return 'This field must be a valid e-mail!'
    }
}

export function ValidatePassword(value) {
    if (!value)
        return 'Password is mandatory!'

    const isValid = /^.*(?=.{8,})(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!*@#$%^&+=].*$)/i.test(value)

    if (!isValid) {
      return `password must be have 8 caracteres.<br>
              Include at least one number.<br>
              Include at least one lowercase letter.<br>
              Include at least one capital letter.<br>
              Include at least one special character.`
    }          
}