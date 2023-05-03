using System.Linq.Expressions;

namespace PizzaCuDeToateAPI.Repositories;

public interface IRepository<TEntity> where TEntity : class
{
    IEnumerable<TEntity> GetAll();

    TEntity? GetSingle(Expression<Func<TEntity, bool>> predicate);

    TEntity? AddSingle(TEntity elemToAdd);

    void DeleteAll();

    IEnumerable<TEntity>? DeleteSingle(TEntity elemToDelete);

    bool UpdateSingle(TEntity newElem);
}