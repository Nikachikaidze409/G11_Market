using Microsoft.EntityFrameworkCore;

namespace Market.Repositories.Data;

public class MarketDbContext : DbContext
{
    public MarketDbContext(DbContextOptions<MarketDbContext> options) : base(options)
    {
    }
    public DbSet<Domain.Entities.User> Users { get; set; }
}