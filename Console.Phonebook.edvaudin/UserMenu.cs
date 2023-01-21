using ConsoleTableExt;
using Phonebook.Models;

namespace Phonebook
{
    internal class UserMenu
    {
        public static void DisplayOptionsMenu()
        {
            Console.WriteLine("\nChoose an action from the following list:\n");
            Console.WriteLine("\tv - View your contacts");
            Console.WriteLine("\ta - Add a new contact");
            Console.WriteLine("\td - Delete a contact");
            Console.WriteLine("\tu - Update a contact");
            Console.WriteLine("\t0 - Quit this application");
            Console.Write("\nYour option? ");
        }

        public static void DisplayTitle()
        {
            Console.WriteLine("Your Contacts\r");
            Console.WriteLine("-------------\n");
        }

        internal static void ViewContacts(List<Contact> contacts)
        {
            var tableData = new List<List<object>>();
            foreach (Contact contact in contacts)
            {
                tableData.Add(new List<object>
                {   contact.Name,
                    contact.PhoneNumber
                });
            }
            ConsoleTableBuilder.From(tableData).WithTitle("Your Contacts").WithColumn("Name", "Phone Number").ExportAndWriteLine();
        }
    }
}
