using DevFreela.Application.InputModels;
using DevFreela.Application.Services.Interfaces;
using DevFreela.Application.ViewModels;
using DevFreela.Infrastructure.Persistence;

namespace DevFreela.Application.Services.Implementations
{
    public class ProjectService : IProjectService
    {
        private readonly DevFreelaDbContext _dbContext;

        public ProjectService(DevFreelaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public int Create(CreateProjectInputModel inputModel)
        {
            //_dbContext.Projects.Add();
            return 0;
        }

        public void CreateComment(CreateCommentProjectInputModel inputModel)
        {
            //_dbContext.ProjectComments.Add();
        }

        public void Delete(int id)
        {
            var project = _dbContext.Projects.Find(p => p.Id == id);
            project.Cancel();
        }

        public void Finish(int id)
        {
            var project = _dbContext.Projects.Find(p => p.Id == id);
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
            var project = _dbContext.Projects.Find(p => p.Id == id);

            if (project is null) return null;

            var projectDetailsViewModel = new ProjectDetailsViewModel(
              project.Id,
              project.Title,
              project.Description,
              project.TotalCost,
              project.StartedAt,
              project.FinishedAt);

            return projectDetailsViewModel;
        }

        public void Start(int id)
        {
            var project = _dbContext.Projects.Find(p => p.Id == id);
            project.Start();
        }

        public void Update(UpdateProjectInputModel inputModel)
        {
            var project = _dbContext.Projects.Find(p => p.Id == inputModel.Id);
            project.Update(project.Title, project.Description, project.TotalCost);

        }
    }
}
