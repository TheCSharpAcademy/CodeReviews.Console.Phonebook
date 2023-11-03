using Spectre.Console;

namespace Phonebook.K_MYR;

internal class UserInterface
{
    private readonly ContactsController _contactsController;

    public UserInterface(ContactsController contactsController)
    {
        _contactsController = contactsController;
    }

    internal void ShowMainMenu()
    {
        var option = AnsiConsole.Prompt(new SelectionPrompt<MenuOption>()
                                            .AddChoices(Enum.GetValues(typeof(MenuOption)).Cast<MenuOption>())
                                            .Title("MainMenu"));

        switch(option)
        {
            case MenuOption.AddContact:
                _contactsController.AddContact();
                break;
            case MenuOption.UpdateContact:
                _contactsController.UpdateContact();
                break;
            case MenuOption.DeleteContact:
                _contactsController.DeleteContact();
                break;
            case MenuOption.ViewAllContacts:
                _contactsController.ViewAllContacts();
                break;
            case MenuOption.ViewContact:
                _contactsController.ViewContact();
                break;
            case MenuOption.Exit:
                Environment.Exit(1);
                break;    
        }
    }
}

enum MenuOption 
{
    AddContact,
    UpdateContact,
    DeleteContact,
    ViewAllContacts,
    ViewContact,
    Exit
}
