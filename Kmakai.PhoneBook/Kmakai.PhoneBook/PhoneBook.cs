
using Kmakai.PhoneBook.Services;
using Spectre.Console;

namespace Kmakai.PhoneBook;

public class PhoneBook
{
    private bool IsRunning = true;
    public void Start()
    {
        while (IsRunning)
        {
            var option = AnsiConsole.Prompt(
                         new SelectionPrompt<Menu>()
                         .Title("What would you like to do?")
                         .PageSize(10)
                         .AddChoices(Menu.AddContact, Menu.GetContacts, Menu.GetContact, Menu.UpdateContact, Menu.DeleteContact, Menu.Exit));

            switch (option)
            {
                case Menu.AddContact:
                    ContactService.CreateContact();
                    break;
                case Menu.GetContacts:
                    ContactService.GetContacts();
                    break;
                case Menu.GetContact:
                    ContactService.GetContact();
                    break;
                case Menu.UpdateContact:
                    ContactService.UpdateContact();
                    break;
                case Menu.DeleteContact:
                    ContactService.DeleteContact();
                    break;
                case Menu.Exit:
                    IsRunning = false;
                    break;
            }
        }

    }

}

enum Menu
{
    AddContact,
    GetContacts,
    GetContact,
    UpdateContact,
    DeleteContact,
    Exit
}