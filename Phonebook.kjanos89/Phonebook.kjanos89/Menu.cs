using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Model;

namespace Phonebook.kjanos89
{
    public class Menu(PhonebookManipulation manipulation)
    { 
        Validation validation;
    
        public void DisplayMenu()
        {
            Console.Clear();
            Console.WriteLine("Choose from the options below:");
            Console.WriteLine("1 - View all contacts");
            Console.WriteLine("2 - Add contact");
            Console.WriteLine("3 - Update contact");
            Console.WriteLine("4 - Delete contact");
            Console.WriteLine("0 - Quit");
            string choice = Console.ReadLine();
            MenuOption(choice[0]);
        }
        public void MenuOption(char option)
        {
                switch (option)
                {
                    case '1':
                    manipulation.GetContacts();
                        break;
                    case '2':
                    Console.WriteLine("We're here.");
                    manipulation.AddContact();
                        break;
                    case '3':
                    manipulation.UpdateContact();
                        break;
                case '4':
                    //manipulation.DeleteContact();
                    break;
                case '0':
                        QuitApplication();
                        break;
                    default:
                        Console.WriteLine("Please try again!");
                        DisplayMenu();
                        break;
                }
           
        }

        private void QuitApplication()
        {
            Environment.Exit(2);
        }
    }
}
