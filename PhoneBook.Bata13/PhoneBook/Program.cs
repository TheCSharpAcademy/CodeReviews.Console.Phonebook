using PhoneBook;
using Spectre.Console;

var isAppRunning = true;
while (isAppRunning)
{
    var option = AnsiConsole.Prompt(
        new SelectionPrompt<MenuOptions>()
        .Title("What would you like to do?")
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
            ContactServce.AddContact();
            break;
        case MenuOptions.DeleteContact:
            ContactServce.DeleteContact();
            break;

        case MenuOptions.UpdateContact:
            ContactServce.UpdateContact();
            break;

        case MenuOptions.ViewContact:
            var contact = ContactServce.GetContactOptionInput();
            UserInterface.ShowContact(contact);
            break;

        case MenuOptions.ViewAllContacts:
            var contacts = ContactController.GetContacts();
            UserInterface.ShowContactTable(contacts);           
            break;

        case MenuOptions.Quit:
            ExitProgram();
            break;
    }
}
void ExitProgram()
{
    isAppRunning = false;
    Environment.Exit(0);
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