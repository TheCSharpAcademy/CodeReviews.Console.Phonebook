using MailKit.Net.Smtp;
using MimeKit;

namespace PhoneBook.Services
{
    internal class EmailService
    {
        public static void SendEmail(string To, string message, string name, string Subject)
        {
            try
            {
                using var email = new MimeMessage();
                email.From.Add(new MailboxAddress("Gustas", "gustas@gmail.com"));
                email.To.Add(new MailboxAddress($"{name}", $"{To}"));

                email.Subject = $"{Subject}";

                var builder = new BodyBuilder()
                {
                    TextBody = message
                };

                email.Body = builder.ToMessageBody();

                using var smtp = new SmtpClient();

                smtp.Connect("sandbox.smtp.mailtrap.io", 587, false);

                smtp.Authenticate("your_username", "your_password");//You need to create mailtrap account and enter username and password that
                                                                    //are given in account to authenticate

                smtp.Send(email);
                smtp.Disconnect(true);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + ". Press any key to continue");
                Console.ReadLine();
            }

        }
    }
}
