using Microsoft.EntityFrameworkCore;

using Phonebook.MartinL_no.Models;

namespace Phonebook.MartinL_no;

internal class ContactsContext : DbContext
{
	public DbSet<Contact> Contacts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
        optionsBuilder.UseSqlServer(
            "Server=127.0.0.1,1433;Initial Catalog=Phonebook;User ID=SA;Password=YourStrong@Passw0rd;Trust Server Certificate=True;"
            );
}
