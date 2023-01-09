namespace PhoneBookConsole.PhoneBookService;

public static class Menus
{
    public static void DisplayMainMenu()
    {
        Console.Clear();
        Console.WriteLine("**********WELCOME**********");
        Console.WriteLine("1 to view all contacts");
        Console.WriteLine("2 to add a new contact");
        Console.WriteLine("3 to edit a contact");
        Console.WriteLine("4 to delete a contact");
        Console.WriteLine("0 to close the app");
        Console.WriteLine("\nType your choice and hit Enter");
        Console.Write("Your choice? ");
    }

    public static void DisplayUpdateMenu()
    {
        Console.Clear();
        Console.WriteLine("1 to edit name");
        Console.WriteLine("2 to edit phone number");
        Console.WriteLine("3 to edit email");
        Console.WriteLine("0 to go back");
        Console.WriteLine("\nType your choice and hit Enter");
        Console.Write("Your choice? ");
    }
}