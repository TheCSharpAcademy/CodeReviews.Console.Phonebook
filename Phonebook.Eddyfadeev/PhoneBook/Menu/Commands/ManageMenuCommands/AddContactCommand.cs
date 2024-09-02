using PhoneBook.Interfaces.Handlers.ContactHandlers;
using PhoneBook.Interfaces.Menu.Command;
using PhoneBook.Services;
using Spectre.Console;

namespace PhoneBook.Menu.Commands.ManageMenuCommands;

internal class AddContactCommand : ICommand
{
    private readonly IContactAdder _contactAdder;
    
    public AddContactCommand(IContactAdder contactAdder)
    {
        _contactAdder = contactAdder;
    }
    
    public void Execute()
    {
        _contactAdder.AddContact(out var message);
        
        AnsiConsole.MarkupLine(message ?? ProblemWithCommand);
        HelperService.PressAnyKey();
    }
}