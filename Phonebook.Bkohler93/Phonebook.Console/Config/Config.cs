using Microsoft.Extensions.Configuration;
using Phonebook.Console.UserInterface;
using Spectre.Console;

namespace Phonebook.Console.Config;

public class AppConfig {
    public IConfigurationRoot Config { get; set; }
    public EmailConfiguration? EmailConfig { get; set; }
    public AppConfig() {
        Config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .AddJsonFile("appsettings.development.json").Build();

        EmailConfig = new EmailConfiguration{
            Email = Config.GetSection("EmailCredentials").GetSection("Email").Value ?? "",
            Password = Config.GetSection("EmailCredentials").GetSection("Password").Value ?? "", 
        };
    }

    public string GetConnectionString() => Config.GetConnectionString("sqlServerString") ?? "No string found";
    public string GetEmailUsername() => EmailConfig?.Email ?? "No email string found";
    public string GetEmailPassword() => EmailConfig?.Password ?? "No email password found";
}

public class EmailConfiguration{
    public required string Email { get; set; }
    public required string Password { get; set; }
}