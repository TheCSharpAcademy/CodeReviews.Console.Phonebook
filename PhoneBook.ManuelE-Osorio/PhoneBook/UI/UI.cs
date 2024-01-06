namespace PhoneBookProgram;

public class UI
{
    public static List<List<object>> MainMenuOptions {get; set;} = [["[ ]", "View all the contacts"],
        ["[ ]", "Add a new contact"],
        ["[ ]", "Modify a contact"],
        ["[ ]", "Delete a contact"]];

    public static List<List<object>> ModifyMenuOptions {get; set;} = [["[ ]", "View all the contacts"],
        ["[ ]", "Add a new contact"],
        ["[ ]", "Modify a contact"],
        ["[ ]", "Delete a contact"]];

    public static void WelcomeMessage()
    {
        Console.Clear();
        Console.WriteLine("Welcome to the Phone Book App!\n");
        Thread.Sleep(2000);
    }
    public static void MainMenu(int selection, string? errorMessage = null)
    {
        Console.Clear();

        if(errorMessage != null)
            Console.WriteLine($"Error: {errorMessage}");
        Console.WriteLine("Please select one of the following options:\n");
        
        Helpers.ClearSelection(MainMenuOptions);
        MainMenuOptions[selection][0] = "[x]";
        TableUI.PrintTableWithSelection(MainMenuOptions);
    }

    public static void DisplayContacts(List<ContactDTO> contacts)
    {
        Console.Clear();
        TableUI.PrintTableAddSelection(contacts);
        Console.WriteLine("Press any key to return.");
    }

    public static void ExitMessage(string? errorMessage)
    {
        Console.Clear();
        if(errorMessage != null)
            Console.WriteLine("Error: " + errorMessage);

        Console.WriteLine("Thank you for using the Phone Book App!\n");
        Thread.Sleep(2000);
    }
}
