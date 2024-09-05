using DevFreela.Application.Queries.User.GetUser;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using Moq;

namespace DevFreela.UnitTests.Application.Queries
{
    public class GetUserByIdCommandHandlerTests
    {
        [Fact]
        public async Task UserOk_Executed_ReturnUserId()
        {
            var user = new User("User teste", "teste@teste.com", DateTime.Now, "", "client");

            var userRepositoryMock = new Mock<IUserRepository>();
            userRepositoryMock.Setup(u => u.GetByIdAsync(0).Result).Returns(user);

            var getUserByIdQuery = new GetUserQuery(0);
            var getUserByIdCommandHandler = new GetUserQueryHandler(userRepositoryMock.Object);

            var result =  getUserByIdCommandHandler.Handle(getUserByIdQuery, new CancellationToken());
            Assert.NotNull(result);

        }
    }
}
