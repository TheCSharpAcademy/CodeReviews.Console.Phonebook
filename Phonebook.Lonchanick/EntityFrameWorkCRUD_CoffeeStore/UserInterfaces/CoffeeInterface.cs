using PlayingSpectre.Controllers;
using PlayingSpectre.Models;
using Spectre.Console;

namespace PlayingSpectre.UserInterfaces;

internal class CoffeeInterface
{
	//prints a table with all coffees available
	internal static void PrintCoffeeList(List<Product> coffees)
	{
		var table = new Table();
		table.AddColumn("Id");
		table.AddColumn("Name");
		table.AddColumn("Price");
		table.AddColumn("Category");
		table.AddColumn("Coffee of the Month?");

		foreach (var coffe in coffees)
		{
			table.AddRow
				(coffe.CoffeeId.ToString(),
				coffe.Name,
				coffe.Price.ToString(),
				coffe.Category.categoryName,
				coffe.IsCoffeeOfTheMonth.ToString());
		}

		AnsiConsole.Write(table);
		Console.Write("Press any Key to continue");
		Console.ReadLine();

	}
	//present a complete list of coffees as a pickable menu option
	public static Product CoffeeListMenu(List<Product> coffees)
	{
		var option = AnsiConsole.Prompt(new SelectionPrompt<Product>()
			.Title("Choose any Coffee")
			.AddChoices(coffees));

		return option;
	}

	//shows a single coffe details in a pretty way
	public static void ShowSingleCoffeeDetails(Product coffee, string optional = "", bool newCoffee = false)
	{
		//si newCoffee es true se usan las instrucciones para imprimir solo el 
		//id de la categoria que se le asigno al cafe nuevo
		if(newCoffee) 
		{
		var panel = new Panel($@"Id: {coffee.CoffeeId}
Name: {coffee.Name}
Price: {coffee.Price}
Category: {coffee.CategoryId}
Is Coffee of the month?: {coffee.IsCoffeeOfTheMonth}");

			if (optional == "")
				panel.Header = new PanelHeader("Coffee Details");
			else
				panel.Header = new PanelHeader(optional);

			panel.Padding = new Padding(1, 1, 1, 1);
			AnsiConsole.Write(panel);
			Console.WriteLine("Press any key to continue ...");
			Console.ReadLine();
		}
		else
		{
			var panel = new Panel($@"Id: {coffee.CoffeeId}
Name: {coffee.Name}
Price: {coffee.Price}
Category: {coffee.Category.categoryName}
Is Coffee of the month?: {coffee.IsCoffeeOfTheMonth}");

			if (optional == "")
				panel.Header = new PanelHeader("Coffee Details");
			else
				panel.Header = new PanelHeader(optional);

			panel.Padding = new Padding(1, 1, 1, 1);
			AnsiConsole.Write(panel);
			Console.WriteLine("Press any key to continue ...");
			Console.ReadLine();
		}

	}

	public static Product UpdateInterface(Product coffeeToUpdate)
	{
		coffeeToUpdate.Name = AnsiConsole.Confirm("Wanna Update name? ")
			? AnsiConsole.Ask<string>("Enter new name: ")
			: coffeeToUpdate.Name;

		coffeeToUpdate.Price = AnsiConsole.Confirm("Wanna Update Price? ")
			? AnsiConsole.Ask<decimal>("Enter new Price: ")
			: coffeeToUpdate.Price;


		coffeeToUpdate.IsCoffeeOfTheMonth = AnsiConsole.Confirm("Wanna Update isCoffeeOfTheMonth? ")
			? AnsiConsole.Ask<bool>("Enter True or False: ")
			: coffeeToUpdate.IsCoffeeOfTheMonth;


		coffeeToUpdate.CategoryId = AnsiConsole.Confirm("Wanna Update Category? ")
			? CategoryInterface.CategoryMenuPickable().categoryId
			: coffeeToUpdate.CategoryId;

		return coffeeToUpdate;
	}

	public static Product AddCoffee()
	{
		Product coffee = new()
		{
			Name = AnsiConsole.Ask<string>("Name: "),
			Price = AnsiConsole.Ask<decimal>("Price: "),
			IsCoffeeOfTheMonth = AnsiConsole.Confirm("Is Coffe Of the Month?")
			? true
			: false
		};
		//coffee.CategoryId = 1;
		var category = CategoryInterface.CategoryMenuPickable();
		coffee.CategoryId = category.categoryId;
		//coffee.Category = category;

		return coffee;
	}

	

}
