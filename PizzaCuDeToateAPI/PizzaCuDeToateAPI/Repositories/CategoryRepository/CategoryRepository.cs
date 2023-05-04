using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using PizzaCuDeToateAPI.DataContexts;
using PizzaCuDeToateAPI.Models;

namespace PizzaCuDeToateAPI.Repositories.CategoryRepository;

public class CategoryRepository : IRepository<Category>, ICategoryRepository
{
    private readonly ApplicationContext Context;

    public CategoryRepository( ApplicationContext context )
    {
        Context = context;
    }

    public IEnumerable<Category> GetAll()
    {
        return Context.Categories.ToList();
    }

    public Category? GetSingle(Expression<Func<Category, bool>> predicate)
    {
        return Context.Categories.Where(predicate).FirstOrDefault();
    }

    public Category? AddSingle(Category elemToAdd)
    {
        try
        {
            Context.Categories.Add(elemToAdd);
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
        foreach (var itemToRemove in Context.Categories)
        {
            Context.Categories.Remove(itemToRemove);
        }
        Context.SaveChanges();
    }

    public IEnumerable<Category>? DeleteSingle(Category elemToDelete)
    {
        try
        {
            Context.Categories.Remove(elemToDelete);
            Context.SaveChanges();
            return Context.Categories;
        }
        catch (Exception e)
        {
            return null;
        }
    }

    public bool UpdateSingle(Category oldElem, Category newElem)
    {
        try
        {
            oldElem.Name = newElem.Name;
            oldElem.Description = newElem.Description;
            oldElem.Logo = oldElem.Logo;
            Context.SaveChanges();
            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }
}