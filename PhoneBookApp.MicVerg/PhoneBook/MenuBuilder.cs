using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBook
{
    internal class MenuBuilder
    {
        internal static void MainMenu()
        {
            Console.Clear();
            Console.WriteLine("Choose an action:\n");
            Console.WriteLine("Add a contact. Type A.\n");
            Console.WriteLine("Delete a contact. Type D.\n");
            Console.WriteLine("View a contact. Type V.\n");
            Console.WriteLine("Update a contact. Type U.\n");
            Console.WriteLine("Quit. Type Q.\n");
            string userInput = Console.ReadLine();
            switch (userInput.ToLower())
            {
                case "a":
                    CrudController.Add();
                    break;
                case "d":
                    CrudController.Delete();
                    break;
                case "v":
                    CrudController.Read();
                    break;
                case "u":
                    CrudController.Update();
                    break;
                case "q":
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Invalid input.");
                    break;
            }
        }
    }
}
