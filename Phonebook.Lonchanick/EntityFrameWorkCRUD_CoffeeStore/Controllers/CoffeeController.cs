using Microsoft.EntityFrameworkCore;
using PlayingSpectre.Models;
using PlayingSpectre.UserInterfaces;

namespace PlayingSpectre.Controllers;

internal class CoffeeController
{
    internal static int Add(Product coffee)
    {
        using var dbRepository = new CoffeeDBcontext();
        dbRepository.Add(coffee);
        var status = dbRepository.SaveChanges();

        return status;

    }

    internal static int Remove(Product coffee)
    {
        using var contextDb = new CoffeeDBcontext();
        contextDb.Remove(coffee);
        return contextDb.SaveChanges();

    }

    internal static List<Product> GetAllCoffees()
    {
        using var dbContext = new CoffeeDBcontext();
        var coffees = dbContext.Coffees
            .Include(c => c.Category)
            .ToList();
        return coffees;
    }

    internal static int Update(Product coffee)
    {
        using var dbContext = new CoffeeDBcontext();
        dbContext.Update(coffee);
        return dbContext.SaveChanges();

    }
    internal static Product GetCoffeeById(int id)
    {
        using var db = new CoffeeDBcontext();
        var coffee = db.Coffees.First(x => x.CoffeeId == id);
        return coffee;

    }

    public static void SuccefullOrUnsuccefullMessage(int status, string message, Product coffee)
    {
        if (status > 0)
        {
            CoffeeInterface.ShowSingleCoffeeDetails(coffee, message, true);
        }
        else
        {
            Console.WriteLine("Something went wrong! ");
        }
    }

}
