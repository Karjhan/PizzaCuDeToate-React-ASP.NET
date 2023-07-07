using System;
using System.Collections.Generic;
using System.Linq;
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
            // var ordersByPerson = Context.Orders.Where((order) => order.LastName == elemToAdd.LastName && order.FirstName == elemToAdd.FirstName);
            // if (ordersByPerson.Count() == 0)
            // {
            //     Context.Orders.Add(elemToAdd);
            //     Context.SaveChanges();
            //     return elemToAdd;
            // }
            // var lastDate = ordersByPerson.Max(order => order.OrderPlacedTime);
            // if ((DateTime.Now - lastDate).TotalSeconds > 30)
            // {
            //     Context.Orders.Add(elemToAdd);
            //     Context.SaveChanges();
            //     return elemToAdd;
            // }
            // return ordersByPerson.FirstOrDefault((order) => order.OrderPlacedTime == lastDate);
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