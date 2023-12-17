using ConsoleTableExt;
using Spectre.Console;

namespace PhoneBook.UgniusFalze;

enum MenuOptions
{
    ViewContacts,
    AddContact
}

public class Menu
{
    private ContactsController _contactsController { get; set; }
    
    public Menu()
    {
        _contactsController = new ContactsController();
    }
    
    public void Start()
    {
        var option = AnsiConsole.Prompt(
            new SelectionPrompt<MenuOptions>()
                .Title("What would you like to do?")
                .AddChoices(
                    MenuOptions.AddContact,
                    MenuOptions.ViewContacts));
        switch (option)
        {
            case MenuOptions.ViewContacts:
                DisplayContacts(_contactsController.GetContacts());
                break;
        }

    }

    private void DisplayContacts(List<Contact> contacts)
    {
        ConsoleTableBuilder
            .From(contacts)
            .WithColumn("Id", "Name", "Email", "Number")
            .ExportAndWriteLine();
    }
}