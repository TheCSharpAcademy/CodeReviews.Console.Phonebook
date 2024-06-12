using Spectre.Console;

namespace Phonebook.UI;

public class Menu
{
    public string CancelOperation { get; private set; } = $"[maroon]Go Back[/]";

    public string[] MainMenu = ["View All Contacts", "Add Contact", "Update Contact", "Delete Contact", "Exit"];
    
    internal string GetMainMenu()
    {
        return AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Please Select An Action From The Options Below")
                    .PageSize(10)
                    .AddChoices(MainMenu)
        );
    }

}