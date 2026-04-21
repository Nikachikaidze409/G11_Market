using Market.Repositories.Data;
using Market.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Market.Services.Implementations
{
    internal class PasswordService : IPasswordService
    {
        private readonly MarketDbContext _context;

        public PasswordService(MarketDbContext context)
        {
            _context = context;
        }

        public async Task SetPasswordAsync(string authId, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.AuthId == authId);
            if (user == null) throw new Exception("User not found");
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(password);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> VerifyPasswordAsync(string authId, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.AuthId == authId);
            if (user == null || string.IsNullOrEmpty(user.PasswordHash)) return false;
            return BCrypt.Net.BCrypt.Verify(password, user.PasswordHash);
        }
    }
}
