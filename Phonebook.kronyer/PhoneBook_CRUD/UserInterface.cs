

using Spectre.Console;

namespace PhoneBook_CRUD
{
    static internal class UserInterface
    {
        public static void MainMenu()
        {
            
            bool isRunning = true;

            while (isRunning)
            {

                var options = AnsiConsole.Prompt(new SelectionPrompt<MenuOperations>().Title("What do you want to do?").AddChoices(MenuOperations.AddContact, MenuOperations.DeleteContact, MenuOperations.UpdateContact, MenuOperations.ShowAllContacts, MenuOperations.SearchContact, MenuOperations.Quit));

                switch (options)
                {
                    case MenuOperations.AddContact:
                        string name = AnsiConsole.Ask<string>("Name:");
                        string email = AnsiConsole.Ask<string>("Email:");
                        string phoneNumber = AnsiConsole.Ask<string>("PhoneNumber:");
                        Console.Clear();
                        ContactController.AddContact(name, email, phoneNumber);
                        break;
                    case MenuOperations.DeleteContact:
                        ContactService.DeleteContact();
                        break;
                    case MenuOperations.UpdateContact:
                        ContactService.UpdateContact();
                        break;
                    case MenuOperations.ShowAllContacts:
                        ContactService.ShowAllContacts();
                        break;
                    case MenuOperations.SearchContact:
                        ContactService.SearchContact();
                        break;

                    case MenuOperations.Quit:
                        isRunning = false;
                        Console.WriteLine("Bye!");
                        break;
                }

            }

        }

        enum MenuOperations
        {
            AddContact,
            DeleteContact,
            UpdateContact,
            ShowAllContacts,
            SearchContact,
            Quit
        }

        static internal void ShowContactsTable(List<Contacts> contacts)
        {
            Console.Clear();
            var table = new Table();
            table.AddColumn("ID");
            table.AddColumn("Name");
            table.AddColumn("Email");
            table.AddColumn("Phone");

            foreach (var contact in contacts)
            {
                table.AddRow(contact.Id.ToString(), contact.Name, contact.Email, contact.PhoneNumber);
            }
            AnsiConsole.Write(table);
            Console.WriteLine("Press any key to return");
            Console.ReadKey();
            Console.Clear();


        }
    }
}
