using DevFreela.Application.Queries.Projects.GetAllProjects;
using DevFreela.Core.Entities;
using DevFreela.Core.Models;
using DevFreela.Core.Repositories;
using Moq;

namespace DevFreela.UnitTests.Application.Queries
{
    public class GetAllProjectsCommandHandlerTests
    {
        [Fact]
        public async Task ThreeProjectsExist_Executed_ReturnThreeProjectViewModels()
        {
            //Arrange
            var projects = new PaginationResult<Project>
            {
                Data = new List<Project>
                {
                    new Project("Nome do Teste 1", "Descricao do Teste 1", 1 , 2, 10000),
                    new Project("Nome do Teste 2", "Descricao do Teste 2", 1 , 2, 20000),
                    new Project("Nome do Teste 2", "Descricao do Teste 3", 1 , 2, 30000)
                }
            };

            // ao chamar o GetAllAsync irá retornar a lista de projects
            var projectRepositoryMock = new Mock<IProjectRepository>();
            //It.IsAny<string>() = passa uma string qualquer
            projectRepositoryMock.Setup(pr => pr.GetAllAsync(It.IsAny<string>(), It.IsAny<int>()).Result).Returns(projects);

            var getAllProjectsQuery = new GetAllProjectsQuery { Query = "", Page = 1};
            var getAllProjectsQueryHandler = new GetAllProjectsQueryHandler(projectRepositoryMock.Object);

            //Act
            var paginationProjectViewModelList = await getAllProjectsQueryHandler.Handle(getAllProjectsQuery, new CancellationToken());

            //Assert
            Assert.NotNull(paginationProjectViewModelList);
            Assert.NotEmpty(paginationProjectViewModelList.Data);
            Assert.Equal(paginationProjectViewModelList.Data.Count, 
                         paginationProjectViewModelList.Data.Count);

            // verificar se o método GetAllAsync foi chamado somente uma vez
            projectRepositoryMock.Verify(pr => pr.GetAllAsync(It.IsAny<string>(), It.IsAny<int>()).Result, Times.Once);
        }
    }
}
