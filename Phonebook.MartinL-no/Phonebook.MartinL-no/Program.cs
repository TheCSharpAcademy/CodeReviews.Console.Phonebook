using System.Reflection;
using Phonebook.MartinL_no.Services;
using Phonebook.MartinL_no.UserInterfaces;
using Spectre.Console;

while (true)
{
    var option = AnsiConsole.Prompt(
        new SelectionPrompt<MenuOptions>()
        .Title("What would you like to do:")
        .AddChoices(
            MenuOptions.AddContact,
            MenuOptions.DeleteContact,
            MenuOptions.UpdateContact,
            MenuOptions.ViewAllContacts,
            MenuOptions.ViewContact));

    switch (option)
    {
        case MenuOptions.AddContact:
            ContactService.AddContact();
            break;
        case MenuOptions.DeleteContact:
            ContactService.DeleteContact();
            break;
        case MenuOptions.UpdateContact:
            ContactService.UpdateContact();
            break;
        case MenuOptions.ViewAllContacts:
            ContactService.GetContacts();
            break;
        case MenuOptions.ViewContact:
            ContactService.GetContact();
            break;
    }
    Console.Clear();
}   