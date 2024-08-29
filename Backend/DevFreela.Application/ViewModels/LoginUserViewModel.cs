namespace DevFreela.Application.ViewModels
{
    public class LoginUserViewModel
    {
        public LoginUserViewModel(string name, string email, string token)
        {
            Name = name;
            Email = email;
            Token = token;
        }

        public string Name { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
    }
}
