using PlayingSpectre.Controllers;
using PlayingSpectre.Models;
using PlayingSpectre.UserInterfaces;
using Spectre.Console;

namespace PlayingSpectre.Services;

public class OrderService
{
    internal static void NewOrder()
    {
        List<OrderProd> orderProd = new();

        var coffeeList = CoffeeController.GetAllCoffees();
        var order = new Order { CreatedDate = DateTime.Now };

        bool choosingProducts = true;
        while (choosingProducts)
        {
            var coffee = CoffeeInterface.CoffeeListMenu(coffeeList);
            Console.WriteLine(coffee);
            var productQuantity = AnsiConsole.Ask<int>("Product Quantity?: ");

            order.TotalAmount += coffee.Price * productQuantity;

            orderProd.Add(
                new OrderProd
                {
                    Order = order,
                    CoffeeId = coffee.CoffeeId,
                    ProductQuantity = productQuantity
                });

            choosingProducts = AnsiConsole.Confirm("Add more products? ");
        }
        //Thread.Sleep(800);
        OrderController.Add(orderProd);
        Console.Write("Press any Key to continue");
        Console.Read();

    }

    internal static void ViewAllOrders()
    {
        var orders = OrderController.GetOrders();
        OrderInterface.PrintOrderTable(orders);
    }

    internal static void ViewOrderById()
    {
        var orders = OrderController.GetOrders();
        var order = OrderInterface.OrderMenuPickable(orders);
        OrderInterface.ShowSingleOrderDetail(order);
        var orderDetailed = OrderController.GetOrderDetailedById(order.Id);
        OrderInterface.PrintOrderProdTable(orderDetailed);

        Console.Write("Press any Key to continue");
        Console.Read();

        /*var orderId = AnsiConsole.Ask<int>("Order Id?: ");
		var order = OrderController.GetOrderById(orderId);
		//OrderInterface.PrintOrderTable(orders);
		Console.WriteLine(order);
		Console.ReadLine();*/
    }

    internal static void RemoveOrder()
    {
        var orders = OrderController.GetOrders();
        var order = OrderInterface.OrderMenuPickable(orders);
        var status = OrderController.RemoveOrder(order);
        if (status > 0)
            Console.WriteLine("Order Succefully deleted");
        else
            Console.WriteLine("Something went Wrong");

        Console.Write("Press any Key to continue");
        Console.Read();
    }
}
