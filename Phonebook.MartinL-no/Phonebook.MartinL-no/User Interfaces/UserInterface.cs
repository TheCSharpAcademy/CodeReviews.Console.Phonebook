using Phonebook.MartinL_no.Models;
using Spectre.Console;

namespace Phonebook.MartinL_no.UserInterfaces;

public class UserInterface
{
    public static void ShowContacts(List<Contact> contacts)
    {
        var table = new Table();
        table.AddColumn("Id");
        table.AddColumn("Name");
        table.AddColumn("Phone Number");
        table.AddColumn("Email address");


        foreach (var contact in contacts)
        {
            table.AddRow(contact.Id.ToString(), contact.Name, contact.PhoneNumber, contact.Email);
        }

        AnsiConsole.Write(table);

        Console.WriteLine("Enter any key to continue");
        Console.ReadKey();
    }

    public static void ShowContact(Contact contact)
    {
        var panel = new Panel($"""
            Id:             {contact.Id}
            Name:           {contact.Name}
            Phone Number:   {contact.PhoneNumber}
            Email Address:  {contact.Email}
            """);
        panel.Header("Contact Info");
        panel.Padding(2,2,2,2);

        AnsiConsole.Write(panel);

        Console.WriteLine("Enter any key to continue");
        Console.ReadKey();
    }
}

