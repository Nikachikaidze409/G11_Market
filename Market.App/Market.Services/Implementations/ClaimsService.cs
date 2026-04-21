using System.Security.Claims;
using Market.Services.Interfaces;

namespace Market.Services.Implementations;

internal class ClaimsService : IClaimsService
{
    public string GetAuth0Id(IEnumerable<Claim> claims)
    {
        return claims.FirstOrDefault(c => c.Type == "sub")?.Value ?? string.Empty;
    }

    public string GetEmail(IEnumerable<Claim> claims)
    {
        return claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value ?? string.Empty;
    }

    public string GetName(IEnumerable<Claim> claims)
    {
        return claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value ?? string.Empty;
    }
}
