using LucianoNicolasArrieta.PhoneBook.Models;
using Spectre.Console;

namespace LucianoNicolasArrieta.PhoneBook
{
    public class TableVisualization
    {
        public void PrintContacts(List<Contact> contacts)
        {
            var table = new Table();
            table.AddColumn("Id");
            table.AddColumn("Name");
            table.AddColumn("Phone Number");
            table.AddColumn("Email");
            table.AddColumn("Category");

            foreach (var contact in contacts)
            {
                table.AddRow(new string[] { $"{contact.ContactID}", $"{contact.Name}", $"{contact.PhoneNumber}", $"{contact.Email}", $"{contact.CategoryID}"});
            }

            AnsiConsole.Write(table);
        }

        public void PrintCategories(List<Category> categories)
        {
            var table = new Table();
            table.AddColumn("Id");
            table.AddColumn("Name");
            table.Title = new TableTitle("[aqua]Categories[/]");

            foreach (var category in categories)
            {
                table.AddRow(new string[] { $"{category.CategoryID}", $"{category.Name}"});
            }

            AnsiConsole.Write(table);
        }
    }
}
