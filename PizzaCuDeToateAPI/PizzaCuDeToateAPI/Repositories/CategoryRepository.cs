using PizzaCuDeToateAPI.Models;

namespace PizzaCuDeToateAPI.Repositories;

public class CategoryRepository : IRepository<Category>
{
    public IEnumerable<Category> GetAll()
    {
        throw new NotImplementedException();
    }

    public Category? GetSingle(int id)
    {
        throw new NotImplementedException();
    }

    public Category? AddSingle(Category elemToAdd)
    {
        throw new NotImplementedException();
    }

    public void DeleteAll()
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Category> DeleteSingle(int id)
    {
        throw new NotImplementedException();
    }

    public Category? UpdateSingle(Category newElem)
    {
        throw new NotImplementedException();
    }
}