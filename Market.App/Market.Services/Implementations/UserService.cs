using System.Security.Claims;
using Market.Domain.Entities;
using Market.DTO;
using Market.Domain.Enums;
using Market.Repositories.Data;
using Market.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Market.Services.Implementations;

public class UserService : IUserService
{
    private readonly MarketDbContext _context;
    private readonly IClaimsService _claimService;
    private readonly IProfileMapperService _profileMapperService;
    private readonly IRoleService _roleService;

    public UserService(MarketDbContext context, IClaimsService claimService, IProfileMapperService profileMapperService, IRoleService roleService)
    {
        _context = context;
        _claimService = claimService;
        _profileMapperService = profileMapperService;
        _roleService = roleService;
    }

    public async Task<UserDto?> GetByAuth0IdAsync(string auth0Id)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.AuthId == auth0Id);
        if (user == null) return null;
        return new UserDto
        {
            AuthId = user.AuthId,
            Email = user.Email,
            DisplayName = user.UserName,
            Role = user.Role,
            Permissions = await _roleService.GetPermissionsAsync(user.AuthId)
        };
    }

    public async Task<UserDto> SyncUserAsync(IEnumerable<Claim> claims)
    {
        var auth0Id = _claimService.GetAuth0Id(claims);
        if (string.IsNullOrEmpty(auth0Id)) throw new UnauthorizedAccessException("No Auth0 ID found in claims.");
        var email = _claimService.GetEmail(claims);
        var user = await _context.Users.FirstOrDefaultAsync(u => u.AuthId == auth0Id);

        if (user == null)
        {
            var newUserDto = _profileMapperService.MapClaimsToUser(claims);
            user = new User
            {
                AuthId = auth0Id,
                Email = email,
                UserName = newUserDto.DisplayName,
                Role = EmployeeType.Operator,
                CreatedAt = DateTime.UtcNow
            };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }
        var permissions = await _roleService.GetPermissionsAsync(user.AuthId);
        return new UserDto
        {
            AuthId = user.AuthId,
            Email = user.Email,
            DisplayName = user.UserName,
            Role = user.Role,
            Permissions = permissions
        };
    }

    public async Task<bool> UpdateUserProfileAsync(UserDto userDto)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.AuthId == userDto.AuthId);
        if (user == null) return false;
        user.Email = userDto.Email;
        user.UserName = userDto.DisplayName;
        await _context.SaveChangesAsync();
        return true;
    }
}