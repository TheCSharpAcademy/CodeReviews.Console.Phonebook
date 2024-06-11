using PhoneBook.kalsson.Models;
using Spectre.Console;

namespace PhoneBook.kalsson.UI;

public static class UserInterface
{
    static internal void ShowContactsTable(List<Contact> contacts)
    {
        var table = new Table();
        table.AddColumn("Id");
        table.AddColumn("Firstname");
        table.AddColumn("Lastname");
        table.AddColumn("Email");
        table.AddColumn("Phone");
        
        foreach (var contact in contacts)
        {
            var emails = String.Join(", ", contact.EmailAddresses.Select(e => e.EmailAddress));
            var phones = String.Join(", ", contact.PhoneNumbers.Select(p => p.PhoneNumber));
            
            table.AddRow(contact.Id.ToString(), contact.FirstName, contact.LastName, emails, phones);
        }
        
        AnsiConsole.Write(table);

        Console.WriteLine("Enter any key to continue");
        Console.ReadLine();
        Console.Clear();
    }
}