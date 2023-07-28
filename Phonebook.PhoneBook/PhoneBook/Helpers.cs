using static PhoneBook.DataValidation;

namespace PhoneBook;

internal class Helpers
{
    public static void DisplayError(string error)
    {
        Console.WriteLine($"\n|---> Error: {error} <---|\n");
    }

    public static bool GetConfirmation(string message = "")
    {
        bool confirmation = false;

        Console.WriteLine(message);

        string input = GetTextInput().ToLower();

        while (input != "yes" && input != "no")
        {
            DisplayError("ERROR: type 'yes' or 'no'");

            input = GetTextInput().ToLower();
        }

        if (input == "yes") confirmation = true;

        return confirmation;
    }
}
