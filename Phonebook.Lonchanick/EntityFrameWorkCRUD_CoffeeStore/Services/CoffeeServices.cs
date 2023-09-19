
using PlayingSpectre.Controllers;
using PlayingSpectre.UserInterfaces;

namespace PlayingSpectre.Services;

internal class CoffeeServices
{
    internal static void AddCoffee()
    {
        var coffee = CoffeeInterface.AddCoffee();
        int status = CoffeeController.Add(coffee);
        CoffeeController.SuccefullOrUnsuccefullMessage(status, "Coffee successfully saved!", coffee);

    }

    internal static void RemoveCoffee()
    {
        var coffees = CoffeeController.GetAllCoffees();
        var coffee = CoffeeInterface.CoffeeListMenu(coffees);
        var status = CoffeeController.Remove(coffee);

        CoffeeController.SuccefullOrUnsuccefullMessage(status, "Coffee successfully deleted!", coffee);

    }
    internal static void UpdateCoffee()
    {
        var coffees = CoffeeController.GetAllCoffees();
        var coffee = CoffeeInterface.CoffeeListMenu(coffees);

        var upDatedCoffee = CoffeeInterface.UpdateInterface(coffee);
        var status = CoffeeController.Update(coffee);
        CoffeeController.SuccefullOrUnsuccefullMessage(status, "Coffee successfully Updated!", upDatedCoffee);

    }
    internal static void ViewAllCoffees()
    {
        var coffeList = CoffeeController.GetAllCoffees();
        CoffeeInterface.PrintCoffeeList(coffeList);
    }
    internal static void ViewCoffee()
    {
        var coffees = CoffeeController.GetAllCoffees();
        var coffee = CoffeeInterface.CoffeeListMenu(coffees);
        CoffeeInterface.ShowSingleCoffeeDetails(coffee);
    }
}
