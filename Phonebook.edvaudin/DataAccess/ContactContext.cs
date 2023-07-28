using Microsoft.EntityFrameworkCore;
using Phonebook.Models;

namespace Phonebook.DataAccess;

public class ContactContext : DbContext
{
    public DbSet<Contact> Contacts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Initial Catalog=flashcardapp;Integrated Security=True");
    }
}
