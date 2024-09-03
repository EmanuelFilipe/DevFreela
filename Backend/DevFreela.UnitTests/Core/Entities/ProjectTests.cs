using DevFreela.Core.Entities;
using DevFreela.Core.Enums;

namespace DevFreela.UnitTests.Core.Entities
{
    public class ProjectTests
    {
        [Fact]
        public void TestIfProjectStartWorks()
        {
            var project = new Project("Nome de teste", "Descrição de teste", 1, 2, 10000);

            Assert.Equal(ProjectStatusEnum.Created, project.Status);
            Assert.Null(project.StartedAt);

            Assert.NotNull(project.Title);
            Assert.NotEmpty(project.Title);

            Assert.NotNull(project.Description);
            Assert.NotEmpty(project.Description);

            project.Start();

            Assert.Equal(ProjectStatusEnum.InProgress, project.Status);
            Assert.NotNull(project.StartedAt);
        }

        [Fact]
        public void TestIfProjectCancelledWorks()
        {
            var project = new Project("Nome de teste", "Descrição de teste", 1, 2, 10000);

            Assert.Equal(ProjectStatusEnum.Created, project.Status);
            Assert.Null(project.StartedAt);

            Assert.NotNull(project.Title);
            Assert.NotEmpty(project.Title);

            Assert.NotNull(project.Description);
            Assert.NotEmpty(project.Description);

            project.Start();

            Assert.Equal(ProjectStatusEnum.InProgress, project.Status);
            Assert.NotNull(project.StartedAt);

            project.Cancel();
            Assert.Equal(ProjectStatusEnum.Cancelled, project.Status);
        }

        [Fact]
        public void TestIfStatusProjectChangeforPaymentPendingWorks()
        {
            var project = new Project("Projeto Teste", "Descrição do Projeto de Teste", 1, 1, 20000);

            Assert.Equal(ProjectStatusEnum.Created, project.Status);
            Assert.Null(project.StartedAt);

            Assert.NotNull(project.Title);
            Assert.NotEmpty(project.Title);

            Assert.NotNull(project.Description);
            Assert.NotEmpty(project.Description);

            Assert.True(project.IdClient > 0);
            Assert.True(project.IdFreelancer > 0);

            Assert.NotNull(project.Description);
            Assert.NotEmpty(project.Description);

            project.SetPaymentPending();

            Assert.Equal(ProjectStatusEnum.PaymentPending, project.Status);
            Assert.Null(project.FinishedAt);
        }

        [Fact]
        public void TestIfStatusProjectFinishWorks()
        {
            var project = new Project("Projeto Teste", "Descrição do Projeto de Teste", 1, 1, 20000);

            Assert.Equal(ProjectStatusEnum.Created, project.Status);
            Assert.Null(project.StartedAt);

            Assert.NotNull(project.Title);
            Assert.NotEmpty(project.Title);

            Assert.NotNull(project.Description);
            Assert.NotEmpty(project.Description);

            Assert.True(project.IdClient > 0);
            Assert.True(project.IdFreelancer > 0);

            Assert.NotNull(project.Description);
            Assert.NotEmpty(project.Description);

            project.SetPaymentPending();

            Assert.Equal(ProjectStatusEnum.PaymentPending, project.Status);
            Assert.Null(project.FinishedAt);

            project.Finish();

            Assert.Equal(ProjectStatusEnum.Finished, project.Status);
            Assert.NotNull(project.FinishedAt);
        }
    }
}
