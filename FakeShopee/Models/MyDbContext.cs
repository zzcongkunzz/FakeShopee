using Microsoft.EntityFrameworkCore;

namespace FakeShopee.Models;

public class MyDbContext : DbContext
{
    public MyDbContext (DbContextOptions options) : base(options) {}
    
    #region DbSet
    
    public DbSet<UserGroup> UserGroups { get; set; }
    public DbSet<Users> Users { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Cart> Carts { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderDetail> OrderDetails { get; set; }

    #endregion

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}