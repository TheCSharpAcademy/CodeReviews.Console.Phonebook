using Microsoft.EntityFrameworkCore;
using Phonebook.Model;
using System.Configuration;

namespace Phonebook.Data;

internal class PhonebookContext : DbContext
{

    internal DbSet<Contact> Contacts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var dbPath = ConfigurationManager.AppSettings.Get("DbPath");
        optionsBuilder.UseSqlServer(dbPath);
    }

}
