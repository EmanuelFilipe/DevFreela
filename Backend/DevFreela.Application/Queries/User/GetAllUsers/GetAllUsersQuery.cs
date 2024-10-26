using DevFreela.Core.DTOs;
using MediatR;

namespace DevFreela.Application.Queries.User.GetAllUsers
{
    public class GetAllUsersQuery : IRequest<List<UserDTO>>
    {
        public string Email { get; set; }
    }
}
