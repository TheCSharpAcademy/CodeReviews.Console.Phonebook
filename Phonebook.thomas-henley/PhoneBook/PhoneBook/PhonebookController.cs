using Microsoft.Extensions.Configuration;

namespace PhoneBook;

public class PhonebookController(
    IConfigurationRoot config,
    PhonebookDbContext db,
    SpectreConsole ui,
    ContactValidator validator)
{
    public void Run()
    {
        while (true)
        {
            ui.Clear();
            ui.Welcome();
            
            var contacts = db.Contacts.OrderBy(c => c.Name);

            var selection = ui.Menu(contacts.Select(c => c.Name).ToList());

            switch (selection)
            {
                case "Add Contact":
                    AddContact();
                    break;
                case "Exit":
                    ui.Goodbye();
                    return;
                default:
                    var contact = contacts.First(c => c.Name == selection);
                    ContactMenu(contact);
                    break;
            }
        }
    }

    private void AddContact()
    {
        Contact contact = new()
        {
            Name = ui.NewName(),
            Email = ui.NewEmail(),
            Phone = ui.NewPhone()
        };
        
        db.Contacts.Add(contact);
        db.SaveChanges();
    }

    private void ContactMenu(Contact contact)
    {
        ui.ContactDetails(contact);
        var selection = ui.ContactMenu();

        switch (selection)
        {
            case "Edit":
                EditContact(contact);
                break;
            case "Delete":
                DeleteContact(contact);
                break;
            case "Return":
                return;
        }
    }

    private void EditContact(Contact contact)
    {
        while (true)
        {
            switch (ui.EditContactMenu())
            {
                case "Name":
                    contact.Name = ui.NewName();
                    break;
                case "Email":
                    contact.Email = ui.NewEmail();
                    break;
                case "Phone":
                    contact.Phone = ui.NewPhone();
                    break;
                case "Return":
                    return;
            }
            
            db.Contacts.Update(contact);
            db.SaveChanges();
            
            ui.ContactDetails(contact);
        }
    }

    private void DeleteContact(Contact contact)
    {
        if (!ui.ConfirmDelete(contact)) return;
        
        db.Contacts.Remove(contact);
        db.SaveChanges();
    }
}