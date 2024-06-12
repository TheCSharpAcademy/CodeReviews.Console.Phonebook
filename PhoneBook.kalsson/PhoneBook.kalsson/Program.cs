using PhoneBook.kalsson;
using PhoneBook.kalsson.Controllers;
using PhoneBook.kalsson.Services;
using PhoneBook.kalsson.UI;
using Spectre.Console;

bool isAppRunning = true;

while (isAppRunning)
{
    var option = AnsiConsole.Prompt(new SelectionPrompt<MenuOptions>()
        .Title("What would you like to do?")
        .AddChoices(
            MenuOptions.ViewAllContacts,
            MenuOptions.AddContact,
            MenuOptions.ViewContact,
            MenuOptions.UpdateContact,
            MenuOptions.DeleteContact,
            MenuOptions.Exit));

    switch (option)
    {
        case MenuOptions.ViewAllContacts:
            var contacts = ContactController.GetAllContacts();
            UserInterface.ShowContactsTable(contacts);
            break;
    
        case MenuOptions.AddContact:
            ContactController.AddContact();
            break;
    
        case MenuOptions.ViewContact:
            var contact = ContactService.GetContactOptionInput();
            ContactController.GetContactById(contact.Id);
            break;
    
        case MenuOptions.UpdateContact:
            ContactController.UpdateContact();
            break;
    
        case MenuOptions.DeleteContact:
            ContactController.DeleteContact();
            break;
        
        case MenuOptions.Exit:
            isAppRunning = false;
        break;
    }   
}