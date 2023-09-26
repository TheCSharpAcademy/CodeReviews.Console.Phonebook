namespace PhoneBook;

class ContactController
{
    private readonly MainController mainController;
    private readonly PhoneBookContext phoneBookContext;

    public ContactController(MainController mainController, PhoneBookContext phoneBookContext)
    {
        this.mainController = mainController;
        this.phoneBookContext = phoneBookContext;
    }

    public void ShowList(Category category)
    {
        ShowList(category, null);
    }

    public void ShowList(Category category, string? message)
    {
        var contacts = category.Contacts.OrderBy(c => c.Name).ToList();
        var view = new ContactListView(this, category, contacts);
        view.SetMessage(message);
        view.Show();
    }

    public void ChangeCategory()
    {
        mainController.ShowCategories();
    }

    public void ShowDetails(Contact contact)
    {
        ShowDetails(contact, null);
    }

    public void ShowDetails(Contact contact, string? message)
    {
        var view = new ContactDetailView(this, contact);
        view.SetMessage(message);
        view.Show();
    }

    public void ShowAdd(Category category)
    {
        ShowAdd(category, null);
    }

    public void ShowAdd(Category category, string? message)
    {
        var view = new ContactAddView(this, category);
        view.SetMessage(message);
        view.Show();
    }

    public void Create(Category category, ContactDto dto)
    {
        if (String.IsNullOrEmpty(dto.Name))
        {
            ShowAdd(category, "ERROR - A contact must at least have a name.");
            return;
        }
        var contact = new Contact
        {
            Name = dto.Name,
            Email = dto.Email,
            PhoneNumber = dto.PhoneNumber,
            MobileNumber = dto.MobileNumber
        };
        category.Contacts.Add(contact);
        phoneBookContext.SaveChanges();
        ShowList(category, $"OK - New contact '{contact.Name}' added.");
    }

    public void ShowEdit(Contact contact)
    {
        var view = new ContactEditView(this, contact);
        view.Show();
    }

    public void Update(Contact contact, ContactDto dto)
    {
        if (String.IsNullOrEmpty(dto.Name))
        {
            ShowAdd(contact.Category, "ERROR - A contact must at least have a name.");
            return;
        }
        try
        {
            contact.Name = dto.Name;
            contact.Email = dto.Email;
            contact.PhoneNumber = dto.PhoneNumber;
            contact.MobileNumber = dto.MobileNumber;
            phoneBookContext.SaveChanges();
            ShowList(contact.Category, $"OK - Contact '{contact.Name}' updated.");
        }
        catch (Exception)
        {
            ShowList(contact.Category, $"ERROR - Failed to update contact. ID: {contact.Id}");
        }
    }

    public void ShowDelete(Contact contact)
    {
        var view = new ContactDeleteView(this, contact);
        view.Show();
    }

    public void Delete(Contact contact)
    {
        phoneBookContext.Remove(contact);
        phoneBookContext.SaveChanges();
        ShowList(contact.Category, $"OK - Contact '{contact.Name}' deleted.");
    }

    public static void ShowExit()
    {
        var view = new ExitView();
        view.Show();
    }

    public void SendMail(Contact contact)
    {
        mainController.SendMail(contact);
    }

    public void SendSms(Contact contact)
    {
        mainController.SendSms(contact);
    }
}