using DevFreela.Application.Commands.CreateUser;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using DevFreela.Core.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.UnitTests.Application.Commands
{
    public class CreateUserCommandHandlerTests
    {
        [Fact]
        public async Task InputDataIsOk_Executed_ReturnUserId()
        {
            //Arrange
            var userRepositoryMock = new Mock<IUserRepository>();
            var authServiceMock = new Mock<IAuthService>();

            var createUserCommand = new CreateUserCommand
            {
                FullName = "Nome Usuario Teste",
                Email = "userteste@teste.com",
                BirthDate = DateTime.Now,
                Password = "Aa12345!",
                Role = "client"
            };

            var createUserCommandHandler = new CreateUserCommandHandler(userRepositoryMock.Object, authServiceMock.Object);

            //Act
            var id = await createUserCommandHandler.Handle(createUserCommand, new CancellationToken());

            //Assert
            Assert.True(id >= 0);
            authServiceMock.Verify(a => a.ComputeSha256Hash(It.IsAny<string>()), Times.Once);
            userRepositoryMock.Verify(a => a.AddAsync(It.IsAny<User>()), Times.Once);
        }
    }
}
