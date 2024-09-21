using PhoneBook.Interfaces.Strategies;
using PhoneBook.Model;
using Spectre.Console;

namespace PhoneBook.Strategies.EditStrategies;

internal sealed class FirstNameEditStrategy : IContactEditStrategy
{
    public void Edit(Contact contact)
    {
        var prompt = new TextPrompt<string>(AskFirstName)
            
        {
            Validator = 
                input => 
                    string.IsNullOrEmpty(input) ? 
                    ValidationResult.Error() : 
                    ValidationResult.Success(),
            
            ValidationErrorMessage = NameIsRequired,
            AllowEmpty = true,
        };
        
        contact.FirstName = AnsiConsole.Prompt(prompt);
    }
}