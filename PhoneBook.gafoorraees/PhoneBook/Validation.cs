using Spectre.Console;
using System.Text.RegularExpressions;

namespace PhoneBook;

internal class Validation
{
    public static string GetValidName()
    {
        string name;
        var namePattern = @"^[a-zA-Z]+ [a-zA-Z]+$";

        do
        {
            name = AnsiConsole.Ask<string>("Enter your contact's first name and last name.\n(Please enter both separated by a space. Ex: 'Hermione Granger'): ").Trim();
            if (!Regex.IsMatch(name, namePattern))
            {
                AnsiConsole.Markup("\nInvalid name! Enter any key to try again.\n");
                Console.ReadKey();
                AnsiConsole.Clear();
                
            }
        } while (!Regex.IsMatch(name, namePattern));

        return CapitalizeName(name);
    }

    public static string GetValidPhoneNumber()
    {
        string phoneNumber;
        var phonePattern = @"^\d{3}-\d{3}-\d{4}$";

        do
        {
            phoneNumber = AnsiConsole.Ask<string>("Enter your contact's phone number. Format: 10 digits separated by '-'. Ex: '111-111-111': ").Trim();
            if (!Regex.IsMatch(phoneNumber, phonePattern))
            {
                AnsiConsole.Markup("Invalid phone number! Enter any key to try again.\n");
                Console.ReadKey();
                AnsiConsole.Clear();
            }
        } while (!Regex.IsMatch(phoneNumber, phonePattern));

        return phoneNumber;
    }

    internal static string GetValidEmail()
    {
        string email;
        var emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";

        do
        {
            email = AnsiConsole.Ask<string>("Enter your contact's email address. Ex: 'HermioneGranger@hogwarts.net'. Special characters allowed: '.', '_' '%', '+', '-'.: ").Trim();
            if (!Regex.IsMatch(email, emailPattern))
            {
                AnsiConsole.Markup("Invalid email address! Enter any key to try again.\n");
                Console.ReadKey();
                AnsiConsole.Clear();
            }
        } while (!Regex.IsMatch(email,emailPattern));

        return email;
    }

    private static string CapitalizeName(string name)
    {
        var chars = name.Split(' ');
        return string.Join(' ', chars.Select(n => char.ToUpper(n[0]) + n.Substring(1).ToLower()));
    }
}
