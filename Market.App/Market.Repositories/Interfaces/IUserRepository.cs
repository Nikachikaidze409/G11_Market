using System;
using System.Collections.Generic;
using System.Text;
using Market.Domain.Entities;

namespace Market.Repositories.Interfaces
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<User> GetBy0IdAsync(string id);
        Task<User> GetByEmailAsync(string email);
        Task<User> GetUserWithRolesAsync(string userId);
    }
}

