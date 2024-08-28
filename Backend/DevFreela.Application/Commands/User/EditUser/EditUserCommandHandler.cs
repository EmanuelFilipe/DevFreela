using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using DevFreela.Core.Services;
using MediatR;

namespace DevFreela.Application.Commands.User.EditUser
{
    public class EditUserCommandHandler : IRequestHandler<EditUserCommand, Unit>
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuthService _authService;

        public EditUserCommandHandler(IUserRepository userRepository, IAuthService authService)
        {
            _userRepository = userRepository;
            _authService = authService;
        }

        public async Task<Unit> Handle(EditUserCommand request, CancellationToken cancellationToken)
        {
            var passwordHash = _authService.ComputeSha256Hash(request.Password);
            //var user = new Core.Entities.User(request.FullName, request.Email, request.BirthDate, passwordHash, request.Role);
            var user = await _userRepository.GetByIdAsync(request.Id);

            user.Update(request.Id, request.FullName, request.Email, request.BirthDate, request.Active);

            await _userRepository.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
