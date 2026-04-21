using System.Security.Claims;

namespace Market.Services.Interfaces;

public interface IClaimsService
{
    string GetAuth0Id(IEnumerable<Claim> claims);
    string GetEmail(IEnumerable<Claim> claims);
    string GetName(IEnumerable<Claim> claims);
}
