using PizzaCuDeToateAPI.Models;

namespace PizzaCuDeToateAPI.Repositories;

public class StockItemRepository : IRepository<StockItem>
{
    public IEnumerable<StockItem> GetAll()
    {
        throw new NotImplementedException();
    }

    public StockItem? GetSingle(int id)
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

    public IEnumerable<StockItem> DeleteSingle(int id)
    {
        throw new NotImplementedException();
    }

    public StockItem? UpdateSingle(StockItem newElem)
    {
        throw new NotImplementedException();
    }
}