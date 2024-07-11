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
    }
}