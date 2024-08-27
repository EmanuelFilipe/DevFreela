using DevFreela.Core.DTOs;
using DevFreela.Core.Entities;

namespace DevFreela.Core.Repositories
{
    public interface IUserRepository
    {
        Task<List<UserDTO>> GetAllAsync();
        Task<User> GetByIdAsync(int id);
        Task<User> GetUserByEmailAndPasswordAsync(string email, string passwordHash);
        Task AddAsync(User user);
        Task SaveChangesAsync();
    }
}
