namespace PhoneBookConsole.PhoneBookService;

public interface IContactService
{
    public void ViewAllContacts();

    public void AddNewContact();

    public void EditContact();

    public void DeleteContact();
}