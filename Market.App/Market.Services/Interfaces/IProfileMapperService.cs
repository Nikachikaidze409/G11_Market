using System.Security.Claims;
using Market.DTO;

namespace Market.Services.Interfaces;

public interface IProfileMapperService
{
    UserDto MapClaimsToUser(IEnumerable<Claim> claims);
}
