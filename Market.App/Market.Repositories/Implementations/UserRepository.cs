
using Market.Domain.Entities;
using Market.Repositories.Data;
using Market.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Market.Repositories.Implementations
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(MarketDbContext context) : base(context) { }

        public async Task<User> GetBy0IdAsync(string id)
        {
            return await _dbSet.FirstOrDefaultAsync(u => u.AuthId == id);
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            return await _dbSet.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User> GetUserWithRolesAsync(string userId)
        {
            return await _dbSet.Include(u => u.Role).FirstOrDefaultAsync(u => u.AuthId == userId);
        }
    }
}
