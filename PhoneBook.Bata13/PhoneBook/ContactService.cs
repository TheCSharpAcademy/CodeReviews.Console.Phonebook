using Spectre.Console;

namespace PhoneBook;
internal class ContactServce
{
    internal static void AddContact()
    {
        var contact = new Contact();

        contact.Name = AnsiConsole.Ask<string>("Contact's name:");

        string validEmail = Validation.GetValidEmailFromUser();         
        
        contact.Email = validEmail;

        string validPhoneNumber = Validation.GetValidPhoneNumberFromUser();

        contact.PhoneNumber = validPhoneNumber;

        ContactController.InsertContactInDatabase(contact);

        Helper.waitUserToPressAnyKeyToContinue();
    }    
    internal static void DeleteContact()
    {
        var contact = GetContactOptionInput();
        ContactController.DeleteContact(contact);

        Helper.waitUserToPressAnyKeyToContinue();
    }
    internal static void UpdateContact()
    {
        var contact = GetContactOptionInput();

        contact.Name = AnsiConsole.Confirm("Update name?")
            ? AnsiConsole.Ask<string>("Contact's new name:")
            : contact.Name;

        string contactEmail;

        contactEmail = AnsiConsole.Confirm("Update Email?")
            ? AnsiConsole.Ask<string>("Contact's new Email:")
            : contact.Email;

        string validEmail = Validation.GetValidEmailToUpdateFromUser(contactEmail);

        string contactPhoneNumber;

        contactPhoneNumber = AnsiConsole.Confirm("Update PhoneNumber?")
            ? AnsiConsole.Ask<string>("Contact's new PhoneNumber:")
            : contact.PhoneNumber;

        string validPhoneNumber = Validation.GetValidPhoneNumberToUpdateFromUser(contactPhoneNumber);

        ContactController.UpdateContact(contact);

        Helper.waitUserToPressAnyKeyToContinue();
    }
    static internal Contact GetContactOptionInput()
    {
        var contacts = ContactController.GetContacts();
        var contactsArray = contacts.Select(x => x.Name).ToArray();
        var option = AnsiConsole.Prompt(new SelectionPrompt<string>()
            .Title("Choose Contact")
            .AddChoices(contactsArray));
        var id = contacts.Single(x => x.Name == option).Id;
        var contact = ContactController.GetContactById(id);

        return contact;
    }        
}
