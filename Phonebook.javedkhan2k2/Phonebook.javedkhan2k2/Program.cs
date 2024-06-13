using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Phonebook.UI;
using Phonebook.Data;

var builder = new ConfigurationBuilder()
    .AddUserSecrets<Program>()
    .AddEnvironmentVariables();

var configuration = builder.Build();

var databaseUserId = configuration["DatabaseUserID"];
var databasePassword = configuration["DatabasePassword"];

if (databaseUserId == null || databasePassword == null)
{
    Console.WriteLine("Please create the secrets using dotnet user-secrets set");
    System.Environment.Exit(0);
}

var serviceProvider = new ServiceCollection()
    .AddSingleton(new PhonebookDbContext(databaseUserId, databasePassword))
    .BuildServiceProvider();

var dbContext = serviceProvider.GetRequiredService<PhonebookDbContext>();

ActionManager actionManager = new ActionManager(dbContext);
actionManager.RunApp();
