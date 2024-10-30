using DevFreela.Core.DTOs;
using DevFreela.Core.Repositories;
using MediatR;

namespace DevFreela.Application.Queries.Projects.GetProjectsGrouped
{
    public class GetProjectsGroupedQueryHandler : IRequestHandler<GetProjectsGroupedQuery, List<ProjectsGroupedDTO>>
    {
        private readonly IProjectRepository _projectRepository;

        public GetProjectsGroupedQueryHandler(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task<List<ProjectsGroupedDTO>> Handle(GetProjectsGroupedQuery request, CancellationToken cancellationToken)
        {
            return await _projectRepository.GetProjectsGrouped();
        }
    }
}
