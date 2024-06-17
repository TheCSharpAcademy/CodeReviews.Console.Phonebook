using Microsoft.IdentityModel.Tokens;
using Spectre.Console;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Model;

public class Validation
{
    public string GetValidName(string question)
    {
        var validator = new NameValidation();

        while (true)
        {
            var input = AnsiConsole.Ask<string>(question);
            var result = validator.Validate(new Contact { Name = input });

            if (!result.IsValid)
            {
                DisplayValidationErrors(result);
            }
            else
            {
                return input;
            }
        }
    }

    public string GetValidEmail(string question)
    {
        var validator = new EmailValidation();

        while (true)
        {
            var input = AnsiConsole.Ask<string>(question);
            var result = validator.Validate(new Email { EmailAddress = input });

            if (!result.IsValid)
            {
                DisplayValidationErrors(result);
            }
            else
            {
                return input;
            }
        }
    }

    public string GetValidPhoneNumber(string question)
    {
        var validator = new PhoneValidation();

        while (true)
        {
            var input = AnsiConsole.Ask<string>(question);
            var result = validator.Validate(new PhoneNumber { Number = input });

            if (!result.IsValid)
            {
                DisplayValidationErrors(result);
            }
            else
            {
                return input;
            }
        }
    }

    public List<Email> AddEmails()
    {
        var emails = new List<Email>();
        while (AnsiConsole.Confirm("Would you like to add an email address?"))
        {
            var email = GetValidEmail("Add a E-Mail Address");
            emails.Add(new Email { EmailAddress = email });
        }
        return emails;
    }

    public List<PhoneNumber> AddPhoneNumber()
    {
        var phoneNumbers = new List<PhoneNumber>();
        var confirm = AnsiConsole.Confirm("Would you like to add an Phone Number?");

        while (confirm || phoneNumbers.Count < 1)
        {
            if (!confirm)
            {
                AnsiConsole.MarkupLine($"[red] A contact must have at least one phone number connected to it. [/]");
            }

            var phoneNumber = GetValidPhoneNumber("Add a phone number");
            phoneNumbers.Add(new PhoneNumber { Number = phoneNumber });
        }
        return phoneNumbers;
    }

    private void DisplayValidationErrors(FluentValidation.Results.ValidationResult result)
    {
        foreach (var error in result.Errors)
        {
            AnsiConsole.MarkupLine($"[red]{error.ErrorMessage}[/]");
        }
    }
}
