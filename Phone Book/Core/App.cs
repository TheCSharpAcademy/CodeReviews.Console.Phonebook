using Microsoft.IdentityModel.Tokens;
using Spectre.Console;

public class App
{
    private UserInput _userInput;
    private ContactRepository _contactRepository;
    private ContactController _contactController;

    public App()
    {
        _userInput = new UserInput();
        _contactRepository = new ContactRepository();
        _contactController = new ContactController(_contactRepository);
    }

    public void Run()
    {
        while (true)
        {
            var option = _userInput.Menu<MainMenuOptions>("Please choose an option:");

            // In a large-scale application or database, it's crucial to retrieve only the specific data we need instead of querying for all records.
            // This approach significantly improves efficiency and performance by reducing the amount of data transferred and processed.
            // In this case, we should consider using more specific queries or filters to retrieve only the necessary contacts.
            // However, for our small data this has no impact.
            var contacts = _contactController.GetAll();
            Contact contact;

            switch (option)
            {
                case MainMenuOptions.Add:
                    contact = _userInput.Add();
                    _contactController.Add(contact);
                    break;
                case MainMenuOptions.Update:

                    if (contacts.Count > 0)
                    {
                        contact = _userInput.PickAContact(contacts);
                        contact = GetUpdatedRecord(contact);
                        _contactController.Update(contact);
                    }
                    else _userInput.NoRecords();

                    break;
                case MainMenuOptions.Delete:
                    break;
                case MainMenuOptions.ViewByName:

                    if (contacts.Count > 0)
                    {
                        contact = _userInput.PickAContact(contacts);
                        _userInput.DisplayContact(contact);
                    }
                    else _userInput.NoRecords();

                    break;
                case MainMenuOptions.ViewAll:

                    if (contacts.Count > 0) _userInput.DisplayContacts(contacts);
                    else _userInput.NoRecords();

                    break;
            }
        }
    }

    private Contact GetUpdatedRecord(Contact contact)
    {
        var updateOptions = _userInput.Menu<UpdateDetailsOptions>("How would you like to update this contact?");
        switch (updateOptions)
        {
            case UpdateDetailsOptions.Name:
                contact.Name = _userInput.UpdateName();
                // TODO verify unique names
                break;
            case UpdateDetailsOptions.PhoneNumber:
                contact = UpdateNumber(contact);
                break;
            case UpdateDetailsOptions.Email:
                contact = UpdateEmail(contact);
                break;
        }

        return contact;
    }

    private Contact UpdateNumber(Contact contact)
    {
        var updateOptions = _userInput.Menu<UpdateOption>("How would you like to update this?");
        PhoneNumber number;
        switch (updateOptions)
        {
            case UpdateOption.Insert:
                number = new PhoneNumber { Number = _userInput.UpdateNumber() };
                contact.PhoneNumbers.Add(number);
                break;
            case UpdateOption.Edit:
                number = _userInput.SelectItemFromList(contact.PhoneNumbers, "Pick a phone number to edit");
                number.Number = _userInput.UpdateNumber();
                break;
            case UpdateOption.Delete:
                if (contact.PhoneNumbers.Count > 1)
                {
                    number = _userInput.SelectItemFromList(contact.PhoneNumbers, "Pick a phone number to Delete");
                    contact.PhoneNumbers.Remove(number);
                }
                else _userInput.NoRecords();
                break;
        }
        return contact;
    }

    private Contact UpdateEmail(Contact contact)
    {
        var updateOptions = _userInput.Menu<UpdateOption>("How would you like to update this?");
        Email email;
        switch (updateOptions)
        {
            case UpdateOption.Insert:
                email = new Email { EmailAddress = _userInput.UpdateEmail() };
                contact.EmailAddresses.Add(email);
                break;
            case UpdateOption.Edit:
                if (contact.EmailAddresses.Count > 0)
                {
                    email = _userInput.SelectItemFromList(contact.EmailAddresses, "Pick a email to edit");
                    email.EmailAddress = _userInput.UpdateEmail();
                }
                else _userInput.NoRecords();
                break;
            case UpdateOption.Delete:
                if (contact.EmailAddresses.Count > 0)
                {
                    email = _userInput.SelectItemFromList(contact.EmailAddresses, "Pick a email to delete");
                    contact.EmailAddresses.Remove(email);
                }
                else _userInput.NoRecords();
                break;
        }
        return contact;
    }


}
