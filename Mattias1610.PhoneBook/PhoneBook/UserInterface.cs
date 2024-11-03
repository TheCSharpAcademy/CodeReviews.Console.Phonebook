using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Spectre.Console;

namespace PhoneBook
{
    public class UserInterface
    {
        static internal void ShowTable(List<Number> numbers){
            var table = new Spectre.Console.Table();
            table.AddColumn("ID");
            table.AddColumn("Name");
            table.AddColumn("Email");
            table.AddColumn("Phone Number");
            table.AddColumn("Category");

            foreach(var number in numbers){
                table.AddRow(number.ID.ToString(), number.Name, number.Email, number.PhoneNumber, number.Category);
            }

            AnsiConsole.Write(table);
            Console.ReadLine();
            Console.Clear();
            
        }
    }
}