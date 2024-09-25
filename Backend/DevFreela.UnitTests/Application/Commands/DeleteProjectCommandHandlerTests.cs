using DevFreela.Application.Commands.Project.DeleteProject;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.UnitTests.Application.Commands
{
    public class DeleteProjectCommandHandlerTests
    {
        [Fact]
        public async Task InputDataIsOk_Executed_ReturnUnitValue()
        {
            //Arrange
            var project = new Project("Nome do Teste 1", "Descricao do Teste 1", 1, 2, 10000);
            
            var projectRepositoryMock = new Mock<IProjectRepository>();
            projectRepositoryMock.Setup(d => d.GetByIdAsync(project.Id).Result).Returns(project);
            
            var deleteProjectCommand = new DeleteProjectCommand(project.Id);
            var deleteProjectCommandHandler = new DeleteProjectCommandHandler(projectRepositoryMock.Object);

            //Act
            var result = await deleteProjectCommandHandler.Handle(deleteProjectCommand, new CancellationToken());

            //Assert
            projectRepositoryMock.Verify(d => d.GetByIdAsync(project.Id), Times.Once());
            //projectRepositoryMock.Verify(d => d.GetByIdAsync(1).Result.Cancel(), Times.Once());
        }
    }
}
