using PhoneBook.Console.DataLayer;
using PhoneBook.Console.Views;
using Spectre.Console;

namespace PhoneBook.Console.Menu;

public class MainMenu
{
    private readonly string menuChoices = "1.Add a New Contact\n2.Edit Contacts\n3.View All Contacts\n4.Delete a Contact\n5.Exit the phone book";
    private readonly string menuHeader = "Main Menu";
    private readonly string prompt = "Please select from 1 of the menu choices";
    private readonly string errorMessage = "Invalid Menu Choice. Must be between 1 and 5";
    private const int minChoice = 1;
    private const int maxChoice = 5;
    private readonly PhoneContext phoneContext = new();

    public bool MainLoop()
    {
        while (true)
        {
            AnsiConsole.Clear();
            var panel = new Panel(menuChoices)
            {
                Header = new PanelHeader(menuHeader)
            };

            AnsiConsole.Write(panel);
            AnsiConsole.WriteLine();
            var choice = AnsiConsole.Prompt(
                new TextPrompt<int>(prompt)
                .PromptStyle("green")
                .ValidationErrorMessage($"[red]{errorMessage}[/]")
                .Validate(option =>
                {
                    return option switch
                    {
                        < minChoice => ValidationResult.Error(errorMessage),
                        > maxChoice => ValidationResult.Error(errorMessage),
                        _ => ValidationResult.Success(),
                    };
                }));

            switch (choice)
            {
                case 1:
                    AddContact();
                    break;
                case 2:
                    EditContacts();
                    break;
                case 3:
                    ViewAllContacts();
                    break;
                case 4:
                    DeleteContacts();
                    break;
                case 5:
                    break;
            }
            if (choice == 5)
                break;
        }
        return true;
    }

    private void DeleteContacts()
    {
        DeleteContact _contact = new(phoneContext);
        _contact.Delete();
    }

    private void ViewAllContacts()
    {
        ViewContacts contact = new(phoneContext);
        contact.DisplayContacts();
    }

    private void EditContacts()
    {
        EditContacts contact = new(phoneContext);
        contact.Edit();
    }

    private void AddContact()
    {
        AddContacts contact = new(phoneContext);
        contact.CreateContact();
    }

    private void DisplayNotImplemented()
    {
        AnsiConsole.WriteLine();
        AnsiConsole.WriteLine("Not Yet Implemented. Please Check Back.");
        Thread.Sleep(500);
    }
}
