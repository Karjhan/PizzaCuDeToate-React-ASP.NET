using Microsoft.EntityFrameworkCore;
using PizzaCuDeToateAPI.Models;

namespace PizzaCuDeToateAPI.Repositories.FoodItemRepository;

public class FoodItemRepository : Repository<FoodItem>, IFoodItemRepository
{
    public FoodItemRepository(DbContext context) : base(context)
    {
    }
}