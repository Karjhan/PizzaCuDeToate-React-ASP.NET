using PizzaCuDeToateAPI.Models;

namespace PizzaCuDeToateAPI.Repositories.StockItemRepository;

public interface IStockRepository : IRepository<StockItem>
{
    StockItem? AddMeal(StockItem stockItem, int mealIdToAdd);

    StockItem? RemoveMeal(StockItem stockItem, int mealIdToRemove);

    StockItem? ChangeCategory(StockItem stockItem, int newCategoryId);
}