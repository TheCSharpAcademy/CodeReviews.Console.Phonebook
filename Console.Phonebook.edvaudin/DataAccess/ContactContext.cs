using Microsoft.EntityFrameworkCore;
using Phonebook.Models;

namespace Phonebook.DataAccess;

public class ContactContext : DbContext
{
    public DbSet<Contact> Contacts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Data Source=(LocalDb)\\MyLocalDb;Initial Catalog=phonebook;Integrated Security=True");
    }
}
