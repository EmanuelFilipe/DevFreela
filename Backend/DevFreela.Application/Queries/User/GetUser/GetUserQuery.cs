using DevFreela.Core.DTOs;
using MediatR;

namespace DevFreela.Application.Queries.User.GetUser
{
    public class GetUserQuery : IRequest<UserDTO>
    {
        public GetUserQuery(int id)
        {
            Id = id;
        }

        public int Id { get; private set; }
    }
}
