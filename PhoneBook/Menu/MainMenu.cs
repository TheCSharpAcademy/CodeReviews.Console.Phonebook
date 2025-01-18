using Spectre.Console;

internal class MainMenu
{
    private static readonly Dictionary<string, Action> _menuActions = new()
    {
        { "Show all contacts", ContactRead.ShowAllContacts },
        { "Add new contact", ContactCreate.Create },
        { "Update contact", ContactUpdate.Update },
        { "Delete contact", ContactDelete.Delete },
        { "[yellow]Create random contacts[/]", ContactMockClass.Generate },
        { "[red]Delete all contacts[/]", ContactMockClass.DeleteAll },
        { "Exit", () =>
            {
                Console.Clear();
                AnsiConsole.MarkupLine("[yellow]Goodbye![/]");
                Environment.Exit(0);
            }
        }
    };

    internal static void ShowMainMenu()
    {
        while (true)
        {
            var choice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                .Title("Choose an action: ")
                .PageSize(10)
                .AddChoices(_menuActions.Keys));
            _menuActions[choice]();
        }
    }
}
