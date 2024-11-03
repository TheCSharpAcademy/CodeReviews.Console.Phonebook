using Console.Phonebook.App.Data;
using Console.Phonebook.App.Entities;
using Console.Phonebook.App.Enums;
using Console.Phonebook.App.Services;
using Spectre.Console;

namespace Console.Phonebook.App.UI;

public class CreateContactUI
{
    DataServices DbServices = new();

    public void CreateNewContact()
    {
        Contact newContact = new();
        MainMenuUI MainMenuUI = new();

        newContact.Name = AnsiConsole.Ask<string>("Please enter contact's name: ");
        do
        {
            newContact.EmailAddress = AnsiConsole.Ask<string>("Please enter contact's email: ");
        } while (!ValidationServices.IsValidEmailAddress(newContact.EmailAddress));

        do
        {
            newContact.PhoneNumber = AnsiConsole.Ask<string>("Please enter contact's phone number (eg. 0404 123 987): ");
        } while (!ValidationServices.IsValidPhoneNumber(newContact.PhoneNumber));

        newContact.Categories = AnsiConsole.Ask<Categories>("Please enter contact's category (Friends, Family, Work): ");

        DbServices.PostContact(newContact);

        System.Console.Clear();
        System.Console.WriteLine("\n\nContact successfuly created.\n\n\n\n\n");
        MainMenuUI.MainMenuSelection();
    }
}