using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Phonebook.UI;
using Phonebook.Data;
using Phonebook.Validators;

var builder = new ConfigurationBuilder()
    .AddUserSecrets<Program>();

var configuration = builder.Build();

var databaseUserId = configuration["DatabaseUserID"];
var databasePassword = configuration["DatabasePassword"];

if (ValidatorHelper.IsValidConfiguration(configuration))
{
    var serviceProvider = new ServiceCollection()
        .AddSingleton(new PhonebookDbContext(configuration["DatabaseUserID"], configuration["DatabasePassword"]))
        .BuildServiceProvider();

    ActionManager actionManager = new ActionManager(configuration);
    actionManager.RunApp();
}
