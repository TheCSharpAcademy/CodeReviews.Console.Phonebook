using PhoneBook.Interfaces.Strategies;
using PhoneBook.Model;
using Spectre.Console;

namespace PhoneBook.Strategies.EditStrategies;

internal sealed class LastNameEditStrategy : IContactEditStrategy
{
    public void Edit(Contact contact)
    {
        var prompt = new TextPrompt<string>(AskLastName)
        {
            AllowEmpty = true
        };
        
        contact.LastName = AnsiConsole.Prompt(prompt);
    }
}