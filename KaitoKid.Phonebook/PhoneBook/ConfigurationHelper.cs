using Microsoft.Extensions.Configuration;

namespace PhoneBook
{
    internal class ConfigurationHelper
    {
        public static IConfiguration BuildConfiguration()
        {
            var builder = new ConfigurationBuilder().AddUserSecrets<Program>();
            return builder.Build();
        }
    }
}
