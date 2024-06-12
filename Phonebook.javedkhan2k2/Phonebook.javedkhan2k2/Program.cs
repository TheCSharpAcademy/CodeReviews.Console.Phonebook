
using Microsoft.Extensions.Configuration;
using Phonebook.UI;

var builder = new Microsoft.Extensions.Configuration.ConfigurationBuilder()
    .AddUserSecrets<Program>()
    .AddEnvironmentVariables();

var configuration = builder.Build();

var databaseUserId = configuration["DatabaseUserID"];
var databasePassword = configuration["DatabasePassword"];

if(databaseUserId == null || databasePassword == null)
{
    Console.WriteLine("Please create the secrects using dotnet addsecret\n");
    System.Environment.Exit(0);
}

ActionManager actionManager = new ActionManager(databaseUserId, databasePassword);
actionManager.RunApp();
