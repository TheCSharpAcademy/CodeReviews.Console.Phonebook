using System.Net;
using System.Net.Mail;
using jollejonas.Phonebook.UserInput;



namespace jollejonas.Phonebook.Services;

public class EmailService
{

    public static void SendEmail(string email, string name)
    {
        var fromAddress = new MailAddress("jollevb@hotmail.com", "Jonas Jensen");
        var toAddress = new MailAddress(email, name);
        string fromPassword = Environment.GetEnvironmentVariable("EMAIL_PASSWORD");


        string subject = ContactInput.GetSubject();
        string body = ContactInput.GetBody();

        var smtp = new SmtpClient
        {
            Host = "smtp-mail.outlook.com",
            Port = 587,
            EnableSsl = true,
            DeliveryMethod = SmtpDeliveryMethod.Network,
            UseDefaultCredentials = false,
            Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
        };

        try
        {
            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body
            })
            {
                smtp.Send(message);
            }

            Console.WriteLine("Your email has been sent");
            Console.ReadKey();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while sending the email: {ex.Message}");
            Console.ReadKey();
        }
    }
}
