using PhoneBook.JsPeanut;
using Spectre.Console;

var isAppRunning = true;
while (isAppRunning)
{
    Console.Clear();
    var option = AnsiConsole.Prompt(
        new SelectionPrompt<MenuOptions>()
        .Title("- PhoneBook App - \n\n  Welcome! What do you want to do?")
        .AddChoices(
            MenuOptions.AddContact,
            MenuOptions.DeleteContact,
            MenuOptions.UpdateContact,
            MenuOptions.ViewContact,
            MenuOptions.ViewAllContacts,
            MenuOptions.Quit));

    switch (option)
    {
        case MenuOptions.AddContact:
            ContactService.InsertContact();
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
        case MenuOptions.Quit:
            isAppRunning = false;
            Console.WriteLine("You quit the app successfully!");
            break;
    }
}

enum MenuOptions
{
    AddContact,
    DeleteContact,
    UpdateContact,
    ViewContact,
    ViewAllContacts,
    Quit
}

enum YesNoOptions
{
    Yes,
    No
}