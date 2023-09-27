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

    internal string MailFrom
    {
        get
        {
            return config["Mail:From"] ?? "phonebook@example.com";
        }
    }

    internal string MailHost
    {
        get
        {
            return config["Mail:Host"] ?? "127.0.0.1";
        }
    }

    internal int MailPort
    {
        get
        {
            string port = config["Mail:Port"] ?? "25";
            return int.Parse(port);
        }
    }

    internal string MailUsername
    {
        get
        {
            var username = config["Mail:Username"];
            if (!String.IsNullOrEmpty(username))
            {
                username = username.Replace("{MailUsername}", config["MailUsername"]);
            }
            return username ?? "";
        }
    }

    internal string MailPassword
    {
        get
        {
            var password = config["Mail:Password"];
            if (!String.IsNullOrEmpty(password))
            {
                password = password.Replace("{MailPassword}", config["MailPassword"]);
            }
            return password ?? "";
        }
    }

    internal string TwilioFrom
    {
        get
        {
            var from = config["Twilio:From"];
            if (!String.IsNullOrEmpty(from))
            {
                from = from.Replace("{TwilioFrom}", config["TwilioFrom"]);
            }
            return from ?? "";
        }
    }
    
    internal string TwilioAccountSid
    {
        get
        {
            var accountSid = config["Twilio:AccountSid"];
            if (!String.IsNullOrEmpty(accountSid))
            {
                accountSid = accountSid.Replace("{TwilioAccountSid}", config["TwilioAccountSid"]);
            }
            return accountSid ?? "";
        }
    }

    internal string TwilioAuthToken
    {
        get
        {
            var authToken = config["Twilio:AuthToken"];
            if (!String.IsNullOrEmpty(authToken))
            {
                authToken = authToken.Replace("{TwilioAuthToken}", config["TwilioAuthToken"]);
            }
            return authToken ?? "";
        }
    }
}