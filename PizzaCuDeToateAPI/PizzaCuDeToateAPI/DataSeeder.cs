using PizzaCuDeToateAPI.DataContexts;

namespace PizzaCuDeToateAPI;

public static class DataSeeder
{
    public static void Seed(this IHost host)
    {
        using var scope = host.Services.CreateScope();
        using var context = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
        context.Database.EnsureCreated();
        AddCategories(context);
        AddStockItems(context);
        AddFoodItems(context);
    }

    private static void AddCategories(ApplicationContext context)
    {
        var movie = context.Categories.FirstOrDefault();
        if (movie != null) return;
        
        //add data
    }

    private static void AddFoodItems(ApplicationContext context)
    {
        var movie = context.FoodItems.FirstOrDefault();
        if (movie != null) return;
        
        //add data
    }

    private static void AddStockItems(ApplicationContext context)
    {
        var movie = context.StockItems.FirstOrDefault();
        if (movie != null) return;
        
        //add data
    }
}