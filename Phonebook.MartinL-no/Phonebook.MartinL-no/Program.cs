using Phonebook.MartinL_no.Services;
using Phonebook.MartinL_no.UserInterfaces;
using Spectre.Console;

using Phonebook.MartinL_no.Models;

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
            MenuOptions.ViewContact,
            MenuOptions.SendEmail));

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
        case MenuOptions.SendEmail:
            await ContactService.SendEmail();
            break;
    }
    Console.Clear();
}   