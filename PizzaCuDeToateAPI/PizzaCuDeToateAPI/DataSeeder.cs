using PizzaCuDeToateAPI.DataContexts;
using PizzaCuDeToateAPI.Models;

namespace PizzaCuDeToateAPI;

public static class DataSeeder
{
    public static void Seed(this IHost host)
    {
        using var scope = host.Services.CreateScope();
        using var context = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
        context.Database.EnsureCreated();
        AddCategories(context);
        AddItems(context);
    }

    private static void AddCategories(ApplicationContext context)
    {
        var cateogry = context.Categories.FirstOrDefault();
        if (cateogry != null) return;
        
        //add data
    }

    private static void AddItems(ApplicationContext context)
    {
        var fooditem = context.FoodItems.FirstOrDefault();
        if (fooditem != null) return;
        var stockitem = context.StockItems.FirstOrDefault();
        if (stockitem != null) return;
        
        //add data
    }
    
}