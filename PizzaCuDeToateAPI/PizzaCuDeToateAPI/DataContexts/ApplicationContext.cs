using Microsoft.EntityFrameworkCore;
using PizzaCuDeToateAPI.Models;

namespace PizzaCuDeToateAPI.DataContexts;

public class ApplicationContext : DbContext
{
    public DbSet<FoodItem> FoodItems { get; set; }

    public DbSet<StockItem> StockItems { get; set; }

    public DbSet<Category> Categories { get; set; }

    public DbSet<Image> Images { get; set; }

    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
    {
        
    }
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.UseSerialColumns();
    }
}