using System.Security.Claims;
using Market.DTO;

namespace Market.Services.Interfaces;

public interface IUserService
{
    Task<UserDto> SyncUserAsync(IEnumerable<Claim> claims);
    Task<UserDto> GetByAuth0IdAsync(string auth0Id);
    Task<bool> UpdateUserProfileAsync(UserDto userDto);
}