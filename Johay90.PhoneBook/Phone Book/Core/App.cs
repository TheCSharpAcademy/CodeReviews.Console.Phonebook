public class App
{
    private UserInput _userInput;
    private ContactRepository _contactRepository;
    private ContactController _contactController;
    private List<Contact> _contacts;

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
            _contacts = _contactController.GetAll();
            Contact contact;

            switch (option)
            {
                case MainMenuOptions.Add:
                    contact = _userInput.Add();
                    if (_contacts.Count(n => n.Name == contact.Name) == 0) _contactController.Add(contact);
                    else _userInput.InvalidName();

                    break;
                case MainMenuOptions.Update:

                    if (_contacts.Count > 0)
                    {
                        contact = _userInput.PickAContact(_contacts);
                        contact = GetUpdatedRecord(contact);
                        _contactController.Update(contact);
                    }
                    else _userInput.NoRecords();

                    break;
                case MainMenuOptions.Delete:
                    if (_contacts.Count > 0)
                    {
                        contact = _userInput.PickAContact(_contacts);
                        _contactController.Remove(contact);
                    }
                    else _userInput.NoRecords();
                    break;
                case MainMenuOptions.ViewByName:

                    if (_contacts.Count > 0)
                    {
                        contact = _userInput.PickAContact(_contacts);
                        _userInput.DisplayContact(contact);
                    }
                    else _userInput.NoRecords();

                    break;
                case MainMenuOptions.ViewAll:

                    if (_contacts.Count > 0) _userInput.DisplayContacts(_contacts);
                    else _userInput.NoRecords();
                    break;
                case MainMenuOptions.SendMessage:
                    // Currently, only the Email service is available for messaging. However, if we decide to
                    // implement additional messaging services in the future (e.g., SMS), we can create an
                    // enum to represent the different services and pass it as a generic parameter to the
                    // Menu<T> method. This way, we can prompt the user to select their preferred messaging
                    // service. To add a new service, simply create a class that implements the IMessagingService
                    // interface, such as SMSMessaging with the appropriate parameters.
                    if (_contacts.Count > 0)
                    {
                        var selectedContact = _userInput.PickAContact(_contacts);
                        if (selectedContact.EmailAddresses.Any())
                        {
                            var emailAddress = _userInput.SelectItemFromList(selectedContact.EmailAddresses, "Pick an email to send to");
                            var message = _userInput.SendMessage();
                            SendMessageToContact(selectedContact, message.Title, message.Body);
                        }
                        else
                        {
                            _userInput.NoRecords();
                        }
                    }
                    else
                    {
                        _userInput.NoRecords();
                    }
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
                var tempName = _userInput.UpdateName();

                if (_contacts.Count(n => n.Name == tempName) == 0)
                {
                    contact.Name = tempName;
                }
                else
                {
                    _userInput.InvalidName();
                }

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

    private void SendMessageToContact(Contact contact, string title, string body)
    {
        var emailService = new EmailService();
        var messagingService = new MessagingService(emailService);
        messagingService.Send(contact, title, body);
    }

}
