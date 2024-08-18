using DevFreela.Application.Commands.CreateComment;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using MediatR;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.UnitTests.Application.Commands
{
    public class CreateCommentCommandHandlerTests
    {
        [Fact]
        public async Task InputDataIsOk_Executed_CommentIsCreated()
        {
            //Arrange
            var projectRepositoryMock = new Mock<IProjectRepository>();

            var createCommentCommand = new CreateCommentCommand
            {
                Content = "Teste Comment",
                IdProject = 1,
                IdUser = 1
            };

            var createCommentCommandHandler = new CreateCommentCommandHandler(projectRepositoryMock.Object);

            //Act
            var result = await createCommentCommandHandler.Handle(createCommentCommand, new CancellationToken());

            //Assert
            projectRepositoryMock.Verify(pr => pr.AddCommentAsync(It.IsAny<ProjectComment>()), Times.Once);
           
        }
    }
}
