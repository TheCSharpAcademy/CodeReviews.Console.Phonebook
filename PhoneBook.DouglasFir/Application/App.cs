using Microsoft.EntityFrameworkCore;
using Spectre.Console;
using PhoneBook.DouglasFir.Utilities;

namespace PhoneBook.DouglasFir.Application;

public class App
{
    private bool _running;
    
    public App()
    {
        _running = true;

        //// Setup database
        //_dbContext = new DatabaseContext();
        //DatabaseInitializer dbInitializer = new DatabaseInitializer(_dbContext);
        //dbInitializer.Initialize();

        //// Initialize services
        //_inputHandler = new InputHandler();
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
            new FigletText("Coding Tracker")
                .LeftJustified()
                .Color(Color.SeaGreen1_1));

        Utilities.Utilities.PrintNewLines(2);
    }

    private void PromptForSessionAction()
    {
        var selectedOption = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Select an option:")
                .PageSize(10)
                .AddChoices(Enum.GetNames(typeof(MainMenuOption))
                .Select(Utilities.Utilities.SplitCamelCase)));

        ExecuteSelectedOption(selectedOption);
    }

    private void ExecuteSelectedOption(string option)
    {
        switch (Enum.Parse<MainMenuOption>(option.Replace(" ", "")))
        {
            //case MainMenuOption.StartNewSession:
            //    AppStopwatchManager _appStopwatchManager = new AppStopwatchManager(_codingSessionDAO, _inputHandler);
            //    _appStopwatchManager.Run();
            //    break;
            //case MainMenuOption.LogManualSession:
            //    AppNewLogManager _appNewLogManager = new AppNewLogManager(_codingSessionDAO, _inputHandler);
            //    _appNewLogManager.Run();
            //    break;
            //case MainMenuOption.ViewAndEditPreviousSessions:
            //    AppSessionManager _appSessionManager = new AppSessionManager(_codingSessionDAO, _inputHandler);
            //    _appSessionManager.Run();
            //    break;
            //case MainMenuOption.ViewAndEditGoals:
            //    AppGoalManager _appGoalManager = new AppGoalManager(_codingGoalDAO, _inputHandler);
            //    _appGoalManager.Run();
            //    break;
            //case MainMenuOption.ViewReports:
            //    AppReportManager _appReportManager = new AppReportManager(_codingSessionDAO, _inputHandler);
            //    _appReportManager.Run();
            //    break;
            //case MainMenuOption.SeedDatabase:
            //    _dbSeeder.SeedDatabase();
            //    break;
            case MainMenuOption.Exit:
                CloseSession();
                break;
        }
    }

    private void CloseSession()
    {
        _running = false;
        AnsiConsole.Markup("[teal]Goodbye![/]");
        Utilities.Utilities.PrintNewLines(2);
    }
}
