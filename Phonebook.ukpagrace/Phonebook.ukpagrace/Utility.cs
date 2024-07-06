using Spectre.Console;
using System.Text.RegularExpressions;

namespace Phonebook.ukpagrace.Utility
{
    internal class Validation
    {
        public bool IsPhoneNumberValid(string phoneNumber)
        {
            string pattern = @"^\d{3}-\d{3}-\d{4}$";
            return Regex.IsMatch(phoneNumber, pattern);
        }

        public bool IsEmailValid(string email)
        {
            string pattern = @"^[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,}$";
            return Regex.IsMatch(email, pattern);
        }
    }

    class UserInput
    {
        readonly Validation validation = new();
        public string Category()
        {
            var option = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("What Category [green]does the contact belong[/]?")
                    .AddChoices(new[] {
                            "Family", "Friends", "Work"
                    }));

            return option;
        }

        public string PhoneNumber()
        {
            var number = AnsiConsole.Ask<string>("Enter [green] phone number[/]? format 234-555-7890\"");
            while (!validation.IsPhoneNumberValid(number))
            {
                AnsiConsole.WriteLine("Invalid Phone Number");
                number = AnsiConsole.Ask<string>("Enter [green] phone number[/]? format 234-555-7890\"");
            }

            return number;
        }

        public string Email()
        {
            var email = AnsiConsole.Ask<string>("Enter [green] a vaild email[/]?");
            while (!validation.IsEmailValid(email))
            {
                Console.WriteLine("Email not valid");
                email = AnsiConsole.Ask<string>("Enter [green] a valid email[/]?");
            }
            return email;
        }
    }
}
