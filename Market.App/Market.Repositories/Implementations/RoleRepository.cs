
using Market.Domain.Entities;
using Market.Repositories.Data;
using Market.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Market.DTO.Enums;

namespace Market.Repositories.Implementations
{
    public class RoleRepository : BaseRepository<Role>, IRoleRepository
    {
        public RoleRepository(MarketDbContext context) : base(context)
        {
        }

        public async Task<Role?> GetByNameAsync(string roleName)
        {
            return await _dbSet.FirstOrDefaultAsync(r => r.Name == roleName);
        }
    }
}
