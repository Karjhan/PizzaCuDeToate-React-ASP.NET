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
        return Context.StockItems.Include(stockItem => stockItem.Meals).ToList();
    }

    public StockItem? GetSingle(Expression<Func<StockItem, bool>> predicate)
    {
        return Context.StockItems.Where(predicate).Include(stockItem => stockItem.Meals).First();
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

    public bool UpdateSingle(StockItem newElem)
    {
        try
        {
            Context.StockItems.Update(newElem);
            Context.SaveChanges();
            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }
}