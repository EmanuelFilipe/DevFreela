using DevFreela.Core.Entities;

namespace DevFreela.UnitTests.Core.Entities
{
    public class UserTests
    {
        [Fact]
        public void TestICreateActiveUserWorks()
        {
            var user = new User("User Teste", "teste@teste.com", DateTime.Now, "Aa12345!", "client");
            Assert.NotNull(user.FullName);
            Assert.NotEmpty(user.FullName);

            Assert.NotNull(user.Email);
            Assert.NotEmpty(user.Email);

            Assert.True(user.Active);
        }

        [Fact]
        public void TestIfDisableUserWorks()
        {
            var user = new User("User Teste", "teste@teste.com", DateTime.Now, "Aa12345!", "client");
            Assert.NotNull(user.FullName);
            Assert.NotEmpty(user.FullName);

            Assert.NotNull(user.Email);
            Assert.NotEmpty(user.Email);

            Assert.True(user.Active);

            user.Delete();
            Assert.True(!user.Active);
        }
    }
}
