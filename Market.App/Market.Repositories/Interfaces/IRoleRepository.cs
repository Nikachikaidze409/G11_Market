
using Market.Domain.Entities;

namespace Market.Repositories.Interfaces;

public interface IRoleRepository
{
    Task<Role?> GetByNameAsync(string roleName);
}