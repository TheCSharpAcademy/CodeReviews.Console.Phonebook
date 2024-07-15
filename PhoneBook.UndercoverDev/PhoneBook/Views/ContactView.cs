
using PhoneBook.Models;
using Spectre.Console;

namespace PhoneBook.Views
{
    public class ContactView
    {
        private static readonly string[] columns = ["Id", "Name", "Phone", "Email"];

        internal static void DisplayContacts(List<Contact> contacts)
        {
            var table = new Table{ Border = TableBorder.DoubleEdge };
            table.AddColumns(columns);

            var count = 1;
            foreach (var contact in contacts)
            {
                table.AddRow(count.ToString(), contact.Name, contact.PhoneNumber, contact.Email);
                count++;
            }

            AnsiConsole.Write(table);
        }
    }
}