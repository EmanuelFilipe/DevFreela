using DevFreela.Core.DTOs;
using DevFreela.Core.Repositories;
using MediatR;

namespace DevFreela.Application.Queries.GetProjectById
{
    public class GetProjectByIdQueryHandler : IRequestHandler<GetProjectByIdQuery, ProjectDetailDTO>
    {
        private readonly IProjectRepository _projectRepository;

        public GetProjectByIdQueryHandler(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task<ProjectDetailDTO> Handle(GetProjectByIdQuery request, CancellationToken cancellationToken)
        {
            var project = await _projectRepository.GetByIdAsync(request.Id);

            if (project is null) return null;

            var projectDetailDTO = new ProjectDetailDTO
            {
                Id = project.Id,
                Title = project.Title,
                Description = project.Description,
                TotalCost = project.TotalCost,
                StartedAt = project.StartedAt,
                FinishedAt = project.FinishedAt,
                ClientFullName = project?.Client?.FullName,
                FreelancerFullName = project?.Freelancer.FullName
            };

            return projectDetailDTO;
        }
    }
}
