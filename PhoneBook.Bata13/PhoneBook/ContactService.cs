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

        if (contact == null)
        {
            Console.WriteLine("Can't update as there are no contacts available to update.");
            Helper.waitUserToPressAnyKeyToContinue();
        }

        else
        {
            Helper.AskAndUpdateName(contact);
            Helper.AskAndUpdateEmail(contact);
            Helper.AskAndUpdatePhoneNumber(contact);              

            ContactController.UpdateContact(contact);

            Helper.waitUserToPressAnyKeyToContinue();
        }
    }
    static internal Contact GetContactOptionInput()
    {
        var contacts = ContactController.GetContacts();

        if (contacts.Count == 0)
        {
            Console.WriteLine("Passing null as there are no contacts available to update.");
            return null;
        }

        else
        {
            var contactsArray = contacts.Select(x => x.Name).ToArray();
            var option = AnsiConsole.Prompt(new SelectionPrompt<string>()
                .Title("Choose Contact")
                .AddChoices(contactsArray));

            var id = contacts.Single(x => x.Name == option).Id;
            var contact = ContactController.GetContactById(id);

            return contact;
        }
    }        
}
