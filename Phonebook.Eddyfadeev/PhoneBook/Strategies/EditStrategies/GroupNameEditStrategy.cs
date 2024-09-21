using PhoneBook.Interfaces.Strategies;
using PhoneBook.Model;
using Spectre.Console;

namespace PhoneBook.Strategies.EditStrategies;

internal sealed class GroupNameEditStrategy : IContactEditStrategy
{
    public void Edit(Contact contact)
    {
        var prompt = new TextPrompt<string>(AskGroupName)
        {
            AllowEmpty = true
        };
        
        contact.Group = AnsiConsole.Prompt(prompt);
    }
}