using Spectre.Console;
namespace Phonebook.JaegerByte
{
    enum MenuOption
    {
        AddContact, DeleteContact, UpdateContact, SearchContact, ViewAllContacts, Exit
    }
    internal class Program
    {
        static public DatabaseService DbService { get; set; }
        static public  InputValidationService IvService { get; set; }
        static public InputManager InpManager { get; set; }
        static void Main()
        {
            DbService = new DatabaseService();
            IvService = new InputValidationService();
            InpManager = new InputManager();

            while (true)
            {
                Console.Clear();
                MenuOption selection = InpManager.GetMenuSelection();
                switch (selection)
                {
                    case MenuOption.AddContact:
                        DbService.InsertContact(InpManager.GetInsertInput());
                        break;
                    case MenuOption.DeleteContact:
                        DbService.DeleteContact(InpManager.GetDeleteInput());
                        break;
                    case MenuOption.UpdateContact:
                        DataModel newContactData = InpManager.GetUpdateInput();
                        DbService.UpdateContact(newContactData);
                        break;
                    case MenuOption.SearchContact:
                        List<DataModel> searchList = DbService.SearchContacts(InpManager.GetSearchInput());
                        Console.Clear();
                        DisplayTable(searchList);
                        AnsiConsole.Write("Press ANY key to return");
                        Console.ReadKey(true);
                        break;
                    case MenuOption.ViewAllContacts:
                        List<DataModel> allList = DbService.GetAllContacts();
                        Console.Clear();
                        DisplayTable(allList);
                        AnsiConsole.Write("Press ANY key to return");
                        Console.ReadKey();
                        break;
                    case MenuOption.Exit:
                        System.Environment.Exit(0);
                            break;
                }
            }
        }
        
        static void DisplayTable(List<DataModel> contacts)
        {
            Table table = new Table();
            table.AddColumns(
                new TableColumn("Firstname"),
                new TableColumn("Lastname"),
                new TableColumn("Phonenumber"),
                new TableColumn("Email"));
            foreach (DataModel item in contacts)
            {
                table.AddRow(item.FirstName, item.LastName, item.Phonenumber, item.Email);
            }
            AnsiConsole.Write(table);
        }
    }
}
