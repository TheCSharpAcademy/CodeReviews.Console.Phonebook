using PhoneBook.JsPeanut.Models;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBook.JsPeanut
{
    static internal class UserInterface
    {
        internal static void ShowContact(Contact contact)
        {
            var panel = new Panel($@"Id: {contact.Id}
Name: {contact.Name}
Phone number: {contact.PhoneNumber}");
            panel.Header = new PanelHeader("Contact Info");
            panel.Padding = new Padding(2, 2, 2, 2);

            AnsiConsole.Write(panel);

            Console.WriteLine("Enter any key to continue");
            Console.ReadLine();
            Console.Clear();
        }
        static internal void ShowContactTable(List<Contact> contacts)
        {
            var table = new Table();
            table.AddColumn("Id");
            table.AddColumn("Name");
            table.AddColumn("Phone number");


            foreach (var contact in contacts)
            {
                table.AddRow(contact.Id.ToString(), contact.Name, contact.PhoneNumber);
            }

            AnsiConsole.Write(table);

            Console.WriteLine("\nEnter any key to continue");
            Console.ReadLine();
            Console.Clear();
        }
    }
}

