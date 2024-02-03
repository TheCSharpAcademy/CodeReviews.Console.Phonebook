using Spectre.Console;
using Phonebook.frockett.Service_Layer;

namespace Phonebook.frockett.UI;

public class MenuHandler
{
    private readonly PhonebookService phonebookService;
    private readonly InputValidator validator;
    private readonly TableEngine tableEngine;

    public MenuHandler(PhonebookService phonebookService, InputValidator inputValidator, TableEngine tableEngine)
    {
        this.phonebookService = phonebookService;
        validator = inputValidator;
        this.tableEngine = tableEngine;
    }

    public void ShowMainMenu()
    {
        AnsiConsole.Clear();

        string[] menuOptions =
                {"View Contacts", "Add/Delete Groups", "Exit Program"};

        string choice = AnsiConsole.Prompt(
                            new SelectionPrompt<string>()
                            .Title("Which operation would you like to perform? Use [green]arrow[/] and [green]enter[/] keys to make a selection.")
                            .PageSize(10)
                            .MoreChoicesText("Keep scrolling for more options")
                            .AddChoices(menuOptions));

        int menuSelection = Array.IndexOf(menuOptions, choice) + 1;

        switch (menuSelection)
        {
            case 1:
                HandleContactsMenu();
                break;
            case 2:
                HandleGroupMenu();
                break;
            case 3:
                Environment.Exit(0);
                break;
        }
    }

    private void HandleContactsMenu()
    {
        AnsiConsole.Clear();

        string[] menuOptions =
                {"View Contacts", "View Group of Contacts", "Add New Contact", "Return to Main Menu"};

        string choice = AnsiConsole.Prompt(
                            new SelectionPrompt<string>()
                            .Title("Which operation would you like to perform? Use [green]arrow[/] and [green]enter[/] keys to make a selection.")
                            .PageSize(10)
                            .MoreChoicesText("Keep scrolling for more options")
                            .AddChoices(menuOptions));

        int menuSelection = Array.IndexOf(menuOptions, choice) + 1;

        switch (menuSelection)
        {
            case 1:
                // TODO print all contacts
                break;
            case 2:
                // TODO print groups, let user select group
                break;
            case 3:
                AddContact();
                break;
            case 4:
                ShowMainMenu();
                break;
        }
    }

    private void HandleGroupMenu()
    {
        AnsiConsole.Clear();

        string[] menuOptions =
                {"Add group", "Delete Group", "Exit Program"};

        string choice = AnsiConsole.Prompt(
                            new SelectionPrompt<string>()
                            .Title("Which operation would you like to perform? Use [green]arrow[/] and [green]enter[/] keys to make a selection.")
                            .PageSize(10)
                            .MoreChoicesText("Keep scrolling for more options")
                            .AddChoices(menuOptions));

        int menuSelection = Array.IndexOf(menuOptions, choice) + 1;

        switch (menuSelection)
        {
            case 1:
                HandleContactsMenu();
                break;
            case 2:
                HandleGroupMenu();
                break;
            case 3:
                Environment.Exit(0);
                break;
        }
    }
}
