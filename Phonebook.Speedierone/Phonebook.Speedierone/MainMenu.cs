namespace Phonebook
{
    internal class MainMenu
    {
        public static void ShowMenu()
        {
            bool closeApp = false;
            while (closeApp == false)
            {
                Console.Clear();
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
                        closeApp = true;
                        Environment.Exit(0);
                        break;
                    case "1":
                        Console.Clear();
                        Console.WriteLine("Which contacts would you like to view.");
                        Console.WriteLine("Press 0 to return to main menu.");
                        Console.WriteLine("Press 1 to view all contacts.");
                        Console.WriteLine("Press 2 to view by group.");
                        var views = Console.ReadLine();
                        
                        if (views == "0")
                        {
                            ShowMenu();
                        }
                        if (views == "1")
                        {
                            Console.Clear();
                            UserInput.ViewContacts();
                            Console.WriteLine("\n\nPress any button to continue.");
                            Console.ReadLine();
                        }
                        if(views == "2")
                        {
                            Console.Clear();
                            UserInput.ViewByGroup();
                        }                       
                        break;
                    case "2":
                        UserInput.AddContact();
                        break;
                    case "3":
                        UserInput.DeleteContact();
                        break;
                    case "4":
                        UserInput.UpdateContact();
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
}
