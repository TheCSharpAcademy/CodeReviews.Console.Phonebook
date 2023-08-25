namespace PhoneBookCarDioLogics;
using System.Net;
using System.Net.Mail;
internal class EmailService
{
    internal static SmtpClient SetEmailClient(string emailDomain, string emailSender, string emailSenderPassword)
    {
        SmtpClient smtpClient = new SmtpClient();

        if (emailDomain == "gmail.com")
        {
            smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(emailSender, emailSenderPassword),
                EnableSsl = true,
            };
        }
        else if (emailDomain == "hotmail.com")
        {
            smtpClient = new SmtpClient("smtp.office365.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(emailSender, emailSenderPassword),
                EnableSsl = true,
            };
        }
        else
        {
            Console.WriteLine("Sorry! Email domain not supported!");
            Console.ReadLine();
        }

        return smtpClient;
    }
}
