using DevFreela.Core.DTOs;
using DevFreela.Core.Repositories;

namespace DevFreela.Infrastructure.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DevFreelaDbContext _dbContext;

        public UserRepository(DevFreelaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<UserDTO> GetByIdAsync(int id)
        {
            var user = await _dbContext.Users.FindAsync(id);

            if (user is null) return null;

            UserDTO userDTO = new UserDTO
            {
                FullName = user.FullName,
                Email = user.Email,
            };

            return userDTO;
        }
    }
}
