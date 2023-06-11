using ConsoleTableExt;
using System.Diagnostics.CodeAnalysis;

namespace PhoneBook
{
    public class CreateTableEngine
    {
        public static void ShowTable<T>( List<T> tableData, [AllowNull] string tableName ) where T : class
        {
            Console.Clear();
            if (tableName == null)
                tableName = "";

            Console.WriteLine("\n\n");

            ConsoleTableBuilder
                .From(tableData)
                .WithColumn("Id", "Name", "Phone Number", "E-mail", "Category")
                .ExportAndWriteLine();
            Console.WriteLine("\n\n");
        }
    }
}
