using Spectre.Console;
using System.Text.RegularExpressions;

namespace PhoneBook;

public class PhoneNumberValidator : IValidator
{
    public string ErrorMsg { get; set; } = "Phone Number format isn't valid.";

    public ValidationResult Validate(string? input)
    {
        Regex regex = new Regex(@"^\+[0-9]{7,15}$");
        if (regex.IsMatch(input))
        {
            return ValidationResult.Success();
        }
        else
        {
            return ValidationResult.Error("[red]Phone Number format isn't valid[/]");
        }
    }
}