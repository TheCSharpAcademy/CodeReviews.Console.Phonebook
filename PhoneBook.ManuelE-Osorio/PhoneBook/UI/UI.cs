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

    public static void DisplayContacts(List<ContactDtoWithSelection> contacts, int selection, int prevSelection) 
    {
        Console.Clear();
        contacts[prevSelection].Selected = "[ ]";
        contacts[selection].Selected = "[x]";
        TableUI.PrintTable(contacts);
        Console.WriteLine("Press any key to return.");
    }

    public static void DisplayContactData(List<PhoneNumberDto> phonesDto, List<EmailDto> emailsDto, 
        int selection, int prevSelection, string? contactName) //to 
    {
        Console.Clear();
        int length = emailsDto.Count + phonesDto.Count;
        if(length>0)
        {
            if(prevSelection < phonesDto.Count)  // error is emails or phones are empty fix
                phonesDto[prevSelection].Selected = "[ ]";
            else
                emailsDto[prevSelection - phonesDto.Count].Selected = "[ ]";
            if(selection < phonesDto.Count)
                phonesDto[selection].Selected = "[X]";
            else
                emailsDto[selection - phonesDto.Count].Selected = "[X]";
        }
        
        TableUI.PrintTable(phonesDto, contactName);
        TableUI.PrintTable(emailsDto, contactName);
        Console.WriteLine("Press any key to return."); // instructions
    }

    public static void DisplayConfirmationPromt(string objectToDelete) //make it better
    {
        Console.Clear();
        Console.WriteLine($"Do you want to delete the selected {objectToDelete}? [y/N]");
    }

    public static void DisplayInsert(string objectToInsert)  //make it better
    {
        Console.Clear();
        Console.WriteLine($"Please write the {objectToInsert}");
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
