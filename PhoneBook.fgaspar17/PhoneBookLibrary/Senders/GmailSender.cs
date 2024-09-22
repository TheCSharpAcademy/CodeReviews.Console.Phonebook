using System.Net.Mail;

namespace PhoneBookLibrary;

public class GmailSender : ISender
{
    public string ToUser { get; set; }
    public string Receiver { get; set; }
    public string Subject { get; set; }

    public GmailSender(string toUser, string subject, string receiver)
    {
        ToUser = toUser;
        Subject = subject;
        Receiver = receiver;
    }

    public bool Send(string body)
    {
        if (string.IsNullOrEmpty(GlobalConfig.FromAddress) || string.IsNullOrEmpty(GlobalConfig.GmailAppPassword)) return false;
        var fromAddress = new MailAddress(GlobalConfig.FromAddress);
        var toAddress = new MailAddress(Receiver, ToUser);
        string fromPassword = GlobalConfig.GmailAppPassword;

        var smtp = new SmtpClient
        {
            Host = "smtp.gmail.com",
            Port = 587,
            EnableSsl = true,
            DeliveryMethod = SmtpDeliveryMethod.Network,
            UseDefaultCredentials = false,
            Credentials = new System.Net.NetworkCredential(fromAddress.Address, fromPassword)
        };
        using var message = new MailMessage(fromAddress, toAddress)
        {
            Subject = Subject,
            Body = body
        };

        try
        {
            smtp.Send(message);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return false;
        }

        return true;
    }
}