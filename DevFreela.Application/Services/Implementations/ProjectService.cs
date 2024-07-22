using Dapper;
using DevFreela.Application.InputModels;
using DevFreela.Application.Services.Interfaces;
using DevFreela.Application.ViewModels;
using DevFreela.Infrastructure.Persistence;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DevFreela.Application.Services.Implementations
{
    public class ProjectService : IProjectService
    {
        private readonly DevFreelaDbContext _dbContext;
        private readonly string _connectionString;

        public ProjectService(DevFreelaDbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _connectionString = configuration.GetConnectionString("DevFreelaCs");
        }

        public int Create(CreateProjectInputModel inputModel)
        {
            //_dbContext.Projects.Add();
            _dbContext.SaveChanges();
            return 0;
        }

        public void CreateComment(CreateCommentProjectInputModel inputModel)
        {
            //_dbContext.ProjectComments.Add();
            _dbContext.SaveChanges();
        }

        public void Delete(int id)
        {
            var project = _dbContext.Projects.Find(id);
            project.Cancel();
        }

        public void Finish(int id)
        {
            var project = _dbContext.Projects.Find(id);
            project.Finish();
        }

        public List<ProjectViewModel> GetAll(string query)
        {
            var projects = _dbContext.Projects;

            var projectsViewModel = projects.Select(p => new ProjectViewModel(p.Id, p.Title, p.CreatedAt))
                .ToList();

            return projectsViewModel;
        }

        public ProjectDetailsViewModel GetById(int id)
        {
            var project = _dbContext.Projects
                                    .Include(p => p.Client)
                                    .Include(p => p.Freelancer)
                                    .SingleOrDefault(p => p.Id == id);

            if (project is null) return null;

            var projectDetailsViewModel = new ProjectDetailsViewModel(
              project.Id,
              project.Title,
              project.Description,
              project.TotalCost,
              project.StartedAt,
              project.FinishedAt,
              project.Client.FullName,
              project.Freelancer.FullName);

            return projectDetailsViewModel;
        }

        public void Start(int id)
        {
            var project = _dbContext.Projects.Find(id);
            project.Start();
            //_dbContext.SaveChanges();

            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                sqlConnection.Open();
                var script = "UPDATE Projects SET Status = @status, StartedAt = @startedAt WHERE Id = @id";

                sqlConnection.Execute(script, new { status = project.Status, startedAt = project.StartedAt, id });
            }
        }

        public void Update(UpdateProjectInputModel inputModel)
        {
            var project = _dbContext.Projects.Find(inputModel.Id);
            project.Update(project.Title, project.Description, project.TotalCost);

        }
    }
}
