using PhoneBook.Interfaces.Strategies;
using PhoneBook.Model;
using PhoneBook.Services;
using Spectre.Console;

namespace PhoneBook.Strategies.EditStrategies;

internal sealed class EmailEditStrategy : IContactEditStrategy
{
    public void Edit(Contact contact)
    {
        var prompt = new TextPrompt<string>(AskEmail)
        {
            Validator =
                input =>
                {
                    if (string.IsNullOrWhiteSpace(input))
                    {
                        return ValidationResult.Success();
                    }
                    
                    return ValidationService.IsValidEmail(input) ?
                            ValidationResult.Success() :
                            ValidationResult.Error();
                },
            
            ValidationErrorMessage = InvalidEmailAddress,
            AllowEmpty = true
        };
        
        contact.Email = AnsiConsole.Prompt(prompt);
    }
}