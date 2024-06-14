using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Phonebook.UI;
using Phonebook.Data;
using Phonebook.Validators;

var builder = new ConfigurationBuilder()
    .AddUserSecrets<Program>();

var configuration = builder.Build();

if (ValidatorHelper.IsValidConfiguration(configuration))
{
    ActionManager actionManager = new ActionManager(configuration);
    actionManager.RunApp();
}
