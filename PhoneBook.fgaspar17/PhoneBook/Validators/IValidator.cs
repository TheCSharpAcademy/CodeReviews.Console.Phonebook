using Spectre.Console;

namespace PhoneBook;

public interface IValidator
{
    string ErrorMsg { get; set; }
    ValidationResult Validate(string? input);
}