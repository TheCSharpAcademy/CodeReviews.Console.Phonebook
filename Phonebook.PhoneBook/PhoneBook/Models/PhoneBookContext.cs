using Microsoft.EntityFrameworkCore;
using PhoneBook.Models;

namespace PhoneBook;

public class PhoneBookContext : DbContext
{
    string dbString = System.Configuration.ConfigurationManager.AppSettings.Get("ConnectionString");

    public DbSet<Contact> Contacts { get; set; }

    public string DbPath { get; }

    public PhoneBookContext()
    {
        DbPath = System.Configuration.ConfigurationManager.AppSettings.Get("DbFilePath");
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlServer(dbString);
}