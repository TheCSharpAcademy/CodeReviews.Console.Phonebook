using MailKit.Net.Smtp;
using MimeKit;
using Phonebook.kwm0304.Models;
using Spectre.Console;

namespace Phonebook.kwm0304.Services;

public class EmailService
{
  private static readonly Dictionary<string, (string server, int port)> KnownProviders = new()
    {
        {"gmail.com", ("smtp.gmail.com", 587)},
        {"outlook.com", ("smtp-mail.outlook.com", 587)},
        {"hotmail.com", ("smtp-mail.outlook.com", 587)},
        {"yahoo.com", ("smtp.mail.yahoo.com", 587)},
    };
  
  public static (string server, int port) GetServer(string emailAddress)
  {
    string domain = emailAddress.Split('@')[1].ToLower();
    if (KnownProviders.TryGetValue(domain, out var serverInfo))
    {
      return serverInfo;
    }
    throw new ArgumentException("Unknown email provider");
  }
  public async Task? CreateMessage(Contact contact)
  {

    string from = GetAndConfirm("What is your email address?");
    string password = GetAndConfirm("What is your email password?");
    var (server, port) = GetServer(from);
    string to = contact.ContactEmail!;
    string header = GetAndConfirm("What should the header of your email be?");
    string body = GetAndConfirm("What should the body of your email say?");

    var message = new MimeMessage();
    message.From.Add(new MailboxAddress("", from));
    message.To.Add(new MailboxAddress("", to));
    message.Subject = header;
    message.Body = new TextPart("plain")
    {
      Text = body
    };
    using var client = new SmtpClient();
    try
    {
      await client.ConnectAsync(server, port, MailKit.Security.SecureSocketOptions.StartTls);
      await client.AuthenticateAsync(from, password);
      await client.SendAsync(message);
      await client.DisconnectAsync(true);
      AnsiConsole.MarkupLine($"[bold lime]Message successfully sent to {to}[/]");
    }
    catch (Exception e)
    {
      AnsiConsole.WriteException(e);
      Thread.Sleep(4000);
    }
  }
  public static string GetAndConfirm(string question)
  {
    string answer = AnsiConsole.Ask<string>(question);
    bool isConfirmed = AnsiConsole.Confirm($"Is this correct?\n\n {answer}");
    if (isConfirmed)
    {
      return answer;
    }
    return GetAndConfirm(question);
  }
}