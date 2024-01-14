using System.Net.Mail;
using PhoneValidation = PhoneNumbers;

namespace PhoneBookProgram;

public class InputValidation
{
    public static string? ContactNameValidation(string input, List<Contact> contacts)
    {
        if(input.Length<=0)
            return "The contact name is empty";
        else if(input.Length >= PhoneBookContext.ContactNameLenght/2)
            return "The contact name is more than 50 caracters.";
        else if(contacts.Any(p => p.ContactName == input))
            return "The contact name you've entered already exists";
        return null;
    }

    public static string? CategoryNameValidation(string input)
    {
        if(input.Length >= PhoneBookContext.ContactCategoryLenght/2)
            return "The category name is more than 50 caracters.";
        return null;
    }

    public static string? EmailValidation(string email)
    {
        if(email.Contains(' '))
            return "You've entered an email with a blank space";

        if(MailAddress.TryCreate(email, out MailAddress? validEmail))
        {
            Console.WriteLine(validEmail.DisplayName);
            string localName = validEmail.User;
            string domainName = validEmail.Host;
            if (localName.Length <= PhoneBookContext.EmailLocalNameLenght/2 && 
                domainName.Length <= PhoneBookContext.EmailDomainNameLenght/2)
                return null;
        }
        return "You've entered an invalid email.";
    }

    public static string? PhoneNumberValidation(string phoneInput)
    {
        var phoneNumberUtil = PhoneValidation.PhoneNumberUtil.GetInstance();
        PhoneValidation.PhoneNumber phoneNumber;
        try
        {
            phoneNumber = phoneNumberUtil.Parse(phoneInput, null);
        }
        catch
        {
            return "You've entered an invalid phone number.";
        }

        if(!phoneNumberUtil.IsValidNumber(phoneNumber))
            return "You've entered an invalid phone number.";
        return null;
    }
}