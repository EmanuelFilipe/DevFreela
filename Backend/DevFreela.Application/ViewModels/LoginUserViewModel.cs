namespace DevFreela.Application.ViewModels
{
    public class LoginUserViewModel
    {
        public LoginUserViewModel(int id, string name, string email, bool isAdmin, string token, string role)
        {
            Id = id;
            Name = name;
            Email = email;
            IsAdmin = isAdmin;
            Token = token;
            Role = role;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public bool IsAdmin { get; set; }
        public string Token { get; set; }
        public string Role { get; set; }
    }
}
