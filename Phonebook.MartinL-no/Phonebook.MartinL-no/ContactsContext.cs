using Microsoft.EntityFrameworkCore;
using System.Configuration;

using Phonebook.MartinL_no.Models;

namespace Phonebook.MartinL_no;

internal class ContactsContext : DbContext
{
	public DbSet<Contact> Contacts { get; set; }
    public DbSet<Category> Categories { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
        optionsBuilder.UseSqlServer(
            ConfigurationManager.AppSettings.Get("connString")
        );
}
