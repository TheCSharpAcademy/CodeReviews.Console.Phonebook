using Console.Phonebook.Controllers;
using Console.Phonebook.Data;
using Console.Phonebook.Services;
using Console.Phonebook.View;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Console.Phonebook;

internal class Program : ConsoleController
{
    static void Main(string[] args)
    {
        IConfiguration config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        string? connectionString = config["ConnectionStrings:DefaultConnection"];
        if (connectionString == null)
        {
            ErrorMessage("Dear user, please ensure that you have your connection string set up in the appsettings.json.");
            return;
        }

        var optionsBuilder = new DbContextOptionsBuilder<DataContext>();
        optionsBuilder.UseSqlServer(connectionString);

        using var context = new DataContext(optionsBuilder.Options);
        context.Database.Migrate();

        var contactService = new ContactService(context);

        UserInterface userInterface = new UserInterface(contactService);

        userInterface.MainMenu();
    }
}