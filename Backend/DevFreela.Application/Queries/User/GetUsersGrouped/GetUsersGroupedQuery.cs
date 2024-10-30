using DevFreela.Core.DTOs;
using MediatR;

namespace DevFreela.Application.Queries.User.GetUsersGrouped
{
    public class GetUsersGroupedQuery : IRequest<List<UsersGroupedDTO>>
    {
    }
}
