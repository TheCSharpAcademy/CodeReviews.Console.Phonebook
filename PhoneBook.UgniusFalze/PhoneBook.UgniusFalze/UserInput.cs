using PhoneBook.UgniusFalze.Utils;
using Spectre.Console;

namespace PhoneBook.UgniusFalze;

public static class UserInput
{
    public static string GetPhoneNumber(string message = "Please enter the new contacts phone number: ")
    {
        var phoneNumber = AnsiConsole.Prompt(
            new TextPrompt<string>(message)
                .Validate(phoneNumber =>
                {

                    if (Validator.IsValidPhoneNumber(phoneNumber))
                    {
                        return ValidationResult.Success();
                    }
                    else
                    {
                        return ValidationResult.Error("Phone number must all have digits and the length between 7 and 15");
                    }
                }));
        return phoneNumber;
    }

    public static string GetEmail(string message ="Please enter the new contacts email: ")
    {
        var email = AnsiConsole.Prompt(
            new TextPrompt<string>(message)
                .Validate(email =>
                {
                    if (Validator.IsValidEmail(email))
                    {
                        return ValidationResult.Success();
                    }
                    else
                    {
                        return ValidationResult.Error("Email must have a valid email format such as test@gmail.com");
                    }
                }));
        return email;
    }

    public static string GetName()
    {
        var contactName = AnsiConsole.Ask<string>("Please enter the new contacts name:");
        return contactName;
    }
}