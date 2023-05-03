using Microsoft.EntityFrameworkCore;
using PizzaCuDeToateAPI.Models;

namespace PizzaCuDeToateAPI.Repositories.StockItemRepository;

public class StockRepository : Repository<StockItem>, IStockRepository
{
    public StockRepository(DbContext context) : base(context)
    {
    }
}