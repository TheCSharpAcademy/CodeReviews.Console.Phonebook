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

            foreach (var category in categories)
            {
                grid.AddRow(columns);
                grid.AddRow(category.CategoryId.ToString(), category.Name);
            }

            AnsiConsole.Write(grid);
        }
    }
}