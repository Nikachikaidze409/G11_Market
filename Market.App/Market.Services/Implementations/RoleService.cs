using Market.Domain.Enums;
using Market.Repositories.Data;
using Market.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Market.Services.Implementations;

public class RoleService : IRoleService
{
    private readonly MarketDbContext _context;
    private static readonly Dictionary<EmployeeType, Permission> RolePermissions = new()
    {
        { EmployeeType.Admin, Permission.Admin },
        { EmployeeType.Manager, Permission.Manager },
        { EmployeeType.Operator, Permission.User }
    };

    public RoleService(MarketDbContext context)
    {
        _context = context;
    }

    public async Task AssignRoleAsync(string userId, string role)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.AuthId == userId);
        if (user == null) throw new KeyNotFoundException("User not found");
        if (!Enum.TryParse<EmployeeType>(role, true, out var employeeType) || !RolePermissions.ContainsKey(employeeType)) throw new ArgumentException("Invalid role");
        user.Role = employeeType;
        await _context.SaveChangesAsync();
    }

    public async Task<Permission> GetPermissionsAsync(string userId)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.AuthId == userId);
        if (user is null) return Permission.None;
        return RolePermissions.TryGetValue(user.Role, out var permissions)? permissions : Permission.None;
    }
}
