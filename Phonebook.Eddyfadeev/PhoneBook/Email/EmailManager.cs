using Microsoft.Extensions.Configuration;
using Spectre.Console;

namespace PhoneBook.Email;

/// <summary>
/// Manages email sending operations using the configuration provided.
/// </summary>
internal class EmailManager : IEmailManager
{
    private const string ServerSection = "EmailCredentials:Server";
    private const string PortSection = "EmailCredentials:Port";
    private const string LoginSection = "EmailCredentials:Login";
    private const string PasswordSection = "EmailCredentials:Password";
    private const int DefaultPort = 587;
    
    private readonly IConfiguration _connectionConfiguration;
    
    private readonly string _serverAddress;
    private readonly int _port;
    private readonly string _login;
    private readonly string _password;

    public EmailManager(IConfiguration configuration)
    {
        _connectionConfiguration = configuration;

        _serverAddress = ConfigureServerAddress();
        _port = ConfigurePort();
        _login = ConfigureLogin();
        _password = ConfigurePassword();
    }

    /// <summary>
    /// Creates and configures a new instance of the <see cref="SmtpClient"/> class
    /// using the server address, port, login credentials, and SSL settings defined
    /// in the <see cref="EmailManager"/> configuration.
    /// </summary>
    /// <returns>
    /// A fully configured <see cref="SmtpClient"/> instance ready for sending emails.
    /// </returns>
    public SmtpClient GetSmtpClient() =>
        new()
        {
            Host = _serverAddress,
            Port = _port,
            Credentials = new NetworkCredential(userName: _login, password: _password),
            EnableSsl = true
        };

    private string ConfigureServerAddress() => 
        _connectionConfiguration[ServerSection];

    private int ConfigurePort()
    {
        int configuredPort;
        
        try
        {
            string? port = _connectionConfiguration[PortSection];
            configuredPort = Convert.ToInt32(port);
        }
        catch (Exception)
        {
            AnsiConsole.MarkupLine($"[red]Error reading port from configuration. Defaulting to {DefaultPort}.[/]");
            configuredPort = DefaultPort;
        }
        
        return configuredPort;
    }

    private string ConfigureLogin() =>
        _connectionConfiguration[LoginSection];
    
    private string ConfigurePassword() =>
        _connectionConfiguration[PasswordSection];
}

internal interface IEmailManager
{
    SmtpClient GetSmtpClient();
}