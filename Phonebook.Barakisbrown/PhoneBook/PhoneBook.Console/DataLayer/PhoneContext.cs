namespace PhoneBook.Console.DataLayer;

using Microsoft.EntityFrameworkCore;
using PhoneBook.Console.Model;

public class PhoneContext : DbContext
{
    private readonly string connString = "Server=(LocalDB)\\MyLocalDB;Database=PhoneNumber;Trusted_Connection=True;";
    public DbSet<Contact> Contacts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseSqlServer(connString);

    public int Save()
    {
        return SaveChanges();
    }

    public bool Add(Contact _contact)
    {
        Contacts.Add(_contact);
        return Save() == 1;
    }

    public bool Remove(Contact _contact)
    {
        if (Contacts.Contains(_contact))
        {
            Contacts.Remove(_contact);
            return Save() == 1;
        }
        return false;
    }

    public bool Edit(Contact _oldContact, Contact _newContact)
    {
        var old = Contacts.First(c => c.Id == _oldContact.Id);
        old.Name = _newContact.Name;
        old.Email = _newContact.Email;
        old.PhoneNumber = _newContact.PhoneNumber;
        return Save() == 1;
    }

    public List<Contact> GetContacts()
    {
        return Contacts.Where(c => c.Id != 0).ToList();
    }
}
