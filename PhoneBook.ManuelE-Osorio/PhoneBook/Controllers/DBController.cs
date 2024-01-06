using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace PhoneBookProgram;

public class DBController: IDisposable
{
    private PhoneBookContext db;

    public DBController()
    {
        db = new();
    }

    public List<Contact> GetContacts()
    {
        List<Contact> contacts = [.. db.Contacts];
        return contacts;
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }
}