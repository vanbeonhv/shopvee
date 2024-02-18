using Microsoft.EntityFrameworkCore;
using shopveeAPI.Models;

namespace shopveeAPI.DbContext;

public class ShopveeDbContext : Microsoft.EntityFrameworkCore.DbContext
{
    public ShopveeDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<User> User { get; set; } = null!;
}