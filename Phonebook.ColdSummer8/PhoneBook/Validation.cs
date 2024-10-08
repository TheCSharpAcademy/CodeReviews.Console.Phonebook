using System.Text.RegularExpressions;

namespace PhoneBook;
internal class Validation
{
    public static int userInput;
    public static int NumericInputOnly()
    {
        string msg = string.Empty;
        ConsoleKeyInfo key;
        do
        {
            key = Console.ReadKey(true);
            if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
            {
                if (char.IsDigit(key.KeyChar))
                {
                    msg += key.KeyChar;
                    Console.Write(key.KeyChar);
                }
            }
            else if (key.Key == ConsoleKey.Backspace && msg.Length > 0)
            {
                msg = msg.Substring(0, (msg.Length - 1));
                Console.Write("\b \b");
            }
        }
        while (key.Key != ConsoleKey.Enter || string.IsNullOrEmpty(msg));

        if (int.TryParse(msg, out int val))
        {
            return val;
        }
        return userInput;
    }
    public static bool IsValidEmail(out string? userEmail)
    {
        userEmail = string.Empty;
        bool validEmail = false;
        string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";

        do
        {
            Console.Write("Email: ");
            userEmail = Console.ReadLine();
            validEmail = Regex.IsMatch(userEmail, pattern);
        } while (!validEmail);
        return !string.IsNullOrEmpty(userEmail) && validEmail;
    }
    public static bool IsValidName(out string? userName)
    {
        userName = string.Empty;
        bool validName = false;
        string pattern = @"^[A-Za-z\s\-\']+$";

        do
        {
            Console.Write("Name: ");
            userName = Console.ReadLine();
            validName = Regex.IsMatch(userName, pattern);
        } while (!validName);
        return !string.IsNullOrEmpty(userName) && validName;
    }
    public static bool IsValidNumber(out int userNumber)
    {
        userNumber = 0;
        bool validNumber = false;
        do
        {
            Console.Write("Number(8 digits): ");
            userNumber = NumericInputOnly();
            validNumber = userNumber.ToString().Length == 8;
            Console.WriteLine();
        } while (!validNumber);
        return validNumber;
    }
}
