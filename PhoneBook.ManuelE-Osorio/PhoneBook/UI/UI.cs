namespace PhoneBookProgram;

public class UI
{
    public static void WelcomeMessage()
    {
        Console.Clear();
        Console.WriteLine("Welcome to the Phone Book App!\n");
        Thread.Sleep(2000);
    }

    public static void DisplayContacts(List<ContactDtoWithSelection> contacts, int selection, int prevSelection) 
    {
        Console.Clear();
        if(contacts.Count > 0)
        {
            contacts[prevSelection].Selected = "[ ]";
            contacts[selection].Selected = "[x]";
        }
        TableUI.PrintTable(contacts);
        Console.WriteLine("User the arrows to select a contact\n"+
            "Press enter to check the details of the selected contact\n"+
            "Press I to create a new contact\n"+
            "Press M to modify the selected contact\n"+
            "Press D to delete the selected contact\n"+
            "Press Backspace/Esc to exit the application\n");
    }

    public static void DisplayContactData(List<PhoneNumberDto> phonesDto, List<EmailDto> emailsDto, 
        int selection, int prevSelection, string? contactName) //to 
    {
        Console.Clear();
        if(emailsDto.Count+phonesDto.Count  > 0)
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
        Console.WriteLine("User the arrows to select a phone number/email\n"+
            "Press enter to send an SMS/email to the selection\n"+          //pending
            "Press P/E to create a new phone/email to the selected contact\n"+
            "Press M to modify your selection\n"+
            "Press D to delete your selection\n"+
            "Press Backspace/Esc to return\n");
    }

    public static void DisplayConfirmationPromt(string objectName)
    {
        Console.Clear();
        Console.WriteLine($"Do you want to delete the selected {objectName}? [y/N]");
    }

    public static void DisplayInsert(string objectToInsert, string? errorMessage) //Pending restrictions
    {
        Console.Clear();
        if(errorMessage != null)
            Console.WriteLine($"Error: {errorMessage}");
        Console.WriteLine($"Please write the new {objectToInsert} or enter \"0\" to cancel.");
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
