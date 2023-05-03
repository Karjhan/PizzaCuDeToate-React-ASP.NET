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
        return Context.FoodItems.Include(foodItem => foodItem.Ingredients).Include(foodItem => foodItem.Logo).ToList();
    }

    public FoodItem? GetSingle(Expression<Func<FoodItem, bool>> predicate)
    {
        return Context.FoodItems.Where(predicate).Include(foodItem => foodItem.Ingredients).Include(foodItem => foodItem.Logo).First();
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
            Context.FoodItems.Update(newElem);
            Context.SaveChanges();
            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }
}