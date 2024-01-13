using shopveeAPI.Entities;

namespace shopveeAPI.DbContext;

using Microsoft.EntityFrameworkCore;

public class ShopveeDbContext : DbContext
{
    public ShopveeDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<User> User { get; set; } = null!;
}