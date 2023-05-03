using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace PizzaCuDeToateAPI.Repositories;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
{
    protected readonly DbContext Context;

    public Repository( DbContext context )
    {
        Context = context;
    }
    
    public IEnumerable<TEntity> GetAll()
    {
        return Context.Set<TEntity>().ToList();
    }

    public TEntity? GetSingle(Expression<Func<TEntity, bool>> predicate)
    {
        return (TEntity?)Context.Set<TEntity>().Where(predicate);
    }

    public TEntity? AddSingle(TEntity elemToAdd)
    {
        Context.Set<TEntity>().Add(elemToAdd);
        Context.SaveChanges();
        return elemToAdd;
    }

    public void DeleteAll()
    {
        foreach (var itemToRemove in Context.Set<TEntity>())
        {
            Context.Set<TEntity>().Remove(itemToRemove);
        }
        Context.SaveChanges();
    }

    public IEnumerable<TEntity> DeleteSingle(TEntity elemToDelete)
    {
        Context.Set<TEntity>().Remove(elemToDelete);
        Context.SaveChanges();
        return Context.Set<TEntity>();
    }

    public bool UpdateSingle(TEntity newElem)
    {
        try
        {
            Context.Set<TEntity>().Update(newElem);
            Context.SaveChanges();
            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }
}