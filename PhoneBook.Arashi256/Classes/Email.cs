using Microsoft.IdentityModel.Tokens;
using PhoneBook.Arashi256.Config;
using PhoneBook.Arashi256.Models;
using System.Net.Mail;

namespace PhoneBook.Arashi256.Classes
{
    internal class Email
    {
        private EmailConnection _emailConnection;
        private SmtpClient _smtpClient;
        private string? _smtpServer;
        private string? _smtpUsername;
        private string? _smtpPassword;
        private int _smtpPort;
        private bool _smtpSSLEnabled;
        private string _recipietEmail;
        private string _emailSubject;
        private string _emailContent;

        public Email() 
        {
            _emailConnection = new EmailConnection();
            _smtpServer = _emailConnection.SmtpServer;
            _smtpUsername = _emailConnection.SmtpUser;
            _smtpPassword = _emailConnection.SmtpPassword;
            _smtpPort = _emailConnection.SmtpPort;
            _smtpSSLEnabled = _emailConnection.SmtpSsl;
        }

        public bool CheckValidSettings()
        {
            if (_smtpServer.IsNullOrEmpty() || _smtpUsername.IsNullOrEmpty() || _smtpPassword.IsNullOrEmpty() || _smtpPort == 0) return false;
            else return true;
        }

        public bool Init()
        {
            if (!CheckValidSettings()) return false;
            try
            {
                _smtpClient = new SmtpClient(_smtpServer);
                _smtpClient.UseDefaultCredentials = false;
                System.Net.NetworkCredential basicAuthenticationInfo = new System.Net.NetworkCredential(_smtpUsername, _smtpPassword);
                _smtpClient.Credentials = basicAuthenticationInfo;
                _smtpClient.Port = _smtpPort;
                _smtpClient.EnableSsl = _smtpSSLEnabled;
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }   
        }

        public bool SendEmail(string senderName, string senderEmail, ContactDto contact, string subject, string content)
        {
            try
            {
                MailAddress from = new MailAddress(senderEmail, senderName);
                MailAddress to = new MailAddress(contact.Email, contact.Name);
                MailMessage myMail = new System.Net.Mail.MailMessage(from, to);
                MailAddress replyTo = new MailAddress(senderEmail);
                myMail.ReplyToList.Add(replyTo);
                myMail.Subject = subject;
                myMail.SubjectEncoding = System.Text.Encoding.UTF8;
                myMail.Body = content;
                myMail.BodyEncoding = System.Text.Encoding.UTF8;
                myMail.IsBodyHtml = false;
                // Send.
                if (_smtpClient != null)
                {
                    _smtpClient.Send(myMail);
                    return true;
                }
                else
                    return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}