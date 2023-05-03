using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using PizzaCuDeToateAPI.Models;

namespace PizzaCuDeToateAPI.Repositories.FoodItemRepository;

public class FoodItemRepository : IRepository<FoodItem>, IFoodItemRepository
{
    private readonly DbContext Context;

    public FoodItemRepository( DbContext context )
    {
        Context = context;
    }

    public IEnumerable<FoodItem> GetAll()
    {
        throw new NotImplementedException();
    }

    public FoodItem? GetSingle(Expression<Func<FoodItem, bool>> predicate)
    {
        throw new NotImplementedException();
    }

    public FoodItem? AddSingle(FoodItem elemToAdd)
    {
        throw new NotImplementedException();
    }

    public void DeleteAll()
    {
        throw new NotImplementedException();
    }

    public IEnumerable<FoodItem> DeleteSingle(FoodItem elemToDelete)
    {
        throw new NotImplementedException();
    }

    public bool UpdateSingle(FoodItem newElem)
    {
        throw new NotImplementedException();
    }
}