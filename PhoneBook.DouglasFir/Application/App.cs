using PhoneBook.DouglasFir.Data;
using PhoneBook.DouglasFir.Handlers;
using PhoneBook.DouglasFir.Services;
using PhoneBook.DouglasFir.Utilities;
using Spectre.Console;

namespace PhoneBook.DouglasFir.Application;

public class App
{
    private bool _running;
    private ContactCommandHandler _contactCommandHandler;
    private ContactService _contactService;
    private PhoneBookContext _context;

    public App()
    {
        _running = true;
        _context = new PhoneBookContext();
        _contactService = new ContactService(_context);
        _contactCommandHandler = new ContactCommandHandler(_contactService);
    }

    public void Run()
    {
        while (_running)
        {
            AnsiConsole.Clear();
            DisplayMainScreenBanner();
            PromptForMenuOption();
        }
    }

    private void DisplayMainScreenBanner()
    {
        AnsiConsole.Write(
            new FigletText("Robodex")
                .LeftJustified()
                .Color(Color.SeaGreen1_1));
        Util.PrintNewLines(2);
    }

    private void PromptForMenuOption()
    {
        var selectedOption = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Select an option:")
                .PageSize(10)
                .AddChoices(Enum.GetNames(typeof(MainMenuOption))
                .Select(Util.SplitCamelCase)));

        ExecuteSelectedOption(selectedOption);
    }

    private void ExecuteSelectedOption(string option)
    {
        switch (Enum.Parse<MainMenuOption>(option.Replace(" ", "")))
        {
            case MainMenuOption.Exit:
                CloseSession();
                break;
            case MainMenuOption.ViewContacts:
                _contactCommandHandler.HandleViewContacts();
                UserInput.PauseForContinueInput();
                break;
            case MainMenuOption.AddNewContact:
                _contactCommandHandler.HandleAddContact();
                UserInput.PauseForContinueInput();
                break;
            case MainMenuOption.UpdateContact:
                _contactCommandHandler.HandleUpdateContact();
                UserInput.PauseForContinueInput();
                break;
            case MainMenuOption.DeleteContact:
                _contactCommandHandler.HandleDeleteContact();
                UserInput.PauseForContinueInput();
                break;
        }
    }

    private void CloseSession()
    {
        _running = false;
        AnsiConsole.Markup("[teal]Goodbye![/]");
        Util.PrintNewLines(2);
    }
}
