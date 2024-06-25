using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Protocols.Configuration;

namespace Program;

public class ConfigManager
{
    private static IConfiguration? _config;

    public static IConfiguration Config
    {
        get
        {
            if (_config == null)
            {
                var builder = new ConfigurationBuilder()
                    .AddJsonFile("appsettings.json", optional: false);

                _config = builder.Build();
            }

            return _config ?? throw new InvalidConfigurationException();
        }

        set
        {
            _config = value;
        }
    }

    public static IConfiguration Database
    {
        get
        {

            return Config.GetSection("Database");

        }
    }
}