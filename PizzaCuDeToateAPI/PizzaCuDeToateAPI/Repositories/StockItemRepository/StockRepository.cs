using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using PizzaCuDeToateAPI.Models;

namespace PizzaCuDeToateAPI.Repositories.StockItemRepository;

public class StockRepository : IRepository<StockItem>, IStockRepository
{
    private readonly DbContext Context;

    public StockRepository( DbContext context )
    {
        Context = context;
    }

    public IEnumerable<StockItem> GetAll()
    {
        throw new NotImplementedException();
    }

    public StockItem? GetSingle(Expression<Func<StockItem, bool>> predicate)
    {
        throw new NotImplementedException();
    }

    public StockItem? AddSingle(StockItem elemToAdd)
    {
        throw new NotImplementedException();
    }

    public void DeleteAll()
    {
        throw new NotImplementedException();
    }

    public IEnumerable<StockItem> DeleteSingle(StockItem elemToDelete)
    {
        throw new NotImplementedException();
    }

    public bool UpdateSingle(StockItem newElem)
    {
        throw new NotImplementedException();
    }
}