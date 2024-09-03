using DevFreela.Core.Repositories;
using Microsoft.EntityFrameworkCore.Storage;

namespace DevFreela.Infrastructure.Persistence
{
    public class UnityOfWork : IUnitOfWork
    {
        private readonly DevFreelaDbContext _dbContext;
        private IDbContextTransaction _dbTransaction;

        public IProjectRepository Projects { get; }

        public IUserRepository Users { get; }

        public ISkillRepository Skills { get; }

        public UnityOfWork(DevFreelaDbContext dbContext, IProjectRepository projects, 
                           IUserRepository users, ISkillRepository skills)
        {
            _dbContext = dbContext;
            Projects = projects;
            Users = users;
            Skills = skills;
        }

        public async Task<int> CompleteAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing) _dbContext.Dispose();
        }

        public async Task BeginTransactionAsync()
        {
            _dbTransaction = await _dbContext.Database.BeginTransactionAsync();
        }

        public async Task CommitAsync()
        {
            try
            {
                await _dbTransaction.CommitAsync();
            }
            catch (Exception ex)
            {
                await _dbTransaction.RollbackAsync();
                throw ex;
            }
        }
    }
}
