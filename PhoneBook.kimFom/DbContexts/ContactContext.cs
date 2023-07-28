using System.Configuration;
using Microsoft.EntityFrameworkCore;
using PhoneBookConsole.Models;

namespace PhoneBookConsole.DbContexts;

public class ContactContext : DbContext
{
    public DbSet<Contact> Contacts { get; set; }
    private readonly string connectionString = ConfigurationManager.AppSettings.Get("connectionString");

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseSqlServer(connectionString);
    }
}