using DevFreela.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Infrastructure.Persistence.Repositories
{
    public class DataService : IDataService
    {
        private readonly DevFreelaDbContext _dbContext;

        public DataService(DevFreelaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void InitializeDB()
        {
            _dbContext.Database.Migrate();
        }
    }
}
