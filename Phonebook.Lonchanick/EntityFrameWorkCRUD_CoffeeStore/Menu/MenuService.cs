using PlayingSpectre.Services;
using Spectre.Console;

namespace PlayingSpectre.Menu;

public class Main
{
    internal static void MainMenu()
    {
        AnsiConsole.Status()
            .Start("Loading...", ctx =>
            {
                ctx.SpinnerStyle(Style.Parse("green"));
                Thread.Sleep(500);
            });

        bool AppIsRunningYet = true;
        while (AppIsRunningYet)
        {
            AnsiConsole.Write(new FigletText("Main Menu!").LeftJustified().Color(Color.Blue));
            var options = new SelectionPrompt<MainMenuOptions>();
            options.AddChoices
                (
                    MainMenuOptions.Manage_Categories,
                    MainMenuOptions.Manage_Coffees,
                    MainMenuOptions.Manage_Orders,
                    MainMenuOptions.QUIT
                );
            var r = AnsiConsole.Prompt(options);

            switch (r)
            {
                case MainMenuOptions.Manage_Categories:
                    CategoryMenu();
                    Console.Clear();
                    break;

                case MainMenuOptions.Manage_Coffees:
                    CoffeeMenu();
                    Console.Clear();
                    break;
                case MainMenuOptions.Manage_Orders:
                    OrderMenu();
                    Console.Clear();
                    break;

                case MainMenuOptions.QUIT:
                    AppIsRunningYet = false;
                    break;
            }
        }

    }

    internal static void CategoryMenu()
    {
        Console.Clear();
        bool AppIsRunningYet = true;
        while (AppIsRunningYet)
        {
            AnsiConsole.Write(new FigletText("Category Menu!").LeftJustified().Color(Color.Blue));
            var options = new SelectionPrompt<CategoryOptions>();
            options.AddChoices
                (
                    CategoryOptions.ADD_Category,
                    CategoryOptions.VIEWALL_Categories,
                    CategoryOptions.VIEW_Category,
                    CategoryOptions.DELETE_Category,
                    CategoryOptions.BACK
                );

            var r = AnsiConsole.Prompt(options);
            switch (r)
            {
                case CategoryOptions.ADD_Category:
                    CategoryServices.AddCategory();
                    Console.Clear();
                    break;

                case CategoryOptions.VIEW_Category:
                    CategoryServices.GetCategory();
                    Console.Clear();
                    break;

                case CategoryOptions.VIEWALL_Categories:
                    CategoryServices.ShowCategories();
                    Console.Clear();
                    break;

                case CategoryOptions.DELETE_Category:
                    CategoryServices.DeleteCategory();
                    Console.Clear();
                    break;

                case CategoryOptions.BACK:
                    AppIsRunningYet = false;
                    break;
            }
        }
    }

    internal static void CoffeeMenu()
    {
        Console.Clear();
        bool AppIsRunningYet = true;
        while (AppIsRunningYet)
        {
            AnsiConsole.Write(new FigletText("Coffee Menu!").LeftJustified().Color(Color.Blue));
            var options = new SelectionPrompt<CoffeeOptions>();
            options.AddChoices
                (
                    CoffeeOptions.ADD_Coffee,
                    CoffeeOptions.VIEW_Coffee,
                    CoffeeOptions.VIEWALL_Coffee,
                    CoffeeOptions.DELETE_Coffee,
                    CoffeeOptions.UPDATE_Coffee,
                    CoffeeOptions.BACK
                );
            var r = AnsiConsole.Prompt(options);

            switch (r)
            {
                case CoffeeOptions.ADD_Coffee:
                    CoffeeServices.AddCoffee();
                    Console.Clear();
                    break;

                case CoffeeOptions.VIEW_Coffee:
                    CoffeeServices.ViewCoffee();
                    Console.Clear();
                    break;

                case CoffeeOptions.VIEWALL_Coffee:
                    CoffeeServices.ViewAllCoffees();
                    Console.Clear();
                    break;

                case CoffeeOptions.DELETE_Coffee:
                    CoffeeServices.RemoveCoffee();
                    Console.Clear();
                    break;

                case CoffeeOptions.UPDATE_Coffee:
                    CoffeeServices.UpdateCoffee();
                    Console.Clear();
                    break;

                case CoffeeOptions.BACK:
                    AppIsRunningYet = false;
                    break;
            }
        }
    }

    private static void OrderMenu()
    {
        Console.Clear();
        bool AppIsRunningYet = true;
        while (AppIsRunningYet)
        {
            AnsiConsole.Write(new FigletText("Orders Menu!").LeftJustified().Color(Color.Blue));
            var options = new SelectionPrompt<OrderOptions>();
            options.AddChoices
                (
                    OrderOptions.NewOrder,
                    OrderOptions.ViewOrder_Details,
                    OrderOptions.ViewAllOrders,
                    OrderOptions.DeleteOrder,
                    OrderOptions.BACK
                );
            var r = AnsiConsole.Prompt(options);

            switch (r)
            {
                case OrderOptions.NewOrder:
                    OrderService.NewOrder();
                    Console.Clear();
                    break;

                case OrderOptions.ViewOrder_Details:
                    OrderService.ViewOrderById();
                    Console.Clear();
                    break;

                case OrderOptions.ViewAllOrders:
                    OrderService.ViewAllOrders();
                    Console.Clear();
                    break;

                case OrderOptions.DeleteOrder:
                    OrderService.RemoveOrder();
                    Console.Clear();
                    break;

                case OrderOptions.BACK:
                    AppIsRunningYet = false;
                    break;
            }
        }
    }


}
