using System.Net.Mail;

namespace PhoneBookProgram;

public class Email
{
    public int ContactId {get; set;}
    public int EmailId {get; set;}
    public string? LocalName {get; set;} 
    public string? DomainName {get; set;}

    public static Email FromCsv(string emailLine, int contactId)
    {
        string[] data = emailLine.Split(',');
        string? errorMessage = InputValidation.EmailValidation(data[1]);
    
        if(errorMessage == null)
        { 
            var validEmail = new MailAddress(data[1]);
            var email = new Email
            { 
                ContactId = contactId,
                LocalName = validEmail.User,
                DomainName = validEmail.Host
            };
            return email;
        }
        else
            throw new Exception($"Error: Invalid Email \"{data[1]}\". {errorMessage}");
    }

    public string? GetEmail()
    {
        return $"{LocalName}@{DomainName}";
    }
}