using DevFreela.Application.ViewModels;
using DevFreela.Core.Models;
using DevFreela.Core.Repositories;
using MediatR;

namespace DevFreela.Application.Queries.Projects.GetAllProjects
{
    public class GetAllProjectsQueryHandler : IRequestHandler<GetAllProjectsQuery, PaginationResult<ProjectViewModel>>
    {
        private readonly IProjectRepository _projectRepository;
        public GetAllProjectsQueryHandler(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task<PaginationResult<ProjectViewModel>> Handle(GetAllProjectsQuery request, CancellationToken cancellationToken)
        {
            var projectPaginationResult = await _projectRepository.GetAllAsync(request.Query, request.Page);

            var projectsViewModel = projectPaginationResult
                .Data
                .Select(p => new ProjectViewModel(p.Id, p.Title, p.Client?.Id ?? 0, p.Client?.FullName ?? string.Empty,
                                                  p.Freelancer?.Id ?? 0, p.Freelancer?.FullName ?? string.Empty,
                                                  p.Status.ToString(), p.CreatedAt))
                .ToList();

            var paginationResult = new PaginationResult<ProjectViewModel>(
               projectPaginationResult.Page,
               projectPaginationResult.TotalPages,
               projectPaginationResult.PageSize,
               projectPaginationResult.ItemsCount,
               projectsViewModel
            );

            return paginationResult;
        }
    }
}
