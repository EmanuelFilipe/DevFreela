using MediatR;

namespace DevFreela.Application.Commands.User.EditUser
{
    public class EditUserCommand : IRequest<Unit>
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        //public string Password { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public bool Active { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
