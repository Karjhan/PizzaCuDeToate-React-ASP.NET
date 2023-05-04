using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using PizzaCuDeToateAPI.DataContexts;
using PizzaCuDeToateAPI.Models;
using Z.EntityFramework.Plus;

namespace PizzaCuDeToateAPI.Repositories.FoodItemRepository;

public class FoodItemRepository : IRepository<FoodItem>, IFoodItemRepository
{
    private readonly ApplicationContext Context;

    public FoodItemRepository( ApplicationContext context )
    {
        Context = context;
    }

    public IEnumerable<FoodItem> GetAll()
    {
        return Context.FoodItems.Include(foodItem => foodItem.Ingredients).Include(foodItem => foodItem.Category).ToList();
    }

    public FoodItem? GetSingle(Expression<Func<FoodItem, bool>> predicate)
    {
        return Context.FoodItems.Where(predicate).Include(foodItem => foodItem.Ingredients).Include(foodItem => foodItem.Category).FirstOrDefault();
    }

    public FoodItem? AddSingle(FoodItem elemToAdd)
    {
        try
        {
            Context.FoodItems.Add(elemToAdd);
            Context.SaveChanges();
            return elemToAdd;
        }
        catch (Exception e)
        {
            return null;
        }
    }

    public void DeleteAll()
    {
        foreach (var itemToRemove in Context.FoodItems)
        {
            Context.FoodItems.Remove(itemToRemove);
        }
        Context.SaveChanges();
    }

    public IEnumerable<FoodItem>? DeleteSingle(FoodItem elemToDelete)
    {
        try
        {
            Context.FoodItems.Remove(elemToDelete);
            Context.SaveChanges();
            return Context.FoodItems;
        }
        catch (Exception e)
        {
            return null;
        }
    }

    public bool UpdateSingle(FoodItem oldElem,FoodItem newElem)
    {
        try
        {
            oldElem.Name = newElem.Name;
            oldElem.Description = newElem.Description;
            oldElem.Category = newElem.Category;
            oldElem.UnitPrice = newElem.UnitPrice;
            oldElem.Ingredients = newElem.Ingredients;
            oldElem.Images = newElem.Images;
            oldElem.Logo = newElem.Logo;
            Context.SaveChanges();
            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }

    public void AddImage(FoodItem foodItem, string imagePathToAdd)
    {
        if (!foodItem.Images.Contains(imagePathToAdd))
        {
            foodItem.Images.Add(imagePathToAdd);
        }
        Context.SaveChanges();
    }

    public void RemoveImage(FoodItem foodItem, string imagePathToRemove)
    {
        if (foodItem.Images.Contains(imagePathToRemove))
        {
            foodItem.Images.Remove(imagePathToRemove);
        }
        Context.SaveChanges();
    }

    public FoodItem? AddIngredient(FoodItem foodItem, int ingredientIdToAdd)
    {
        var ingredientToAdd = Context.StockItems.Where(stockItem => stockItem.Id == ingredientIdToAdd).FirstOrDefault();
        if (ingredientToAdd is null || !ingredientToAdd.IsIngredient)
        {
            return null;
        }
        foodItem.Ingredients.Add(ingredientToAdd);
        Context.SaveChanges();
        return foodItem;
    }

    public FoodItem? RemoveIngredient(FoodItem foodItem, int ingredientIdToRemove)
    {
        var ingredientToRemove = Context.StockItems.Where(stockItem => stockItem.Id == ingredientIdToRemove).FirstOrDefault();
        if (ingredientToRemove is null || !ingredientToRemove.IsIngredient)
        {
            return null;
        }
        foodItem.Ingredients.Remove(ingredientToRemove);
        Context.SaveChanges();
        return foodItem;
    }

    public FoodItem? ChangeCategory(FoodItem foodItem, int newCategoryId)
    {
        var newCategory = Context.Categories.Where(category => category.Id == newCategoryId).FirstOrDefault();
        if (newCategory is null)
        {
            return null;
        }
        foodItem.Category = newCategory;
        Context.SaveChanges();
        return foodItem;
    }
}