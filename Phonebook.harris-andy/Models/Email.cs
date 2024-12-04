namespace Phonebook.Models
{
    public class Email
    {
        public string FromAddress { get; set; } = string.Empty;

        public string ToAddress { get; set; } = string.Empty;

        public string Subject { get; set; } = string.Empty;

        public string Body { get; set; } = string.Empty;

        public Email(string fromAddress, string toAddress, string subject, string body)
        {
            FromAddress = fromAddress;
            ToAddress = toAddress;
            Subject = subject;
            Body = body;
        }
    }
}