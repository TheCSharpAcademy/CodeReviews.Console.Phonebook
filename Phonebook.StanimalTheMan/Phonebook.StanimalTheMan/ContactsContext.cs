using Microsoft.EntityFrameworkCore;
using Phonebook.StanimalTheMan.Models;

namespace Phonebook.StanimalTheMan;

internal class ContactsContext : DbContext
{
    public DbSet<Contact> Contacts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder
        optionsBuilder) => optionsBuilder
         .UseSqlServer($"Server=JiggyLatitude;Database=Test;Integrated Security=True;Encrypt=False"); // probably store in env file or something in real app, don't use Encrypt=False shortcut to navigate certificate error
}
