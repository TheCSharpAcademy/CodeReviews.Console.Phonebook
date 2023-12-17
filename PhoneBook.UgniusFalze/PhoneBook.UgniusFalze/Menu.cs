using System.Text.RegularExpressions;
using EmailValidation;
using PhoneBook.UgniusFalze.Utils;
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
        var selectionPrompt = new SelectionPrompt<MenuOptions>()
            .Title("What would you like to do?")
            .AddChoices(
                MenuOptions.AddContact,
                MenuOptions.ViewContacts);
        selectionPrompt.Converter = options => Regex.Replace(options.ToString(), "(\\B[A-Z])", " $1"); //Using regex to split camel-cased enum option to better readable option
        var option = AnsiConsole.Prompt(selectionPrompt);
        switch (option)
        {
            case MenuOptions.ViewContacts:
                DisplayContacts(_contactsController.GetContacts());
                break;
            case MenuOptions.AddContact:
                AddContact();
                break;
        }

    }

    private void DisplayContacts(List<Contact> contacts)
    {
        var table = new Table();
        table.AddColumns("Id", "Name", "Email", "Phone Number");
        foreach (var contact in contacts)
        {
            table.AddRow(contact.ContactId.ToString(), contact.Name, contact.Email, contact.Number);
        }
        AnsiConsole.Write(table);
    }

    private void AddContact()
    {
        var contactName = UserInput.GetName();
        var email = UserInput.GetEmail();
        var phoneNumber = UserInput.GetPhoneNumber();
    }


}