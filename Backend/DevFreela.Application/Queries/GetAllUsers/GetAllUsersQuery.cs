using DevFreela.Core.DTOs;
using MediatR;

namespace DevFreela.Application.Queries.GetAllUsers
{
    public class GetAllUsersQuery : IRequest<List<UserDTO>>
    {
    }
}
