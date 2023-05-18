using System.Linq.Expressions;
using PizzaCuDeToateAPI.DataContexts;
using PizzaCuDeToateAPI.Models;
using Z.EntityFramework.Plus;

namespace PizzaCuDeToateAPI.Repositories;

public class OrderRepository: IOrderRepository
{
    private readonly ApplicationContext Context;
    
    public OrderRepository( ApplicationContext context )
    {
        Context = context;
    }
    public IEnumerable<Order> GetAll()
    {
        return Context.Orders.ToList();
    }

    public Order? GetSingle(Expression<Func<Order, bool>> predicate)
    {
        return Context.Orders.Where(predicate).First();
    }

    public Order? AddSingle(Order elemToAdd)
    {
        try
        {
            Context.Orders.Add(elemToAdd);
            Context.SaveChanges();
            return elemToAdd;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return null;
        }
    }

    public void DeleteAll()
    {
        foreach (var order in Context.Orders)
        {
            Context.Orders.Remove(order);
        }

        Context.SaveChanges();
    }

    public IEnumerable<Order>? DeleteSingle(Order elemToDelete)
    {
        try
        {
            Context.Orders.Remove(elemToDelete);
            Context.SaveChanges();
            return Context.Orders.ToList();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return null;
        }
    }

    public bool UpdateSingle(Order oldElem, Order newElem)
    {
        return false;
    }
}