using Microsoft.EntityFrameworkCore;
using Phonebook.ConsoleApp.Views;
using Phonebook.Controllers;
using Phonebook.Data.Contexts;
using Spectre.Console;

namespace Phonebook.ConsoleApp;

/// <summary>
/// Main insertion point for the console application.
/// Configures the required application settings and launches the main menu view.
/// </summary>
internal class Program
{
    private static void Main()
    {
        try
        {
            // Create the SQL Server database context.
            var databaseContext = new SqlDatabaseContext();

            // Ensure database is created and any database migrations are performed.
            // NOTE: this does not generate migrations. It only actions them.
            InitialiseDatabase(databaseContext);
            
            // Create the required services.
            var phonebookController = new PhonebookController(databaseContext);

            // Show the main menu.
            var mainMenu = new MainMenuPage(phonebookController);
            mainMenu.Show();
        }
        catch (Exception exception)
        {
            MessagePage.Show("Error", exception);
        }
        finally
        {
            Environment.Exit(0);
        }
    }

    private static void InitialiseDatabase(SqlDatabaseContext databaseContext)
    {
        AnsiConsole.Status()
            .Spinner(Spinner.Known.Aesthetic)
            .Start("Performing database migrations. Please wait...", ctx =>
            {
                databaseContext.Database.Migrate();
            });
    }
}
