namespace PhoneBook;

class ContactController
{
    private readonly PhoneBookContext phoneBookContext;
    private MessageController? messageController;

    public ContactController(PhoneBookContext phoneBookContext)
    {
        this.phoneBookContext = phoneBookContext;
    }

    public void SetMessageController(MessageController messageController)
    {
        this.messageController = messageController;
    }

    public void ShowList()
    {
        ShowList(null);
    }

    public void ShowList(string? message)
    {
        var contacts = phoneBookContext.Contacts.OrderBy(c => c.Name).ToList();
        var view = new ContactListView(this, contacts);
        view.SetMessage(message);
        view.Show();
    }

    public void ShowDetails(int id)
    {
        ShowDetails(id, null);
    }

    public void ShowDetails(int id, string? message)
    {
        try
        {
            var contact = phoneBookContext.Contacts.Where(c => c.ContactID == id).Single();
            var view = new ContactDetailView(this, contact);
            view.SetMessage(message);
            view.Show();
        }
        catch (InvalidOperationException)
        {
            ShowList($"ERROR - Could not load details for ID {id}");
        }
    }

    public void ShowAdd()
    {
        ShowAdd(null);
    }

    public void ShowAdd(string? message)
    {
        var view = new ContactAddView(this);
        view.SetMessage(message);
        view.Show();
    }

    public void Create(ContactDto dto)
    {
        if (String.IsNullOrEmpty(dto.Name))
        {
            ShowAdd("ERROR - A contact must at least have a name.");
            return;
        }
        var contact = new Contact
        {
            Name = dto.Name,
            Email = dto.Email,
            PhoneNumber = dto.PhoneNumber
        };
        phoneBookContext.Add(contact);
        phoneBookContext.SaveChanges();
        ShowList($"OK - New contact '{contact.Name}' added.");
    }

    public void ShowEdit(int id)
    {
        try
        {
            var contact = phoneBookContext.Contacts.Where(c => c.ContactID == id).Single();
            var view = new ContactEditView(this, contact);
            view.Show();
        }
        catch (InvalidOperationException)
        {
            ShowList($"ERROR - Could not load details for ID {id}");
        }
    }

    public void Update(int id, ContactDto dto)
    {
        if (String.IsNullOrEmpty(dto.Name))
        {
            ShowAdd("ERROR - A contact must at least have a name.");
            return;
        }
        try
        {
            var contact = phoneBookContext.Contacts.Where(c => c.ContactID == id).Single();
            contact.Name = dto.Name;
            contact.Email = dto.Email;
            contact.PhoneNumber = dto.PhoneNumber;
            phoneBookContext.SaveChanges();
            ShowList($"OK - Contact '{contact.Name}' updated.");
        }
        catch (Exception)
        {
            ShowList($"ERROR - Failed to update contact. ID: {id}");
        }
    }

    public void ShowDelete(int id)
    {
        try
        {
            var contact = phoneBookContext.Contacts.Where(c => c.ContactID == id).Single();
            var view = new ContactDeleteView(this, contact);
            view.Show();
        }
        catch (InvalidOperationException)
        {
            ShowList($"ERROR - Could not load details for ID {id}");
        }
    }

    public void Delete(int id)
    {
        try
        {
            var contact = phoneBookContext.Contacts.Where(c => c.ContactID == id).Single();
            phoneBookContext.Remove(contact);
            phoneBookContext.SaveChanges();
            ShowList($"OK - Contact '{contact.Name}' deleted.");
        }
        catch (InvalidOperationException)
        {
            ShowList($"ERROR - Failed to delete contact with '{id}'.");
        }
    }

    public static void ShowExit()
    {
        var view = new ExitView();
        view.Show();
    }

    public void SendMail(Contact contact)
    {
        if (messageController == null)
        {
            throw new InvalidOperationException("Required MessageController missing.");
        }
        messageController.ShowCreateMail(contact);
    }
}