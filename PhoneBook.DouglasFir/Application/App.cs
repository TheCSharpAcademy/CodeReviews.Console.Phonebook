using Microsoft.EntityFrameworkCore;
using Spectre.Console;
using PhoneBook.DouglasFir.Services;

namespace PhoneBook.DouglasFir.Application;

public class App
{
    private bool _running;
    private readonly InputHandler _inputHandler;

    public App()
    {
        _running = true;

        //// Setup database
        //_dbContext = new DatabaseContext();
        //DatabaseInitializer dbInitializer = new DatabaseInitializer(_dbContext);
        //dbInitializer.Initialize();

        // Initialize services
        _inputHandler = new InputHandler();
        //_codingGoalDAO = new CodingGoalDao(_dbContext);
        //_codingSessionDAO = new CodingSessionDao(_dbContext, _codingGoalDAO);
        //_dbSeeder = new DatabaseSeeder(_codingSessionDAO, _codingGoalDAO, _inputHandler);
    }

    public void Run()
    {
        while (_running)
        {
            AnsiConsole.Clear();
            DisplayMainScreenBanner();
            PromptForSessionAction();
        }
    }

    private void DisplayMainScreenBanner()
    {
        AnsiConsole.Write(
            new FigletText("Robodex")
                .LeftJustified()
                .Color(Color.SeaGreen1_1));
        Utilities.PrintNewLines(2);
    }

    private void PromptForSessionAction()
    {
        var selectedOption = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Select an option:")
                .PageSize(10)
                .AddChoices(Enum.GetNames(typeof(MainMenuOption))
                .Select(Utilities.SplitCamelCase)));

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
                Utilities.DisplayWarningMessage("This feature is not yet implemented.");
                _inputHandler.PauseForContinueInput();
                break;
            case MainMenuOption.AddNewContanct:
                Utilities.DisplayWarningMessage("This feature is not yet implemented.");
                _inputHandler.PauseForContinueInput();
                break;
            case MainMenuOption.ManageContacts:
                Utilities.DisplayWarningMessage("This feature is not yet implemented.");
                _inputHandler.PauseForContinueInput();
                break;
            case MainMenuOption.ManageContactGroups:
                Utilities.DisplayWarningMessage("This feature is not yet implemented.");
                _inputHandler.PauseForContinueInput();
                break;
            case MainMenuOption.SencMessageToContact:
                Utilities.DisplayWarningMessage("This feature is not yet implemented.");
                _inputHandler.PauseForContinueInput();
                break;
            case MainMenuOption.SeedDatabase:
                Utilities.DisplayWarningMessage("This feature is not yet implemented.");
                _inputHandler.PauseForContinueInput();
                break;
        }
    }

    private void CloseSession()
    {
        _running = false;
        AnsiConsole.Markup("[teal]Goodbye![/]");
        Utilities.PrintNewLines(2);
    }
}
