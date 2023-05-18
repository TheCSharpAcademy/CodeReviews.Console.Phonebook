using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phonebook
{
    internal class MainMenu
    {
        public static void ShowMenu()
        {
            Console.WriteLine("Hello from your friendly phonebook!!");
            Console.WriteLine("Please choose from the following options.");
            Console.WriteLine("Press 0 to exit program.");
            Console.WriteLine("Press 1 to view contacts.");
            Console.WriteLine("Press 2 to add new contact.");
            Console.WriteLine("Press 3 to delete contact.");
            Console.WriteLine("Press 4 to update contact.");

            var command = Console.ReadLine();
            
            switch (command)
            {
                case "0":
                    Console.WriteLine("Goodbye.");
                    Environment.Exit(0);
                    break;
                case "1":
                    UserInput.ViewContacts();
                    break;
                case "2":
                    UserInput.AddContact();
                    break;
                case "3":
                    UserInput.DeleteContact();
                    break;
                case "4":
                    break;
                default:
                    Console.WriteLine("Invalid entry. Press any key to continue.");
                    Console.ReadLine();
                    ShowMenu();
                    break;
            }
        }
    }
}
