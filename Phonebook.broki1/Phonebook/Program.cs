namespace Phonebook;

internal class Program
{
    static void Main(string[] args)
    {
        bool exitApp = false;

        while (!exitApp)
        {
            Console.Clear();

            var userInput = UserInput.GetMainMenuInput();

            switch (userInput)
            {
                case "0":
                    exitApp = true;
                    break;
                case "1":
                    PhonebookManager.AddContact();
                    break;
                case "2":
                    PhonebookManager.ReadContact();
                    break;
                case "3":
                    PhonebookManager.UpdateContact();
                    break;
                case "4":
                    PhonebookManager.DeleteContact();
                    break;
                default:
                    Console.WriteLine("\ninvalid input");
                    break;
            }
        }
    }
}
