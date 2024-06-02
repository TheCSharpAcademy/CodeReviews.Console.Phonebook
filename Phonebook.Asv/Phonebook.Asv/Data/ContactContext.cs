using Phonebook.Models;
using Microsoft.EntityFrameworkCore;

namespace Phonebook.Data;

internal class ContactContext: DbContext
{
    public DbSet<Contact> Contacts { get; set; } = null!;
    public DbSet<Category> Categories { get; set; } = null!;
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"Your_connection_string");
    }

}
