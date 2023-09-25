namespace PhoneBook;

class Controller
{
    private PhoneBookContext phoneBookContext;

    public Controller(PhoneBookContext phoneBookContext)
    {
        this.phoneBookContext = phoneBookContext;
    }

    public void ShowList()
    {
        ShowList(null);
    }

    public void ShowList(string? message)
    {
        using var db = new PhoneBookContext();
        var contacts = db.Contacts.OrderBy(c => c.Name).ToList();
        var view = new ListView(this, contacts);
        view.SetMessage(message);
        view.Show();
    }

    public void ShowDetails(int id)
    {
        using var db = new PhoneBookContext();
        try
        {
            var contact = db.Contacts.Where(c => c.ContactID == id).Single();
            var view = new DetailView(this, contact);
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
        var view = new AddView(this);
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
        using var db = new PhoneBookContext();
        db.Add(contact);
        db.SaveChanges();
        ShowList($"OK - New contact '{contact.Name}' added.");
    }

    public void ShowEdit(int id)
    {
        using var db = new PhoneBookContext();
        try
        {
            var contact = db.Contacts.Where(c => c.ContactID == id).Single();
            var view = new EditView(this, contact);
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
        using var db = new PhoneBookContext();
        try
        {
            var contact = db.Contacts.Where(c => c.ContactID == id).Single();
            contact.Name = dto.Name;
            contact.Email = dto.Email;
            contact.PhoneNumber = dto.PhoneNumber;
            db.SaveChanges();
            ShowList($"OK - Contact '{contact.Name}' updated.");
        }
        catch (Exception)
        {
            ShowList($"ERROR - Failed to update contact. ID: {id}");
        }
    }

    public void ShowDelete(int id)
    {
        using var db = new PhoneBookContext();
        try
        {
            var contact = db.Contacts.Where(c => c.ContactID == id).Single();
            var view = new DeleteView(this, contact);
            view.Show();
        }
        catch (InvalidOperationException)
        {
            ShowList($"ERROR - Could not load details for ID {id}");
        }
    }

    public void Delete(int id)
    {
        using var db = new PhoneBookContext();
        try
        {
            var contact = db.Contacts.Where(c => c.ContactID == id).Single();
            db.Remove(contact);
            db.SaveChanges();
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
}