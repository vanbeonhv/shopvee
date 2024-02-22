using Microsoft.EntityFrameworkCore;
using shopveeAPI.Models;

namespace shopveeAPI.DbContext;

public class ShopveeDbContext : Microsoft.EntityFrameworkCore.DbContext
{
    public ShopveeDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<User> User { get; set; } = null!;
    public DbSet<AddressEntity> Address { get; set; } = null!;
    public DbSet<AttachmentEntity> AttachmentEntity{ get; set; } = null!;
    public DbSet<CartEntity> CartEntity{ get; set; } = null!;
    public DbSet<CartItemEntity> CartItemEntity{ get; set; } = null!;
    public DbSet<PaymentMethodEntity> PaymentMethodEntity{ get; set; } = null!;
    public DbSet<ProductCategoryEntity> ProductCategoryEntity{ get; set; } = null!;
    public DbSet<ProductEntity> ProductEntity{ get; set; } = null!;
    public DbSet<ProductOptionEntity> ProductOptionEntity{ get; set; } = null!;
    public DbSet<ProductOptionValueEntity> ProductOptionValueEntity{ get; set; } = null!;
    public DbSet<ReviewEntity> ReviewEntity{ get; set; } = null!;
    public DbSet<ShopEntity> ShopEntity{ get; set; } = null!;
    public DbSet<UserOrderEntity> UserOrderEntity{ get; set; } = null!;
}