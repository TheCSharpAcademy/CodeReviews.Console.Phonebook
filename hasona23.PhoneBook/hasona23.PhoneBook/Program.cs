namespace hasona23.PhoneBook
{
    using hasona23.PhoneBook.Database;
    using hasona23.PhoneBook.Models;
    using Spectre.Console;
    internal class Program
    {
        static void Main()
        {

            Task dbInit = new (() => ContactsDB.Initialise());
            dbInit.Start();
            AnsiConsole.MarkupLine("Welcome to ");
            FigletText figlet = new FigletText("Phone Book").Color(Color.Blue);
            AnsiConsole.Write(figlet);
            dbInit.Wait();
            Console.Clear();
            bool isRunning = true;
            while (isRunning)
            {
                Console.Clear();
                switch (Input.GetOperation())
                {

                    case "Add Contact":
                        Add();
                        break;
                    case "Delete Contact":
                        Delete();
                        break;
                    case "Update Contact":
                        Update();
                        break;
                    case "Get Contacts":
                        Read(); 
                        break;
                    case "Exit":
                        isRunning = false;
                        break;

                }
            }

        }
        static void Add()
        {
            Contact contact = Input.GetContact();


            Console.WriteLine(contact);
            ContactsDB.AddContact(contact);
        }
        static void Delete()
        {
            Contact contact = Input.ChooseContact();
            ContactsDB.Delete(contact);
        }

        static void Update()
        {
            Console.Clear();
            Contact oldContact = Input.ChooseContact();
            AnsiConsole.MarkupLine($"[blue]Old: {oldContact}[/]");
            AnsiConsole.Markup("[white]Enter Contact new Info(press enter to skip)[/]\n");
            Contact newContact = Input.GetOptionalContact();
            ContactsDB.Update(oldContact, newContact);
        }
        static void Read()
        {
            AnsiConsole.WriteLine("Enter Search Filters(press enter to skip)");
            var contact = Input.GetOptionalContact();
            var contacts = ContactsDB.GetContacts(contact);
            var table = new Table().AddColumns("ID", "Name", "PhoneNumber", "Email");
            for (int i = 0; i < contacts.Count; i++)
            {
                table.AddRow($"{i + 1}", contacts[i].Name, contacts[i].Phone, contacts[i].Email);
            }
            table.Border(TableBorder.Double);
            table.Expand();
            table.Title("Contacts");
            table.Alignment(Justify.Center);
  
            AnsiConsole.Write(table);
            Console.ReadLine();
        }

    }
}
