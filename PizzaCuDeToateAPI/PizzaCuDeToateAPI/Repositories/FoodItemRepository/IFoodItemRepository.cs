using PizzaCuDeToateAPI.Models;

namespace PizzaCuDeToateAPI.Repositories.FoodItemRepository;

public interface IFoodItemRepository : IRepository<FoodItem>
{
    void AddImage(FoodItem foodItem, string imagePathToAdd);

    void RemoveImage(FoodItem foodItem, string imagePathToRemove);

    FoodItem? AddIngredient(FoodItem foodItem, int ingredientIdToAdd);

    FoodItem? RemoveIngredient(FoodItem foodItem, int ingredientIdToRemove);

    FoodItem? ChangeCategory(FoodItem foodItem, int newCategoryId);

}