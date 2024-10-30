using Console.Phonebook.App.Data;
using Console.Phonebook.App.Entities;
using Spectre.Console;

namespace Console.Phonebook.App.UI;

public class ViewAllContactsUI
{
    DataServices DbServices = new();

    public void ViewContacts()
    {
        List<Contact> contactsList = DbServices.GetAllContacts();
        DisplayAllReturnedContacts(contactsList);
    }

    public void DisplayAllReturnedContacts(List<Contact> contactsList)
    {
        MainMenuUI MainMenuUI = new();

        AnsiConsole.Clear();
        var table = new Table();
        table.Border = TableBorder.Ascii;
        table.AddColumn("[green]Name[/]");
        table.AddColumn("[blue]Email[/]");
        table.AddColumn("[red]Phone Number[/]");
        table.AddColumn("[orange3]Category[/]");

        foreach (Contact contact in contactsList)
        {
            table.AddRow(contact.Name.ToString(), contact.EmailAddress.ToString(), contact.PhoneNumber.ToString(), contact.Categories.ToString());
        }

        AnsiConsole.Write(table);
        AnsiConsole.Markup("\n\n\n\n");
        MainMenuUI.MainMenuSelection();
    }
}