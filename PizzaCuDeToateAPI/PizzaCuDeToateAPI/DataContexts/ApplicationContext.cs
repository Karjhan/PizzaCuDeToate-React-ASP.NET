using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PizzaCuDeToateAPI.Models;

namespace PizzaCuDeToateAPI.DataContexts;

public class ApplicationContext : IdentityDbContext<ApplicationUser>
{
    public DbSet<FoodItem> FoodItems { get; set; }

    public DbSet<StockItem> StockItems { get; set; }

    public DbSet<Category> Categories { get; set; }

    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
    {
        
    }
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.UseSerialColumns();
        SeedRoles(builder);
    }

    private void SeedRoles(ModelBuilder builder)
    {
        builder.Entity<IdentityRole>().HasData(
            new IdentityRole() { Id = "1", Name = "Admin", ConcurrencyStamp = "1", NormalizedName = "ADMIN" },
            new IdentityRole() { Id = "2", Name = "User", ConcurrencyStamp = "2", NormalizedName = "USER" }
        );
    }
}