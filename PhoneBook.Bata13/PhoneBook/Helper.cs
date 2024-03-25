using Spectre.Console;

namespace PhoneBook;
internal class Helper
{
    internal static void waitUserToPressAnyKeyToContinue()
    {
        Console.WriteLine("Enter any key to continue");
        Console.ReadLine();
        Console.Clear();
    }
    internal static void AskAndUpdatePhoneNumber(Contact contact)
    {
        contact.PhoneNumber = AnsiConsole.Confirm("Update PhoneNumber?")
            ? AnsiConsole.Ask<string>("Contact's new PhoneNumber:")
            : contact.PhoneNumber;

        string contactPhoneNumber = contact.PhoneNumber;

        string validPhoneNumber = Validation.GetValidPhoneNumberToUpdateFromUser(contactPhoneNumber);
    }
    internal static void AskAndUpdateEmail(Contact contact)
    {
        contact.Email = AnsiConsole.Confirm("Update Email?")
            ? AnsiConsole.Ask<string>("Contact's new Email:")
            : contact.Email;

        string contactEmail = contact.Email;

        string validEmail = Validation.GetValidEmailToUpdateFromUser(contactEmail);
    }
    internal static void AskAndUpdateName(Contact contact)
    {
        contact.Name = AnsiConsole.Confirm("Update name?")
               ? AnsiConsole.Ask<string>("Contact's new name:")
               : contact.Name;
    }
}
