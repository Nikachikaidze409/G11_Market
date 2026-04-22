using Market.Domain.Enums;

namespace Market.Services.Interfaces;

public interface IRoleService
{
    Task AssignRoleAsync(string userId, string role);
    Task<Permission> GetPermissionsAsync(string userId);
}