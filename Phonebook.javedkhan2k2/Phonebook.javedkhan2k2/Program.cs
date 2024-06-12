
using Microsoft.Extensions.Configuration;
using Phonebook.UI;

var builder = new Microsoft.Extensions.Configuration.ConfigurationBuilder()
    .AddUserSecrets<Program>()
    .AddEnvironmentVariables();

var configuration = builder.Build();

var databaseUserId = configuration["DatabaseUserID"];
var databasePassword = configuration["DatabasePassword"];

ActionManager actionManager = new ActionManager(databaseUserId, databasePassword);
actionManager.RunApp();
