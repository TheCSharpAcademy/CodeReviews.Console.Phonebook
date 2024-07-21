using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Model;

namespace Phonebook.kjanos89
{
    public class Menu()
    {
        PhonebookManipulation manipulation;
        public void SetPBManipulation(PhonebookManipulation pbm)
        {
            this.manipulation = pbm;
        }
        public void DisplayMenu()
        {
            bool validInput = false;
            while (!validInput)
            {
                Console.Clear();
                CenterText("<<<<<WELCOME!>>>>>");
                Console.WriteLine("\nChoose from the options below:\n");
                Console.WriteLine("1 - View all contacts");
                Console.WriteLine("2 - Add contact");
                Console.WriteLine("3 - Update contact");
                Console.WriteLine("4 - Delete contact");
                Console.WriteLine("0 - Quit");
                string choice = Console.ReadLine();

                if (!string.IsNullOrEmpty(choice) && choice.Length == 1 && "12340".Contains(choice[0]))
                {
                    validInput = true;
                    MenuOption(choice[0]);
                }
                else
                {
                    Console.WriteLine("Invalid input. Please try again.");
                }
            }
        }
        public void MenuOption(char option)
        {
                switch (option)
                {
                    case '1':
                    manipulation.ShowContacts();
                    break;
                    case '2':
                    Console.WriteLine("We're here.");
                    manipulation.AddContact();
                    break;
                    case '3':
                    manipulation.UpdateContact();
                    break;
                    case '4':
                    manipulation.DeleteContact();
                    break;
                    case '0':
                    QuitApplication();
                    break;
                    default:
                    Console.WriteLine("Please try again!");
                    break;
                }
           
        }
        private void CenterText(string text)
        {
            int screenWidth = Console.WindowWidth;
            int textWidth = text.Length;
            int padding = (screenWidth - textWidth) / 2;
            Console.WriteLine(new string(' ', padding) + text);
        }

        private void QuitApplication()
        {
            Environment.Exit(2);
        }
    }
}
