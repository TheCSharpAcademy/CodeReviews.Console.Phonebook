using ConsoleTableExt;

namespace PhoneBook
{
    internal class UI
    {
        public UI() { }

        public SELECTOR MainMenu()
        {
            Console.Clear();
            Write("PhoneBooks App");
            Write("".PadRight(24, '='));
            Write("1. Add a contact");
            Write("2. View a contact");
            Write("3. Update a contact");
            Write("4. Delete a contact");
            Write("0. Exit\n");
            var selector = (SELECTOR)GetInput("Select ").val;

            return selector;
        }
        public static void Write(string text)
        {
            Console.WriteLine(text);
        }
        public static void Write(int text)
        {
            Console.WriteLine(text);
        }
        public static void Clear()
        {
            // Somehow, Console.Clear() doesn't work properly. it just skips lines.
            // This code clears the console.
            // https://github.com/dotnet/runtime/issues/28355

            Console.Write("\f\u001bc\x1b[3J");
        }
        public static void MakeTable(List<List<object>> data, string tableName)
        {
            Clear();
            ConsoleTableBuilder
                .From(data)
                .WithTitle(tableName, ConsoleColor.Green)
                .WithColumn("ID","Name","Email Address", "Phone Number")
                .ExportAndWriteLine();
            Console.WriteLine("".PadRight(24, '='));
        }

        public (bool res, string str, int val) GetInput(string message)
        {
            // This function returns string input too in case you need it
            int number;
            Write(message);
            Console.Write(">> ");
            string str = Console.ReadLine();
            var res = int.TryParse(str, out number);

            number = res ? number : -1;
            str = str == null ? "" : str;

            return (res, str, number);
        }
        public static void WaitForInput(string message = "")
        {
            Write(message);
            Console.ReadKey();
        }
        public SELECTOR GoToMainMenu(string message = "")
        {
            WaitForInput(message);
            return MainMenu();
        }
    }
}