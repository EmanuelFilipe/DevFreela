using DevFreela.Application.Queries.GetAllSkills;
using DevFreela.Core.DTOs;
using DevFreela.Core.Repositories;
using Moq;

namespace DevFreela.UnitTests.Application.Queries
{
    public class GetAllSkillsQueryCommandHandlerTests
    {
        [Fact]
        public async Task SkillProjectOk_Executed_ReturnOneSkillProject()
        {
            var skills = new List<SkillDTO>();
            skills.Add(new SkillDTO { Id = 1, Description = "Test" });

            var skillRepositoryMock = new Mock<ISkillRepository>();
            skillRepositoryMock.Setup(sr => sr.GetAllAsync().Result).Returns(skills);

            var getAllSkillsQuery = new GetAllSkillsQuery();
            var getAllSkillsQueryHandler = new GetAllSkillsQueryHandler(skillRepositoryMock.Object);

            var skillsDTO = await getAllSkillsQueryHandler.Handle(getAllSkillsQuery, new CancellationToken());
            Assert.NotNull(skillsDTO);
            Assert.NotEmpty(skillsDTO);
            Assert.Equal(skills.Count(), skillsDTO.Count);
        }
    }
}
