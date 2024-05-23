using PhoneBook.DouglasFir.Services;
using Spectre.Console;

namespace PhoneBook.DouglasFir.Application;

public class App
{
    private bool _running;
    private readonly UserInput _inputHandler;

    public App()
    {
        _running = true;

        // Setup database
        //_dbContext = new DatabaseContext();
        //DatabaseInitializer dbInitializer = new DatabaseInitializer(_dbContext);
        //dbInitializer.Initialize();

        // Initialize services
        _inputHandler = new UserInput();
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
            PromptForMenuOption();
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

    private void PromptForMenuOption()
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
                // TODO: Implement this feature
                Utilities.DisplayWarningMessage("This feature is not yet implemented.");
                _inputHandler.PauseForContinueInput();
                break;
            case MainMenuOption.AddNewContact:
                // TODO: Finish Implement this feature
                Utilities.DisplayWarningMessage("This feature is not yet implemented.");
                _inputHandler.PauseForContinueInput();
                break;
            case MainMenuOption.ManageContacts:
                // TODO: Implement this feature
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
