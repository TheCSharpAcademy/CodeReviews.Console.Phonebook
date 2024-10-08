using DataAccess;
using Spectre.Console;

namespace PhoneBook;
internal class View
{
    public static void DisplayMainMenu(out int userInput)
    {
        userInput = 0;
        Console.WriteLine();
        Console.WriteLine("Select an operation: ");

        Dictionary<string, int> choices = new Dictionary<string, int>
        {
            { "Exit", 0 },
            { "Create Contact", 1 },
            { "Retrieve Contact", 2},
            { "Update Contact", 3 },
            { "Delete Contact", 4 }
        };
        string choice = SelectionPrompt.Selection(choices);
        userInput = choices[choice];
    }
    public static void Check(bool openApp)
    {
        using (MyDbContext db = new MyDbContext())
        {
            if (!db.Database.CanConnect()) AnsiConsole.Write(new Markup("\n[red]No Database Found[/\n"));
            else
            {
                AnsiConsole.Write(new Markup("\n[red]Database Exist[/]\n"));
                while (openApp)
                {
                    DisplayMainMenu(out int userInput);
                    Logic.Do(userInput, out bool openApp2);
                    openApp = openApp2;
                }
            }
        }
    }
}
