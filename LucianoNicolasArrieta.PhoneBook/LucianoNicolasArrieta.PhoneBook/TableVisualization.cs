using LucianoNicolasArrieta.PhoneBook.Models;
using Spectre.Console;

namespace LucianoNicolasArrieta.PhoneBook
{
    public class TableVisualization
    {
        public void printContacts(List<Contact> contacts)
        {
            var table = new Table();

            table.AddColumn("Id");
            table.AddColumn("Name");
            table.AddColumn("Phone Number");
            table.AddColumn("Email");

            foreach (var contact in contacts)
            {
                table.AddRow(new string[] { $"{contact.Id}", $"{contact.Name}", $"{contact.PhoneNumber}", $"{contact.Email}" });
            }

            AnsiConsole.Write(table);
        }
    }
}
