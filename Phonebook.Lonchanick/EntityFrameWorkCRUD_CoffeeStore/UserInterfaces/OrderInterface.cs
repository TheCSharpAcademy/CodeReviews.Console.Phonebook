using PlayingSpectre.Models;
using Spectre.Console;

namespace PlayingSpectre.UserInterfaces;

public class OrderInterface
{
	internal static void PrintOrderTable(List<Order> orders)
	{
		var table = new Table();
		table.AddColumn("Id");
		table.AddColumn("Created Date");
		table.AddColumn("Total Order");

		foreach (var order in orders)
		{
			table.AddRow
				(order.Id.ToString(),
				order.CreatedDate.ToString("dd-MM-yyyy"),
				order.TotalAmount.ToString());
		}

		AnsiConsole.Write(table);
		Console.Write("Press any Key to continue");
		Console.Read();

	}

    internal static Order OrderMenuPickable(List<Order> orders)
    {
        var order = AnsiConsole.Prompt(new SelectionPrompt<Order>()
            .Title("Choose any Order")
            .AddChoices(orders));
        return order;
    }

    public static void ShowSingleOrderDetail(Order order)
    {
		var panel = new Panel(
$@"Id: {order.Id}
Init Date: {order.CreatedDate:d}
Total Amount: {order.TotalAmount}");

        panel.Header = new PanelHeader("Order Details");

        panel.Padding = new Padding(1, 1, 1, 1);

        AnsiConsole.Write(panel);
    }

    internal static void PrintOrderProdTable(Order order)
    {
        var table = new Table();
        table.AddColumn("Id");
        table.AddColumn("Product");
        table.AddColumn("Price");
        table.AddColumn("Quantity");

        foreach (var orderProd in order.OrderProd)
        {
            table.AddRow
                (orderProd.Id.ToString(),
                orderProd.Coffee.Name,
                orderProd.Coffee.Price.ToString(),
                orderProd.ProductQuantity.ToString());
        }

        AnsiConsole.Write(table);
        
    }


}
