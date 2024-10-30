using DevFreela.Core.DTOs;
using MediatR;

namespace DevFreela.Application.Queries.Projects.GetProjectsGrouped
{
    public class GetProjectsGroupedQuery : IRequest<List<ProjectsGroupedDTO>>
    {
    }
}
