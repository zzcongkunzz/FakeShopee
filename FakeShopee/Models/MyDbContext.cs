using Microsoft.EntityFrameworkCore;

namespace FakeShopee.Models;

public class MyDbContext : DbContext
{
    public MyDbContext (DbContextOptions options) : base(options) {}
    
    #region DbSet
    

    #endregion

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}