using Dapper;
using DevFreela.Application.ViewModels;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace DevFreela.Application.Queries.GetAllSkills
{
    public class GetAllSkillsQueryHandler : IRequestHandler<GetAllSkillsQuery, List<SkillViewModel>>
    {
        private readonly string _connectionString;

        public GetAllSkillsQueryHandler(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DevFreelaCs");
        }


        public async Task<List<SkillViewModel>> Handle(GetAllSkillsQuery request, CancellationToken cancellationToken)
        {
            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                sqlConnection.Open();
                var script = "SELECT ID, DESCRIPTION FROM SKILLS";
                var skills = await sqlConnection.QueryAsync<SkillViewModel>(script);
                
                return skills.ToList();

                //var skills = _dbContext.Skills;
                //var skillsViewModel = skills.Select(s => new SkillViewModel(s.Id, s.Description)).ToList();
                //return skillsViewModel;
            }
        }
    }
}
