using PhoneBook.Services;

namespace PhoneBook
{
    internal static class PhoneBookMenu
    {
        public static void GetUserInput()
        {
            while (true) 
            {
                Console.Clear();
                Console.WriteLine("Hello to your phone book! What you want to do?");
                Console.WriteLine("a. Insert contact");
                Console.WriteLine("b. Delete contact");
                Console.WriteLine("c. Update contact");
                Console.WriteLine("d. View contacts");
                Console.WriteLine("e. Send Email");
                Console.WriteLine("f. Exit");
                string? option = Console.ReadLine();

                switch (option)
                {
                    case "a":
                        PhoneBookController.InsertContact();
                        break;
                    case "b":
                        PhoneBookController.RemoveContact();
                        break;
                    case "c":
                        PhoneBookController.UpdateContact();
                        break;
                    case "d":
                        PhoneBookController.ViewContacts();
                        break;
                    case "e":
                        PhoneBookService.SendEmail();
                        break;
                    case "f":
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Invalid option. Try again");
                        Console.ReadLine();
                        break;
                }
            }
        }
    }
}
