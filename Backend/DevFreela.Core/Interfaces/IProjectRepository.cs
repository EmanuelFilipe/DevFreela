using DevFreela.Core.DTOs;
using DevFreela.Core.Entities;
using DevFreela.Core.Models;

namespace DevFreela.Core.Repositories
{
    public interface IProjectRepository
    {
        Task<PaginationResult<Project>> GetAllAsync(string query, int page = 1);
        Task<List<ProjectsGroupedDTO>> GetProjectsGrouped();
        Task<Project> GetByIdAsync(int id);
        Task AddAsync (Project project);
        Task AddCommentAsync(ProjectComment projectComment);
        Task StartAsync(Project project);
        Task SaveChangesAsync();
    }
}
