using PhoneBook.Models;
using Spectre.Console;

namespace PhoneBook.Views
{
    public class CategoryView
    {
        private static readonly string[] columns = ["Id", "Name"];

        internal static void DisplayCategories(List<Category> categories)
        {
            var grid = new Grid()
                .AddColumn()
                .AddColumn();
            
            grid.AddRow(columns);

            var count = 1;
            foreach (var category in categories)
            {
                grid.AddRow(count.ToString(), category.Name);
                count++;
            }

            AnsiConsole.Write(grid);
        }

        internal static void DisplayContacts(Category category)
        {
            if (category.Contacts != null)
            {
            var table = new Table()
                .AddColumn("Contact ID")
                .AddColumn("Name")
                .AddColumn("Phone Number")
                .AddColumn("Email");

            foreach (var contact in category.Contacts)
            {
                table.AddRow(contact.ContactId.ToString(), contact.Name, contact.PhoneNumber, contact.Email);
            }

            AnsiConsole.Write(table);
            }
            else
            {
                AnsiConsole.MarkupLine("\n[bold][red]No contacts found in this category.[/]");
            }
        }
    }
}