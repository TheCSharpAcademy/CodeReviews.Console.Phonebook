using Microsoft.Extensions.Configuration;

namespace Phonebook.Console.Config;

public class AppConfig {
    public IConfigurationRoot Config { get; set; }
    public AppConfig() {
        Config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json").Build();
    }

    public string GetConnectionString() => Config.GetConnectionString("sqlServerString") ?? "No string found";
}