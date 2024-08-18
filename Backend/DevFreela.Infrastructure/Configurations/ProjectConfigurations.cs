using DevFreela.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevFreela.Infrastructure.Configurations
{
    public class ProjectConfigurations : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> builder)
        {
            builder.ToTable("Projects");
            builder.HasKey(p => p.Id);

            builder.HasOne(p => p.Freelancer)
                      .WithMany(f => f.FreelanceProjects)
                      .HasForeignKey(p => p.IdFreelancer)
                      .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(p => p.Client)
                      .WithMany(f => f.OwnedProjects)
                      .HasForeignKey(p => p.IdClient)
                      .OnDelete(DeleteBehavior.Restrict);
            //impede que seja deletado um registro que tenha relacionamento com outras
        }
    }
}
