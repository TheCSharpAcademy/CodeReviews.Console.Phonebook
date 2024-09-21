using PhoneBook.Interfaces.Strategies;
using PhoneBook.Model;
using PhoneBook.Services;
using Spectre.Console;

namespace PhoneBook.Strategies.EditStrategies;

internal sealed class PhoneEditStrategy : IContactEditStrategy
{
    public void Edit(Contact contact)
    {
        var prompt = new TextPrompt<string>(AskPhone)
        {
            Validator =
                input =>
                {
                    if (string.IsNullOrWhiteSpace(input))
                    {
                        return ValidationResult.Success();
                    }

                    return ValidationService.IsValidPhoneNumber(input) ? 
                        ValidationResult.Success() : 
                        ValidationResult.Error();
                },
            
            ValidationErrorMessage = InvalidPhoneNumber,
            AllowEmpty = true
        };
        
        contact.PhoneNumber = AnsiConsole.Prompt(prompt);
    }
}