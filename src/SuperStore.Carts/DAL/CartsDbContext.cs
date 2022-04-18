using Microsoft.EntityFrameworkCore;
using SuperStore.Carts.DAL.Models;

namespace SuperStore.Carts.DAL;

public class CartsDbContext : DbContext
{
    public DbSet<CustomerFundsModel> CustomerFunds { get; set; }
    
    public CartsDbContext(DbContextOptions<CartsDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("SuperStore.Carts");
    }
}