using DevFreela.Core.Repositories;
using MediatR;

namespace DevFreela.Application.Commands.Project.UpdateProject
{
    public class UpdateProjectCommandHandler : IRequestHandler<UpdateProjectCommand, Unit>
    {
        private readonly IProjectRepository _projectRepository;

        public UpdateProjectCommandHandler(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task<Unit> Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
        {
            var project = await _projectRepository.GetByIdAsync(request.Id);
            project.Update(request.Title, request.Description, request.IdClient,
                           request.IdFreelancer, request.TotalCost);

            await _projectRepository.SaveChangesAsync();

            //retorno void
            return Unit.Value;
        }
    }
}
