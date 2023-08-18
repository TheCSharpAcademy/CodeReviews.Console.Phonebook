using Phonebook.MartinL_no.Services;
using Spectre.Console;

using Phonebook.MartinL_no.UserInterfaces;

while (true)
{
    var option = AnsiConsole.Prompt(
        new SelectionPrompt<MenuOptions>()
        .Title("What would you like to do:")
        .AddChoices(
            MenuOptions.AddContact,
            MenuOptions.DeleteContact,
            MenuOptions.UpdateContact,
            MenuOptions.ViewContact,
            MenuOptions.ViewAllContacts,
            MenuOptions.ViewByContactType,
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
        case MenuOptions.ViewContact:
            ContactService.GetContact();
            break;
        case MenuOptions.ViewAllContacts:
            ContactService.GetContacts();
            break;
        case MenuOptions.ViewByContactType:
            ContactService.GetContactsByType();
            break;
        case MenuOptions.SendEmail:
            await ContactService.SendEmail();
            break;
    }
    Console.Clear();
}   