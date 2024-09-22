using Spectre.Console;
using System.Text.RegularExpressions;

namespace PhoneBook;

public class EmailValidator : IValidator
{
    public string ErrorMsg { get; set; } = "Email format isn't valid.";

    public ValidationResult Validate(string? input)
    {
        Regex regex = new Regex(@"^(?!.*\.\.)[a-zA-Z0-9!#$%&'*+/=?\^_`{|}~\.\-]{1,64}@[a-zA-Z0-9\-]{1,63}(\.[a-zA-Z]{2,63})+$");
        if (regex.IsMatch(input))
        {
            return ValidationResult.Success();
        }
        else
        {
            return ValidationResult.Error("[red]Email format isn't valid[/]");
        }
    }
}