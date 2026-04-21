using System.Security.Claims;
using Market.DTO;
using Market.Services.Interfaces;

namespace Market.Services.Implementations;

public class ProfileMapperService : IProfileMapperService
{
    private readonly IClaimsService _claimsService;

    public ProfileMapperService(IClaimsService claimsService)
    {
        _claimsService = claimsService;
    }

    public UserDto MapClaimsToUser(IEnumerable<Claim> claims)
    {
        if (claims == null || !claims.Any())
        {
            return new UserDto();
        }

        return new UserDto
        {
            AuthId = _claimsService.GetAuth0Id(claims),
            Email = _claimsService.GetEmail(claims),
            DisplayName = _claimsService.GetName(claims),
        };
    }
}
