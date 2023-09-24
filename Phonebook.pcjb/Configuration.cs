namespace PhoneBook;

using Microsoft.Extensions.Configuration;

internal sealed class Configuration
{
    private static Configuration? instance;
    private readonly IConfigurationRoot config;

    private Configuration()
    {
        config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .AddUserSecrets<Program>()
            .Build();
    }

    public static Configuration GetInstance()
    {
        if (instance == null)
        {
            instance = new Configuration();

        }
        return instance;
    }

    internal string? DatabaseConnectionString
    {
        get
        {
            var connString = config["DatabaseConnectionString"];
            if (!String.IsNullOrEmpty(connString))
            {
                connString = connString.Replace("{DatabaseUserID}", config["DatabaseUserID"]);
                connString = connString.Replace("{DatabasePassword}", config["DatabasePassword"]);
            }
            return connString;
        }
    }
}