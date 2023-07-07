using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using PizzaCuDeToateAPI.DataContexts;
using PizzaCuDeToateAPI.Models;

namespace PizzaCuDeToateAPI.Repositories.StockItemRepository;

public class StockRepository : IRepository<StockItem>, IStockRepository
{
    private readonly ApplicationContext Context;

    public StockRepository( ApplicationContext context )
    {
        Context = context;
    }

    public IEnumerable<StockItem> GetAll()
    {
        return Context.StockItems.Include(stockItem=>stockItem.Category).Include(stockItem => stockItem.Meals).ToList();
    }

    public StockItem? GetSingle(Expression<Func<StockItem, bool>> predicate)
    {
        return Context.StockItems.Where(predicate).Include(stockItem => stockItem.Meals).Include(category=>category.Category).FirstOrDefault();
    }

    public StockItem? AddSingle(StockItem elemToAdd)
    {
        try
        {
            Context.StockItems.Add(elemToAdd);
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
        foreach (var itemToRemove in Context.StockItems)
        {
            Context.StockItems.Remove(itemToRemove);
        }
        Context.SaveChanges();
    }

    public IEnumerable<StockItem>? DeleteSingle(StockItem elemToDelete)
    {
        try
        {
            Context.StockItems.Remove(elemToDelete);
            Context.SaveChanges();
            return Context.StockItems;
        }
        catch (Exception e)
        {
            return null;
        }
    }

    public bool UpdateSingle(StockItem oldElem,StockItem newElem)
    {
        try
        {
            oldElem.Name = newElem.Name;
            oldElem.IsIngredient = newElem.IsIngredient;
            oldElem.UnitPrice = newElem.UnitPrice;
            oldElem.QuantityPerUnit = newElem.QuantityPerUnit;
            oldElem.Logo = newElem.Logo;
            oldElem.UnitsInStock = newElem.UnitsInStock;
            Context.SaveChanges();
            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }
    
    public StockItem? AddMeal(StockItem stockItem, int mealIdToAdd)
    {
        var mealToAdd = Context.FoodItems.Where(stockItem => stockItem.Id == mealIdToAdd).FirstOrDefault();
        if (mealToAdd is null)
        {
            return null;
        }
        stockItem.Meals.Add(mealToAdd);
        Context.SaveChanges();
        return stockItem;
    }

    public StockItem? RemoveMeal(StockItem stockItem, int stockItemToRemove)
    {
        var mealToRemove = Context.FoodItems.Where(stockItem => stockItem.Id == stockItemToRemove).FirstOrDefault();
        if (mealToRemove is null)
        {
            return null;
        }
        stockItem.Meals.Remove(mealToRemove);
        Context.SaveChanges();
        return stockItem;
    }

    public StockItem? ChangeCategory(StockItem stockItem, int newCategoryId)
    {
        var newCategory = Context.Categories.Where(category => category.Id == newCategoryId).FirstOrDefault();
        if (newCategory is null)
        {
            return null;
        }
        stockItem.Category = newCategory;
        Context.SaveChanges();
        return stockItem;
    }
}