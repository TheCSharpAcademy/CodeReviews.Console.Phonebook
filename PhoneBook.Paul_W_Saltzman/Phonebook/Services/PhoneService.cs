using Phonebook.Controllers;
using Phonebook.Models;
using PhoneNumbers;
using Spectre.Console;

namespace Phonebook.Services;

internal class PhoneService
{
    internal static Contact AddPhone(Contact contact)
    {
        PhoneNumber newPhoneNumber = new PhoneNumber();
        string formattedPhoneNumber = null;
        bool phoneValid = false;
        bool duplicate = true;
        while (!phoneValid || duplicate) 
        {
            string UserInput = AnsiConsole.Ask<string>("Please enter your full phone number including country code.");

            newPhoneNumber = Validators.ParsePhoneNumber(UserInput);
            phoneValid = Validators.IsValidPhone(newPhoneNumber);
            duplicate = Validators.IsDuplicatePhone(newPhoneNumber, contact.PhoneNumbers);
            formattedPhoneNumber = Validators.FormatNumber(newPhoneNumber);
            if (!phoneValid)
            {
                Console.WriteLine($"{formattedPhoneNumber} is an invalid entry");
            }
            if (duplicate)
            {
                Console.WriteLine($"{formattedPhoneNumber} is already entered for {contact.FirstName} {contact.LastName}");
            }
            if (!phoneValid || duplicate)
            {
                Console.WriteLine("Press enter to continue. Press X to Go Back.");
                ConsoleKeyInfo keyInfo = Console.ReadKey();
                if (char.ToUpper(keyInfo.KeyChar) == 'X')
                {
                    return contact;
                }
            }
        }
        Phone newPhone = new Phone(formattedPhoneNumber,contact.ContactId);
        newPhone = PhoneController.AddPhone(newPhone);
        contact.PhoneNumbers.Add(newPhone);
        return contact;

    }

    internal static void DeletePhone(Phone phoneToDelete)
    {
        bool delete = false;
        delete = AnsiConsole.Confirm($"Delete Email: {phoneToDelete.PhoneNumber}");

        if (delete)
        {
            PhoneController.DeletePhone(phoneToDelete);
        }
    }

    internal static Phone GetPhoneOptionInput(Contact contact)
    {
        var smsArray = contact.PhoneNumbers
            .Select(x => $"{x.PhoneNumber}").ToArray();

        var option = AnsiConsole.Prompt(new SelectionPrompt<string>()
            .Title("Choose Phone Number")
            .AddChoices(smsArray));

        Phone selectedPhone = contact.PhoneNumbers.FirstOrDefault(x => $"{x.PhoneNumber}" == option);
        return selectedPhone;
    }
   
    internal static Contact RemovePhoneFromContact(Phone phoneToDelete, Contact contact)
    {
        var phoneToRemove = contact.PhoneNumbers.FirstOrDefault(e => e.PhoneId == phoneToDelete.PhoneId);

        if (phoneToRemove != null)
        {
            contact.PhoneNumbers.Remove(phoneToRemove);
        }

        return contact;
    }

}
