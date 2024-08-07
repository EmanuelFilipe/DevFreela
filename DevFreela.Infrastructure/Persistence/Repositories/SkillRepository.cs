﻿using DevFreela.Core.DTOs;
using DevFreela.Core.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Dapper;

namespace DevFreela.Infrastructure.Persistence.Repositories
{
    public class SkillRepository : ISkillRepository
    {
        private readonly string _connectionString;

        public SkillRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DevFreelaCs");
        }

        public async Task<List<SkillDTO>> GetAllAsync()
        {
            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                sqlConnection.Open();
                var script = "SELECT ID, DESCRIPTION FROM SKILLS";
                var skills = await sqlConnection.QueryAsync<SkillDTO>(script);

                return skills.ToList();

                //var skills = _dbContext.Skills;
                //var skillsViewModel = skills.Select(s => new SkillViewModel(s.Id, s.Description)).ToList();
                //return skillsViewModel;
            }
        }
    }
}
