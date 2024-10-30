using Dapper;
using DevFreela.Core.DTOs;
using DevFreela.Core.Entities;
using DevFreela.Core.Models;
using DevFreela.Core.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DevFreela.Infrastructure.Persistence.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly DevFreelaDbContext _dbContext;
        private readonly string _connectionString;
        private const int PAGE_SIZE = 4;

        public ProjectRepository(DevFreelaDbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _connectionString = configuration.GetConnectionString("DevFreelaCs");
        }


        public async Task<PaginationResult<Project>> GetAllAsync(string query, int page)
        {
            IQueryable<Project> projects = _dbContext.Projects
                                                     .Include(p => p.Client)
                                                     .Include(p => p.Freelancer);

            if (!string.IsNullOrWhiteSpace(query))
            {
                projects = projects.Where(p => p.Title.Contains(query) || p.Description.Contains(query));
            }

            return await projects.GetPaged(page, PAGE_SIZE);
        }

        public async Task<Project> GetByIdAsync(int id)
        {
            var project = await _dbContext.Projects
                                          .Include(p => p.Client)
                                          .Include(p => p.Freelancer)
                                          .SingleOrDefaultAsync(p => p.Id == id);
            return project;
        }

        public async Task AddAsync(Project project)
        {
            await _dbContext.Projects.AddAsync(project);
        }

        public async Task StartAsync(Project project)
        {
            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                sqlConnection.Open();
                var script = "UPDATE Projects SET Status = @status, StartedAt = @startedAt WHERE Id = @id";

                await sqlConnection.ExecuteAsync(script, new { status = project.Status, startedAt = project.StartedAt, project.Id });
            }
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public async Task AddCommentAsync(ProjectComment projectComment)
        {
            await _dbContext.ProjectComments.AddAsync(projectComment);
        }

        public async Task<List<ProjectsGroupedDTO>> GetProjectsGrouped()
        {
            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                sqlConnection.Open();
                var script = "  SELECT Count([Id]) as Quantity, " +
                             "  Case " +
                             "      WHEN Status = 0 then 'Created'" +
                             "      WHEN Status = 1 then 'InProgress'" +
                             "      WHEN Status = 2 then 'Suspended'" +
                             "      WHEN Status = 3 then 'Canceled'" +
                             "      WHEN Status = 4 then 'Finished'" +
                             "      WHEN Status = 5 then 'PaymentPending'" +
                             "  END as Status" +
                             "  FROM Projects" +
                             "  GROUP BY Status";

                var projectsGrouped = await sqlConnection.QueryAsync<ProjectsGroupedDTO>(script);
                return projectsGrouped.ToList();
            };
        }

    }
}
