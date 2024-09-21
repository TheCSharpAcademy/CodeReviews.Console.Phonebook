using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PhoneBook.Database;
using PhoneBook.Database.ContactContext;
using PhoneBook.Email;
using PhoneBook.Enums;
using PhoneBook.Handlers;
using PhoneBook.Handlers.ContactHandlers;
using PhoneBook.Interfaces.Database;
using PhoneBook.Interfaces.Handlers;
using PhoneBook.Interfaces.Handlers.ContactHandlers;
using PhoneBook.Interfaces.Menu;
using PhoneBook.Interfaces.Menu.Factory;
using PhoneBook.Interfaces.Menu.Factory.Initializer;
using PhoneBook.Interfaces.Repository;
using PhoneBook.Interfaces.Services;
using PhoneBook.Menu;
using PhoneBook.Menu.Commands.ManageMenuCommands;
using PhoneBook.Menu.Factory;
using PhoneBook.Menu.Factory.Initializers;
using PhoneBook.Model;
using PhoneBook.Repository;

namespace PhoneBook.Services;

/// <summary>
/// Configures various dependencies for the DI
/// </summary>
internal static class DependenciesConfigurator
{
    private const string JsonFileName = "appsettings.json";

    private static IConfiguration Configuration { get; }
    internal static ServiceCollection ServiceCollection { get; } = new();
    
    static DependenciesConfigurator()
    { 
        Configuration = GetConfiguration().Build();
        ConfigureServices(ServiceCollection);
    }

    private static void ConfigureServices(IServiceCollection services)
    {
        services.AddSingleton<IConfiguration>(Configuration);
        services.AddDbContext<ContactContext>();

        services.AddTransient<IEmailManager, EmailManager>();
        services.AddTransient<IEmailSender, EmailSender>();
        
        services.AddSingleton<IDatabaseManager, DatabaseManager>();
        services.AddSingleton<IRepository<Contact>, ContactRepository>();
        
        services.AddTransient<IMenuEntriesInitializer<MainMenu>, MainMenuEntries>();
        services.AddTransient<IMenuEntriesInitializer<ManageMenu>, ManageMenuEntries>();

        services.AddTransient<IMenuCommandsFactory<MainMenu>, MenuCommandsFactory<MainMenu>>();
        services.AddTransient<IMenuCommandsFactory<ManageMenu>, MenuCommandsFactory<ManageMenu>>();

        services.AddTransient<IMenuEntries, MenuEntries>();
        services.AddTransient<IDynamicEntriesHandler, DynamicEntriesHandler>();

        services.AddSingleton<MenuHandler<MainMenu>>();
        services.AddSingleton<MenuHandler<ManageMenu>>();
        
        services.AddSingleton<IMenuHandler>(provider => provider.GetRequiredService<MenuHandler<MainMenu>>());
        services.AddSingleton<IMenuHandler>(provider => provider.GetRequiredService<MenuHandler<ManageMenu>>());

        services.AddTransient<IContactTableConstructor, ContactTableConstructor>();

        services.AddTransient<IContactSelector, ContactSelector>();
        services.AddTransient<IContactAdder, ContactAdder>();
        services.AddTransient<IContactUpdater, ContactUpdater>();
        services.AddTransient<IContactDeleter, ContactDeleter>();
    }

    private static IConfigurationBuilder GetConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile(JsonFileName, optional: false, reloadOnChange: true);

        return builder;
    }
}