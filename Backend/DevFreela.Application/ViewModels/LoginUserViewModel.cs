namespace DevFreela.Application.ViewModels
{
    public class LoginUserViewModel
    {
        public LoginUserViewModel(string name, string email, bool isAdmin, string token)
        {
            Name = name;
            Email = email;
            IsAdmin = isAdmin;
            Token = token;
        }

        public string Name { get; set; }
        public string Email { get; set; }
        public bool IsAdmin { get; set; }
        public string Token { get; set; }
    }
}
