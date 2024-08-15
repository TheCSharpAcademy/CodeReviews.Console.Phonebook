using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using jollejonas.Phonebook.Data;
using jollejonas.Phonebook.Services;
using Microsoft.Extensions.Logging;

var host = CreateHostBuilder(args).Build();

using (var scope = host.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    try
    {
        var context = services.GetRequiredService<PhonebookContext>();
        var phonebookService = services.GetRequiredService<PhonebookService>();
        phonebookService.Run();
    }
    catch (Exception ex)
    {
        Console.WriteLine($"An error occurred while migrating the database: {ex.Message}");
    }
}

static IHostBuilder CreateHostBuilder(string[] args) =>
    Host.CreateDefaultBuilder(args)
        .ConfigureAppConfiguration((context, config) =>
        {
            config.AddJsonFile("appsettings.json");
        })
        .ConfigureLogging(logging =>
        {
            logging.ClearProviders();
            logging.AddConsole();
            logging.AddFilter("Microsoft.EntityFrameworkCore.Database.Command", LogLevel.Warning);
        })
        .ConfigureServices((context, services) =>
        {
            services.AddDbContext<PhonebookContext>(options =>
                options.UseSqlServer(context.Configuration.GetConnectionString("PhonebookContext")));
            services.AddTransient<PhonebookService>();
        });
