export function validateEmptyAndEmail(value) {
    if (!value)
        return 'E-mail is mandatory!'

    const isValid = /^[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,}$/i.test(value)

    if (!isValid) {
        return 'E-mail field must be a valid e-mail!'
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

export function ValidatePasswordAndConfirmPassword(password, confirmPassword) {
    if (password != confirmPassword){
        return 'The Password and Confirm Password must be equal'
    }
}