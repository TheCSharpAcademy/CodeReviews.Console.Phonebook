using System.Text.RegularExpressions;
using Spectre.Console;

namespace PhoneBook;

internal class Validators
{
    internal static bool IsValidPhone(string phone) 
    {
        var phonePattern = @"^\+?\d{1,3}?[-. ]?(\(?\d{1,4}?\)?[-. ]?)?\d{1,4}[-. ]?\d{1,4}[-. ]?\d{1,9}$";
        bool isValidPhoneNumber = Regex.IsMatch(phone, phonePattern);
        if(isValidPhoneNumber){
            AnsiConsole.MarkupLine("[green]The phone number is valid.[/]");
            return true;
        }
        else{
            AnsiConsole.MarkupLine("[red]The phone number is invalid.[/]");
            return false;
        }
    }
    internal static bool IsValidEmail(string email) 
    {
        var emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
        bool isValidEmail = Regex.IsMatch(email, emailPattern);
        if(isValidEmail){
            AnsiConsole.MarkupLine("[green]The email is valid.[/]");
            return true;
        }
        else{
            AnsiConsole.MarkupLine("[red]The email is invalid.[/]");
            return false;
        }
    }
}