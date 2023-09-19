using Microsoft.EntityFrameworkCore;
using PlayingSpectre.Models;

namespace PlayingSpectre.Controllers;

public class OrderController
{
    internal static int Add(List<OrderProd> orders)
    {
        using var db = new CoffeeDBcontext();
        db.OrderProds.AddRange(orders);
        var status = db.SaveChanges();

        return status;
    }

    internal static Order GetOrderById(int id)
    {
        using var db = new CoffeeDBcontext();
        var order = db.Orders.FirstOrDefault(ord => ord.Id == id);

        return order;
    }

    internal static List<Order> GetOrders()
    {
        using var db = new CoffeeDBcontext();
        return db.Orders.ToList();
    }

    internal static int RemoveOrder(Order order)
    {
        using var contextDb = new CoffeeDBcontext();
        contextDb.Remove(order);
        return contextDb.SaveChanges();

    }

    internal static List<Order> GetOrdersDetailed()
    {
        using var db = new CoffeeDBcontext();

        var orderDetailed = db.Orders
            .Include(o => o.OrderProd)
            .ThenInclude(coffee => coffee.Coffee)
            .ThenInclude(cat => cat.Category)
            .ToList();

        return orderDetailed;
    }

    internal static Order? GetOrderDetailedById(int id)
    {
        using var db = new CoffeeDBcontext();
        var order = db.Orders
            .Include(op => op.OrderProd)
            .ThenInclude(p => p.Coffee)
            .FirstOrDefault(or => or.Id == id);    
        
        return order;
    }

}
