using DevFreela.Core.DTOs;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Infrastructure.Persistence.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly DevFreelaDbContext _dbContext;

        public ProjectRepository(DevFreelaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Project>> GetAllAsync()
        {
            return await _dbContext.Projects.ToListAsync();
        }

        public async Task<ProjectDetailDTO> GetByIdAsync(int id)
        {
            var project = await _dbContext.Projects
                                          .Include(p => p.Client)
                                          .Include(p => p.Freelancer)
                                          .SingleOrDefaultAsync(p => p.Id == id);

            if (project is null) return null;

            var projectDetailDTO = new ProjectDetailDTO
            {
                Id = project.Id,
                Title = project.Title,
                Description = project.Description,
                TotalCost = project.TotalCost,
                StartedAt = project.StartedAt,
                FinishedAt = project.FinishedAt,
                ClientFullName = project.Client.FullName,
                FreelancerFullName = project.Freelancer.FullName
            };

            return projectDetailDTO;
        }
    }
}
