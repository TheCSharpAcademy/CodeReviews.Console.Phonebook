using Phonebook.MartinL_no.Controllers;
using Phonebook.MartinL_no.Services;
using Phonebook.MartinL_no.UserInterface;
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
            //ContactController.AddContact();
            break;
        case MenuOptions.DeleteContact:
            //ContactController.DeleteContact();
            break;
        case MenuOptions.UpdateContact:
            //ContactController.UpdateContact();
            break;
        case MenuOptions.ViewAllContacts:
            var contacts = ContactController.GetContacts();
            UserInterface.ShowContacts(contacts);
            break;
        case MenuOptions.ViewContact:
            var contact = ContactService.GetContactOptionInput();
            UserInterface.ShowContact(contact);
            break;
    }
}