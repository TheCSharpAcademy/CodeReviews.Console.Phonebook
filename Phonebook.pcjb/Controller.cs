using Microsoft.VisualBasic;

namespace PhoneBook;

class Controller
{
    private PhoneBookContext phoneBookContext;

    public Controller(PhoneBookContext phoneBookContext)
    {
        this.phoneBookContext = phoneBookContext;
    }

    public void ShowMenu()
    {
        ShowMenu(null);
    }

    public void ShowMenu(string? message)
    {
        var view = new MenuView(this);
        view.SetMessage(message);
        view.Show();
    }

    public void ShowList()
    {
        using var db = new PhoneBookContext();
        var contacts = db.Contacts.OrderBy(c => c.Name).ToList();
        var view = new ListView(this, contacts);
        view.Show();
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
        ShowMenu($"OK - New contact '{contact.Name}' added.");
    }

    public void ShowEdit()
    {
        var view = new EditView(this);
        view.Show();
    }

    public void ShowDelete()
    {
        var view = new DeleteView(this);
        view.Show();
    }

    public void ShowExit()
    {
        var view = new ExitView();
        view.Show();
    }
}