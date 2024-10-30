using DevFreela.Application.Queries.Projects.GetProjectById;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using Moq;

namespace DevFreela.UnitTests.Application.Queries
{
    public class GetProjectByIdCommandHandlerTests
    {
        [Fact]
        public async Task ProjectOk_Executed_ReturnOneProjectById()
        {
            var project = new Project("Nome do Teste 1", "Descricao do Teste 1", 1, 2, 10000);

            var projectRepositoryMock = new Mock<IProjectRepository>();
            projectRepositoryMock.Setup(p => p.GetByIdAsync(0).Result).Returns(project);

            var getProjectByIdQuery = new GetProjectByIdQuery(0);
            var getProjectByIdCommandHandler = new GetProjectByIdQueryHandler(projectRepositoryMock.Object);

            var projectResult = getProjectByIdCommandHandler.Handle(getProjectByIdQuery, new CancellationToken());
            Assert.NotNull(projectResult);
        }
    }
}
