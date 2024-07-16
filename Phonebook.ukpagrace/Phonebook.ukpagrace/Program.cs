using Phonebook.ukpagrace.Categories;
using Phonebook.ukpagrace.Controller;
using Spectre.Console;

class Program
{

    static void Main()
    {
        ContactController contactController = new ContactController();
        CategoryController categoryController = new CategoryController();
        categoryController.Create();
        do
        {
            Console.Clear();
            var option = Menu();
            switch (option)
            {
                case "Create":
                    contactController.Create();
                    break;
                case "Update":
                    contactController.Update();
                    break;
                case "List":
                    contactController.List();
                    break;
                case "Delete":
                    contactController.Delete();
                    break;
                case "Exit":
                    Environment.Exit(1);
                    break;
                default:
                    Console.WriteLine("Invalid Input");
                    break;
            }

        } while (EndApp());
    }

    static string Menu()
    {
        var option = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("What would you [green]like to do[/]?")
                .AddChoices(new[] {
                    "Create", "List", "Update", "Delete", "Exit"
                }));

        return option;
    }

    static bool EndApp()
    {
        if (!AnsiConsole.Confirm("Do you want to perform another operation?"))
        {
            AnsiConsole.MarkupLine("Ok... :(");
            return false;
        }

        return true;
    }
}
