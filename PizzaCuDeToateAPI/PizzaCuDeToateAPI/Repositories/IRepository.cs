namespace PizzaCuDeToateAPI.Repositories;

public interface IRepository<T>
{
    public IEnumerable<T> GetAll();

    public T? GetSingle(int id);

    public T? AddSingle(T elemToAdd);

    public void DeleteAll();

    public IEnumerable<T> DeleteSingle(int id);

    public T? UpdateSingle(T newElem);
}