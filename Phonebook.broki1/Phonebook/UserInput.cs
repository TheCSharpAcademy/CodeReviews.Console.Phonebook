using Phonebook.Model;
using PhoneNumbers;
using System.Configuration;
using System.Globalization;
using System.Net.Mail;
using System.Reflection;

namespace Phonebook;

internal class UserInput
{
    internal static string GetMainMenuInput()
    {
        Console.WriteLine(new String('-', 10));
        Console.WriteLine("PHONEBOOK");
        Console.WriteLine(new String('-', 10));

        Console.WriteLine("\n1. add contact\n2. view contact\n3. update contact\n4. delete contact\n0. Exit app\n\nchoose option:");

        var mainMenuInput = Console.ReadLine().Trim();

        while (!ValidationEngine.ValidMainMenuInput(mainMenuInput))
        {
            Console.WriteLine("\ninvalid input, try again:");
            mainMenuInput = Console.ReadLine().Trim();
        }

        return mainMenuInput;
    }

    internal static string GetName()
    {
        Console.Clear();

        Console.WriteLine("enter name:");

        var name = Console.ReadLine().Trim();

        while (!ValidationEngine.ValidName(name))
        {
            Console.WriteLine("\ninvalid input, enter name:");
            name = Console.ReadLine().Trim();
        }

        return name;
    }

    internal static string GetEmail()
    {
        Console.Clear();
        Console.WriteLine("enter email:");

        var email = Console.ReadLine().Trim();

        while (!ValidationEngine.ValidEmail(email))
        {
            Console.WriteLine("\ninvalid input, enter email:");
            email = Console.ReadLine().Trim();
        }

        return email;
    }

    internal static string GetPhoneNumber()
    {
        Console.Clear();
        Console.WriteLine("enter 2-letter country code ('US', 'CH', 'DE', etc.):");
        var countryCode = Console.ReadLine().Trim().ToUpper();
        
        while (!ValidationEngine.ValidCountryCode(countryCode))
        {
            Console.WriteLine("\ninvalid input, enter 2-letter country code ('US', 'CH', 'DE', etc.):");
            countryCode = Console.ReadLine().Trim().ToUpper();
        }

        Console.WriteLine("enter phone number:");
        var phoneNumberInput = Console.ReadLine().Trim();

        while (!ValidationEngine.ValidPhoneNumber(phoneNumberInput, countryCode))
        {
            Console.WriteLine("\ninvalid input, enter phone number:");
            phoneNumberInput = Console.ReadLine().Trim();
        }

        var phoneNumberUtil = PhoneNumberUtil.GetInstance();
        var phoneNumber = phoneNumberUtil.Parse(phoneNumberInput, countryCode);

        return phoneNumberUtil.Format(phoneNumber, PhoneNumberFormat.INTERNATIONAL);
    }

    internal static string GetReadInput()
    {
        Console.Clear();
        Console.WriteLine("search contact:");

        var contactInfo = Console.ReadLine().Trim();

        return contactInfo;
    }

    internal static int GetContactID()
    {
        Console.WriteLine("\nenter ID of contact:");
        var contactID = Console.ReadLine().Trim();

        while (!ValidationEngine.ValidID(contactID))
        {
            Console.WriteLine("\ninvalid input, enter ID of contact:\n");
            contactID = Console.ReadLine().Trim();
        }

        return int.Parse(contactID);
    }

    internal static void GetUpdate(Contact contact)
    {
        foreach (PropertyInfo prop in contact.GetType().GetProperties())
        {
            Console.Clear();
            var propName = prop.Name;
            var propValue = prop.GetValue(contact);

            if (propName == "Id")
            {
                continue;
            }

            Console.WriteLine($"{propName}: {propValue}\n\nupdate? press '1' to confirm, press '0' to make no changes");
            var userInput = UserInput.GetUpdateChoice();

            if (userInput == "0")
            {
                continue;
            }

            if (propName == "Name")
            {
                contact.Name = UserInput.GetName();
            }
            else if (propName == "Email")
            {
                contact.Email = UserInput.GetEmail();
            }
            else
            {
                contact.PhoneNumber = UserInput.GetPhoneNumber();
            }
        }
    }

    internal static string GetUpdateChoice()
    {
        var userChoice = Console.ReadLine().Trim();

        while (userChoice != "0" && userChoice != "1")
        {
            Console.WriteLine("\ninvalid input, press '1' to confirm, press '0' to make no changes");
            userChoice = Console.ReadLine().Trim();
        }

        return userChoice;
    }

    internal static MailMessage CreateEmail(string emailAddress)
    {
        var fromEmail = new MailAddress(ConfigurationManager.AppSettings.Get("email"));

        var email = new MailMessage();
        email.From = fromEmail;
        email.To.Add(emailAddress);

        Console.WriteLine("enter email subject:");
        email.Subject = Console.ReadLine().Trim();

        Console.WriteLine("enter email body");
        email.Body = Console.ReadLine().Trim();

        return email;
    }
}
