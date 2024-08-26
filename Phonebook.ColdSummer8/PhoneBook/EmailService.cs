using System.Net;
using System.Net.Mail;

namespace PhoneBook;
internal class EmailService
{
    public static void SendEmail(string toEmail, string subject, string body)
    {
        string? email = Environment.GetEnvironmentVariable("GMAIL_EMAIL");
        string? password = "oqsr exgq fhqh mtvf";

        try
        {
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(email);
            mail.To.Add(toEmail);
            mail.Subject = subject;
            mail.Body = body;

            SmtpClient client = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(email, password),
                EnableSsl = true,
                UseDefaultCredentials = false,
                DeliveryMethod = SmtpDeliveryMethod.Network
            };

            client.Send(mail);
            Console.WriteLine("Email sent!");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }
    }
}
