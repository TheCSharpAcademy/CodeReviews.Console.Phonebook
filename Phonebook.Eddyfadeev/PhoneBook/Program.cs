using Microsoft.Extensions.DependencyInjection;
using PhoneBook.Enums;
using PhoneBook.Exceptions;
using PhoneBook.Handlers;
using PhoneBook.Interfaces.Handlers;
using PhoneBook.Services;
using Spectre.Console;

namespace PhoneBook;

internal static class Program
{
    static void Main(string[] args)
    {
        var services = DependenciesConfigurator.ServiceCollection;
        var serviceProvider = services.BuildServiceProvider();
        
        var menuHandler = serviceProvider.GetRequiredService<MenuHandler<MainMenu>>();
        
        Start(menuHandler);
    }

    private static void Start(IMenuHandler menuHandler)
    {
        while (true)
        {
            Console.Clear();
            try
            {
                menuHandler.HandleMenu();
            }
            catch (ReturnToMainMenuException e)
            {
                // Do nothing
            }
            catch (ExitApplicationException e)
            {
                AnsiConsole.MarkupLine(e.Message);
                break;
            }
            catch (Exception e)
            {
                AnsiConsole.MarkupLine(e.Message);
                HelperService.PressAnyKey();
            }
        }
        
    }
}