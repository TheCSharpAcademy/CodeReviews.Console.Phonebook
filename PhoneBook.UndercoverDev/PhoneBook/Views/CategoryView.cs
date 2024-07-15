using PhoneBook.Models;
using Spectre.Console;

namespace PhoneBook.Views
{
    public class CategoryView
    {
        private static readonly string[] categoryColumns = ["Id", "Name"];
        private static readonly string[] contactsColumns = ["Id", "Name", "Phone", "Email"];

        internal static void DisplayCategories(List<Category> categories)
        {
            var table = new Table{ Border = TableBorder.DoubleEdge };
            table.AddColumns(categoryColumns);

            var count = 1;
            foreach (var category in categories)
            {
                table.AddRow(count.ToString(), category.Name);
                count++;
            }

            AnsiConsole.Write(table);
        }

        internal static void DisplayContacts(Category category)
        {
            if (category.Contacts != null)
            {
                var table = new Table{ Border = TableBorder.DoubleEdge };
                table.AddColumns(contactsColumns);

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