using Auth0.OidcClient;
using Market.DTO;
using Market.Domain.Enums;
using Market.Services.Interfaces;

namespace Market.Services.Implementations;

public class AuthService : IAuthService
{
    private readonly Auth0Client _auth0Client;
    private readonly IUserService _userService;

    public AuthService(IUserService userService)
    {
        _userService = userService;
        _auth0Client = new Auth0Client(new Auth0ClientOptions
        {
            Domain = "dev-pl44tt6h1n4snyxw.us.auth0.com",
            ClientId = "D2m5EZxvnsD8g8hUZINgd76sZcKEuyOW",
            RedirectUri = "http://localhost:4242",
            PostLogoutRedirectUri = "http://localhost:4242"
        });
    }

    public async Task<AuthResultDto> LoginAsync()
    {
        var loginResult = await _auth0Client.LoginAsync(new { connection = "email" });

        if (loginResult.IsError)
        {
            return new AuthResultDto
            {
                Success = false,
                Message = loginResult.Error
            };
        }

        var internalUser = await _userService.SyncUserAsync(loginResult.User.Claims);
        bool isPrivileged = internalUser.Role == EmployeeType.Admin || internalUser.Role == EmployeeType.Manager;

        if (isPrivileged)
        {
            return new AuthResultDto
            {
                Success = true,
                Email = internalUser.Email,
                AuthId = internalUser.AuthId,
                Token = loginResult.IdentityToken,
                Message = "Login Successful",
                RequiresPassword = false
            };
        }
        return new AuthResultDto
        {
            Success = true,
            Email = internalUser.Email,
            AuthId = internalUser.AuthId,
            Token = loginResult.IdentityToken,
            Message = "Password is required",
            RequiresPassword = true
        };
    }

    public async Task LogoutAsync()
    {
        await _auth0Client.LogoutAsync();
    }
}