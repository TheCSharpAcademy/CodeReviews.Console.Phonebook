using System.Text.RegularExpressions;
using PhoneBook.UgniusFalze.Controllers;
using PhoneBook.UgniusFalze.Models;
using PhoneBook.UgniusFalze.Utils;
using Spectre.Console;

namespace PhoneBook.UgniusFalze;

enum MenuOptions
{
    ViewContacts,
    AddContact,
    ManageContacts,
    Exit
}

enum ManageOptions
{
    ChangeName,
    ChangeEmail,
    ChangePhoneNumber,
    Delete,
    Exit
}

public class Menu
{
    public void Start()
    {
        bool exit = false;
        do
        {
            AnsiConsole.Clear();
            var selectionPrompt = new SelectionPrompt<MenuOptions>()
                .Title("What would you like to do?")
                .AddChoices(
                    MenuOptions.AddContact,
                    MenuOptions.ViewContacts,
                    MenuOptions.ManageContacts,
                    MenuOptions.Exit);
            selectionPrompt.Converter =
                options => Regex.Replace(options.ToString(), "(\\B[A-Z])",
                    " $1"); //Using regex to split camel-cased enum option to better readable option
            var option = AnsiConsole.Prompt(selectionPrompt);
            switch (option)
            {
                case MenuOptions.ViewContacts:
                    DisplayContacts();
                    break;
                case MenuOptions.AddContact:
                    AddContact();
                    break;
                case MenuOptions.ManageContacts:
                    ManageContacts();
                    break;
                case MenuOptions.Exit:
                    exit = true;
                    break;
            }
        } while (!exit);

    }

    private void DisplayContacts()
    {
        var contacts = ContactsController.GetContacts();
        if (contacts.Count < 1)
        {
            Console.WriteLine("Contacts is empty.");
            Console.ReadKey();
            return;
        }

        var table = new Table();
        table.AddColumns("Id", "Name", "Email", "Phone Number");
        foreach (var contact in contacts)
        {
            table.AddRow(contact.ContactId.ToString(), contact.Name, contact.Email, contact.Number);
        }
        AnsiConsole.Write(table);
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }

    private void AddContact()
    {
        var contactName = UserInput.GetName();
        var email = UserInput.GetEmail();
        var phoneNumber = UserInput.GetPhoneNumber();
        ContactsController.AddContact(contactName, email, phoneNumber);
        Console.WriteLine("New contact added. Press any key to continue...");
        Console.ReadKey();
    }

    private void ManageContacts()
    {
        var contacts = ContactsController.GetContacts();
        if (contacts.Count < 1)
        {
            Console.WriteLine("Contacts is empty.");
            Console.ReadKey();
            return;
        }
        var selectionPrompt = new SelectionPrompt<Contact>()
            .Title("Select Which Contact you want to edit");
        selectionPrompt.Converter = contact => contact.Name;
        foreach (var contact in contacts)
        {
            selectionPrompt.AddChoice(contact);
        }

        var selectedContact = AnsiConsole.Prompt(selectionPrompt);
        ManageContact(selectedContact);
    }

    private void ManageContact(Contact contact)
    {
        AnsiConsole.Clear();
        var selectionPrompt = new SelectionPrompt<ManageOptions>()
            .Title("What would you like to do?")
            .AddChoices(
                ManageOptions.ChangeName,
                ManageOptions.ChangeEmail,
                ManageOptions.ChangePhoneNumber,
                ManageOptions.Delete,
                ManageOptions.Exit);
        selectionPrompt.Converter =
            options => Regex.Replace(options.ToString(), "(\\B[A-Z])",
                " $1");
        var option = AnsiConsole.Prompt(selectionPrompt);
        switch (option)
        {
            case ManageOptions.ChangeName:
                contact.Name = UserInput.GetName();
                ContactsController.Update(contact);
                break;
            case ManageOptions.ChangeEmail:
                contact.Email = UserInput.GetEmail("Please enter updated contacts email: ");
                ContactsController.Update(contact);
                break;
            case ManageOptions.ChangePhoneNumber:
                contact.Number = UserInput.GetPhoneNumber("Please enter updated contacts phone number: ");
                ContactsController.Update(contact);
                break;
            case ManageOptions.Delete:
                ContactsController.Delete(contact);
                Console.WriteLine("Contact deleted. Press any key to continue..");
                Console.ReadKey();
                return;
            case ManageOptions.Exit:
                return;
        }
        
        Console.WriteLine("Contact updated. Press any key to continue...");
        Console.ReadKey();
    }


}