using Market.DTO;

namespace Market.Services.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResultDto> LoginAsync();
        Task LogoutAsync();
    }
}