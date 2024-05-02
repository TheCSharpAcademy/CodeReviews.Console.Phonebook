using Microsoft.EntityFrameworkCore;
using Phonebook.samggannon.Models;
using System.Configuration;

namespace Phonebook.samggannon;

internal class ContactContext : DbContext
{
    public DbSet<Contact> Contacts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(ConfigurationManager.ConnectionStrings["DataConnection"].ConnectionString);
    }

}
