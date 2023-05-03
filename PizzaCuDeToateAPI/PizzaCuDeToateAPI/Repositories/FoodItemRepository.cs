using PizzaCuDeToateAPI.Models;

namespace PizzaCuDeToateAPI.Repositories;

public class FoodItemRepository : IRepository<FoodItem>
{
    public IEnumerable<FoodItem> GetAll()
    {
        throw new NotImplementedException();
    }

    public FoodItem? GetSingle(int id)
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

    public IEnumerable<FoodItem> DeleteSingle(int id)
    {
        throw new NotImplementedException();
    }

    public FoodItem? UpdateSingle(FoodItem newElem)
    {
        throw new NotImplementedException();
    }
}