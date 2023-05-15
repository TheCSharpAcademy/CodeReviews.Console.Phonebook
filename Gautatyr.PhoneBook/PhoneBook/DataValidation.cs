using static PhoneBook.Helpers;
using static PhoneBook.DatabaseInterface;

namespace PhoneBook;

internal class DataValidation
{
    public static int GetNumberInput(string message = "")
    {
        Console.WriteLine(message);

        var input = Console.ReadLine();

        while (!int.TryParse(input, out _))
        {
            DisplayError($"{input} is not a number");
            Console.WriteLine(message);

            input = Console.ReadLine();
        }

        int validNumber = int.Parse(input);

        return validNumber;
    }

    public static string GetTextInput(string message = "")
    {
        Console.WriteLine(message);

        var input = Console.ReadLine();

        while (string.IsNullOrEmpty(input))
        {
            DisplayError($"Text can't be empty !");
            Console.WriteLine(message);

            input = Console.ReadLine();
        }

        return input;
    }

    public static int GetContactIdInput(string message = "")
    {
        int input = GetNumberInput(message);

        while (ContactExists(input) == false && input != 0)
        {
            DisplayError($"{input} is not a valid id, please try again or type 0 to go back");
            input = GetNumberInput();
        }

        return input;
    }
}
