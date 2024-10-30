using DevFreela.Core.DTOs;
using DevFreela.Core.Repositories;
using MediatR;

namespace DevFreela.Application.Queries.User.GetUsersGrouped
{
    public class GetUsersGroupedQueryHandler : IRequestHandler<GetUsersGroupedQuery, List<UsersGroupedDTO>>
    {
        private readonly IUserRepository _userRepository;

        public GetUsersGroupedQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<List<UsersGroupedDTO>> Handle(GetUsersGroupedQuery request, CancellationToken cancellationToken)
        {
            var usersGrouped = await _userRepository.GetUsersGrouped();
            return usersGrouped;
        }
    }
}
